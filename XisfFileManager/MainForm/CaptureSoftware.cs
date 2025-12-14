using System.Drawing;
using System.Windows.Forms;
using XisfFileManager.Files;
using XisfFileManager.Helpers;
using XisfFileManager.Models;
using XisfFileManager.Models.CaptureSoftware;
using XisfFileManager.Services;

namespace XisfFileManager;

public partial class MainForm
{
    // Capture software radio button mappings
    private Dictionary<CaptureSoftwareConfiguration, RadioButton>? _softwareRadioButtons;

    private void InitializeCaptureSoftwareMappings()
    {
        _softwareRadioButtons = new Dictionary<CaptureSoftwareConfiguration, RadioButton>
        {
            { NinaSoftware.Instance, RadioButton_KeywordUpdateTab_CaptureSoftware_NINA },
            { TheSkyXSoftware.Instance, RadioButton_KeywordUpdateTab_CaptureSoftware_TheSkyX },
            { SequenceGeneratorProSoftware.Instance, RadioButton_KeywordUpdateTab_CaptureSoftware_SGPro },
            { VoyagerSoftware.Instance, RadioButton_KeywordUpdateTab_CaptureSoftware_Voyager },
            { SharpCapSoftware.Instance, RadioButton_KeywordUpdateTab_CaptureSoftware_SharpCap }
        };
    }

    public void ClearCaptureSoftwareGroup()
    {
        if (_softwareRadioButtons == null)
            InitializeCaptureSoftwareMappings();

        // Reset all radio buttons
        foreach (var rb in _softwareRadioButtons!.Values)
            UIHelpers.ResetRadioButton(rb);

        // Reset buttons
        UIHelpers.ResetControlColors(
            Button_KeywordUpdateTab_CaptureSoftware_SetAll,
            Button_KeywordUpdateTab_CaptureSoftware_SetByFile);
    }

    private void FindCaptureSoftware()
    {
        if (_softwareRadioButtons == null)
            InitializeCaptureSoftwareMappings();

        var analysis = CaptureSoftwareService.AnalyzeSoftware(mFileList);

        // Update radio buttons
        foreach (var (software, radioButton) in _softwareRadioButtons!)
        {
            radioButton.ForeColor = CaptureSoftwareService.GetRadioButtonColor(software, analysis);
            radioButton.Checked = CaptureSoftwareService.ShouldBeChecked(software, analysis);
        }

        // Update button colors
        var buttonColor = CaptureSoftwareService.GetButtonColor(analysis);
        Button_KeywordUpdateTab_CaptureSoftware_SetAll.ForeColor = buttonColor;
        Button_KeywordUpdateTab_CaptureSoftware_SetByFile.ForeColor = buttonColor;
    }

    private CaptureSoftwareConfiguration? GetSelectedCaptureSoftware() =>
        _softwareRadioButtons?.FirstOrDefault(kv => kv.Value.Checked).Key;

    private void Button_CaptureSoftware_SetAll_Click(object sender, EventArgs e)
    {
        var selected = GetSelectedCaptureSoftware();
        if (selected == null) return;

        foreach (XisfFile file in mFileList)
        {
            if (!file.CaptureSoftware.Equals(selected.Identifier))
                selected.ApplyKeyword(file);
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
                if (string.IsNullOrEmpty(file.CaptureSoftware))
                    file.AddKeyword("SWCREATE", captureSoftware, "XISF File Manager");
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
