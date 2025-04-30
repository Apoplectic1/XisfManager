using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using XisfFileManager.Calculations;
using XisfFileManager.Globals;
using XisfFileManager.Files;

namespace XisfFileManager
{
    // Event used to update the Calibration Tab Page Text Boxes and Progress Bar
    public delegate void DataReceivedEvent(CalibrationTabPageValues data);

    // ##########################################################################################################################
    // ##########################################################################################################################

    //[DesignerCategory("Form")]
    public partial class MainForm : Form
    {
        private readonly List<XisfFile> mFileList;
        private XisfFile mFile;
        private readonly Calibration mCalibration;
        private readonly ImageCalculations ImageParameterLists;
        private readonly XisfXmlReader mXmlReader;
        private readonly XisfFileRename mRenameFile;
        private string mFolderBrowseState;
        private readonly XisfFileManager.TargetScheduler.SqlLiteManager mSchedulerDB;
        private bool mBCancel;
        private readonly XisfFileUpdate mXisfFileUpdate;
        private eKeywordUpdateMode mKeywordUpdateProtection;
        private readonly DirectoryProperties mDirectoryProperties;
        private readonly CustomTreeView mExposureTreeView = new();

        // ##########################################################################################################################
        // Constructor
        // ##########################################################################################################################
        public MainForm()
        {
            InitializeComponent();
            CalibrationTabPageEvent.CalibrationTabPage_InvokeEvent += EventHandler_UpdateCalibrationPageForm;
            TreeView_SchedulerTab_ProfileTree.NodeMouseClick += TreeView_SchedulerTab_ProfileTree_NodeMouseClick;
            TreeView_SchedulerTab_ProjectTree.NodeMouseClick += TreeView_SchedulerTab_ProjectTree_NodeMouseClick;
            //TreeView_SchedulerTab_PlansTree.NodeMouseClick += TreeView_SchedulerTab_PlanTree_NodeMouseClick_NodeMouseClick;


            Panel existing = TabPage_TargetScheduler.Controls.OfType<Panel>().FirstOrDefault();

            mExposureTreeView.Dock = DockStyle.Fill; // TreeView fills the panel

            existing.Controls.Add(mExposureTreeView); // Add the TreeView to the panel
            TabPage_TargetScheduler.Controls.Add(existing); // Add the panel to the tab page



            mDirectoryProperties = new DirectoryProperties();
            mCalibration = new Calibration();
            mXmlReader = new XisfXmlReader();
            mSchedulerDB = new XisfFileManager.TargetScheduler.SqlLiteManager();
            mXisfFileUpdate = new XisfFileUpdate();
            mKeywordUpdateProtection = eKeywordUpdateMode.UPDATE_NEW;
            Label_FileSelection_Statistics_OperationStatus.Text = "";
            mFileList = new List<XisfFile>();
            mRenameFile = new XisfFileRename
            {
                RenameOrder = eOrder.INDEX
            };

            // This set of a much smaller number of numeric lists contains per image data used for Focuser Temperature compensation coefficient calculation and SSWEIGHTs
            ImageParameterLists = new ImageCalculations();

            Label_FileSelection_Statistics_OperationStatus.Text = "No Images Selected";
            Label_FileSelection_Statistics_TempratureCoefficient.Text = "Temperature Coefficient: Not Computed";

            Version version = Assembly.GetExecutingAssembly().GetName().Version;
            this.Text = $"XISF File Manager - " + System.IO.File.GetLastWriteTime(System.Reflection.Assembly.GetExecutingAssembly().Location).ToString("yyyy.MM.dd - h:mm tt");


            Utility.ToolTips.AddToolTip(RadioButton_FileSelection_Index_ByFilter, "Orders Files by Capture Time per Filter", "\"By Target\" orders each filter's files consecutively.\r\n\"By Night\" orders each filter's files consecutively by night.");
            Utility.ToolTips.AddToolTip(RadioButton_FileSelection_Index_ByTime, "Orders Files by Capture Time", "\"By Target\" orders all files consecutively.\r\n\"By Night\" orders all files consecutively by night.");
            UpdateUI(eUiState.DISABLED);
        }

