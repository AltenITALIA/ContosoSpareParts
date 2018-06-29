using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using CustomVisionClient.Exceptions;
using CustomVisionClient.Models;
using Newtonsoft.Json;

namespace CustomVisionClient
{
    public class OnlineClassifier
    {
        private readonly HttpClient client;

        public const string DefaultCustomVisionEndPoint = "https://southcentralus.api.cognitive.microsoft.com/customvision/v2.0/";

        public string PredictionKey { get; set; }

        public Guid ProjectId { get; set; }

        public string CustomVisionEndpoint
        {
            get => client.BaseAddress.ToString();
            set => client.BaseAddress = new Uri(value.EndsWith("/") ? value : value += "/");
        }

        public OnlineClassifier(string predictionKey = null, Guid projectId = default, string customVisionEndpoint = DefaultCustomVisionEndPoint)
        {
            client = new HttpClient();
            Initialize(predictionKey, projectId, customVisionEndpoint);
        }

        public void Initialize(string predictionKey, Guid projectId, string customVisionEndpoint = DefaultCustomVisionEndPoint)
        {
            PredictionKey = predictionKey;
            ProjectId = projectId;
            CustomVisionEndpoint = customVisionEndpoint;
        }

        public async Task<IEnumerable<Recognition>> RecognizeAsync(Stream image, Guid? iterationId = null)
        {
            var request = CreatePredictRequest(ProjectId, iterationId);

            request.Content = new StreamContent(image);
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

            var predictions = await SendRequestAsync<ImagePredictionResult>(request);
            var results = predictions.Predictions.Select(p => new Recognition
            {
                Tag = p.Tag,
                Probability = p.Probability
            }).ToList();

            return results;
        }

        public async Task<IEnumerable<Recognition>> RecognizeAsync(string predictionKey, Guid projectId, Stream image, Guid? iterationId = null, string customVisionEndpoint = DefaultCustomVisionEndPoint)
        {
            Initialize(predictionKey, projectId, customVisionEndpoint);
            var results = await RecognizeAsync(image, iterationId);

            return results;
        }

        private HttpRequestMessage CreatePredictRequest(Guid projectId, Guid? iterationId)
        {
            var endpoint = $"Prediction/{projectId}/image?iterationId={iterationId}";
            var request = new HttpRequestMessage(HttpMethod.Post, endpoint);
            request.Headers.Add("Prediction-Key", PredictionKey);

            return request;
        }

        private async Task<T> SendRequestAsync<T>(HttpRequestMessage request)
        {
            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var responseContentString = await response.Content.ReadAsStringAsync();
                var content = JsonConvert.DeserializeObject<T>(responseContentString);

                return content;
            }
            else
            {
                var exception = new ClassifierException(response.ReasonPhrase);
                throw exception;
            }
        }
    }
}
