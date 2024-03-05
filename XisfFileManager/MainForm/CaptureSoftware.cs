using System;
using System.Drawing;
using XisfFileManager.Files;

namespace XisfFileManager
{
    public partial class MainForm
    {
        public void ClearCaptureSoftwareGroup()
        {
            RadioButton_KeywordUpdateTab_CaptureSoftware_NINA.Checked = false;
            RadioButton_KeywordUpdateTab_CaptureSoftware_SharpCap.Checked = false;
            RadioButton_KeywordUpdateTab_CaptureSoftware_SGPro.Checked = false;
            RadioButton_KeywordUpdateTab_CaptureSoftware_TheSkyX.Checked = false;
            RadioButton_KeywordUpdateTab_CaptureSoftware_Voyager.Checked = false;

            RadioButton_KeywordUpdateTab_CaptureSoftware_NINA.ForeColor = Color.Black;
            RadioButton_KeywordUpdateTab_CaptureSoftware_SharpCap.ForeColor = Color.Black;
            RadioButton_KeywordUpdateTab_CaptureSoftware_SGPro.ForeColor = Color.Black;
            RadioButton_KeywordUpdateTab_CaptureSoftware_TheSkyX.ForeColor = Color.Black;
            RadioButton_KeywordUpdateTab_CaptureSoftware_Voyager.ForeColor = Color.Black;

            Button_KeywordUpdateTab_CaptureSoftware_SetAll.ForeColor = Color.Black;
            Button_KeywordUpdateTab_CaptureSoftware_SetByFile.ForeColor = Color.Black;
        }

        private void FindCaptureSoftware()
        {
            // Check each source file for different or the same capture software
            int foundTSX = 0;
            int foundSGP = 0;
            int foundNINA = 0;
            int foundVOY = 0;
            int foundSCP = 0;
            int count = 0;

            foreach (XisfFile file in mFileList)
            {
                string softwareCreator = file.CaptureSoftware; // from SWCREATE

                if (softwareCreator.Equals("NINA"))
                {
                    foundNINA++;
                    count++;
                    continue;
                }

                if (softwareCreator.Equals("SGP"))
                {
                    foundSGP++;
                    count++;
                    continue;
                }

                if (softwareCreator.Equals("TSX"))
                {
                    foundTSX++;
                    count++;
                    continue;
                }

                if (softwareCreator.Equals("VOY"))
                {
                    foundVOY++;
                    count++;
                    continue;
                }

                if (softwareCreator.Equals("SCP"))
                {
                    foundSCP++;
                    count++;
                    continue;
                }
            }

            if ((count != (foundNINA + foundSCP + foundSGP + foundTSX + foundVOY)) || (count == 0))
            {
                // Missing at least one. If we found any, make DarkViolet otherwise Red
                RadioButton_KeywordUpdateTab_CaptureSoftware_NINA.ForeColor = (foundNINA == 0) ? Color.Red : Color.DarkViolet;
                RadioButton_KeywordUpdateTab_CaptureSoftware_SharpCap.ForeColor = (foundSCP == 0) ? Color.Red : Color.DarkViolet;
                RadioButton_KeywordUpdateTab_CaptureSoftware_SGPro.ForeColor = (foundSGP == 0) ? Color.Red : Color.DarkViolet;
                RadioButton_KeywordUpdateTab_CaptureSoftware_TheSkyX.ForeColor = (foundTSX == 0) ? Color.Red : Color.DarkViolet;
                RadioButton_KeywordUpdateTab_CaptureSoftware_Voyager.ForeColor = (foundVOY == 0) ? Color.Red : Color.DarkViolet;

                // Missing at least on. Uncheck all
                RadioButton_KeywordUpdateTab_CaptureSoftware_TheSkyX.Checked = false;
                RadioButton_KeywordUpdateTab_CaptureSoftware_NINA.Checked = false;
                RadioButton_KeywordUpdateTab_CaptureSoftware_SGPro.Checked = false;
                RadioButton_KeywordUpdateTab_CaptureSoftware_Voyager.Checked = false;
                RadioButton_KeywordUpdateTab_CaptureSoftware_SharpCap.Checked = false;

                // Missing at least one. Set SetAll and SetByFile Buttons to Red
                Button_KeywordUpdateTab_CaptureSoftware_SetAll.ForeColor = Color.Red;
                Button_KeywordUpdateTab_CaptureSoftware_SetByFile.ForeColor = Color.Red;
            }
            else
            {
                // All matched. Is there more that one SoftwareCreator? If so, DarkViolet otherwise Black
                RadioButton_KeywordUpdateTab_CaptureSoftware_NINA.ForeColor = ((foundNINA == count) || (foundNINA == 0)) ? Color.Black : Color.DarkGreen;
                RadioButton_KeywordUpdateTab_CaptureSoftware_SharpCap.ForeColor = ((foundSCP == count) || (foundSCP == 0)) ? Color.Black : Color.DarkGreen;
                RadioButton_KeywordUpdateTab_CaptureSoftware_SGPro.ForeColor = ((foundSGP == count) || (foundSGP == 0)) ? Color.Black : Color.DarkGreen;
                RadioButton_KeywordUpdateTab_CaptureSoftware_TheSkyX.ForeColor = ((foundTSX == count) || (foundTSX == 0)) ? Color.Black : Color.DarkGreen;
                RadioButton_KeywordUpdateTab_CaptureSoftware_Voyager.ForeColor = ((foundVOY == count) || (foundVOY == 0)) ? Color.Black : Color.DarkGreen;

                // All matched. Is there a single SoftwareCreator? If so, checked otherwise unchecked
                RadioButton_KeywordUpdateTab_CaptureSoftware_NINA.Checked = (foundNINA == count) ? true : false;
                RadioButton_KeywordUpdateTab_CaptureSoftware_SharpCap.Checked = (foundSCP == count) ? true : false;
                RadioButton_KeywordUpdateTab_CaptureSoftware_SGPro.Checked = (foundSGP == count) ? true : false;
                RadioButton_KeywordUpdateTab_CaptureSoftware_TheSkyX.Checked = (foundTSX == count) ? true : false;
                RadioButton_KeywordUpdateTab_CaptureSoftware_Voyager.Checked = (foundVOY == count) ? true : false;

                // All matched. Set SetAll and SetByFile Buttons to Black
                Button_KeywordUpdateTab_CaptureSoftware_SetAll.ForeColor = Color.Black;
                Button_KeywordUpdateTab_CaptureSoftware_SetByFile.ForeColor = Color.Black;
            }
        }

