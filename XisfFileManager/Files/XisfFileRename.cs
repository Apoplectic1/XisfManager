using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

using XisfFileManager.Enums;

namespace XisfFileManager.Files
{
    public class XisfFileRename
    {
        private int mDupIndex;

        public List<XisfFile> mFileList;

        public eOrder RenameOrder;

        private static string RecurseDupFileName(string dupFileName)
        {
            if (File.Exists(dupFileName) == true)
            {
                int lastParen = dupFileName.LastIndexOf(')');
                dupFileName = dupFileName.Insert(lastParen + 1, "-Dup");

                dupFileName = RecurseDupFileName(dupFileName);
            }
            return dupFileName;
        }

        public Tuple<int, string> RenameFile(XisfFile file)
        {
            try
            {
                string newFileName;
                string sourceFileDirectory;

                sourceFileDirectory = Path.GetDirectoryName(file.FilePath);

                newFileName = BuildFileName(file.FileNameNumberIndex, file) + ".xisf";

                // Rename the file if its name actually changed
                if (file.FilePath != sourceFileDirectory + "\\" + newFileName)
                {
                    if (File.Exists(sourceFileDirectory + "\\" + newFileName) == false)
                    {
                        File.Move(file.FilePath, sourceFileDirectory + "\\" + newFileName);
                    }
                }
                return new Tuple<int, string>(1, newFileName);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), " RenameFiles(List<XisfFile.XisfFile> fileList)");
                return new Tuple<int, string>(-1, "");
            }
        }

        public static int MoveDuplicates(List<XisfFile> fileList)
        {
            // Duplicates are files with identical GroupBy items.
            var groupedDuplicates = fileList.GroupBy(item =>
                                    {
                                        return new
                                        {
                                            item.Camera,
                                            item.FrameType,
                                            item.Binning,
                                            item.FilterName,
                                            item.ExposureSeconds,
                                            item.Gain,
                                            item.Offset,
                                            item.CaptureTime
                                        };
                                    })
                                    .Where(group => group.Skip(1).Any())
                                    .ToList();

            // List to keep track of files that have been moved.
            List<XisfFile> movedFiles = new List<XisfFile>();

            foreach (var group in groupedDuplicates)
            {
                List<XisfFile> items = group.ToList();
                for (int i = 1; i < items.Count; i++) // Skip the first item to keep it in place
                {
                    XisfFile item = items[i];
                    string sourceFilePath = Path.GetDirectoryName(item.FilePath);
                    string duplicatesPath = Path.Combine(sourceFilePath, "Duplicates");

                    if (!Directory.Exists(duplicatesPath))
                        Directory.CreateDirectory(duplicatesPath);

                    string destinationFilePath = Path.Combine(duplicatesPath, Path.GetFileName(item.FilePath));

                    // Move the file to the Duplicates directory
                    File.Move(item.FilePath, destinationFilePath, overwrite: true); // overwrite if the file already exists

                    // Add the file to the movedFiles list
                    movedFiles.Add(item);
                }
            }

            // Remove the moved files from the original fileList.
            fileList.RemoveAll(file => movedFiles.Contains(file));

            // Return the count of groups that had duplicates.
            return movedFiles.Count;
        }


        private string BuildFileName(int index, XisfFile mFile)
        {
            string newName = string.Empty;
            string targetName;
            eFrame frameType;

            if (mFile.TargetName.Contains("Master"))
            {
                newName = "Master ";
                targetName = mFile.TargetName;
                frameType = mFile.FrameType;

                if (targetName.Equals("Master"))
                {
                    if (mFile.FrameType == eFrame.LIGHT)
                    {
                        newName = targetName + "  Integration  L-" + mFile.FilterName + "  ";

                        if (mFile.MSTRFRMS != -1)
                            newName += mFile.ExposureSeconds.FormatExposureTime() + "x" + mFile.Binning + "x" + mFile.MSTRFRMS + "  ";
                        else
                            newName += mFile.ExposureSeconds.FormatExposureTime() + "x" + mFile.Binning + "  ";

                        newName += mFile.Camera + "G" + mFile.Gain.ToString("D3") + "O" + mFile.Offset;
                        newName += "@" + mFile.SensorTemperature.FormatTemperature() + "C";

                        if (mFile.MSTRALG != string.Empty)
                            newName += "  (" + mFile.MSTRALG + "  " + mFile.CaptureTime.ToString("yyyy-MM-dd");
                        else
                            newName += "  (" + mFile.CaptureTime.ToString("yyyy-MM-dd");

                        newName += "  " + mFile.CaptureSoftware;

                        newName += ")";
                    }

                    if (frameType == eFrame.DARK)
                    {
                        newName += "Dark  " + mFile.CaptureTime.ToString("yyyy-MM-dd") + "  ";

                        if (mFile.MSTRFRMS != -1)
                            newName += mFile.ExposureSeconds.FormatExposureTime() + "x" + mFile.Binning + "x" + mFile.MSTRFRMS + "  ";
                        else
                            newName += mFile.ExposureSeconds.FormatExposureTime() + "x" + mFile.Binning + "  ";

                        newName += mFile.Camera + "G" + mFile.Gain.ToString("D3") + "O" + mFile.Offset;
                        newName += "@" + mFile.SensorTemperature.FormatTemperature() + "C";
                    }

                    if (frameType == eFrame.BIAS)
                    {
                        newName += "Bias  " + mFile.CaptureTime.ToString("yyyy-MM-dd") + "  ";

                        if (mFile.MSTRFRMS != -1)
                            newName += mFile.ExposureSeconds.FormatExposureTime() + "x" + mFile.Binning.ToString() + "x" + mFile.MSTRFRMS + "  ";
                        else
                            newName += mFile.ExposureSeconds.FormatExposureTime() + "x" + mFile.Binning.ToString() + "  ";

                        newName += mFile.Camera + "G" + mFile.Gain.ToString("D3") + "O" + mFile.Offset.ToString();
                        newName += "@" + mFile.SensorTemperature.FormatTemperature() + "C";
                    }

                    if (frameType == eFrame.FLAT)
                    {
                        newName += "Flat " + mFile.FilterName + "  " + mFile.CaptureTime.ToString("yyyy-MM-dd") + "  ";

                        if (mFile.MSTRFRMS != -1)
                            newName += mFile.ExposureSeconds.FormatExposureTime() + "x" + mFile.Binning.ToString() + "x" + mFile.MSTRFRMS + "  ";
                        else
                            newName += mFile.ExposureSeconds.FormatExposureTime() + "x" + mFile.Binning.ToString() + "  ";

                        newName += mFile.Camera + "G" + mFile.Gain.ToString("D3") + "O" + mFile.Offset.ToString();
                        newName += "@" + mFile.SensorTemperature.FormatTemperature() + "C  ";

                        newName += mFile.Telescope + "@";
                        newName += mFile.FocalLength.ToString("F0");
                    }

                    if (mFile.MSTRALG != string.Empty)
                    {
                        newName += "  (" + mFile.MSTRALG;
                        if (mFile.CaptureSoftware != string.Empty)
                            newName += " " + mFile.CaptureSoftware + ")";
                        else
                            newName += ")";
                    }
                    else
                        if (mFile.CaptureSoftware != string.Empty)
                        newName += "  (" + mFile.CaptureSoftware + ")";
                }

                return newName;
            }

            // *************************************************************************************************************

            if (mFile.FrameType == eFrame.DARK)
            {
                newName = index.ToString("D3") + " ";
                newName += " Dark  ";
                newName += mFile.ExposureSeconds.FormatExposureTime() + "x" + mFile.Binning + "  ";
                newName += mFile.Camera + "G" + mFile.Gain.ToString("D3") + "O" + mFile.Offset;
                newName += "@" + mFile.SensorTemperature.FormatTemperature() + "C";

                newName += "  (" + mFile.CaptureTime.ToString("yyyy-MM-dd  hh-mm-ss tt") + "  ";
                newName += mFile.CaptureSoftware;
                newName += ")";

                return newName;
            }

            if (mFile.FrameType == eFrame.BIAS)
            {
                newName = index.ToString("D4") + " ";
                newName += " Bias  ";
                newName += mFile.ExposureSeconds.FormatExposureTime() + "x" + mFile.Binning + "  ";
                newName += mFile.Camera + "G" + mFile.Gain.ToString("D3") + "O" + mFile.Offset;
                newName += "@" + mFile.SensorTemperature.FormatTemperature() + "C";

                newName += "  (" + mFile.CaptureTime.ToString("yyyy-MM-dd  hh-mm-ss tt") + "  ";
                newName += mFile.CaptureSoftware;
                newName += ")";

                return newName;
            }

            if (mFile.FrameType == eFrame.FLAT)
            {
                newName = index.ToString("D3") + " ";
                newName += " F-" + mFile.FilterName + "  ";

                newName += mFile.ExposureSeconds.FormatExposureTime() + "x" + mFile.Binning + "  ";
                newName += mFile.Camera + "G" + mFile.Gain.ToString("D3") + "O" + mFile.Offset;
                newName += "@" + mFile.SensorTemperature.FormatTemperature() + "C  ";

                newName += mFile.Telescope + "@";
                newName += mFile.FocalLength.ToString("F0");

                if (mFile.AmbientTemperature != -273)
                    newName += mFile.AmbientTemperature.FormatTemperature() + "C  ";
                else
                    newName += "  ";

                if ((mFile.FocuserPosition != int.MinValue) && mFile.FocuserTemperature != -273.0)
                {
                    newName += "F" + mFile.FocuserPosition.ToString("D5") + "@" + mFile.FocuserTemperature.FormatTemperature() + "C";
                }

                if (mFile.RotationAngle.StartsWith('S'))
                    newName += "  " + mFile.RotationAngle + "  ";
                else
                    newName += "  ";

                newName += "(" + mFile.CaptureTime.ToString("yyyy-MM-dd  hh-mm-ss tt") + "  ";
                newName += mFile.CaptureSoftware;
                newName += ")";

                return newName;
            }

            if (mFile.FrameType == eFrame.LIGHT)
            {
                switch (RenameOrder)
                {
                    case eOrder.INDEX:
                        newName = index.ToString("D3") + " ";
                        break;

                    case eOrder.INDEXWEIGHT:
                        if (mFile.SSWeight < 0)
                        {
                            newName = index.ToString("D3") + " ";
                        }
                        else
                        {
                            newName = index.ToString("D3") + " " + Convert.ToInt32(mFile.SSWeight * 1000.0).ToString("D4") + " ";
                        }
                        break;
                    case eOrder.WEIGHT:
                        if (Double.IsNaN(mFile.SSWeight))
                        {
                            newName = index.ToString("D3") + " ";
                        }
                        else
                        {
                            newName = Convert.ToInt32(mFile.SSWeight * 1000.0).ToString("D4") + " ";
                        }
                        break;
                    case eOrder.WEIGHTINDEX:
                        if (Double.IsNaN(mFile.SSWeight))
                        {
                            newName = index.ToString("D3") + " ";
                        }
                        else
                        {
                            newName = Convert.ToInt32(mFile.SSWeight * 1000.0).ToString("D4") + " " + index.ToString("D3") + " ";
                        }
                        break;
                }

                newName += " " + mFile.TargetName + "  L-" + mFile.FilterName + "  ";

                newName += mFile.ExposureSeconds.FormatExposureTime() + "x" + mFile.Binning + "  ";
                newName += mFile.Camera + "G" + mFile.Gain.ToString("D3") + "O" + mFile.Offset;
                newName += "@" + mFile.SensorTemperature.FormatTemperature() + "C  ";

                newName += mFile.Telescope + "@";
                newName += mFile.FocalLength.ToString("F0");

                if (mFile.AmbientTemperature != -273.0)
                    newName += mFile.AmbientTemperature.FormatTemperature() + "C  ";
                else
                    newName += mFile.FocuserTemperature.FormatTemperature() + "C  ";

                newName += "F" + mFile.FocuserPosition.ToString("D5") + "@" + mFile.FocuserTemperature.FormatTemperature() + "C";

                if (mFile.RotationAngle.StartsWith('S'))
                    newName += "  " + mFile.RotationAngle + "  ";
                else
                    newName += "  ";

                newName += "(" + mFile.CaptureTime.ToString("yyyy-MM-dd  hh-mm-ss tt") + "  ";
                newName += mFile.CaptureSoftware;
                newName += ")";

                return newName;
            }

            return index.ToString("D3") + " " + "Rename Failed";
        }

        private void MoveDuplicates(XisfFile currentFile, string sourceFilePath, string newFileName)
        {
            mDupIndex = 1;

            foreach (XisfFile entry in mFileList)
            {
                mDupIndex++;

                Directory.CreateDirectory(sourceFilePath + '\\' + "Duplicates");

                int last = currentFile.FilePath.LastIndexOf('\\');

                // Remove the index or SSWEIGHT from the file name (the first four characters) and postfix duplicate index
                string duplicateFileName = newFileName.Remove(0, 4).Insert(0, mDupIndex.ToString("D3") + " ");

                File.Move(entry.FilePath, sourceFilePath + "\\" + "Duplicates" + "\\" + duplicateFileName);
                mFileList.Remove(entry);
            }
        }

        private static bool IsFileLocked(string path)
        {
            FileInfo file = new FileInfo(path);

            FileStream stream = null;

            try
            {
                stream = file.Open(FileMode.Open, FileAccess.ReadWrite, FileShare.None);
            }
            catch (IOException)
            {
                //the file is unavailable because it is:
                //still being written to
                //or being processed by another thread
                //or does not exist (has already been processed)
                return true;
            }
            finally
            {
                if (stream != null)
                    stream.Close();
            }

            //file is not locked
            return false;
        }
    }
}
