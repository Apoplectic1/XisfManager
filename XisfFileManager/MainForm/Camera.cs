using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using XisfFileManager.Files;

namespace XisfFileManager
{
    public partial class MainForm
    {
        private void ClearCameraGroup()
        {
            Label_KeywordUpdateTab_Camera_Camera.ForeColor = Color.Black;
            Label_KeywordUpdateTab_Camera_SensorTemp.ForeColor = Color.Black;
            Label_KeywordUpdateTab_Camera_Gain.ForeColor = Color.Black;
            Label_KeywordUpdateTab_Camera_Offset.ForeColor = Color.Black;
            Label_KeywordUpdateTab_Camera_Binning.ForeColor = Color.Black;
            Label_KeywordUpdateTab_Camera_Seconds.ForeColor = Color.Black;

            CheckBox_KeywordUpdateTab_Camera_Z533.Checked = false;
            CheckBox_KeywordUpdateTab_Camera_Z533.ForeColor = Color.Black;
            CheckBox_KeywordUpdateTab_Camera_Z183.Checked = false;
            CheckBox_KeywordUpdateTab_Camera_Z183.ForeColor = Color.Black;
            CheckBox_KeywordUpdateTab_Camera_Q178.Checked = false;
            CheckBox_KeywordUpdateTab_Camera_Q178.ForeColor = Color.Black;
            CheckBox_KeywordUpdateTab_Camera_A144.Checked = false;
            CheckBox_KeywordUpdateTab_Camera_A144.ForeColor = Color.Black;

            ComboBox_KeywordUpdateTab_Camera_A144Binning.DataSource = null;
            ComboBox_KeywordUpdateTab_Camera_A144Binning.Text = string.Empty;
            ComboBox_KeywordUpdateTab_Camera_A144Binning.Items.Clear();
            ComboBox_KeywordUpdateTab_Camera_A144Seconds.DataSource = null;
            ComboBox_KeywordUpdateTab_Camera_A144Seconds.Text = string.Empty;
            ComboBox_KeywordUpdateTab_Camera_A144Seconds.Items.Clear();
            ComboBox_KeywordUpdateTab_Camera_A144SensorTemp.DataSource = null;
            ComboBox_KeywordUpdateTab_Camera_A144SensorTemp.Text = string.Empty;
            ComboBox_KeywordUpdateTab_Camera_A144SensorTemp.Items.Clear();

            ComboBox_KeywordUpdateTab_Camera_Q178Binning.DataSource = null;
            ComboBox_KeywordUpdateTab_Camera_Q178Binning.Text = string.Empty;
            ComboBox_KeywordUpdateTab_Camera_Q178Binning.Items.Clear();
            ComboBox_KeywordUpdateTab_Camera_Q178Gain.DataSource = null;
            ComboBox_KeywordUpdateTab_Camera_Q178Gain.Text = string.Empty;
            ComboBox_KeywordUpdateTab_Camera_Q178Gain.Items.Clear();
            ComboBox_KeywordUpdateTab_Camera_Q178Offset.DataSource = null;
            ComboBox_KeywordUpdateTab_Camera_Q178Offset.Text = string.Empty;
            ComboBox_KeywordUpdateTab_Camera_Q178Offset.Items.Clear();
            ComboBox_KeywordUpdateTab_Camera_Q178Seconds.DataSource = null;
            ComboBox_KeywordUpdateTab_Camera_Q178Seconds.Text = string.Empty;
            ComboBox_KeywordUpdateTab_Camera_Q178Seconds.Items.Clear();
            ComboBox_KeywordUpdateTab_Camera_Q178SensorTemp.DataSource = null;
            ComboBox_KeywordUpdateTab_Camera_Q178SensorTemp.Text = string.Empty;
            ComboBox_KeywordUpdateTab_Camera_Q178SensorTemp.Items.Clear();

            ComboBox_KeywordUpdateTab_Camera_Z183Binning.DataSource = null;
            ComboBox_KeywordUpdateTab_Camera_Z183Binning.Text = string.Empty;
            ComboBox_KeywordUpdateTab_Camera_Z183Binning.Items.Clear();
            ComboBox_KeywordUpdateTab_Camera_Z183Gain.DataSource = null;
            ComboBox_KeywordUpdateTab_Camera_Z183Gain.Text = string.Empty;
            ComboBox_KeywordUpdateTab_Camera_Z183Gain.Items.Clear();
            ComboBox_KeywordUpdateTab_Camera_Z183Offset.DataSource = null;
            ComboBox_KeywordUpdateTab_Camera_Z183Offset.Text = string.Empty;
            ComboBox_KeywordUpdateTab_Camera_Z183Offset.Items.Clear();
            ComboBox_KeywordUpdateTab_Camera_Z183Seconds.DataSource = null;
            ComboBox_KeywordUpdateTab_Camera_Z183Seconds.Text = string.Empty;
            ComboBox_KeywordUpdateTab_Camera_Z183Seconds.Items.Clear();
            ComboBox_KeywordUpdateTab_Camera_Z183SensorTemp.DataSource = null;
            ComboBox_KeywordUpdateTab_Camera_Z183SensorTemp.Text = string.Empty;
            ComboBox_KeywordUpdateTab_Camera_Z183SensorTemp.Items.Clear();

            ComboBox_KeywordUpdateTab_Camera_Z533Binning.DataSource = null;
            ComboBox_KeywordUpdateTab_Camera_Z533Binning.Text = string.Empty;
            ComboBox_KeywordUpdateTab_Camera_Z533Binning.Items.Clear();
            ComboBox_KeywordUpdateTab_Camera_Z533Gain.DataSource = null;
            ComboBox_KeywordUpdateTab_Camera_Z533Gain.Text = string.Empty;
            ComboBox_KeywordUpdateTab_Camera_Z533Gain.Items.Clear();
            ComboBox_KeywordUpdateTab_Camera_Z533Offset.DataSource = null;
            ComboBox_KeywordUpdateTab_Camera_Z533Offset.Text = string.Empty;
            ComboBox_KeywordUpdateTab_Camera_Z533Offset.Items.Clear();
            ComboBox_KeywordUpdateTab_Camera_Z533Seconds.DataSource = null;
            ComboBox_KeywordUpdateTab_Camera_Z533Seconds.Text = string.Empty;
            ComboBox_KeywordUpdateTab_Camera_Z533Seconds.Items.Clear();
            ComboBox_KeywordUpdateTab_Camera_Z533SensorTemp.DataSource = null;
            ComboBox_KeywordUpdateTab_Camera_Z533SensorTemp.Text = string.Empty;
            ComboBox_KeywordUpdateTab_Camera_Z533SensorTemp.Items.Clear();

            Button_KeywordUpdateTab_Camera_SetAll.ForeColor = Color.Black;
            Button_KeywordUpdateTab_Camera_SetByFile.ForeColor = Color.Black;
        }

