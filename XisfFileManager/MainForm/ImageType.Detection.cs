using System;
using System.Drawing;
using XisfFileManager.Globals;
using XisfFileManager.Files;
using XisfFileManager.Helpers;
using System.IO;

namespace XisfFileManager
{
    public partial class MainForm
    {
        public void ClearFilterFrameTypeGroup()
        {
            UIHelpers.ResetRadioButtons(
                RadioButton_KeywordsTab_ImageType_Filter_Luma,
                RadioButton_KeywordsTab_ImageType_Filter_Red,
                RadioButton_KeywordsTab_ImageType_Filter_Green,
                RadioButton_KeywordsTab_ImageType_Filter_Blue,
                RadioButton_KeywordsTab_ImageType_Filter_Ha,
                RadioButton_KeywordsTab_ImageType_Filter_O3,
                RadioButton_KeywordsTab_ImageType_Filter_S2,
                RadioButton_KeywordsTab_ImageType_Filter_Shutter);

            UIHelpers.ResetControlColors(
                Button_KeywordsTab_ImageType_Frame_SetMaster,
                Button_KeywordsTab_ImageType_SetAll,
                Button_KeywordsTab_ImageType_SetByFile);

            UIHelpers.ResetRadioButtons(
                RadioButton_KeywordsTab_ImageType_Frame_Light,
                RadioButton_KeywordsTab_ImageType_Frame_Dark,
                RadioButton_KeywordsTab_ImageType_Frame_Flat,
                RadioButton_KeywordsTab_ImageType_Frame_Bias);
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

                if (filter.StartsWith('L'))
                {
                    foundLuma = true;
                    lumaCount++;
                    filterCount++;
                }

                if (filter.StartsWith('R'))
                {
                    foundRed = true;
                    redCount++;
                    filterCount++;
                }

                if (filter.StartsWith('G'))
                {
                    foundGreen = true;
                    greenCount++;
                    filterCount++;
                }

                if (filter.StartsWith('B'))
                {
                    foundBlue = true;
                    blueCount++;
                    filterCount++;
                }

                if (filter.StartsWith('H'))
                {
                    foundHa = true;
                    haCount++;
                    filterCount++;
                }

                if (filter.StartsWith('O'))
                {
                    foundO3 = true;
                    o3Count++;
                    filterCount++;
                }

                if (filter.StartsWith('S') && !filter.StartsWith("Sh"))
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
                        RadioButton_KeywordsTab_ImageType_Filter_Luma.ForeColor = Color.DarkGreen;
                    else
                        RadioButton_KeywordsTab_ImageType_Filter_Luma.Checked = true;
                }
                if (foundRed)
                {
                    if (redCount != filterCount)
                        RadioButton_KeywordsTab_ImageType_Filter_Red.ForeColor = Color.DarkGreen;
                    else
                        RadioButton_KeywordsTab_ImageType_Filter_Red.Checked = true;
                }
                if (foundGreen)
                {
                    if (greenCount != filterCount)
                        RadioButton_KeywordsTab_ImageType_Filter_Green.ForeColor = Color.DarkGreen;
                    else
                        RadioButton_KeywordsTab_ImageType_Filter_Green.Checked = true;
                }
                if (foundBlue)
                {
                    if (blueCount != filterCount)
                        RadioButton_KeywordsTab_ImageType_Filter_Blue.ForeColor = Color.DarkGreen;
                    else
                        RadioButton_KeywordsTab_ImageType_Filter_Blue.Checked = true;
                }
                if (foundHa)
                {
                    if (haCount != filterCount)
                        RadioButton_KeywordsTab_ImageType_Filter_Ha.ForeColor = Color.DarkGreen;
                    else
                        RadioButton_KeywordsTab_ImageType_Filter_Ha.Checked = true;
                }
                if (foundO3)
                {
                    if (o3Count != filterCount)
                        RadioButton_KeywordsTab_ImageType_Filter_O3.ForeColor = Color.DarkGreen;
                    else
                        RadioButton_KeywordsTab_ImageType_Filter_O3.Checked = true;
                }
                if (foundS2)
                {
                    if (s2Count != filterCount)
                        RadioButton_KeywordsTab_ImageType_Filter_S2.ForeColor = Color.DarkGreen;
                    else
                        RadioButton_KeywordsTab_ImageType_Filter_S2.Checked = true;
                }
                if (foundShutter)
                {
                    if (shutterCount != filterCount)
                        RadioButton_KeywordsTab_ImageType_Filter_Shutter.ForeColor = Color.DarkGreen;
                    else
                        RadioButton_KeywordsTab_ImageType_Filter_Shutter.Checked = true;
                }
            }
            else
            {
                // Some source files are missing filters

                if (foundLuma)
                {
                    if (foundRed || foundGreen || foundBlue || foundHa || foundO3 || foundS2 || foundShutter)
                    {
                        RadioButton_KeywordsTab_ImageType_Filter_Luma.ForeColor = Color.Red;
                        RadioButton_KeywordsTab_ImageType_Filter_Luma.Checked = false;
                    }
                    else
                    {
                        RadioButton_KeywordsTab_ImageType_Filter_Luma.Checked = true;
                    }
                }

                if (foundRed)
                {
                    if (foundLuma || foundGreen || foundBlue || foundHa || foundO3 || foundS2 || foundShutter)
                    {
                        RadioButton_KeywordsTab_ImageType_Filter_Red.ForeColor = Color.Red;
                        RadioButton_KeywordsTab_ImageType_Filter_Red.Checked = false;
                    }
                    else
                    {
                        RadioButton_KeywordsTab_ImageType_Filter_Red.Checked = true;
                    }
                }

                if (foundGreen)
                {
                    if (foundLuma || foundRed || foundBlue || foundHa || foundO3 || foundS2 || foundShutter)
                    {
                        RadioButton_KeywordsTab_ImageType_Filter_Green.ForeColor = Color.Red;
                        RadioButton_KeywordsTab_ImageType_Filter_Green.Checked = false;
                    }
                    else
                    {
                        RadioButton_KeywordsTab_ImageType_Filter_Green.Checked = true;
                    }
                }

                if (foundBlue)
                {
                    if (foundLuma || foundRed || foundGreen || foundHa || foundO3 || foundS2 || foundShutter)
                    {
                        RadioButton_KeywordsTab_ImageType_Filter_Blue.ForeColor = Color.Red;
                        RadioButton_KeywordsTab_ImageType_Filter_Blue.Checked = false;
                    }
                    else
                    {
                        RadioButton_KeywordsTab_ImageType_Filter_Blue.Checked = true;
                    }
                }

                if (foundHa)
                {
                    if (foundLuma || foundRed || foundGreen || foundBlue || foundO3 || foundS2 || foundShutter)
                    {
                        RadioButton_KeywordsTab_ImageType_Filter_Ha.ForeColor = Color.Red;
                        RadioButton_KeywordsTab_ImageType_Filter_Ha.Checked = false;
                    }
                    else
                    {
                        RadioButton_KeywordsTab_ImageType_Filter_Ha.Checked = true;
                    }
                }

                if (foundO3)
                {
                    if (foundLuma || foundRed || foundGreen || foundBlue || foundHa || foundS2 || foundShutter)
                    {
                        RadioButton_KeywordsTab_ImageType_Filter_O3.ForeColor = Color.Red;
                        RadioButton_KeywordsTab_ImageType_Filter_O3.Checked = false;
                    }
                    else
                    {
                        RadioButton_KeywordsTab_ImageType_Filter_O3.Checked = true;
                    }
                }

                if (foundS2)
                {
                    if (foundLuma || foundRed || foundGreen || foundBlue || foundHa || foundO3 || foundShutter)
                    {
                        RadioButton_KeywordsTab_ImageType_Filter_S2.ForeColor = Color.Red;
                        RadioButton_KeywordsTab_ImageType_Filter_S2.Checked = false;
                    }
                    else
                    {
                        RadioButton_KeywordsTab_ImageType_Filter_S2.Checked = true;
                    }
                }

                if (foundShutter)
                {
                    if (foundLuma || foundRed || foundGreen || foundBlue || foundHa || foundO3 || foundS2)
                    {
                        RadioButton_KeywordsTab_ImageType_Filter_Shutter.ForeColor = Color.Red;
                        RadioButton_KeywordsTab_ImageType_Filter_Shutter.Checked = false;
                    }
                    else
                    {
                        RadioButton_KeywordsTab_ImageType_Filter_Shutter.Checked = true;
                    }
                }
            }


