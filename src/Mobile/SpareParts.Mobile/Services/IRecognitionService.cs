using CustomVisionClient.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpareParts.Mobile.Services
{
    public interface IRecognitionService
    {
        Task<Recognition> RecognizeAsync(Stream image);
    }
}
