using System;
using System.Drawing;
using XisfFileManager.Enums;
using XisfFileManager.Files;

namespace XisfFileManager
{
    public partial class MainForm
    {
        public void ClearFilterFrameTypeGroup()
        {
            RadioButton_KeywordUpdateTab_ImageType_Filter_Luma.ForeColor = Color.Black;
            RadioButton_KeywordUpdateTab_ImageType_Filter_Red.ForeColor = Color.Black;
            RadioButton_KeywordUpdateTab_ImageType_Filter_Green.ForeColor = Color.Black;
            RadioButton_KeywordUpdateTab_ImageType_Filter_Blue.ForeColor = Color.Black;
            RadioButton_KeywordUpdateTab_ImageType_Filter_Ha.ForeColor = Color.Black;
            RadioButton_KeywordUpdateTab_ImageType_Filter_O3.ForeColor = Color.Black;
            RadioButton_KeywordUpdateTab_ImageType_Filter_S2.ForeColor = Color.Black;
            RadioButton_KeywordUpdateTab_ImageType_Filter_Shutter.ForeColor = Color.Black;

            RadioButton_KeywordUpdateTab_ImageType_Filter_Luma.Checked = false;
            RadioButton_KeywordUpdateTab_ImageType_Filter_Red.Checked = false;
            RadioButton_KeywordUpdateTab_ImageType_Filter_Green.Checked = false;
            RadioButton_KeywordUpdateTab_ImageType_Filter_Blue.Checked = false;
            RadioButton_KeywordUpdateTab_ImageType_Filter_Ha.Checked = false;
            RadioButton_KeywordUpdateTab_ImageType_Filter_O3.Checked = false;
            RadioButton_KeywordUpdateTab_ImageType_Filter_S2.Checked = false;
            RadioButton_KeywordUpdateTab_ImageType_Filter_Shutter.Checked = false;

            CheckBox_FileSelection_DirectorySelection_Master.ForeColor = Color.Black;
            CheckBox_FileSelection_DirectorySelection_Master.Checked = false;

            Button_KeywordUpdateTab_ImageType_Frame_SetMaster.ForeColor = Color.Black;
            Button_KeywordUpdateTab_ImageType_SetAll.ForeColor = Color.Black;
            Button_KeywordUpdateTab_ImageType_SetByFile.ForeColor = Color.Black;

            RadioButton_KeywordUpdateTab_ImageType_Frame_Light.ForeColor = Color.Black;
            RadioButton_KeywordUpdateTab_ImageType_Frame_Dark.ForeColor = Color.Black;
            RadioButton_KeywordUpdateTab_ImageType_Frame_Flat.ForeColor = Color.Black;
            RadioButton_KeywordUpdateTab_ImageType_Frame_Bias.ForeColor = Color.Black;

            RadioButton_KeywordUpdateTab_ImageType_Frame_Light.Checked = false;
            RadioButton_KeywordUpdateTab_ImageType_Frame_Dark.Checked = false;
            RadioButton_KeywordUpdateTab_ImageType_Frame_Flat.Checked = false;
            RadioButton_KeywordUpdateTab_ImageType_Frame_Bias.Checked = false;
        }