            // Now check each and every source file for a valid frame type

            RadioButton_KeywordsTab_ImageType_Frame_Light.ForeColor = Color.Black;
            RadioButton_KeywordsTab_ImageType_Frame_Dark.ForeColor = Color.Black;
            RadioButton_KeywordsTab_ImageType_Frame_Flat.ForeColor = Color.Black;
            RadioButton_KeywordsTab_ImageType_Frame_Bias.ForeColor = Color.Black;

            RadioButton_KeywordsTab_ImageType_Frame_Light.Checked = false;
            RadioButton_KeywordsTab_ImageType_Frame_Dark.Checked = false;
            RadioButton_KeywordsTab_ImageType_Frame_Flat.Checked = false;
            RadioButton_KeywordsTab_ImageType_Frame_Bias.Checked = false;

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
                        RadioButton_KeywordsTab_ImageType_Frame_Light.ForeColor = Color.DarkGreen;
                    else
                        RadioButton_KeywordsTab_ImageType_Frame_Light.Checked = true;
                }
                if (foundDark)
                {
                    if (darkCount != frameTypeCount)
                        RadioButton_KeywordsTab_ImageType_Frame_Dark.ForeColor = Color.DarkGreen;
                    else
                        RadioButton_KeywordsTab_ImageType_Frame_Dark.Checked = true;
                }
                if (foundFlat)
                {
                    if (flatCount != frameTypeCount)
                        RadioButton_KeywordsTab_ImageType_Frame_Flat.ForeColor = Color.DarkGreen;
                    else
                        RadioButton_KeywordsTab_ImageType_Frame_Flat.Checked = true;
                }
                if (foundBias)
                {
                    if (biasCount != frameTypeCount)
                        RadioButton_KeywordsTab_ImageType_Frame_Bias.ForeColor = Color.DarkGreen;
                    else
                        RadioButton_KeywordsTab_ImageType_Frame_Bias.Checked = true;
                }
            }
            else
            {
                if (foundLight)
                {
                    if (foundDark || foundFlat || foundBias)
                    {
                        RadioButton_KeywordsTab_ImageType_Frame_Light.ForeColor = Color.Red;
                        RadioButton_KeywordsTab_ImageType_Frame_Light.Checked = false;
                    }
                    else
                    {
                        RadioButton_KeywordsTab_ImageType_Frame_Light.Checked = true;
                    }
                }

                if (foundDark)
                {
                    if (foundLight || foundFlat || foundBias)
                    {
                        RadioButton_KeywordsTab_ImageType_Frame_Dark.ForeColor = Color.Red;
                        RadioButton_KeywordsTab_ImageType_Frame_Dark.Checked = false;
                    }
                    else
                    {
                        RadioButton_KeywordsTab_ImageType_Frame_Dark.Checked = true;
                    }
                }

                if (foundFlat)
                {
                    if (foundLight || foundDark || foundBias)
                    {
                        RadioButton_KeywordsTab_ImageType_Frame_Flat.ForeColor = Color.Red;
                        RadioButton_KeywordsTab_ImageType_Frame_Flat.Checked = false;
                    }
                    else
                    {
                        RadioButton_KeywordsTab_ImageType_Frame_Flat.Checked = true;
                    }
                }

                if (foundBias)
                {
                    if (foundLight || foundDark || foundFlat)
                    {
                        RadioButton_KeywordsTab_ImageType_Frame_Bias.ForeColor = Color.Red;
                        RadioButton_KeywordsTab_ImageType_Frame_Bias.Checked = false;
                    }
                    else
                    {
                        RadioButton_KeywordsTab_ImageType_Frame_Bias.Checked = true;
                    }
                }

                if (!foundLight && !foundDark && !foundFlat && !foundBias)
                {
                    RadioButton_KeywordsTab_ImageType_Frame_Light.ForeColor = Color.DarkViolet;
                    RadioButton_KeywordsTab_ImageType_Frame_Dark.ForeColor = Color.DarkViolet;
                    RadioButton_KeywordsTab_ImageType_Frame_Flat.ForeColor = Color.DarkViolet;
                    RadioButton_KeywordsTab_ImageType_Frame_Bias.ForeColor = Color.DarkViolet;

                    return;
                }
            }
            if (foundMaster)
            {
                if ((masterCount != mFileList.Count) && (masterCount > 0))
                {
                    CheckBox_FileSelection_DirectorySelection_Masters_Enable.ForeColor = Color.Red;
                    Button_KeywordsTab_ImageType_Frame_SetMaster.ForeColor = Color.Red;
                }
                else
                {
                    CheckBox_FileSelection_DirectorySelection_Masters_Enable.Checked = true;
                    CheckBox_FileSlection_DirectorySelection_NoStatistics.Checked = true;
                }
            }


            // *****************************************************************************


            if ((foundLight || foundDark || foundFlat || foundBias) & (foundLuma || foundRed || foundGreen || foundBlue || foundHa || foundO3 || foundS2 || foundShutter))
            {
                // Set "SetAll" to black if only a single filter and a single frame type was found
                Button_KeywordsTab_ImageType_SetAll.ForeColor = Color.Black;
            }
            else
            {
                // More that one software program - set "SetByFile" to red
                Button_KeywordsTab_ImageType_SetAll.ForeColor = Color.Red;
            }

            if ((masterCount != mFileList.Count) && (masterCount != 0))
            {
                CheckBox_FileSelection_DirectorySelection_Masters_Enable.ForeColor = Color.Red;
                Button_KeywordsTab_ImageType_SetByFile.ForeColor = Color.Red;
            }

            if ((filterCount != mFileList.Count) || (frameTypeCount != mFileList.Count))
            {
                // The number of source files didn't equal the number of files with a known filter
                // Set "SetByFile" to red
                Button_KeywordsTab_ImageType_SetByFile.ForeColor = Color.Red;
            }
        }
    }
}
