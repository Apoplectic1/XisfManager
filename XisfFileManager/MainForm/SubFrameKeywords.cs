using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using XisfFileManager.Enums;
using XisfFileManager.Files;

namespace XisfFileManager
{
    public partial class MainForm
    {
        /// <summary>
        /// Refreshes the combo boxes for keyword selection by clearing existing items and populating them with unique, sorted keyword names.
        /// </summary>
        private void RefreshComboBoxes()
        {
            // Clear and reset combo box items and texts
            ClearComboBox(ComboBox_KeywordUpdateTab_SubFrameKeywords_KeywordFile, "File");
            ClearComboBox(ComboBox_KeywordUpdateTab_SubFrameKeywords_KeywordName, "Name");
            ClearComboBox(ComboBox_KeywordUpdateTab_SubFrameKeywords_KeywordValue, "Value");
            ClearComboBox(ComboBox_KeywordUpdateTab_SubFrameKeywords_KeywordComment, "Comment");

            // Get a distinct, sorted list of keyword names from all files in the file list
            var keywordNames = mFileList
                .SelectMany(file => file.KeywordList.mKeywordList.Select(keyword => keyword.Name))
                .Distinct()
                .OrderBy(name => name)
                .ToList();

            // Add keyword names to the keyword name combo box
            keywordNames.ForEach(name => ComboBox_KeywordUpdateTab_SubFrameKeywords_KeywordName.Items.Add(name));
        }

        /// <summary>
        /// Clears the items and sets the text of a combo box.
        /// </summary>
        /// <param name="comboBox">The combo box to clear and reset.</param>
        /// <param name="defaultText">The default text to set for the combo box.</param>
        private static void ClearComboBox(ComboBox comboBox, string defaultText)
        {
            comboBox.Items.Clear();
            comboBox.Text = defaultText;
        }


        private void RadioButton_KeywordUpdateTab_SubFrameKeywords_KeywordProtection_Protect_CheckedChanged(object sender, EventArgs e)
        {
            mKeywordUpdateProtection = (RadioButton_KeywordUpdateTab_SubFrameKeywords_KeywordProtection_Protect.Checked) ? eKeywordUpdateMode.PROTECT : mKeywordUpdateProtection;
        }

        private void RadioButton_KeywordUpdateTab_SubFrameKeywords_KeywordProtection_UpdateNew_CheckedChanged(object sender, EventArgs e)
        {
            mKeywordUpdateProtection = (RadioButton_KeywordUpdateTab_SubFrameKeywords_KeywordProtection_UpdateNew.Checked) ? eKeywordUpdateMode.UPDATE_NEW : mKeywordUpdateProtection;
        }

        private void RadioButton_KeywordUpdateTab_SubFrameKeywords_KeywordProtection_Force_CheckedChanged(object sender, EventArgs e)
        {
            mKeywordUpdateProtection = (RadioButton_KeywordUpdateTab_SubFrameKeywords_KeywordProtection_Force.Checked) ? eKeywordUpdateMode.FORCE : mKeywordUpdateProtection;
        }

