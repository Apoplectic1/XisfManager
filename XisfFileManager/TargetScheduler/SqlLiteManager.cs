using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using XisfFileManager.Files;
using XisfFileManager.TargetScheduler.Tables;

namespace XisfFileManager.TargetScheduler
{
    internal sealed class SqlLiteManager
    {
        public List<AcquiredImage> mAcquiredImageList;
        public List<ExposurePlan> mExposurePlanList;
        public List<ExposureTemplate> mExposureTemplateList;
        public List<ImageData> mImageDataList;
        public List<ProfilePreference> mProfilePreferenceList;
        public List<Project> mProjectList;
        public List<RuleWeight> mRuleWeightList;
        public List<Target> mTargetList;
        public SqlLiteReader mSqlReader { get; private set; }
        public SqlLiteUpdater mSqlUpdater { get; private set; }
        public SqlLiteWriter mSqlWriter { get; private set; }

        internal SqlLiteManager()
        {
            mAcquiredImageList = new List<AcquiredImage>();
            mExposurePlanList = new List<ExposurePlan>();
            mExposureTemplateList = new List<ExposureTemplate>();
            mImageDataList = new List<ImageData>();
            mProfilePreferenceList = new List<ProfilePreference>();
            mProjectList = new List<Project>();
            mRuleWeightList = new List<RuleWeight>();
            mSqlReader = new SqlLiteReader(this);
            mSqlUpdater = new SqlLiteUpdater(this);
            mSqlWriter = new SqlLiteWriter(this);
            mTargetList = new List<Target>();
        }

        public void UpdateTargetImageCounts(List<XisfFile> xFileList)
        {
            foreach (XisfFile xFile in xFileList)
            {
                double degrees = 0;

                object objectRA = xFile.KeywordList.GetKeywordValue("OBJCTRA");
                if (objectRA is string)
                {
                    string RA = (string)objectRA;
                    RA = RA.Contains('.') ? RA : RA + ".0";
                    //if (Regex.IsMatch(RA, @"^[+-]?\d{1,2} \d{1,2} \d{1,2}\.\d+"))
                    if (Regex.IsMatch(RA, @"^\d{1,2} \d{1,2} \d{1,2}\.\d+"))
                    {
                        // We matched [hh mm ss] string format
                        degrees = ConvertHoursMinutesSecondsToDecimal(RA);
                        xFile.KeywordList.AddKeyword("RA", degrees.ToString(), "Degrees Right Ascension");
                    }
                }
                else
                {
                    // objectRA is a number. Is it in hours or degrees?
                    double number;
                    bool status = double.TryParse(objectRA.ToString(), out number);
                    if (status)
                    {
                        // Assume its a packed hhmmss format
                        string result = UnpackTimeString(number.ToString()).Replace("+", "").Replace("-", "");

                        if (Regex.IsMatch(result, @"^\d{1,2} \d{1,2} \d{1,2}\.\d+"))
                        {
                            xFile.KeywordList.AddKeyword("OBJCTRA", result, "[h m s] Target Right Ascension");
                            degrees = ConvertHoursMinutesSecondsToDecimal(result);
                            xFile.KeywordList.AddKeyword("RA", degrees.ToString(), "Degrees Right Ascension");
                        }
                    }
                }

                object objectDEC = xFile.KeywordList.GetKeywordValue("OBJCTDEC");
                if (objectDEC is string)
                {
                    string DEC = (string)objectDEC;
                    DEC = DEC.Contains('.') ? DEC : DEC + ".0";
                    if (Regex.IsMatch(DEC, @"^[+-]?\d{1,2} \d{1,2} \d{1,2}\.\d+"))
                    {
                        // We matched [hh mm ss] string format
                        degrees = ConvertHoursMinutesSecondsToDecimal((string)objectDEC);
                        xFile.KeywordList.AddKeyword("DEC", degrees.ToString(), "Hours Declination");
                    }
                    else
                    {
                    }
                }
                else
                {
                    // objectDEC is a number. Is it in hours or degrees?

                    double number;
                    bool status = double.TryParse(objectDEC.ToString(), out number);
                    if (status)
                    {
                        // Assume its a packed hhmmss format
                        string result = UnpackTimeString(number.ToString());

                        if (Regex.IsMatch(result, @"^[+-]?\d{1,2} \d{1,2} \d{1,2}\.\d+"))
                        {
                            xFile.KeywordList.AddKeyword("OBJCTDEC", result, "[d m s] Target Declination");
                            degrees = ConvertHoursMinutesSecondsToDecimal(result);
                            xFile.KeywordList.AddKeyword("DEC", degrees.ToString(), "Hours Declination");
                        }
                    }
                }

                xFile.KeywordList.RemoveKeyword("RA");
                xFile.KeywordList.RemoveKeyword("DEC");
                xFile.KeywordList.RemoveKeyword("PROTECT");
                xFile.KeywordList.RemoveKeyword("Protect");

            }


            // DEBUG to find missing RA or DEC
            double tsRA = mTargetList[0].ra * 15.0;
            double tsDEC = mTargetList[0].dec;
            /*
            var skyPposition = xFileList
                .Where(obj =>
                    IsWithinPercent((double)obj.KeywordList.GetKeywordValue("RA"), mTargetList[0].ra * 15.0, 0.1) &&
                    IsWithinPercent((double)obj.KeywordList.GetKeywordValue("DEC"), mTargetList[0].dec, 0.1))
                   .ToList();

            var name = xFileList.Where(obj => obj.FilePath.Contains(mTargetList[1].name)).ToList();
            */
            static double ConvertHoursMinutesSecondsToDegrees(string input)
            {
                string[] parts = input.Split(' ');
                if (parts.Length != 3)
                    return -360.0;

                double hours = double.Parse(parts[0]);
                double minutes = double.Parse(parts[1]);
                double seconds = double.Parse(parts[2]);

                double degrees = (((hours + minutes) / 60.0) + (seconds / 3600.0)) * 15.0;

                if (hours < 0)
                    degrees = -degrees;

                return degrees;
            }

            static double ConvertHoursMinutesSecondsToDecimal(string input)
            {
                string[] parts = input.Split(' ');
                if (parts.Length != 3)
                    return -360.0;

                double hours = double.Parse(parts[0]);
                double minutes = double.Parse(parts[1]);
                double seconds = double.Parse(parts[2]);

                double degrees = Math.Abs(hours) + (minutes / 60.0) + (seconds / 3600.0);

                if (hours < 0)
                    degrees = -degrees;

                return degrees;
            }

            static string UnpackTimeString(string input)
            {
                int decimalPoint = input.IndexOf('.');
                if (decimalPoint < 0)
                {
                    input += ".0";
                    decimalPoint = input.IndexOf('.');
                }
                int length = input.Length - (decimalPoint - 2);

                string seconds = input.Substring(decimalPoint - 2, length);
                string minutes = input.Substring(decimalPoint - 4, 2);

                length = decimalPoint - 4;
                string hours = input.Substring(0, length);

                if (hours == string.Empty)
                    hours = "00";

                if (hours.Contains('-'))
                    return hours.PadLeft(2, '0') + " " + minutes.PadLeft(2, '0') + " " + seconds.PadLeft(2, '0');
                else
                    return "+" + hours.PadLeft(2, '0') + " " + minutes.PadLeft(2, '0') + " " + seconds.PadLeft(2, '0');
            }
        }

        public static bool IsWithinPercent(double value, double target, double percent)
        {
            double absoluteDifference = Math.Abs(value - target);
            double threshold = (percent / 100) * Math.Abs(target);
            return absoluteDifference <= threshold;
        }
    }
}
