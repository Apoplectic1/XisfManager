using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using XisfFileManager.Enums;
using XisfFileManager.Files;

namespace XisfFileManager
{
    public partial class MainForm
    {
        public void SetDirectoryStatistics()
        {
            // Potentally rename containing Directory
            mDirectoryProperties.SetDirectoryStatistics(mFileList, CheckBox_FileSlection_NoTotals.Checked);

            // Iterate through each Target, Camera and associated Panel Directory
            foreach (KeyValuePair<string, string> group in mDirectoryProperties.DirectoryStatistics)
            {
                string currentDirectory = group.Key;
                string newDirectory = group.Value;

                if (currentDirectory.Equals(newDirectory))
                    continue;

                if (!Directory.Exists(newDirectory))
                    Directory.Move(currentDirectory, newDirectory);
            }
        }

        public void SetFileIndex(bool bFilter, bool bTime)
        {
            if (bTime)
            {
                int index = 0;

                foreach (XisfFile xFile in mFileList)
                {
                    xFile.FileNameNumberIndex = ++index;
                }
                return;
            }

            foreach (KeyValuePair<string, string> group in mDirectoryProperties.DirectoryStatistics)
            {
                int lumaIndex = 0;
                int redIndex = 0;
                int greenIndex = 0;
                int blueIndex = 0;
                int haIndex = 0;
                int o3Index = 0;
                int s2Index = 0;
                int shutterIndex = 0;

                // The newDirectory may or may not be the same as the original directory.
                // The group.Key will index into the DirectoryStatistics Dictionary to get the directory we are working in.
                // This allows us to rename the directory BEFORE renaming the files in the renamed directory without changing mFileList and
                // it's an attempt to avoid ndows file rename/busy problems doing this the other way around.

                string originalDirectory = group.Key;
                string newDirectory = mDirectoryProperties.DirectoryStatistics[group.Key];



                foreach (XisfFile xFile in mFileList)
                {
                    // Assuptions
                    // For a given Target, we are assuming that there are one set of captures (Filters) under a particular Camera.
                    // or a Panel in a Mosaic (the Mosaic Panel is effectively treaded as unique target.
                    // 
                    // We are also assuming that mFileList is in time sequential order independent of Filter (this is done before SetFileIndex() is called).

                    // This code will sequentially number Filter image files in each group

                    if (xFile.FilePath.Contains(originalDirectory))
                    {
                        if (xFile.FilterName.Equals("Luma"))
                            xFile.FileNameNumberIndex = ++lumaIndex;

                        if (xFile.FilterName.Equals("Red"))
                            xFile.FileNameNumberIndex = ++redIndex;

                        if (xFile.FilterName.Equals("Green"))
                            xFile.FileNameNumberIndex = ++greenIndex;

                        if (xFile.FilterName.Equals("Blue"))
                            xFile.FileNameNumberIndex = ++blueIndex;

                        if (xFile.FilterName.Equals("Ha"))
                            xFile.FileNameNumberIndex = ++haIndex;

                        if (xFile.FilterName.Equals("O3"))
                            xFile.FileNameNumberIndex = ++o3Index;

                        if (xFile.FilterName.Equals("S2"))
                            xFile.FileNameNumberIndex = ++s2Index;

                        if (xFile.FilterName.Equals("Shutter"))
                            xFile.FileNameNumberIndex = ++shutterIndex;
                    }
                }
            }
        }