        /// <summary>
        /// Handles the event when the keyword name combo box selection is changed.
        /// Populates the value and comment combo boxes with unique values and comments associated with the selected keyword name.
        /// </summary>
        private void ComboBox_KeywordUpdateTab_SubFrameKeywords_KeywordName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ComboBox_KeywordUpdateTab_SubFrameKeywords_KeywordName.Text) ||
                ComboBox_KeywordUpdateTab_SubFrameKeywords_KeywordName.Text == "Keyword")
                return;

            // Get a distinct, sorted list of keywords from all files in the file list
            var keywordList = mFileList
                .SelectMany(file => file.KeywordList.mKeywordList)
                .GroupBy(k => new { k.Name, k.Value })
                .Select(g => g.First())
                .OrderBy(k => k.Name)
                .ToList();

            // Clear and reset value and comment combo boxes
            ClearComboBox(ComboBox_KeywordUpdateTab_SubFrameKeywords_KeywordValue, "");
            ClearComboBox(ComboBox_KeywordUpdateTab_SubFrameKeywords_KeywordComment, "");

            // Populate the value and comment combo boxes with values and comments for the selected keyword name
            var selectedKeywordName = ComboBox_KeywordUpdateTab_SubFrameKeywords_KeywordName.Text;
            var matchingKeywords = keywordList.Where(k => k.Name == selectedKeywordName);

            foreach (var keyword in matchingKeywords)
            {
                ComboBox_KeywordUpdateTab_SubFrameKeywords_KeywordValue.Items.Add(keyword.Value);
                ComboBox_KeywordUpdateTab_SubFrameKeywords_KeywordValue.Text = keyword.Value;
                ComboBox_KeywordUpdateTab_SubFrameKeywords_KeywordComment.Text = keyword.Comment;
            }
        }

        private void ComboBox_KeywordUpdateTab_SubFrameKeywords_KeywordValue_SelectedValueChanged(object sender, EventArgs e)
        {
            List<Keyword> keywordList = new List<Keyword>();

            foreach (XisfFile file in mFileList)
            {
                if (RadioButton_KeywordUpdateTab_SubFrameKeywords_AllValues.Checked)
                    file.KeywordList.AddKeyword(ComboBox_KeywordUpdateTab_SubFrameKeywords_KeywordName.Text, ComboBox_KeywordUpdateTab_SubFrameKeywords_KeywordValue.Text, ComboBox_KeywordUpdateTab_SubFrameKeywords_KeywordComment.Text);

                if (RadioButton_KeywordUpdateTab_SubFrameKeywords_SpecificValue.Checked)
                {
                    file.KeywordList.RemoveKeyword(ComboBox_KeywordUpdateTab_SubFrameKeywords_KeywordName.Text, ComboBox_KeywordUpdateTab_SubFrameKeywords_KeywordValue.Text);
                    file.KeywordList.AddKeywordKeepDuplicates(ComboBox_KeywordUpdateTab_SubFrameKeywords_KeywordName.Text, ComboBox_KeywordUpdateTab_SubFrameKeywords_KeywordValue.Text, ComboBox_KeywordUpdateTab_SubFrameKeywords_KeywordComment.Text);
                }
            }
        }

        private void Button_KeywordUpdateTab_SubFrameKeywords_Delete_Click(object sender, EventArgs e)
        {
            string name = ComboBox_KeywordUpdateTab_SubFrameKeywords_KeywordName.Text;
            string value = ComboBox_KeywordUpdateTab_SubFrameKeywords_KeywordValue.Text;

            foreach (XisfFile xFile in mFileList)
            {
                if (RadioButton_KeywordUpdateTab_SubFrameKeywords_AllValues.Checked)
                    xFile.RemoveKeyword(ComboBox_KeywordUpdateTab_SubFrameKeywords_KeywordName.Text);

                if (RadioButton_KeywordUpdateTab_SubFrameKeywords_SpecificValue.Checked)
                    xFile.RemoveKeyword(ComboBox_KeywordUpdateTab_SubFrameKeywords_KeywordName.Text, ComboBox_KeywordUpdateTab_SubFrameKeywords_KeywordValue.Text);
            }

            RefreshComboBoxes();
        }

        private void Button_KeywordUpdateTab_SubFrameKeywords_AddReplace_Click(object sender, EventArgs e)
        {
            foreach (XisfFile xFile in mFileList)
            {
                if (RadioButton_KeywordUpdateTab_SubFrameKeywords_AllValues.Checked)
                    xFile.KeywordList.AddKeyword(ComboBox_KeywordUpdateTab_SubFrameKeywords_KeywordName.Text, ComboBox_KeywordUpdateTab_SubFrameKeywords_KeywordValue.Text, ComboBox_KeywordUpdateTab_SubFrameKeywords_KeywordComment.Text);

                if (RadioButton_KeywordUpdateTab_SubFrameKeywords_SpecificValue.Checked)
                {
                    xFile.KeywordList.RemoveKeyword(ComboBox_KeywordUpdateTab_SubFrameKeywords_KeywordName.Text, ComboBox_KeywordUpdateTab_SubFrameKeywords_KeywordValue.Text);
                    xFile.KeywordList.AddKeywordKeepDuplicates(ComboBox_KeywordUpdateTab_SubFrameKeywords_KeywordName.Text, ComboBox_KeywordUpdateTab_SubFrameKeywords_KeywordValue.Text, ComboBox_KeywordUpdateTab_SubFrameKeywords_KeywordComment.Text);
                }
            }

            RefreshComboBoxes();
        }

        private void Button_SubFrameKeyword_UpdateXisfFiles_Click(object sender, EventArgs e)
        {
            if (RadioButton_KeywordUpdateTab_SubFrameKeywords_KeywordProtection_Protect.Checked)
                return;

            bool bStatus;
            GroupBox_FileSelection.Enabled = false;
            GroupBox_KeywordUpdateTab_SubFrameKeywords.Enabled = false;
            GroupBox_KeywordUpdateTab_CaptureSoftware.Enabled = false;
            GroupBox_KeywordUpdateTab_Telescope.Enabled = false;
            GroupBox_KeywordUpdateTab_Camera.Enabled = false;
            GroupBox_KeywordUpdateTab_ImageType.Enabled = false;
            ProgressBar_KeywordUpdateTab_WriteProgress.Value = 0;
            ProgressBar_KeywordUpdateTab_WriteProgress.Maximum = mFileList.Count;

            // If multiple Targets or if a Target has multiple Panels do not update with the ComboBox Text
            List<string> targetNames = new List<string>();
            targetNames.Clear();
            foreach (string target in ComboBox_KeywordUpdateTab_SubFrameKeywords_TargetNames.Items)
            {
                // Remove " Stars" from targetName so there is a single target name for the next foreach below (" Stars" will be added there)
                string targetName = target.Replace(" Stars", "");
                targetNames.Add(targetName.Trim());
            }
            targetNames = targetNames.Distinct().ToList();


            int count = 0;
            foreach (XisfFile xFile in mFileList)
            {
                xFile.KeywordUpdateMode = mKeywordUpdateProtection;
                if (xFile.KeywordUpdateMode == eKeywordUpdateMode.PROTECT)
                    return;

                if (mBCancel) { mBCancel = false; return; }

                xFile.SetObservationSite();
                xFile.KeepPanel = CheckBox_KeywordUpdateTab_SubFrameKeywords_UpdatePanelName.Checked;

                // Update with ComboBox Text if checked
                if (CheckBox_KeywordUpdateTab_SubFrameKeywords_UpdateTargetName.Checked)
                    // Rename everything to the ComboBox Text value
                    xFile.TargetName = ComboBox_KeywordUpdateTab_SubFrameKeywords_TargetNames.Text;

                ProgressBar_KeywordUpdateTab_WriteProgress.Value += 1;

               // if (xFile.FilePath.Contains("reject", StringComparison.OrdinalIgnoreCase))
               // {
               //     xFile.AddKeyword("CREJECT", "Included", "NSG or Other Rejected Frame");
               // }

                bStatus = mXisfFileUpdate.UpdateFile(xFile, xFile.FilePath);
                Label_KeywordUpdateTab_FileName.Text = Label_KeywordUpdateTab_FileName.Text = Path.GetDirectoryName(xFile.FilePath) + "\n" + Path.GetFileName(xFile.FilePath);
                System.Windows.Forms.Application.DoEvents();

                if (bStatus == false)
                {
                    Label_FileSelection_Statistics_OperationStatus.Text = "File Write Error";

                    DialogResult result = MessageBox.Show(
                        "File Update Failed - Protected or I/O Error.\n\n" + Label_KeywordUpdateTab_FileName.Text,
                        "\nMainForm.cs Button_Update_Click()",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);

                    GroupBox_FileSelection.Enabled = true;
                    GroupBox_KeywordUpdateTab_SubFrameKeywords.Enabled = true;
                    GroupBox_KeywordUpdateTab_CaptureSoftware.Enabled = true;
                    GroupBox_KeywordUpdateTab_Telescope.Enabled = true;
                    GroupBox_KeywordUpdateTab_Camera.Enabled = true;
                    GroupBox_KeywordUpdateTab_ImageType.Enabled = true;
                    return;
                }

                count++;
            }

            Label_FileSelection_Statistics_OperationStatus.Text = count.ToString() + " Images Updated";
            GroupBox_FileSelection.Enabled = true;
            GroupBox_KeywordUpdateTab_SubFrameKeywords.Enabled = true;
            GroupBox_KeywordUpdateTab_CaptureSoftware.Enabled = true;
            GroupBox_KeywordUpdateTab_Telescope.Enabled = true;
            GroupBox_KeywordUpdateTab_Camera.Enabled = true;
            GroupBox_KeywordUpdateTab_ImageType.Enabled = true;


            FindFilterFrameType(); // Update UI - NOT SURE WHY I NEED THIS HERE
        }

    }
}
