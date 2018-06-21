using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpareParts.Mobile.Common
{
    public static class ServiceKeys
    {
#if DEBUG
        public const string CustomVisionPredictionKey = "740625682c404203a490c8e49a69b094";
        public const string CustomVisionProjectId = "7794d59e-f452-4bf5-ae9a-c4a4be42f6a3";
#else
        public const string CustomVisionPredictionKey = "";
        public const string CustomVisionProjectId = "";
#endif
    }
}
