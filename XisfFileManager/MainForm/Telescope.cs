using System.Drawing;
using System.Windows.Forms;
using XisfFileManager.Files;
using XisfFileManager.Models;
using XisfFileManager.Models.Telescopes;
using XisfFileManager.Services;

namespace XisfFileManager;

public partial class MainForm
{
    // Telescope radio button mappings
    private Dictionary<TelescopeConfiguration, RadioButton>? _telescopeRadioButtons;

    private void InitializeTelescopeMappings()
    {
        _telescopeRadioButtons = new Dictionary<TelescopeConfiguration, RadioButton>
        {
            { APM107Telescope.Instance, RadioButton_KeywordUpdateTab_Telescope_APM107 },
            { EvoStar150Telescope.Instance, RadioButton_KeywordUpdateTab_Telescope_EvoStar150 },
            { Newtonian254Telescope.Instance, RadioButton_KeywordUpdateTab_Telescope_Newtonian254 }
        };
    }

    public void ClearTelescopeGroup()
    {
        if (_telescopeRadioButtons == null)
            InitializeTelescopeMappings();

        // Reset radio buttons
        foreach (var rb in _telescopeRadioButtons!.Values)
        {
            rb.Checked = false;
            rb.ForeColor = Color.Black;
        }

        // Reset checkbox and text
        CheckBox_KeywordUpdateTab_Telescope_Riccardi.Checked = false;
        CheckBox_KeywordUpdateTab_Telescope_Riccardi.ForeColor = Color.Black;
        TextBox_KeywordUpdateTab_Telescope_FocalLength.Text = string.Empty;
        Label_KeywordUpdateTab_Telescope_FocalLength.ForeColor = Color.Black;

        // Reset buttons
        Button_KeywordUpdateTab_Telescope_SetAll.ForeColor = Color.Black;
        Button_KeywordUpdateTab_Telescope_SetByFile.ForeColor = Color.Black;
    }

    private void FindTelescope()
    {
        if (mFileList.Count == 0) return;

        var analysis = TelescopeService.AnalyzeTelescopes(mFileList);

        // No telescopes found
        if (analysis.FilesWithTelescope == 0)
        {
            SetTelescopeControlsColor(Color.DarkViolet);
            return;
        }

        // Update Riccardi checkbox
        CheckBox_KeywordUpdateTab_Telescope_Riccardi.Checked =
            analysis.FilesWithReducer == analysis.FilesWithTelescope;
        CheckBox_KeywordUpdateTab_Telescope_Riccardi.ForeColor =
            TelescopeService.GetRiccardiColor(analysis);

        // Update focal length label
        Label_KeywordUpdateTab_Telescope_FocalLength.ForeColor =
            TelescopeService.GetFocalLengthLabelColor(analysis);

        // Update radio buttons and focal length
        var uniqueTelescope = TelescopeService.GetUniqueTelescope(analysis);
        foreach (var (telescope, radioButton) in _telescopeRadioButtons!)
        {
            int count = analysis.TelescopeCounts.GetValueOrDefault(telescope, 0);

            if (count > 0)
            {
                radioButton.ForeColor = TelescopeService.GetRadioButtonColor(telescope, analysis);

                if (telescope == uniqueTelescope)
                {
                    radioButton.Checked = true;
                    bool withReducer = CheckBox_KeywordUpdateTab_Telescope_Riccardi.Checked;
                    TextBox_KeywordUpdateTab_Telescope_FocalLength.Text =
                        telescope.GetFocalLength(withReducer).ToString();
                }
            }
            else
            {
                radioButton.ForeColor = Color.Black;
            }
        }

        // Update button colors
        bool allFilesHaveTelescope = analysis.FilesWithTelescope == analysis.TotalFiles;
        bool allFilesHaveFocalLength = analysis.FilesWithFocalLength == analysis.TotalFiles;

        Button_KeywordUpdateTab_Telescope_SetAll.ForeColor =
            uniqueTelescope != null && allFilesHaveFocalLength ? Color.Black : Color.Red;
        Button_KeywordUpdateTab_Telescope_SetByFile.ForeColor =
            allFilesHaveTelescope ? Color.Black : Color.Red;
    }