        public void FindCamera()
        {
            ClearCameraGroup();

            // cameraList should contain an entry for each file
            List<string> CameraList = mFileList.Select(c => c.Camera).ToList();
            bool bFoundZ183 = CameraList.Any(c => c.Contains("183"));
            bool bFoundZ533 = CameraList.Any(c => c.Contains("533"));
            bool bFoundQ178 = CameraList.Any(c => c.Contains("178"));
            bool bFoundA144 = CameraList.Any(c => c.Contains("144"));

            bool bNoCameras = CameraList.Count == 0;
            bool bMissingCameras = (CameraList.Count != mFileList.Count) && !bNoCameras;
            bool bDifferentCameras = ((bFoundZ183 ? 1 : 0) + (bFoundZ533 ? 1 : 0) + (bFoundQ178 ? 1 : 0) + (bFoundA144 ? 1 : 0) >= 2) && !bMissingCameras;
            bool bUniqueCamera = !bMissingCameras && !bDifferentCameras && !bNoCameras;


            CheckBox_KeywordUpdateTab_Camera_A144.Checked = bFoundA144;
            CheckBox_KeywordUpdateTab_Camera_Q178.Checked = bFoundQ178;
            CheckBox_KeywordUpdateTab_Camera_Z183.Checked = bFoundZ183;
            CheckBox_KeywordUpdateTab_Camera_Z533.Checked = bFoundZ533;

            if (bNoCameras)
            {
                // All files are missing cameras
                CheckBox_KeywordUpdateTab_Camera_A144.ForeColor = Color.Red;
                CheckBox_KeywordUpdateTab_Camera_Q178.ForeColor = Color.Red;
                CheckBox_KeywordUpdateTab_Camera_Z183.ForeColor = Color.Red;
                CheckBox_KeywordUpdateTab_Camera_Z533.ForeColor = Color.Red;
            }

            if (bMissingCameras)
            {
                // Found at least one Camera but some files are missing Cameras 
                CheckBox_KeywordUpdateTab_Camera_A144.ForeColor = bFoundA144 ? Color.DarkViolet : Color.Red;
                CheckBox_KeywordUpdateTab_Camera_Q178.ForeColor = bFoundQ178 ? Color.DarkViolet : Color.Red;
                CheckBox_KeywordUpdateTab_Camera_Z183.ForeColor = bFoundZ183 ? Color.DarkViolet : Color.Red;
                CheckBox_KeywordUpdateTab_Camera_Z533.ForeColor = bFoundZ533 ? Color.DarkViolet : Color.Red;
            }

            if (bDifferentCameras)
            {
                // Found different Cameras and all files contain Cameras 
                CheckBox_KeywordUpdateTab_Camera_A144.ForeColor = CheckBox_KeywordUpdateTab_Camera_A144.Checked ? Color.Green : Color.Black;
                CheckBox_KeywordUpdateTab_Camera_Q178.ForeColor = CheckBox_KeywordUpdateTab_Camera_Q178.Checked ? Color.Green : Color.Black;
                CheckBox_KeywordUpdateTab_Camera_Z183.ForeColor = CheckBox_KeywordUpdateTab_Camera_Z183.Checked ? Color.Green : Color.Black;
                CheckBox_KeywordUpdateTab_Camera_Z533.ForeColor = CheckBox_KeywordUpdateTab_Camera_Z533.Checked ? Color.Green : Color.Black;
            }

            if (bUniqueCamera)
            {
                CheckBox_KeywordUpdateTab_Camera_A144.ForeColor = Color.Black;
                CheckBox_KeywordUpdateTab_Camera_Q178.ForeColor = Color.Black;
                CheckBox_KeywordUpdateTab_Camera_Z183.ForeColor = Color.Black;
                CheckBox_KeywordUpdateTab_Camera_Z533.ForeColor = Color.Black;
            }

            // ****************************************************************

            bool bNoSecondsZ533 = false;
            bool bMissingSecondsZ533 = false;
            bool bDifferentSecondsZ533 = false;
            bool bUniqueSecondsZ533 = false;

            if (bFoundZ533)
            {
                List<double> SecondsListZ533 = mFileList.Where(i => i.ExposureSeconds >= 0 && i.Camera.Contains("533")).Select(i => i.ExposureSeconds).ToList();
                bNoSecondsZ533 = SecondsListZ533.Count == 0;
                bMissingSecondsZ533 = SecondsListZ533.Count != CameraList.Count(c => c.Contains("533")) && !bNoSecondsZ533;
                bDifferentSecondsZ533 = SecondsListZ533.Distinct().Count() > 1;
                bUniqueSecondsZ533 = !bMissingSecondsZ533 && !bDifferentSecondsZ533 && !bNoSecondsZ533;

                ComboBox_KeywordUpdateTab_Camera_Z533Seconds.DataSource = SecondsListZ533.OrderBy(item => item).Distinct().ToList();

                if (bUniqueSecondsZ533)
                    ComboBox_KeywordUpdateTab_Camera_Z533Seconds.ForeColor = Color.Black;

                if (!bMissingSecondsZ533 && bDifferentSecondsZ533)
                    ComboBox_KeywordUpdateTab_Camera_Z533Seconds.ForeColor = Color.Green;

                if (bMissingSecondsZ533 && !bDifferentSecondsZ533)
                    ComboBox_KeywordUpdateTab_Camera_Z533Seconds.ForeColor = Color.DarkViolet;

                ComboBox_KeywordUpdateTab_Camera_Z533Seconds.SelectedIndex = bUniqueSecondsZ533 || (bMissingSecondsZ533 && !bDifferentSecondsZ533) ? 0 : -1;
            }


            bool bNoSecondsZ183 = false;
            bool bMissingSecondsZ183 = false;
            bool bDifferentSecondsZ183 = false;
            bool bUniqueSecondsZ183 = false;

            if (bFoundZ183)
            {
                List<double> SecondsListZ183 = mFileList.Where(i => i.ExposureSeconds >= 0 && i.Camera.Contains("183")).Select(i => i.ExposureSeconds).ToList();
                bNoSecondsZ183 = SecondsListZ183.Count == 0;
                bMissingSecondsZ183 = SecondsListZ183.Count != CameraList.Count(c => c.Contains("183")) && !bNoSecondsZ183;
                bDifferentSecondsZ183 = SecondsListZ183.Distinct().Count() > 1;
                bUniqueSecondsZ183 = !bMissingSecondsZ183 && !bDifferentSecondsZ183 && !bNoSecondsZ183;

                ComboBox_KeywordUpdateTab_Camera_Z183Seconds.DataSource = SecondsListZ183.OrderBy(item => item).Distinct().ToList();

                if (bUniqueSecondsZ183)
                    ComboBox_KeywordUpdateTab_Camera_Z183Seconds.ForeColor = Color.Black;

                if (!bMissingSecondsZ183 && bDifferentSecondsZ183)
                    ComboBox_KeywordUpdateTab_Camera_Z183Seconds.ForeColor = Color.Green;

                if (bMissingSecondsZ183 && !bDifferentSecondsZ183)
                    ComboBox_KeywordUpdateTab_Camera_Z183Seconds.ForeColor = Color.DarkViolet;

                ComboBox_KeywordUpdateTab_Camera_Z183Seconds.SelectedIndex = bUniqueSecondsZ183 || (bMissingSecondsZ183 && !bDifferentSecondsZ183) ? 0 : -1;
            }


            bool bNoSecondsQ178 = false;
            bool bMissingSecondsQ178 = false;
            bool bDifferentSecondsQ178 = false;
            bool bUniqueSecondsQ178 = false;

            if (bFoundQ178)
            {
                List<double> SecondsListQ178 = mFileList.Where(i => i.ExposureSeconds >= 0 && i.Camera.Contains("178")).Select(i => i.ExposureSeconds).ToList();
                bNoSecondsQ178 = SecondsListQ178.Count == 0;
                bMissingSecondsQ178 = SecondsListQ178.Count != CameraList.Count(c => c.Contains("178")) && !bNoSecondsQ178;
                bDifferentSecondsQ178 = SecondsListQ178.Distinct().Count() > 1;
                bUniqueSecondsQ178 = !bMissingSecondsQ178 && !bDifferentSecondsQ178 && !bNoSecondsQ178;

                ComboBox_KeywordUpdateTab_Camera_Q178Seconds.DataSource = SecondsListQ178.OrderBy(item => item).Distinct().ToList();

                if (bUniqueSecondsQ178)
                    ComboBox_KeywordUpdateTab_Camera_Q178Seconds.ForeColor = Color.Black;

                if (!bMissingSecondsQ178 && bDifferentSecondsQ178)
                    ComboBox_KeywordUpdateTab_Camera_Q178Seconds.ForeColor = Color.Green;

                if (bMissingSecondsQ178 && !bDifferentSecondsQ178)
                    ComboBox_KeywordUpdateTab_Camera_Q178Seconds.ForeColor = Color.DarkViolet;

                ComboBox_KeywordUpdateTab_Camera_Q178Seconds.SelectedIndex = bUniqueSecondsQ178 || (bMissingSecondsQ178 && !bDifferentSecondsQ178) ? 0 : -1;
            }

            bool bNoSecondsA144 = false;
            bool bMissingSecondsA144 = false;
            bool bDifferentSecondsA144 = false;
            bool bUniqueSecondsA144 = false;

            if (bFoundA144)
            {
                List<double> SecondsListA144 = mFileList.Where(i => i.ExposureSeconds >= 0 && i.Camera.Contains("A44")).Select(i => i.ExposureSeconds).ToList();
                bNoSecondsA144 = SecondsListA144.Count == 0;
                bMissingSecondsA144 = SecondsListA144.Count != CameraList.Count(c => c.Contains("144")) && !bNoSecondsA144;
                bDifferentSecondsA144 = SecondsListA144.Distinct().Count() > 1;
                bUniqueSecondsA144 = !bMissingSecondsA144 && !bDifferentSecondsA144 && !bNoSecondsA144;

                ComboBox_KeywordUpdateTab_Camera_A144Seconds.DataSource = SecondsListA144.OrderBy(item => item).Distinct().ToList();

                if (bUniqueSecondsA144)
                    ComboBox_KeywordUpdateTab_Camera_A144Seconds.ForeColor = Color.Black;

                if (!bMissingSecondsA144 && bDifferentSecondsA144)
                    ComboBox_KeywordUpdateTab_Camera_A144Seconds.ForeColor = Color.Green;

                if (bMissingSecondsA144 && !bDifferentSecondsA144)
                    ComboBox_KeywordUpdateTab_Camera_A144Seconds.ForeColor = Color.DarkViolet;

                ComboBox_KeywordUpdateTab_Camera_A144Seconds.SelectedIndex = bUniqueSecondsA144 || (bMissingSecondsA144 && !bDifferentSecondsA144) ? 0 : -1;
            }

            if (bNoSecondsZ533 || bMissingSecondsZ533 || bNoSecondsZ183 || bMissingSecondsZ183 || bNoSecondsQ178 || bMissingSecondsQ178 || bNoSecondsA144 || bMissingSecondsA144)
                Label_KeywordUpdateTab_Camera_Seconds.ForeColor = Color.Red;
            else
                if (bDifferentSecondsZ533 || bDifferentSecondsZ183 || bDifferentSecondsQ178 || bDifferentSecondsA144)
                Label_KeywordUpdateTab_Camera_Seconds.ForeColor = Color.Green;

            // ****************************************************************

            bool bNoGainsZ533 = false;
            bool bMissingGainsZ533 = false;
            bool bDifferentGainsZ533 = false;
            bool bUniqueGainZ533 = false;

            if (bFoundZ533)
            {
                List<int> GainListZ533 = mFileList.Where(i => i.Gain >= 0 && i.Camera.Contains("533")).Select(i => i.Gain).ToList();
                bNoGainsZ533 = GainListZ533.Count == 0;
                bMissingGainsZ533 = GainListZ533.Count != CameraList.Count(c => c.Contains("533")) && !bNoGainsZ533;
                bDifferentGainsZ533 = GainListZ533.Distinct().Count() > 1 && !bMissingGainsZ533;
                bUniqueGainZ533 = !bMissingGainsZ533 && !bDifferentGainsZ533 && !bNoGainsZ533;

                ComboBox_KeywordUpdateTab_Camera_Z533Gain.DataSource = GainListZ533.OrderBy(item => item).Distinct().ToList();

                if (bUniqueGainZ533)
                    ComboBox_KeywordUpdateTab_Camera_Z533Gain.ForeColor = Color.Black;

                if (!bMissingGainsZ533 && bDifferentGainsZ533)
                    ComboBox_KeywordUpdateTab_Camera_Z533Gain.ForeColor = Color.Green;

                if (bMissingGainsZ533 && !bDifferentGainsZ533)
                    ComboBox_KeywordUpdateTab_Camera_Z533Gain.ForeColor = Color.DarkViolet;

                ComboBox_KeywordUpdateTab_Camera_Z533Gain.SelectedIndex = bUniqueGainZ533 || (bMissingGainsZ533 && !bDifferentGainsZ533) ? 0 : -1;
            }


            bool bNoGainsZ183 = false;
            bool bMissingGainsZ183 = false;
            bool bDifferentGainsZ183 = false;
            bool bUniqueGainZ183 = false;

            if (bFoundZ183)
            {
                List<int> GainListZ183 = mFileList.Where(i => i.Gain >= 0 && i.Camera.Contains("183")).Select(i => i.Gain).ToList();
                bNoGainsZ183 = GainListZ183.Count == 0;
                bMissingGainsZ183 = GainListZ183.Count != CameraList.Count(c => c.Contains("183")) && !bNoGainsZ183;
                bDifferentGainsZ183 = GainListZ183.Distinct().Count() > 1;
                bUniqueGainZ183 = !bMissingGainsZ183 && !bDifferentGainsZ183 && !bNoGainsZ183;

                ComboBox_KeywordUpdateTab_Camera_Z183Gain.DataSource = GainListZ183.OrderBy(item => item).Distinct().ToList();

                if (bUniqueGainZ183)
                    ComboBox_KeywordUpdateTab_Camera_Z183Gain.ForeColor = Color.Black;

                if (!bMissingGainsZ183 && bDifferentGainsZ183)
                    ComboBox_KeywordUpdateTab_Camera_Z183Gain.ForeColor = Color.Green;

                if (bMissingGainsZ183 && !bDifferentGainsZ183)
                    ComboBox_KeywordUpdateTab_Camera_Z183Gain.ForeColor = Color.DarkViolet;

                ComboBox_KeywordUpdateTab_Camera_Z183Gain.SelectedIndex = bUniqueGainZ183 || (bMissingGainsZ183 && !bDifferentGainsZ183) ? 0 : -1;
            }


            bool bNoGainsQ178 = false;
            bool bMissingGainsQ178 = false;
            bool bDifferentGainsQ178 = false;
            bool bUniqueGainQ178 = false;

            if (bFoundQ178)
            {
                List<int> GainListQ178 = mFileList.Where(i => i.Gain >= 0 && i.Camera.Contains("178")).Select(i => i.Gain).ToList();
                bNoGainsQ178 = GainListQ178.Count == 0;
                bMissingGainsQ178 = GainListQ178.Count != CameraList.Count(c => c.Contains("178")) && !bNoGainsQ178;
                bDifferentGainsQ178 = GainListQ178.Distinct().Count() > 1 && !bMissingGainsQ178;
                bUniqueGainQ178 = !bMissingGainsQ178 && !bDifferentGainsQ178 && !bNoGainsQ178;

                ComboBox_KeywordUpdateTab_Camera_Q178Gain.DataSource = GainListQ178.OrderBy(item => item).Distinct().ToList();

                if (bUniqueGainQ178)
                    ComboBox_KeywordUpdateTab_Camera_Q178Gain.ForeColor = Color.Black;

                if (!bMissingGainsQ178 && bDifferentGainsQ178)
                    ComboBox_KeywordUpdateTab_Camera_Q178Gain.ForeColor = Color.Green;

                if (bMissingGainsQ178 && !bDifferentGainsQ178)
                    ComboBox_KeywordUpdateTab_Camera_Q178Gain.ForeColor = Color.DarkViolet;

                ComboBox_KeywordUpdateTab_Camera_Q178Gain.SelectedIndex = bUniqueGainQ178 || (bMissingGainsQ178 && !bDifferentGainsQ178) ? 0 : -1;
            }


            if (bNoGainsZ533 || bMissingGainsZ533 || bNoGainsZ183 || bMissingGainsZ183 || bNoGainsQ178 || bMissingGainsQ178)
                Label_KeywordUpdateTab_Camera_Gain.ForeColor = Color.Red;
            else
                if (bDifferentGainsZ533 || bDifferentGainsZ183 || bDifferentGainsQ178)
                Label_KeywordUpdateTab_Camera_Gain.ForeColor = Color.Green;

            // ****************************************************************


            bool bNoOffsetsZ533 = false;
            bool bMissingOffsetsZ533 = false;
            bool bDifferentOffsetsZ533 = false;
            bool bUniqueOffsetZ533 = false;

            if (bFoundZ533)
            {
                List<int> OffsetListZ533 = mFileList.Where(i => i.Offset >= 0 && i.Camera.Contains("533")).Select(i => i.Offset).ToList();
                bNoOffsetsZ533 = OffsetListZ533.Count == 0;
                bMissingOffsetsZ533 = OffsetListZ533.Count != CameraList.Count(c => c.Contains("533")) && !bNoOffsetsZ533;
                bDifferentOffsetsZ533 = OffsetListZ533.Distinct().Count() > 1 && !bMissingOffsetsZ533;
                bUniqueOffsetZ533 = !bMissingOffsetsZ533 && !bDifferentOffsetsZ533 && !bNoOffsetsZ533;

                ComboBox_KeywordUpdateTab_Camera_Z533Offset.DataSource = OffsetListZ533.OrderBy(item => item).Distinct().ToList();

                if (bUniqueOffsetZ533)
                    ComboBox_KeywordUpdateTab_Camera_Z533Offset.ForeColor = Color.Black;

                if (!bMissingOffsetsZ533 && bDifferentOffsetsZ533)
                    ComboBox_KeywordUpdateTab_Camera_Z533Offset.ForeColor = Color.Green;

                if (bMissingOffsetsZ533 && !bDifferentOffsetsZ533)
                    ComboBox_KeywordUpdateTab_Camera_Z533Offset.ForeColor = Color.DarkViolet;

                ComboBox_KeywordUpdateTab_Camera_Z533Offset.SelectedIndex = bUniqueOffsetZ533 || (bMissingOffsetsZ533 && !bDifferentOffsetsZ533) ? 0 : -1;
            }


            bool bNoOffsetsZ183 = false;
            bool bMissingOffsetsZ183 = false;
            bool bDifferentOffsetsZ183 = false;
            bool bUniqueOffsetZ183 = false;

            if (bFoundZ183)
            {
                List<int> OffsetListZ183 = mFileList.Where(i => i.Offset >= 0 && i.Camera.Contains("183")).Select(i => i.Offset).ToList();
                bNoOffsetsZ183 = OffsetListZ183.Count == 0;
                bMissingOffsetsZ183 = OffsetListZ183.Count != CameraList.Count(c => c.Contains("183")) && !bNoOffsetsZ183;
                bDifferentOffsetsZ183 = OffsetListZ183.Distinct().Count() > 1;
                bUniqueOffsetZ183 = !bMissingOffsetsZ183 && !bDifferentOffsetsZ183 && !bNoOffsetsZ183;

                ComboBox_KeywordUpdateTab_Camera_Z183Offset.DataSource = OffsetListZ183.OrderBy(item => item).Distinct().ToList();

                if (bUniqueOffsetZ183)
                    ComboBox_KeywordUpdateTab_Camera_Z183Offset.ForeColor = Color.Black;

                if (!bMissingOffsetsZ183 && bDifferentOffsetsZ183)
                    ComboBox_KeywordUpdateTab_Camera_Z183Offset.ForeColor = Color.Green;

                if (bMissingOffsetsZ183 && !bDifferentOffsetsZ183)
                    ComboBox_KeywordUpdateTab_Camera_Z183Offset.ForeColor = Color.DarkViolet;

                ComboBox_KeywordUpdateTab_Camera_Z183Offset.SelectedIndex = bUniqueOffsetZ183 || (bMissingOffsetsZ183 && !bDifferentOffsetsZ183) ? 0 : -1;
            }

            bool bNoOffsetsQ178 = false;
            bool bMissingOffsetsQ178 = false;
            bool bDifferentOffsetsQ178 = false;
            bool bUniqueOffsetQ178 = false;

            if (bFoundQ178)
            {
                List<int> OffsetListQ178 = mFileList.Where(i => i.Offset >= 0 && i.Camera.Contains("178")).Select(i => i.Offset).ToList();
                bNoOffsetsQ178 = OffsetListQ178.Count == 0;
                bMissingOffsetsQ178 = OffsetListQ178.Count != CameraList.Count(c => c.Contains("178")) && !bNoOffsetsQ178;
                bDifferentOffsetsQ178 = OffsetListQ178.Distinct().Count() > 1 && !bMissingOffsetsQ178;
                bUniqueOffsetQ178 = !bMissingOffsetsQ178 && !bDifferentOffsetsQ178 && !bNoOffsetsQ178;

                ComboBox_KeywordUpdateTab_Camera_Q178Offset.DataSource = OffsetListQ178.OrderBy(item => item).Distinct().ToList();

                if (bUniqueOffsetQ178)
                    ComboBox_KeywordUpdateTab_Camera_Q178Offset.ForeColor = Color.Black;

                if (!bMissingOffsetsQ178 && bDifferentOffsetsQ178)
                    ComboBox_KeywordUpdateTab_Camera_Q178Offset.ForeColor = Color.Green;

                if (bMissingOffsetsQ178 && !bDifferentOffsetsQ178)
                    ComboBox_KeywordUpdateTab_Camera_Q178Offset.ForeColor = Color.DarkViolet;

                ComboBox_KeywordUpdateTab_Camera_Q178Offset.SelectedIndex = bUniqueOffsetQ178 || (bMissingOffsetsQ178 && !bDifferentOffsetsQ178) ? 0 : -1;
            }

            if (bNoOffsetsZ533 || bMissingOffsetsZ533 || bNoOffsetsZ183 || bMissingOffsetsZ183 || bNoOffsetsQ178 || bMissingOffsetsQ178)
                Label_KeywordUpdateTab_Camera_Offset.ForeColor = Color.Red;
            else
               if (bDifferentOffsetsZ533 || bDifferentOffsetsZ183 || bDifferentOffsetsQ178)
                Label_KeywordUpdateTab_Camera_Offset.ForeColor = Color.Green;


            // ****************************************************************

            bool bNoSensorTempsZ533 = false;
            bool bMissingSensorTempsZ533 = false;
            bool bDifferentSensorTempsZ533 = false;
            bool bUniqueSensorTempZ533 = false;

            if (bFoundZ533)
            {
                List<double> SensorTempListZ533 = mFileList.Where(i => i.SensorTemperature != -273 && i.Camera.Contains("533")).Select(i => i.SensorTemperature).ToList();
                bNoSensorTempsZ533 = SensorTempListZ533.Count == 0;
                bMissingSensorTempsZ533 = SensorTempListZ533.Count != CameraList.Count(c => c.Contains("533")) && !bNoSensorTempsZ533;
                bDifferentSensorTempsZ533 = SensorTempListZ533.Distinct().Count() > 1 && !bMissingSensorTempsZ533;
                bUniqueSensorTempZ533 = !bMissingSensorTempsZ533 && !bDifferentSensorTempsZ533 && !bNoSensorTempsZ533;

                ComboBox_KeywordUpdateTab_Camera_Z533SensorTemp.DataSource = SensorTempListZ533.OrderBy(item => item).Distinct().ToList();

                if (bUniqueSensorTempZ533)
                    ComboBox_KeywordUpdateTab_Camera_Z533SensorTemp.ForeColor = Color.Black;

                if (!bMissingSensorTempsZ533 && bDifferentSensorTempsZ533)
                    ComboBox_KeywordUpdateTab_Camera_Z533SensorTemp.ForeColor = Color.Green;

                if (bMissingSensorTempsZ533 && !bDifferentSensorTempsZ533)
                    ComboBox_KeywordUpdateTab_Camera_Z533SensorTemp.ForeColor = Color.DarkViolet;

                ComboBox_KeywordUpdateTab_Camera_Z533SensorTemp.SelectedIndex = bUniqueSensorTempZ533 || (bMissingSensorTempsZ533 && !bDifferentSensorTempsZ533) ? 0 : -1;
            }

            bool bNoSensorTempsZ183 = false;
            bool bMissingSensorTempsZ183 = false;
            bool bDifferentSensorTempsZ183 = false;
            bool bUniqueSensorTempZ183 = false;

            if (bFoundZ183)
            {
                List<double> SensorTempListZ183 = mFileList.Where(i => i.SensorTemperature != -273 && i.Camera.Contains("183")).Select(i => i.SensorTemperature).ToList();
                bNoSensorTempsZ183 = SensorTempListZ183.Count == 0;
                bMissingSensorTempsZ183 = SensorTempListZ183.Count != CameraList.Count(c => c.Contains("183")) && !bNoSensorTempsZ183;
                bDifferentSensorTempsZ183 = SensorTempListZ183.Distinct().Count() > 1;
                bUniqueSensorTempZ183 = !bMissingSensorTempsZ183 && !bDifferentSensorTempsZ183 && !bNoSensorTempsZ183;

                ComboBox_KeywordUpdateTab_Camera_Z183SensorTemp.DataSource = SensorTempListZ183.OrderBy(item => item).Distinct().ToList();

                if (bUniqueSensorTempZ183)
                    ComboBox_KeywordUpdateTab_Camera_Z183SensorTemp.ForeColor = Color.Black;

                if (!bMissingSensorTempsZ183 && bDifferentSensorTempsZ183)
                    ComboBox_KeywordUpdateTab_Camera_Z183SensorTemp.ForeColor = Color.Green;

                if (bMissingSensorTempsZ183 && !bDifferentSensorTempsZ183)
                    ComboBox_KeywordUpdateTab_Camera_Z183SensorTemp.ForeColor = Color.DarkViolet;

                ComboBox_KeywordUpdateTab_Camera_Z183SensorTemp.SelectedIndex = bUniqueSensorTempZ183 || (bMissingSensorTempsZ183 && !bDifferentSensorTempsZ183) ? 0 : -1;
            }

            bool bNoSensorTempsQ178 = false;
            bool bMissingSensorTempsQ178 = false;
            bool bDifferentSensorTempsQ178 = false;
            bool bUniqueSensorTempQ178 = false;

            if (bFoundQ178)
            {
                List<double> SensorTempListQ178 = mFileList.Where(i => i.FocuserTemperature != -273 && i.Camera.Contains("178")).Select(i => i.FocuserTemperature).ToList();
                bNoSensorTempsQ178 = SensorTempListQ178.Count == 0;
                bMissingSensorTempsQ178 = SensorTempListQ178.Count != CameraList.Count(c => c.Contains("178")) && !bNoSensorTempsQ178;
                bDifferentSensorTempsQ178 = SensorTempListQ178.Distinct().Count() > 1 && !bMissingSensorTempsQ178;
                bUniqueSensorTempQ178 = !bMissingSensorTempsQ178 && !bDifferentSensorTempsQ178 && !bNoSensorTempsQ178;

                ComboBox_KeywordUpdateTab_Camera_Q178SensorTemp.DataSource = SensorTempListQ178.OrderBy(item => item).Distinct().ToList();

                if (bUniqueSensorTempQ178)
                    ComboBox_KeywordUpdateTab_Camera_Q178SensorTemp.ForeColor = Color.Black;

                if (!bMissingSensorTempsQ178 && bDifferentSensorTempsQ178)
                    ComboBox_KeywordUpdateTab_Camera_Q178SensorTemp.ForeColor = Color.Green;

                if (bMissingSensorTempsQ178 && !bDifferentSensorTempsQ178)
                    ComboBox_KeywordUpdateTab_Camera_Q178SensorTemp.ForeColor = Color.DarkViolet;

                ComboBox_KeywordUpdateTab_Camera_Q178SensorTemp.SelectedIndex = bUniqueSensorTempQ178 || (bMissingSensorTempsQ178 && !bDifferentSensorTempsQ178) ? 0 : -1;
            }

            bool bNoSensorTempsA144 = false;
            bool bMissingSensorTempsA144 = false;
            bool bDifferentSensorTempsA144 = false;
            bool bUniqueSensorTempA144 = false;

            if (bFoundA144)
            {
                List<double> SensorTempListA144 = mFileList.Where(i => i.FocuserTemperature != -273 && i.Camera.Contains("144")).Select(i => i.FocuserTemperature).ToList();
                bNoSensorTempsA144 = SensorTempListA144.Count == 0;
                bMissingSensorTempsA144 = SensorTempListA144.Count != CameraList.Count(c => c.Contains("144")) && !bNoSensorTempsA144;
                bDifferentSensorTempsA144 = SensorTempListA144.Distinct().Count() > 1 && !bMissingSensorTempsA144;
                bUniqueSensorTempA144 = !bMissingSensorTempsA144 && !bDifferentSensorTempsA144 && !bNoSensorTempsA144;

                ComboBox_KeywordUpdateTab_Camera_A144SensorTemp.DataSource = SensorTempListA144.OrderBy(item => item).Distinct().ToList();

                if (bUniqueSensorTempA144)
                    ComboBox_KeywordUpdateTab_Camera_A144SensorTemp.ForeColor = Color.Black;

                if (!bMissingSensorTempsA144 && bDifferentSensorTempsA144)
                    ComboBox_KeywordUpdateTab_Camera_A144SensorTemp.ForeColor = Color.Green;

                if (bMissingSensorTempsA144 && !bDifferentSensorTempsA144)
                    ComboBox_KeywordUpdateTab_Camera_A144SensorTemp.ForeColor = Color.DarkViolet;

                ComboBox_KeywordUpdateTab_Camera_A144SensorTemp.SelectedIndex = bUniqueSensorTempA144 || (bMissingSensorTempsA144 && !bDifferentSensorTempsA144) ? 0 : -1;
            }

            if (bNoSensorTempsZ533 || bMissingSensorTempsZ533 || bNoSensorTempsZ183 || bMissingSensorTempsZ183 || bNoSensorTempsQ178 || bMissingSensorTempsQ178 || bNoSensorTempsA144 || bMissingSensorTempsA144)
                Label_KeywordUpdateTab_Camera_SensorTemp.ForeColor = Color.Red;
            else
                if (bDifferentSensorTempsZ533 || bDifferentSensorTempsZ183 || bDifferentSensorTempsQ178 || bDifferentSensorTempsA144)
                Label_KeywordUpdateTab_Camera_SensorTemp.ForeColor = Color.Green;

            // ****************************************************************

            bool bNoBinningsZ533 = false;
            bool bMissingBinningsZ533 = false;
            bool bDifferentBinningsZ533 = false;
            bool bUniqueBinningZ533 = false;

            if (bFoundZ533)
            {
                List<int> BinningListZ533 = mFileList.Where(i => i.Binning > 0 && i.Camera.Contains("533")).Select(i => i.Binning).ToList();
                bNoBinningsZ533 = BinningListZ533.Count == 0;
                bMissingBinningsZ533 = BinningListZ533.Count != CameraList.Count(c => c.Contains("533")) && !bNoBinningsZ533;
                bDifferentBinningsZ533 = BinningListZ533.Distinct().Count() > 1;
                bUniqueBinningZ533 = !bMissingBinningsZ533 && !bDifferentBinningsZ533 && !bNoBinningsZ533;

                ComboBox_KeywordUpdateTab_Camera_Z533Binning.DataSource = BinningListZ533.OrderBy(item => item).Distinct().ToList();

                if (bUniqueBinningZ533)
                    ComboBox_KeywordUpdateTab_Camera_Z533Binning.ForeColor = Color.Black;

                if (!bMissingBinningsZ533 && bDifferentBinningsZ533)
                    ComboBox_KeywordUpdateTab_Camera_Z533Binning.ForeColor = Color.Green;

                if (bMissingBinningsZ533 && !bDifferentBinningsZ533)
                    ComboBox_KeywordUpdateTab_Camera_Z533Binning.ForeColor = Color.DarkViolet;

                ComboBox_KeywordUpdateTab_Camera_Z533Binning.SelectedIndex = bUniqueBinningZ533 || (bMissingBinningsZ533 && !bDifferentBinningsZ533) ? 0 : -1;
            }

            bool bNoBinningsZ183 = false;
            bool bMissingBinningsZ183 = false;
            bool bDifferentBinningsZ183 = false;
            bool bUniqueBinningZ183 = false;

            if (bFoundZ183)
            {
                List<int> BinningListZ183 = mFileList.Where(i => i.Binning > 0 && i.Camera.Contains("183")).Select(i => i.Binning).ToList();
                bNoBinningsZ183 = BinningListZ183.Count == 0;
                bMissingBinningsZ183 = BinningListZ183.Count != CameraList.Count(c => c.Contains("183")) && !bNoBinningsZ183;
                bDifferentBinningsZ183 = BinningListZ183.Distinct().Count() > 1;
                bUniqueBinningZ183 = !bMissingBinningsZ183 && !bDifferentBinningsZ183 && !bNoBinningsZ183;

                ComboBox_KeywordUpdateTab_Camera_Z183Binning.DataSource = BinningListZ183.OrderBy(item => item).Distinct().ToList();

                if (bUniqueBinningZ183)
                    ComboBox_KeywordUpdateTab_Camera_Z183Binning.ForeColor = Color.Black;

                if (!bMissingBinningsZ183 && bDifferentBinningsZ183)
                    ComboBox_KeywordUpdateTab_Camera_Z183Binning.ForeColor = Color.Green;

                if (bMissingBinningsZ183 && !bDifferentBinningsZ183)
                    ComboBox_KeywordUpdateTab_Camera_Z183Binning.ForeColor = Color.DarkViolet;

                ComboBox_KeywordUpdateTab_Camera_Z183Binning.SelectedIndex = bUniqueBinningZ183 || (bMissingBinningsZ183 && !bDifferentBinningsZ183) ? 0 : -1;
            }

            bool bNoBinningsQ178 = false;
            bool bMissingBinningsQ178 = false;
            bool bDifferentBinningsQ178 = false;
            bool bUniqueBinningQ178 = false;

            if (bFoundQ178)
            {
                List<int> BinningListQ178 = mFileList.Where(i => i.Binning > 0 && i.Camera.Contains("178")).Select(i => i.Binning).ToList();
                bNoBinningsQ178 = BinningListQ178.Count == 0;
                bMissingBinningsQ178 = BinningListQ178.Count != CameraList.Count(c => c.Contains("178")) && !bNoBinningsQ178;
                bDifferentBinningsQ178 = BinningListQ178.Distinct().Count() > 1;
                bUniqueBinningQ178 = !bMissingBinningsQ178 && !bDifferentBinningsQ178 && !bNoBinningsQ178;

                ComboBox_KeywordUpdateTab_Camera_Q178Binning.DataSource = BinningListQ178.OrderBy(item => item).Distinct().ToList();

                if (bUniqueBinningQ178)
                    ComboBox_KeywordUpdateTab_Camera_Q178Binning.ForeColor = Color.Black;

                if (!bMissingBinningsQ178 && bDifferentBinningsQ178)
                    ComboBox_KeywordUpdateTab_Camera_Q178Binning.ForeColor = Color.Green;

                if (bMissingBinningsQ178 && !bDifferentBinningsQ178)
                    ComboBox_KeywordUpdateTab_Camera_Q178Binning.ForeColor = Color.DarkViolet;

                ComboBox_KeywordUpdateTab_Camera_Q178Binning.SelectedIndex = bUniqueBinningQ178 || (bMissingBinningsQ178 && !bDifferentBinningsQ178) ? 0 : -1;
            }

            bool bNoBinningsA144 = false;
            bool bMissingBinningsA144 = false;
            bool bDifferentBinningsA144 = false;
            bool bUniqueBinningsA144 = false;

            if (bFoundA144)
            {
                List<int> BinningsListA144 = mFileList.Where(i => i.Binning > 0 && i.Camera.Contains("144")).Select(i => i.Binning).ToList();
                bNoBinningsA144 = BinningsListA144.Count == 0;
                bMissingBinningsA144 = BinningsListA144.Count != CameraList.Count(c => c.Contains("144")) && !bNoBinningsA144;
                bDifferentBinningsA144 = BinningsListA144.Distinct().Count() > 1;
                bUniqueBinningsA144 = !bMissingBinningsA144 && !bDifferentBinningsA144 && !bNoBinningsA144;

                ComboBox_KeywordUpdateTab_Camera_A144Binning.DataSource = BinningsListA144.OrderBy(item => item).Distinct().ToList();

                if (bUniqueBinningsA144)
                    ComboBox_KeywordUpdateTab_Camera_A144Binning.ForeColor = Color.Black;

                if (!bMissingBinningsA144 && bDifferentBinningsA144)
                    ComboBox_KeywordUpdateTab_Camera_A144Binning.ForeColor = Color.Green;

                if (bMissingBinningsA144 && !bDifferentBinningsA144)
                    ComboBox_KeywordUpdateTab_Camera_A144Binning.ForeColor = Color.DarkViolet;

                ComboBox_KeywordUpdateTab_Camera_A144Binning.SelectedIndex = bUniqueBinningsA144 || (bMissingBinningsA144 && !bDifferentBinningsA144) ? 0 : -1;
            }

            if (bNoBinningsZ533 || bMissingBinningsZ533 || bNoBinningsZ183 || bMissingBinningsZ183 || bNoBinningsQ178 || bMissingBinningsQ178 || bNoBinningsA144 || bMissingBinningsA144)
                Label_KeywordUpdateTab_Camera_Binning.ForeColor = Color.Red;
            else
                if (bDifferentBinningsZ533 || bDifferentBinningsZ183 || bDifferentBinningsQ178 || bDifferentBinningsA144)
                Label_KeywordUpdateTab_Camera_Binning.ForeColor = Color.Green;

            // ****************************************************************
            // ****************************************************************
        }
        