        private void Button_CaptureSoftware_SetAll_Click(object sender, EventArgs e)
        {
            foreach (XisfFile file in mFileList)
            {
                if (RadioButton_KeywordUpdateTab_CaptureSoftware_NINA.Checked)
                    if (!file.CaptureSoftware.Equals("NINA"))
                        file.AddKeyword("SWCREATE", "NINA", "[name] Equipment Control and Automation Application");

                if (RadioButton_KeywordUpdateTab_CaptureSoftware_TheSkyX.Checked)
                    if (!file.CaptureSoftware.Equals("TSX"))
                        file.AddKeyword("SWCREATE", "TSX", "[name] Equipment Control and Automation Application");

                if (RadioButton_KeywordUpdateTab_CaptureSoftware_SGPro.Checked)
                    if (!file.CaptureSoftware.Equals("SGP"))
                        file.AddKeyword("SWCREATE", "SGP", "[name] Equipment Control and Automation Application");

                if (RadioButton_KeywordUpdateTab_CaptureSoftware_Voyager.Checked)
                    if (!file.CaptureSoftware.Equals("VOY"))
                        file.AddKeyword("SWCREATE", "VOY", "[name] Equipment Control and Automation Application");

                if (RadioButton_KeywordUpdateTab_CaptureSoftware_SharpCap.Checked)
                    if (!file.CaptureSoftware.Equals("SCP"))
                        file.AddKeyword("SWCREATE", "SCP", "[name] Equipment Control and Automation Application");
            }

            FindCaptureSoftware();
        }

        private void Button_CaptureSoftware_SetByFile_Click(object sender, EventArgs e)
        {
            bool global = false;
            string captureSoftware = string.Empty;

            foreach (XisfFile file in mFileList)
            {
                if (global)
                {
                    if (file.CaptureSoftware == string.Empty)
                        file.AddKeyword("SWCREATE", captureSoftware.ToString(), "XISF File Manager");
                }
                else
                {
                    captureSoftware = file.CaptureSoftware;
                    if (captureSoftware.Contains("Global_"))
                    {
                        global = true;
                        captureSoftware = captureSoftware.Replace("Global_", "");
                    }
                }

                file.CaptureSoftware = captureSoftware;

            }

            FindCaptureSoftware();
        }
    }
}
