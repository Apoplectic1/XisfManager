using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Windows.Shapes;
using XisfFileManager.Files;

namespace XisfFileManager
{
    public partial class MainForm
    {
        public async void SetupFluxDensity()
        {
            string fluxDir;
            string debayerDir;
            string targetDir;

            fluxDir = @"F:\PreProcessing\FluxDensity";
            if (Directory.Exists(fluxDir))
            {
                Directory.Delete(fluxDir, true);
            }

            // Clear all lists - we are reading or re-reading what will become a new xisf file data set that will invalidate any existing data.         
            mBCancel = false;
            mFileList.Clear();
            ImageParameterLists.Clear();
            ComboBox_KeywordUpdateTab_SubFrameKeywords_KeywordName.Items.Clear();
            ComboBox_KeywordUpdateTab_SubFrameKeywords_KeywordName.Text = "Keyword";
            ComboBox_KeywordUpdateTab_SubFrameKeywords_KeywordValue.Items.Clear();
            ComboBox_KeywordUpdateTab_SubFrameKeywords_KeywordValue.Text = "Value";
            ComboBox_KeywordUpdateTab_SubFrameKeywords_TargetNames.Text = "";
            ComboBox_KeywordUpdateTab_SubFrameKeywords_TargetNames.Items.Clear();
            TextBox_CalibrationTab_Messgaes.Clear();
            TreeView_CalibrationTab_TargetFileTree.Nodes.Clear();

            mCalibration.ResetAll();

            ClearCaptureSoftwareGroup();
            ClearTelescopeGroup();
            ClearCameraGroup();
            ClearFilterFrameTypeGroup();

            Label_FileSelection_Statistics_TempratureCoefficient.Text = "Temperature Coefficient: Not Computed";
            Label_FileSelection_Statistics_SubFrameOverhead.Text = "SubFrame Overhead: Not Computed";

            ProgressBar_FileSelection_ReadProgress.Value = 0;
            ProgressBar_KeywordUpdateTab_WriteProgress.Value = 0;

            // ************************************************************************************
            // PreProcessing Debayered files
            // ************************************************************************************

            List<string> mExcludeList = new List<string>()
            {
            };

            // Recurese into subdirectories
            Files.DirectoryOperations.Recurse = CheckBox_FileSelection_DirectorySelection_Recurse.Checked;

            // Find xisf files from the F:\PreProcessing\debayered folder
            DialogResult result = Files.DirectoryOperations.FindTargetFilesString(@"F:\PreProcessing\debayered", mExcludeList, ExcludeType.None);

            if ((result != DialogResult.OK) || (Files.DirectoryOperations.FileInfoList.Count == 0))
            {
                mFolderBrowseState = string.Empty;
            }
            else
            {
                mFolderBrowseState = Files.DirectoryOperations.SelectedFolder;
            }

            if (mFolderBrowseState != string.Empty)
            {
                Label_FileSelection_Statistics_OperationStatus.Text = "Reading " + Files.DirectoryOperations.FileInfoList.Count.ToString() + " Image Files";
                Label_FileSelection_Statistics_TempratureCoefficient.Text = "Temperature Coefficient: Not Computed";
                Label_FileSelection_Statistics_SubFrameOverhead.Text = "SubFrame Overhead: Not Computed";

                ProgressBar_FileSelection_ReadProgress.Value = 0;
                ProgressBar_FileSelection_ReadProgress.Maximum = Files.DirectoryOperations.FileInfoList.Count;

                System.Windows.Forms.Application.DoEvents();

                foreach (FileInfo debayerFile in Files.DirectoryOperations.FileInfoList)
                {
                    if (mBCancel)
                        break;

                    Label_FileSelection_BrowseFileName.Text = debayerFile.DirectoryName + "\n" + debayerFile.Name;
                    ProgressBar_FileSelection_ReadProgress.Value += 1;
                    System.Windows.Forms.Application.DoEvents();

                    // Create a new xisf file instance
                    mFile = new XisfFile
                    {
                        FilePath = debayerFile.FullName
                    };

                    await mXmlReader.ReadXisfFileHeaderKeywords(mFile);

                    mFileList.Add(mFile);
                }

                FindCaptureSoftware();
                FindFilterFrameType();
                FindTelescope();
                FindCamera();
                System.Windows.Forms.Application.DoEvents();

                // Force each file's keywords to be mono images with correct filter keywords
                Button_KeywordImageTypeFrame_SetAll_Click(this, EventArgs.Empty);

                // Okay - we have the complete list of debayed mono w/filter files.
                // Write them to the FluxDensity directory subdirectories

                ProgressBar_KeywordUpdateTab_WriteProgress.Value = 0;
                ProgressBar_KeywordUpdateTab_WriteProgress.Maximum = mFileList.Count;

                foreach (XisfFile xFile in mFileList)
                {
                    if (mBCancel)
                        break;

                    ProgressBar_KeywordUpdateTab_WriteProgress.Value += 1;

                    Keyword panel = xFile.GetKeyword("CPANEL");
                    Keyword stars = xFile.GetKeyword("CSTARS");
                    Keyword filter = xFile.GetKeyword("FILTER");

                    debayerDir = @"F:\PreProcessing\FluxDensity\";

                    if (panel != null)
                        debayerDir += panel.Value + @"\";

                    if (stars != null)
                        debayerDir += stars.Value + @"\" + filter.Value + @"\";
                    else
                        debayerDir += filter.Value + @"\";

                    if (!Directory.Exists(debayerDir))
                    {
                        Directory.CreateDirectory(debayerDir);
                    }

                    Label_KeywordUpdateTab_FileName.Text = Label_KeywordUpdateTab_FileName.Text = debayerDir + "\n" + System.IO.Path.GetFileName(xFile.FilePath);

                    debayerDir += System.IO.Path.GetFileName(xFile.FilePath);

                    var bStatus = mXisfFileUpdate.UpdateFile(xFile, debayerDir);

                    System.Windows.Forms.Application.DoEvents();

                    if (bStatus == false)
                    {
                        Label_FileSelection_Statistics_OperationStatus.Text = "File Write Error";

                        result = MessageBox.Show(
                            "File Update Failed - Protected or I/O Error.\n\n" + Label_KeywordUpdateTab_FileName.Text,
                            "\nMainForm.cs Button_Update_Click()",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);

                        return;
                    }

                }
            }
            
            // ************************************************************************************
            // PreProcessing Target files
            // ************************************************************************************

            // Clear all lists 
            mBCancel = false;
            mFileList.Clear();
            ImageParameterLists.Clear();
            ComboBox_KeywordUpdateTab_SubFrameKeywords_KeywordName.Items.Clear();
            ComboBox_KeywordUpdateTab_SubFrameKeywords_KeywordName.Text = "Keyword";
            ComboBox_KeywordUpdateTab_SubFrameKeywords_KeywordValue.Items.Clear();
            ComboBox_KeywordUpdateTab_SubFrameKeywords_KeywordValue.Text = "Value";
            ComboBox_KeywordUpdateTab_SubFrameKeywords_TargetNames.Text = "";
            ComboBox_KeywordUpdateTab_SubFrameKeywords_TargetNames.Items.Clear();
            TextBox_CalibrationTab_Messgaes.Clear();
            TreeView_CalibrationTab_TargetFileTree.Nodes.Clear();

            mCalibration.ResetAll();

            ClearCaptureSoftwareGroup();
            ClearTelescopeGroup();
            ClearCameraGroup();
            ClearFilterFrameTypeGroup();
            System.Windows.Forms.Application.DoEvents();

            if (Directory.Exists(@"F:\PreProcessing\cosmetized"))
                targetDir = @"F:\PreProcessing\cosmetized";
            else
                targetDir = @"F:\PreProcessing\calibrated";

            mExcludeList = new List<string>()
            {
                "CFA"
            };

            // Find xisf files from the selected folder
            result = Files.DirectoryOperations.FindTargetFilesString(targetDir, mExcludeList, ExcludeType.Contains);

            if ((result != DialogResult.OK) || (Files.DirectoryOperations.FileInfoList.Count == 0))
            {
                mFolderBrowseState = string.Empty;
            }
            else
            {
                mFolderBrowseState = Files.DirectoryOperations.SelectedFolder;
            }

            if (mFolderBrowseState != string.Empty)
            {
                Label_FileSelection_Statistics_OperationStatus.Text = "Reading " + Files.DirectoryOperations.FileInfoList.Count.ToString() + " Image Files";
                Label_FileSelection_Statistics_TempratureCoefficient.Text = "Temperature Coefficient: Not Computed";
                Label_FileSelection_Statistics_SubFrameOverhead.Text = "SubFrame Overhead: Not Computed";

                ProgressBar_FileSelection_ReadProgress.Value = 0;
                ProgressBar_KeywordUpdateTab_WriteProgress.Value = 0;
                ProgressBar_FileSelection_ReadProgress.Maximum = Files.DirectoryOperations.FileInfoList.Count;


                // Upate the UI with data from the .xisf recursive directory search
                System.Windows.Forms.Application.DoEvents();

                foreach (FileInfo targetFile in Files.DirectoryOperations.FileInfoList)
                {
                    if (mBCancel)
                        break;

                    Label_FileSelection_BrowseFileName.Text = targetFile.DirectoryName + "\n" + targetFile.Name;
                    ProgressBar_FileSelection_ReadProgress.Value += 1;
                    System.Windows.Forms.Application.DoEvents();

                    // Create a new xisf file instance
                    mFile = new XisfFile
                    {
                        FilePath = targetFile.FullName
                    };

                    await mXmlReader.ReadXisfFileHeaderKeywords(mFile);

                    mFileList.Add(mFile);
                }

                FindCaptureSoftware();
                FindFilterFrameType();
                FindTelescope();
                FindCamera();
                System.Windows.Forms.Application.DoEvents();

                // *************************************************************************************
                // Write Target Files to FluxDensity Directory
                // *************************************************************************************

                // Okay - we have the complete list of target files
                // Write them to the FluxDensity directory subdirectories

                ProgressBar_KeywordUpdateTab_WriteProgress.Maximum = mFileList.Count;
                ProgressBar_KeywordUpdateTab_WriteProgress.Value = 0;

                foreach (XisfFile targetFile in mFileList)
                {
                    if (mBCancel)
                        break;

                    ProgressBar_KeywordUpdateTab_WriteProgress.Value += 1;
                    System.Windows.Forms.Application.DoEvents();

                    Keyword panel = targetFile.GetKeyword("CPANEL");
                    Keyword stars = targetFile.GetKeyword("CSTARS");
                    Keyword filter = targetFile.GetKeyword("FILTER");

                    targetDir = @"F:\PreProcessing\FluxDensity\";

                    if (panel != null)
                    {
                        targetDir += panel.Value + @"\";
                    }

                    if (stars != null)
                    {
                        targetDir += stars.Value + @"\" + filter.Value + @"\";
                    }
                    else
                    {
                        targetDir += filter.Value + @"\";
                    }

                    if (!Directory.Exists(targetDir))
                    {
                        Directory.CreateDirectory(targetDir);
                    }


                    Label_KeywordUpdateTab_FileName.Text = targetDir.ToString() + "\n" + System.IO.Path.GetFileName(targetFile.FilePath);

                    targetDir += System.IO.Path.GetFileName(targetFile.FilePath);

                    var bStatus = mXisfFileUpdate.UpdateFile(targetFile, targetDir);
                    System.Windows.Forms.Application.DoEvents();

                    if (bStatus == false)
                    {
                        Label_FileSelection_Statistics_OperationStatus.Text = "File Write Error";

                        result = MessageBox.Show(
                            "File Update Failed - Protected or I/O Error.\n\n" + Label_KeywordUpdateTab_FileName.Text,
                            "\nMainForm.cs Button_Update_Click()",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);

                        return;
                    }
                }
            }
        }
    }
}