        public void FindFilterFrameType()
        {
            string filter;
            int filterCount;
            int masterCount;
            int lumaCount;
            int redCount;
            int greenCount;
            int blueCount;
            int haCount;
            int o3Count;
            int s2Count;
            int shutterCount;

            bool foundLuma = false;
            bool foundRed = false;
            bool foundGreen = false;
            bool foundBlue = false;
            bool foundHa = false;
            bool foundO3 = false;
            bool foundS2 = false;
            bool foundShutter = false;
            bool foundMaster = false;

            ClearFilterFrameTypeGroup();

            // *****************************************************************************

            filterCount = 0;
            lumaCount = 0;
            redCount = 0;
            greenCount = 0;
            blueCount = 0;
            haCount = 0;
            o3Count = 0;
            s2Count = 0;
            shutterCount = 0;

            foreach (XisfFile file in mFileList)
            {
                filter = file.FilterName.Trim();

                file.FilterName = filter;

                if (filter == "Luma")
                {
                    foundLuma = true;
                    lumaCount++;
                    filterCount++;
                }

                if (filter == "Red")
                {
                    foundRed = true;
                    redCount++;
                    filterCount++;
                }

                if (filter == "Green")
                {
                    foundGreen = true;
                    greenCount++;
                    filterCount++;
                }

                if (filter == "Blue")
                {
                    foundBlue = true;
                    blueCount++;
                    filterCount++;
                }

                if (filter == "Ha")
                {
                    foundHa = true;
                    haCount++;
                    filterCount++;
                }

                if (filter == "O3")
                {
                    foundO3 = true;
                    o3Count++;
                    filterCount++;
                }

                if (filter == "S2")
                {
                    foundS2 = true;
                    s2Count++;
                    filterCount++;
                }

                if (filter == "Shutter")
                {
                    foundShutter = true;
                    shutterCount++;
                    filterCount++;
                }
            }

            if (filterCount == mFileList.Count)
            {
                // Every source file has a filter.

                // If one filter is used, check that filter's radio button and leave the radio button as black
                // if more than one filter is used, make a found filter's radio button unchecked and color DarkGreen
                // Do this for each filter

                if (foundLuma)
                {
                    if (lumaCount != filterCount)
                        RadioButton_KeywordUpdateTab_ImageType_Filter_Luma.ForeColor = Color.DarkGreen;
                    else
                        RadioButton_KeywordUpdateTab_ImageType_Filter_Luma.Checked = true;
                }
                if (foundRed)
                {
                    if (redCount != filterCount)
                        RadioButton_KeywordUpdateTab_ImageType_Filter_Red.ForeColor = Color.DarkGreen;
                    else
                        RadioButton_KeywordUpdateTab_ImageType_Filter_Red.Checked = true;
                }
                if (foundGreen)
                {
                    if (greenCount != filterCount)
                        RadioButton_KeywordUpdateTab_ImageType_Filter_Green.ForeColor = Color.DarkGreen;
                    else
                        RadioButton_KeywordUpdateTab_ImageType_Filter_Green.Checked = true;
                }
                if (foundBlue)
                {
                    if (blueCount != filterCount)
                        RadioButton_KeywordUpdateTab_ImageType_Filter_Blue.ForeColor = Color.DarkGreen;
                    else
                        RadioButton_KeywordUpdateTab_ImageType_Filter_Blue.Checked = true;
                }
                if (foundHa)
                {
                    if (haCount != filterCount)
                        RadioButton_KeywordUpdateTab_ImageType_Filter_Ha.ForeColor = Color.DarkGreen;
                    else
                        RadioButton_KeywordUpdateTab_ImageType_Filter_Ha.Checked = true;
                }
                if (foundO3)
                {
                    if (o3Count != filterCount)
                        RadioButton_KeywordUpdateTab_ImageType_Filter_O3.ForeColor = Color.DarkGreen;
                    else
                        RadioButton_KeywordUpdateTab_ImageType_Filter_O3.Checked = true;
                }
                if (foundS2)
                {
                    if (s2Count != filterCount)
                        RadioButton_KeywordUpdateTab_ImageType_Filter_S2.ForeColor = Color.DarkGreen;
                    else
                        RadioButton_KeywordUpdateTab_ImageType_Filter_S2.Checked = true;
                }
                if (foundShutter)
                {
                    if (shutterCount != filterCount)
                        RadioButton_KeywordUpdateTab_ImageType_Filter_Shutter.ForeColor = Color.DarkGreen;
                    else
                        RadioButton_KeywordUpdateTab_ImageType_Filter_Shutter.Checked = true;
                }
            }
            else
            {
                // Some source files are missing filters

                if (foundLuma)
                {
                    if (foundRed || foundGreen || foundBlue || foundHa || foundO3 || foundS2 || foundShutter)
                    {
                        RadioButton_KeywordUpdateTab_ImageType_Filter_Luma.ForeColor = Color.Red;
                        RadioButton_KeywordUpdateTab_ImageType_Filter_Luma.Checked = false;
                    }
                    else
                    {
                        RadioButton_KeywordUpdateTab_ImageType_Filter_Luma.Checked = true;
                    }
                }

                if (foundRed)
                {
                    if (foundLuma || foundGreen || foundBlue || foundHa || foundO3 || foundS2 || foundShutter)
                    {
                        RadioButton_KeywordUpdateTab_ImageType_Filter_Red.ForeColor = Color.Red;
                        RadioButton_KeywordUpdateTab_ImageType_Filter_Red.Checked = false;
                    }
                    else
                    {
                        RadioButton_KeywordUpdateTab_ImageType_Filter_Red.Checked = true;
                    }
                }

                if (foundGreen)
                {
                    if (foundLuma || foundRed || foundBlue || foundHa || foundO3 || foundS2 || foundShutter)
                    {
                        RadioButton_KeywordUpdateTab_ImageType_Filter_Green.ForeColor = Color.Red;
                        RadioButton_KeywordUpdateTab_ImageType_Filter_Green.Checked = false;
                    }
                    else
                    {
                        RadioButton_KeywordUpdateTab_ImageType_Filter_Green.Checked = true;
                    }
                }

                if (foundBlue)
                {
                    if (foundLuma || foundRed || foundGreen || foundHa || foundO3 || foundS2 || foundShutter)
                    {
                        RadioButton_KeywordUpdateTab_ImageType_Filter_Blue.ForeColor = Color.Red;
                        RadioButton_KeywordUpdateTab_ImageType_Filter_Blue.Checked = false;
                    }
                    else
                    {
                        RadioButton_KeywordUpdateTab_ImageType_Filter_Blue.Checked = true;
                    }
                }

                if (foundHa)
                {
                    if (foundLuma || foundRed || foundGreen || foundBlue || foundO3 || foundS2 || foundShutter)
                    {
                        RadioButton_KeywordUpdateTab_ImageType_Filter_Ha.ForeColor = Color.Red;
                        RadioButton_KeywordUpdateTab_ImageType_Filter_Ha.Checked = false;
                    }
                    else
                    {
                        RadioButton_KeywordUpdateTab_ImageType_Filter_Ha.Checked = true;
                    }
                }

                if (foundO3)
                {
                    if (foundLuma || foundRed || foundGreen || foundBlue || foundHa || foundS2 || foundShutter)
                    {
                        RadioButton_KeywordUpdateTab_ImageType_Filter_O3.ForeColor = Color.Red;
                        RadioButton_KeywordUpdateTab_ImageType_Filter_O3.Checked = false;
                    }
                    else
                    {
                        RadioButton_KeywordUpdateTab_ImageType_Filter_O3.Checked = true;
                    }
                }

                if (foundS2)
                {
                    if (foundLuma || foundRed || foundGreen || foundBlue || foundHa || foundO3 || foundShutter)
                    {
                        RadioButton_KeywordUpdateTab_ImageType_Filter_S2.ForeColor = Color.Red;
                        RadioButton_KeywordUpdateTab_ImageType_Filter_S2.Checked = false;
                    }
                    else
                    {
                        RadioButton_KeywordUpdateTab_ImageType_Filter_S2.Checked = true;
                    }
                }

                if (foundShutter)
                {
                    if (foundLuma || foundRed || foundGreen || foundBlue || foundHa || foundO3 || foundS2)
                    {
                        RadioButton_KeywordUpdateTab_ImageType_Filter_Shutter.ForeColor = Color.Red;
                        RadioButton_KeywordUpdateTab_ImageType_Filter_Shutter.Checked = false;
                    }
                    else
                    {
                        RadioButton_KeywordUpdateTab_ImageType_Filter_Shutter.Checked = true;
                    }
                }
            }


            // Now check each and every source file for a valid frame type

            RadioButton_KeywordUpdateTab_ImageType_Frame_Light.ForeColor = Color.Black;
            RadioButton_KeywordUpdateTab_ImageType_Frame_Dark.ForeColor = Color.Black;
            RadioButton_KeywordUpdateTab_ImageType_Frame_Flat.ForeColor = Color.Black;
            RadioButton_KeywordUpdateTab_ImageType_Frame_Bias.ForeColor = Color.Black;

            RadioButton_KeywordUpdateTab_ImageType_Frame_Light.Checked = false;
            RadioButton_KeywordUpdateTab_ImageType_Frame_Dark.Checked = false;
            RadioButton_KeywordUpdateTab_ImageType_Frame_Flat.Checked = false;
            RadioButton_KeywordUpdateTab_ImageType_Frame_Bias.Checked = false;

            bool foundLight = false;
            bool foundDark = false;
            bool foundFlat = false;
            bool foundBias = false;
            int lightCount = 0;
            int darkCount = 0;
            int flatCount = 0;
            int biasCount = 0;
            int frameTypeCount;

            masterCount = 0;
            frameTypeCount = 0;
            foreach (XisfFile file in mFileList)
            {
                if (file.FrameType == eFrame.LIGHT)
                {
                    foundLight = true;
                    lightCount++;
                    frameTypeCount++;
                }

                if (file.FrameType == eFrame.DARK)
                {
                    foundDark = true;
                    darkCount++;
                    frameTypeCount++;
                }

                if (file.FrameType == eFrame.FLAT)
                {
                    foundFlat = true;
                    flatCount++;
                    frameTypeCount++;
                }

                if (file.FrameType == eFrame.BIAS)
                {
                    foundBias = true;
                    biasCount++;
                    frameTypeCount++;
                }

                if (file.TargetName.Equals("Master"))
                {
                    foundMaster = true;
                    masterCount++;
                }
            }

            if (frameTypeCount == mFileList.Count)
            {
                // Every source file has a frameType.

                // If one filter is used, check that filter's radio button and leave the radio button as black
                // if more than one filter is used, make a found filter's radio button unchecked and color DarkGreen
                // Do this for each filter

                if (foundLight)
                {
                    if (lightCount != frameTypeCount)
                        RadioButton_KeywordUpdateTab_ImageType_Frame_Light.ForeColor = Color.DarkGreen;
                    else
                        RadioButton_KeywordUpdateTab_ImageType_Frame_Light.Checked = true;
                }
                if (foundDark)
                {
                    if (darkCount != frameTypeCount)
                        RadioButton_KeywordUpdateTab_ImageType_Frame_Dark.ForeColor = Color.DarkGreen;
                    else
                        RadioButton_KeywordUpdateTab_ImageType_Frame_Dark.Checked = true;
                }
                if (foundFlat)
                {
                    if (flatCount != frameTypeCount)
                        RadioButton_KeywordUpdateTab_ImageType_Frame_Flat.ForeColor = Color.DarkGreen;
                    else
                        RadioButton_KeywordUpdateTab_ImageType_Frame_Flat.Checked = true;
                }
                if (foundBias)
                {
                    if (biasCount != frameTypeCount)
                        RadioButton_KeywordUpdateTab_ImageType_Frame_Bias.ForeColor = Color.DarkGreen;
                    else
                        RadioButton_KeywordUpdateTab_ImageType_Frame_Bias.Checked = true;
                }
            }
            else
            {
                if (foundLight)
                {
                    if (foundDark || foundFlat || foundBias)
                    {
                        RadioButton_KeywordUpdateTab_ImageType_Frame_Light.ForeColor = Color.Red;
                        RadioButton_KeywordUpdateTab_ImageType_Frame_Light.Checked = false;
                    }
                    else
                    {
                        RadioButton_KeywordUpdateTab_ImageType_Frame_Light.Checked = true;
                    }
                }

                if (foundDark)
                {
                    if (foundLight || foundFlat || foundBias)
                    {
                        RadioButton_KeywordUpdateTab_ImageType_Frame_Dark.ForeColor = Color.Red;
                        RadioButton_KeywordUpdateTab_ImageType_Frame_Dark.Checked = false;
                    }
                    else
                    {
                        RadioButton_KeywordUpdateTab_ImageType_Frame_Dark.Checked = true;
                    }
                }

                if (foundFlat)
                {
                    if (foundLight || foundDark || foundBias)
                    {
                        RadioButton_KeywordUpdateTab_ImageType_Frame_Flat.ForeColor = Color.Red;
                        RadioButton_KeywordUpdateTab_ImageType_Frame_Flat.Checked = false;
                    }
                    else
                    {
                        RadioButton_KeywordUpdateTab_ImageType_Frame_Flat.Checked = true;
                    }
                }

                if (foundBias)
                {
                    if (foundLight || foundDark || foundFlat)
                    {
                        RadioButton_KeywordUpdateTab_ImageType_Frame_Bias.ForeColor = Color.Red;
                        RadioButton_KeywordUpdateTab_ImageType_Frame_Bias.Checked = false;
                    }
                    else
                    {
                        RadioButton_KeywordUpdateTab_ImageType_Frame_Bias.Checked = true;
                    }
                }

                if (!foundLight && !foundDark && !foundFlat && !foundBias)
                {
                    RadioButton_KeywordUpdateTab_ImageType_Frame_Light.ForeColor = Color.DarkViolet;
                    RadioButton_KeywordUpdateTab_ImageType_Frame_Dark.ForeColor = Color.DarkViolet;
                    RadioButton_KeywordUpdateTab_ImageType_Frame_Flat.ForeColor = Color.DarkViolet;
                    RadioButton_KeywordUpdateTab_ImageType_Frame_Bias.ForeColor = Color.DarkViolet;

                    return;
                }
            }
            if (foundMaster)
            {
                if ((masterCount != mFileList.Count) && (masterCount > 0))
                {
                    CheckBox_FileSelection_DirectorySelection_Master.ForeColor = Color.Red;
                    Button_KeywordUpdateTab_ImageType_Frame_SetMaster.ForeColor = Color.Red;
                }
                else
                {
                    CheckBox_FileSelection_DirectorySelection_Master.Checked = true;
                    CheckBox_FileSlection_NoTotals.Checked = true;
                }
            }


            // *****************************************************************************


            if ((foundLight || foundDark || foundFlat || foundBias) & (foundLuma || foundRed || foundGreen || foundBlue || foundHa || foundO3 || foundS2 || foundShutter))
            {
                // Set "SetAll" to black if only a single filter and a single frame type was found
                Button_KeywordUpdateTab_ImageType_SetAll.ForeColor = Color.Black;
            }
            else
            {
                // More that one software program - set "SetByFile" to red
                Button_KeywordUpdateTab_ImageType_SetAll.ForeColor = Color.Red;
            }

            if ((masterCount != mFileList.Count) && (masterCount != 0))
            {
                CheckBox_FileSelection_DirectorySelection_Master.ForeColor = Color.Red;
                Button_KeywordUpdateTab_ImageType_SetByFile.ForeColor = Color.Red;
            }

            if ((filterCount != mFileList.Count) || (frameTypeCount != mFileList.Count))
            {
                // The number of source files didn't equal the number of files with a known filter
                // Set "SetByFile" to red
                Button_KeywordUpdateTab_ImageType_SetByFile.ForeColor = Color.Red;
            }
        }

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
                if (RadioButton_KeywordUpdateTab_ImageType_Frame_Light.Checked)
                {
                    if (CheckBox_FileSelection_DirectorySelection_Master.Checked)
                    {
                        file.AddKeyword("IMAGETYP", "Light", "Integration Master");
                    }
                    else
                    {
                        file.AddKeyword("IMAGETYP", "Light", "Sub Frame");
                    }
                }

