using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

using Contoso.IoT.UWP.Models;

namespace Contoso.IoT.UWP.Services
{
    public static class SampleMetricsService
    {
        private static IEnumerable<CarMetric> AllMetrics()
        {
            var data = new ObservableCollection<CarMetric>
            {
                new CarMetric
                {
                VehicleID = "JEEPCompass",
                DriverID = "MDP",
                RecordDate = new DateTime(2018, 06, 24).ToLongDateString(),
                Latitude = 43.9016461,
                Longitude =10.2191949,
                EngineLoad = 20,
                ShortTermFuelBank1  = 602,
                LongTermFuelBank1  =650,
                RPM  =3500,
                Speed  =0,
                InsideTemperature  =28,
                MAFAirFlowRate  =10,
                ThrottlePosition  =0,
                Runtime  =0,
                DistancewithML  =0,
                CommandedEGR  =0,
                EGRError  =0,
                BarometricPressure  =0,
                RelativeThrottlePosition  =0,
                OutsideTemperature  =30,
                EngineFuelRate  =0
                },
                new CarMetric
                {
                VehicleID = "JEEPCompass",
                DriverID = "MDP",
                RecordDate = new DateTime(2018, 06, 24).ToLongDateString(),
                Latitude = 43.9023113,
                Longitude =10.2189589,
                EngineLoad = 22,
                ShortTermFuelBank1  = 601,
                LongTermFuelBank1  =648,
                RPM  =3700,
                Speed  =0,
                InsideTemperature  =28,
                MAFAirFlowRate  =10,
                ThrottlePosition  =0,
                Runtime  =0,
                DistancewithML  =0,
                CommandedEGR  =0,
                EGRError  =0,
                BarometricPressure  =0,
                RelativeThrottlePosition  =0,
                OutsideTemperature  =30,
                EngineFuelRate  =0
                },
                new CarMetric
                {
                VehicleID = "JEEPCompass",
                DriverID = "MDP",
                RecordDate = new DateTime(2018, 06, 24).ToLongDateString(),
                Latitude = 43.90715,
                Longitude =10.2153647,
                EngineLoad = 22,
                ShortTermFuelBank1  = 601,
                LongTermFuelBank1  =647,
                RPM  =3500,
                Speed  =0,
                InsideTemperature  =27,
                MAFAirFlowRate  =10,
                ThrottlePosition  =0,
                Runtime  =0,
                DistancewithML  =0,
                CommandedEGR  =0,
                EGRError  =0,
                BarometricPressure  =0,
                RelativeThrottlePosition  =0,
                OutsideTemperature  =30,
                EngineFuelRate  =0
                },
                new CarMetric
                {
                VehicleID = "JEEPCompass",
                DriverID = "MDP",
                RecordDate = new DateTime(2018, 06, 24).ToLongDateString(),
                Latitude = 43.9104331,
                Longitude =10.2239478,
                EngineLoad = 25,
                ShortTermFuelBank1  = 600,
                LongTermFuelBank1  =645,
                RPM  =4150,
                Speed  =0,
                InsideTemperature  =28,
                MAFAirFlowRate  =10,
                ThrottlePosition  =0,
                Runtime  =0,
                DistancewithML  =0,
                CommandedEGR  =0,
                EGRError  =0,
                BarometricPressure  =0,
                RelativeThrottlePosition  =0,
                OutsideTemperature  =30,
                EngineFuelRate  =0
                },
                new CarMetric
                {
                VehicleID = "JEEPCompass",
                DriverID = "MDP",
                RecordDate = new DateTime(2018, 06, 24).ToLongDateString(),
                Latitude = 43.9273739,
                Longitude =10.2202463,
                EngineLoad = 25,
                ShortTermFuelBank1  = 598,
                LongTermFuelBank1  =643,
                RPM  =4050,
                Speed  =0,
                InsideTemperature  =28,
                MAFAirFlowRate  =10,
                ThrottlePosition  =0,
                Runtime  =0,
                DistancewithML  =0,
                CommandedEGR  =0,
                EGRError  =0,
                BarometricPressure  =0,
                RelativeThrottlePosition  =0,
                OutsideTemperature  =30,
                EngineFuelRate  =0
                },
                new CarMetric
                {
                VehicleID = "JEEPCompass",
                DriverID = "MDP",
                RecordDate = new DateTime(2018, 06, 24).ToLongDateString(),
                Latitude = 43.9347768,
                Longitude =10.2150965,
                EngineLoad = 24,
                ShortTermFuelBank1  = 597,
                LongTermFuelBank1  =642,
                RPM  =3950,
                Speed  =0,
                InsideTemperature  =28,
                MAFAirFlowRate  =10,
                ThrottlePosition  =0,
                Runtime  =0,
                DistancewithML  =0,
                CommandedEGR  =0,
                EGRError  =0,
                BarometricPressure  =0,
                RelativeThrottlePosition  =0,
                OutsideTemperature  =30,
                EngineFuelRate  =0
                },
                new CarMetric
                {
                VehicleID = "JEEPCompass",
                DriverID = "MDP",
                RecordDate = new DateTime(2018, 06, 24).ToLongDateString(),
                Latitude = 43.9560843,
                Longitude =10.1969647,
                EngineLoad = 19,
                ShortTermFuelBank1  = 596,
                LongTermFuelBank1  =642,
                RPM  =3780,
                Speed  =0,
                InsideTemperature  =28,
                MAFAirFlowRate  =10,
                ThrottlePosition  =0,
                Runtime  =0,
                DistancewithML  =0,
                CommandedEGR  =0,
                EGRError  =0,
                BarometricPressure  =0,
                RelativeThrottlePosition  =0,
                OutsideTemperature  =30,
                EngineFuelRate  =0
                },
                new CarMetric
                {
                VehicleID = "JEEPCompass",
                DriverID = "MDP",
                RecordDate = new DateTime(2018, 06, 24).ToLongDateString(),
                Latitude = 44.1505015,
                Longitude = 9.9052048,
                EngineLoad = 26,
                ShortTermFuelBank1  = 594,
                LongTermFuelBank1  =641,
                RPM  =4210,
                Speed  =0,
                InsideTemperature  =28,
                MAFAirFlowRate  =10,
                ThrottlePosition  =0,
                Runtime  =0,
                DistancewithML  =0,
                CommandedEGR  =0,
                EGRError  =0,
                BarometricPressure  =0,
                RelativeThrottlePosition  =0,
                OutsideTemperature  =30,
                EngineFuelRate  =0
                },
                new CarMetric
                {
                VehicleID = "JEEPCompass",
                DriverID = "MDP",
                RecordDate = new DateTime(2018, 06, 24).ToLongDateString(),
                Latitude = 44.8609221,
                Longitude = 10.2338827,
                EngineLoad = 29,
                ShortTermFuelBank1  = 591,
                LongTermFuelBank1  =638,
                RPM  =4250,
                Speed  =0,
                InsideTemperature  =28,
                MAFAirFlowRate  =10,
                ThrottlePosition  =0,
                Runtime  =0,
                DistancewithML  =0,
                CommandedEGR  =0,
                EGRError  =0,
                BarometricPressure  =0,
                RelativeThrottlePosition  =0,
                OutsideTemperature  =30,
                EngineFuelRate  =0
                },
                new CarMetric
                {
                VehicleID = "JEEPCompass",
                DriverID = "MDP",
                RecordDate = new DateTime(2018, 06, 24).ToLongDateString(),
                Latitude = 45.4286814,
                Longitude = 9.2478275,
                EngineLoad = 27,
                ShortTermFuelBank1  = 589,
                LongTermFuelBank1  =636,
                RPM  =4180,
                Speed  =0,
                InsideTemperature  =28,
                MAFAirFlowRate  =10,
                ThrottlePosition  =0,
                Runtime  =0,
                DistancewithML  =0,
                CommandedEGR  =0,
                EGRError  =0,
                BarometricPressure  =0,
                RelativeThrottlePosition  =0,
                OutsideTemperature  =30,
                EngineFuelRate  =0
                },
            };

            return data;
        }

        // TODO WTS: Remove this once your MasterDetail pages are displaying real data
        public static async Task<IEnumerable<CarMetric>> GetMetricsModelDataAsync()
        {
            await Task.CompletedTask;

            return AllMetrics();
        }

        // TODO WTS: Remove this once your grid page is displaying real data
        public static ObservableCollection<CarMetric> GetGridMetricsData()
        {
            return new ObservableCollection<CarMetric>(AllMetrics());
        }
    }
}