        private void Button_Rename_Click(object sender, EventArgs e)
        {
            bool bFilter = RadioButton_FileSelection_Index_ByFilter.Checked;
            bool bTime = RadioButton_FileSelection_Index_ByTime.Checked;

            Label_FileSelection_Statistics_Task.Text = "Renaming " + mFileList.Count.ToString() + " Images";

            ProgressBar_KeywordUpdateTab_WriteProgress.Maximum = mFileList.Count;
            ProgressBar_KeywordUpdateTab_WriteProgress.Value = 0;

            int duplicates = XisfFileRename.MoveDuplicates(mFileList);

            // SetFileIndex will preset the index for each file in mFileList based on the bools for Target, Night (by existing subdirectory (typically yyyy-mm-dd)), Filter and Time (Date and Time)
            // Filters with different exposure times are not considered to be unique meaning a 600 second Blue filter uses the same index list as 60 second Blue filter
            // An exception to this is if the containing directory includes the word "Stars". Files in "Stars" directories have unique Filter indexes that are independent of exposure time. 
            // Any found Duplicates are handled inside the RenameFile method

            SetDirectoryStatistics();

            SetFileIndex(bFilter, bTime);

            foreach (XisfFile xFile in mFileList)
            {
                if (mBCancel) { mBCancel = false; return; }

                ProgressBar_KeywordUpdateTab_WriteProgress.Value += 1;

                string key = Path.GetDirectoryName(xFile.FilePath);
                xFile.FilePath = mDirectoryProperties.DirectoryStatistics[key] + "\\" + Path.GetFileName(xFile.FilePath);

                Label_FileSelection_BrowseFileName.Text = Path.GetDirectoryName(xFile.FilePath) + "\n" + Path.GetFileName(xFile.FilePath);

                Tuple<int, string> renameTuple = mRenameFile.RenameFile(xFile);

                Label_KeywordUpdateTab_FileName.Text = Path.GetDirectoryName(renameTuple.Item2) + "\n" + Path.GetFileName(renameTuple.Item2);

                System.Windows.Forms.Application.DoEvents(); // Update UI
            }

            ProgressBar_KeywordUpdateTab_WriteProgress.Value = ProgressBar_KeywordUpdateTab_WriteProgress.Maximum;

            if (duplicates == 1)
                Label_FileSelection_Statistics_Task.Text = (mFileList.Count).ToString() + " Images Renamed\n" + duplicates.ToString() + " Duplicate";
            else
                Label_FileSelection_Statistics_Task.Text = (mFileList.Count).ToString() + " Images Renamed\n" + duplicates.ToString() + " Duplicates";

            mDirectoryProperties.DirectoryStatistics.Clear();
            mFileList.Clear();

            ProgressBar_FileSelection_ReadProgress.Value = 0;
        }

        private void RadioButton_WeightIndex_CheckedChanged(object sender, EventArgs e)
        {
            if (RadioButton_FileSelection_SequenceNumbering_WeightIndex.Checked)
            {
                mRenameFile.RenameOrder = eOrder.WEIGHTINDEX;
            }
        }

        private void RadioButton_Index_CheckedChanged(object sender, EventArgs e)
        {
            if (RadioButton_FileSelection_SequenceNumbering_IndexOnly.Checked)
            {
                mRenameFile.RenameOrder = eOrder.INDEX;
            }
        }

        private void RadioButton_Weight_CheckedChanged(object sender, EventArgs e)
        {
            if (RadioButton_FileSelection_SequenceNumbering_WeightOnly.Checked)
            {
                mRenameFile.RenameOrder = eOrder.WEIGHT;
            }
        }

        private void RadioButton_IndexWeight_CheckedChanged(object sender, EventArgs e)
        {
            if (RadioButton_FileSelection_SequenceNumbering_IndexWeight.Checked)
            {
                mRenameFile.RenameOrder = eOrder.INDEXWEIGHT;
            }
        }

        private void CheckBox_Master_CheckedChanged(object sender, EventArgs e)
        {
            string rejection = string.Empty;
            string comment = string.Empty;

            Files.DirectoryOperations.Recurse = CheckBox_FileSelection_DirectorySelection_Recurse.Checked;

            TextBox_FileSelection_DirectorySelection_TotalFrames.Enabled = CheckBox_FileSelection_DirectorySelection_Master.Checked;
            ComboBox_FileSelection_DirectorySelection_RejectionAlgorithm.Enabled = CheckBox_FileSelection_DirectorySelection_Master.Checked;

            if (CheckBox_FileSelection_DirectorySelection_Master.Checked)
            {
                foreach (XisfFile file in mFileList)
                {
                    file.KeywordList.SetMasterFrameKeywords();
                }
            }
        }

