using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using XisfFileManager.Globals;
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
    }
}
