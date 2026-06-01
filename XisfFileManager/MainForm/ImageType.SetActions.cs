using System;
using System.IO;
using XisfFileManager.Files;
using XisfFileManager.Globals;

namespace XisfFileManager
{
    public partial class MainForm
    {
        // Image Type tab - "Set By File" and "Set All" keyword-writing actions.

        private void Button_KeywordImageTypeFrame_SetByFile_Click(object sender, EventArgs e)
        {
            bool globalFrameType = false;
            string frameTypeText = string.Empty;

            bool globalFilter = false;
            string globalFilterText = string.Empty;

            foreach (XisfFile file in mFileList)
            {
                if (globalFrameType)
                {
                    if (file.FrameType == eFrame.EMPTY)
                        file.AddKeyword("IMAGETYP", frameTypeText.ToString(), "XISF File Manager");
                }
                else
                {
                    frameTypeText = string.Empty; // file.FrameType;
                    if (frameTypeText.Contains("Global_"))
                    {
                        globalFrameType = true;
                        frameTypeText = frameTypeText.Replace("Global_", "");

                    }
                }

                file.AddKeyword("IMAGETYP", frameTypeText.ToString(), "XISF File Manager");
                if (frameTypeText.Equals("Dark") || frameTypeText.Equals("Bias"))
                {
                    file.AddKeyword("FILTER", "Shutter", "Opaque 1.25 via Starlight Xpress USB 7 Position Wheel");
                }
            }



            foreach (XisfFile file in mFileList)
            {
                if (globalFilter)
                {
                    if (file.FilterName == string.Empty)
                        file.AddKeyword("FILTER", globalFilterText.ToString(), "Astrodon 1.25 via Starlight Xpress USB 7 Position Wheel");
                }
                else
                {
                    globalFilterText = file.FilterName;
                    if (globalFilterText.Contains("Global_"))
                    {
                        globalFilter = true;
                        globalFilterText = globalFilterText.Replace("Global_", "");
                    }
                }

                file.AddKeyword("FILTER", globalFilterText.ToString(), "Astrodon 1.25 via Starlight Xpress USB 7 Position Wheel");
            }

            FindFilterFrameType();
        }