        private void Button_KeywordSubFrameWeight_Remove_Click(object sender, EventArgs e)
        {
            List<string> WeightKeywords = new List<string>();

            bool bStatus;
            string frames = TextBox_FileSelection_DirectorySelection_TotalFrames.Text;
            string algo = ComboBox_FileSelection_DirectorySelection_RejectionAlgorithm.Text;

            int mTotalFrames = 0;
            bStatus = int.TryParse(frames, out mTotalFrames);



            ComboBox_KeywordUpdateTab_SubFrameKeywords_Weights_WeightKeywords.Items.Clear();

            // Repopulate the list of any present weight keywords (not values). Find unique Keyords, sort and populate Weight combobox
            foreach (XisfFile xFile in mFileList)
            {
                WeightKeywords.Add(xFile.WeightKeyword.ToString());
            }

            if (WeightKeywords.Count > 0)
            {
                WeightKeywords = WeightKeywords.Distinct().ToList();
                WeightKeywords = WeightKeywords.OrderBy(q => q).ToList();

                foreach (string item in WeightKeywords)
                {
                    ComboBox_KeywordUpdateTab_SubFrameKeywords_Weights_WeightKeywords.Items.Add(item).ToString();
                }

                if (WeightKeywords.Count > 1)
                {
                    Label_KeywordUpdateTab_SubFrameKeywords_Weights_WeightKeywords.ForeColor = Color.Red;
                }
                else
                {
                    Label_KeywordUpdateTab_SubFrameKeywords_Weights_WeightKeywords.ForeColor = Color.Black;
                }
            }
            else
            {
                ComboBox_KeywordUpdateTab_SubFrameKeywords_Weights_WeightKeywords.Items.Clear();
                Label_KeywordUpdateTab_SubFrameKeywords_Weights_WeightKeywords.ForeColor = Color.Black;
                return;
            }

            // Remove ALL WEIGHT items
            if (RadioButton_KeywordUpdateTab_SubFrameKeywords_Weights_All.Checked)
            {
                foreach (string item in ComboBox_KeywordUpdateTab_SubFrameKeywords_Weights_WeightKeywords.Items)
                {
                    foreach (XisfFile file in mFileList)
                    {
                        file.RemoveKeyword(item);
                    }
                }

                Label_KeywordUpdateTab_SubFrameKeywords_Weights_WeightKeywords.ForeColor = Color.Black;
                ComboBox_KeywordUpdateTab_SubFrameKeywords_Weights_WeightKeywords.Items.Clear();
                ComboBox_KeywordUpdateTab_SubFrameKeywords_Weights_WeightKeywords.Text = "";
                return;
            }

            // Only Remove selected item
            if (RadioButton_KeywordUpdateTab_SubFrameKeywords_Weights_Selected.Checked)
            {
                foreach (XisfFile file in mFileList)
                {
                    file.RemoveKeyword(ComboBox_KeywordUpdateTab_SubFrameKeywords_Weights_WeightKeywords.Text);
                }

                WeightKeywords.Remove(ComboBox_KeywordUpdateTab_SubFrameKeywords_Weights_WeightKeywords.Text);
                ComboBox_KeywordUpdateTab_SubFrameKeywords_Weights_WeightKeywords.Items.Remove(ComboBox_KeywordUpdateTab_SubFrameKeywords_Weights_WeightKeywords.Text);
                ComboBox_KeywordUpdateTab_SubFrameKeywords_Weights_WeightKeywords.Text = "";

                if (WeightKeywords.Count > 1)
                    Label_KeywordUpdateTab_SubFrameKeywords_Weights_WeightKeywords.ForeColor = Color.Red;
                else
                    Label_KeywordUpdateTab_SubFrameKeywords_Weights_WeightKeywords.ForeColor = Color.Black;

                if (WeightKeywords.Count > 0)
                    ComboBox_KeywordUpdateTab_SubFrameKeywords_Weights_WeightKeywords.SelectedIndex = 0;
            }
        }

        private void Button_SubFrameKeywords_CalibrationFiles_ClearAll_Click(object sender, EventArgs e)
        {
            foreach (XisfFile file in mFileList)
            {
                file.CDARK = string.Empty;
                file.CFLAT = string.Empty;
                file.CBIAS = string.Empty;
                file.CPANEL = string.Empty;
                file.CSTARS = string.Empty;
                file.RemoveKeyword("CLIGHT");
            }
        }
    }
}