        // ****************************************************************************************************************
        // ************************ Event Handlers ************************************************************************
        // ****************************************************************************************************************
        private void EventHandler_UpdateCalibrationPageForm(CalibrationTabPageValues data)
        {
            ProgressBar_CalibrationTab.Maximum = data.ProgressMax;
            ProgressBar_CalibrationTab.Value = data.Progress;
            Label_CalibrationTab_ReadFileName.Text = data.FileName;
            Label_CalibrationTab_TotalFiles.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            Label_CalibrationTab_TotalFiles.Text = "Found " + data.TotalFiles.ToString() + " Library Files";

            switch (data.MessageMode)
            {
                case eMessageMode.CLEAR:
                    TextBox_CalibrationTab_Messgaes.Clear();
                    break;

                case eMessageMode.APPEND:
                    TextBox_CalibrationTab_Messgaes.AppendText(data.MatchCalibrationMessage);
                    break;

                case eMessageMode.NEW:
                    TextBox_CalibrationTab_Messgaes.Clear();
                    TextBox_CalibrationTab_Messgaes.AppendText(data.MatchCalibrationMessage);
                    break;

                default:
                    break;

            }
            data.MessageMode = eMessageMode.KEEP;

            TextBox_CalibrationTab_MatchingTolerance_Exposure.Text = mCalibration.ExposureTolerance.ToString();
            TextBox_CalibrationTab_MatchingTolerance_Gain.Text = mCalibration.GainTolerance.ToString();
            TextBox_CalibrationTab_MatchingTolerance_Offset.Text = mCalibration.OffsetTolerance.ToString();
            TextBox_CalibrationTab_MatchingTolerance_Temperature.Text = mCalibration.TemperatureTolerance.ToString();

            TabPage_Calibration.Update();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            mFolderBrowseState = Properties.Settings.Default.Persist_FolderBrowseState;
            CheckBox_KeywordUpdateTab_SubFrameKeywords_UpdateTargetName.Checked = Properties.Settings.Default.Persist_UpdateTargetNameState;
            CheckBox_KeywordUpdateTab_SubFrameKeywords_UpdatePanelName.Checked = Properties.Settings.Default.Persist_UpdatePanelNameState;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);

            Properties.Settings.Default.Persist_FolderBrowseState = mFolderBrowseState;
            Properties.Settings.Default.Persist_UpdateTargetNameState = CheckBox_KeywordUpdateTab_SubFrameKeywords_UpdateTargetName.Checked;
            Properties.Settings.Default.Persist_UpdatePanelNameState = CheckBox_KeywordUpdateTab_SubFrameKeywords_UpdatePanelName.Checked;

            Properties.Settings.Default.Save();
        }

        // ##########################################################################################################################
        // ##########################################################################################################################

