using MathNet.Numerics;
using MathNet.Numerics.Statistics;
using System;
using System.Collections.Generic;
using System.Linq;
using XisfFileManager.Enums;
using XisfFileManager.Files;

namespace XisfFileManager.Calculations
{
    public class ImageCalculations
    {
        public List<double> mFocuserPositionList { get; set; }
        public List<double> mFocuserTemperatureList { get; set; }
        public List<double> mAmbientTemperatureList { get; set; }

        public ImageCalculations()
        {
            mFocuserPositionList = new List<double>();
            mFocuserTemperatureList = new List<double>();
            mAmbientTemperatureList = new List<double>();
        }

        public void Clear()
        {
            mFocuserPositionList.Clear();
            mFocuserTemperatureList.Clear();
            mAmbientTemperatureList.Clear();
        }

        public void BuildImageParameterValueLists(XisfFile xFile)
        {
            eFrame imageType = xFile.FrameType;

            if (imageType == eFrame.LIGHT)
            {
                int focusPosition = xFile.FocuserPosition;
                double focusTemperature = xFile.FocuserTemperature;
                double ambientTemperature = xFile.AmbientTemperature;

                if ((focusPosition != int.MinValue) && (focusTemperature != -273.0) && (ambientTemperature != -273.0))
                {
                    mFocuserPositionList.Add(focusPosition);
                    mFocuserTemperatureList.Add(focusTemperature);
                    mAmbientTemperatureList.Add(ambientTemperature);
                }
                else
                {
                    mFocuserPositionList.Add(-1);
                    mFocuserTemperatureList.Add(-273);
                    mAmbientTemperatureList.Add(-273);
                }

                //FileSSWeight.Add(Convert.ToDouble(Keywords.AmbientTemperature()));
            }
        }

        public static string CalculateOverhead(List<XisfFile> fileList)
        {
            int index;
            List<double> subFrameIntervalList;
            List<double> subFrameExposureList;
            List<double> exposureList;
            List<double> intervalList;

            subFrameIntervalList = new List<double>();
            subFrameExposureList = new List<double>();

            exposureList = new List<double>();
            intervalList = new List<double>();

            if (fileList.Count <= 1)
            {
                return "SubFrame Overhead: Not Calculated";
            }

            XisfFile fistFile = fileList[0];

            DateTime firstInterval = new DateTime(2000, 1, 1, 0, 0, 0);
            DateTime secondInterval = new DateTime(2000, 1, 1, 0, 0, 0);


            foreach (XisfFile file in fileList)
            {
                secondInterval = file.CaptureTime;
                double delta = secondInterval.Subtract(firstInterval).TotalSeconds;
                firstInterval = secondInterval;

                if (delta > (60 * 60))  // Don't add if over 1 hour between frames
                {
                    continue;
                }

                subFrameIntervalList.Add(delta);

                subFrameExposureList.Add(file.ExposureSeconds);
            }

            double sigma = subFrameIntervalList.StandardDeviation();

            if (sigma.Equals(double.NaN))
            {
                return "SubFrame Overhead: Calculation Error";
            }

            for (index = 0; index < subFrameIntervalList.Count; index++)
            {
                double interval = Math.Abs(subFrameIntervalList[index]);

                if ((interval < ((sigma * 4) + subFrameIntervalList.Mean())) && (interval > 0))
                {
                    intervalList.Add(subFrameIntervalList[index]);
                    exposureList.Add(subFrameExposureList[index]);
                }
            }

            double totalIntervalTime = intervalList.Sum();
            double totalExposureTime = exposureList.Sum();

            double overheadPercent = ((totalIntervalTime - totalExposureTime) / totalExposureTime) * 100.0;
            double overheadSeconds = (totalIntervalTime - totalExposureTime) / exposureList.Count;

            return "SubFrame Overhead: " + overheadPercent.ToString("F0") + " % at " + overheadSeconds.ToString("F0") + " Seconds/Frame\n" +
                   "Average Exposure:     " + (totalExposureTime / exposureList.Count).ToString("F0") + " Seconds" + " over " + fileList.Count + " Frames";
        }

        public static Dictionary<int, double> BuildUnsortedPositionDictionary(List<XisfFile> xFileList)
        {
            Dictionary<int, double> focuserDictionary = new Dictionary<int, double>();

            foreach (XisfFile xFile in xFileList)
            {
                int focuserPosition = xFile.FocuserPosition;
                double focuserTemperature = xFile.FocuserTemperature;

                if (focuserDictionary.ContainsKey(focuserPosition))
                {
                    if (focuserDictionary[focuserPosition] > focuserTemperature)
                        focuserDictionary[focuserPosition] = focuserTemperature;
                }
                else
                    if (focuserPosition < 60000)
                    focuserDictionary[focuserPosition] = focuserTemperature;
            }

            return focuserDictionary;
        }

