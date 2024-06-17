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
            mFocuserPositionList = [];
            mFocuserTemperatureList = [];
            mAmbientTemperatureList = [];
        }

        public void Clear()
        {
            mFocuserPositionList.Clear();
            mFocuserTemperatureList.Clear();
            mAmbientTemperatureList.Clear();
        }


        /// <summary>
        /// Builds and updates the lists of image parameters for a given XisfFile object.
        /// This method extracts the focuser position, focuser temperature, and ambient temperature
        /// from the XisfFile if it is of type LIGHT and adds these values to corresponding lists.
        /// If any of the parameters are invalid, default values are added to the lists instead.
        /// </summary>
        /// <param name="xFile">The XisfFile object containing the image parameters to be processed.</param>
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
            }
        }


        /// <summary>
        /// Calculates the overhead time for capturing subframes in a series of XisfFile objects.
        /// This method determines the time intervals between consecutive captures, filters out invalid intervals,
        /// and computes the overhead as a percentage and in seconds per frame. It also calculates the average exposure time.
        /// </summary>
        /// <param name="fileList">A list of XisfFile objects containing capture time and exposure data.</param>
        /// <returns>A string describing the calculated subframe overhead and average exposure time.
        /// Returns an error message if the calculation cannot be performed due to insufficient data or invalid intervals.</returns>
        public static string CalculateOverhead(List<XisfFile> fileList)
        {
            // Check if there are enough files to perform the calculation
            if (fileList.Count <= 1)
            {
                return "SubFrame Overhead: Not Calculated";
            }

            // Initialize the first interval to a DateTime before I statrted AstroPhotography
            DateTime firstInterval = new DateTime(2000, 1, 1, 0, 0, 0);

            // Calculate the intervals between consecutive captures, excluding intervals longer than one hour
            var subFrameIntervals = fileList.Skip(1)
                .Select(file => new
                {
                    Interval = file.CaptureTime.Subtract(firstInterval).TotalSeconds,
                    Exposure = file.ExposureSeconds,
                    CaptureTime = file.CaptureTime
                })
                .Where(delta => delta.Interval <= 3600)
                .ToList();

            // Extract the list of intervals and exposures
            var subFrameIntervalList = subFrameIntervals.Select(item => item.Interval).ToList();
            var subFrameExposureList = subFrameIntervals.Select(item => item.Exposure).ToList();

            // Check if the intervals list is empty or contains invalid values
            if (subFrameIntervalList.Count == 0 || double.IsNaN(subFrameIntervalList.StandardDeviation()))
            {
                return "SubFrame Overhead: Calculation Error";
            }

            // Calculate the standard deviation and mean of the intervals
            double sigma = subFrameIntervalList.StandardDeviation();
            double mean = subFrameIntervalList.Mean();

            // Filter intervals to remove outliers and negative values
            var filteredIntervals = subFrameIntervalList
                .Select((interval, index) => new { Interval = interval, Exposure = subFrameExposureList[index] })
                .Where(item => item.Interval > 0 && item.Interval < (mean + sigma * 4))
                .ToList();

            // Extract the filtered intervals and exposures into separate lists
            var intervalList = filteredIntervals.Select(item => item.Interval).ToList();
            var exposureList = filteredIntervals.Select(item => item.Exposure).ToList();

            // Calculate the total interval and exposure times
            double totalIntervalTime = intervalList.Sum();
            double totalExposureTime = exposureList.Sum();

            // Calculate the overhead as a percentage and in seconds per frame
            double overheadPercent = ((totalIntervalTime - totalExposureTime) / totalExposureTime) * 100.0;
            double overheadSeconds = (totalIntervalTime - totalExposureTime) / exposureList.Count;

            // Return the formatted result string
            return $"SubFrame Overhead: {overheadPercent:F0} % at {overheadSeconds:F0} Seconds/Frame\n" +
                   $"Average Exposure: {totalExposureTime / exposureList.Count:F0} Seconds over {fileList.Count} Frames";
        }



        /// <summary>
        /// Builds an unsorted dictionary of focuser positions and their corresponding minimum temperatures.
        /// This method iterates through a list of XisfFile objects, extracts focuser positions and temperatures,
        /// and updates the dictionary such that each position is associated with the lowest recorded temperature.
        /// Focuser positions greater than or equal to 60000 are excluded from the dictionary.
        /// </summary>
        /// <param name="xFileList">A list of XisfFile objects containing focuser position and temperature data.</param>
        /// <returns>A dictionary with focuser positions as keys and the minimum recorded temperatures as values.</returns>
        public static Dictionary<int, double> BuildUnsortedPositionDictionary(List<XisfFile> xFileList)
        {
            // Initialize an empty dictionary to store focuser positions and their corresponding minimum temperatures
            Dictionary<int, double> focuserDictionary = new Dictionary<int, double>();

            // Iterate through each XisfFile in the provided list
            foreach (XisfFile xFile in xFileList)
            {
                // Get the focuser position and temperature from the current XisfFile
                int focuserPosition = xFile.FocuserPosition;
                double focuserTemperature = xFile.FocuserTemperature;

                // Check if the dictionary already contains the current focuser position
                if (focuserDictionary.TryGetValue(focuserPosition, out double existingTemperature))
                {
                    // If the current temperature is lower than the existing one, update the dictionary
                    if (existingTemperature > focuserTemperature)
                    {
                        focuserDictionary[focuserPosition] = focuserTemperature;
                    }
                }
                else
                {
                    // If the focuser position is less than 60000 and not already in the dictionary, add it
                    if (focuserPosition < 60000)
                    {
                        focuserDictionary[focuserPosition] = focuserTemperature;
                    }
                }
            }

            // Return the dictionary containing focuser positions and their minimum temperatures
            return focuserDictionary;
        }


        /// <summary>
        /// Builds a sorted dictionary of temperatures and their corresponding minimum focuser positions.
        /// This method processes a dictionary of focuser positions and temperatures, groups entries by temperature,
        /// and selects the minimum focuser position for each unique temperature.
        /// The resulting dictionary is sorted by temperature in ascending order.
        /// </summary>
        /// <param name="unsortedPositionTemperaturePairs">A dictionary with focuser positions as keys and temperatures as values.</param>
        /// <returns>A sorted dictionary with temperatures as keys and the minimum focuser positions as values.</returns>
        public static SortedDictionary<double, int> BuildSortedTemperatureDictionary(Dictionary<int, double> unsortedPositionTemperaturePairs)
        {
            return new SortedDictionary<double, int>(
                // Group entries by temperature
                unsortedPositionTemperaturePairs
                    .GroupBy(pair => pair.Value)
                    // Create a dictionary with the minimum focuser position for each temperature
                    .ToDictionary(
                        group => group.Key,                    // Temperature as key
                        group => group.Min(pair => pair.Key)   // Minimum focuser position as value
                    )
            );
        }


        /// <summary>
        /// Calculates the temperature compensation coefficient for a focuser based on imaging sessions.
        /// This method groups source images into capture days (from 4 pm to 9 am the next day),
        /// computes the minimum temperature at each unique focus position, and performs a linear fit
        /// to determine the temperature compensation coefficient.
        /// </summary>
        /// <param name="xFileList">A list of XisfFile objects containing capture time, focuser position, and temperature data.</param>
        /// <returns>A string describing the calculated temperature compensation coefficient and the range of temperatures and positions used.
        /// Returns an error message if the coefficient cannot be computed due to insufficient or invalid data.</returns>
        public static string CalculateFocuserTemperatureCompensationCoefficient(List<XisfFile> xFileList)
        {
            // The temperature coefficient needs to be computed per imaging session - not an overall average.
            // Group source images into capture days. Capture Day: 4 pm today to 9 am tomorrow

            TimeSpan fourPM = new(16, 0, 0); // 4 pm
            TimeSpan nineAM = new(9, 0, 0);  // 9 am the next day

            // Group files by capture day
            var groupedBySessionFiles = xFileList
                .GroupBy(xFile => xFile.CaptureTime.TimeOfDay >= fourPM || xFile.CaptureTime.TimeOfDay <= nineAM
                    ? xFile.CaptureTime.Date
                    : xFile.CaptureTime.Date.AddDays(-1))
                .ToList();

            // Find the minimum temperature at each unique focus position
            var unsortedPositionTemperaturePairs = BuildUnsortedPositionDictionary(xFileList);
            if (unsortedPositionTemperaturePairs.Count == 0)
                return "Temperature Coefficient: Not Computed";

            // Find the minimum position of each unique temperature then sort by temperature
            var sortedTemperaturePositionPairs = BuildSortedTemperatureDictionary(unsortedPositionTemperaturePairs);

            double minTemperature = xFileList.Min(file => file.FocuserTemperature);
            double maxTemperature = xFileList.Max(file => file.FocuserTemperature);

            double minPosition = sortedTemperaturePositionPairs.First().Value;
            double maxPosition = sortedTemperaturePositionPairs.Last().Value;

            // Linear fit position vs temperature
            int degree = 1;

            double[] x = sortedTemperaturePositionPairs.Keys.ToArray();
            double[] y = sortedTemperaturePositionPairs.Values.Select(position => (double)position).ToArray();

            if (x.Length > 1)
            {
                double[] polynomial = Fit.Polynomial(x, y, degree);

                return $"{polynomial[1]:F0} Steps/Degree IN computed from {sortedTemperaturePositionPairs.Count} unique focuser positions:\n" +
                       $"                                             {maxPosition}@{maxTemperature.FormatTemperature()}C to {minPosition}@{minTemperature.FormatTemperature()}C";
            }
            else
            {
                return "Temperature Coefficient: Not Computed";
            }
        }
    }
}