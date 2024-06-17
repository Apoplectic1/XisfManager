using System;
using System.Drawing;
using System.Linq;
using XisfFileManager.Files;

namespace XisfFileManager
{
    public partial class MainForm
    {
        /// <summary>
        /// Clears the selection and resets the telescope group UI elements to their default state.
        /// </summary>
        public void ClearTelescopeGroup()
        {
            // Reset radio buttons and checkboxes
            new[] { RadioButton_KeywordUpdateTab_Telescope_APM107, RadioButton_KeywordUpdateTab_Telescope_EvoStar150, RadioButton_KeywordUpdateTab_Telescope_Newtonian254 }.ToList().ForEach(rb =>
            {
                rb.Checked = false;
                rb.ForeColor = Color.Black;
            });

            CheckBox_KeywordUpdateTab_Telescope_Riccardi.Checked = false;
            CheckBox_KeywordUpdateTab_Telescope_Riccardi.ForeColor = Color.Black;

            // Reset focal length text box and labels
            TextBox_KeywordUpdateTab_Telescope_FocalLength.Text = string.Empty;
            Label_KeywordUpdateTab_Telescope_FocalLength.ForeColor = Color.Black;

            // Reset buttons
            new[] { Button_KeywordUpdateTab_Telescope_SetAll, Button_KeywordUpdateTab_Telescope_SetByFile }.ToList().ForEach(btn => btn.ForeColor = Color.Black);
        }

        /// <summary>
        /// Finds and updates the telescope information based on the files in the file list.
        /// </summary>
        private void FindTelescope()
        {
            if (mFileList.Count == 0) return;

            double focalLength = mFileList.First().FocalLength;
            var telescopes = mFileList.Where(f => !string.IsNullOrEmpty(f.Telescope)).ToList();

            var telescopeCounts = new
            {
                Total = telescopes.Count,
                APM = telescopes.Count(f => f.Telescope.Contains("APM")),
                EVO = telescopes.Count(f => f.Telescope.Contains("EVO")),
                NWT = telescopes.Count(f => f.Telescope.Contains("NWT")),
                Riccardi = telescopes.Count(f => f.Telescope.EndsWith('R')),
                FocalLength = telescopes.Count(f => f.FocalLength != -1)
            };

            bool multipleFocalLengths = telescopes.Select(f => f.FocalLength).Distinct().Count() > 1;
            bool foundFocalLength = telescopeCounts.FocalLength > 0;

            // Update Riccardi checkbox
            CheckBox_KeywordUpdateTab_Telescope_Riccardi.Checked = telescopeCounts.Riccardi == telescopeCounts.Total;
            CheckBox_KeywordUpdateTab_Telescope_Riccardi.ForeColor = telescopeCounts.Riccardi == 0 || telescopeCounts.Riccardi == telescopeCounts.Total ? Color.Black : Color.Red;

            // Update focal length label
            Label_KeywordUpdateTab_Telescope_FocalLength.ForeColor = telescopeCounts.FocalLength == telescopeCounts.Total && foundFocalLength && !multipleFocalLengths ? Color.Black : Color.Red;

            // Update telescope radio buttons and focal length text box
            UpdateTelescopeUI(RadioButton_KeywordUpdateTab_Telescope_APM107, "APM107", 700, 531, telescopeCounts.APM, telescopeCounts);
            UpdateTelescopeUI(RadioButton_KeywordUpdateTab_Telescope_EvoStar150, "EVO150", 1000, 750, telescopeCounts.EVO, telescopeCounts);
            UpdateTelescopeUI(RadioButton_KeywordUpdateTab_Telescope_Newtonian254, "NWT254", 1100, 825, telescopeCounts.NWT, telescopeCounts);

            // Set colors for multiple telescopes or missing data
            if (telescopeCounts.APM == 0 && telescopeCounts.EVO == 0 && telescopeCounts.NWT == 0)
            {
                SetControlsColor(Color.DarkViolet, true);
                return;
            }

            // Update SetAll and SetByFile button colors
            Button_KeywordUpdateTab_Telescope_SetAll.ForeColor = telescopeCounts.Total == telescopeCounts.APM + telescopeCounts.EVO + telescopeCounts.NWT && telescopeCounts.FocalLength == mFileList.Count ? Color.Black : Color.Red;
            Button_KeywordUpdateTab_Telescope_SetByFile.ForeColor = telescopeCounts.Total == mFileList.Count ? Color.Black : Color.Red;
        }

