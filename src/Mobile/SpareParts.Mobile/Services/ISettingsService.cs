﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpareParts.Mobile.Services
{
    public interface ISettingsService
    {
        string PredictionKey { get; set; }

        string ProjectId { get; set;  }
    }
}
