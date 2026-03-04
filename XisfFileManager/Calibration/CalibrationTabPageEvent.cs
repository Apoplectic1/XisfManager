using XisfFileManager.Globals;

namespace XisfFileManager
{
    public class CalibrationTabPageValues
    {

        public eMessageMode MessageMode { get; set; } = eMessageMode.KEEP;
        public string FileName { get; set; } = string.Empty;
        public int Progress { get; set; }
        public int ProgressMax { get; set; } = 100;
        public int TotalFiles { get; set; }
        public int TotalMatchedCalibrationFiles { get; set; }
        public int TotalUniqueDarkCalibrationFiles { get; set; }
        public int TotalUniqueFlatCalibrationFiles { get; set; }
        public int TotalUniqueBiasCalibrationFiles { get; set; }
        public string MatchCalibrationMessage { get; set; } = string.Empty;
    }

    public class CalibrationTabPageEvent
    {
        public static event DataReceivedEvent? CalibrationTabPage_InvokeEvent;

        public static void TransmitData(CalibrationTabPageValues data)
        {
            CalibrationTabPage_InvokeEvent?.Invoke(data); // Raise the event.
        }
    }
}