        /// <summary>
        /// Updates the telescope UI elements based on the current selection.
        /// </summary>
        /// <param name="radioButton">The radio button representing the telescope.</param>
        /// <param name="telescopeName">The name of the telescope.</param>
        /// <param name="defaultFocalLength">The default focal length without Riccardi reducer.</param>
        /// <param name="reducedFocalLength">The focal length with Riccardi reducer.</param>
        /// <param name="telescopeCount">The count of the specified telescope in the file list.</param>
        /// <param name="telescopeCounts">The total counts of different telescopes.</param>
        private void UpdateTelescopeUI(System.Windows.Forms.RadioButton radioButton, string telescopeName, int defaultFocalLength, int reducedFocalLength, int telescopeCount, dynamic telescopeCounts)
        {
            if (telescopeCount > 0)
            {
                if (telescopeCount == telescopeCounts.Total)
                {
                    radioButton.Checked = true;
                    TextBox_KeywordUpdateTab_Telescope_FocalLength.Text = CheckBox_KeywordUpdateTab_Telescope_Riccardi.Checked ? reducedFocalLength.ToString() : defaultFocalLength.ToString();
                }
                else
                {
                    radioButton.ForeColor = Color.Red;
                }
            }
        }

        /// <summary>
        /// Sets the color of specified controls.
        /// </summary>
        /// <param name="color">The color to set.</param>
        /// <param name="resetCheckbox">Whether to reset the Riccardi checkbox.</param>
        private void SetControlsColor(Color color, bool resetCheckbox)
        {
            RadioButton_KeywordUpdateTab_Telescope_APM107.ForeColor = color;
            RadioButton_KeywordUpdateTab_Telescope_EvoStar150.ForeColor = color;
            RadioButton_KeywordUpdateTab_Telescope_Newtonian254.ForeColor = color;
            Label_KeywordUpdateTab_Telescope_FocalLength.ForeColor = color;
            CheckBox_KeywordUpdateTab_Telescope_Riccardi.ForeColor = color;
            Button_KeywordUpdateTab_Telescope_SetAll.ForeColor = color;
            Button_KeywordUpdateTab_Telescope_SetByFile.ForeColor = color;

            if (resetCheckbox)
            {
                CheckBox_KeywordUpdateTab_Telescope_Riccardi.Checked = false;
            }
        }

        private void RadioButton_KeywordTelescope_APM107_CheckedChanged(object sender, EventArgs e)
        {
            TextBox_KeywordUpdateTab_Telescope_FocalLength.Text = CheckBox_KeywordUpdateTab_Telescope_Riccardi.Checked ? "531" : "700";

            if (mFileList.Any(file => TextBox_KeywordUpdateTab_Telescope_FocalLength.Text != file.FocalLength.ToString()))
            {
                TextBox_KeywordUpdateTab_Telescope_FocalLength.Text = string.Empty;
                Label_KeywordUpdateTab_Telescope_FocalLength.ForeColor = Color.Red;
            }
            else
            {
                Label_KeywordUpdateTab_Telescope_FocalLength.ForeColor = Color.Black;
            }
        }

        private void RadioButton_KeywordTelescope_EVO150_CheckedChanged(object sender, EventArgs e)
        {
            TextBox_KeywordUpdateTab_Telescope_FocalLength.Text = CheckBox_KeywordUpdateTab_Telescope_Riccardi.Checked ? "750" : "1000";
        }

        private void RadioButton_KeywordTelescope_NWT254_CheckedChanged(object sender, EventArgs e)
        {
            TextBox_KeywordUpdateTab_Telescope_FocalLength.Text = CheckBox_KeywordUpdateTab_Telescope_Riccardi.Checked ? "825" : "1100";
        }

