using System;
using System.Drawing;
using XisfFileManager.Files;

namespace XisfFileManager
{
    public partial class MainForm
    {
        public void ClearTelescopeGroup()
        {
            RadioButton_KeywordUpdateTab_Telescope_APM107.Checked = false;
            RadioButton_KeywordUpdateTab_Telescope_EvoStar150.Checked = false;
            RadioButton_KeywordUpdateTab_Telescope_Newtonian254.Checked = false;
            CheckBox_KeywordUpdateTab_Telescope_Riccardi.Checked = false;

            RadioButton_KeywordUpdateTab_Telescope_APM107.ForeColor = Color.Black;
            RadioButton_KeywordUpdateTab_Telescope_EvoStar150.ForeColor = Color.Black;
            RadioButton_KeywordUpdateTab_Telescope_Newtonian254.ForeColor = Color.Black;
            CheckBox_KeywordUpdateTab_Telescope_Riccardi.ForeColor = Color.Black;

            TextBox_KeywordUpdateTab_Telescope_FocalLength.Text = string.Empty;
            Label_KeywordUpdateTab_Telescope_FocalLength.ForeColor = Color.Black;

            Button_KeywordUpdateTab_Telescope_SetAll.ForeColor = Color.Black;
            Button_KeywordUpdateTab_Telescope_SetByFile.ForeColor = Color.Black;
        }

        private void FindTelescope()
        {
            string telescope;
            double focalLength;
            int telescopeCount = 0;
            int riccardiCount = 0;
            int focalCount = 0;
            bool foundAPM = false;
            bool foundEVO = false;
            bool foundNWT = false;
            bool foundRiccardi = false;
            bool multipleFocalLengths = false;
            bool foundFocalLength = false;

            RadioButton_KeywordUpdateTab_Telescope_APM107.Checked = false;
            RadioButton_KeywordUpdateTab_Telescope_APM107.ForeColor = Color.Black;

            RadioButton_KeywordUpdateTab_Telescope_EvoStar150.Checked = false;
            RadioButton_KeywordUpdateTab_Telescope_EvoStar150.ForeColor = Color.Black;

            RadioButton_KeywordUpdateTab_Telescope_Newtonian254.Checked = false;
            RadioButton_KeywordUpdateTab_Telescope_Newtonian254.ForeColor = Color.Black;

            CheckBox_KeywordUpdateTab_Telescope_Riccardi.Checked = false;
            CheckBox_KeywordUpdateTab_Telescope_Riccardi.ForeColor = Color.Black;

            TextBox_KeywordUpdateTab_Telescope_FocalLength.Text = string.Empty;
            Label_KeywordUpdateTab_Telescope_FocalLength.ForeColor = Color.Black;

            Button_KeywordUpdateTab_Telescope_SetAll.ForeColor = Color.Black;
            Button_KeywordUpdateTab_Telescope_SetByFile.ForeColor = Color.Black;

            if (mFileList.Count == 0)
                return;

            focalLength = mFileList[0].FocalLength;

            foreach (XisfFile file in mFileList)
            {
                telescope = file.Telescope;
                if (telescope == string.Empty)
                    continue;

                if (telescope.EndsWith('R'))
                {
                    riccardiCount++;
                    foundRiccardi = true;
                }

                if (telescope.Contains("APM"))
                {
                    telescopeCount++;
                    foundAPM = true;
                }

                if (telescope.Contains("EVO"))
                {
                    telescopeCount++;
                    foundEVO = true;
                }

                if (telescope.Contains("NWT"))
                {
                    telescopeCount++;
                    foundNWT = true;
                }

                if (focalLength != file.FocalLength)
                {
                    multipleFocalLengths = true;
                }

                focalLength = file.FocalLength;
                if (focalLength != -1)
                {
                    focalCount++;
                    foundFocalLength = true;
                }
            }

            if ((riccardiCount != mFileList.Count) && (riccardiCount != 0))
            {
                CheckBox_KeywordUpdateTab_Telescope_Riccardi.ForeColor = Color.Red;
            }
            else
            {
                CheckBox_KeywordUpdateTab_Telescope_Riccardi.Checked = true;
            }

            if ((focalCount != mFileList.Count) || !foundFocalLength || multipleFocalLengths)
            {
                Label_KeywordUpdateTab_Telescope_FocalLength.ForeColor = Color.Red;
            }


            if (foundAPM)
            {
                if (foundEVO || foundNWT)
                {
                    RadioButton_KeywordUpdateTab_Telescope_APM107.ForeColor = Color.Red;
                }
                else
                {
                    RadioButton_KeywordUpdateTab_Telescope_APM107.Checked = true;

                    if (foundRiccardi)
                        TextBox_KeywordUpdateTab_Telescope_FocalLength.Text = "531";
                    else
                        TextBox_KeywordUpdateTab_Telescope_FocalLength.Text = "700";
                }
            }

            if (foundEVO)
            {
                if (foundAPM || foundNWT)
                {
                    RadioButton_KeywordUpdateTab_Telescope_EvoStar150.ForeColor = Color.Red;
                }
                else
                {
                    RadioButton_KeywordUpdateTab_Telescope_EvoStar150.Checked = true;

                    if (foundRiccardi)
                        TextBox_KeywordUpdateTab_Telescope_FocalLength.Text = "750";
                    else
                        TextBox_KeywordUpdateTab_Telescope_FocalLength.Text = "1000";
                }
            }

            if (foundNWT)
            {
                if (foundAPM || foundEVO)
                {
                    RadioButton_KeywordUpdateTab_Telescope_Newtonian254.ForeColor = Color.Red;
                }
                else
                {
                    RadioButton_KeywordUpdateTab_Telescope_Newtonian254.Checked = true;

                    if (foundRiccardi)
                        TextBox_KeywordUpdateTab_Telescope_FocalLength.Text = "825";
                    else
                        TextBox_KeywordUpdateTab_Telescope_FocalLength.Text = "1100";
                }
            }

            if (!foundAPM && !foundEVO & !foundNWT)
            {
                RadioButton_KeywordUpdateTab_Telescope_APM107.ForeColor = Color.DarkViolet;
                RadioButton_KeywordUpdateTab_Telescope_EvoStar150.ForeColor = Color.DarkViolet;
                RadioButton_KeywordUpdateTab_Telescope_Newtonian254.ForeColor = Color.DarkViolet;
                Label_KeywordUpdateTab_Telescope_FocalLength.ForeColor = Color.DarkViolet;
                CheckBox_KeywordUpdateTab_Telescope_Riccardi.Checked = false;
                CheckBox_KeywordUpdateTab_Telescope_Riccardi.ForeColor = Color.DarkViolet;
                Button_KeywordUpdateTab_Telescope_SetAll.ForeColor = Color.Red;
                Button_KeywordUpdateTab_Telescope_SetByFile.ForeColor = Color.Red;
                return;
            }

            // Set SetAll button to black if only a single telescope has been found or a signle focal lenght has been found
            if ((foundAPM ^ foundEVO ^ foundNWT) && (focalCount == mFileList.Count))
            {
                // Set "SetAll" to black if only a single filter and a single frame type was found
                Button_KeywordUpdateTab_Telescope_SetAll.ForeColor = Color.Black;
            }
            else
            {
                // More that one software program - set "SetByFile" to red
                Button_KeywordUpdateTab_Telescope_SetAll.ForeColor = Color.Red;
            }

            if ((telescopeCount < mFileList.Count) || (riccardiCount < mFileList.Count) || (focalCount < mFileList.Count))
            {
                Button_KeywordUpdateTab_Telescope_SetByFile.ForeColor = Color.Red;
            }
        }

