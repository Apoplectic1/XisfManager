namespace XisfFileManager
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            ProgressBar_FileSelection_ReadProgress = new System.Windows.Forms.ProgressBar();
            GroupBox_FileSelection_SequenceNumbering = new System.Windows.Forms.GroupBox();
            RadioButton_FileSelection_SequenceNumbering_WeightOnly = new System.Windows.Forms.RadioButton();
            GroupBox_FileSelection_Count = new System.Windows.Forms.GroupBox();
            RadioButton_FileSelection_Index_ByFilter = new System.Windows.Forms.RadioButton();
            RadioButton_FileSelection_Index_ByTime = new System.Windows.Forms.RadioButton();
            RadioButton_FileSelection_SequenceNumbering_IndexOnly = new System.Windows.Forms.RadioButton();
            RadioButton_FileSelection_SequenceNumbering_IndexWeight = new System.Windows.Forms.RadioButton();
            RadioButton_FileSelection_SequenceNumbering_WeightIndex = new System.Windows.Forms.RadioButton();
            GroupBox_FileSelection_DirectorySelection = new System.Windows.Forms.GroupBox();
            GroupBox_FileSelection_DirectorySelection_FluxDensity = new System.Windows.Forms.GroupBox();
            Button_FileSelection_DirectorySelection_FluxDensity_Run = new System.Windows.Forms.Button();
            CheckBox_FileSelection_DirectorySelection_FluxDensity_Enable = new System.Windows.Forms.CheckBox();
            GroupBox_FileSelection_DirectorySelection_Masters = new System.Windows.Forms.GroupBox();
            TextBox_FileSelection_DirectorySelection_Masters_Rejection = new System.Windows.Forms.TextBox();
            CheckBox_FileSelection_DirectorySelection_Masters_Enable = new System.Windows.Forms.CheckBox();
            TextBox_FileSelection_DirectorySelection_Masters_Frames = new System.Windows.Forms.TextBox();
            CheckBox_FileSlection_DirectorySelection_NoStatistics = new System.Windows.Forms.CheckBox();
            Button_FileSelection_DirectorySelection_Rename = new System.Windows.Forms.Button();
            Button_FileSelection_DirectorySelection_Browse = new System.Windows.Forms.Button();
            CheckBox_FileSelection_DirectorySelection_Recurse = new System.Windows.Forms.CheckBox();
            GroupBox_FileSelection_Statistics = new System.Windows.Forms.GroupBox();
            Label_FileSelection_Statistics_OperationStatus = new System.Windows.Forms.Label();
            Label_FileSelection_Statistics_SubFrameOverhead = new System.Windows.Forms.Label();
            Label_FileSelection_Statistics_TempratureCoefficient = new System.Windows.Forms.Label();
            Label_FileSelection_BrowseFileName = new System.Windows.Forms.Label();
            GroupBox_FileSelection = new System.Windows.Forms.GroupBox();
            backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            TabPage_TargetScheduler = new System.Windows.Forms.TabPage();
            Panel_TargetScheduler = new System.Windows.Forms.Panel();
            GroupBox_Project_Priority = new System.Windows.Forms.GroupBox();
            RadioButton_ProjectPriority_High = new System.Windows.Forms.RadioButton();
            RadioButton_ProjectPriority_Normal = new System.Windows.Forms.RadioButton();
            RadioButton_ProjectPriority_Low = new System.Windows.Forms.RadioButton();
            Label_SchedulerTab_PlansText = new System.Windows.Forms.Label();
            Label_SchedulerTab_TargetsText = new System.Windows.Forms.Label();
            TreeView_SchedulerTab_TargetTree = new System.Windows.Forms.TreeView();
            Label_SchedulerTab_ProjectsText = new System.Windows.Forms.Label();
            Label_SchedulerTab_ProfilesText = new System.Windows.Forms.Label();
            TreeView_SchedulerTab_ProjectTree = new System.Windows.Forms.TreeView();
            TreeView_SchedulerTab_ProfileTree = new System.Windows.Forms.TreeView();
            Button_SchedulerTab_OpenDatabase = new System.Windows.Forms.Button();
            TabPage_Calibration = new System.Windows.Forms.TabPage();
            groupBox1 = new System.Windows.Forms.GroupBox();
            Label_CalibrationTab_Minimum = new System.Windows.Forms.Label();
            Button_CalibrationTab_FindPedestal = new System.Windows.Forms.Button();
            Label_CalibrationTab_Pedestal = new System.Windows.Forms.Label();
            NumericUpDown_CalibrationTab_MinBackground = new System.Windows.Forms.NumericUpDown();
            CheckBox_CalibrationTab_CreateNew = new System.Windows.Forms.CheckBox();
            TreeView_CalibrationTab_TargetFileTree = new System.Windows.Forms.TreeView();
            TextBox_CalibrationTab_Messgaes = new System.Windows.Forms.TextBox();
            GroupBox_CalibrationTab_MatchingTolerance = new System.Windows.Forms.GroupBox();
            CheckBox_CalibrationTab_MatchingTolerance_TemperatureNearest = new System.Windows.Forms.CheckBox();
            CheckBox_CalibrationTab_MatchingTolerance_OffsetNearest = new System.Windows.Forms.CheckBox();
            CheckBox_CalibrationTab_MatchingTolerance_GainNearest = new System.Windows.Forms.CheckBox();
            CheckBox_CalibrationTab_MatchingTolerance_ExposureNearest = new System.Windows.Forms.CheckBox();
            Label_CalibrationTab_MatchingTolerance_TemperatureDegrees = new System.Windows.Forms.Label();
            Label_CalibrationTab_MatchingTolerance_OffsetADU = new System.Windows.Forms.Label();
            Label_CalibrationTab_MatchingTolerance_GainUnits = new System.Windows.Forms.Label();
            Label_CalibrationTab_MatchingTolerance_ExposureSeconds = new System.Windows.Forms.Label();
            Label_CalibrationTab_MatchingTolerance_Percentage = new System.Windows.Forms.Label();
            TextBox_CalibrationTab_MatchingTolerance_Temperature = new System.Windows.Forms.TextBox();
            TextBox_CalibrationTab_MatchingTolerance_Offset = new System.Windows.Forms.TextBox();
            TextBox_CalibrationTab_MatchingTolerance_Gain = new System.Windows.Forms.TextBox();
            Label_CalibrationTab_MatchingTolerance_Temperature = new System.Windows.Forms.Label();
            Label_CalibrationTab_MatchingTolerance_Offset = new System.Windows.Forms.Label();
            Label_CalibrationTab_MatchingTolerance_Gain = new System.Windows.Forms.Label();
            Label_CalibrationTab_MatchingTolerance_Exposure = new System.Windows.Forms.Label();
            TextBox_CalibrationTab_MatchingTolerance_Exposure = new System.Windows.Forms.TextBox();
            Label_CalibrationTab_TotalFiles = new System.Windows.Forms.Label();
            ProgressBar_CalibrationTab = new System.Windows.Forms.ProgressBar();
            Label_CalibrationTab_ReadFileName = new System.Windows.Forms.Label();
            Button_CalibrationTab_CreateCalibrationDirectory = new System.Windows.Forms.Button();
            Button_CalibrationTab_MatchCalibrationFrames = new System.Windows.Forms.Button();
            Button_CalibrationTab_FindCalibrationFrames = new System.Windows.Forms.Button();
            TabPage_Keywords = new System.Windows.Forms.TabPage();
            Button_KeywordUpdateTab_Cancel = new System.Windows.Forms.Button();
            Label_KeywordUpdateTab_FileName = new System.Windows.Forms.Label();
            ProgressBar_KeywordUpdateTab_WriteProgress = new System.Windows.Forms.ProgressBar();
            GroupBox_KeywordUpdateTab_CaptureSoftware = new System.Windows.Forms.GroupBox();
            RadioButton_KeywordUpdateTab_CaptureSoftware_NINA = new System.Windows.Forms.RadioButton();
            Button_KeywordUpdateTab_CaptureSoftware_SetByFile = new System.Windows.Forms.Button();
            Button_KeywordUpdateTab_CaptureSoftware_SetAll = new System.Windows.Forms.Button();
            RadioButton_KeywordUpdateTab_CaptureSoftware_Voyager = new System.Windows.Forms.RadioButton();
            RadioButton_KeywordUpdateTab_CaptureSoftware_SharpCap = new System.Windows.Forms.RadioButton();
            RadioButton_KeywordUpdateTab_CaptureSoftware_SGPro = new System.Windows.Forms.RadioButton();
            RadioButton_KeywordUpdateTab_CaptureSoftware_TheSkyX = new System.Windows.Forms.RadioButton();
            GroupBox_KeywordUpdateTab_Telescope = new System.Windows.Forms.GroupBox();
            TextBox_KeywordUpdateTab_Telescope_FocalLength = new System.Windows.Forms.ComboBox();
            Label_KeywordUpdateTab_Telescope_FocalLength = new System.Windows.Forms.Label();
            Button_KeywordUpdateTab_Telescope_SetByFile = new System.Windows.Forms.Button();
            Button_KeywordUpdateTab_Telescope_SetAll = new System.Windows.Forms.Button();
            CheckBox_KeywordUpdateTab_Telescope_Riccardi = new System.Windows.Forms.CheckBox();
            RadioButton_KeywordUpdateTab_Telescope_Newtonian254 = new System.Windows.Forms.RadioButton();
            RadioButton_KeywordUpdateTab_Telescope_EvoStar150 = new System.Windows.Forms.RadioButton();
            RadioButton_KeywordUpdateTab_Telescope_APM107 = new System.Windows.Forms.RadioButton();
            GroupBox_KeywordUpdateTab_SubFrameKeywords = new System.Windows.Forms.GroupBox();
            ComboBox_KeywordUpdateTab_SubFrameKeywords_KeywordFile = new System.Windows.Forms.ComboBox();
            CheckBox_KeywordUpdateTab_SubFrameKeywords_UpdatePanelName = new System.Windows.Forms.CheckBox();
            RadioButton_KeywordUpdateTab_SubFrameKeywords_SpecificValue = new System.Windows.Forms.RadioButton();
            RadioButton_KeywordUpdateTab_SubFrameKeywords_AllValues = new System.Windows.Forms.RadioButton();
            ComboBox_KeywordUpdateTab_SubFrameKeywords_KeywordComment = new System.Windows.Forms.ComboBox();
            ComboBox_KeywordUpdateTab_SubFrameKeywords_KeywordValue = new System.Windows.Forms.ComboBox();
            GroupBox_SubFrameKeywords_CalibrationFiles = new System.Windows.Forms.GroupBox();
            Button_SubFrameKeywords_CalibrationFiles_ClearAll = new System.Windows.Forms.Button();
            GroupBox_KeywordUpdateTab_SubFrameKeywords_KeywordProtection = new System.Windows.Forms.GroupBox();
            RadioButton_KeywordUpdateTab_SubFrameKeywords_KeywordProtection_Protect = new System.Windows.Forms.RadioButton();
            RadioButton_KeywordUpdateTab_SubFrameKeywords_KeywordProtection_UpdateNew = new System.Windows.Forms.RadioButton();
            RadioButton_KeywordUpdateTab_SubFrameKeywords_KeywordProtection_Force = new System.Windows.Forms.RadioButton();
            CheckBox_KeywordUpdateTab_SubFrameKeywords_AlphabetizeKeywords = new System.Windows.Forms.CheckBox();
            GroupBox_KeywordUpdateTab_SubFrameKeywords_Weights = new System.Windows.Forms.GroupBox();
            RadioButton_KeywordUpdateTab_SubFrameKeywords_Weights_Calibration = new System.Windows.Forms.RadioButton();
            Button_KeywordUpdateTab_SubFrameKeywords_Weights_Remove = new System.Windows.Forms.Button();
            RadioButton_KeywordUpdateTab_SubFrameKeywords_Weights_Selected = new System.Windows.Forms.RadioButton();
            RadioButton_KeywordUpdateTab_SubFrameKeywords_Weights_All = new System.Windows.Forms.RadioButton();
            Label_KeywordUpdateTab_SubFrameKeywords_Weights_WeightKeywords = new System.Windows.Forms.Label();
            ComboBox_KeywordUpdateTab_SubFrameKeywords_Weights_WeightKeywords = new System.Windows.Forms.ComboBox();
            Button_KeywordUpdateTab_SubFrameKeywords_Delete = new System.Windows.Forms.Button();
            Button_KeywordUpdateTab_SubFrameKeywords_AddReplace = new System.Windows.Forms.Button();
            ComboBox_KeywordUpdateTab_SubFrameKeywords_KeywordName = new System.Windows.Forms.ComboBox();
            CheckBox_KeywordUpdateTab_SubFrameKeywords_UpdateTargetName = new System.Windows.Forms.CheckBox();
            Button_KeywordUpdateTab_SubFrameKeywords_UpdateKeywords = new System.Windows.Forms.Button();
            ComboBox_KeywordUpdateTab_SubFrameKeywords_TargetNames = new System.Windows.Forms.ComboBox();
            Label_KeywordUpdateTab_SubFrameKeywords_TagetName = new System.Windows.Forms.Label();
            GroupBox_KeywordUpdateTab_Camera = new System.Windows.Forms.GroupBox();
            ComboBox_KeywordUpdateTab_Camera_A144Binning = new System.Windows.Forms.ComboBox();
            ComboBox_KeywordUpdateTab_Camera_Q178Binning = new System.Windows.Forms.ComboBox();
            ComboBox_KeywordUpdateTab_Camera_Z183Binning = new System.Windows.Forms.ComboBox();
            ComboBox_KeywordUpdateTab_Camera_Z533Binning = new System.Windows.Forms.ComboBox();
            ComboBox_KeywordUpdateTab_Camera_A144SensorTemp = new System.Windows.Forms.ComboBox();
            ComboBox_KeywordUpdateTab_Camera_Q178SensorTemp = new System.Windows.Forms.ComboBox();
            ComboBox_KeywordUpdateTab_Camera_Z183SensorTemp = new System.Windows.Forms.ComboBox();
            ComboBox_KeywordUpdateTab_Camera_Z533SensorTemp = new System.Windows.Forms.ComboBox();
            ComboBox_KeywordUpdateTab_Camera_Q178Offset = new System.Windows.Forms.ComboBox();
            ComboBox_KeywordUpdateTab_Camera_Z183Offset = new System.Windows.Forms.ComboBox();
            ComboBox_KeywordUpdateTab_Camera_Z533Offset = new System.Windows.Forms.ComboBox();
            ComboBox_KeywordUpdateTab_Camera_Q178Gain = new System.Windows.Forms.ComboBox();
            ComboBox_KeywordUpdateTab_Camera_Z183Gain = new System.Windows.Forms.ComboBox();
            ComboBox_KeywordUpdateTab_Camera_Z533Gain = new System.Windows.Forms.ComboBox();
            ComboBox_KeywordUpdateTab_Camera_A144Seconds = new System.Windows.Forms.ComboBox();
            ComboBox_KeywordUpdateTab_Camera_Q178Seconds = new System.Windows.Forms.ComboBox();
            ComboBox_KeywordUpdateTab_Camera_Z533Seconds = new System.Windows.Forms.ComboBox();
            ComboBox_KeywordUpdateTab_Camera_Z183Seconds = new System.Windows.Forms.ComboBox();
            Label_KeywordUpdateTab_Camera_ToggleNBPreset = new System.Windows.Forms.Label();
            Label_KeywordUpdateTab_Camera_Seconds = new System.Windows.Forms.Label();
            Button_KeywordUpdateSubFrameKeywordsCamera_ToggleNB = new System.Windows.Forms.Button();
            CheckBox_KeywordUpdateTab_Camera_A144 = new System.Windows.Forms.CheckBox();
            CheckBox_KeywordUpdateTab_Camera_Q178 = new System.Windows.Forms.CheckBox();
            CheckBox_KeywordUpdateTab_Camera_Z183 = new System.Windows.Forms.CheckBox();
            CheckBox_KeywordUpdateTab_Camera_Z533 = new System.Windows.Forms.CheckBox();
            Label_KeywordUpdateTab_Camera_Binning = new System.Windows.Forms.Label();
            Label_KeywordUpdateTab_Camera_SensorTemp = new System.Windows.Forms.Label();
            Label_KeywordUpdateTab_Camera_Camera = new System.Windows.Forms.Label();
            Button_KeywordUpdateTab_Camera_SetByFile = new System.Windows.Forms.Button();
            Button_KeywordUpdateTab_Camera_SetAll = new System.Windows.Forms.Button();
            Label_KeywordUpdateTab_Camera_A144Gain = new System.Windows.Forms.Label();
            Label_KeywordUpdateTab_Camera_Offset = new System.Windows.Forms.Label();
            Label_KeywordUpdateTab_Camera_Gain = new System.Windows.Forms.Label();
            GroupBox_KeywordUpdateTab_ImageType = new System.Windows.Forms.GroupBox();
            Button_KeywordsTab_ImageType_SetByFile = new System.Windows.Forms.Button();
            Button_KeywordsTab_ImageType_SetAll = new System.Windows.Forms.Button();
            GroupBox_KeywordUpdateTab_ImageType_Frame = new System.Windows.Forms.GroupBox();
            Button_KeywordsTab_ImageType_Frame_SetMaster = new System.Windows.Forms.Button();
            RadioButton_KeywordsTab_ImageType_Frame_Bias = new System.Windows.Forms.RadioButton();
            RadioButton_KeywordsTab_ImageType_Frame_Flat = new System.Windows.Forms.RadioButton();
            RadioButton_KeywordsTab_ImageType_Frame_Dark = new System.Windows.Forms.RadioButton();
            RadioButton_KeywordsTab_ImageType_Frame_Light = new System.Windows.Forms.RadioButton();
            GroupBox_KeywordUpdateTab_ImageType_Filter = new System.Windows.Forms.GroupBox();
            RadioButton_KeywordsTab_ImageType_Filter_Luma = new System.Windows.Forms.RadioButton();
            RadioButton_KeywordsTab_ImageType_Filter_Shutter = new System.Windows.Forms.RadioButton();
            RadioButton_KeywordsTab_ImageType_Filter_Red = new System.Windows.Forms.RadioButton();
            RadioButton_KeywordsTab_ImageType_Filter_S2 = new System.Windows.Forms.RadioButton();
            RadioButton_KeywordsTab_ImageType_Filter_Ha = new System.Windows.Forms.RadioButton();
            RadioButton_KeywordsTab_ImageType_Filter_Blue = new System.Windows.Forms.RadioButton();
            RadioButton_KeywordsTab_ImageType_Filter_Green = new System.Windows.Forms.RadioButton();
            RadioButton_KeywordsTab_ImageType_Filter_O3 = new System.Windows.Forms.RadioButton();
            TabControl = new System.Windows.Forms.TabControl();
            GroupBox_FileSelection_SequenceNumbering.SuspendLayout();
            GroupBox_FileSelection_Count.SuspendLayout();
            GroupBox_FileSelection_DirectorySelection.SuspendLayout();
            GroupBox_FileSelection_DirectorySelection_FluxDensity.SuspendLayout();
            GroupBox_FileSelection_DirectorySelection_Masters.SuspendLayout();
            GroupBox_FileSelection_Statistics.SuspendLayout();
            GroupBox_FileSelection.SuspendLayout();
            TabPage_TargetScheduler.SuspendLayout();
            GroupBox_Project_Priority.SuspendLayout();
            TabPage_Calibration.SuspendLayout();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)NumericUpDown_CalibrationTab_MinBackground).BeginInit();
            GroupBox_CalibrationTab_MatchingTolerance.SuspendLayout();
            TabPage_Keywords.SuspendLayout();
            GroupBox_KeywordUpdateTab_CaptureSoftware.SuspendLayout();
            GroupBox_KeywordUpdateTab_Telescope.SuspendLayout();
            GroupBox_KeywordUpdateTab_SubFrameKeywords.SuspendLayout();
            GroupBox_SubFrameKeywords_CalibrationFiles.SuspendLayout();
            GroupBox_KeywordUpdateTab_SubFrameKeywords_KeywordProtection.SuspendLayout();
            GroupBox_KeywordUpdateTab_SubFrameKeywords_Weights.SuspendLayout();
            GroupBox_KeywordUpdateTab_Camera.SuspendLayout();
            GroupBox_KeywordUpdateTab_ImageType.SuspendLayout();
            GroupBox_KeywordUpdateTab_ImageType_Frame.SuspendLayout();
            GroupBox_KeywordUpdateTab_ImageType_Filter.SuspendLayout();
            TabControl.SuspendLayout();
            SuspendLayout();
            // 
            // ProgressBar_FileSelection_ReadProgress
            // 
            ProgressBar_FileSelection_ReadProgress.Location = new System.Drawing.Point(20, 232);
            ProgressBar_FileSelection_ReadProgress.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            ProgressBar_FileSelection_ReadProgress.Name = "ProgressBar_FileSelection_ReadProgress";
            ProgressBar_FileSelection_ReadProgress.Size = new System.Drawing.Size(1099, 13);
            ProgressBar_FileSelection_ReadProgress.Step = 1;
            ProgressBar_FileSelection_ReadProgress.TabIndex = 1;
            // 
            // GroupBox_FileSelection_SequenceNumbering
            // 
            GroupBox_FileSelection_SequenceNumbering.Controls.Add(RadioButton_FileSelection_SequenceNumbering_WeightOnly);
            GroupBox_FileSelection_SequenceNumbering.Controls.Add(GroupBox_FileSelection_Count);
            GroupBox_FileSelection_SequenceNumbering.Controls.Add(RadioButton_FileSelection_SequenceNumbering_IndexOnly);
            GroupBox_FileSelection_SequenceNumbering.Controls.Add(RadioButton_FileSelection_SequenceNumbering_IndexWeight);
            GroupBox_FileSelection_SequenceNumbering.Controls.Add(RadioButton_FileSelection_SequenceNumbering_WeightIndex);
            GroupBox_FileSelection_SequenceNumbering.Location = new System.Drawing.Point(886, 23);
            GroupBox_FileSelection_SequenceNumbering.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            GroupBox_FileSelection_SequenceNumbering.Name = "GroupBox_FileSelection_SequenceNumbering";
            GroupBox_FileSelection_SequenceNumbering.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            GroupBox_FileSelection_SequenceNumbering.Size = new System.Drawing.Size(232, 141);
            GroupBox_FileSelection_SequenceNumbering.TabIndex = 3;
            GroupBox_FileSelection_SequenceNumbering.TabStop = false;
            GroupBox_FileSelection_SequenceNumbering.Text = "Sequence Numbering";
            // 
            // RadioButton_FileSelection_SequenceNumbering_WeightOnly
            // 
            RadioButton_FileSelection_SequenceNumbering_WeightOnly.AutoSize = true;
            RadioButton_FileSelection_SequenceNumbering_WeightOnly.Location = new System.Drawing.Point(27, 38);
            RadioButton_FileSelection_SequenceNumbering_WeightOnly.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            RadioButton_FileSelection_SequenceNumbering_WeightOnly.Name = "RadioButton_FileSelection_SequenceNumbering_WeightOnly";
            RadioButton_FileSelection_SequenceNumbering_WeightOnly.Size = new System.Drawing.Size(91, 19);
            RadioButton_FileSelection_SequenceNumbering_WeightOnly.TabIndex = 3;
            RadioButton_FileSelection_SequenceNumbering_WeightOnly.Text = "Weight Only";
            RadioButton_FileSelection_SequenceNumbering_WeightOnly.UseVisualStyleBackColor = true;
            RadioButton_FileSelection_SequenceNumbering_WeightOnly.CheckedChanged += RadioButton_Weight_CheckedChanged;
            // 
            // GroupBox_FileSelection_Count
            // 
            GroupBox_FileSelection_Count.Controls.Add(RadioButton_FileSelection_Index_ByFilter);
            GroupBox_FileSelection_Count.Controls.Add(RadioButton_FileSelection_Index_ByTime);
            GroupBox_FileSelection_Count.Location = new System.Drawing.Point(14, 79);
            GroupBox_FileSelection_Count.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            GroupBox_FileSelection_Count.Name = "GroupBox_FileSelection_Count";
            GroupBox_FileSelection_Count.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            GroupBox_FileSelection_Count.Size = new System.Drawing.Size(205, 52);
            GroupBox_FileSelection_Count.TabIndex = 16;
            GroupBox_FileSelection_Count.TabStop = false;
            GroupBox_FileSelection_Count.Text = "Index";
            // 
            // RadioButton_FileSelection_Index_ByFilter
            // 
            RadioButton_FileSelection_Index_ByFilter.AutoSize = true;
            RadioButton_FileSelection_Index_ByFilter.Checked = true;
            RadioButton_FileSelection_Index_ByFilter.Location = new System.Drawing.Point(33, 21);
            RadioButton_FileSelection_Index_ByFilter.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            RadioButton_FileSelection_Index_ByFilter.Name = "RadioButton_FileSelection_Index_ByFilter";
            RadioButton_FileSelection_Index_ByFilter.Size = new System.Drawing.Size(67, 19);
            RadioButton_FileSelection_Index_ByFilter.TabIndex = 6;
            RadioButton_FileSelection_Index_ByFilter.TabStop = true;
            RadioButton_FileSelection_Index_ByFilter.Text = "By Filter";
            RadioButton_FileSelection_Index_ByFilter.UseVisualStyleBackColor = true;
            // 
            // RadioButton_FileSelection_Index_ByTime
            // 
            RadioButton_FileSelection_Index_ByTime.AutoSize = true;
            RadioButton_FileSelection_Index_ByTime.Location = new System.Drawing.Point(110, 21);
            RadioButton_FileSelection_Index_ByTime.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            RadioButton_FileSelection_Index_ByTime.Name = "RadioButton_FileSelection_Index_ByTime";
            RadioButton_FileSelection_Index_ByTime.Size = new System.Drawing.Size(68, 19);
            RadioButton_FileSelection_Index_ByTime.TabIndex = 5;
            RadioButton_FileSelection_Index_ByTime.Text = "By Time";
            RadioButton_FileSelection_Index_ByTime.UseVisualStyleBackColor = true;
            // 
            // RadioButton_FileSelection_SequenceNumbering_IndexOnly
            // 
            RadioButton_FileSelection_SequenceNumbering_IndexOnly.AutoSize = true;
            RadioButton_FileSelection_SequenceNumbering_IndexOnly.Checked = true;
            RadioButton_FileSelection_SequenceNumbering_IndexOnly.Location = new System.Drawing.Point(27, 16);
            RadioButton_FileSelection_SequenceNumbering_IndexOnly.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            RadioButton_FileSelection_SequenceNumbering_IndexOnly.Name = "RadioButton_FileSelection_SequenceNumbering_IndexOnly";
            RadioButton_FileSelection_SequenceNumbering_IndexOnly.Size = new System.Drawing.Size(81, 19);
            RadioButton_FileSelection_SequenceNumbering_IndexOnly.TabIndex = 2;
            RadioButton_FileSelection_SequenceNumbering_IndexOnly.TabStop = true;
            RadioButton_FileSelection_SequenceNumbering_IndexOnly.Text = "Index Only";
            RadioButton_FileSelection_SequenceNumbering_IndexOnly.UseVisualStyleBackColor = true;
            RadioButton_FileSelection_SequenceNumbering_IndexOnly.CheckedChanged += RadioButton_Index_CheckedChanged;
            // 
            // RadioButton_FileSelection_SequenceNumbering_IndexWeight
            // 
            RadioButton_FileSelection_SequenceNumbering_IndexWeight.AutoSize = true;
            RadioButton_FileSelection_SequenceNumbering_IndexWeight.Location = new System.Drawing.Point(122, 16);
            RadioButton_FileSelection_SequenceNumbering_IndexWeight.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            RadioButton_FileSelection_SequenceNumbering_IndexWeight.Name = "RadioButton_FileSelection_SequenceNumbering_IndexWeight";
            RadioButton_FileSelection_SequenceNumbering_IndexWeight.Size = new System.Drawing.Size(94, 19);
            RadioButton_FileSelection_SequenceNumbering_IndexWeight.TabIndex = 1;
            RadioButton_FileSelection_SequenceNumbering_IndexWeight.Text = "Index Weight";
            RadioButton_FileSelection_SequenceNumbering_IndexWeight.UseVisualStyleBackColor = true;
            RadioButton_FileSelection_SequenceNumbering_IndexWeight.CheckedChanged += RadioButton_IndexWeight_CheckedChanged;
            // 
            // RadioButton_FileSelection_SequenceNumbering_WeightIndex
            // 
            RadioButton_FileSelection_SequenceNumbering_WeightIndex.AutoSize = true;
            RadioButton_FileSelection_SequenceNumbering_WeightIndex.Location = new System.Drawing.Point(122, 38);
            RadioButton_FileSelection_SequenceNumbering_WeightIndex.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            RadioButton_FileSelection_SequenceNumbering_WeightIndex.Name = "RadioButton_FileSelection_SequenceNumbering_WeightIndex";
            RadioButton_FileSelection_SequenceNumbering_WeightIndex.Size = new System.Drawing.Size(94, 19);
            RadioButton_FileSelection_SequenceNumbering_WeightIndex.TabIndex = 0;
            RadioButton_FileSelection_SequenceNumbering_WeightIndex.Text = "Weight Index";
            RadioButton_FileSelection_SequenceNumbering_WeightIndex.UseVisualStyleBackColor = true;
            RadioButton_FileSelection_SequenceNumbering_WeightIndex.CheckedChanged += RadioButton_WeightIndex_CheckedChanged;
            // 
            // GroupBox_FileSelection_DirectorySelection
            // 
            GroupBox_FileSelection_DirectorySelection.Controls.Add(GroupBox_FileSelection_DirectorySelection_FluxDensity);
            GroupBox_FileSelection_DirectorySelection.Controls.Add(GroupBox_FileSelection_DirectorySelection_Masters);
            GroupBox_FileSelection_DirectorySelection.Controls.Add(CheckBox_FileSlection_DirectorySelection_NoStatistics);
            GroupBox_FileSelection_DirectorySelection.Controls.Add(Button_FileSelection_DirectorySelection_Rename);
            GroupBox_FileSelection_DirectorySelection.Controls.Add(Button_FileSelection_DirectorySelection_Browse);
            GroupBox_FileSelection_DirectorySelection.Controls.Add(CheckBox_FileSelection_DirectorySelection_Recurse);
            GroupBox_FileSelection_DirectorySelection.Location = new System.Drawing.Point(20, 23);
            GroupBox_FileSelection_DirectorySelection.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            GroupBox_FileSelection_DirectorySelection.Name = "GroupBox_FileSelection_DirectorySelection";
            GroupBox_FileSelection_DirectorySelection.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            GroupBox_FileSelection_DirectorySelection.Size = new System.Drawing.Size(388, 169);
            GroupBox_FileSelection_DirectorySelection.TabIndex = 7;
            GroupBox_FileSelection_DirectorySelection.TabStop = false;
            GroupBox_FileSelection_DirectorySelection.Text = "Directory Selection";
            // 
            // GroupBox_FileSelection_DirectorySelection_FluxDensity
            // 
            GroupBox_FileSelection_DirectorySelection_FluxDensity.Controls.Add(Button_FileSelection_DirectorySelection_FluxDensity_Run);
            GroupBox_FileSelection_DirectorySelection_FluxDensity.Controls.Add(CheckBox_FileSelection_DirectorySelection_FluxDensity_Enable);
            GroupBox_FileSelection_DirectorySelection_FluxDensity.Location = new System.Drawing.Point(281, 46);
            GroupBox_FileSelection_DirectorySelection_FluxDensity.Name = "GroupBox_FileSelection_DirectorySelection_FluxDensity";
            GroupBox_FileSelection_DirectorySelection_FluxDensity.Size = new System.Drawing.Size(100, 100);
            GroupBox_FileSelection_DirectorySelection_FluxDensity.TabIndex = 23;
            GroupBox_FileSelection_DirectorySelection_FluxDensity.TabStop = false;
            GroupBox_FileSelection_DirectorySelection_FluxDensity.Text = "Flux Density";
            // 
            // Button_FileSelection_DirectorySelection_FluxDensity_Run
            // 
            Button_FileSelection_DirectorySelection_FluxDensity_Run.Enabled = false;
            Button_FileSelection_DirectorySelection_FluxDensity_Run.Location = new System.Drawing.Point(10, 62);
            Button_FileSelection_DirectorySelection_FluxDensity_Run.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Button_FileSelection_DirectorySelection_FluxDensity_Run.Name = "Button_FileSelection_DirectorySelection_FluxDensity_Run";
            Button_FileSelection_DirectorySelection_FluxDensity_Run.Size = new System.Drawing.Size(83, 27);
            Button_FileSelection_DirectorySelection_FluxDensity_Run.TabIndex = 34;
            Button_FileSelection_DirectorySelection_FluxDensity_Run.Text = "Run";
            Button_FileSelection_DirectorySelection_FluxDensity_Run.UseVisualStyleBackColor = true;
            Button_FileSelection_DirectorySelection_FluxDensity_Run.Click += Button_KeywordUpdateTab_SubFrameKeywords_SetupFluxDensity_Click;
            // 
            // CheckBox_FileSelection_DirectorySelection_FluxDensity_Enable
            // 
            CheckBox_FileSelection_DirectorySelection_FluxDensity_Enable.AutoSize = true;
            CheckBox_FileSelection_DirectorySelection_FluxDensity_Enable.Location = new System.Drawing.Point(22, 33);
            CheckBox_FileSelection_DirectorySelection_FluxDensity_Enable.Name = "CheckBox_FileSelection_DirectorySelection_FluxDensity_Enable";
            CheckBox_FileSelection_DirectorySelection_FluxDensity_Enable.Size = new System.Drawing.Size(61, 19);
            CheckBox_FileSelection_DirectorySelection_FluxDensity_Enable.TabIndex = 33;
            CheckBox_FileSelection_DirectorySelection_FluxDensity_Enable.Text = "Enable";
            CheckBox_FileSelection_DirectorySelection_FluxDensity_Enable.UseVisualStyleBackColor = true;
            CheckBox_FileSelection_DirectorySelection_FluxDensity_Enable.CheckedChanged += CheckBox_FileSelection_DirectorySelection_EnableFluxDensity_CheckedChanged;
            // 
            // GroupBox_FileSelection_DirectorySelection_Masters
            // 
            GroupBox_FileSelection_DirectorySelection_Masters.Controls.Add(TextBox_FileSelection_DirectorySelection_Masters_Rejection);
            GroupBox_FileSelection_DirectorySelection_Masters.Controls.Add(CheckBox_FileSelection_DirectorySelection_Masters_Enable);
            GroupBox_FileSelection_DirectorySelection_Masters.Controls.Add(TextBox_FileSelection_DirectorySelection_Masters_Frames);
            GroupBox_FileSelection_DirectorySelection_Masters.Location = new System.Drawing.Point(13, 65);
            GroupBox_FileSelection_DirectorySelection_Masters.Name = "GroupBox_FileSelection_DirectorySelection_Masters";
            GroupBox_FileSelection_DirectorySelection_Masters.Size = new System.Drawing.Size(249, 52);
            GroupBox_FileSelection_DirectorySelection_Masters.TabIndex = 15;
            GroupBox_FileSelection_DirectorySelection_Masters.TabStop = false;
            GroupBox_FileSelection_DirectorySelection_Masters.Text = "Masters";
            // 
            // TextBox_FileSelection_DirectorySelection_Masters_Rejection
            // 
            TextBox_FileSelection_DirectorySelection_Masters_Rejection.Location = new System.Drawing.Point(182, 20);
            TextBox_FileSelection_DirectorySelection_Masters_Rejection.Name = "TextBox_FileSelection_DirectorySelection_Masters_Rejection";
            TextBox_FileSelection_DirectorySelection_Masters_Rejection.Size = new System.Drawing.Size(59, 23);
            TextBox_FileSelection_DirectorySelection_Masters_Rejection.TabIndex = 23;
            TextBox_FileSelection_DirectorySelection_Masters_Rejection.Text = "WSC";
            TextBox_FileSelection_DirectorySelection_Masters_Rejection.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // CheckBox_FileSelection_DirectorySelection_Masters_Enable
            // 
            CheckBox_FileSelection_DirectorySelection_Masters_Enable.AutoSize = true;
            CheckBox_FileSelection_DirectorySelection_Masters_Enable.Enabled = false;
            CheckBox_FileSelection_DirectorySelection_Masters_Enable.Location = new System.Drawing.Point(10, 22);
            CheckBox_FileSelection_DirectorySelection_Masters_Enable.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            CheckBox_FileSelection_DirectorySelection_Masters_Enable.Name = "CheckBox_FileSelection_DirectorySelection_Masters_Enable";
            CheckBox_FileSelection_DirectorySelection_Masters_Enable.Size = new System.Drawing.Size(61, 19);
            CheckBox_FileSelection_DirectorySelection_Masters_Enable.TabIndex = 3;
            CheckBox_FileSelection_DirectorySelection_Masters_Enable.Text = "Enable";
            CheckBox_FileSelection_DirectorySelection_Masters_Enable.UseVisualStyleBackColor = true;
            CheckBox_FileSelection_DirectorySelection_Masters_Enable.CheckedChanged += CheckBox_FileSelection_DirectorySelection_Masters_Enable_CheckedChanged;
            // 
            // TextBox_FileSelection_DirectorySelection_Masters_Frames
            // 
            TextBox_FileSelection_DirectorySelection_Masters_Frames.Enabled = false;
            TextBox_FileSelection_DirectorySelection_Masters_Frames.Location = new System.Drawing.Point(100, 20);
            TextBox_FileSelection_DirectorySelection_Masters_Frames.Name = "TextBox_FileSelection_DirectorySelection_Masters_Frames";
            TextBox_FileSelection_DirectorySelection_Masters_Frames.Size = new System.Drawing.Size(59, 23);
            TextBox_FileSelection_DirectorySelection_Masters_Frames.TabIndex = 9;
            TextBox_FileSelection_DirectorySelection_Masters_Frames.Text = "Frames";
            TextBox_FileSelection_DirectorySelection_Masters_Frames.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // CheckBox_FileSlection_DirectorySelection_NoStatistics
            // 
            CheckBox_FileSlection_DirectorySelection_NoStatistics.AutoSize = true;
            CheckBox_FileSlection_DirectorySelection_NoStatistics.Location = new System.Drawing.Point(173, 132);
            CheckBox_FileSlection_DirectorySelection_NoStatistics.Name = "CheckBox_FileSlection_DirectorySelection_NoStatistics";
            CheckBox_FileSlection_DirectorySelection_NoStatistics.Size = new System.Drawing.Size(91, 19);
            CheckBox_FileSlection_DirectorySelection_NoStatistics.TabIndex = 22;
            CheckBox_FileSlection_DirectorySelection_NoStatistics.Text = "No Statistics";
            CheckBox_FileSlection_DirectorySelection_NoStatistics.UseVisualStyleBackColor = true;
            // 
            // Button_FileSelection_DirectorySelection_Rename
            // 
            Button_FileSelection_DirectorySelection_Rename.Location = new System.Drawing.Point(17, 126);
            Button_FileSelection_DirectorySelection_Rename.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Button_FileSelection_DirectorySelection_Rename.Name = "Button_FileSelection_DirectorySelection_Rename";
            Button_FileSelection_DirectorySelection_Rename.Size = new System.Drawing.Size(145, 27);
            Button_FileSelection_DirectorySelection_Rename.TabIndex = 4;
            Button_FileSelection_DirectorySelection_Rename.Text = "Rename XISF Files";
            Button_FileSelection_DirectorySelection_Rename.UseVisualStyleBackColor = true;
            Button_FileSelection_DirectorySelection_Rename.Click += Button_FileSelection_DirectorySelection_Rename_Click;
            // 
            // Button_FileSelection_DirectorySelection_Browse
            // 
            Button_FileSelection_DirectorySelection_Browse.Location = new System.Drawing.Point(22, 29);
            Button_FileSelection_DirectorySelection_Browse.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Button_FileSelection_DirectorySelection_Browse.Name = "Button_FileSelection_DirectorySelection_Browse";
            Button_FileSelection_DirectorySelection_Browse.Size = new System.Drawing.Size(88, 27);
            Button_FileSelection_DirectorySelection_Browse.TabIndex = 0;
            Button_FileSelection_DirectorySelection_Browse.Text = "Browse";
            Button_FileSelection_DirectorySelection_Browse.UseVisualStyleBackColor = true;
            Button_FileSelection_DirectorySelection_Browse.Click += Button_Browse_Click;
            // 
            // CheckBox_FileSelection_DirectorySelection_Recurse
            // 
            CheckBox_FileSelection_DirectorySelection_Recurse.AutoSize = true;
            CheckBox_FileSelection_DirectorySelection_Recurse.Checked = true;
            CheckBox_FileSelection_DirectorySelection_Recurse.CheckState = System.Windows.Forms.CheckState.Checked;
            CheckBox_FileSelection_DirectorySelection_Recurse.Location = new System.Drawing.Point(129, 34);
            CheckBox_FileSelection_DirectorySelection_Recurse.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            CheckBox_FileSelection_DirectorySelection_Recurse.Name = "CheckBox_FileSelection_DirectorySelection_Recurse";
            CheckBox_FileSelection_DirectorySelection_Recurse.Size = new System.Drawing.Size(126, 19);
            CheckBox_FileSelection_DirectorySelection_Recurse.TabIndex = 2;
            CheckBox_FileSelection_DirectorySelection_Recurse.Text = "Recurse Directories";
            CheckBox_FileSelection_DirectorySelection_Recurse.UseVisualStyleBackColor = true;
            // 
            // GroupBox_FileSelection_Statistics
            // 
            GroupBox_FileSelection_Statistics.Controls.Add(Label_FileSelection_Statistics_OperationStatus);
            GroupBox_FileSelection_Statistics.Controls.Add(Label_FileSelection_Statistics_SubFrameOverhead);
            GroupBox_FileSelection_Statistics.Controls.Add(Label_FileSelection_Statistics_TempratureCoefficient);
            GroupBox_FileSelection_Statistics.Location = new System.Drawing.Point(416, 23);
            GroupBox_FileSelection_Statistics.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            GroupBox_FileSelection_Statistics.Name = "GroupBox_FileSelection_Statistics";
            GroupBox_FileSelection_Statistics.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            GroupBox_FileSelection_Statistics.Size = new System.Drawing.Size(460, 169);
            GroupBox_FileSelection_Statistics.TabIndex = 20;
            GroupBox_FileSelection_Statistics.TabStop = false;
            GroupBox_FileSelection_Statistics.Text = "Statistics";
            // 
            // Label_FileSelection_Statistics_OperationStatus
            // 
            Label_FileSelection_Statistics_OperationStatus.AutoSize = true;
            Label_FileSelection_Statistics_OperationStatus.Location = new System.Drawing.Point(16, 23);
            Label_FileSelection_Statistics_OperationStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            Label_FileSelection_Statistics_OperationStatus.Name = "Label_FileSelection_Statistics_OperationStatus";
            Label_FileSelection_Statistics_OperationStatus.Size = new System.Drawing.Size(95, 15);
            Label_FileSelection_Statistics_OperationStatus.TabIndex = 12;
            Label_FileSelection_Statistics_OperationStatus.Text = "Operation Status";
            // 
            // Label_FileSelection_Statistics_SubFrameOverhead
            // 
            Label_FileSelection_Statistics_SubFrameOverhead.AutoSize = true;
            Label_FileSelection_Statistics_SubFrameOverhead.Location = new System.Drawing.Point(16, 103);
            Label_FileSelection_Statistics_SubFrameOverhead.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            Label_FileSelection_Statistics_SubFrameOverhead.Name = "Label_FileSelection_Statistics_SubFrameOverhead";
            Label_FileSelection_Statistics_SubFrameOverhead.Size = new System.Drawing.Size(200, 15);
            Label_FileSelection_Statistics_SubFrameOverhead.TabIndex = 14;
            Label_FileSelection_Statistics_SubFrameOverhead.Text = "SubFrame Overhead: Not Computed";
            // 
            // Label_FileSelection_Statistics_TempratureCoefficient
            // 
            Label_FileSelection_Statistics_TempratureCoefficient.AutoSize = true;
            Label_FileSelection_Statistics_TempratureCoefficient.Location = new System.Drawing.Point(16, 63);
            Label_FileSelection_Statistics_TempratureCoefficient.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            Label_FileSelection_Statistics_TempratureCoefficient.Name = "Label_FileSelection_Statistics_TempratureCoefficient";
            Label_FileSelection_Statistics_TempratureCoefficient.Size = new System.Drawing.Size(221, 15);
            Label_FileSelection_Statistics_TempratureCoefficient.TabIndex = 13;
            Label_FileSelection_Statistics_TempratureCoefficient.Text = "Temperature Coefficient: Not Computed";
            // 
            // Label_FileSelection_BrowseFileName
            // 
            Label_FileSelection_BrowseFileName.AutoSize = true;
            Label_FileSelection_BrowseFileName.Location = new System.Drawing.Point(20, 195);
            Label_FileSelection_BrowseFileName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            Label_FileSelection_BrowseFileName.Name = "Label_FileSelection_BrowseFileName";
            Label_FileSelection_BrowseFileName.Size = new System.Drawing.Size(101, 15);
            Label_FileSelection_BrowseFileName.TabIndex = 21;
            Label_FileSelection_BrowseFileName.Text = "Browse File Name";
            // 
            // GroupBox_FileSelection
            // 
            GroupBox_FileSelection.Controls.Add(Label_FileSelection_BrowseFileName);
            GroupBox_FileSelection.Controls.Add(GroupBox_FileSelection_Statistics);
            GroupBox_FileSelection.Controls.Add(GroupBox_FileSelection_DirectorySelection);
            GroupBox_FileSelection.Controls.Add(GroupBox_FileSelection_SequenceNumbering);
            GroupBox_FileSelection.Controls.Add(ProgressBar_FileSelection_ReadProgress);
            GroupBox_FileSelection.Location = new System.Drawing.Point(14, 6);
            GroupBox_FileSelection.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            GroupBox_FileSelection.Name = "GroupBox_FileSelection";
            GroupBox_FileSelection.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            GroupBox_FileSelection.Size = new System.Drawing.Size(1142, 260);
            GroupBox_FileSelection.TabIndex = 19;
            GroupBox_FileSelection.TabStop = false;
            GroupBox_FileSelection.Text = "File Selection";
            // 
            // TabPage_TargetScheduler
            // 
            TabPage_TargetScheduler.BackColor = System.Drawing.SystemColors.Control;
            TabPage_TargetScheduler.Controls.Add(Panel_TargetScheduler);
            TabPage_TargetScheduler.Controls.Add(GroupBox_Project_Priority);
            TabPage_TargetScheduler.Controls.Add(Label_SchedulerTab_PlansText);
            TabPage_TargetScheduler.Controls.Add(Label_SchedulerTab_TargetsText);
            TabPage_TargetScheduler.Controls.Add(TreeView_SchedulerTab_TargetTree);
            TabPage_TargetScheduler.Controls.Add(Label_SchedulerTab_ProjectsText);
            TabPage_TargetScheduler.Controls.Add(Label_SchedulerTab_ProfilesText);
            TabPage_TargetScheduler.Controls.Add(TreeView_SchedulerTab_ProjectTree);
            TabPage_TargetScheduler.Controls.Add(TreeView_SchedulerTab_ProfileTree);
            TabPage_TargetScheduler.Controls.Add(Button_SchedulerTab_OpenDatabase);
            TabPage_TargetScheduler.Location = new System.Drawing.Point(4, 24);
            TabPage_TargetScheduler.Name = "TabPage_TargetScheduler";
            TabPage_TargetScheduler.Padding = new System.Windows.Forms.Padding(3);
            TabPage_TargetScheduler.Size = new System.Drawing.Size(1139, 507);
            TabPage_TargetScheduler.TabIndex = 3;
            TabPage_TargetScheduler.Text = "Target Scheduler";
            // 
            // Panel_TargetScheduler
            // 
            Panel_TargetScheduler.Location = new System.Drawing.Point(530, 37);
            Panel_TargetScheduler.Name = "Panel_TargetScheduler";
            Panel_TargetScheduler.Size = new System.Drawing.Size(584, 445);
            Panel_TargetScheduler.TabIndex = 9;
            // 
            // GroupBox_Project_Priority
            // 
            GroupBox_Project_Priority.Controls.Add(RadioButton_ProjectPriority_High);
            GroupBox_Project_Priority.Controls.Add(RadioButton_ProjectPriority_Normal);
            GroupBox_Project_Priority.Controls.Add(RadioButton_ProjectPriority_Low);
            GroupBox_Project_Priority.Location = new System.Drawing.Point(130, 459);
            GroupBox_Project_Priority.Name = "GroupBox_Project_Priority";
            GroupBox_Project_Priority.Size = new System.Drawing.Size(197, 45);
            GroupBox_Project_Priority.TabIndex = 0;
            GroupBox_Project_Priority.TabStop = false;
            GroupBox_Project_Priority.Text = "Priority";
            // 
            // RadioButton_ProjectPriority_High
            // 
            RadioButton_ProjectPriority_High.AutoSize = true;
            RadioButton_ProjectPriority_High.Location = new System.Drawing.Point(139, 18);
            RadioButton_ProjectPriority_High.Name = "RadioButton_ProjectPriority_High";
            RadioButton_ProjectPriority_High.Size = new System.Drawing.Size(51, 19);
            RadioButton_ProjectPriority_High.TabIndex = 2;
            RadioButton_ProjectPriority_High.TabStop = true;
            RadioButton_ProjectPriority_High.Text = "High";
            RadioButton_ProjectPriority_High.UseVisualStyleBackColor = true;
            // 
            // RadioButton_ProjectPriority_Normal
            // 
            RadioButton_ProjectPriority_Normal.AutoSize = true;
            RadioButton_ProjectPriority_Normal.Location = new System.Drawing.Point(67, 18);
            RadioButton_ProjectPriority_Normal.Name = "RadioButton_ProjectPriority_Normal";
            RadioButton_ProjectPriority_Normal.Size = new System.Drawing.Size(65, 19);
            RadioButton_ProjectPriority_Normal.TabIndex = 1;
            RadioButton_ProjectPriority_Normal.TabStop = true;
            RadioButton_ProjectPriority_Normal.Text = "Normal";
            RadioButton_ProjectPriority_Normal.UseVisualStyleBackColor = true;
            // 
            // RadioButton_ProjectPriority_Low
            // 
            RadioButton_ProjectPriority_Low.AutoSize = true;
            RadioButton_ProjectPriority_Low.Location = new System.Drawing.Point(13, 18);
            RadioButton_ProjectPriority_Low.Name = "RadioButton_ProjectPriority_Low";
            RadioButton_ProjectPriority_Low.Size = new System.Drawing.Size(47, 19);
            RadioButton_ProjectPriority_Low.TabIndex = 0;
            RadioButton_ProjectPriority_Low.TabStop = true;
            RadioButton_ProjectPriority_Low.Text = "Low";
            RadioButton_ProjectPriority_Low.UseVisualStyleBackColor = true;
            // 
            // Label_SchedulerTab_PlansText
            // 
            Label_SchedulerTab_PlansText.AutoSize = true;
            Label_SchedulerTab_PlansText.Location = new System.Drawing.Point(590, 21);
            Label_SchedulerTab_PlansText.Name = "Label_SchedulerTab_PlansText";
            Label_SchedulerTab_PlansText.Size = new System.Drawing.Size(85, 15);
            Label_SchedulerTab_PlansText.TabIndex = 8;
            Label_SchedulerTab_PlansText.Text = "Exposure Plans";
            // 
            // Label_SchedulerTab_TargetsText
            // 
            Label_SchedulerTab_TargetsText.AutoSize = true;
            Label_SchedulerTab_TargetsText.Location = new System.Drawing.Point(403, 21);
            Label_SchedulerTab_TargetsText.Name = "Label_SchedulerTab_TargetsText";
            Label_SchedulerTab_TargetsText.Size = new System.Drawing.Size(45, 15);
            Label_SchedulerTab_TargetsText.TabIndex = 6;
            Label_SchedulerTab_TargetsText.Text = "Targets";
            // 
            // TreeView_SchedulerTab_TargetTree
            // 
            TreeView_SchedulerTab_TargetTree.CheckBoxes = true;
            TreeView_SchedulerTab_TargetTree.Location = new System.Drawing.Point(332, 38);
            TreeView_SchedulerTab_TargetTree.Name = "TreeView_SchedulerTab_TargetTree";
            TreeView_SchedulerTab_TargetTree.Size = new System.Drawing.Size(187, 444);
            TreeView_SchedulerTab_TargetTree.TabIndex = 5;
            // 
            // Label_SchedulerTab_ProjectsText
            // 
            Label_SchedulerTab_ProjectsText.AutoSize = true;
            Label_SchedulerTab_ProjectsText.Location = new System.Drawing.Point(204, 21);
            Label_SchedulerTab_ProjectsText.Name = "Label_SchedulerTab_ProjectsText";
            Label_SchedulerTab_ProjectsText.Size = new System.Drawing.Size(49, 15);
            Label_SchedulerTab_ProjectsText.TabIndex = 4;
            Label_SchedulerTab_ProjectsText.Text = "Projects";
            // 
            // Label_SchedulerTab_ProfilesText
            // 
            Label_SchedulerTab_ProfilesText.AutoSize = true;
            Label_SchedulerTab_ProfilesText.Location = new System.Drawing.Point(44, 72);
            Label_SchedulerTab_ProfilesText.Name = "Label_SchedulerTab_ProfilesText";
            Label_SchedulerTab_ProfilesText.Size = new System.Drawing.Size(46, 15);
            Label_SchedulerTab_ProfilesText.TabIndex = 3;
            Label_SchedulerTab_ProfilesText.Text = "Profiles";
            // 
            // TreeView_SchedulerTab_ProjectTree
            // 
            TreeView_SchedulerTab_ProjectTree.CheckBoxes = true;
            TreeView_SchedulerTab_ProjectTree.Indent = 19;
            TreeView_SchedulerTab_ProjectTree.Location = new System.Drawing.Point(135, 38);
            TreeView_SchedulerTab_ProjectTree.Name = "TreeView_SchedulerTab_ProjectTree";
            TreeView_SchedulerTab_ProjectTree.Size = new System.Drawing.Size(187, 415);
            TreeView_SchedulerTab_ProjectTree.TabIndex = 2;
            TreeView_SchedulerTab_ProjectTree.DrawNode += TreeView_SchedulerTab_ProjectTree_DrawNode;
            TreeView_SchedulerTab_ProjectTree.Click += TreeView_SchedulerTab_ProjectTree_Click;
            // 
            // TreeView_SchedulerTab_ProfileTree
            // 
            TreeView_SchedulerTab_ProfileTree.CheckBoxes = true;
            TreeView_SchedulerTab_ProfileTree.Location = new System.Drawing.Point(10, 89);
            TreeView_SchedulerTab_ProfileTree.Name = "TreeView_SchedulerTab_ProfileTree";
            TreeView_SchedulerTab_ProfileTree.Size = new System.Drawing.Size(115, 141);
            TreeView_SchedulerTab_ProfileTree.TabIndex = 1;
            // 
            // Button_SchedulerTab_OpenDatabase
            // 
            Button_SchedulerTab_OpenDatabase.Location = new System.Drawing.Point(10, 15);
            Button_SchedulerTab_OpenDatabase.Name = "Button_SchedulerTab_OpenDatabase";
            Button_SchedulerTab_OpenDatabase.Size = new System.Drawing.Size(115, 40);
            Button_SchedulerTab_OpenDatabase.TabIndex = 0;
            Button_SchedulerTab_OpenDatabase.Text = "Open Scheduler Database";
            Button_SchedulerTab_OpenDatabase.UseVisualStyleBackColor = true;
            Button_SchedulerTab_OpenDatabase.Click += Button_SchedulerTab_OpenDatabase_Click;
            // 
            // TabPage_Calibration
            // 
            TabPage_Calibration.BackColor = System.Drawing.SystemColors.Control;
            TabPage_Calibration.Controls.Add(groupBox1);
            TabPage_Calibration.Controls.Add(CheckBox_CalibrationTab_CreateNew);
            TabPage_Calibration.Controls.Add(TreeView_CalibrationTab_TargetFileTree);
            TabPage_Calibration.Controls.Add(TextBox_CalibrationTab_Messgaes);
            TabPage_Calibration.Controls.Add(GroupBox_CalibrationTab_MatchingTolerance);
            TabPage_Calibration.Controls.Add(Label_CalibrationTab_TotalFiles);
            TabPage_Calibration.Controls.Add(ProgressBar_CalibrationTab);
            TabPage_Calibration.Controls.Add(Label_CalibrationTab_ReadFileName);
            TabPage_Calibration.Controls.Add(Button_CalibrationTab_CreateCalibrationDirectory);
            TabPage_Calibration.Controls.Add(Button_CalibrationTab_MatchCalibrationFrames);
            TabPage_Calibration.Controls.Add(Button_CalibrationTab_FindCalibrationFrames);
            TabPage_Calibration.Location = new System.Drawing.Point(4, 24);
            TabPage_Calibration.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            TabPage_Calibration.Name = "TabPage_Calibration";
            TabPage_Calibration.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            TabPage_Calibration.Size = new System.Drawing.Size(1139, 507);
            TabPage_Calibration.TabIndex = 1;
            TabPage_Calibration.Text = "Calibration";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(Label_CalibrationTab_Minimum);
            groupBox1.Controls.Add(Button_CalibrationTab_FindPedestal);
            groupBox1.Controls.Add(Label_CalibrationTab_Pedestal);
            groupBox1.Controls.Add(NumericUpDown_CalibrationTab_MinBackground);
            groupBox1.Location = new System.Drawing.Point(231, 187);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new System.Drawing.Size(301, 83);
            groupBox1.TabIndex = 17;
            groupBox1.TabStop = false;
            groupBox1.Text = "Background";
            // 
            // Label_CalibrationTab_Minimum
            // 
            Label_CalibrationTab_Minimum.AutoSize = true;
            Label_CalibrationTab_Minimum.Location = new System.Drawing.Point(37, 29);
            Label_CalibrationTab_Minimum.Name = "Label_CalibrationTab_Minimum";
            Label_CalibrationTab_Minimum.Size = new System.Drawing.Size(63, 15);
            Label_CalibrationTab_Minimum.TabIndex = 16;
            Label_CalibrationTab_Minimum.Text = "Minimum:";
            // 
            // Button_CalibrationTab_FindPedestal
            // 
            Button_CalibrationTab_FindPedestal.Location = new System.Drawing.Point(187, 25);
            Button_CalibrationTab_FindPedestal.Name = "Button_CalibrationTab_FindPedestal";
            Button_CalibrationTab_FindPedestal.Size = new System.Drawing.Size(75, 23);
            Button_CalibrationTab_FindPedestal.TabIndex = 12;
            Button_CalibrationTab_FindPedestal.Text = "Find Pedestal";
            Button_CalibrationTab_FindPedestal.UseVisualStyleBackColor = true;
            Button_CalibrationTab_FindPedestal.Click += Button_CalibrationTab_FindPedestal_Click;
            // 
            // Label_CalibrationTab_Pedestal
            // 
            Label_CalibrationTab_Pedestal.AutoSize = true;
            Label_CalibrationTab_Pedestal.Location = new System.Drawing.Point(80, 56);
            Label_CalibrationTab_Pedestal.Name = "Label_CalibrationTab_Pedestal";
            Label_CalibrationTab_Pedestal.Size = new System.Drawing.Size(113, 15);
            Label_CalibrationTab_Pedestal.TabIndex = 14;
            Label_CalibrationTab_Pedestal.Text = "Required Pedestal: 0";
            // 
            // NumericUpDown_CalibrationTab_MinBackground
            // 
            NumericUpDown_CalibrationTab_MinBackground.Increment = new decimal(new int[] { 16, 0, 0, 0 });
            NumericUpDown_CalibrationTab_MinBackground.Location = new System.Drawing.Point(107, 25);
            NumericUpDown_CalibrationTab_MinBackground.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            NumericUpDown_CalibrationTab_MinBackground.Name = "NumericUpDown_CalibrationTab_MinBackground";
            NumericUpDown_CalibrationTab_MinBackground.Size = new System.Drawing.Size(70, 23);
            NumericUpDown_CalibrationTab_MinBackground.TabIndex = 13;
            NumericUpDown_CalibrationTab_MinBackground.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            NumericUpDown_CalibrationTab_MinBackground.ThousandsSeparator = true;
            // 
            // CheckBox_CalibrationTab_CreateNew
            // 
            CheckBox_CalibrationTab_CreateNew.AutoSize = true;
            CheckBox_CalibrationTab_CreateNew.Checked = true;
            CheckBox_CalibrationTab_CreateNew.CheckState = System.Windows.Forms.CheckState.Checked;
            CheckBox_CalibrationTab_CreateNew.Location = new System.Drawing.Point(12, 211);
            CheckBox_CalibrationTab_CreateNew.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            CheckBox_CalibrationTab_CreateNew.Name = "CheckBox_CalibrationTab_CreateNew";
            CheckBox_CalibrationTab_CreateNew.Size = new System.Drawing.Size(60, 19);
            CheckBox_CalibrationTab_CreateNew.TabIndex = 11;
            CheckBox_CalibrationTab_CreateNew.Text = "Create";
            CheckBox_CalibrationTab_CreateNew.UseVisualStyleBackColor = true;
            // 
            // TreeView_CalibrationTab_TargetFileTree
            // 
            TreeView_CalibrationTab_TargetFileTree.Location = new System.Drawing.Point(575, 6);
            TreeView_CalibrationTab_TargetFileTree.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            TreeView_CalibrationTab_TargetFileTree.Name = "TreeView_CalibrationTab_TargetFileTree";
            TreeView_CalibrationTab_TargetFileTree.Size = new System.Drawing.Size(514, 257);
            TreeView_CalibrationTab_TargetFileTree.TabIndex = 10;
            // 
            // TextBox_CalibrationTab_Messgaes
            // 
            TextBox_CalibrationTab_Messgaes.Location = new System.Drawing.Point(25, 274);
            TextBox_CalibrationTab_Messgaes.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            TextBox_CalibrationTab_Messgaes.Multiline = true;
            TextBox_CalibrationTab_Messgaes.Name = "TextBox_CalibrationTab_Messgaes";
            TextBox_CalibrationTab_Messgaes.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            TextBox_CalibrationTab_Messgaes.Size = new System.Drawing.Size(1084, 161);
            TextBox_CalibrationTab_Messgaes.TabIndex = 8;
            // 
            // GroupBox_CalibrationTab_MatchingTolerance
            // 
            GroupBox_CalibrationTab_MatchingTolerance.Controls.Add(CheckBox_CalibrationTab_MatchingTolerance_TemperatureNearest);
            GroupBox_CalibrationTab_MatchingTolerance.Controls.Add(CheckBox_CalibrationTab_MatchingTolerance_OffsetNearest);
            GroupBox_CalibrationTab_MatchingTolerance.Controls.Add(CheckBox_CalibrationTab_MatchingTolerance_GainNearest);
            GroupBox_CalibrationTab_MatchingTolerance.Controls.Add(CheckBox_CalibrationTab_MatchingTolerance_ExposureNearest);
            GroupBox_CalibrationTab_MatchingTolerance.Controls.Add(Label_CalibrationTab_MatchingTolerance_TemperatureDegrees);
            GroupBox_CalibrationTab_MatchingTolerance.Controls.Add(Label_CalibrationTab_MatchingTolerance_OffsetADU);
            GroupBox_CalibrationTab_MatchingTolerance.Controls.Add(Label_CalibrationTab_MatchingTolerance_GainUnits);
            GroupBox_CalibrationTab_MatchingTolerance.Controls.Add(Label_CalibrationTab_MatchingTolerance_ExposureSeconds);
            GroupBox_CalibrationTab_MatchingTolerance.Controls.Add(Label_CalibrationTab_MatchingTolerance_Percentage);
            GroupBox_CalibrationTab_MatchingTolerance.Controls.Add(TextBox_CalibrationTab_MatchingTolerance_Temperature);
            GroupBox_CalibrationTab_MatchingTolerance.Controls.Add(TextBox_CalibrationTab_MatchingTolerance_Offset);
            GroupBox_CalibrationTab_MatchingTolerance.Controls.Add(TextBox_CalibrationTab_MatchingTolerance_Gain);
            GroupBox_CalibrationTab_MatchingTolerance.Controls.Add(Label_CalibrationTab_MatchingTolerance_Temperature);
            GroupBox_CalibrationTab_MatchingTolerance.Controls.Add(Label_CalibrationTab_MatchingTolerance_Offset);
            GroupBox_CalibrationTab_MatchingTolerance.Controls.Add(Label_CalibrationTab_MatchingTolerance_Gain);
            GroupBox_CalibrationTab_MatchingTolerance.Controls.Add(Label_CalibrationTab_MatchingTolerance_Exposure);
            GroupBox_CalibrationTab_MatchingTolerance.Controls.Add(TextBox_CalibrationTab_MatchingTolerance_Exposure);
            GroupBox_CalibrationTab_MatchingTolerance.Location = new System.Drawing.Point(231, 5);
            GroupBox_CalibrationTab_MatchingTolerance.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            GroupBox_CalibrationTab_MatchingTolerance.Name = "GroupBox_CalibrationTab_MatchingTolerance";
            GroupBox_CalibrationTab_MatchingTolerance.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            GroupBox_CalibrationTab_MatchingTolerance.Size = new System.Drawing.Size(303, 180);
            GroupBox_CalibrationTab_MatchingTolerance.TabIndex = 7;
            GroupBox_CalibrationTab_MatchingTolerance.TabStop = false;
            GroupBox_CalibrationTab_MatchingTolerance.Text = "Match Tolerance";
            // 
            // CheckBox_CalibrationTab_MatchingTolerance_TemperatureNearest
            // 
            CheckBox_CalibrationTab_MatchingTolerance_TemperatureNearest.AutoSize = true;
            CheckBox_CalibrationTab_MatchingTolerance_TemperatureNearest.Location = new System.Drawing.Point(219, 138);
            CheckBox_CalibrationTab_MatchingTolerance_TemperatureNearest.Name = "CheckBox_CalibrationTab_MatchingTolerance_TemperatureNearest";
            CheckBox_CalibrationTab_MatchingTolerance_TemperatureNearest.Size = new System.Drawing.Size(66, 19);
            CheckBox_CalibrationTab_MatchingTolerance_TemperatureNearest.TabIndex = 16;
            CheckBox_CalibrationTab_MatchingTolerance_TemperatureNearest.Text = "Nearest";
            CheckBox_CalibrationTab_MatchingTolerance_TemperatureNearest.UseVisualStyleBackColor = true;
            CheckBox_CalibrationTab_MatchingTolerance_TemperatureNearest.CheckedChanged += CheckBox_CalibrationTab_MatchingTolerance_TemperatureNearest_CheckedChanged;
            // 
            // CheckBox_CalibrationTab_MatchingTolerance_OffsetNearest
            // 
            CheckBox_CalibrationTab_MatchingTolerance_OffsetNearest.AutoSize = true;
            CheckBox_CalibrationTab_MatchingTolerance_OffsetNearest.Location = new System.Drawing.Point(219, 109);
            CheckBox_CalibrationTab_MatchingTolerance_OffsetNearest.Name = "CheckBox_CalibrationTab_MatchingTolerance_OffsetNearest";
            CheckBox_CalibrationTab_MatchingTolerance_OffsetNearest.Size = new System.Drawing.Size(66, 19);
            CheckBox_CalibrationTab_MatchingTolerance_OffsetNearest.TabIndex = 15;
            CheckBox_CalibrationTab_MatchingTolerance_OffsetNearest.Text = "Nearest";
            CheckBox_CalibrationTab_MatchingTolerance_OffsetNearest.UseVisualStyleBackColor = true;
            CheckBox_CalibrationTab_MatchingTolerance_OffsetNearest.CheckedChanged += CheckBox_CalibrationTab_MatchingTolerance_OffsetNearest_CheckedChanged;
            // 
            // CheckBox_CalibrationTab_MatchingTolerance_GainNearest
            // 
            CheckBox_CalibrationTab_MatchingTolerance_GainNearest.AutoSize = true;
            CheckBox_CalibrationTab_MatchingTolerance_GainNearest.Location = new System.Drawing.Point(219, 81);
            CheckBox_CalibrationTab_MatchingTolerance_GainNearest.Name = "CheckBox_CalibrationTab_MatchingTolerance_GainNearest";
            CheckBox_CalibrationTab_MatchingTolerance_GainNearest.Size = new System.Drawing.Size(66, 19);
            CheckBox_CalibrationTab_MatchingTolerance_GainNearest.TabIndex = 14;
            CheckBox_CalibrationTab_MatchingTolerance_GainNearest.Text = "Nearest";
            CheckBox_CalibrationTab_MatchingTolerance_GainNearest.UseVisualStyleBackColor = true;
            CheckBox_CalibrationTab_MatchingTolerance_GainNearest.CheckedChanged += CheckBox_CalibrationTab_MatchingTolerance_GainNearest_CheckedChanged;
            // 
            // CheckBox_CalibrationTab_MatchingTolerance_ExposureNearest
            // 
            CheckBox_CalibrationTab_MatchingTolerance_ExposureNearest.AutoSize = true;
            CheckBox_CalibrationTab_MatchingTolerance_ExposureNearest.Location = new System.Drawing.Point(219, 52);
            CheckBox_CalibrationTab_MatchingTolerance_ExposureNearest.Name = "CheckBox_CalibrationTab_MatchingTolerance_ExposureNearest";
            CheckBox_CalibrationTab_MatchingTolerance_ExposureNearest.Size = new System.Drawing.Size(66, 19);
            CheckBox_CalibrationTab_MatchingTolerance_ExposureNearest.TabIndex = 13;
            CheckBox_CalibrationTab_MatchingTolerance_ExposureNearest.Text = "Nearest";
            CheckBox_CalibrationTab_MatchingTolerance_ExposureNearest.UseVisualStyleBackColor = true;
            CheckBox_CalibrationTab_MatchingTolerance_ExposureNearest.CheckedChanged += CheckBox_CalibrationTab_MatchingTolerance_ExposureNearest_CheckedChanged;
            // 
            // Label_CalibrationTab_MatchingTolerance_TemperatureDegrees
            // 
            Label_CalibrationTab_MatchingTolerance_TemperatureDegrees.AutoSize = true;
            Label_CalibrationTab_MatchingTolerance_TemperatureDegrees.Location = new System.Drawing.Point(159, 140);
            Label_CalibrationTab_MatchingTolerance_TemperatureDegrees.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            Label_CalibrationTab_MatchingTolerance_TemperatureDegrees.Name = "Label_CalibrationTab_MatchingTolerance_TemperatureDegrees";
            Label_CalibrationTab_MatchingTolerance_TemperatureDegrees.Size = new System.Drawing.Size(49, 15);
            Label_CalibrationTab_MatchingTolerance_TemperatureDegrees.TabIndex = 12;
            Label_CalibrationTab_MatchingTolerance_TemperatureDegrees.Text = "Degrees";
            // 
            // Label_CalibrationTab_MatchingTolerance_OffsetADU
            // 
            Label_CalibrationTab_MatchingTolerance_OffsetADU.AutoSize = true;
            Label_CalibrationTab_MatchingTolerance_OffsetADU.Location = new System.Drawing.Point(159, 111);
            Label_CalibrationTab_MatchingTolerance_OffsetADU.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            Label_CalibrationTab_MatchingTolerance_OffsetADU.Name = "Label_CalibrationTab_MatchingTolerance_OffsetADU";
            Label_CalibrationTab_MatchingTolerance_OffsetADU.Size = new System.Drawing.Size(31, 15);
            Label_CalibrationTab_MatchingTolerance_OffsetADU.TabIndex = 11;
            Label_CalibrationTab_MatchingTolerance_OffsetADU.Text = "ADU";
            // 
            // Label_CalibrationTab_MatchingTolerance_GainUnits
            // 
            Label_CalibrationTab_MatchingTolerance_GainUnits.AutoSize = true;
            Label_CalibrationTab_MatchingTolerance_GainUnits.Location = new System.Drawing.Point(159, 83);
            Label_CalibrationTab_MatchingTolerance_GainUnits.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            Label_CalibrationTab_MatchingTolerance_GainUnits.Name = "Label_CalibrationTab_MatchingTolerance_GainUnits";
            Label_CalibrationTab_MatchingTolerance_GainUnits.Size = new System.Drawing.Size(34, 15);
            Label_CalibrationTab_MatchingTolerance_GainUnits.TabIndex = 10;
            Label_CalibrationTab_MatchingTolerance_GainUnits.Text = "Units";
            // 
            // Label_CalibrationTab_MatchingTolerance_ExposureSeconds
            // 
            Label_CalibrationTab_MatchingTolerance_ExposureSeconds.AutoSize = true;
            Label_CalibrationTab_MatchingTolerance_ExposureSeconds.Location = new System.Drawing.Point(159, 54);
            Label_CalibrationTab_MatchingTolerance_ExposureSeconds.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            Label_CalibrationTab_MatchingTolerance_ExposureSeconds.Name = "Label_CalibrationTab_MatchingTolerance_ExposureSeconds";
            Label_CalibrationTab_MatchingTolerance_ExposureSeconds.Size = new System.Drawing.Size(51, 15);
            Label_CalibrationTab_MatchingTolerance_ExposureSeconds.TabIndex = 9;
            Label_CalibrationTab_MatchingTolerance_ExposureSeconds.Text = "Seconds";
            // 
            // Label_CalibrationTab_MatchingTolerance_Percentage
            // 
            Label_CalibrationTab_MatchingTolerance_Percentage.AutoSize = true;
            Label_CalibrationTab_MatchingTolerance_Percentage.Location = new System.Drawing.Point(88, 26);
            Label_CalibrationTab_MatchingTolerance_Percentage.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            Label_CalibrationTab_MatchingTolerance_Percentage.Name = "Label_CalibrationTab_MatchingTolerance_Percentage";
            Label_CalibrationTab_MatchingTolerance_Percentage.Size = new System.Drawing.Size(79, 15);
            Label_CalibrationTab_MatchingTolerance_Percentage.TabIndex = 8;
            Label_CalibrationTab_MatchingTolerance_Percentage.Text = "Match Within";
            // 
            // TextBox_CalibrationTab_MatchingTolerance_Temperature
            // 
            TextBox_CalibrationTab_MatchingTolerance_Temperature.Location = new System.Drawing.Point(105, 136);
            TextBox_CalibrationTab_MatchingTolerance_Temperature.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            TextBox_CalibrationTab_MatchingTolerance_Temperature.Name = "TextBox_CalibrationTab_MatchingTolerance_Temperature";
            TextBox_CalibrationTab_MatchingTolerance_Temperature.Size = new System.Drawing.Size(48, 23);
            TextBox_CalibrationTab_MatchingTolerance_Temperature.TabIndex = 7;
            TextBox_CalibrationTab_MatchingTolerance_Temperature.Text = "5";
            TextBox_CalibrationTab_MatchingTolerance_Temperature.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            TextBox_CalibrationTab_MatchingTolerance_Temperature.TextChanged += TextBox_CalibrationTab_TemperatureTolerance_TextChanged;
            // 
            // TextBox_CalibrationTab_MatchingTolerance_Offset
            // 
            TextBox_CalibrationTab_MatchingTolerance_Offset.Location = new System.Drawing.Point(105, 107);
            TextBox_CalibrationTab_MatchingTolerance_Offset.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            TextBox_CalibrationTab_MatchingTolerance_Offset.Name = "TextBox_CalibrationTab_MatchingTolerance_Offset";
            TextBox_CalibrationTab_MatchingTolerance_Offset.Size = new System.Drawing.Size(48, 23);
            TextBox_CalibrationTab_MatchingTolerance_Offset.TabIndex = 6;
            TextBox_CalibrationTab_MatchingTolerance_Offset.Text = "0";
            TextBox_CalibrationTab_MatchingTolerance_Offset.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            TextBox_CalibrationTab_MatchingTolerance_Offset.TextChanged += TextBox_CalibrationTab_OffsetTolerance_TextChanged;
            // 
            // TextBox_CalibrationTab_MatchingTolerance_Gain
            // 
            TextBox_CalibrationTab_MatchingTolerance_Gain.Location = new System.Drawing.Point(105, 79);
            TextBox_CalibrationTab_MatchingTolerance_Gain.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            TextBox_CalibrationTab_MatchingTolerance_Gain.Name = "TextBox_CalibrationTab_MatchingTolerance_Gain";
            TextBox_CalibrationTab_MatchingTolerance_Gain.Size = new System.Drawing.Size(48, 23);
            TextBox_CalibrationTab_MatchingTolerance_Gain.TabIndex = 5;
            TextBox_CalibrationTab_MatchingTolerance_Gain.Text = "0";
            TextBox_CalibrationTab_MatchingTolerance_Gain.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            TextBox_CalibrationTab_MatchingTolerance_Gain.TextChanged += TextBox_CalibrationTab_GainTolerance_TextChanged;
            // 
            // Label_CalibrationTab_MatchingTolerance_Temperature
            // 
            Label_CalibrationTab_MatchingTolerance_Temperature.AutoSize = true;
            Label_CalibrationTab_MatchingTolerance_Temperature.Location = new System.Drawing.Point(24, 140);
            Label_CalibrationTab_MatchingTolerance_Temperature.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            Label_CalibrationTab_MatchingTolerance_Temperature.Name = "Label_CalibrationTab_MatchingTolerance_Temperature";
            Label_CalibrationTab_MatchingTolerance_Temperature.Size = new System.Drawing.Size(74, 15);
            Label_CalibrationTab_MatchingTolerance_Temperature.TabIndex = 4;
            Label_CalibrationTab_MatchingTolerance_Temperature.Text = "Temperature";
            // 
            // Label_CalibrationTab_MatchingTolerance_Offset
            // 
            Label_CalibrationTab_MatchingTolerance_Offset.AutoSize = true;
            Label_CalibrationTab_MatchingTolerance_Offset.Location = new System.Drawing.Point(24, 111);
            Label_CalibrationTab_MatchingTolerance_Offset.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            Label_CalibrationTab_MatchingTolerance_Offset.Name = "Label_CalibrationTab_MatchingTolerance_Offset";
            Label_CalibrationTab_MatchingTolerance_Offset.Size = new System.Drawing.Size(39, 15);
            Label_CalibrationTab_MatchingTolerance_Offset.TabIndex = 3;
            Label_CalibrationTab_MatchingTolerance_Offset.Text = "Offset";
            // 
            // Label_CalibrationTab_MatchingTolerance_Gain
            // 
            Label_CalibrationTab_MatchingTolerance_Gain.AutoSize = true;
            Label_CalibrationTab_MatchingTolerance_Gain.Location = new System.Drawing.Point(24, 83);
            Label_CalibrationTab_MatchingTolerance_Gain.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            Label_CalibrationTab_MatchingTolerance_Gain.Name = "Label_CalibrationTab_MatchingTolerance_Gain";
            Label_CalibrationTab_MatchingTolerance_Gain.Size = new System.Drawing.Size(31, 15);
            Label_CalibrationTab_MatchingTolerance_Gain.TabIndex = 2;
            Label_CalibrationTab_MatchingTolerance_Gain.Text = "Gain";
            // 
            // Label_CalibrationTab_MatchingTolerance_Exposure
            // 
            Label_CalibrationTab_MatchingTolerance_Exposure.AutoSize = true;
            Label_CalibrationTab_MatchingTolerance_Exposure.Location = new System.Drawing.Point(24, 54);
            Label_CalibrationTab_MatchingTolerance_Exposure.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            Label_CalibrationTab_MatchingTolerance_Exposure.Name = "Label_CalibrationTab_MatchingTolerance_Exposure";
            Label_CalibrationTab_MatchingTolerance_Exposure.Size = new System.Drawing.Size(54, 15);
            Label_CalibrationTab_MatchingTolerance_Exposure.TabIndex = 1;
            Label_CalibrationTab_MatchingTolerance_Exposure.Text = "Exposure";
            // 
            // TextBox_CalibrationTab_MatchingTolerance_Exposure
            // 
            TextBox_CalibrationTab_MatchingTolerance_Exposure.Location = new System.Drawing.Point(105, 50);
            TextBox_CalibrationTab_MatchingTolerance_Exposure.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            TextBox_CalibrationTab_MatchingTolerance_Exposure.Name = "TextBox_CalibrationTab_MatchingTolerance_Exposure";
            TextBox_CalibrationTab_MatchingTolerance_Exposure.Size = new System.Drawing.Size(48, 23);
            TextBox_CalibrationTab_MatchingTolerance_Exposure.TabIndex = 0;
            TextBox_CalibrationTab_MatchingTolerance_Exposure.Text = "0";
            TextBox_CalibrationTab_MatchingTolerance_Exposure.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            TextBox_CalibrationTab_MatchingTolerance_Exposure.TextChanged += TextBox_CalibrationTab_ExposureTolerance_TextChanged;
            // 
            // Label_CalibrationTab_TotalFiles
            // 
            Label_CalibrationTab_TotalFiles.AutoSize = true;
            Label_CalibrationTab_TotalFiles.Location = new System.Drawing.Point(56, 8);
            Label_CalibrationTab_TotalFiles.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            Label_CalibrationTab_TotalFiles.Name = "Label_CalibrationTab_TotalFiles";
            Label_CalibrationTab_TotalFiles.Size = new System.Drawing.Size(125, 15);
            Label_CalibrationTab_TotalFiles.TabIndex = 6;
            Label_CalibrationTab_TotalFiles.Text = "No Calibration Frames";
            Label_CalibrationTab_TotalFiles.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ProgressBar_CalibrationTab
            // 
            ProgressBar_CalibrationTab.Location = new System.Drawing.Point(20, 476);
            ProgressBar_CalibrationTab.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            ProgressBar_CalibrationTab.Name = "ProgressBar_CalibrationTab";
            ProgressBar_CalibrationTab.Size = new System.Drawing.Size(1099, 13);
            ProgressBar_CalibrationTab.TabIndex = 5;
            // 
            // Label_CalibrationTab_ReadFileName
            // 
            Label_CalibrationTab_ReadFileName.AutoSize = true;
            Label_CalibrationTab_ReadFileName.Location = new System.Drawing.Point(16, 440);
            Label_CalibrationTab_ReadFileName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            Label_CalibrationTab_ReadFileName.Name = "Label_CalibrationTab_ReadFileName";
            Label_CalibrationTab_ReadFileName.Size = new System.Drawing.Size(121, 15);
            Label_CalibrationTab_ReadFileName.TabIndex = 4;
            Label_CalibrationTab_ReadFileName.Text = "Calibration File Name";
            // 
            // Button_CalibrationTab_CreateCalibrationDirectory
            // 
            Button_CalibrationTab_CreateCalibrationDirectory.Location = new System.Drawing.Point(74, 182);
            Button_CalibrationTab_CreateCalibrationDirectory.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Button_CalibrationTab_CreateCalibrationDirectory.Name = "Button_CalibrationTab_CreateCalibrationDirectory";
            Button_CalibrationTab_CreateCalibrationDirectory.Size = new System.Drawing.Size(88, 76);
            Button_CalibrationTab_CreateCalibrationDirectory.TabIndex = 2;
            Button_CalibrationTab_CreateCalibrationDirectory.Text = "Create Target Calibration Directory";
            Button_CalibrationTab_CreateCalibrationDirectory.UseVisualStyleBackColor = true;
            Button_CalibrationTab_CreateCalibrationDirectory.Click += CalibrationTab_CreateCalibrationDirectory_Click;
            // 
            // Button_CalibrationTab_MatchCalibrationFrames
            // 
            Button_CalibrationTab_MatchCalibrationFrames.Location = new System.Drawing.Point(74, 104);
            Button_CalibrationTab_MatchCalibrationFrames.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Button_CalibrationTab_MatchCalibrationFrames.Name = "Button_CalibrationTab_MatchCalibrationFrames";
            Button_CalibrationTab_MatchCalibrationFrames.Size = new System.Drawing.Size(88, 76);
            Button_CalibrationTab_MatchCalibrationFrames.TabIndex = 1;
            Button_CalibrationTab_MatchCalibrationFrames.Text = "ReMatch Calibration Frames";
            Button_CalibrationTab_MatchCalibrationFrames.UseVisualStyleBackColor = true;
            Button_CalibrationTab_MatchCalibrationFrames.Click += CalibrationTab_ReMatchCalibrationFrames_Click;
            // 
            // Button_CalibrationTab_FindCalibrationFrames
            // 
            Button_CalibrationTab_FindCalibrationFrames.Location = new System.Drawing.Point(74, 28);
            Button_CalibrationTab_FindCalibrationFrames.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Button_CalibrationTab_FindCalibrationFrames.Name = "Button_CalibrationTab_FindCalibrationFrames";
            Button_CalibrationTab_FindCalibrationFrames.Size = new System.Drawing.Size(88, 76);
            Button_CalibrationTab_FindCalibrationFrames.TabIndex = 0;
            Button_CalibrationTab_FindCalibrationFrames.Text = "Find Calibration Frames";
            Button_CalibrationTab_FindCalibrationFrames.UseVisualStyleBackColor = true;
            Button_CalibrationTab_FindCalibrationFrames.Click += CalibrationTab_FindCalibrationFrames_Click;
            // 
            // TabPage_Keywords
            // 
            TabPage_Keywords.BackColor = System.Drawing.SystemColors.Control;
            TabPage_Keywords.Controls.Add(Button_KeywordUpdateTab_Cancel);
            TabPage_Keywords.Controls.Add(Label_KeywordUpdateTab_FileName);
            TabPage_Keywords.Controls.Add(ProgressBar_KeywordUpdateTab_WriteProgress);
            TabPage_Keywords.Controls.Add(GroupBox_KeywordUpdateTab_CaptureSoftware);
            TabPage_Keywords.Controls.Add(GroupBox_KeywordUpdateTab_Telescope);
            TabPage_Keywords.Controls.Add(GroupBox_KeywordUpdateTab_SubFrameKeywords);
            TabPage_Keywords.Controls.Add(GroupBox_KeywordUpdateTab_Camera);
            TabPage_Keywords.Controls.Add(GroupBox_KeywordUpdateTab_ImageType);
            TabPage_Keywords.Location = new System.Drawing.Point(4, 24);
            TabPage_Keywords.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            TabPage_Keywords.Name = "TabPage_Keywords";
            TabPage_Keywords.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            TabPage_Keywords.Size = new System.Drawing.Size(1139, 507);
            TabPage_Keywords.TabIndex = 0;
            TabPage_Keywords.Text = "Keywords";
            // 
            // Button_KeywordUpdateTab_Cancel
            // 
            Button_KeywordUpdateTab_Cancel.Location = new System.Drawing.Point(1023, 441);
            Button_KeywordUpdateTab_Cancel.Name = "Button_KeywordUpdateTab_Cancel";
            Button_KeywordUpdateTab_Cancel.Size = new System.Drawing.Size(88, 27);
            Button_KeywordUpdateTab_Cancel.TabIndex = 23;
            Button_KeywordUpdateTab_Cancel.Text = "Cancel";
            Button_KeywordUpdateTab_Cancel.UseVisualStyleBackColor = true;
            Button_KeywordUpdateTab_Cancel.Click += Button_KeywordUpdateTab_Cancel_Click;
            // 
            // Label_KeywordUpdateTab_FileName
            // 
            Label_KeywordUpdateTab_FileName.AutoSize = true;
            Label_KeywordUpdateTab_FileName.Location = new System.Drawing.Point(20, 441);
            Label_KeywordUpdateTab_FileName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            Label_KeywordUpdateTab_FileName.Name = "Label_KeywordUpdateTab_FileName";
            Label_KeywordUpdateTab_FileName.Size = new System.Drawing.Size(77, 15);
            Label_KeywordUpdateTab_FileName.TabIndex = 19;
            Label_KeywordUpdateTab_FileName.Text = "Updating File";
            // 
            // ProgressBar_KeywordUpdateTab_WriteProgress
            // 
            ProgressBar_KeywordUpdateTab_WriteProgress.Location = new System.Drawing.Point(20, 479);
            ProgressBar_KeywordUpdateTab_WriteProgress.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            ProgressBar_KeywordUpdateTab_WriteProgress.Name = "ProgressBar_KeywordUpdateTab_WriteProgress";
            ProgressBar_KeywordUpdateTab_WriteProgress.Size = new System.Drawing.Size(1094, 13);
            ProgressBar_KeywordUpdateTab_WriteProgress.Step = 1;
            ProgressBar_KeywordUpdateTab_WriteProgress.TabIndex = 13;
            // 
            // GroupBox_KeywordUpdateTab_CaptureSoftware
            // 
            GroupBox_KeywordUpdateTab_CaptureSoftware.Controls.Add(RadioButton_KeywordUpdateTab_CaptureSoftware_NINA);
            GroupBox_KeywordUpdateTab_CaptureSoftware.Controls.Add(Button_KeywordUpdateTab_CaptureSoftware_SetByFile);
            GroupBox_KeywordUpdateTab_CaptureSoftware.Controls.Add(Button_KeywordUpdateTab_CaptureSoftware_SetAll);
            GroupBox_KeywordUpdateTab_CaptureSoftware.Controls.Add(RadioButton_KeywordUpdateTab_CaptureSoftware_Voyager);
            GroupBox_KeywordUpdateTab_CaptureSoftware.Controls.Add(RadioButton_KeywordUpdateTab_CaptureSoftware_SharpCap);
            GroupBox_KeywordUpdateTab_CaptureSoftware.Controls.Add(RadioButton_KeywordUpdateTab_CaptureSoftware_SGPro);
            GroupBox_KeywordUpdateTab_CaptureSoftware.Controls.Add(RadioButton_KeywordUpdateTab_CaptureSoftware_TheSkyX);
            GroupBox_KeywordUpdateTab_CaptureSoftware.Location = new System.Drawing.Point(20, 212);
            GroupBox_KeywordUpdateTab_CaptureSoftware.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            GroupBox_KeywordUpdateTab_CaptureSoftware.Name = "GroupBox_KeywordUpdateTab_CaptureSoftware";
            GroupBox_KeywordUpdateTab_CaptureSoftware.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            GroupBox_KeywordUpdateTab_CaptureSoftware.Size = new System.Drawing.Size(148, 216);
            GroupBox_KeywordUpdateTab_CaptureSoftware.TabIndex = 22;
            GroupBox_KeywordUpdateTab_CaptureSoftware.TabStop = false;
            GroupBox_KeywordUpdateTab_CaptureSoftware.Text = "Capture Software";
            // 
            // RadioButton_KeywordUpdateTab_CaptureSoftware_NINA
            // 
            RadioButton_KeywordUpdateTab_CaptureSoftware_NINA.AutoSize = true;
            RadioButton_KeywordUpdateTab_CaptureSoftware_NINA.Location = new System.Drawing.Point(23, 47);
            RadioButton_KeywordUpdateTab_CaptureSoftware_NINA.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            RadioButton_KeywordUpdateTab_CaptureSoftware_NINA.Name = "RadioButton_KeywordUpdateTab_CaptureSoftware_NINA";
            RadioButton_KeywordUpdateTab_CaptureSoftware_NINA.Size = new System.Drawing.Size(54, 19);
            RadioButton_KeywordUpdateTab_CaptureSoftware_NINA.TabIndex = 6;
            RadioButton_KeywordUpdateTab_CaptureSoftware_NINA.Text = "NINA";
            RadioButton_KeywordUpdateTab_CaptureSoftware_NINA.UseVisualStyleBackColor = true;
            // 
            // Button_KeywordUpdateTab_CaptureSoftware_SetByFile
            // 
            Button_KeywordUpdateTab_CaptureSoftware_SetByFile.Location = new System.Drawing.Point(34, 181);
            Button_KeywordUpdateTab_CaptureSoftware_SetByFile.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Button_KeywordUpdateTab_CaptureSoftware_SetByFile.Name = "Button_KeywordUpdateTab_CaptureSoftware_SetByFile";
            Button_KeywordUpdateTab_CaptureSoftware_SetByFile.Size = new System.Drawing.Size(88, 27);
            Button_KeywordUpdateTab_CaptureSoftware_SetByFile.TabIndex = 5;
            Button_KeywordUpdateTab_CaptureSoftware_SetByFile.Text = "Set By File";
            Button_KeywordUpdateTab_CaptureSoftware_SetByFile.UseVisualStyleBackColor = true;
            Button_KeywordUpdateTab_CaptureSoftware_SetByFile.Click += Button_CaptureSoftware_SetByFile_Click;
            // 
            // Button_KeywordUpdateTab_CaptureSoftware_SetAll
            // 
            Button_KeywordUpdateTab_CaptureSoftware_SetAll.Location = new System.Drawing.Point(34, 150);
            Button_KeywordUpdateTab_CaptureSoftware_SetAll.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Button_KeywordUpdateTab_CaptureSoftware_SetAll.Name = "Button_KeywordUpdateTab_CaptureSoftware_SetAll";
            Button_KeywordUpdateTab_CaptureSoftware_SetAll.Size = new System.Drawing.Size(88, 27);
            Button_KeywordUpdateTab_CaptureSoftware_SetAll.TabIndex = 4;
            Button_KeywordUpdateTab_CaptureSoftware_SetAll.Text = "Set All";
            Button_KeywordUpdateTab_CaptureSoftware_SetAll.UseVisualStyleBackColor = true;
            Button_KeywordUpdateTab_CaptureSoftware_SetAll.Click += Button_CaptureSoftware_SetAll_Click;
            // 
            // RadioButton_KeywordUpdateTab_CaptureSoftware_Voyager
            // 
            RadioButton_KeywordUpdateTab_CaptureSoftware_Voyager.AutoSize = true;
            RadioButton_KeywordUpdateTab_CaptureSoftware_Voyager.Location = new System.Drawing.Point(23, 93);
            RadioButton_KeywordUpdateTab_CaptureSoftware_Voyager.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            RadioButton_KeywordUpdateTab_CaptureSoftware_Voyager.Name = "RadioButton_KeywordUpdateTab_CaptureSoftware_Voyager";
            RadioButton_KeywordUpdateTab_CaptureSoftware_Voyager.Size = new System.Drawing.Size(67, 19);
            RadioButton_KeywordUpdateTab_CaptureSoftware_Voyager.TabIndex = 3;
            RadioButton_KeywordUpdateTab_CaptureSoftware_Voyager.Text = "Voyager";
            RadioButton_KeywordUpdateTab_CaptureSoftware_Voyager.UseVisualStyleBackColor = true;
            // 
            // RadioButton_KeywordUpdateTab_CaptureSoftware_SharpCap
            // 
            RadioButton_KeywordUpdateTab_CaptureSoftware_SharpCap.AutoSize = true;
            RadioButton_KeywordUpdateTab_CaptureSoftware_SharpCap.Location = new System.Drawing.Point(23, 117);
            RadioButton_KeywordUpdateTab_CaptureSoftware_SharpCap.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            RadioButton_KeywordUpdateTab_CaptureSoftware_SharpCap.Name = "RadioButton_KeywordUpdateTab_CaptureSoftware_SharpCap";
            RadioButton_KeywordUpdateTab_CaptureSoftware_SharpCap.Size = new System.Drawing.Size(76, 19);
            RadioButton_KeywordUpdateTab_CaptureSoftware_SharpCap.TabIndex = 2;
            RadioButton_KeywordUpdateTab_CaptureSoftware_SharpCap.Text = "SharpCap";
            RadioButton_KeywordUpdateTab_CaptureSoftware_SharpCap.UseVisualStyleBackColor = true;
            // 
            // RadioButton_KeywordUpdateTab_CaptureSoftware_SGPro
            // 
            RadioButton_KeywordUpdateTab_CaptureSoftware_SGPro.AutoSize = true;
            RadioButton_KeywordUpdateTab_CaptureSoftware_SGPro.Location = new System.Drawing.Point(23, 70);
            RadioButton_KeywordUpdateTab_CaptureSoftware_SGPro.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            RadioButton_KeywordUpdateTab_CaptureSoftware_SGPro.Name = "RadioButton_KeywordUpdateTab_CaptureSoftware_SGPro";
            RadioButton_KeywordUpdateTab_CaptureSoftware_SGPro.Size = new System.Drawing.Size(57, 19);
            RadioButton_KeywordUpdateTab_CaptureSoftware_SGPro.TabIndex = 1;
            RadioButton_KeywordUpdateTab_CaptureSoftware_SGPro.Text = "SGPro";
            RadioButton_KeywordUpdateTab_CaptureSoftware_SGPro.UseVisualStyleBackColor = true;
            // 
            // RadioButton_KeywordUpdateTab_CaptureSoftware_TheSkyX
            // 
            RadioButton_KeywordUpdateTab_CaptureSoftware_TheSkyX.AutoSize = true;
            RadioButton_KeywordUpdateTab_CaptureSoftware_TheSkyX.Location = new System.Drawing.Point(23, 24);
            RadioButton_KeywordUpdateTab_CaptureSoftware_TheSkyX.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            RadioButton_KeywordUpdateTab_CaptureSoftware_TheSkyX.Name = "RadioButton_KeywordUpdateTab_CaptureSoftware_TheSkyX";
            RadioButton_KeywordUpdateTab_CaptureSoftware_TheSkyX.Size = new System.Drawing.Size(73, 19);
            RadioButton_KeywordUpdateTab_CaptureSoftware_TheSkyX.TabIndex = 0;
            RadioButton_KeywordUpdateTab_CaptureSoftware_TheSkyX.Text = "The SkyX";
            RadioButton_KeywordUpdateTab_CaptureSoftware_TheSkyX.UseVisualStyleBackColor = true;
            // 
            // GroupBox_KeywordUpdateTab_Telescope
            // 
            GroupBox_KeywordUpdateTab_Telescope.Controls.Add(TextBox_KeywordUpdateTab_Telescope_FocalLength);
            GroupBox_KeywordUpdateTab_Telescope.Controls.Add(Label_KeywordUpdateTab_Telescope_FocalLength);
            GroupBox_KeywordUpdateTab_Telescope.Controls.Add(Button_KeywordUpdateTab_Telescope_SetByFile);
            GroupBox_KeywordUpdateTab_Telescope.Controls.Add(Button_KeywordUpdateTab_Telescope_SetAll);
            GroupBox_KeywordUpdateTab_Telescope.Controls.Add(CheckBox_KeywordUpdateTab_Telescope_Riccardi);
            GroupBox_KeywordUpdateTab_Telescope.Controls.Add(RadioButton_KeywordUpdateTab_Telescope_Newtonian254);
            GroupBox_KeywordUpdateTab_Telescope.Controls.Add(RadioButton_KeywordUpdateTab_Telescope_EvoStar150);
            GroupBox_KeywordUpdateTab_Telescope.Controls.Add(RadioButton_KeywordUpdateTab_Telescope_APM107);
            GroupBox_KeywordUpdateTab_Telescope.Location = new System.Drawing.Point(176, 212);
            GroupBox_KeywordUpdateTab_Telescope.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            GroupBox_KeywordUpdateTab_Telescope.Name = "GroupBox_KeywordUpdateTab_Telescope";
            GroupBox_KeywordUpdateTab_Telescope.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            GroupBox_KeywordUpdateTab_Telescope.Size = new System.Drawing.Size(211, 216);
            GroupBox_KeywordUpdateTab_Telescope.TabIndex = 21;
            GroupBox_KeywordUpdateTab_Telescope.TabStop = false;
            GroupBox_KeywordUpdateTab_Telescope.Text = "Telescope";
            // 
            // TextBox_KeywordUpdateTab_Telescope_FocalLength
            // 
            TextBox_KeywordUpdateTab_Telescope_FocalLength.FormattingEnabled = true;
            TextBox_KeywordUpdateTab_Telescope_FocalLength.Location = new System.Drawing.Point(39, 139);
            TextBox_KeywordUpdateTab_Telescope_FocalLength.Name = "TextBox_KeywordUpdateTab_Telescope_FocalLength";
            TextBox_KeywordUpdateTab_Telescope_FocalLength.Size = new System.Drawing.Size(67, 23);
            TextBox_KeywordUpdateTab_Telescope_FocalLength.TabIndex = 19;
            // 
            // Label_KeywordUpdateTab_Telescope_FocalLength
            // 
            Label_KeywordUpdateTab_Telescope_FocalLength.AutoSize = true;
            Label_KeywordUpdateTab_Telescope_FocalLength.Location = new System.Drawing.Point(110, 143);
            Label_KeywordUpdateTab_Telescope_FocalLength.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            Label_KeywordUpdateTab_Telescope_FocalLength.Name = "Label_KeywordUpdateTab_Telescope_FocalLength";
            Label_KeywordUpdateTab_Telescope_FocalLength.Size = new System.Drawing.Size(75, 15);
            Label_KeywordUpdateTab_Telescope_FocalLength.TabIndex = 18;
            Label_KeywordUpdateTab_Telescope_FocalLength.Text = "Focal Length";
            // 
            // Button_KeywordUpdateTab_Telescope_SetByFile
            // 
            Button_KeywordUpdateTab_Telescope_SetByFile.Location = new System.Drawing.Point(108, 181);
            Button_KeywordUpdateTab_Telescope_SetByFile.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Button_KeywordUpdateTab_Telescope_SetByFile.Name = "Button_KeywordUpdateTab_Telescope_SetByFile";
            Button_KeywordUpdateTab_Telescope_SetByFile.Size = new System.Drawing.Size(88, 27);
            Button_KeywordUpdateTab_Telescope_SetByFile.TabIndex = 17;
            Button_KeywordUpdateTab_Telescope_SetByFile.Text = "Set By File";
            Button_KeywordUpdateTab_Telescope_SetByFile.UseVisualStyleBackColor = true;
            Button_KeywordUpdateTab_Telescope_SetByFile.Click += Button_Telescope_SetByFile_Click;
            // 
            // Button_KeywordUpdateTab_Telescope_SetAll
            // 
            Button_KeywordUpdateTab_Telescope_SetAll.Location = new System.Drawing.Point(13, 181);
            Button_KeywordUpdateTab_Telescope_SetAll.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Button_KeywordUpdateTab_Telescope_SetAll.Name = "Button_KeywordUpdateTab_Telescope_SetAll";
            Button_KeywordUpdateTab_Telescope_SetAll.Size = new System.Drawing.Size(88, 27);
            Button_KeywordUpdateTab_Telescope_SetAll.TabIndex = 16;
            Button_KeywordUpdateTab_Telescope_SetAll.Text = "Set All";
            Button_KeywordUpdateTab_Telescope_SetAll.UseVisualStyleBackColor = true;
            Button_KeywordUpdateTab_Telescope_SetAll.Click += Button_Telescope_SetAll_Click;
            // 
            // CheckBox_KeywordUpdateTab_Telescope_Riccardi
            // 
            CheckBox_KeywordUpdateTab_Telescope_Riccardi.AutoSize = true;
            CheckBox_KeywordUpdateTab_Telescope_Riccardi.Location = new System.Drawing.Point(20, 114);
            CheckBox_KeywordUpdateTab_Telescope_Riccardi.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            CheckBox_KeywordUpdateTab_Telescope_Riccardi.Name = "CheckBox_KeywordUpdateTab_Telescope_Riccardi";
            CheckBox_KeywordUpdateTab_Telescope_Riccardi.Size = new System.Drawing.Size(138, 19);
            CheckBox_KeywordUpdateTab_Telescope_Riccardi.TabIndex = 3;
            CheckBox_KeywordUpdateTab_Telescope_Riccardi.Text = "Riccardi 0.75 Reducer";
            CheckBox_KeywordUpdateTab_Telescope_Riccardi.UseVisualStyleBackColor = true;
            CheckBox_KeywordUpdateTab_Telescope_Riccardi.CheckedChanged += CheckBox_KeywordTelescope_Riccardi_CheckedChanged;
            // 
            // RadioButton_KeywordUpdateTab_Telescope_Newtonian254
            // 
            RadioButton_KeywordUpdateTab_Telescope_Newtonian254.AutoSize = true;
            RadioButton_KeywordUpdateTab_Telescope_Newtonian254.Location = new System.Drawing.Point(20, 84);
            RadioButton_KeywordUpdateTab_Telescope_Newtonian254.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            RadioButton_KeywordUpdateTab_Telescope_Newtonian254.Name = "RadioButton_KeywordUpdateTab_Telescope_Newtonian254";
            RadioButton_KeywordUpdateTab_Telescope_Newtonian254.Size = new System.Drawing.Size(104, 19);
            RadioButton_KeywordUpdateTab_Telescope_Newtonian254.TabIndex = 2;
            RadioButton_KeywordUpdateTab_Telescope_Newtonian254.Text = "Newtonian 254";
            RadioButton_KeywordUpdateTab_Telescope_Newtonian254.UseVisualStyleBackColor = true;
            RadioButton_KeywordUpdateTab_Telescope_Newtonian254.CheckedChanged += RadioButton_KeywordTelescope_NWT254_CheckedChanged;
            // 
            // RadioButton_KeywordUpdateTab_Telescope_EvoStar150
            // 
            RadioButton_KeywordUpdateTab_Telescope_EvoStar150.AutoSize = true;
            RadioButton_KeywordUpdateTab_Telescope_EvoStar150.Location = new System.Drawing.Point(20, 54);
            RadioButton_KeywordUpdateTab_Telescope_EvoStar150.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            RadioButton_KeywordUpdateTab_Telescope_EvoStar150.Name = "RadioButton_KeywordUpdateTab_Telescope_EvoStar150";
            RadioButton_KeywordUpdateTab_Telescope_EvoStar150.Size = new System.Drawing.Size(85, 19);
            RadioButton_KeywordUpdateTab_Telescope_EvoStar150.TabIndex = 1;
            RadioButton_KeywordUpdateTab_Telescope_EvoStar150.Text = "EvoStar 150";
            RadioButton_KeywordUpdateTab_Telescope_EvoStar150.UseVisualStyleBackColor = true;
            RadioButton_KeywordUpdateTab_Telescope_EvoStar150.CheckedChanged += RadioButton_KeywordTelescope_EVO150_CheckedChanged;
            // 
            // RadioButton_KeywordUpdateTab_Telescope_APM107
            // 
            RadioButton_KeywordUpdateTab_Telescope_APM107.AutoSize = true;
            RadioButton_KeywordUpdateTab_Telescope_APM107.Location = new System.Drawing.Point(20, 24);
            RadioButton_KeywordUpdateTab_Telescope_APM107.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            RadioButton_KeywordUpdateTab_Telescope_APM107.Name = "RadioButton_KeywordUpdateTab_Telescope_APM107";
            RadioButton_KeywordUpdateTab_Telescope_APM107.Size = new System.Drawing.Size(72, 19);
            RadioButton_KeywordUpdateTab_Telescope_APM107.TabIndex = 0;
            RadioButton_KeywordUpdateTab_Telescope_APM107.Text = "APM 107";
            RadioButton_KeywordUpdateTab_Telescope_APM107.UseVisualStyleBackColor = true;
            RadioButton_KeywordUpdateTab_Telescope_APM107.CheckedChanged += RadioButton_KeywordTelescope_APM107_CheckedChanged;
            // 
            // GroupBox_KeywordUpdateTab_SubFrameKeywords
            // 
            GroupBox_KeywordUpdateTab_SubFrameKeywords.Controls.Add(ComboBox_KeywordUpdateTab_SubFrameKeywords_KeywordFile);
            GroupBox_KeywordUpdateTab_SubFrameKeywords.Controls.Add(CheckBox_KeywordUpdateTab_SubFrameKeywords_UpdatePanelName);
            GroupBox_KeywordUpdateTab_SubFrameKeywords.Controls.Add(RadioButton_KeywordUpdateTab_SubFrameKeywords_SpecificValue);
            GroupBox_KeywordUpdateTab_SubFrameKeywords.Controls.Add(RadioButton_KeywordUpdateTab_SubFrameKeywords_AllValues);
            GroupBox_KeywordUpdateTab_SubFrameKeywords.Controls.Add(ComboBox_KeywordUpdateTab_SubFrameKeywords_KeywordComment);
            GroupBox_KeywordUpdateTab_SubFrameKeywords.Controls.Add(ComboBox_KeywordUpdateTab_SubFrameKeywords_KeywordValue);
            GroupBox_KeywordUpdateTab_SubFrameKeywords.Controls.Add(GroupBox_SubFrameKeywords_CalibrationFiles);
            GroupBox_KeywordUpdateTab_SubFrameKeywords.Controls.Add(GroupBox_KeywordUpdateTab_SubFrameKeywords_KeywordProtection);
            GroupBox_KeywordUpdateTab_SubFrameKeywords.Controls.Add(CheckBox_KeywordUpdateTab_SubFrameKeywords_AlphabetizeKeywords);
            GroupBox_KeywordUpdateTab_SubFrameKeywords.Controls.Add(GroupBox_KeywordUpdateTab_SubFrameKeywords_Weights);
            GroupBox_KeywordUpdateTab_SubFrameKeywords.Controls.Add(Button_KeywordUpdateTab_SubFrameKeywords_Delete);
            GroupBox_KeywordUpdateTab_SubFrameKeywords.Controls.Add(Button_KeywordUpdateTab_SubFrameKeywords_AddReplace);
            GroupBox_KeywordUpdateTab_SubFrameKeywords.Controls.Add(ComboBox_KeywordUpdateTab_SubFrameKeywords_KeywordName);
            GroupBox_KeywordUpdateTab_SubFrameKeywords.Controls.Add(CheckBox_KeywordUpdateTab_SubFrameKeywords_UpdateTargetName);
            GroupBox_KeywordUpdateTab_SubFrameKeywords.Controls.Add(Button_KeywordUpdateTab_SubFrameKeywords_UpdateKeywords);
            GroupBox_KeywordUpdateTab_SubFrameKeywords.Controls.Add(ComboBox_KeywordUpdateTab_SubFrameKeywords_TargetNames);
            GroupBox_KeywordUpdateTab_SubFrameKeywords.Controls.Add(Label_KeywordUpdateTab_SubFrameKeywords_TagetName);
            GroupBox_KeywordUpdateTab_SubFrameKeywords.Location = new System.Drawing.Point(20, 15);
            GroupBox_KeywordUpdateTab_SubFrameKeywords.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            GroupBox_KeywordUpdateTab_SubFrameKeywords.Name = "GroupBox_KeywordUpdateTab_SubFrameKeywords";
            GroupBox_KeywordUpdateTab_SubFrameKeywords.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            GroupBox_KeywordUpdateTab_SubFrameKeywords.Size = new System.Drawing.Size(1092, 191);
            GroupBox_KeywordUpdateTab_SubFrameKeywords.TabIndex = 14;
            GroupBox_KeywordUpdateTab_SubFrameKeywords.TabStop = false;
            GroupBox_KeywordUpdateTab_SubFrameKeywords.Text = "SubFrame Keywords";
            // 
            // ComboBox_KeywordUpdateTab_SubFrameKeywords_KeywordFile
            // 
            ComboBox_KeywordUpdateTab_SubFrameKeywords_KeywordFile.FormattingEnabled = true;
            ComboBox_KeywordUpdateTab_SubFrameKeywords_KeywordFile.Location = new System.Drawing.Point(529, 16);
            ComboBox_KeywordUpdateTab_SubFrameKeywords_KeywordFile.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            ComboBox_KeywordUpdateTab_SubFrameKeywords_KeywordFile.Name = "ComboBox_KeywordUpdateTab_SubFrameKeywords_KeywordFile";
            ComboBox_KeywordUpdateTab_SubFrameKeywords_KeywordFile.Size = new System.Drawing.Size(252, 23);
            ComboBox_KeywordUpdateTab_SubFrameKeywords_KeywordFile.Sorted = true;
            ComboBox_KeywordUpdateTab_SubFrameKeywords_KeywordFile.TabIndex = 32;
            ComboBox_KeywordUpdateTab_SubFrameKeywords_KeywordFile.Text = "File";
            // 
            // CheckBox_KeywordUpdateTab_SubFrameKeywords_UpdatePanelName
            // 
            CheckBox_KeywordUpdateTab_SubFrameKeywords_UpdatePanelName.AutoSize = true;
            CheckBox_KeywordUpdateTab_SubFrameKeywords_UpdatePanelName.Location = new System.Drawing.Point(9, 98);
            CheckBox_KeywordUpdateTab_SubFrameKeywords_UpdatePanelName.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            CheckBox_KeywordUpdateTab_SubFrameKeywords_UpdatePanelName.Name = "CheckBox_KeywordUpdateTab_SubFrameKeywords_UpdatePanelName";
            CheckBox_KeywordUpdateTab_SubFrameKeywords_UpdatePanelName.Size = new System.Drawing.Size(151, 19);
            CheckBox_KeywordUpdateTab_SubFrameKeywords_UpdatePanelName.TabIndex = 31;
            CheckBox_KeywordUpdateTab_SubFrameKeywords_UpdatePanelName.Text = "Do Not Update CPANEL";
            CheckBox_KeywordUpdateTab_SubFrameKeywords_UpdatePanelName.UseVisualStyleBackColor = true;
            // 
            // RadioButton_KeywordUpdateTab_SubFrameKeywords_SpecificValue
            // 
            RadioButton_KeywordUpdateTab_SubFrameKeywords_SpecificValue.AutoSize = true;
            RadioButton_KeywordUpdateTab_SubFrameKeywords_SpecificValue.Location = new System.Drawing.Point(651, 133);
            RadioButton_KeywordUpdateTab_SubFrameKeywords_SpecificValue.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            RadioButton_KeywordUpdateTab_SubFrameKeywords_SpecificValue.Name = "RadioButton_KeywordUpdateTab_SubFrameKeywords_SpecificValue";
            RadioButton_KeywordUpdateTab_SubFrameKeywords_SpecificValue.Size = new System.Drawing.Size(97, 19);
            RadioButton_KeywordUpdateTab_SubFrameKeywords_SpecificValue.TabIndex = 30;
            RadioButton_KeywordUpdateTab_SubFrameKeywords_SpecificValue.Text = "Specific Value";
            RadioButton_KeywordUpdateTab_SubFrameKeywords_SpecificValue.UseVisualStyleBackColor = true;
            // 
            // RadioButton_KeywordUpdateTab_SubFrameKeywords_AllValues
            // 
            RadioButton_KeywordUpdateTab_SubFrameKeywords_AllValues.AutoSize = true;
            RadioButton_KeywordUpdateTab_SubFrameKeywords_AllValues.Checked = true;
            RadioButton_KeywordUpdateTab_SubFrameKeywords_AllValues.Location = new System.Drawing.Point(567, 133);
            RadioButton_KeywordUpdateTab_SubFrameKeywords_AllValues.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            RadioButton_KeywordUpdateTab_SubFrameKeywords_AllValues.Name = "RadioButton_KeywordUpdateTab_SubFrameKeywords_AllValues";
            RadioButton_KeywordUpdateTab_SubFrameKeywords_AllValues.Size = new System.Drawing.Size(75, 19);
            RadioButton_KeywordUpdateTab_SubFrameKeywords_AllValues.TabIndex = 29;
            RadioButton_KeywordUpdateTab_SubFrameKeywords_AllValues.TabStop = true;
            RadioButton_KeywordUpdateTab_SubFrameKeywords_AllValues.Text = "All Values";
            RadioButton_KeywordUpdateTab_SubFrameKeywords_AllValues.UseVisualStyleBackColor = true;
            // 
            // ComboBox_KeywordUpdateTab_SubFrameKeywords_KeywordComment
            // 
            ComboBox_KeywordUpdateTab_SubFrameKeywords_KeywordComment.FormattingEnabled = true;
            ComboBox_KeywordUpdateTab_SubFrameKeywords_KeywordComment.Location = new System.Drawing.Point(529, 103);
            ComboBox_KeywordUpdateTab_SubFrameKeywords_KeywordComment.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            ComboBox_KeywordUpdateTab_SubFrameKeywords_KeywordComment.Name = "ComboBox_KeywordUpdateTab_SubFrameKeywords_KeywordComment";
            ComboBox_KeywordUpdateTab_SubFrameKeywords_KeywordComment.Size = new System.Drawing.Size(252, 23);
            ComboBox_KeywordUpdateTab_SubFrameKeywords_KeywordComment.Sorted = true;
            ComboBox_KeywordUpdateTab_SubFrameKeywords_KeywordComment.TabIndex = 28;
            ComboBox_KeywordUpdateTab_SubFrameKeywords_KeywordComment.Text = "Comment";
            // 
            // ComboBox_KeywordUpdateTab_SubFrameKeywords_KeywordValue
            // 
            ComboBox_KeywordUpdateTab_SubFrameKeywords_KeywordValue.FormattingEnabled = true;
            ComboBox_KeywordUpdateTab_SubFrameKeywords_KeywordValue.Location = new System.Drawing.Point(529, 74);
            ComboBox_KeywordUpdateTab_SubFrameKeywords_KeywordValue.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            ComboBox_KeywordUpdateTab_SubFrameKeywords_KeywordValue.Name = "ComboBox_KeywordUpdateTab_SubFrameKeywords_KeywordValue";
            ComboBox_KeywordUpdateTab_SubFrameKeywords_KeywordValue.Size = new System.Drawing.Size(252, 23);
            ComboBox_KeywordUpdateTab_SubFrameKeywords_KeywordValue.Sorted = true;
            ComboBox_KeywordUpdateTab_SubFrameKeywords_KeywordValue.TabIndex = 27;
            ComboBox_KeywordUpdateTab_SubFrameKeywords_KeywordValue.Text = "Value";
            ComboBox_KeywordUpdateTab_SubFrameKeywords_KeywordValue.SelectedValueChanged += ComboBox_KeywordUpdateTab_SubFrameKeywords_KeywordValue_SelectedValueChanged;
            // 
            // GroupBox_SubFrameKeywords_CalibrationFiles
            // 
            GroupBox_SubFrameKeywords_CalibrationFiles.Controls.Add(Button_SubFrameKeywords_CalibrationFiles_ClearAll);
            GroupBox_SubFrameKeywords_CalibrationFiles.Location = new System.Drawing.Point(252, 108);
            GroupBox_SubFrameKeywords_CalibrationFiles.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            GroupBox_SubFrameKeywords_CalibrationFiles.Name = "GroupBox_SubFrameKeywords_CalibrationFiles";
            GroupBox_SubFrameKeywords_CalibrationFiles.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            GroupBox_SubFrameKeywords_CalibrationFiles.Size = new System.Drawing.Size(252, 53);
            GroupBox_SubFrameKeywords_CalibrationFiles.TabIndex = 25;
            GroupBox_SubFrameKeywords_CalibrationFiles.TabStop = false;
            GroupBox_SubFrameKeywords_CalibrationFiles.Text = "Calibration Files and Keywords";
            // 
            // Button_SubFrameKeywords_CalibrationFiles_ClearAll
            // 
            Button_SubFrameKeywords_CalibrationFiles_ClearAll.Location = new System.Drawing.Point(44, 20);
            Button_SubFrameKeywords_CalibrationFiles_ClearAll.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Button_SubFrameKeywords_CalibrationFiles_ClearAll.Name = "Button_SubFrameKeywords_CalibrationFiles_ClearAll";
            Button_SubFrameKeywords_CalibrationFiles_ClearAll.Size = new System.Drawing.Size(168, 27);
            Button_SubFrameKeywords_CalibrationFiles_ClearAll.TabIndex = 0;
            Button_SubFrameKeywords_CalibrationFiles_ClearAll.Text = "Delete All Calibration Data";
            Button_SubFrameKeywords_CalibrationFiles_ClearAll.UseVisualStyleBackColor = true;
            Button_SubFrameKeywords_CalibrationFiles_ClearAll.Click += Button_SubFrameKeywords_CalibrationFiles_ClearAll_Click;
            // 
            // GroupBox_KeywordUpdateTab_SubFrameKeywords_KeywordProtection
            // 
            GroupBox_KeywordUpdateTab_SubFrameKeywords_KeywordProtection.Controls.Add(RadioButton_KeywordUpdateTab_SubFrameKeywords_KeywordProtection_Protect);
            GroupBox_KeywordUpdateTab_SubFrameKeywords_KeywordProtection.Controls.Add(RadioButton_KeywordUpdateTab_SubFrameKeywords_KeywordProtection_UpdateNew);
            GroupBox_KeywordUpdateTab_SubFrameKeywords_KeywordProtection.Controls.Add(RadioButton_KeywordUpdateTab_SubFrameKeywords_KeywordProtection_Force);
            GroupBox_KeywordUpdateTab_SubFrameKeywords_KeywordProtection.Location = new System.Drawing.Point(252, 49);
            GroupBox_KeywordUpdateTab_SubFrameKeywords_KeywordProtection.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            GroupBox_KeywordUpdateTab_SubFrameKeywords_KeywordProtection.Name = "GroupBox_KeywordUpdateTab_SubFrameKeywords_KeywordProtection";
            GroupBox_KeywordUpdateTab_SubFrameKeywords_KeywordProtection.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            GroupBox_KeywordUpdateTab_SubFrameKeywords_KeywordProtection.Size = new System.Drawing.Size(252, 48);
            GroupBox_KeywordUpdateTab_SubFrameKeywords_KeywordProtection.TabIndex = 24;
            GroupBox_KeywordUpdateTab_SubFrameKeywords_KeywordProtection.TabStop = false;
            GroupBox_KeywordUpdateTab_SubFrameKeywords_KeywordProtection.Text = "Keyword Protection";
            // 
            // RadioButton_KeywordUpdateTab_SubFrameKeywords_KeywordProtection_Protect
            // 
            RadioButton_KeywordUpdateTab_SubFrameKeywords_KeywordProtection_Protect.AutoSize = true;
            RadioButton_KeywordUpdateTab_SubFrameKeywords_KeywordProtection_Protect.Location = new System.Drawing.Point(14, 20);
            RadioButton_KeywordUpdateTab_SubFrameKeywords_KeywordProtection_Protect.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            RadioButton_KeywordUpdateTab_SubFrameKeywords_KeywordProtection_Protect.Name = "RadioButton_KeywordUpdateTab_SubFrameKeywords_KeywordProtection_Protect";
            RadioButton_KeywordUpdateTab_SubFrameKeywords_KeywordProtection_Protect.Size = new System.Drawing.Size(63, 19);
            RadioButton_KeywordUpdateTab_SubFrameKeywords_KeywordProtection_Protect.TabIndex = 25;
            RadioButton_KeywordUpdateTab_SubFrameKeywords_KeywordProtection_Protect.Text = "Protect";
            RadioButton_KeywordUpdateTab_SubFrameKeywords_KeywordProtection_Protect.UseVisualStyleBackColor = true;
            RadioButton_KeywordUpdateTab_SubFrameKeywords_KeywordProtection_Protect.CheckedChanged += RadioButton_KeywordUpdateTab_SubFrameKeywords_KeywordProtection_Protect_CheckedChanged;
            // 
            // RadioButton_KeywordUpdateTab_SubFrameKeywords_KeywordProtection_UpdateNew
            // 
            RadioButton_KeywordUpdateTab_SubFrameKeywords_KeywordProtection_UpdateNew.AutoSize = true;
            RadioButton_KeywordUpdateTab_SubFrameKeywords_KeywordProtection_UpdateNew.Checked = true;
            RadioButton_KeywordUpdateTab_SubFrameKeywords_KeywordProtection_UpdateNew.Location = new System.Drawing.Point(85, 20);
            RadioButton_KeywordUpdateTab_SubFrameKeywords_KeywordProtection_UpdateNew.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            RadioButton_KeywordUpdateTab_SubFrameKeywords_KeywordProtection_UpdateNew.Name = "RadioButton_KeywordUpdateTab_SubFrameKeywords_KeywordProtection_UpdateNew";
            RadioButton_KeywordUpdateTab_SubFrameKeywords_KeywordProtection_UpdateNew.Size = new System.Drawing.Size(90, 19);
            RadioButton_KeywordUpdateTab_SubFrameKeywords_KeywordProtection_UpdateNew.TabIndex = 24;
            RadioButton_KeywordUpdateTab_SubFrameKeywords_KeywordProtection_UpdateNew.TabStop = true;
            RadioButton_KeywordUpdateTab_SubFrameKeywords_KeywordProtection_UpdateNew.Text = "Update New";
            RadioButton_KeywordUpdateTab_SubFrameKeywords_KeywordProtection_UpdateNew.UseVisualStyleBackColor = true;
            RadioButton_KeywordUpdateTab_SubFrameKeywords_KeywordProtection_UpdateNew.CheckedChanged += RadioButton_KeywordUpdateTab_SubFrameKeywords_KeywordProtection_UpdateNew_CheckedChanged;
            // 
            // RadioButton_KeywordUpdateTab_SubFrameKeywords_KeywordProtection_Force
            // 
            RadioButton_KeywordUpdateTab_SubFrameKeywords_KeywordProtection_Force.AutoSize = true;
            RadioButton_KeywordUpdateTab_SubFrameKeywords_KeywordProtection_Force.Location = new System.Drawing.Point(183, 20);
            RadioButton_KeywordUpdateTab_SubFrameKeywords_KeywordProtection_Force.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            RadioButton_KeywordUpdateTab_SubFrameKeywords_KeywordProtection_Force.Name = "RadioButton_KeywordUpdateTab_SubFrameKeywords_KeywordProtection_Force";
            RadioButton_KeywordUpdateTab_SubFrameKeywords_KeywordProtection_Force.Size = new System.Drawing.Size(54, 19);
            RadioButton_KeywordUpdateTab_SubFrameKeywords_KeywordProtection_Force.TabIndex = 23;
            RadioButton_KeywordUpdateTab_SubFrameKeywords_KeywordProtection_Force.Text = "Force";
            RadioButton_KeywordUpdateTab_SubFrameKeywords_KeywordProtection_Force.UseVisualStyleBackColor = true;
            RadioButton_KeywordUpdateTab_SubFrameKeywords_KeywordProtection_Force.CheckedChanged += RadioButton_KeywordUpdateTab_SubFrameKeywords_KeywordProtection_Force_CheckedChanged;
            // 
            // CheckBox_KeywordUpdateTab_SubFrameKeywords_AlphabetizeKeywords
            // 
            CheckBox_KeywordUpdateTab_SubFrameKeywords_AlphabetizeKeywords.AutoSize = true;
            CheckBox_KeywordUpdateTab_SubFrameKeywords_AlphabetizeKeywords.Checked = true;
            CheckBox_KeywordUpdateTab_SubFrameKeywords_AlphabetizeKeywords.CheckState = System.Windows.Forms.CheckState.Checked;
            CheckBox_KeywordUpdateTab_SubFrameKeywords_AlphabetizeKeywords.Location = new System.Drawing.Point(263, 21);
            CheckBox_KeywordUpdateTab_SubFrameKeywords_AlphabetizeKeywords.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            CheckBox_KeywordUpdateTab_SubFrameKeywords_AlphabetizeKeywords.Name = "CheckBox_KeywordUpdateTab_SubFrameKeywords_AlphabetizeKeywords";
            CheckBox_KeywordUpdateTab_SubFrameKeywords_AlphabetizeKeywords.Size = new System.Drawing.Size(142, 19);
            CheckBox_KeywordUpdateTab_SubFrameKeywords_AlphabetizeKeywords.TabIndex = 23;
            CheckBox_KeywordUpdateTab_SubFrameKeywords_AlphabetizeKeywords.Text = "Alphabetize Keywords";
            CheckBox_KeywordUpdateTab_SubFrameKeywords_AlphabetizeKeywords.UseVisualStyleBackColor = true;
            // 
            // GroupBox_KeywordUpdateTab_SubFrameKeywords_Weights
            // 
            GroupBox_KeywordUpdateTab_SubFrameKeywords_Weights.Controls.Add(RadioButton_KeywordUpdateTab_SubFrameKeywords_Weights_Calibration);
            GroupBox_KeywordUpdateTab_SubFrameKeywords_Weights.Controls.Add(Button_KeywordUpdateTab_SubFrameKeywords_Weights_Remove);
            GroupBox_KeywordUpdateTab_SubFrameKeywords_Weights.Controls.Add(RadioButton_KeywordUpdateTab_SubFrameKeywords_Weights_Selected);
            GroupBox_KeywordUpdateTab_SubFrameKeywords_Weights.Controls.Add(RadioButton_KeywordUpdateTab_SubFrameKeywords_Weights_All);
            GroupBox_KeywordUpdateTab_SubFrameKeywords_Weights.Controls.Add(Label_KeywordUpdateTab_SubFrameKeywords_Weights_WeightKeywords);
            GroupBox_KeywordUpdateTab_SubFrameKeywords_Weights.Controls.Add(ComboBox_KeywordUpdateTab_SubFrameKeywords_Weights_WeightKeywords);
            GroupBox_KeywordUpdateTab_SubFrameKeywords_Weights.Location = new System.Drawing.Point(798, 14);
            GroupBox_KeywordUpdateTab_SubFrameKeywords_Weights.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            GroupBox_KeywordUpdateTab_SubFrameKeywords_Weights.Name = "GroupBox_KeywordUpdateTab_SubFrameKeywords_Weights";
            GroupBox_KeywordUpdateTab_SubFrameKeywords_Weights.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            GroupBox_KeywordUpdateTab_SubFrameKeywords_Weights.Size = new System.Drawing.Size(279, 134);
            GroupBox_KeywordUpdateTab_SubFrameKeywords_Weights.TabIndex = 7;
            GroupBox_KeywordUpdateTab_SubFrameKeywords_Weights.TabStop = false;
            GroupBox_KeywordUpdateTab_SubFrameKeywords_Weights.Text = "Weights";
            // 
            // RadioButton_KeywordUpdateTab_SubFrameKeywords_Weights_Calibration
            // 
            RadioButton_KeywordUpdateTab_SubFrameKeywords_Weights_Calibration.AutoSize = true;
            RadioButton_KeywordUpdateTab_SubFrameKeywords_Weights_Calibration.Location = new System.Drawing.Point(184, 60);
            RadioButton_KeywordUpdateTab_SubFrameKeywords_Weights_Calibration.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            RadioButton_KeywordUpdateTab_SubFrameKeywords_Weights_Calibration.Name = "RadioButton_KeywordUpdateTab_SubFrameKeywords_Weights_Calibration";
            RadioButton_KeywordUpdateTab_SubFrameKeywords_Weights_Calibration.Size = new System.Drawing.Size(83, 19);
            RadioButton_KeywordUpdateTab_SubFrameKeywords_Weights_Calibration.TabIndex = 21;
            RadioButton_KeywordUpdateTab_SubFrameKeywords_Weights_Calibration.TabStop = true;
            RadioButton_KeywordUpdateTab_SubFrameKeywords_Weights_Calibration.Text = "Calibration";
            RadioButton_KeywordUpdateTab_SubFrameKeywords_Weights_Calibration.UseVisualStyleBackColor = true;
            // 
            // Button_KeywordUpdateTab_SubFrameKeywords_Weights_Remove
            // 
            Button_KeywordUpdateTab_SubFrameKeywords_Weights_Remove.Location = new System.Drawing.Point(41, 86);
            Button_KeywordUpdateTab_SubFrameKeywords_Weights_Remove.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Button_KeywordUpdateTab_SubFrameKeywords_Weights_Remove.Name = "Button_KeywordUpdateTab_SubFrameKeywords_Weights_Remove";
            Button_KeywordUpdateTab_SubFrameKeywords_Weights_Remove.Size = new System.Drawing.Size(88, 27);
            Button_KeywordUpdateTab_SubFrameKeywords_Weights_Remove.TabIndex = 20;
            Button_KeywordUpdateTab_SubFrameKeywords_Weights_Remove.Text = "Remove";
            Button_KeywordUpdateTab_SubFrameKeywords_Weights_Remove.UseVisualStyleBackColor = true;
            Button_KeywordUpdateTab_SubFrameKeywords_Weights_Remove.Click += Button_KeywordSubFrameWeight_Remove_Click;
            // 
            // RadioButton_KeywordUpdateTab_SubFrameKeywords_Weights_Selected
            // 
            RadioButton_KeywordUpdateTab_SubFrameKeywords_Weights_Selected.AutoSize = true;
            RadioButton_KeywordUpdateTab_SubFrameKeywords_Weights_Selected.Location = new System.Drawing.Point(184, 85);
            RadioButton_KeywordUpdateTab_SubFrameKeywords_Weights_Selected.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            RadioButton_KeywordUpdateTab_SubFrameKeywords_Weights_Selected.Name = "RadioButton_KeywordUpdateTab_SubFrameKeywords_Weights_Selected";
            RadioButton_KeywordUpdateTab_SubFrameKeywords_Weights_Selected.Size = new System.Drawing.Size(69, 19);
            RadioButton_KeywordUpdateTab_SubFrameKeywords_Weights_Selected.TabIndex = 9;
            RadioButton_KeywordUpdateTab_SubFrameKeywords_Weights_Selected.TabStop = true;
            RadioButton_KeywordUpdateTab_SubFrameKeywords_Weights_Selected.Text = "Selected";
            RadioButton_KeywordUpdateTab_SubFrameKeywords_Weights_Selected.UseVisualStyleBackColor = true;
            // 
            // RadioButton_KeywordUpdateTab_SubFrameKeywords_Weights_All
            // 
            RadioButton_KeywordUpdateTab_SubFrameKeywords_Weights_All.AutoSize = true;
            RadioButton_KeywordUpdateTab_SubFrameKeywords_Weights_All.Location = new System.Drawing.Point(184, 34);
            RadioButton_KeywordUpdateTab_SubFrameKeywords_Weights_All.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            RadioButton_KeywordUpdateTab_SubFrameKeywords_Weights_All.Name = "RadioButton_KeywordUpdateTab_SubFrameKeywords_Weights_All";
            RadioButton_KeywordUpdateTab_SubFrameKeywords_Weights_All.Size = new System.Drawing.Size(39, 19);
            RadioButton_KeywordUpdateTab_SubFrameKeywords_Weights_All.TabIndex = 8;
            RadioButton_KeywordUpdateTab_SubFrameKeywords_Weights_All.TabStop = true;
            RadioButton_KeywordUpdateTab_SubFrameKeywords_Weights_All.Text = "All";
            RadioButton_KeywordUpdateTab_SubFrameKeywords_Weights_All.UseVisualStyleBackColor = true;
            // 
            // Label_KeywordUpdateTab_SubFrameKeywords_Weights_WeightKeywords
            // 
            Label_KeywordUpdateTab_SubFrameKeywords_Weights_WeightKeywords.AutoSize = true;
            Label_KeywordUpdateTab_SubFrameKeywords_Weights_WeightKeywords.Location = new System.Drawing.Point(36, 36);
            Label_KeywordUpdateTab_SubFrameKeywords_Weights_WeightKeywords.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            Label_KeywordUpdateTab_SubFrameKeywords_Weights_WeightKeywords.Name = "Label_KeywordUpdateTab_SubFrameKeywords_Weights_WeightKeywords";
            Label_KeywordUpdateTab_SubFrameKeywords_Weights_WeightKeywords.Size = new System.Drawing.Size(99, 15);
            Label_KeywordUpdateTab_SubFrameKeywords_Weights_WeightKeywords.TabIndex = 6;
            Label_KeywordUpdateTab_SubFrameKeywords_Weights_WeightKeywords.Text = "Weight Keywords";
            // 
            // ComboBox_KeywordUpdateTab_SubFrameKeywords_Weights_WeightKeywords
            // 
            ComboBox_KeywordUpdateTab_SubFrameKeywords_Weights_WeightKeywords.FormattingEnabled = true;
            ComboBox_KeywordUpdateTab_SubFrameKeywords_Weights_WeightKeywords.Location = new System.Drawing.Point(15, 58);
            ComboBox_KeywordUpdateTab_SubFrameKeywords_Weights_WeightKeywords.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            ComboBox_KeywordUpdateTab_SubFrameKeywords_Weights_WeightKeywords.Name = "ComboBox_KeywordUpdateTab_SubFrameKeywords_Weights_WeightKeywords";
            ComboBox_KeywordUpdateTab_SubFrameKeywords_Weights_WeightKeywords.Size = new System.Drawing.Size(140, 23);
            ComboBox_KeywordUpdateTab_SubFrameKeywords_Weights_WeightKeywords.Sorted = true;
            ComboBox_KeywordUpdateTab_SubFrameKeywords_Weights_WeightKeywords.TabIndex = 5;
            // 
            // Button_KeywordUpdateTab_SubFrameKeywords_Delete
            // 
            Button_KeywordUpdateTab_SubFrameKeywords_Delete.Location = new System.Drawing.Point(674, 158);
            Button_KeywordUpdateTab_SubFrameKeywords_Delete.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Button_KeywordUpdateTab_SubFrameKeywords_Delete.Name = "Button_KeywordUpdateTab_SubFrameKeywords_Delete";
            Button_KeywordUpdateTab_SubFrameKeywords_Delete.Size = new System.Drawing.Size(108, 27);
            Button_KeywordUpdateTab_SubFrameKeywords_Delete.TabIndex = 21;
            Button_KeywordUpdateTab_SubFrameKeywords_Delete.Text = "Delete";
            Button_KeywordUpdateTab_SubFrameKeywords_Delete.UseVisualStyleBackColor = true;
            Button_KeywordUpdateTab_SubFrameKeywords_Delete.Click += Button_KeywordUpdateTab_SubFrameKeywords_Delete_Click;
            // 
            // Button_KeywordUpdateTab_SubFrameKeywords_AddReplace
            // 
            Button_KeywordUpdateTab_SubFrameKeywords_AddReplace.Location = new System.Drawing.Point(529, 157);
            Button_KeywordUpdateTab_SubFrameKeywords_AddReplace.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Button_KeywordUpdateTab_SubFrameKeywords_AddReplace.Name = "Button_KeywordUpdateTab_SubFrameKeywords_AddReplace";
            Button_KeywordUpdateTab_SubFrameKeywords_AddReplace.Size = new System.Drawing.Size(126, 27);
            Button_KeywordUpdateTab_SubFrameKeywords_AddReplace.TabIndex = 20;
            Button_KeywordUpdateTab_SubFrameKeywords_AddReplace.Text = "Add/Replace";
            Button_KeywordUpdateTab_SubFrameKeywords_AddReplace.UseVisualStyleBackColor = true;
            Button_KeywordUpdateTab_SubFrameKeywords_AddReplace.Click += Button_KeywordUpdateTab_SubFrameKeywords_AddReplace_Click;
            // 
            // ComboBox_KeywordUpdateTab_SubFrameKeywords_KeywordName
            // 
            ComboBox_KeywordUpdateTab_SubFrameKeywords_KeywordName.FormattingEnabled = true;
            ComboBox_KeywordUpdateTab_SubFrameKeywords_KeywordName.Location = new System.Drawing.Point(529, 45);
            ComboBox_KeywordUpdateTab_SubFrameKeywords_KeywordName.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            ComboBox_KeywordUpdateTab_SubFrameKeywords_KeywordName.Name = "ComboBox_KeywordUpdateTab_SubFrameKeywords_KeywordName";
            ComboBox_KeywordUpdateTab_SubFrameKeywords_KeywordName.Size = new System.Drawing.Size(252, 23);
            ComboBox_KeywordUpdateTab_SubFrameKeywords_KeywordName.Sorted = true;
            ComboBox_KeywordUpdateTab_SubFrameKeywords_KeywordName.TabIndex = 18;
            ComboBox_KeywordUpdateTab_SubFrameKeywords_KeywordName.Text = "Keyword";
            ComboBox_KeywordUpdateTab_SubFrameKeywords_KeywordName.SelectedIndexChanged += ComboBox_KeywordUpdateTab_SubFrameKeywords_KeywordName_SelectedIndexChanged;
            // 
            // CheckBox_KeywordUpdateTab_SubFrameKeywords_UpdateTargetName
            // 
            CheckBox_KeywordUpdateTab_SubFrameKeywords_UpdateTargetName.AutoSize = true;
            CheckBox_KeywordUpdateTab_SubFrameKeywords_UpdateTargetName.Location = new System.Drawing.Point(9, 73);
            CheckBox_KeywordUpdateTab_SubFrameKeywords_UpdateTargetName.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            CheckBox_KeywordUpdateTab_SubFrameKeywords_UpdateTargetName.Name = "CheckBox_KeywordUpdateTab_SubFrameKeywords_UpdateTargetName";
            CheckBox_KeywordUpdateTab_SubFrameKeywords_UpdateTargetName.Size = new System.Drawing.Size(236, 19);
            CheckBox_KeywordUpdateTab_SubFrameKeywords_UpdateTargetName.TabIndex = 17;
            CheckBox_KeywordUpdateTab_SubFrameKeywords_UpdateTargetName.Text = "Update Target Name (and include Stars)";
            CheckBox_KeywordUpdateTab_SubFrameKeywords_UpdateTargetName.UseVisualStyleBackColor = true;
            // 
            // Button_KeywordUpdateTab_SubFrameKeywords_UpdateKeywords
            // 
            Button_KeywordUpdateTab_SubFrameKeywords_UpdateKeywords.Location = new System.Drawing.Point(66, 127);
            Button_KeywordUpdateTab_SubFrameKeywords_UpdateKeywords.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Button_KeywordUpdateTab_SubFrameKeywords_UpdateKeywords.Name = "Button_KeywordUpdateTab_SubFrameKeywords_UpdateKeywords";
            Button_KeywordUpdateTab_SubFrameKeywords_UpdateKeywords.Size = new System.Drawing.Size(111, 27);
            Button_KeywordUpdateTab_SubFrameKeywords_UpdateKeywords.TabIndex = 4;
            Button_KeywordUpdateTab_SubFrameKeywords_UpdateKeywords.Text = "Update Keywords";
            Button_KeywordUpdateTab_SubFrameKeywords_UpdateKeywords.UseVisualStyleBackColor = true;
            Button_KeywordUpdateTab_SubFrameKeywords_UpdateKeywords.Click += Button_KeywordUpdateTab_SubFrameKeywords_UpdateKeywords_Click;
            // 
            // ComboBox_KeywordUpdateTab_SubFrameKeywords_TargetNames
            // 
            ComboBox_KeywordUpdateTab_SubFrameKeywords_TargetNames.AllowDrop = true;
            ComboBox_KeywordUpdateTab_SubFrameKeywords_TargetNames.FormattingEnabled = true;
            ComboBox_KeywordUpdateTab_SubFrameKeywords_TargetNames.Location = new System.Drawing.Point(24, 40);
            ComboBox_KeywordUpdateTab_SubFrameKeywords_TargetNames.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            ComboBox_KeywordUpdateTab_SubFrameKeywords_TargetNames.Name = "ComboBox_KeywordUpdateTab_SubFrameKeywords_TargetNames";
            ComboBox_KeywordUpdateTab_SubFrameKeywords_TargetNames.Size = new System.Drawing.Size(194, 23);
            ComboBox_KeywordUpdateTab_SubFrameKeywords_TargetNames.Sorted = true;
            ComboBox_KeywordUpdateTab_SubFrameKeywords_TargetNames.TabIndex = 5;
            // 
            // Label_KeywordUpdateTab_SubFrameKeywords_TagetName
            // 
            Label_KeywordUpdateTab_SubFrameKeywords_TagetName.AllowDrop = true;
            Label_KeywordUpdateTab_SubFrameKeywords_TagetName.AutoSize = true;
            Label_KeywordUpdateTab_SubFrameKeywords_TagetName.Location = new System.Drawing.Point(84, 22);
            Label_KeywordUpdateTab_SubFrameKeywords_TagetName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            Label_KeywordUpdateTab_SubFrameKeywords_TagetName.Name = "Label_KeywordUpdateTab_SubFrameKeywords_TagetName";
            Label_KeywordUpdateTab_SubFrameKeywords_TagetName.Size = new System.Drawing.Size(75, 15);
            Label_KeywordUpdateTab_SubFrameKeywords_TagetName.TabIndex = 0;
            Label_KeywordUpdateTab_SubFrameKeywords_TagetName.Text = "Target Name";
            Label_KeywordUpdateTab_SubFrameKeywords_TagetName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GroupBox_KeywordUpdateTab_Camera
            // 
            GroupBox_KeywordUpdateTab_Camera.Controls.Add(ComboBox_KeywordUpdateTab_Camera_A144Binning);
            GroupBox_KeywordUpdateTab_Camera.Controls.Add(ComboBox_KeywordUpdateTab_Camera_Q178Binning);
            GroupBox_KeywordUpdateTab_Camera.Controls.Add(ComboBox_KeywordUpdateTab_Camera_Z183Binning);
            GroupBox_KeywordUpdateTab_Camera.Controls.Add(ComboBox_KeywordUpdateTab_Camera_Z533Binning);
            GroupBox_KeywordUpdateTab_Camera.Controls.Add(ComboBox_KeywordUpdateTab_Camera_A144SensorTemp);
            GroupBox_KeywordUpdateTab_Camera.Controls.Add(ComboBox_KeywordUpdateTab_Camera_Q178SensorTemp);
            GroupBox_KeywordUpdateTab_Camera.Controls.Add(ComboBox_KeywordUpdateTab_Camera_Z183SensorTemp);
            GroupBox_KeywordUpdateTab_Camera.Controls.Add(ComboBox_KeywordUpdateTab_Camera_Z533SensorTemp);
            GroupBox_KeywordUpdateTab_Camera.Controls.Add(ComboBox_KeywordUpdateTab_Camera_Q178Offset);
            GroupBox_KeywordUpdateTab_Camera.Controls.Add(ComboBox_KeywordUpdateTab_Camera_Z183Offset);
            GroupBox_KeywordUpdateTab_Camera.Controls.Add(ComboBox_KeywordUpdateTab_Camera_Z533Offset);
            GroupBox_KeywordUpdateTab_Camera.Controls.Add(ComboBox_KeywordUpdateTab_Camera_Q178Gain);
            GroupBox_KeywordUpdateTab_Camera.Controls.Add(ComboBox_KeywordUpdateTab_Camera_Z183Gain);
            GroupBox_KeywordUpdateTab_Camera.Controls.Add(ComboBox_KeywordUpdateTab_Camera_Z533Gain);
            GroupBox_KeywordUpdateTab_Camera.Controls.Add(ComboBox_KeywordUpdateTab_Camera_A144Seconds);
            GroupBox_KeywordUpdateTab_Camera.Controls.Add(ComboBox_KeywordUpdateTab_Camera_Q178Seconds);
            GroupBox_KeywordUpdateTab_Camera.Controls.Add(ComboBox_KeywordUpdateTab_Camera_Z533Seconds);
            GroupBox_KeywordUpdateTab_Camera.Controls.Add(ComboBox_KeywordUpdateTab_Camera_Z183Seconds);
            GroupBox_KeywordUpdateTab_Camera.Controls.Add(Label_KeywordUpdateTab_Camera_ToggleNBPreset);
            GroupBox_KeywordUpdateTab_Camera.Controls.Add(Label_KeywordUpdateTab_Camera_Seconds);
            GroupBox_KeywordUpdateTab_Camera.Controls.Add(Button_KeywordUpdateSubFrameKeywordsCamera_ToggleNB);
            GroupBox_KeywordUpdateTab_Camera.Controls.Add(CheckBox_KeywordUpdateTab_Camera_A144);
            GroupBox_KeywordUpdateTab_Camera.Controls.Add(CheckBox_KeywordUpdateTab_Camera_Q178);
            GroupBox_KeywordUpdateTab_Camera.Controls.Add(CheckBox_KeywordUpdateTab_Camera_Z183);
            GroupBox_KeywordUpdateTab_Camera.Controls.Add(CheckBox_KeywordUpdateTab_Camera_Z533);
            GroupBox_KeywordUpdateTab_Camera.Controls.Add(Label_KeywordUpdateTab_Camera_Binning);
            GroupBox_KeywordUpdateTab_Camera.Controls.Add(Label_KeywordUpdateTab_Camera_SensorTemp);
            GroupBox_KeywordUpdateTab_Camera.Controls.Add(Label_KeywordUpdateTab_Camera_Camera);
            GroupBox_KeywordUpdateTab_Camera.Controls.Add(Button_KeywordUpdateTab_Camera_SetByFile);
            GroupBox_KeywordUpdateTab_Camera.Controls.Add(Button_KeywordUpdateTab_Camera_SetAll);
            GroupBox_KeywordUpdateTab_Camera.Controls.Add(Label_KeywordUpdateTab_Camera_A144Gain);
            GroupBox_KeywordUpdateTab_Camera.Controls.Add(Label_KeywordUpdateTab_Camera_Offset);
            GroupBox_KeywordUpdateTab_Camera.Controls.Add(Label_KeywordUpdateTab_Camera_Gain);
            GroupBox_KeywordUpdateTab_Camera.Location = new System.Drawing.Point(395, 212);
            GroupBox_KeywordUpdateTab_Camera.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            GroupBox_KeywordUpdateTab_Camera.Name = "GroupBox_KeywordUpdateTab_Camera";
            GroupBox_KeywordUpdateTab_Camera.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            GroupBox_KeywordUpdateTab_Camera.Size = new System.Drawing.Size(385, 216);
            GroupBox_KeywordUpdateTab_Camera.TabIndex = 20;
            GroupBox_KeywordUpdateTab_Camera.TabStop = false;
            GroupBox_KeywordUpdateTab_Camera.Text = "Camera";
            // 
            // ComboBox_KeywordUpdateTab_Camera_A144Binning
            // 
            ComboBox_KeywordUpdateTab_Camera_A144Binning.FormattingEnabled = true;
            ComboBox_KeywordUpdateTab_Camera_A144Binning.Location = new System.Drawing.Point(330, 133);
            ComboBox_KeywordUpdateTab_Camera_A144Binning.Name = "ComboBox_KeywordUpdateTab_Camera_A144Binning";
            ComboBox_KeywordUpdateTab_Camera_A144Binning.Size = new System.Drawing.Size(44, 23);
            ComboBox_KeywordUpdateTab_Camera_A144Binning.TabIndex = 58;
            // 
            // ComboBox_KeywordUpdateTab_Camera_Q178Binning
            // 
            ComboBox_KeywordUpdateTab_Camera_Q178Binning.FormattingEnabled = true;
            ComboBox_KeywordUpdateTab_Camera_Q178Binning.Location = new System.Drawing.Point(330, 103);
            ComboBox_KeywordUpdateTab_Camera_Q178Binning.Name = "ComboBox_KeywordUpdateTab_Camera_Q178Binning";
            ComboBox_KeywordUpdateTab_Camera_Q178Binning.Size = new System.Drawing.Size(44, 23);
            ComboBox_KeywordUpdateTab_Camera_Q178Binning.TabIndex = 57;
            // 
            // ComboBox_KeywordUpdateTab_Camera_Z183Binning
            // 
            ComboBox_KeywordUpdateTab_Camera_Z183Binning.FormattingEnabled = true;
            ComboBox_KeywordUpdateTab_Camera_Z183Binning.Location = new System.Drawing.Point(330, 73);
            ComboBox_KeywordUpdateTab_Camera_Z183Binning.Name = "ComboBox_KeywordUpdateTab_Camera_Z183Binning";
            ComboBox_KeywordUpdateTab_Camera_Z183Binning.Size = new System.Drawing.Size(44, 23);
            ComboBox_KeywordUpdateTab_Camera_Z183Binning.TabIndex = 56;
            // 
            // ComboBox_KeywordUpdateTab_Camera_Z533Binning
            // 
            ComboBox_KeywordUpdateTab_Camera_Z533Binning.FormattingEnabled = true;
            ComboBox_KeywordUpdateTab_Camera_Z533Binning.Location = new System.Drawing.Point(330, 43);
            ComboBox_KeywordUpdateTab_Camera_Z533Binning.Name = "ComboBox_KeywordUpdateTab_Camera_Z533Binning";
            ComboBox_KeywordUpdateTab_Camera_Z533Binning.Size = new System.Drawing.Size(44, 23);
            ComboBox_KeywordUpdateTab_Camera_Z533Binning.TabIndex = 55;
            // 
            // ComboBox_KeywordUpdateTab_Camera_A144SensorTemp
            // 
            ComboBox_KeywordUpdateTab_Camera_A144SensorTemp.FormattingEnabled = true;
            ComboBox_KeywordUpdateTab_Camera_A144SensorTemp.Location = new System.Drawing.Point(265, 133);
            ComboBox_KeywordUpdateTab_Camera_A144SensorTemp.Name = "ComboBox_KeywordUpdateTab_Camera_A144SensorTemp";
            ComboBox_KeywordUpdateTab_Camera_A144SensorTemp.Size = new System.Drawing.Size(55, 23);
            ComboBox_KeywordUpdateTab_Camera_A144SensorTemp.TabIndex = 54;
            // 
            // ComboBox_KeywordUpdateTab_Camera_Q178SensorTemp
            // 
            ComboBox_KeywordUpdateTab_Camera_Q178SensorTemp.FormattingEnabled = true;
            ComboBox_KeywordUpdateTab_Camera_Q178SensorTemp.Location = new System.Drawing.Point(265, 103);
            ComboBox_KeywordUpdateTab_Camera_Q178SensorTemp.Name = "ComboBox_KeywordUpdateTab_Camera_Q178SensorTemp";
            ComboBox_KeywordUpdateTab_Camera_Q178SensorTemp.Size = new System.Drawing.Size(55, 23);
            ComboBox_KeywordUpdateTab_Camera_Q178SensorTemp.TabIndex = 53;
            // 
            // ComboBox_KeywordUpdateTab_Camera_Z183SensorTemp
            // 
            ComboBox_KeywordUpdateTab_Camera_Z183SensorTemp.FormattingEnabled = true;
            ComboBox_KeywordUpdateTab_Camera_Z183SensorTemp.Location = new System.Drawing.Point(265, 73);
            ComboBox_KeywordUpdateTab_Camera_Z183SensorTemp.Name = "ComboBox_KeywordUpdateTab_Camera_Z183SensorTemp";
            ComboBox_KeywordUpdateTab_Camera_Z183SensorTemp.Size = new System.Drawing.Size(55, 23);
            ComboBox_KeywordUpdateTab_Camera_Z183SensorTemp.TabIndex = 52;
            // 
            // ComboBox_KeywordUpdateTab_Camera_Z533SensorTemp
            // 
            ComboBox_KeywordUpdateTab_Camera_Z533SensorTemp.FormattingEnabled = true;
            ComboBox_KeywordUpdateTab_Camera_Z533SensorTemp.Location = new System.Drawing.Point(265, 43);
            ComboBox_KeywordUpdateTab_Camera_Z533SensorTemp.Name = "ComboBox_KeywordUpdateTab_Camera_Z533SensorTemp";
            ComboBox_KeywordUpdateTab_Camera_Z533SensorTemp.Size = new System.Drawing.Size(55, 23);
            ComboBox_KeywordUpdateTab_Camera_Z533SensorTemp.TabIndex = 51;
            // 
            // ComboBox_KeywordUpdateTab_Camera_Q178Offset
            // 
            ComboBox_KeywordUpdateTab_Camera_Q178Offset.FormattingEnabled = true;
            ComboBox_KeywordUpdateTab_Camera_Q178Offset.Location = new System.Drawing.Point(204, 103);
            ComboBox_KeywordUpdateTab_Camera_Q178Offset.Name = "ComboBox_KeywordUpdateTab_Camera_Q178Offset";
            ComboBox_KeywordUpdateTab_Camera_Q178Offset.Size = new System.Drawing.Size(51, 23);
            ComboBox_KeywordUpdateTab_Camera_Q178Offset.TabIndex = 50;
            // 
            // ComboBox_KeywordUpdateTab_Camera_Z183Offset
            // 
            ComboBox_KeywordUpdateTab_Camera_Z183Offset.FormattingEnabled = true;
            ComboBox_KeywordUpdateTab_Camera_Z183Offset.Location = new System.Drawing.Point(204, 73);
            ComboBox_KeywordUpdateTab_Camera_Z183Offset.Name = "ComboBox_KeywordUpdateTab_Camera_Z183Offset";
            ComboBox_KeywordUpdateTab_Camera_Z183Offset.Size = new System.Drawing.Size(51, 23);
            ComboBox_KeywordUpdateTab_Camera_Z183Offset.TabIndex = 49;
            // 
            // ComboBox_KeywordUpdateTab_Camera_Z533Offset
            // 
            ComboBox_KeywordUpdateTab_Camera_Z533Offset.FormattingEnabled = true;
            ComboBox_KeywordUpdateTab_Camera_Z533Offset.Location = new System.Drawing.Point(204, 43);
            ComboBox_KeywordUpdateTab_Camera_Z533Offset.Name = "ComboBox_KeywordUpdateTab_Camera_Z533Offset";
            ComboBox_KeywordUpdateTab_Camera_Z533Offset.Size = new System.Drawing.Size(51, 23);
            ComboBox_KeywordUpdateTab_Camera_Z533Offset.TabIndex = 48;
            // 
            // ComboBox_KeywordUpdateTab_Camera_Q178Gain
            // 
            ComboBox_KeywordUpdateTab_Camera_Q178Gain.FormattingEnabled = true;
            ComboBox_KeywordUpdateTab_Camera_Q178Gain.Location = new System.Drawing.Point(144, 103);
            ComboBox_KeywordUpdateTab_Camera_Q178Gain.Name = "ComboBox_KeywordUpdateTab_Camera_Q178Gain";
            ComboBox_KeywordUpdateTab_Camera_Q178Gain.Size = new System.Drawing.Size(51, 23);
            ComboBox_KeywordUpdateTab_Camera_Q178Gain.TabIndex = 47;
            // 
            // ComboBox_KeywordUpdateTab_Camera_Z183Gain
            // 
            ComboBox_KeywordUpdateTab_Camera_Z183Gain.FormattingEnabled = true;
            ComboBox_KeywordUpdateTab_Camera_Z183Gain.Location = new System.Drawing.Point(144, 73);
            ComboBox_KeywordUpdateTab_Camera_Z183Gain.Name = "ComboBox_KeywordUpdateTab_Camera_Z183Gain";
            ComboBox_KeywordUpdateTab_Camera_Z183Gain.Size = new System.Drawing.Size(51, 23);
            ComboBox_KeywordUpdateTab_Camera_Z183Gain.TabIndex = 46;
            // 
            // ComboBox_KeywordUpdateTab_Camera_Z533Gain
            // 
            ComboBox_KeywordUpdateTab_Camera_Z533Gain.FormattingEnabled = true;
            ComboBox_KeywordUpdateTab_Camera_Z533Gain.Location = new System.Drawing.Point(144, 43);
            ComboBox_KeywordUpdateTab_Camera_Z533Gain.Name = "ComboBox_KeywordUpdateTab_Camera_Z533Gain";
            ComboBox_KeywordUpdateTab_Camera_Z533Gain.Size = new System.Drawing.Size(51, 23);
            ComboBox_KeywordUpdateTab_Camera_Z533Gain.TabIndex = 45;
            // 
            // ComboBox_KeywordUpdateTab_Camera_A144Seconds
            // 
            ComboBox_KeywordUpdateTab_Camera_A144Seconds.FormattingEnabled = true;
            ComboBox_KeywordUpdateTab_Camera_A144Seconds.Location = new System.Drawing.Point(78, 133);
            ComboBox_KeywordUpdateTab_Camera_A144Seconds.Name = "ComboBox_KeywordUpdateTab_Camera_A144Seconds";
            ComboBox_KeywordUpdateTab_Camera_A144Seconds.Size = new System.Drawing.Size(55, 23);
            ComboBox_KeywordUpdateTab_Camera_A144Seconds.TabIndex = 44;
            // 
            // ComboBox_KeywordUpdateTab_Camera_Q178Seconds
            // 
            ComboBox_KeywordUpdateTab_Camera_Q178Seconds.FormattingEnabled = true;
            ComboBox_KeywordUpdateTab_Camera_Q178Seconds.Location = new System.Drawing.Point(78, 103);
            ComboBox_KeywordUpdateTab_Camera_Q178Seconds.Name = "ComboBox_KeywordUpdateTab_Camera_Q178Seconds";
            ComboBox_KeywordUpdateTab_Camera_Q178Seconds.Size = new System.Drawing.Size(55, 23);
            ComboBox_KeywordUpdateTab_Camera_Q178Seconds.TabIndex = 43;
            // 
            // ComboBox_KeywordUpdateTab_Camera_Z533Seconds
            // 
            ComboBox_KeywordUpdateTab_Camera_Z533Seconds.FormattingEnabled = true;
            ComboBox_KeywordUpdateTab_Camera_Z533Seconds.Location = new System.Drawing.Point(78, 43);
            ComboBox_KeywordUpdateTab_Camera_Z533Seconds.Name = "ComboBox_KeywordUpdateTab_Camera_Z533Seconds";
            ComboBox_KeywordUpdateTab_Camera_Z533Seconds.Size = new System.Drawing.Size(55, 23);
            ComboBox_KeywordUpdateTab_Camera_Z533Seconds.TabIndex = 42;
            // 
            // ComboBox_KeywordUpdateTab_Camera_Z183Seconds
            // 
            ComboBox_KeywordUpdateTab_Camera_Z183Seconds.FormattingEnabled = true;
            ComboBox_KeywordUpdateTab_Camera_Z183Seconds.Location = new System.Drawing.Point(78, 73);
            ComboBox_KeywordUpdateTab_Camera_Z183Seconds.Name = "ComboBox_KeywordUpdateTab_Camera_Z183Seconds";
            ComboBox_KeywordUpdateTab_Camera_Z183Seconds.Size = new System.Drawing.Size(55, 23);
            ComboBox_KeywordUpdateTab_Camera_Z183Seconds.TabIndex = 41;
            // 
            // Label_KeywordUpdateTab_Camera_ToggleNBPreset
            // 
            Label_KeywordUpdateTab_Camera_ToggleNBPreset.AutoSize = true;
            Label_KeywordUpdateTab_Camera_ToggleNBPreset.Location = new System.Drawing.Point(323, 185);
            Label_KeywordUpdateTab_Camera_ToggleNBPreset.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            Label_KeywordUpdateTab_Camera_ToggleNBPreset.Name = "Label_KeywordUpdateTab_Camera_ToggleNBPreset";
            Label_KeywordUpdateTab_Camera_ToggleNBPreset.Size = new System.Drawing.Size(39, 15);
            Label_KeywordUpdateTab_Camera_ToggleNBPreset.TabIndex = 25;
            Label_KeywordUpdateTab_Camera_ToggleNBPreset.Text = "Preset";
            // 
            // Label_KeywordUpdateTab_Camera_Seconds
            // 
            Label_KeywordUpdateTab_Camera_Seconds.AutoSize = true;
            Label_KeywordUpdateTab_Camera_Seconds.Location = new System.Drawing.Point(80, 21);
            Label_KeywordUpdateTab_Camera_Seconds.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            Label_KeywordUpdateTab_Camera_Seconds.Name = "Label_KeywordUpdateTab_Camera_Seconds";
            Label_KeywordUpdateTab_Camera_Seconds.Size = new System.Drawing.Size(51, 15);
            Label_KeywordUpdateTab_Camera_Seconds.TabIndex = 21;
            Label_KeywordUpdateTab_Camera_Seconds.Text = "Seconds";
            // 
            // Button_KeywordUpdateSubFrameKeywordsCamera_ToggleNB
            // 
            Button_KeywordUpdateSubFrameKeywordsCamera_ToggleNB.Location = new System.Drawing.Point(265, 181);
            Button_KeywordUpdateSubFrameKeywordsCamera_ToggleNB.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Button_KeywordUpdateSubFrameKeywordsCamera_ToggleNB.Name = "Button_KeywordUpdateSubFrameKeywordsCamera_ToggleNB";
            Button_KeywordUpdateSubFrameKeywordsCamera_ToggleNB.Size = new System.Drawing.Size(56, 23);
            Button_KeywordUpdateSubFrameKeywordsCamera_ToggleNB.TabIndex = 24;
            Button_KeywordUpdateSubFrameKeywordsCamera_ToggleNB.Text = "Set";
            Button_KeywordUpdateSubFrameKeywordsCamera_ToggleNB.UseVisualStyleBackColor = true;
            Button_KeywordUpdateSubFrameKeywordsCamera_ToggleNB.Click += Button_KeywordUpdateSubFrameKeywordsCamera_ToggleNB_Click;
            // 
            // CheckBox_KeywordUpdateTab_Camera_A144
            // 
            CheckBox_KeywordUpdateTab_Camera_A144.AutoSize = true;
            CheckBox_KeywordUpdateTab_Camera_A144.Location = new System.Drawing.Point(16, 135);
            CheckBox_KeywordUpdateTab_Camera_A144.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            CheckBox_KeywordUpdateTab_Camera_A144.Name = "CheckBox_KeywordUpdateTab_Camera_A144";
            CheckBox_KeywordUpdateTab_Camera_A144.Size = new System.Drawing.Size(52, 19);
            CheckBox_KeywordUpdateTab_Camera_A144.TabIndex = 31;
            CheckBox_KeywordUpdateTab_Camera_A144.Text = "A144";
            CheckBox_KeywordUpdateTab_Camera_A144.UseVisualStyleBackColor = true;
            // 
            // CheckBox_KeywordUpdateTab_Camera_Q178
            // 
            CheckBox_KeywordUpdateTab_Camera_Q178.AutoSize = true;
            CheckBox_KeywordUpdateTab_Camera_Q178.Location = new System.Drawing.Point(16, 105);
            CheckBox_KeywordUpdateTab_Camera_Q178.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            CheckBox_KeywordUpdateTab_Camera_Q178.Name = "CheckBox_KeywordUpdateTab_Camera_Q178";
            CheckBox_KeywordUpdateTab_Camera_Q178.Size = new System.Drawing.Size(53, 19);
            CheckBox_KeywordUpdateTab_Camera_Q178.TabIndex = 30;
            CheckBox_KeywordUpdateTab_Camera_Q178.Text = "Q178";
            CheckBox_KeywordUpdateTab_Camera_Q178.UseVisualStyleBackColor = true;
            // 
            // CheckBox_KeywordUpdateTab_Camera_Z183
            // 
            CheckBox_KeywordUpdateTab_Camera_Z183.AutoSize = true;
            CheckBox_KeywordUpdateTab_Camera_Z183.Location = new System.Drawing.Point(16, 75);
            CheckBox_KeywordUpdateTab_Camera_Z183.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            CheckBox_KeywordUpdateTab_Camera_Z183.Name = "CheckBox_KeywordUpdateTab_Camera_Z183";
            CheckBox_KeywordUpdateTab_Camera_Z183.Size = new System.Drawing.Size(51, 19);
            CheckBox_KeywordUpdateTab_Camera_Z183.TabIndex = 29;
            CheckBox_KeywordUpdateTab_Camera_Z183.Text = "Z183";
            CheckBox_KeywordUpdateTab_Camera_Z183.UseVisualStyleBackColor = true;
            // 
            // CheckBox_KeywordUpdateTab_Camera_Z533
            // 
            CheckBox_KeywordUpdateTab_Camera_Z533.AutoSize = true;
            CheckBox_KeywordUpdateTab_Camera_Z533.Location = new System.Drawing.Point(16, 45);
            CheckBox_KeywordUpdateTab_Camera_Z533.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            CheckBox_KeywordUpdateTab_Camera_Z533.Name = "CheckBox_KeywordUpdateTab_Camera_Z533";
            CheckBox_KeywordUpdateTab_Camera_Z533.Size = new System.Drawing.Size(51, 19);
            CheckBox_KeywordUpdateTab_Camera_Z533.TabIndex = 28;
            CheckBox_KeywordUpdateTab_Camera_Z533.Text = "Z533";
            CheckBox_KeywordUpdateTab_Camera_Z533.UseVisualStyleBackColor = true;
            // 
            // Label_KeywordUpdateTab_Camera_Binning
            // 
            Label_KeywordUpdateTab_Camera_Binning.AutoSize = true;
            Label_KeywordUpdateTab_Camera_Binning.Location = new System.Drawing.Point(328, 21);
            Label_KeywordUpdateTab_Camera_Binning.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            Label_KeywordUpdateTab_Camera_Binning.Name = "Label_KeywordUpdateTab_Camera_Binning";
            Label_KeywordUpdateTab_Camera_Binning.Size = new System.Drawing.Size(48, 15);
            Label_KeywordUpdateTab_Camera_Binning.TabIndex = 18;
            Label_KeywordUpdateTab_Camera_Binning.Text = "Binning";
            // 
            // Label_KeywordUpdateTab_Camera_SensorTemp
            // 
            Label_KeywordUpdateTab_Camera_SensorTemp.AutoSize = true;
            Label_KeywordUpdateTab_Camera_SensorTemp.Location = new System.Drawing.Point(257, 21);
            Label_KeywordUpdateTab_Camera_SensorTemp.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            Label_KeywordUpdateTab_Camera_SensorTemp.Name = "Label_KeywordUpdateTab_Camera_SensorTemp";
            Label_KeywordUpdateTab_Camera_SensorTemp.Size = new System.Drawing.Size(72, 15);
            Label_KeywordUpdateTab_Camera_SensorTemp.TabIndex = 16;
            Label_KeywordUpdateTab_Camera_SensorTemp.Text = "SensorTemp";
            // 
            // Label_KeywordUpdateTab_Camera_Camera
            // 
            Label_KeywordUpdateTab_Camera_Camera.AutoSize = true;
            Label_KeywordUpdateTab_Camera_Camera.Location = new System.Drawing.Point(17, 21);
            Label_KeywordUpdateTab_Camera_Camera.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            Label_KeywordUpdateTab_Camera_Camera.Name = "Label_KeywordUpdateTab_Camera_Camera";
            Label_KeywordUpdateTab_Camera_Camera.Size = new System.Drawing.Size(48, 15);
            Label_KeywordUpdateTab_Camera_Camera.TabIndex = 23;
            Label_KeywordUpdateTab_Camera_Camera.Text = "Camera";
            // 
            // Button_KeywordUpdateTab_Camera_SetByFile
            // 
            Button_KeywordUpdateTab_Camera_SetByFile.Location = new System.Drawing.Point(126, 181);
            Button_KeywordUpdateTab_Camera_SetByFile.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Button_KeywordUpdateTab_Camera_SetByFile.Name = "Button_KeywordUpdateTab_Camera_SetByFile";
            Button_KeywordUpdateTab_Camera_SetByFile.Size = new System.Drawing.Size(88, 27);
            Button_KeywordUpdateTab_Camera_SetByFile.TabIndex = 19;
            Button_KeywordUpdateTab_Camera_SetByFile.Text = "Set By File";
            Button_KeywordUpdateTab_Camera_SetByFile.UseVisualStyleBackColor = true;
            Button_KeywordUpdateTab_Camera_SetByFile.Click += Button_KeywordCamera_SetByFile_Click;
            // 
            // Button_KeywordUpdateTab_Camera_SetAll
            // 
            Button_KeywordUpdateTab_Camera_SetAll.Location = new System.Drawing.Point(30, 181);
            Button_KeywordUpdateTab_Camera_SetAll.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Button_KeywordUpdateTab_Camera_SetAll.Name = "Button_KeywordUpdateTab_Camera_SetAll";
            Button_KeywordUpdateTab_Camera_SetAll.Size = new System.Drawing.Size(88, 27);
            Button_KeywordUpdateTab_Camera_SetAll.TabIndex = 19;
            Button_KeywordUpdateTab_Camera_SetAll.Text = "Set All";
            Button_KeywordUpdateTab_Camera_SetAll.UseVisualStyleBackColor = true;
            Button_KeywordUpdateTab_Camera_SetAll.Click += Button_KeywordCamera_SetAll_Click;
            // 
            // Label_KeywordUpdateTab_Camera_A144Gain
            // 
            Label_KeywordUpdateTab_Camera_A144Gain.AutoSize = true;
            Label_KeywordUpdateTab_Camera_A144Gain.Location = new System.Drawing.Point(155, 137);
            Label_KeywordUpdateTab_Camera_A144Gain.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            Label_KeywordUpdateTab_Camera_A144Gain.Name = "Label_KeywordUpdateTab_Camera_A144Gain";
            Label_KeywordUpdateTab_Camera_A144Gain.Size = new System.Drawing.Size(28, 15);
            Label_KeywordUpdateTab_Camera_A144Gain.TabIndex = 12;
            Label_KeywordUpdateTab_Camera_A144Gain.Text = "0.37";
            // 
            // Label_KeywordUpdateTab_Camera_Offset
            // 
            Label_KeywordUpdateTab_Camera_Offset.AutoSize = true;
            Label_KeywordUpdateTab_Camera_Offset.Location = new System.Drawing.Point(210, 21);
            Label_KeywordUpdateTab_Camera_Offset.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            Label_KeywordUpdateTab_Camera_Offset.Name = "Label_KeywordUpdateTab_Camera_Offset";
            Label_KeywordUpdateTab_Camera_Offset.Size = new System.Drawing.Size(39, 15);
            Label_KeywordUpdateTab_Camera_Offset.TabIndex = 11;
            Label_KeywordUpdateTab_Camera_Offset.Text = "Offset";
            // 
            // Label_KeywordUpdateTab_Camera_Gain
            // 
            Label_KeywordUpdateTab_Camera_Gain.AutoSize = true;
            Label_KeywordUpdateTab_Camera_Gain.Location = new System.Drawing.Point(154, 21);
            Label_KeywordUpdateTab_Camera_Gain.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            Label_KeywordUpdateTab_Camera_Gain.Name = "Label_KeywordUpdateTab_Camera_Gain";
            Label_KeywordUpdateTab_Camera_Gain.Size = new System.Drawing.Size(31, 15);
            Label_KeywordUpdateTab_Camera_Gain.TabIndex = 10;
            Label_KeywordUpdateTab_Camera_Gain.Text = "Gain";
            // 
            // GroupBox_KeywordUpdateTab_ImageType
            // 
            GroupBox_KeywordUpdateTab_ImageType.Controls.Add(Button_KeywordsTab_ImageType_SetByFile);
            GroupBox_KeywordUpdateTab_ImageType.Controls.Add(Button_KeywordsTab_ImageType_SetAll);
            GroupBox_KeywordUpdateTab_ImageType.Controls.Add(GroupBox_KeywordUpdateTab_ImageType_Frame);
            GroupBox_KeywordUpdateTab_ImageType.Controls.Add(GroupBox_KeywordUpdateTab_ImageType_Filter);
            GroupBox_KeywordUpdateTab_ImageType.Location = new System.Drawing.Point(788, 212);
            GroupBox_KeywordUpdateTab_ImageType.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            GroupBox_KeywordUpdateTab_ImageType.Name = "GroupBox_KeywordUpdateTab_ImageType";
            GroupBox_KeywordUpdateTab_ImageType.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            GroupBox_KeywordUpdateTab_ImageType.Size = new System.Drawing.Size(323, 216);
            GroupBox_KeywordUpdateTab_ImageType.TabIndex = 18;
            GroupBox_KeywordUpdateTab_ImageType.TabStop = false;
            GroupBox_KeywordUpdateTab_ImageType.Text = "Image Type";
            // 
            // Button_KeywordsTab_ImageType_SetByFile
            // 
            Button_KeywordsTab_ImageType_SetByFile.Location = new System.Drawing.Point(170, 181);
            Button_KeywordsTab_ImageType_SetByFile.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Button_KeywordsTab_ImageType_SetByFile.Name = "Button_KeywordsTab_ImageType_SetByFile";
            Button_KeywordsTab_ImageType_SetByFile.Size = new System.Drawing.Size(88, 27);
            Button_KeywordsTab_ImageType_SetByFile.TabIndex = 18;
            Button_KeywordsTab_ImageType_SetByFile.Text = "Set By File";
            Button_KeywordsTab_ImageType_SetByFile.UseVisualStyleBackColor = true;
            Button_KeywordsTab_ImageType_SetByFile.Click += Button_KeywordImageTypeFrame_SetByFile_Click;
            // 
            // Button_KeywordsTab_ImageType_SetAll
            // 
            Button_KeywordsTab_ImageType_SetAll.Location = new System.Drawing.Point(68, 181);
            Button_KeywordsTab_ImageType_SetAll.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Button_KeywordsTab_ImageType_SetAll.Name = "Button_KeywordsTab_ImageType_SetAll";
            Button_KeywordsTab_ImageType_SetAll.Size = new System.Drawing.Size(88, 27);
            Button_KeywordsTab_ImageType_SetAll.TabIndex = 18;
            Button_KeywordsTab_ImageType_SetAll.Text = "Set All";
            Button_KeywordsTab_ImageType_SetAll.UseVisualStyleBackColor = true;
            Button_KeywordsTab_ImageType_SetAll.Click += Button_KeywordImageTypeFrame_SetAll_Click;
            // 
            // GroupBox_KeywordUpdateTab_ImageType_Frame
            // 
            GroupBox_KeywordUpdateTab_ImageType_Frame.Controls.Add(Button_KeywordsTab_ImageType_Frame_SetMaster);
            GroupBox_KeywordUpdateTab_ImageType_Frame.Controls.Add(RadioButton_KeywordsTab_ImageType_Frame_Bias);
            GroupBox_KeywordUpdateTab_ImageType_Frame.Controls.Add(RadioButton_KeywordsTab_ImageType_Frame_Flat);
            GroupBox_KeywordUpdateTab_ImageType_Frame.Controls.Add(RadioButton_KeywordsTab_ImageType_Frame_Dark);
            GroupBox_KeywordUpdateTab_ImageType_Frame.Controls.Add(RadioButton_KeywordsTab_ImageType_Frame_Light);
            GroupBox_KeywordUpdateTab_ImageType_Frame.Location = new System.Drawing.Point(10, 100);
            GroupBox_KeywordUpdateTab_ImageType_Frame.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            GroupBox_KeywordUpdateTab_ImageType_Frame.Name = "GroupBox_KeywordUpdateTab_ImageType_Frame";
            GroupBox_KeywordUpdateTab_ImageType_Frame.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            GroupBox_KeywordUpdateTab_ImageType_Frame.Size = new System.Drawing.Size(299, 75);
            GroupBox_KeywordUpdateTab_ImageType_Frame.TabIndex = 11;
            GroupBox_KeywordUpdateTab_ImageType_Frame.TabStop = false;
            GroupBox_KeywordUpdateTab_ImageType_Frame.Text = "Frame";
            // 
            // Button_KeywordsTab_ImageType_Frame_SetMaster
            // 
            Button_KeywordsTab_ImageType_Frame_SetMaster.Location = new System.Drawing.Point(112, 43);
            Button_KeywordsTab_ImageType_Frame_SetMaster.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Button_KeywordsTab_ImageType_Frame_SetMaster.Name = "Button_KeywordsTab_ImageType_Frame_SetMaster";
            Button_KeywordsTab_ImageType_Frame_SetMaster.Size = new System.Drawing.Size(88, 27);
            Button_KeywordsTab_ImageType_Frame_SetMaster.TabIndex = 4;
            Button_KeywordsTab_ImageType_Frame_SetMaster.Text = "Set Master";
            Button_KeywordsTab_ImageType_Frame_SetMaster.UseVisualStyleBackColor = true;
            Button_KeywordsTab_ImageType_Frame_SetMaster.Click += Button_KeywordImageTypeFrame_SetMaster_Click;
            // 
            // RadioButton_KeywordsTab_ImageType_Frame_Bias
            // 
            RadioButton_KeywordsTab_ImageType_Frame_Bias.AutoSize = true;
            RadioButton_KeywordsTab_ImageType_Frame_Bias.Location = new System.Drawing.Point(231, 20);
            RadioButton_KeywordsTab_ImageType_Frame_Bias.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            RadioButton_KeywordsTab_ImageType_Frame_Bias.Name = "RadioButton_KeywordsTab_ImageType_Frame_Bias";
            RadioButton_KeywordsTab_ImageType_Frame_Bias.Size = new System.Drawing.Size(46, 19);
            RadioButton_KeywordsTab_ImageType_Frame_Bias.TabIndex = 3;
            RadioButton_KeywordsTab_ImageType_Frame_Bias.TabStop = true;
            RadioButton_KeywordsTab_ImageType_Frame_Bias.Text = "Bias";
            RadioButton_KeywordsTab_ImageType_Frame_Bias.UseVisualStyleBackColor = true;
            // 
            // RadioButton_KeywordsTab_ImageType_Frame_Flat
            // 
            RadioButton_KeywordsTab_ImageType_Frame_Flat.AutoSize = true;
            RadioButton_KeywordsTab_ImageType_Frame_Flat.Location = new System.Drawing.Point(170, 20);
            RadioButton_KeywordsTab_ImageType_Frame_Flat.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            RadioButton_KeywordsTab_ImageType_Frame_Flat.Name = "RadioButton_KeywordsTab_ImageType_Frame_Flat";
            RadioButton_KeywordsTab_ImageType_Frame_Flat.Size = new System.Drawing.Size(44, 19);
            RadioButton_KeywordsTab_ImageType_Frame_Flat.TabIndex = 2;
            RadioButton_KeywordsTab_ImageType_Frame_Flat.TabStop = true;
            RadioButton_KeywordsTab_ImageType_Frame_Flat.Text = "Flat";
            RadioButton_KeywordsTab_ImageType_Frame_Flat.UseVisualStyleBackColor = true;
            // 
            // RadioButton_KeywordsTab_ImageType_Frame_Dark
            // 
            RadioButton_KeywordsTab_ImageType_Frame_Dark.AutoSize = true;
            RadioButton_KeywordsTab_ImageType_Frame_Dark.Location = new System.Drawing.Point(104, 20);
            RadioButton_KeywordsTab_ImageType_Frame_Dark.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            RadioButton_KeywordsTab_ImageType_Frame_Dark.Name = "RadioButton_KeywordsTab_ImageType_Frame_Dark";
            RadioButton_KeywordsTab_ImageType_Frame_Dark.Size = new System.Drawing.Size(49, 19);
            RadioButton_KeywordsTab_ImageType_Frame_Dark.TabIndex = 1;
            RadioButton_KeywordsTab_ImageType_Frame_Dark.TabStop = true;
            RadioButton_KeywordsTab_ImageType_Frame_Dark.Text = "Dark";
            RadioButton_KeywordsTab_ImageType_Frame_Dark.UseVisualStyleBackColor = true;
            // 
            // RadioButton_KeywordsTab_ImageType_Frame_Light
            // 
            RadioButton_KeywordsTab_ImageType_Frame_Light.AutoSize = true;
            RadioButton_KeywordsTab_ImageType_Frame_Light.Location = new System.Drawing.Point(35, 20);
            RadioButton_KeywordsTab_ImageType_Frame_Light.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            RadioButton_KeywordsTab_ImageType_Frame_Light.Name = "RadioButton_KeywordsTab_ImageType_Frame_Light";
            RadioButton_KeywordsTab_ImageType_Frame_Light.Size = new System.Drawing.Size(52, 19);
            RadioButton_KeywordsTab_ImageType_Frame_Light.TabIndex = 0;
            RadioButton_KeywordsTab_ImageType_Frame_Light.TabStop = true;
            RadioButton_KeywordsTab_ImageType_Frame_Light.Text = "Light";
            RadioButton_KeywordsTab_ImageType_Frame_Light.UseVisualStyleBackColor = true;
            // 
            // GroupBox_KeywordUpdateTab_ImageType_Filter
            // 
            GroupBox_KeywordUpdateTab_ImageType_Filter.Controls.Add(RadioButton_KeywordsTab_ImageType_Filter_Luma);
            GroupBox_KeywordUpdateTab_ImageType_Filter.Controls.Add(RadioButton_KeywordsTab_ImageType_Filter_Shutter);
            GroupBox_KeywordUpdateTab_ImageType_Filter.Controls.Add(RadioButton_KeywordsTab_ImageType_Filter_Red);
            GroupBox_KeywordUpdateTab_ImageType_Filter.Controls.Add(RadioButton_KeywordsTab_ImageType_Filter_S2);
            GroupBox_KeywordUpdateTab_ImageType_Filter.Controls.Add(RadioButton_KeywordsTab_ImageType_Filter_Ha);
            GroupBox_KeywordUpdateTab_ImageType_Filter.Controls.Add(RadioButton_KeywordsTab_ImageType_Filter_Blue);
            GroupBox_KeywordUpdateTab_ImageType_Filter.Controls.Add(RadioButton_KeywordsTab_ImageType_Filter_Green);
            GroupBox_KeywordUpdateTab_ImageType_Filter.Controls.Add(RadioButton_KeywordsTab_ImageType_Filter_O3);
            GroupBox_KeywordUpdateTab_ImageType_Filter.Location = new System.Drawing.Point(10, 20);
            GroupBox_KeywordUpdateTab_ImageType_Filter.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            GroupBox_KeywordUpdateTab_ImageType_Filter.Name = "GroupBox_KeywordUpdateTab_ImageType_Filter";
            GroupBox_KeywordUpdateTab_ImageType_Filter.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            GroupBox_KeywordUpdateTab_ImageType_Filter.Size = new System.Drawing.Size(299, 81);
            GroupBox_KeywordUpdateTab_ImageType_Filter.TabIndex = 10;
            GroupBox_KeywordUpdateTab_ImageType_Filter.TabStop = false;
            GroupBox_KeywordUpdateTab_ImageType_Filter.Text = "Filter";
            // 
            // RadioButton_KeywordsTab_ImageType_Filter_Luma
            // 
            RadioButton_KeywordsTab_ImageType_Filter_Luma.AutoSize = true;
            RadioButton_KeywordsTab_ImageType_Filter_Luma.Location = new System.Drawing.Point(34, 21);
            RadioButton_KeywordsTab_ImageType_Filter_Luma.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            RadioButton_KeywordsTab_ImageType_Filter_Luma.Name = "RadioButton_KeywordsTab_ImageType_Filter_Luma";
            RadioButton_KeywordsTab_ImageType_Filter_Luma.Size = new System.Drawing.Size(55, 19);
            RadioButton_KeywordsTab_ImageType_Filter_Luma.TabIndex = 0;
            RadioButton_KeywordsTab_ImageType_Filter_Luma.TabStop = true;
            RadioButton_KeywordsTab_ImageType_Filter_Luma.Text = "Luma";
            RadioButton_KeywordsTab_ImageType_Filter_Luma.UseVisualStyleBackColor = true;
            // 
            // RadioButton_KeywordsTab_ImageType_Filter_Shutter
            // 
            RadioButton_KeywordsTab_ImageType_Filter_Shutter.AutoSize = true;
            RadioButton_KeywordsTab_ImageType_Filter_Shutter.Location = new System.Drawing.Point(231, 51);
            RadioButton_KeywordsTab_ImageType_Filter_Shutter.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            RadioButton_KeywordsTab_ImageType_Filter_Shutter.Name = "RadioButton_KeywordsTab_ImageType_Filter_Shutter";
            RadioButton_KeywordsTab_ImageType_Filter_Shutter.Size = new System.Drawing.Size(63, 19);
            RadioButton_KeywordsTab_ImageType_Filter_Shutter.TabIndex = 8;
            RadioButton_KeywordsTab_ImageType_Filter_Shutter.TabStop = true;
            RadioButton_KeywordsTab_ImageType_Filter_Shutter.Text = "Shutter";
            RadioButton_KeywordsTab_ImageType_Filter_Shutter.UseVisualStyleBackColor = true;
            // 
            // RadioButton_KeywordsTab_ImageType_Filter_Red
            // 
            RadioButton_KeywordsTab_ImageType_Filter_Red.AutoSize = true;
            RadioButton_KeywordsTab_ImageType_Filter_Red.Location = new System.Drawing.Point(34, 51);
            RadioButton_KeywordsTab_ImageType_Filter_Red.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            RadioButton_KeywordsTab_ImageType_Filter_Red.Name = "RadioButton_KeywordsTab_ImageType_Filter_Red";
            RadioButton_KeywordsTab_ImageType_Filter_Red.Size = new System.Drawing.Size(45, 19);
            RadioButton_KeywordsTab_ImageType_Filter_Red.TabIndex = 1;
            RadioButton_KeywordsTab_ImageType_Filter_Red.TabStop = true;
            RadioButton_KeywordsTab_ImageType_Filter_Red.Text = "Red";
            RadioButton_KeywordsTab_ImageType_Filter_Red.UseVisualStyleBackColor = true;
            // 
            // RadioButton_KeywordsTab_ImageType_Filter_S2
            // 
            RadioButton_KeywordsTab_ImageType_Filter_S2.AutoSize = true;
            RadioButton_KeywordsTab_ImageType_Filter_S2.Location = new System.Drawing.Point(231, 21);
            RadioButton_KeywordsTab_ImageType_Filter_S2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            RadioButton_KeywordsTab_ImageType_Filter_S2.Name = "RadioButton_KeywordsTab_ImageType_Filter_S2";
            RadioButton_KeywordsTab_ImageType_Filter_S2.Size = new System.Drawing.Size(40, 19);
            RadioButton_KeywordsTab_ImageType_Filter_S2.TabIndex = 6;
            RadioButton_KeywordsTab_ImageType_Filter_S2.TabStop = true;
            RadioButton_KeywordsTab_ImageType_Filter_S2.Text = "S II";
            RadioButton_KeywordsTab_ImageType_Filter_S2.UseVisualStyleBackColor = true;
            // 
            // RadioButton_KeywordsTab_ImageType_Filter_Ha
            // 
            RadioButton_KeywordsTab_ImageType_Filter_Ha.AutoSize = true;
            RadioButton_KeywordsTab_ImageType_Filter_Ha.Location = new System.Drawing.Point(108, 21);
            RadioButton_KeywordsTab_ImageType_Filter_Ha.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            RadioButton_KeywordsTab_ImageType_Filter_Ha.Name = "RadioButton_KeywordsTab_ImageType_Filter_Ha";
            RadioButton_KeywordsTab_ImageType_Filter_Ha.Size = new System.Drawing.Size(40, 19);
            RadioButton_KeywordsTab_ImageType_Filter_Ha.TabIndex = 2;
            RadioButton_KeywordsTab_ImageType_Filter_Ha.TabStop = true;
            RadioButton_KeywordsTab_ImageType_Filter_Ha.Text = "Ha";
            RadioButton_KeywordsTab_ImageType_Filter_Ha.UseVisualStyleBackColor = true;
            // 
            // RadioButton_KeywordsTab_ImageType_Filter_Blue
            // 
            RadioButton_KeywordsTab_ImageType_Filter_Blue.AutoSize = true;
            RadioButton_KeywordsTab_ImageType_Filter_Blue.Location = new System.Drawing.Point(167, 51);
            RadioButton_KeywordsTab_ImageType_Filter_Blue.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            RadioButton_KeywordsTab_ImageType_Filter_Blue.Name = "RadioButton_KeywordsTab_ImageType_Filter_Blue";
            RadioButton_KeywordsTab_ImageType_Filter_Blue.Size = new System.Drawing.Size(48, 19);
            RadioButton_KeywordsTab_ImageType_Filter_Blue.TabIndex = 5;
            RadioButton_KeywordsTab_ImageType_Filter_Blue.TabStop = true;
            RadioButton_KeywordsTab_ImageType_Filter_Blue.Text = "Blue";
            RadioButton_KeywordsTab_ImageType_Filter_Blue.UseVisualStyleBackColor = true;
            // 
            // RadioButton_KeywordsTab_ImageType_Filter_Green
            // 
            RadioButton_KeywordsTab_ImageType_Filter_Green.AutoSize = true;
            RadioButton_KeywordsTab_ImageType_Filter_Green.Location = new System.Drawing.Point(95, 51);
            RadioButton_KeywordsTab_ImageType_Filter_Green.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            RadioButton_KeywordsTab_ImageType_Filter_Green.Name = "RadioButton_KeywordsTab_ImageType_Filter_Green";
            RadioButton_KeywordsTab_ImageType_Filter_Green.Size = new System.Drawing.Size(56, 19);
            RadioButton_KeywordsTab_ImageType_Filter_Green.TabIndex = 3;
            RadioButton_KeywordsTab_ImageType_Filter_Green.TabStop = true;
            RadioButton_KeywordsTab_ImageType_Filter_Green.Text = "Green";
            RadioButton_KeywordsTab_ImageType_Filter_Green.UseVisualStyleBackColor = true;
            // 
            // RadioButton_KeywordsTab_ImageType_Filter_O3
            // 
            RadioButton_KeywordsTab_ImageType_Filter_O3.AutoSize = true;
            RadioButton_KeywordsTab_ImageType_Filter_O3.Location = new System.Drawing.Point(167, 21);
            RadioButton_KeywordsTab_ImageType_Filter_O3.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            RadioButton_KeywordsTab_ImageType_Filter_O3.Name = "RadioButton_KeywordsTab_ImageType_Filter_O3";
            RadioButton_KeywordsTab_ImageType_Filter_O3.Size = new System.Drawing.Size(46, 19);
            RadioButton_KeywordsTab_ImageType_Filter_O3.TabIndex = 4;
            RadioButton_KeywordsTab_ImageType_Filter_O3.TabStop = true;
            RadioButton_KeywordsTab_ImageType_Filter_O3.Text = "O III";
            RadioButton_KeywordsTab_ImageType_Filter_O3.UseVisualStyleBackColor = true;
            // 
            // TabControl
            // 
            TabControl.Controls.Add(TabPage_Keywords);
            TabControl.Controls.Add(TabPage_Calibration);
            TabControl.Controls.Add(TabPage_TargetScheduler);
            TabControl.Location = new System.Drawing.Point(14, 282);
            TabControl.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            TabControl.Name = "TabControl";
            TabControl.SelectedIndex = 0;
            TabControl.Size = new System.Drawing.Size(1147, 535);
            TabControl.TabIndex = 23;
            TabControl.SelectedIndexChanged += TabControl_SelectedIndexChanged;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1170, 827);
            Controls.Add(TabControl);
            Controls.Add(GroupBox_FileSelection);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            MaximizeBox = false;
            Name = "MainForm";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "XISF File Manager";
            GroupBox_FileSelection_SequenceNumbering.ResumeLayout(false);
            GroupBox_FileSelection_SequenceNumbering.PerformLayout();
            GroupBox_FileSelection_Count.ResumeLayout(false);
            GroupBox_FileSelection_Count.PerformLayout();
            GroupBox_FileSelection_DirectorySelection.ResumeLayout(false);
            GroupBox_FileSelection_DirectorySelection.PerformLayout();
            GroupBox_FileSelection_DirectorySelection_FluxDensity.ResumeLayout(false);
            GroupBox_FileSelection_DirectorySelection_FluxDensity.PerformLayout();
            GroupBox_FileSelection_DirectorySelection_Masters.ResumeLayout(false);
            GroupBox_FileSelection_DirectorySelection_Masters.PerformLayout();
            GroupBox_FileSelection_Statistics.ResumeLayout(false);
            GroupBox_FileSelection_Statistics.PerformLayout();
            GroupBox_FileSelection.ResumeLayout(false);
            GroupBox_FileSelection.PerformLayout();
            TabPage_TargetScheduler.ResumeLayout(false);
            TabPage_TargetScheduler.PerformLayout();
            GroupBox_Project_Priority.ResumeLayout(false);
            GroupBox_Project_Priority.PerformLayout();
            TabPage_Calibration.ResumeLayout(false);
            TabPage_Calibration.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)NumericUpDown_CalibrationTab_MinBackground).EndInit();
            GroupBox_CalibrationTab_MatchingTolerance.ResumeLayout(false);
            GroupBox_CalibrationTab_MatchingTolerance.PerformLayout();
            TabPage_Keywords.ResumeLayout(false);
            TabPage_Keywords.PerformLayout();
            GroupBox_KeywordUpdateTab_CaptureSoftware.ResumeLayout(false);
            GroupBox_KeywordUpdateTab_CaptureSoftware.PerformLayout();
            GroupBox_KeywordUpdateTab_Telescope.ResumeLayout(false);
            GroupBox_KeywordUpdateTab_Telescope.PerformLayout();
            GroupBox_KeywordUpdateTab_SubFrameKeywords.ResumeLayout(false);
            GroupBox_KeywordUpdateTab_SubFrameKeywords.PerformLayout();
            GroupBox_SubFrameKeywords_CalibrationFiles.ResumeLayout(false);
            GroupBox_KeywordUpdateTab_SubFrameKeywords_KeywordProtection.ResumeLayout(false);
            GroupBox_KeywordUpdateTab_SubFrameKeywords_KeywordProtection.PerformLayout();
            GroupBox_KeywordUpdateTab_SubFrameKeywords_Weights.ResumeLayout(false);
            GroupBox_KeywordUpdateTab_SubFrameKeywords_Weights.PerformLayout();
            GroupBox_KeywordUpdateTab_Camera.ResumeLayout(false);
            GroupBox_KeywordUpdateTab_Camera.PerformLayout();
            GroupBox_KeywordUpdateTab_ImageType.ResumeLayout(false);
            GroupBox_KeywordUpdateTab_ImageType_Frame.ResumeLayout(false);
            GroupBox_KeywordUpdateTab_ImageType_Frame.PerformLayout();
            GroupBox_KeywordUpdateTab_ImageType_Filter.ResumeLayout(false);
            GroupBox_KeywordUpdateTab_ImageType_Filter.PerformLayout();
            TabControl.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private System.Windows.Forms.ProgressBar ProgressBar_FileSelection_ReadProgress;
        private System.Windows.Forms.GroupBox GroupBox_FileSelection_SequenceNumbering;
        private System.Windows.Forms.RadioButton RadioButton_FileSelection_SequenceNumbering_WeightOnly;
        private System.Windows.Forms.RadioButton RadioButton_FileSelection_SequenceNumbering_IndexOnly;
        private System.Windows.Forms.RadioButton RadioButton_FileSelection_SequenceNumbering_IndexWeight;
        private System.Windows.Forms.RadioButton RadioButton_FileSelection_SequenceNumbering_WeightIndex;
        private System.Windows.Forms.GroupBox GroupBox_FileSelection_DirectorySelection;
        private System.Windows.Forms.CheckBox CheckBox_FileSelection_DirectorySelection_Masters_Enable;
        private System.Windows.Forms.Button Button_FileSelection_DirectorySelection_Browse;
        private System.Windows.Forms.CheckBox CheckBox_FileSelection_DirectorySelection_Recurse;
        private System.Windows.Forms.Button Button_FileSelection_DirectorySelection_Rename;
        private System.Windows.Forms.GroupBox GroupBox_FileSelection_Statistics;
        private System.Windows.Forms.Label Label_FileSelection_Statistics_OperationStatus;
        private System.Windows.Forms.Label Label_FileSelection_Statistics_SubFrameOverhead;
        private System.Windows.Forms.Label Label_FileSelection_Statistics_TempratureCoefficient;
        private System.Windows.Forms.Label Label_FileSelection_BrowseFileName;
        private System.Windows.Forms.GroupBox GroupBox_FileSelection_Count;
        private System.Windows.Forms.RadioButton RadioButton_FileSelection_Index_ByFilter;
        private System.Windows.Forms.RadioButton RadioButton_FileSelection_Index_ByTime;
        private System.Windows.Forms.GroupBox GroupBox_FileSelection;
        private System.Windows.Forms.CheckBox CheckBox_FileSlection_DirectorySelection_NoStatistics;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.TabPage TabPage_TargetScheduler;
        private System.Windows.Forms.GroupBox GroupBox_Project_Priority;
        private System.Windows.Forms.RadioButton RadioButton_ProjectPriority_High;
        private System.Windows.Forms.RadioButton RadioButton_ProjectPriority_Normal;
        private System.Windows.Forms.RadioButton RadioButton_ProjectPriority_Low;
        private System.Windows.Forms.Label Label_SchedulerTab_PlansText;
        private System.Windows.Forms.Label Label_SchedulerTab_TargetsText;
        private System.Windows.Forms.TreeView TreeView_SchedulerTab_TargetTree;
        private System.Windows.Forms.Label Label_SchedulerTab_ProjectsText;
        private System.Windows.Forms.Label Label_SchedulerTab_ProfilesText;
        private System.Windows.Forms.TreeView TreeView_SchedulerTab_ProjectTree;
        private System.Windows.Forms.TreeView TreeView_SchedulerTab_ProfileTree;
        private System.Windows.Forms.Button Button_SchedulerTab_OpenDatabase;
        private System.Windows.Forms.TabPage TabPage_Calibration;
        private System.Windows.Forms.CheckBox CheckBox_CalibrationTab_CreateNew;
        private System.Windows.Forms.TreeView TreeView_CalibrationTab_TargetFileTree;
        private System.Windows.Forms.TextBox TextBox_CalibrationTab_Messgaes;
        private System.Windows.Forms.GroupBox GroupBox_CalibrationTab_MatchingTolerance;
        private System.Windows.Forms.Label Label_CalibrationTab_MatchingTolerance_TemperatureDegrees;
        private System.Windows.Forms.Label Label_CalibrationTab_MatchingTolerance_OffsetADU;
        private System.Windows.Forms.Label Label_CalibrationTab_MatchingTolerance_GainUnits;
        private System.Windows.Forms.Label Label_CalibrationTab_MatchingTolerance_ExposureSeconds;
        private System.Windows.Forms.Label Label_CalibrationTab_MatchingTolerance_Percentage;
        private System.Windows.Forms.TextBox TextBox_CalibrationTab_MatchingTolerance_Temperature;
        private System.Windows.Forms.TextBox TextBox_CalibrationTab_MatchingTolerance_Offset;
        private System.Windows.Forms.TextBox TextBox_CalibrationTab_MatchingTolerance_Gain;
        private System.Windows.Forms.Label Label_CalibrationTab_MatchingTolerance_Temperature;
        private System.Windows.Forms.Label Label_CalibrationTab_MatchingTolerance_Offset;
        private System.Windows.Forms.Label Label_CalibrationTab_MatchingTolerance_Gain;
        private System.Windows.Forms.Label Label_CalibrationTab_MatchingTolerance_Exposure;
        private System.Windows.Forms.TextBox TextBox_CalibrationTab_MatchingTolerance_Exposure;
        private System.Windows.Forms.Label Label_CalibrationTab_TotalFiles;
        private System.Windows.Forms.ProgressBar ProgressBar_CalibrationTab;
        private System.Windows.Forms.Label Label_CalibrationTab_ReadFileName;
        private System.Windows.Forms.Button Button_CalibrationTab_CreateCalibrationDirectory;
        private System.Windows.Forms.Button Button_CalibrationTab_MatchCalibrationFrames;
        private System.Windows.Forms.Button Button_CalibrationTab_FindCalibrationFrames;
        private System.Windows.Forms.TabPage TabPage_Keywords;
        private System.Windows.Forms.Label Label_KeywordUpdateTab_FileName;
        private System.Windows.Forms.ProgressBar ProgressBar_KeywordUpdateTab_WriteProgress;
        private System.Windows.Forms.GroupBox GroupBox_KeywordUpdateTab_CaptureSoftware;
        private System.Windows.Forms.RadioButton RadioButton_KeywordUpdateTab_CaptureSoftware_NINA;
        private System.Windows.Forms.Button Button_KeywordUpdateTab_CaptureSoftware_SetByFile;
        private System.Windows.Forms.Button Button_KeywordUpdateTab_CaptureSoftware_SetAll;
        private System.Windows.Forms.RadioButton RadioButton_KeywordUpdateTab_CaptureSoftware_Voyager;
        private System.Windows.Forms.RadioButton RadioButton_KeywordUpdateTab_CaptureSoftware_SharpCap;
        private System.Windows.Forms.RadioButton RadioButton_KeywordUpdateTab_CaptureSoftware_SGPro;
        private System.Windows.Forms.RadioButton RadioButton_KeywordUpdateTab_CaptureSoftware_TheSkyX;
        private System.Windows.Forms.GroupBox GroupBox_KeywordUpdateTab_Telescope;
        private System.Windows.Forms.ComboBox TextBox_KeywordUpdateTab_Telescope_FocalLength;
        private System.Windows.Forms.Label Label_KeywordUpdateTab_Telescope_FocalLength;
        private System.Windows.Forms.Button Button_KeywordUpdateTab_Telescope_SetByFile;
        private System.Windows.Forms.Button Button_KeywordUpdateTab_Telescope_SetAll;
        private System.Windows.Forms.CheckBox CheckBox_KeywordUpdateTab_Telescope_Riccardi;
        private System.Windows.Forms.RadioButton RadioButton_KeywordUpdateTab_Telescope_Newtonian254;
        private System.Windows.Forms.RadioButton RadioButton_KeywordUpdateTab_Telescope_EvoStar150;
        private System.Windows.Forms.RadioButton RadioButton_KeywordUpdateTab_Telescope_APM107;
        private System.Windows.Forms.GroupBox GroupBox_KeywordUpdateTab_SubFrameKeywords;
        private System.Windows.Forms.CheckBox CheckBox_KeywordUpdateTab_SubFrameKeywords_UpdatePanelName;
        private System.Windows.Forms.RadioButton RadioButton_KeywordUpdateTab_SubFrameKeywords_SpecificValue;
        private System.Windows.Forms.RadioButton RadioButton_KeywordUpdateTab_SubFrameKeywords_AllValues;
        private System.Windows.Forms.ComboBox ComboBox_KeywordUpdateTab_SubFrameKeywords_KeywordComment;
        private System.Windows.Forms.ComboBox ComboBox_KeywordUpdateTab_SubFrameKeywords_KeywordValue;
        private System.Windows.Forms.GroupBox GroupBox_SubFrameKeywords_CalibrationFiles;
        private System.Windows.Forms.Button Button_SubFrameKeywords_CalibrationFiles_ClearAll;
        private System.Windows.Forms.GroupBox GroupBox_KeywordUpdateTab_SubFrameKeywords_KeywordProtection;
        private System.Windows.Forms.RadioButton RadioButton_KeywordUpdateTab_SubFrameKeywords_KeywordProtection_UpdateNew;
        private System.Windows.Forms.RadioButton RadioButton_KeywordUpdateTab_SubFrameKeywords_KeywordProtection_Force;
        private System.Windows.Forms.CheckBox CheckBox_KeywordUpdateTab_SubFrameKeywords_AlphabetizeKeywords;
        private System.Windows.Forms.GroupBox GroupBox_KeywordUpdateTab_SubFrameKeywords_Weights;
        private System.Windows.Forms.RadioButton RadioButton_KeywordUpdateTab_SubFrameKeywords_Weights_Calibration;
        private System.Windows.Forms.Button Button_KeywordUpdateTab_SubFrameKeywords_Weights_Remove;
        private System.Windows.Forms.RadioButton RadioButton_KeywordUpdateTab_SubFrameKeywords_Weights_Selected;
        private System.Windows.Forms.RadioButton RadioButton_KeywordUpdateTab_SubFrameKeywords_Weights_All;
        private System.Windows.Forms.Label Label_KeywordUpdateTab_SubFrameKeywords_Weights_WeightKeywords;
        private System.Windows.Forms.ComboBox ComboBox_KeywordUpdateTab_SubFrameKeywords_Weights_WeightKeywords;
        private System.Windows.Forms.Button Button_KeywordUpdateTab_SubFrameKeywords_Delete;
        private System.Windows.Forms.Button Button_KeywordUpdateTab_SubFrameKeywords_AddReplace;
        private System.Windows.Forms.ComboBox ComboBox_KeywordUpdateTab_SubFrameKeywords_KeywordName;
        private System.Windows.Forms.CheckBox CheckBox_KeywordUpdateTab_SubFrameKeywords_UpdateTargetName;
        private System.Windows.Forms.Button Button_KeywordUpdateTab_SubFrameKeywords_UpdateKeywords;
        private System.Windows.Forms.ComboBox ComboBox_KeywordUpdateTab_SubFrameKeywords_TargetNames;
        private System.Windows.Forms.Label Label_KeywordUpdateTab_SubFrameKeywords_TagetName;
        private System.Windows.Forms.GroupBox GroupBox_KeywordUpdateTab_Camera;
        private System.Windows.Forms.ComboBox ComboBox_KeywordUpdateTab_Camera_A144Binning;
        private System.Windows.Forms.ComboBox ComboBox_KeywordUpdateTab_Camera_Q178Binning;
        private System.Windows.Forms.ComboBox ComboBox_KeywordUpdateTab_Camera_Z183Binning;
        private System.Windows.Forms.ComboBox ComboBox_KeywordUpdateTab_Camera_Z533Binning;
        private System.Windows.Forms.ComboBox ComboBox_KeywordUpdateTab_Camera_A144SensorTemp;
        private System.Windows.Forms.ComboBox ComboBox_KeywordUpdateTab_Camera_Q178SensorTemp;
        private System.Windows.Forms.ComboBox ComboBox_KeywordUpdateTab_Camera_Z183SensorTemp;
        private System.Windows.Forms.ComboBox ComboBox_KeywordUpdateTab_Camera_Z533SensorTemp;
        private System.Windows.Forms.ComboBox ComboBox_KeywordUpdateTab_Camera_Q178Offset;
        private System.Windows.Forms.ComboBox ComboBox_KeywordUpdateTab_Camera_Z183Offset;
        private System.Windows.Forms.ComboBox ComboBox_KeywordUpdateTab_Camera_Z533Offset;
        private System.Windows.Forms.ComboBox ComboBox_KeywordUpdateTab_Camera_Q178Gain;
        private System.Windows.Forms.ComboBox ComboBox_KeywordUpdateTab_Camera_Z183Gain;
        private System.Windows.Forms.ComboBox ComboBox_KeywordUpdateTab_Camera_Z533Gain;
        private System.Windows.Forms.ComboBox ComboBox_KeywordUpdateTab_Camera_A144Seconds;
        private System.Windows.Forms.ComboBox ComboBox_KeywordUpdateTab_Camera_Q178Seconds;
        private System.Windows.Forms.ComboBox ComboBox_KeywordUpdateTab_Camera_Z533Seconds;
        private System.Windows.Forms.ComboBox ComboBox_KeywordUpdateTab_Camera_Z183Seconds;
        private System.Windows.Forms.Label Label_KeywordUpdateTab_Camera_ToggleNBPreset;
        private System.Windows.Forms.Label Label_KeywordUpdateTab_Camera_Seconds;
        private System.Windows.Forms.Button Button_KeywordUpdateSubFrameKeywordsCamera_ToggleNB;
        private System.Windows.Forms.CheckBox CheckBox_KeywordUpdateTab_Camera_A144;
        private System.Windows.Forms.CheckBox CheckBox_KeywordUpdateTab_Camera_Q178;
        private System.Windows.Forms.CheckBox CheckBox_KeywordUpdateTab_Camera_Z183;
        private System.Windows.Forms.CheckBox CheckBox_KeywordUpdateTab_Camera_Z533;
        private System.Windows.Forms.Label Label_KeywordUpdateTab_Camera_Binning;
        private System.Windows.Forms.Label Label_KeywordUpdateTab_Camera_SensorTemp;
        private System.Windows.Forms.Label Label_KeywordUpdateTab_Camera_Camera;
        private System.Windows.Forms.Button Button_KeywordUpdateTab_Camera_SetByFile;
        private System.Windows.Forms.Button Button_KeywordUpdateTab_Camera_SetAll;
        private System.Windows.Forms.Label Label_KeywordUpdateTab_Camera_A144Gain;
        private System.Windows.Forms.Label Label_KeywordUpdateTab_Camera_Offset;
        private System.Windows.Forms.Label Label_KeywordUpdateTab_Camera_Gain;
        private System.Windows.Forms.GroupBox GroupBox_KeywordUpdateTab_ImageType;
        private System.Windows.Forms.Button Button_KeywordsTab_ImageType_SetByFile;
        private System.Windows.Forms.Button Button_KeywordsTab_ImageType_SetAll;
        private System.Windows.Forms.GroupBox GroupBox_KeywordUpdateTab_ImageType_Frame;
        private System.Windows.Forms.Button Button_KeywordsTab_ImageType_Frame_SetMaster;
        private System.Windows.Forms.RadioButton RadioButton_KeywordsTab_ImageType_Frame_Bias;
        private System.Windows.Forms.RadioButton RadioButton_KeywordsTab_ImageType_Frame_Flat;
        private System.Windows.Forms.RadioButton RadioButton_KeywordsTab_ImageType_Frame_Dark;
        private System.Windows.Forms.RadioButton RadioButton_KeywordsTab_ImageType_Frame_Light;
        private System.Windows.Forms.GroupBox GroupBox_KeywordUpdateTab_ImageType_Filter;
        private System.Windows.Forms.RadioButton RadioButton_KeywordsTab_ImageType_Filter_Luma;
        private System.Windows.Forms.RadioButton RadioButton_KeywordsTab_ImageType_Filter_Shutter;
        private System.Windows.Forms.RadioButton RadioButton_KeywordsTab_ImageType_Filter_Red;
        private System.Windows.Forms.RadioButton RadioButton_KeywordsTab_ImageType_Filter_S2;
        private System.Windows.Forms.RadioButton RadioButton_KeywordsTab_ImageType_Filter_Ha;
        private System.Windows.Forms.RadioButton RadioButton_KeywordsTab_ImageType_Filter_Blue;
        private System.Windows.Forms.RadioButton RadioButton_KeywordsTab_ImageType_Filter_Green;
        private System.Windows.Forms.RadioButton RadioButton_KeywordsTab_ImageType_Filter_O3;
        private System.Windows.Forms.TabControl TabControl;
        private System.Windows.Forms.Button Button_KeywordUpdateTab_Cancel;
        private System.Windows.Forms.CheckBox CheckBox_CalibrationTab_MatchingTolerance_TemperatureNearest;
        private System.Windows.Forms.CheckBox CheckBox_CalibrationTab_MatchingTolerance_OffsetNearest;
        private System.Windows.Forms.CheckBox CheckBox_CalibrationTab_MatchingTolerance_GainNearest;
        private System.Windows.Forms.CheckBox CheckBox_CalibrationTab_MatchingTolerance_ExposureNearest;
        private System.Windows.Forms.RadioButton RadioButton_KeywordUpdateTab_SubFrameKeywords_KeywordProtection_Protect;
        private System.Windows.Forms.ComboBox ComboBox_KeywordUpdateTab_SubFrameKeywords_KeywordFile;
        private System.Windows.Forms.TextBox TextBox_FileSelection_DirectorySelection_Masters_Frames;
        private System.Windows.Forms.Panel Panel_TargetScheduler;
        private System.Windows.Forms.Label Label_CalibrationTab_Pedestal;
        private System.Windows.Forms.NumericUpDown NumericUpDown_CalibrationTab_MinBackground;
        private System.Windows.Forms.Button Button_CalibrationTab_FindPedestal;
        private System.Windows.Forms.Label Label_CalibrationTab_Minimum;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox TextBox_FileSelection_DirectorySelection_Masters_Rejection;
        private System.Windows.Forms.CheckBox CheckBox_FileSelection_DirectorySelection_FluxDensity_Enable;
        private System.Windows.Forms.Button Button_FileSelection_DirectorySelection_FluxDensity_Run;
        private System.Windows.Forms.GroupBox GroupBox_FileSelection_DirectorySelection_Masters;
        private System.Windows.Forms.GroupBox GroupBox_FileSelection_DirectorySelection_FluxDensity;
    }
}