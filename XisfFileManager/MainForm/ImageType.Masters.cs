using System;

namespace XisfFileManager
{
    public partial class MainForm
    {
        // Image Type tab - "Set Master" shortcut (target name + masters/no-statistics).

        private void Button_KeywordImageTypeFrame_SetMaster_Click(object sender, EventArgs e)
        {
            ComboBox_KeywordUpdateTab_SubFrameKeywords_TargetNames.Text = "Master";
            CheckBox_KeywordUpdateTab_SubFrameKeywords_UpdateTargetName.Checked = true;
            CheckBox_FileSelection_DirectorySelection_Masters_Enable.Checked = true;
            CheckBox_FileSlection_DirectorySelection_NoStatistics.Checked = true;

            if (CheckBox_FileSelection_DirectorySelection_Masters_Enable.Checked)
            {
                TextBox_FileSelection_DirectorySelection_Masters_Frames.Text = mFileList[0].MSTRFRMS.ToString();
                TextBox_FileSelection_DirectorySelection_Masters_Rejection.Text = mFileList[0].MSTRALG;
            }
            else
            {
                TextBox_FileSelection_DirectorySelection_Masters_Frames.Text = "Frames";
                TextBox_FileSelection_DirectorySelection_Masters_Rejection.Text = "Algo";
            }
        }
    }
}