        private void RadioButton_KeywordTelescope_APM107_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBox_KeywordUpdateTab_Telescope_Riccardi.Checked)
            {
                TextBox_KeywordUpdateTab_Telescope_FocalLength.Text = "531";
            }
            else
            {
                TextBox_KeywordUpdateTab_Telescope_FocalLength.Text = "700";
            }

            foreach (XisfFile file in mFileList)
            {
                if (TextBox_KeywordUpdateTab_Telescope_FocalLength.Text != file.FocalLength.ToString())
                {
                    TextBox_KeywordUpdateTab_Telescope_FocalLength.Text = "";
                    Label_KeywordUpdateTab_Telescope_FocalLength.ForeColor = Color.Red;
                    break;
                }
            }

            Label_KeywordUpdateTab_Telescope_FocalLength.ForeColor = Color.Black;
        }

        private void RadioButton_KeywordTelescope_EVO150_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBox_KeywordUpdateTab_Telescope_Riccardi.Checked)
            {
                TextBox_KeywordUpdateTab_Telescope_FocalLength.Text = "750";
            }
            else
            {
                TextBox_KeywordUpdateTab_Telescope_FocalLength.Text = "1000";
            }
        }

        private void RadioButton_KeywordTelescope_NWT254_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBox_KeywordUpdateTab_Telescope_Riccardi.Checked)
            {
                TextBox_KeywordUpdateTab_Telescope_FocalLength.Text = "825";
            }
            else
            {
                TextBox_KeywordUpdateTab_Telescope_FocalLength.Text = "1100";
            }
        }

        private void CheckBox_KeywordTelescope_Riccardi_CheckedChanged(object sender, EventArgs e)
        {
            if (RadioButton_KeywordUpdateTab_Telescope_APM107.Checked)
            {
                if (CheckBox_KeywordUpdateTab_Telescope_Riccardi.Checked)
                    TextBox_KeywordUpdateTab_Telescope_FocalLength.Text = "531";
                else
                    TextBox_KeywordUpdateTab_Telescope_FocalLength.Text = "700";
            }

            if (RadioButton_KeywordUpdateTab_Telescope_EvoStar150.Checked)
            {
                if (CheckBox_KeywordUpdateTab_Telescope_Riccardi.Checked)
                    TextBox_KeywordUpdateTab_Telescope_FocalLength.Text = "750";
                else
                    TextBox_KeywordUpdateTab_Telescope_FocalLength.Text = "1000";
            }

            if (RadioButton_KeywordUpdateTab_Telescope_Newtonian254.Checked)
            {
                if (CheckBox_KeywordUpdateTab_Telescope_Riccardi.Checked)
                    TextBox_KeywordUpdateTab_Telescope_FocalLength.Text = "825";
                else
                    TextBox_KeywordUpdateTab_Telescope_FocalLength.Text = "1100";
            }
        }

        private void SetTelescopeUI(XisfFile file)
        {
            if (RadioButton_KeywordUpdateTab_Telescope_APM107.Checked)
            {
                file.AddKeyword("APTDIA", "107.0", "Aperture Diameter in mm");
                file.AddKeyword("APTAREA", "8992.02", "Aperture area in square mm minus obstructions");

                if (CheckBox_KeywordUpdateTab_Telescope_Riccardi.Checked)
                {
                    file.AddKeyword("TELESCOP", "APM107R", "APM107 Super ED with Riccardi 0.75 Reducer");
                    file.AddKeyword("FOCALLEN", "531", "APM107 Super ED with Riccardi 0.75 Reducer");
                }
                else
                {
                    file.AddKeyword("TELESCOP", "APM107", "APM107 Super ED without Reducer");
                    file.AddKeyword("FOCALLEN", "700", "APM107 Super ED without Reducer");
                }
            }

            if (RadioButton_KeywordUpdateTab_Telescope_EvoStar150.Checked)
            {
                if (CheckBox_KeywordUpdateTab_Telescope_Riccardi.Checked)
                {
                    file.AddKeyword("TELESCOP", "EVO150R", "EvoStar 150 with Riccardi 0.75 Reducer");
                    file.AddKeyword("FOCALLEN", "750", "EvoStar 150 with Riccardi 0.75 Reducer");
                }
                else
                {
                    file.AddKeyword("TELESCOP", "EVO150", "EvoStar 150 without Reducer");
                    file.AddKeyword("FOCALLEN", "1000", "EvoStar 150 without Reducer");
                }
            }

            if (RadioButton_KeywordUpdateTab_Telescope_Newtonian254.Checked)
            {
                if (CheckBox_KeywordUpdateTab_Telescope_Riccardi.Checked)
                {
                    file.AddKeyword("TELESCOP", "NWT254R", "10 Inch Newtownian with Riccardi 0.75 Reducer");
                    file.AddKeyword("FOCALLEN", "825", "10 inch Newtonian with Riccardi 0.75 Reducer");
                }
                else
                {
                    file.AddKeyword("TELESCOP", "NWT254", "10 Inch Newtonian without Reducer");
                    file.AddKeyword("FOCALLEN", "1100", "10 Inch Newtonian without Reducer");
                }
            }
        }

        private void Button_Telescope_SetAll_Click(object sender, EventArgs e)
        {
            foreach (XisfFile file in mFileList)
            {
                SetTelescopeUI(file);
            }

            FindTelescope();
        }

        private void Button_Telescope_SetByFile_Click(object sender, EventArgs e)
        {
            bool globalTelescope = false;
            foreach (XisfFile file in mFileList)
            {
                if (globalTelescope)
                {
                    if (file.Telescope == string.Empty)
                    {
                        SetTelescopeUI(file);
                    }
                }
                else
                {
                    string telescope = file.Telescope;
                    if (telescope.Contains("Global_"))
                    {
                        globalTelescope = true;
                        telescope = telescope.Replace("Global_", "");

                        if (telescope.EndsWith('R'))
                            CheckBox_KeywordUpdateTab_Telescope_Riccardi.Checked = true;
                        else
                            CheckBox_KeywordUpdateTab_Telescope_Riccardi.Checked = false;

                        // Checking the radio button for the found telescope with also set focal length and Riccardi checkbox
                        RadioButton_KeywordUpdateTab_Telescope_APM107.Checked = telescope.Contains("APM") ? true : false;
                        RadioButton_KeywordUpdateTab_Telescope_EvoStar150.Checked = telescope.Contains("EVO") ? true : false;
                        RadioButton_KeywordUpdateTab_Telescope_Newtonian254.Checked = telescope.Contains("NWT") ? true : false;

                        SetTelescopeUI(file);
                    }
                }
            }

            FindTelescope();
        }

    }
}
