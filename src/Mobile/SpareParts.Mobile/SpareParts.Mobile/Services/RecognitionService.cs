using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomVisionClient;
using CustomVisionClient.Models;

namespace SpareParts.Mobile.Services
{
    public class RecognitionService : IRecognitionService
    {
        private const double MINUMUM_PROBABILITY = 0.8D;

        private readonly OnlineClassifier classifer;
        private readonly ISettingsService settingsService;

        public RecognitionService(ISettingsService settingsService)
        {
            classifer = new OnlineClassifier();
            this.settingsService = settingsService;
        }

        public async Task<Recognition> RecognizeAsync(Stream image)
        {
            var results = await classifer.RecognizeAsync(settingsService.PredictionKey, Guid.Parse(settingsService.ProjectId), image);
            var bestResult = results.FirstOrDefault(r => r.Probability > MINUMUM_PROBABILITY);

            return bestResult;
        }
    }
}