        private async void Button_Browse_Click(object sender, EventArgs e)
        {
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

            ProgressBar_FileSelection_ReadProgress.Value = 0;
            ProgressBar_KeywordUpdateTab_WriteProgress.Value = 0;

            // Exclude List
            // This list can contain any number of strings that will be used to exclude any full path (including a specified file name)
            // that contains the string below the selected folder.

            List<string> mExcludeList = new List<string>()
                {
                    "Calibrated",
                    "Calibration",
                    "Cosmetized",
                    "Duplicates",
                    "Master",
                    "PreProcessing",
                    "Project",
                    "Registered"
                };

            // remove "Master" from the exclude list if the Masters checkbox is checked
            if (CheckBox_FileSelection_DirectorySelection_Masters.Checked)
            {
                mExcludeList.Remove("Master");
                mExcludeList.Remove("Calibration");
            }

            // Recurese into subdirectories?
            Files.DirectoryOperations.Recurse = CheckBox_FileSelection_DirectorySelection_Recurse.Checked;

            // Open a dialog to select a folder
            DialogResult result = Files.DirectoryOperations.FindTargetFiles(mFolderBrowseState, mExcludeList);

            if ((result != DialogResult.OK) || (Files.DirectoryOperations.FileInfoList.Count == 0))
            {
                UpdateUI(eUiState.DISABLED);
                MessageBox.Show("No Xisf Files Found", "Select a different folder");
                return;
            }

            mFolderBrowseState = Files.DirectoryOperations.SelectedFolder;

            Label_FileSelection_Statistics_OperationStatus.Text = "Reading " + Files.DirectoryOperations.FileInfoList.Count.ToString() + " Image Files";
            Label_FileSelection_Statistics_TempratureCoefficient.Text = "Temperature Coefficient: Not Computed";
            Label_FileSelection_Statistics_SubFrameOverhead.Text = "SubFrame Overhead: Not Computed";

            ProgressBar_FileSelection_ReadProgress.Value = 0;
            ProgressBar_FileSelection_ReadProgress.Maximum = Files.DirectoryOperations.FileInfoList.Count;


            // Upate the UI with data from the .xisf recursive directory search
            System.Windows.Forms.Application.DoEvents();

            foreach (FileInfo xFile in Files.DirectoryOperations.FileInfoList)
            {
                Label_FileSelection_BrowseFileName.Text = xFile.DirectoryName + "\n" + xFile.Name;
                ProgressBar_FileSelection_ReadProgress.Value += 1;

                // Create a new xisf file instance
                mFile = new XisfFile
                {
                    FilePath = xFile.FullName
                };

                await mXmlReader.ReadXisfFileHeaderKeywords(mFile);

                mFileList.Add(mFile);
            }

            mFileList.Sort((a, b) => a.CaptureTime.CompareTo(b.CaptureTime)); // oldest is first

            if (CheckBox_FileSelection_DirectorySelection_Masters.Checked)
            {
                TextBox_FileSelection_DirectorySelection_Frames.Text = mFileList[0].MSTRFRMS.ToString();
                TextBox_FileSelection_DirectorySelection_Algo.Text = mFileList[0].MSTRALG;
            }
            else
            {
                TextBox_FileSelection_DirectorySelection_Frames.Text = "Frames";
                TextBox_FileSelection_DirectorySelection_Algo.Text = "Algo";
            }

                // **********************************************************************
                // Get TargetName and and Weights to populate ComboBoxes

                // First get a list of all the target names found in the source files, then find unique names and sort.
                // Place culled list in the target name combobox
                List<string> targetNameList = new();
            List<string> weightKeywordList = new();

            foreach (XisfFile file in mFileList)
            {
                targetNameList.Add(file.TargetName);
            }

            targetNameList = targetNameList.Distinct().ToList();
            targetNameList = targetNameList.OrderBy(q => q).ToList();

            // Add the target names to the combobox
            foreach (string item in targetNameList)
            {
                ComboBox_KeywordUpdateTab_SubFrameKeywords_TargetNames.Items.Add(item);
            }

            // Select the first item in the combobox
            ComboBox_KeywordUpdateTab_SubFrameKeywords_TargetNames.SelectedIndex = 0;


            if (targetNameList.Count <= 1)
            {
                // Single name or blank
                Label_KeywordUpdateTab_SubFrameKeywords_TagetName.ForeColor = Color.Black;
            }
            else
            {
                // If target names are not unique, check for pairs
                Dictionary<string, int> matchCounts = new Dictionary<string, int>();
                foreach (string item in targetNameList)
                {
                    string baseItem = item.EndsWith(" stars") ? item.Substring(0, item.Length - 6) : item;

                    if (matchCounts.TryGetValue(baseItem, out int value))
                        matchCounts[baseItem] = ++value;
                    else
                        matchCounts[baseItem] = 1;
                }

                bool bGreen = true;

                foreach (int count in matchCounts.Values)
                {
                    if (count == 1)
                    {
                        // Rule for items without a pair
                        Label_KeywordUpdateTab_SubFrameKeywords_TagetName.ForeColor = Color.DarkViolet;
                        bGreen = false;
                        break;
                    }
                    else if (count != 2)
                    {
                        // Rule for items that do not form exact pairs
                        Label_KeywordUpdateTab_SubFrameKeywords_TagetName.ForeColor = Color.Red;
                        bGreen = false;
                        break;
                    }
                }

                if (bGreen)
                    Label_KeywordUpdateTab_SubFrameKeywords_TagetName.ForeColor = Color.Green;
            }


            // Now find a list of any present weight keywords (not values). Find unique Keyords, sort and populate Weight combobox
            foreach (XisfFile file in mFileList)
            {
                weightKeywordList = file.WeightKeyword;
            }

            if (weightKeywordList.Count > 0)
            {
                weightKeywordList = weightKeywordList.Distinct().ToList();
                weightKeywordList = weightKeywordList.OrderBy(q => q).ToList();

                foreach (string item in weightKeywordList)
                {
                    ComboBox_KeywordUpdateTab_SubFrameKeywords_Weights_WeightKeywords.Items.Add(item);
                }

                if (weightKeywordList.Count > 1)
                {
                    Label_KeywordUpdateTab_SubFrameKeywords_Weights_WeightKeywords.ForeColor = Color.Red;
                }
                else
                {
                    Label_KeywordUpdateTab_SubFrameKeywords_Weights_WeightKeywords.ForeColor = Color.Black;
                }

                ComboBox_KeywordUpdateTab_SubFrameKeywords_Weights_WeightKeywords.SelectedIndex = 0;
            }
            else
            {
                ComboBox_KeywordUpdateTab_SubFrameKeywords_Weights_WeightKeywords.Items.Clear();
                Label_KeywordUpdateTab_SubFrameKeywords_Weights_WeightKeywords.ForeColor = Color.Black;
            }



            // Now make a list of all Keywords found in ALL files. Sort and populate comboBox
            List<string> keywordNamelist = new();

            foreach (XisfFile xFile in mFileList)
            {
                ComboBox_KeywordUpdateTab_SubFrameKeywords_KeywordFile.Items.Add(Path.GetFileName(xFile.FilePath));

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

            // **********************************************************************


            // **********************************************************************
            // Calculate Image paramters for UI
            foreach (XisfFile xFile in mFileList)
            {
                if (xFile.FilePath == string.Empty)
                    xFile.AddKeyword("FILENAME", "Original Name", Path.GetFileName(xFile.FilePath));

                ImageParameterLists.BuildImageParameterValueLists(xFile);
            }

            if (Files.DirectoryOperations.FileInfoList.Count == mFileList.Count)
                Label_FileSelection_Statistics_OperationStatus.Text = "Read all " + mFileList.Count.ToString() + " Image Files";
            else
                Label_FileSelection_Statistics_OperationStatus.Text = "Read " + mFileList.Count.ToString() + " out of " + Files.DirectoryOperations.FileInfoList.Count + " Image Files";

            Label_FileSelection_Statistics_SubFrameOverhead.Text = ImageCalculations.CalculateOverhead(mFileList);
            string stepsPerDegree = ImageCalculations.CalculateFocuserTemperatureCompensationCoefficient(mFileList);
            Label_FileSelection_Statistics_TempratureCoefficient.Text = "Temperature Coefficient: " + stepsPerDegree;

            // **********************************************************************

            FindCaptureSoftware();
            FindFilterFrameType();
            FindTelescope();
            FindCamera();

            // **********************************************************************

            // TreeView_CalibrationTab_Dates

            // Create the TreeView

            TreeView_CalibrationTab_TargetFileTree.Nodes.Clear();

            IOrderedEnumerable<IGrouping<string, XisfFile>> groupedByTargetName = mFileList.GroupBy(item => item.TargetName).OrderBy(group => group.Key);

            // Create the hierarchical TreeView
            foreach (IGrouping<string, XisfFile> targetGroup in groupedByTargetName)
            {
                TreeNode targetNode = new TreeNode(targetGroup.Key);
                TreeView_CalibrationTab_TargetFileTree.Nodes.Add(targetNode);

                // Group the items by Camera
                IOrderedEnumerable<IGrouping<string, XisfFile>> groupedByCamera = targetGroup.GroupBy(item => item.Camera).OrderBy(group => group.Key);

                foreach (IGrouping<string, XisfFile> cameraGroup in groupedByCamera)
                {
                    TreeNode cameraNode = new TreeNode(cameraGroup.Key);
                    targetNode.Nodes.Add(cameraNode);

                    // Group the item by ExposureSeconds
                    IOrderedEnumerable<IGrouping<double, XisfFile>> groupedByExposureSeconds = cameraGroup.GroupBy(item => item.ExposureSeconds).OrderByDescending(group => group.Key);

                    foreach (IGrouping<double, XisfFile> exposureGroup in groupedByExposureSeconds)
                    {
                        TreeNode exposureNode = new TreeNode(exposureGroup.Key.ToString());
                        cameraNode.Nodes.Add(exposureNode);

                        // Group the items by Filter
                        IOrderedEnumerable<IGrouping<string, XisfFile>> groupedByFilter = exposureGroup.GroupBy(item => item.FilterName).OrderBy(group => group.Key);

                        foreach (IGrouping<string, XisfFile> filterGroup in groupedByFilter)
                        {
                            TreeNode filterNode = new TreeNode($"{filterGroup.Key} - {filterGroup.Count()} files");
                            exposureNode.Nodes.Add(filterNode);
                        }
                    }
                }
            }

            ExpandAllNodes(TreeView_CalibrationTab_TargetFileTree.Nodes);
            TabControl.Enabled = true;


            // UI Updates
            UpdateUI(eUiState.ENABLED);

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
                        MessageBoxButtons.OKCancel,
                        MessageBoxIcon.Error);

                    // if Cancel, exit application
                    if (result == DialogResult.Cancel)
                    {
                        GroupBox_FileSelection.Enabled = true;
                        GroupBox_KeywordUpdateTab_SubFrameKeywords.Enabled = true;
                        GroupBox_KeywordUpdateTab_CaptureSoftware.Enabled = true;
                        GroupBox_KeywordUpdateTab_Telescope.Enabled = true;
                        GroupBox_KeywordUpdateTab_Camera.Enabled = true;
                        GroupBox_KeywordUpdateTab_ImageType.Enabled = true;
                        return;
                    }

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


        private void Button_KeywordUpdateTab_Cancel_Click(object sender, EventArgs e)
        {
            mBCancel = true;
        }
        // ************************************************************
        // Update UI
        private void UpdateUI(eUiState eState)
        {
            switch (eState)
            {
                case eUiState.DISABLED:
                    TabControl.Enabled = false;
                    CheckBox_FileSelection_DirectorySelection_Masters.Enabled = true;
                    TextBox_FileSelection_DirectorySelection_Frames.Enabled = false;
                    TextBox_FileSelection_DirectorySelection_Algo.Enabled = false;
                    Button_FileSelection_DirectorySelection_Rename.Enabled = false;
                    CheckBox_FileSlection_DirectorySelection_NoStatistics.Enabled = false;
                    GroupBox_FileSelection_SequenceNumbering.Enabled = false;
                    GroupBox_FileSelection_Statistics.Enabled = false;
                    Label_FileSelection_Statistics_OperationStatus.Text = "Operation Status: Idle";
                    Label_FileSelection_Statistics_SubFrameOverhead.Text = "SubFrame Overhead: Not Computed";
                    Label_FileSelection_Statistics_TempratureCoefficient.Text = "Temperature Coefficient: Not Computed";
                    Label_FileSelection_BrowseFileName.Text = "No Files Selected";
                    break;

                case eUiState.ENABLED:
                    TabControl.Enabled = true;
                    CheckBox_FileSelection_DirectorySelection_Masters.Enabled = true;
                    TextBox_FileSelection_DirectorySelection_Frames.Enabled = true;
                    TextBox_FileSelection_DirectorySelection_Algo.Enabled = true;
                    Button_FileSelection_DirectorySelection_Rename.Enabled = true;
                    CheckBox_FileSlection_DirectorySelection_NoStatistics.Enabled = true;
                    GroupBox_FileSelection_SequenceNumbering.Enabled = true;
                    GroupBox_FileSelection_Statistics.Enabled = true;
                    break;

                case eUiState.RENAME:
                    TabControl.Enabled = false;
                    CheckBox_FileSelection_DirectorySelection_Masters.Enabled = false;
                    TextBox_FileSelection_DirectorySelection_Frames.Enabled = false;
                    TextBox_FileSelection_DirectorySelection_Algo.Enabled = false;
                    Button_FileSelection_DirectorySelection_Rename.Enabled = false;
                    CheckBox_FileSlection_DirectorySelection_NoStatistics.Enabled = false;
                    GroupBox_FileSelection_SequenceNumbering.Enabled = false;
                    GroupBox_FileSelection_Statistics.Enabled = true;
                    Label_FileSelection_BrowseFileName.Text = "No Files Selected";
                    break;
            }
        }

        // ##########################################################################################################################
        // ##########################################################################################################################
    }
}