        private void Button_KeywordUpdateSubFrameKeywordsCamera_ToggleNB_Click(object sender, EventArgs e)
        {
            if (CheckBox_KeywordUpdateTab_Camera_Z533.Checked)
            {
                if (Label_KeywordUpdateTab_Camera_ToggleNBPreset.Text == "NB Preset")
                {
                    Label_KeywordUpdateTab_Camera_ToggleNBPreset.Text = "BB Preset";
                    ComboBox_KeywordUpdateTab_Camera_Z533Gain.Text = "100";
                    ComboBox_KeywordUpdateTab_Camera_Z533Offset.Text = "50";
                }
                else
                {
                    Label_KeywordUpdateTab_Camera_ToggleNBPreset.Text = "NB Preset";
                    ComboBox_KeywordUpdateTab_Camera_Z533Gain.Text = "100";
                    ComboBox_KeywordUpdateTab_Camera_Z533Offset.Text = "50";
                }
            }

            if (CheckBox_KeywordUpdateTab_Camera_Z183.Checked)
            {
                if (Label_KeywordUpdateTab_Camera_ToggleNBPreset.Text == "NB Preset")
                {
                    Label_KeywordUpdateTab_Camera_ToggleNBPreset.Text = "BB Preset";
                    ComboBox_KeywordUpdateTab_Camera_Z183Gain.Text = "53";
                    ComboBox_KeywordUpdateTab_Camera_Z183Offset.Text = "10";
                }
                else
                {
                    Label_KeywordUpdateTab_Camera_ToggleNBPreset.Text = "NB Preset";
                    ComboBox_KeywordUpdateTab_Camera_Z183Gain.Text = "111";
                    ComboBox_KeywordUpdateTab_Camera_Z183Offset.Text = "10";
                }
            }

            if (CheckBox_KeywordUpdateTab_Camera_Q178.Checked)
            {
                if (Label_KeywordUpdateTab_Camera_ToggleNBPreset.Text == "NB Preset")
                {
                    Label_KeywordUpdateTab_Camera_ToggleNBPreset.Text = "NB Preset";
                    ComboBox_KeywordUpdateTab_Camera_Q178Gain.Text = "40";
                    ComboBox_KeywordUpdateTab_Camera_Q178Offset.Text = "15";
                }
                else
                {
                    Label_KeywordUpdateTab_Camera_ToggleNBPreset.Text = "BB Preset";
                    ComboBox_KeywordUpdateTab_Camera_Q178Gain.Text = "40";
                    ComboBox_KeywordUpdateTab_Camera_Q178Offset.Text = "15";
                }
            }
        }