    private void SetTelescopeControlsColor(Color color)
    {
        foreach (var rb in _telescopeRadioButtons!.Values)
            rb.ForeColor = color;

        Label_KeywordUpdateTab_Telescope_FocalLength.ForeColor = color;
        CheckBox_KeywordUpdateTab_Telescope_Riccardi.ForeColor = color;
        CheckBox_KeywordUpdateTab_Telescope_Riccardi.Checked = false;
        Button_KeywordUpdateTab_Telescope_SetAll.ForeColor = color;
        Button_KeywordUpdateTab_Telescope_SetByFile.ForeColor = color;
    }

    private TelescopeConfiguration? GetSelectedTelescope() =>
        _telescopeRadioButtons?.FirstOrDefault(kv => kv.Value.Checked).Key;

    private void UpdateFocalLengthDisplay()
    {
        var selected = GetSelectedTelescope();
        if (selected == null) return;

        bool withReducer = CheckBox_KeywordUpdateTab_Telescope_Riccardi.Checked;
        TextBox_KeywordUpdateTab_Telescope_FocalLength.Text =
            selected.GetFocalLength(withReducer).ToString();
    }

    private void RadioButton_KeywordTelescope_APM107_CheckedChanged(object sender, EventArgs e)
    {
        if (RadioButton_KeywordUpdateTab_Telescope_APM107.Checked)
            UpdateFocalLengthDisplay();
    }

    private void RadioButton_KeywordTelescope_EVO150_CheckedChanged(object sender, EventArgs e)
    {
        if (RadioButton_KeywordUpdateTab_Telescope_EvoStar150.Checked)
            UpdateFocalLengthDisplay();
    }

    private void RadioButton_KeywordTelescope_NWT254_CheckedChanged(object sender, EventArgs e)
    {
        if (RadioButton_KeywordUpdateTab_Telescope_Newtonian254.Checked)
            UpdateFocalLengthDisplay();
    }

    private void CheckBox_KeywordTelescope_Riccardi_CheckedChanged(object sender, EventArgs e)
    {
        UpdateFocalLengthDisplay();
    }

    private void ApplyTelescopeToFile(XisfFile file)
    {
        var selected = GetSelectedTelescope();
        if (selected == null) return;

        bool withReducer = CheckBox_KeywordUpdateTab_Telescope_Riccardi.Checked;
        selected.ApplyKeywords(file, withReducer);
    }

    private void Button_Telescope_SetAll_Click(object sender, EventArgs e)
    {
        foreach (var file in mFileList)
            ApplyTelescopeToFile(file);

        FindTelescope();
    }

    private void Button_Telescope_SetByFile_Click(object sender, EventArgs e)
    {
        bool globalTelescope = false;

        foreach (XisfFile file in mFileList)
        {
            if (globalTelescope)
            {
                if (string.IsNullOrEmpty(file.Telescope))
                    ApplyTelescopeToFile(file);
            }
            else
            {
                string telescope = file.Telescope;
                if (telescope.Contains("Global_"))
                {
                    globalTelescope = true;
                    telescope = telescope.Replace("Global_", "");

                    // Set UI based on detected telescope
                    CheckBox_KeywordUpdateTab_Telescope_Riccardi.Checked =
                        TelescopeService.HasRiccardiReducer(telescope);

                    var detectedTelescope = TelescopeService.DetectTelescope(telescope);
                    if (detectedTelescope != null && _telescopeRadioButtons!.TryGetValue(detectedTelescope, out var radioButton))
                        radioButton.Checked = true;

                    ApplyTelescopeToFile(file);
                }
            }
        }

        FindTelescope();
    }
}
