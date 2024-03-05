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
        private void RefreshComboBoxes()
        {
            ComboBox_KeywordUpdateTab_SubFrameKeywords_KeywordFile.Items.Clear();
            ComboBox_KeywordUpdateTab_SubFrameKeywords_KeywordFile.Text = "File";

            ComboBox_KeywordUpdateTab_SubFrameKeywords_KeywordName.Items.Clear();
            ComboBox_KeywordUpdateTab_SubFrameKeywords_KeywordName.Text = "Name";

            ComboBox_KeywordUpdateTab_SubFrameKeywords_KeywordValue.Items.Clear();
            ComboBox_KeywordUpdateTab_SubFrameKeywords_KeywordValue.Text = "Value";

            ComboBox_KeywordUpdateTab_SubFrameKeywords_KeywordComment.Items.Clear();
            ComboBox_KeywordUpdateTab_SubFrameKeywords_KeywordComment.Text = "Comment";


            List<string> keywordNamelist = new List<string>();

            foreach (XisfFile xFile in mFileList)
            {
                foreach (Keyword keywordName in xFile.KeywordList.mKeywordList)
                {
                    keywordNamelist.Add(keywordName.Name);
                }
            }

            keywordNamelist.Sort();
            keywordNamelist = keywordNamelist.Distinct().ToList();

            foreach (string name in keywordNamelist)
            {
                ComboBox_KeywordUpdateTab_SubFrameKeywords_KeywordName.Items.Add(name);
            }
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

        private void ComboBox_KeywordUpdateTab_SubFrameKeywords_KeywordName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((string.IsNullOrEmpty(ComboBox_KeywordUpdateTab_SubFrameKeywords_KeywordName.Text) || ComboBox_KeywordUpdateTab_SubFrameKeywords_KeywordName.Text == "Keyword"))
                return;

            List<Keyword> keywordList = new List<Keyword>();

            foreach (XisfFile file in mFileList)
            {
                foreach (Keyword keyword in file.KeywordList.mKeywordList)
                {
                    keywordList.Add(keyword);
                }
            }

            // Uniquify the keywordList based on Keyword.Name and Keyword.Value while keeping associated Comment and FilePath
            keywordList = keywordList
                .GroupBy(k => new { k.Name, k.Value })
                .Select(g => g.First())
                .OrderBy(k => k.Name)
                .ToList();

            ComboBox_KeywordUpdateTab_SubFrameKeywords_KeywordValue.Items.Clear();
            ComboBox_KeywordUpdateTab_SubFrameKeywords_KeywordValue.Text = "";

            ComboBox_KeywordUpdateTab_SubFrameKeywords_KeywordComment.Items.Clear();
            ComboBox_KeywordUpdateTab_SubFrameKeywords_KeywordComment.Text = "";

            foreach (Keyword value in keywordList)
            {
                if (ComboBox_KeywordUpdateTab_SubFrameKeywords_KeywordName.Text == value.Name)
                {
                    ComboBox_KeywordUpdateTab_SubFrameKeywords_KeywordValue.Items.Add(value.Value);
                    ComboBox_KeywordUpdateTab_SubFrameKeywords_KeywordValue.Text = value.Value.ToString();
                    ComboBox_KeywordUpdateTab_SubFrameKeywords_KeywordComment.Text = value.Comment;
                }
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
                bStatus = mXisfFileUpdate.UpdateFile(xFile, xFile.FilePath);
                //bStatus = mXisfFileUpdate.UpdateFile2(xFile.FilePath, Path.GetDirectoryName(xFile.FilePath) + "\\" + Path.GetFileNameWithoutExtension(xFile.FilePath) + "_GPU.xisf");
                Label_KeywordUpdateTab_FileName.Text = Label_KeywordUpdateTab_FileName.Text = Path.GetDirectoryName(xFile.FilePath) + "\n" + Path.GetFileName(xFile.FilePath);
                System.Windows.Forms.Application.DoEvents();

                if (bStatus == false)
                {
                    Label_FileSelection_Statistics_Task.Text = "File Write Error";

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

            Label_FileSelection_Statistics_Task.Text = count.ToString() + " Images Updated";
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