        private void Button_KeywordImageTypeFrame_SetAll_Click(object sender, EventArgs e)
        {
            foreach (XisfFile file in mFileList)
            {
                if (RadioButton_KeywordsTab_ImageType_Frame_Light.Checked)
                {
                    if (CheckBox_FileSelection_DirectorySelection_Masters_Enable.Checked)
                    {
                        file.AddKeyword("IMAGETYP", "Light", "Integration Master");
                    }
                    else
                    {
                        file.AddKeyword("IMAGETYP", "Light", "Sub Frame");
                    }
                }

                if (RadioButton_KeywordsTab_ImageType_Frame_Dark.Checked)
                {
                    if (CheckBox_FileSelection_DirectorySelection_Masters_Enable.Checked)
                    {
                        file.AddKeyword("IMAGETYP", "Dark", "Integration Master");
                    }
                    else
                    {
                        file.AddKeyword("IMAGETYP", "Dark", "Sub Frame");
                    }
                }

                if (RadioButton_KeywordsTab_ImageType_Frame_Flat.Checked)
                {
                    if (CheckBox_FileSelection_DirectorySelection_Masters_Enable.Checked)
                    {
                        file.AddKeyword("IMAGETYP", "Flat", "Integration Master");
                    }
                    else
                    {
                        file.AddKeyword("IMAGETYP", "Flat", "Sub Frame");
                    }
                }

                if (RadioButton_KeywordsTab_ImageType_Frame_Bias.Checked)
                {
                    if (CheckBox_FileSelection_DirectorySelection_Masters_Enable.Checked)
                    {
                        file.AddKeyword("IMAGETYP", "Bias", "Integration Master");
                    }
                    else
                    {
                        file.AddKeyword("IMAGETYP", "Bias", "Sub Frame");
                    }

                }

                if (CheckBox_FileSelection_DirectorySelection_FluxDensity_Enable.Checked)
                {
                    if (Path.GetFileNameWithoutExtension(file.FilePath).EndsWith("_L"))
                    {
                        file.AddKeyword("FILTER", "L", "Sony ASI533 RGB Filters");
                        file.AddKeyword("COLORSPC", "Grayscale", "Forced Mono");
                        file.RemoveKeyword("BAYERPAT");
                    }
                }
                else
                {
                    if (RadioButton_KeywordsTab_ImageType_Filter_Luma.Checked)
                        file.AddKeyword("FILTER", "L", "Astrodon Luma 1.25 via Starlight Xpress USB 7 Position Wheel");
                }

                if (CheckBox_FileSelection_DirectorySelection_FluxDensity_Enable.Checked)
                {
                    if (Path.GetFileNameWithoutExtension(file.FilePath).EndsWith("_R"))
                    {
                        file.AddKeyword("FILTER", "R", "Sony ASI533 RGB Filters");
                        file.AddKeyword("COLORSPC", "Grayscale", "Forced Mono");
                        file.RemoveKeyword("BAYERPAT");
                    }
                }
                else
                {
                    if (RadioButton_KeywordsTab_ImageType_Filter_Red.Checked)
                        file.AddKeyword("FILTER", "R", "Astrodon Red 1.25 via Starlight Xpress USB 7 Position Wheel");
                }


                if (CheckBox_FileSelection_DirectorySelection_FluxDensity_Enable.Checked)
                {
                    if (Path.GetFileNameWithoutExtension(file.FilePath).EndsWith("_G"))
                    {
                        file.AddKeyword("FILTER", "G", "Sony ASI533 RGB Filters");
                        file.AddKeyword("COLORSPC", "Grayscale", "Forced Mono");
                        file.RemoveKeyword("BAYERPAT");
                    }
                }
                else
                {
                    if (RadioButton_KeywordsTab_ImageType_Filter_Green.Checked)
                        file.AddKeyword("FILTER", "G", "Astrodon Green 1.25 via Starlight Xpress USB 7 Position Wheel");
                }


                if (CheckBox_FileSelection_DirectorySelection_FluxDensity_Enable.Checked)
                {
                    if (Path.GetFileNameWithoutExtension(file.FilePath).EndsWith("_B"))
                    {
                        file.AddKeyword("FILTER", "B", "Sony ASI533 RGB Filters");
                        file.AddKeyword("COLORSPC", "Grayscale", "Forced Mono");
                        file.RemoveKeyword("BAYERPAT");
                    }
                }
                else
                {
                    if (RadioButton_KeywordsTab_ImageType_Filter_Blue.Checked)
                        file.AddKeyword("FILTER", "B", "Astrodon Blue 1.25 via Starlight Xpress USB 7 Position Wheel");
                }

                if (RadioButton_KeywordsTab_ImageType_Filter_Ha.Checked)
                    file.AddKeyword("FILTER", "H", "Astrodon Ha E-Series 1.25 via Starlight Xpress USB 7 Position Wheel");

                if (RadioButton_KeywordsTab_ImageType_Filter_O3.Checked)
                    file.AddKeyword("FILTER", "O", "Astrodon O3 E-Series 1.25 via Starlight Xpress USB 7 Position Wheel");

                if (RadioButton_KeywordsTab_ImageType_Filter_S2.Checked)
                    file.AddKeyword("FILTER", "S", "Astrodon S2 E-Series 1.25 via Starlight Xpress USB 7 Position Wheel");

                if (RadioButton_KeywordsTab_ImageType_Filter_Shutter.Checked)
                    file.AddKeyword("FILTER", "Shutter", "Opaque 1.25 or placeholder via Starlight Xpress USB 7 Position Wheel");
            }

            FindFilterFrameType();
        }
    }
}