        private void CheckBox_KeywordTelescope_Riccardi_CheckedChanged(object sender, EventArgs e)
        {
            if (RadioButton_KeywordUpdateTab_Telescope_APM107.Checked)
            {
                TextBox_KeywordUpdateTab_Telescope_FocalLength.Text = CheckBox_KeywordUpdateTab_Telescope_Riccardi.Checked ? "531" : "700";
            }

            if (RadioButton_KeywordUpdateTab_Telescope_EvoStar150.Checked)
            {
                TextBox_KeywordUpdateTab_Telescope_FocalLength.Text = CheckBox_KeywordUpdateTab_Telescope_Riccardi.Checked ? "750" : "1000";
            }

            if (RadioButton_KeywordUpdateTab_Telescope_Newtonian254.Checked)
            {
                TextBox_KeywordUpdateTab_Telescope_FocalLength.Text = CheckBox_KeywordUpdateTab_Telescope_Riccardi.Checked ? "825" : "1100";
            }
        }

        private void SetTelescopeUI(XisfFile file)
        {
            if (RadioButton_KeywordUpdateTab_Telescope_APM107.Checked)
            {
                file.AddKeyword("APTDIA", "107.0", "Aperture Diameter in mm");
                file.AddKeyword("APTAREA", "8992.02", "Aperture area in square mm minus obstructions");
                file.AddKeyword("TELESCOP", CheckBox_KeywordUpdateTab_Telescope_Riccardi.Checked ? "APM107R" : "APM107", "APM107 Super ED" + (CheckBox_KeywordUpdateTab_Telescope_Riccardi.Checked ? " with Riccardi 0.75 Reducer" : " without Reducer"));
                file.AddKeyword("FOCALLEN", CheckBox_KeywordUpdateTab_Telescope_Riccardi.Checked ? "531" : "700", "APM107 Super ED" + (CheckBox_KeywordUpdateTab_Telescope_Riccardi.Checked ? " with Riccardi 0.75 Reducer" : " without Reducer"));
            }

            if (RadioButton_KeywordUpdateTab_Telescope_EvoStar150.Checked)
            {
                file.AddKeyword("TELESCOP", CheckBox_KeywordUpdateTab_Telescope_Riccardi.Checked ? "EVO150R" : "EVO150", "EvoStar 150" + (CheckBox_KeywordUpdateTab_Telescope_Riccardi.Checked ? " with Riccardi 0.75 Reducer" : " without Reducer"));
                file.AddKeyword("FOCALLEN", CheckBox_KeywordUpdateTab_Telescope_Riccardi.Checked ? "750" : "1000", "EvoStar 150" + (CheckBox_KeywordUpdateTab_Telescope_Riccardi.Checked ? " with Riccardi 0.75 Reducer" : " without Reducer"));
            }

            if (RadioButton_KeywordUpdateTab_Telescope_Newtonian254.Checked)
            {
                file.AddKeyword("TELESCOP", CheckBox_KeywordUpdateTab_Telescope_Riccardi.Checked ? "NWT254R" : "NWT254", "10 Inch Newtonian" + (CheckBox_KeywordUpdateTab_Telescope_Riccardi.Checked ? " with Riccardi 0.75 Reducer" : " without Reducer"));
                file.AddKeyword("FOCALLEN", CheckBox_KeywordUpdateTab_Telescope_Riccardi.Checked ? "825" : "1100", "10 Inch Newtonian" + (CheckBox_KeywordUpdateTab_Telescope_Riccardi.Checked ? " with Riccardi 0.75 Reducer" : " without Reducer"));
            }
        }

        private void Button_Telescope_SetAll_Click(object sender, EventArgs e)
        {
            mFileList.ForEach(SetTelescopeUI);
            FindTelescope();
        }

        private void Button_Telescope_SetByFile_Click(object sender, EventArgs e)
        {
            bool globalTelescope = false;

            foreach (XisfFile file in mFileList)
            {
                if (globalTelescope)
                {
                    if (string.IsNullOrEmpty(file.Telescope))
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

                        CheckBox_KeywordUpdateTab_Telescope_Riccardi.Checked = telescope.EndsWith('R');
                        RadioButton_KeywordUpdateTab_Telescope_APM107.Checked = telescope.Contains("APM");
                        RadioButton_KeywordUpdateTab_Telescope_EvoStar150.Checked = telescope.Contains("EVO");
                        RadioButton_KeywordUpdateTab_Telescope_Newtonian254.Checked = telescope.Contains("NWT");

                        SetTelescopeUI(file);
                    }
                }
            }

            FindTelescope();
        }
    }
}