        public static SortedDictionary<double, int> BuildSortedTemperatureDictionary(Dictionary<int, double> unsortedPositionTemperaturePairs)
        {
            SortedDictionary<double, int> focuserDictionary = new SortedDictionary<double, int>();

            foreach (KeyValuePair<int, double> positionTemperature in unsortedPositionTemperaturePairs)
            {
                int focuserPosition = positionTemperature.Key;
                double focuserTemperature = positionTemperature.Value;

                if (focuserDictionary.ContainsKey(focuserTemperature))
                {
                    if (focuserDictionary[focuserTemperature] > focuserPosition)
                        focuserDictionary[focuserTemperature] = focuserPosition;
                }
                else
                    focuserDictionary[focuserTemperature] = focuserPosition;
            }

            return focuserDictionary;
        }

        public static string CalculateFocuserTemperatureCompensationCoefficient(List<XisfFile> xFileList)
        {
            // The temperature coefficient needs to be computed per imaging session - not an averal average.
            // Group source images into capture days. Capture Day: 4 pm today to 9 am tomorrow

            TimeSpan fourPM = new TimeSpan(16, 0, 0); // 4 pm
            TimeSpan nineAM = new TimeSpan(9, 0, 0);  // 9 am the next day

            // Filter and group xFiles based on capture day
            IEnumerable<IGrouping<DateTime, XisfFile>> groupedByDateFiles = xFileList.GroupBy(xFile => xFile.CaptureTime);

            IEnumerable<IGrouping<bool, XisfFile>> groupedByEveningFiles = xFileList.GroupBy(xFile => xFile.CaptureTime.TimeOfDay >= fourPM);
            IEnumerable<IGrouping<bool, XisfFile>> groupedByMorningFiles = xFileList.GroupBy(xFile => xFile.CaptureTime.TimeOfDay <= nineAM);
            //var groupedBySessionFiles = IEnumerable<List<XisfFile>>;  // = xFileList.GroupBy(xFile => xFile.CaptureDateTime.TimeOfDay <= nineAM);

            foreach (IGrouping<bool, XisfFile> evening in groupedByEveningFiles)
            {
                foreach (IGrouping<bool, XisfFile> morning in groupedByMorningFiles)
                {
                    if (evening.Key == morning.Key)
                    {
                        //groupedBySessionFiles.Add(evening);
                        //groupedBySessionFiles.Add(morning);
                    }
                }
            }

            // Find the minimum temperature at each unique focus position
            Dictionary<int, double> unsortedPositionTemperaturePairs = BuildUnsortedPositionDictionary(xFileList);
            if (unsortedPositionTemperaturePairs.Count == 0)
                return "Temperature Coefficient: Not Computed";

            // Find the minimum position of each unique temperature then sort by temperature
            SortedDictionary<double, int> sortedTemperaturePositionPairs = BuildSortedTemperatureDictionary(unsortedPositionTemperaturePairs);

            double minTemperature = xFileList.Select(temperature => temperature.FocuserTemperature).Min();
            double maxTemperature = xFileList.Select(temperature => temperature.FocuserTemperature).Max();

            double minPosition = sortedTemperaturePositionPairs.First().Value;
            double maxPosition = sortedTemperaturePositionPairs.Last().Value;

            // Linear fit position vs temperature
            int degree = 1;

            double[] x = sortedTemperaturePositionPairs.Select(temperature => temperature.Key).ToArray();
            double[] y = sortedTemperaturePositionPairs.Select(temperature => (double)temperature.Value).ToArray();

            // Create y = mx + b
            // b = polynomial[0]
            // m = polynomial[1]

            if (x.Length > 1)
            {
                double[] polynomial = Fit.Polynomial(x, y, degree);

                return polynomial[1].ToString("F0") + " Steps/Degree IN computed from " + sortedTemperaturePositionPairs.Count + " unique focuser positions:\n" +
                      "                                             " + maxPosition + "@" + maxTemperature.FormatTemperature() + "C to " + minPosition + "@" + minTemperature.FormatTemperature() + "C";
            }
            else
                return "Temperature Coefficient: Not Computed";
        }
    }
}
