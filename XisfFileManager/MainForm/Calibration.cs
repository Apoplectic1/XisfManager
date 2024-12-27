using System;
using System.IO;
using XisfFileManager.Globals;

namespace XisfFileManager
{
    public partial class MainForm
    {
        private async void CalibrationTab_FindCalibrationFrames_Click(object sender, EventArgs e)
        {
            bool bMatchedAllFiles = false;
            string calibrationFileMasterLibraryLocation;

            TextBox_CalibrationTab_Messgaes.Clear();
            mCalibration.Frame = eFrame.ALL;

            calibrationFileMasterLibraryLocation = @"E:\Photography\Astro Photography\Calibration";

            if (!bMatchedAllFiles)
                await mCalibration.ReadCalibrationFramesAsync(calibrationFileMasterLibraryLocation);

            mCalibration.MatchTargetsWithCalibrationLibraryFrames(mFileList);
        }

        private void CalibrationTab_ReMatchCalibrationFrames_Click(object sender, EventArgs e)
        {
            TextBox_CalibrationTab_Messgaes.Clear();

            mCalibration.MatchTargetsWithCalibrationLibraryFrames(mFileList);
        }

        private void CalibrationTab_CreateCalibrationDirectory_Click(object sender, EventArgs e)
        {
            if (CheckBox_CalibrationTab_CreateNew.Checked == true)
            {
                string targetCalibrationDirectory = Calibration.SetTargetCalibrationFileDirectories(mFileList[0].FilePath);

                if (Directory.Exists(targetCalibrationDirectory))
                    Directory.Delete(targetCalibrationDirectory, true);

                Directory.CreateDirectory(targetCalibrationDirectory);
            }

            mCalibration.CreateTargetCalibrationDirectory(mFileList);
        }

        private void TextBox_CalibrationTab_ExposureTolerance_TextChanged(object sender, EventArgs e)
        {
            double value;

            if (double.TryParse(TextBox_CalibrationTab_MatchingTolerance_Exposure.Text, out value) == false)
            {
                TextBox_CalibrationTab_MatchingTolerance_Exposure.Text = "0";
                return;
            }

            mCalibration.ExposureTolerance = value;
        }

        private void TextBox_CalibrationTab_GainTolerance_TextChanged(object sender, EventArgs e)
        {
            double value;

            if (double.TryParse(TextBox_CalibrationTab_MatchingTolerance_Gain.Text, out value) == false)
            {
                TextBox_CalibrationTab_MatchingTolerance_Gain.Text = "0";
                return;
            }

            mCalibration.GainTolerance = value;
        }

        private void TextBox_CalibrationTab_OffsetTolerance_TextChanged(object sender, EventArgs e)
        {
            double value;

            if (double.TryParse(TextBox_CalibrationTab_MatchingTolerance_Offset.Text, out value) == false)
            {
                TextBox_CalibrationTab_MatchingTolerance_Offset.Text = "0";
                return;
            }

            mCalibration.OffsetTolerance = value;

        }

        private void TextBox_CalibrationTab_TemperatureTolerance_TextChanged(object sender, EventArgs e)
        {
            double value;

            if (double.TryParse(TextBox_CalibrationTab_MatchingTolerance_Temperature.Text, out value) == false)
            {
                TextBox_CalibrationTab_MatchingTolerance_Temperature.Text = "5";
                return;
            }

            mCalibration.TemperatureTolerance = value;
        }

        private void CheckBox_CalibrationTab_MatchingTolerance_ExposureNearest_CheckedChanged(object sender, EventArgs e)
        {
            TextBox_CalibrationTab_MatchingTolerance_Exposure.Enabled = !CheckBox_CalibrationTab_MatchingTolerance_ExposureNearest.Checked;
        }

        private void CheckBox_CalibrationTab_MatchingTolerance_GainNearest_CheckedChanged(object sender, EventArgs e)
        {
            TextBox_CalibrationTab_MatchingTolerance_Gain.Enabled = !CheckBox_CalibrationTab_MatchingTolerance_GainNearest.Checked;
        }

        private void CheckBox_CalibrationTab_MatchingTolerance_OffsetNearest_CheckedChanged(object sender, EventArgs e)
        {
            TextBox_CalibrationTab_MatchingTolerance_Offset.Enabled = !CheckBox_CalibrationTab_MatchingTolerance_OffsetNearest.Checked;
        }

        private void CheckBox_CalibrationTab_MatchingTolerance_TemperatureNearest_CheckedChanged(object sender, EventArgs e)
        {
            TextBox_CalibrationTab_MatchingTolerance_Temperature.Enabled = !CheckBox_CalibrationTab_MatchingTolerance_TemperatureNearest.Checked;
        }
    }
}