        private void Button_KeywordCamera_SetAll_Click(object sender, EventArgs e)
        {
            double value;
            int parseInt;
            int checkedCount = 0;
            bool bStatus;

            if (mFileList.Count == 0)
                return;

            checkedCount += CheckBox_KeywordUpdateTab_Camera_A144.Checked ? 1 : 0;
            checkedCount += CheckBox_KeywordUpdateTab_Camera_Q178.Checked ? 1 : 0;
            checkedCount += CheckBox_KeywordUpdateTab_Camera_Z183.Checked ? 1 : 0;
            checkedCount += CheckBox_KeywordUpdateTab_Camera_Z533.Checked ? 1 : 0;

            if (checkedCount != 1)
                return;

            foreach (XisfFile file in mFileList)
            {
                file.RemoveKeyword("NAXIS3");

                file.AddKeyword("BITPIX", "16", "Bits Per Pixel");
                file.AddKeyword("BSCALE", "1", "Multiply Raw Values by BSCALE");
                file.AddKeyword("BZERO", "32768", "Add value to scale to 65536 (16 bit) values");
                file.AddKeyword("NAXIS", "2", "XISF File Manager");


                if (CheckBox_KeywordUpdateTab_Camera_Z533.Checked)
                {
                    file.AddKeyword("INSTRUME", "Z533", "ZWO ASI533MC Pro Camera (2021)");
                    file.AddKeyword("NAXIS1", "3008", "Horizontal Pixel Width");
                    file.AddKeyword("NAXIS2", "3008", "Vertical Pixel Height");
                    file.AddKeyword("XPIXSZ", "3.76", "Horizonal Pixel Size in Microns");
                    file.AddKeyword("YPIXSZ", "3.76", "Vertical Pixel Size in Microns");
                    file.AddKeyword("BAYERPAT", "RGGB");
                    file.AddKeyword("COLORSPC", "Color", "Color Image");

                    bStatus = double.TryParse(ComboBox_KeywordUpdateTab_Camera_Z533Seconds.Text, out value);
                    if (bStatus)
                        file.ExposureSeconds = value;
                    else
                        file.ExposureSeconds = file.ExposureSeconds;

                    bStatus = int.TryParse(ComboBox_KeywordUpdateTab_Camera_Z533Gain.Text, out parseInt);
                    if (bStatus)
                        file.Gain = parseInt;
                    else
                        file.Gain = file.Gain;

                    bStatus = int.TryParse(ComboBox_KeywordUpdateTab_Camera_Z533Offset.Text, out parseInt);
                    if (bStatus)
                        file.Offset = parseInt;
                    else
                        file.Offset = file.Offset;

                    bStatus = int.TryParse(ComboBox_KeywordUpdateTab_Camera_Z533Binning.Text, out parseInt);
                    if (bStatus)
                        file.Binning = parseInt;
                    else
                        file.Binning = file.Binning;

                    bStatus = double.TryParse(ComboBox_KeywordUpdateTab_Camera_Z533SensorTemp.Text, out value);
                    if (bStatus)
                        file.SensorTemperature = value;
                    else
                        file.SensorTemperature = file.SensorTemperature;
                }

                if (CheckBox_KeywordUpdateTab_Camera_Z183.Checked)
                {
                    file.AddKeyword("INSTRUME", "Z183", "ZWO ASI183MM Pro Camera (2019)");
                    file.AddKeyword("NAXIS1", "5496", "Horizontal Pixel Width");
                    file.AddKeyword("NAXIS2", "3672", "Vertical Pixel Height");
                    file.AddKeyword("XPIXSZ", "2.4", "Horizonal Pixel Size in Microns");
                    file.AddKeyword("YPIXSZ", "2.4", "Vertical Pixel Size in Microns");
                    file.AddKeyword("COLORSPC", "Grayscale", "Monochrome Image");

                    bStatus = double.TryParse(ComboBox_KeywordUpdateTab_Camera_Z183Seconds.Text, out value);
                    if (bStatus)
                        file.ExposureSeconds = value;
                    else
                        file.ExposureSeconds = file.ExposureSeconds;

                    bStatus = int.TryParse(ComboBox_KeywordUpdateTab_Camera_Z183Gain.Text, out parseInt);
                    if (bStatus)
                        file.Gain = parseInt;
                    else
                        file.Gain = file.Gain;

                    bStatus = int.TryParse(ComboBox_KeywordUpdateTab_Camera_Z183Offset.Text, out parseInt);
                    if (bStatus)
                        file.Offset = parseInt;
                    else
                        file.Offset = file.Offset;

                    bStatus = int.TryParse(ComboBox_KeywordUpdateTab_Camera_Z183Binning.Text, out parseInt);
                    if (bStatus)
                        file.Binning = parseInt;
                    else
                        file.Binning = file.Binning;

                    bStatus = double.TryParse(ComboBox_KeywordUpdateTab_Camera_Z183SensorTemp.Text, out value);
                    if (bStatus)
                        file.SensorTemperature = value;
                    else
                        file.SensorTemperature = file.SensorTemperature;
                }

                if (CheckBox_KeywordUpdateTab_Camera_Q178.Checked)
                {
                    file.AddKeyword("INSTRUME", "Q178", "QHYCCD QHY5III178M Camera (2018)");
                    file.AddKeyword("NAXIS1", "3072", "Horizontal Pixel Width");
                    file.AddKeyword("NAXIS2", "2048", "Vertical Pixel Height");
                    file.AddKeyword("XPIXSZ", "2.4", "Horizonal Pixel Size in Microns");
                    file.AddKeyword("YPIXSZ", "2.4", "Vertical Pixel Size in Microns");
                    file.AddKeyword("COLORSPC", "Grayscale", "Monochrome Image");

                    bStatus = double.TryParse(ComboBox_KeywordUpdateTab_Camera_Q178Seconds.Text, out value);
                    if (bStatus)
                        file.ExposureSeconds = value;
                    else
                        file.ExposureSeconds = file.ExposureSeconds;

                    bStatus = int.TryParse(ComboBox_KeywordUpdateTab_Camera_Q178Gain.Text, out parseInt);
                    if (bStatus)
                        file.Gain = parseInt;
                    else
                        file.Gain = file.Gain;

                    bStatus = int.TryParse(ComboBox_KeywordUpdateTab_Camera_Q178Offset.Text, out parseInt);
                    if (bStatus)
                        file.Offset = parseInt;
                    else
                        file.Offset = file.Offset;

                    bStatus = int.TryParse(ComboBox_KeywordUpdateTab_Camera_Q178Binning.Text, out parseInt);
                    if (bStatus)
                        file.Binning = parseInt;
                    else
                        file.Binning = file.Binning;

                    bStatus = double.TryParse(ComboBox_KeywordUpdateTab_Camera_Q178SensorTemp.Text, out value);
                    if (bStatus)
                    {
                        file.FocuserTemperature = value;
                        file.SensorTemperature = value;
                    }
                    else
                    {
                        file.FocuserTemperature = file.FocuserTemperature;
                        file.SensorTemperature = file.SensorTemperature;
                    }
                }

                if (CheckBox_KeywordUpdateTab_Camera_A144.Checked)
                {
                    file.AddKeyword("INSTRUME", "A144", "Atik Infinity Camera (2018)");
                    file.AddKeyword("NAXIS1", "1392", "Horizontal Pixel Width");
                    file.AddKeyword("NAXIS2", "1040", "Vertical Pixel Height");
                    file.AddKeyword("XPIXSZ", "6.45", "Horizonal Pixel Size in Microns");
                    file.AddKeyword("YPIXSZ", "6.45", "Vertical Pixel Size in Microns");
                    file.AddKeyword("BAYERPAT", "RGGB");
                    file.AddKeyword("COLORSPC", "Color", "Color Image");
                    file.AddKeyword("GAIN", "0.37", "Fixed");
                    file.RemoveKeyword("OFFSET");

                    bStatus = double.TryParse(ComboBox_KeywordUpdateTab_Camera_A144Seconds.Text, out value);
                    if (bStatus)
                        file.ExposureSeconds = value;
                    else
                        file.ExposureSeconds = file.ExposureSeconds;

                    bStatus = int.TryParse(ComboBox_KeywordUpdateTab_Camera_A144Binning.Text, out parseInt);
                    if (bStatus)
                        file.Binning = parseInt;
                    else
                        file.Binning = file.Binning;

                    bStatus = double.TryParse(ComboBox_KeywordUpdateTab_Camera_A144SensorTemp.Text, out value);
                    if (bStatus)
                        file.SensorTemperature = value;
                    else
                        file.SensorTemperature = file.SensorTemperature;
                }
            }

            FindCamera();
        }