                if (RadioButton_KeywordUpdateTab_ImageType_Frame_Dark.Checked)
                {
                    if (CheckBox_FileSelection_DirectorySelection_Master.Checked)
                    {
                        file.AddKeyword("IMAGETYP", "Dark", "Integration Master");
                    }
                    else
                    {
                        file.AddKeyword("IMAGETYP", "Dark", "Sub Frame");
                    }
                }

                if (RadioButton_KeywordUpdateTab_ImageType_Frame_Flat.Checked)
                {
                    if (CheckBox_FileSelection_DirectorySelection_Master.Checked)
                    {
                        file.AddKeyword("IMAGETYP", "Flat", "Integration Master");
                    }
                    else
                    {
                        file.AddKeyword("IMAGETYP", "Flat", "Sub Frame");
                    }
                }

                if (RadioButton_KeywordUpdateTab_ImageType_Frame_Bias.Checked)
                {
                    if (CheckBox_FileSelection_DirectorySelection_Master.Checked)
                    {
                        file.AddKeyword("IMAGETYP", "Bias", "Integration Master");
                    }
                    else
                    {
                        file.AddKeyword("IMAGETYP", "Bias", "Sub Frame");
                    }

                }

                if (RadioButton_KeywordUpdateTab_ImageType_Filter_Luma.Checked)
                    file.AddKeyword("FILTER", "Luma", "Astrodon Luma 1.25 via Starlight Xpress USB 7 Position Wheel");