        private void Button_KeywordCamera_SetByFile_Click(object sender, EventArgs e)
        {
            bool status;


            bool globalTemperature = false;
            string globalTemperatureText = string.Empty;
            bool globalSeconds = false;
            string globalSecondsText = string.Empty;
            bool globalGain = false;
            int globalGainValue = -1;

            bool globalOffset = false;
            int globalOffsetValue = -1;


            if (mFileList.Count == 0)
            {
                return;
            }

            foreach (XisfFile xFile in mFileList)
            {
                xFile.RemoveKeyword("NAXIS3");
                xFile.RemoveKeyword("EXPOSURE");

                xFile.AddKeyword("BITPIX", "16", "Bits Per Pixel");
                xFile.AddKeyword("BSCALE", "1", "Multiply Raw Values by BSCALE");
                xFile.AddKeyword("BZERO", "32768", "Add value to scale to 65536 (16 bit) values");
                string temperatureTextUI = ComboBox_KeywordUpdateTab_Camera_Z533SensorTemp.Text;

                string temperatureText;
                if (globalTemperature)
                {
                    temperatureText = xFile.SensorTemperature.ToString();
                    if (temperatureText == string.Empty)
                    {
                        temperatureText = globalTemperatureText;
                    }
                }
                else
                {
                    if (temperatureTextUI == string.Empty)
                    {
                        globalTemperatureText = xFile.SensorTemperature.ToString();
                        if (globalTemperatureText.Contains("Global_"))
                        {
                            globalTemperature = true;
                            globalTemperatureText = globalTemperatureText.Replace("Global_", "");
                        }

                        temperatureText = globalTemperatureText;
                    }
                    else
                    {
                        temperatureText = xFile.SensorTemperature.ToString();
                        if (temperatureText == string.Empty)
                        {
                            temperatureText = temperatureTextUI;
                        }
                    }
                }

                double temperature;
                status = double.TryParse(temperatureText, out temperature);
                xFile.AddKeyword("CCD-TEMP", temperature.ToString(), "Actual Sensor Temperature");

                xFile.AddKeyword("NAXIS", "2", "XISF File Manager");
                xFile.Binning = Int32.Parse(ComboBox_KeywordUpdateTab_Camera_Z533Binning.Text);
                string secondsTextUI = ComboBox_KeywordUpdateTab_Camera_Z533Seconds.Text;

                string secondsText;
                if (globalSeconds)
                {
                    secondsText = xFile.ExposureSeconds.FormatExposureTime();
                    if (secondsText == string.Empty)
                    {
                        secondsText = globalSecondsText;
                    }
                }
                else
                {
                    if (secondsTextUI == string.Empty)
                    {
                        globalSecondsText = xFile.ExposureSeconds.FormatExposureTime();
                        if (globalSecondsText.Contains("Global_"))
                        {
                            globalSeconds = true;
                            globalSecondsText = globalSecondsText.Replace("Global_", "");
                        }

                        secondsText = globalSecondsText;
                    }
                    else
                    {
                        secondsText = xFile.ExposureSeconds.FormatExposureTime();
                        if (secondsText == string.Empty)
                        {
                            secondsText = secondsTextUI;
                        }
                    }
                }

                double seconds;
                status = double.TryParse(secondsText, out seconds);
                xFile.AddKeyword("EXPTIME", seconds.ToString(), "Exposure Time in Seconds");




                int gainValue;
                int gainValueUI;
                int offsetValue;
                int offsetValueUI;
                if (CheckBox_KeywordUpdateTab_Camera_Z533.Checked)
                {
                    xFile.AddKeyword("INSTRUME", "Z533", "ZWO ASI533MC Pro Camera (2021)");
                    xFile.AddKeyword("NAXIS1", "3008", "Horizontal Pixel Width");
                    xFile.AddKeyword("NAXIS2", "3008", "Vertical Pixel Height");
                    xFile.AddKeyword("XPIXSZ", "3.76", "Horizonal Pixel Size in Microns");
                    xFile.AddKeyword("YPIXSZ", "3.76", "Vertical Pixel Size in Microns");
                    xFile.AddKeyword("BAYERPAT", "RGGB");

                    status = int.TryParse(ComboBox_KeywordUpdateTab_Camera_Z533Gain.Text, out gainValueUI);
                    gainValueUI = status ? gainValueUI : -1;

                    if (globalGain)
                    {
                        gainValue = xFile.Gain;
                        if (gainValue < 0)
                        {
                            gainValue = globalGainValue;
                        }
                    }
                    else
                    {
                        if (gainValueUI < 0)
                        {
                            globalGainValue = xFile.Gain;
                            if (globalGainValue < 0)
                            {
                                globalGain = true;
                                globalGainValue = -globalGainValue;
                            }

                            gainValue = globalGainValue;
                        }
                        else
                        {
                            gainValue = xFile.Gain;
                            if (gainValue < 0)
                            {
                                gainValue = gainValueUI;
                            }
                        }
                    }

                    xFile.Gain = gainValue;


                    status = int.TryParse(ComboBox_KeywordUpdateTab_Camera_Z533Offset.Text, out offsetValueUI);
                    offsetValueUI = status ? offsetValueUI : -1;

                    if (globalOffset)
                    {
                        offsetValue = xFile.Offset;
                        if (offsetValue < 0)
                        {
                            offsetValue = globalOffsetValue;
                        }
                    }
                    else
                    {
                        if (offsetValueUI < 0)
                        {
                            globalOffsetValue = xFile.Offset;
                            if (globalOffsetValue < 0)
                            {
                                globalOffset = true;
                                globalOffsetValue = -globalOffsetValue;
                            }

                            offsetValue = globalOffsetValue;
                        }
                        else
                        {
                            offsetValue = xFile.Offset;
                            if (offsetValue < 0)
                            {
                                offsetValue = offsetValueUI;
                            }
                        }
                    }

                    xFile.Offset = offsetValue;
                }

                if (CheckBox_KeywordUpdateTab_Camera_Z183.Checked)
                {
                    xFile.AddKeyword("INSTRUME", "Z183", "ZWO ASI183MM Pro Camera (2019)");
                    xFile.AddKeyword("NAXIS1", "5496", "Horizontal Pixel Width");
                    xFile.AddKeyword("NAXIS2", "3672", "Vertical Pixel Height");
                    xFile.AddKeyword("XPIXSZ", "2.4", "Horizonal Pixel Size in Microns");
                    xFile.AddKeyword("YPIXSZ", "2.4", "Vertical Pixel Size in Microns");
                    xFile.AddKeyword("COLORSPC", "Grayscale", "Monochrome Image");

                    status = int.TryParse(ComboBox_KeywordUpdateTab_Camera_Z183Gain.Text, out gainValueUI);
                    gainValueUI = status ? gainValueUI : -1;

                    if (globalGain)
                    {
                        gainValue = xFile.Gain;
                        if (gainValue < 0)
                        {
                            gainValue = globalGainValue;
                        }
                    }
                    else
                    {
                        if (gainValueUI < 0)
                        {
                            globalGainValue = xFile.Gain;
                            if (globalGainValue < 0)
                            {
                                globalGain = true;
                                globalGainValue = -globalGainValue;
                            }

                            gainValue = globalGainValue;
                        }
                        else
                        {
                            gainValue = xFile.Gain;
                            if (gainValue < 0)
                            {
                                gainValue = gainValueUI;
                            }
                        }
                    }

                    xFile.Gain = gainValue;


                    status = int.TryParse(ComboBox_KeywordUpdateTab_Camera_Z183Offset.Text, out offsetValueUI);
                    offsetValueUI = status ? offsetValueUI : -1;

                    if (globalOffset)
                    {
                        offsetValue = xFile.Offset;
                        if (offsetValue < 0)
                        {
                            offsetValue = globalOffsetValue;
                        }
                    }
                    else
                    {
                        if (offsetValueUI < 0)
                        {
                            globalOffsetValue = xFile.Offset;
                            if (globalOffsetValue < 0)
                            {
                                globalOffset = true;
                                globalOffsetValue = -globalOffsetValue;
                            }

                            offsetValue = globalOffsetValue;
                        }
                        else
                        {
                            offsetValue = xFile.Offset;
                            if (offsetValue < 0)
                            {
                                offsetValue = offsetValueUI;
                            }
                        }
                    }

                    xFile.AddKeyword("OFFSET", offsetValue.ToString(), "Camera Offset");
                }




                if (CheckBox_KeywordUpdateTab_Camera_Q178.Checked)
                {
                    xFile.AddKeyword("INSTRUME", "Q178", "QHYCCD QHY5III178M Camera (2018)");
                    xFile.AddKeyword("NAXIS1", "3072", "Horizontal Pixel Width");
                    xFile.AddKeyword("NAXIS2", "2048", "Vertical Pixel Height");
                    xFile.AddKeyword("XPIXSZ", "2.4", "Horizonal Pixel Size in Microns");
                    xFile.AddKeyword("YPIXSZ", "2.4", "Vertical Pixel Size in Microns");
                    xFile.AddKeyword("COLORSPC", "Grayscale", "Monochrome Image");

                    status = int.TryParse(ComboBox_KeywordUpdateTab_Camera_Q178Gain.Text, out gainValueUI);
                    gainValueUI = status ? gainValueUI : -1;

                    if (globalGain)
                    {
                        gainValue = xFile.Gain;
                        if (gainValue < 0)
                        {
                            gainValue = globalGainValue;
                        }
                    }
                    else
                    {
                        if (gainValueUI < 0)
                        {
                            globalGainValue = xFile.Gain;
                            if (globalGainValue < 0)
                            {
                                globalGain = true;
                                globalGainValue = -globalGainValue;
                            }

                            gainValue = globalGainValue;
                        }
                        else
                        {
                            gainValue = xFile.Gain;
                            if (gainValue < 0)
                            {
                                gainValue = gainValueUI;
                            }
                        }
                    }

                    xFile.Gain = gainValue;


                    status = int.TryParse(ComboBox_KeywordUpdateTab_Camera_Q178Offset.Text, out offsetValueUI);
                    offsetValueUI = status ? offsetValueUI : -1;

                    if (globalOffset)
                    {
                        offsetValue = xFile.Offset;
                        if (offsetValue < 0)
                        {
                            offsetValue = globalOffsetValue;
                        }
                    }
                    else
                    {
                        if (offsetValueUI < 0)
                        {
                            globalOffsetValue = xFile.Offset;
                            if (globalOffsetValue < 0)
                            {
                                globalOffset = true;
                                globalOffsetValue = -globalOffsetValue;
                            }

                            offsetValue = globalOffsetValue;
                        }
                        else
                        {
                            offsetValue = xFile.Offset;
                            if (offsetValue < 0)
                            {
                                offsetValue = offsetValueUI;
                            }
                        }
                    }

                    xFile.Offset = offsetValue;
                }

                if (CheckBox_KeywordUpdateTab_Camera_A144.Checked)
                {
                    xFile.AddKeyword("INSTRUME", "A144", "Atik Infinity Camera (2018)");
                    xFile.AddKeyword("NAXIS1", "1392", "Horizontal Pixel Width");
                    xFile.AddKeyword("NAXIS2", "1040", "Vertical Pixel Height");
                    xFile.AddKeyword("XPIXSZ", "6.45", "Horizonal Pixel Size in Microns");
                    xFile.AddKeyword("YPIXSZ", "6.45", "Vertical Pixel Size in Microns");
                    xFile.AddKeyword("BAYERPAT", "RGGB");
                    xFile.RemoveKeyword("GAIN");
                    xFile.RemoveKeyword("OFFSET");
                }
            }

            FindCamera();
        }
    }
}