                if (RadioButton_KeywordUpdateTab_ImageType_Filter_Red.Checked)
                    file.AddKeyword("FILTER", "Red", "Astrodon Red 1.25 via Starlight Xpress USB 7 Position Wheel");

                if (RadioButton_KeywordUpdateTab_ImageType_Filter_Green.Checked)
                    file.AddKeyword("FILTER", "Green", "Astrodon Green 1.25 via Starlight Xpress USB 7 Position Wheel");

                if (RadioButton_KeywordUpdateTab_ImageType_Filter_Blue.Checked)
                    file.AddKeyword("FILTER", "Blue", "Astrodon Blue 1.25 via Starlight Xpress USB 7 Position Wheel");

                if (RadioButton_KeywordUpdateTab_ImageType_Filter_Ha.Checked)
                    file.AddKeyword("FILTER", "Ha", "Astrodon Ha E-Series 1.25 via Starlight Xpress USB 7 Position Wheel");

                if (RadioButton_KeywordUpdateTab_ImageType_Filter_O3.Checked)
                    file.AddKeyword("FILTER", "O3", "Astrodon O3 E-Series 1.25 via Starlight Xpress USB 7 Position Wheel");

                if (RadioButton_KeywordUpdateTab_ImageType_Filter_S2.Checked)
                    file.AddKeyword("FILTER", "S2", "Astrodon S2 E-Series 1.25 via Starlight Xpress USB 7 Position Wheel");

                if (RadioButton_KeywordUpdateTab_ImageType_Filter_Shutter.Checked)
                    file.AddKeyword("FILTER", "Shutter", "Opaque 1.25 or placeholder via Starlight Xpress USB 7 Position Wheel");
            }

            FindFilterFrameType();
        }


        private void Button_KeywordImageTypeFrame_SetMaster_Click(object sender, EventArgs e)
        {
            ComboBox_KeywordUpdateTab_SubFrameKeywords_TargetNames.Text = "Master";
            CheckBox_KeywordUpdateTab_SubFrameKeywords_UpdateTargetName.Checked = true;
            CheckBox_FileSelection_DirectorySelection_Master.Checked = true;
            CheckBox_FileSlection_NoTotals.Checked = true;
        }

    }
}
