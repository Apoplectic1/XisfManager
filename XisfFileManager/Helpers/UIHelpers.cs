using System.Drawing;
using System.Windows.Forms;

namespace XisfFileManager.Helpers;

/// <summary>
/// Common UI helper methods for control manipulation
/// </summary>
public static class UIHelpers
{
    /// <summary>
    /// Clears a ComboBox completely (DataSource, Text, and Items)
    /// </summary>
    public static void ClearComboBox(ComboBox comboBox)
    {
        comboBox.DataSource = null;
        comboBox.Text = string.Empty;
        comboBox.Items.Clear();
    }

    /// <summary>
    /// Clears a ComboBox and sets default text
    /// </summary>
    public static void ClearComboBox(ComboBox comboBox, string defaultText)
    {
        comboBox.DataSource = null;
        comboBox.Items.Clear();
        comboBox.Text = defaultText;
    }

    /// <summary>
    /// Resets a RadioButton to unchecked with black forecolor
    /// </summary>
    public static void ResetRadioButton(RadioButton radioButton)
    {
        radioButton.Checked = false;
        radioButton.ForeColor = Color.Black;
    }

    /// <summary>
    /// Resets a CheckBox to unchecked with black forecolor
    /// </summary>
    public static void ResetCheckBox(CheckBox checkBox)
    {
        checkBox.Checked = false;
        checkBox.ForeColor = Color.Black;
    }

    /// <summary>
    /// Resets multiple RadioButtons to unchecked with black forecolor
    /// </summary>
    public static void ResetRadioButtons(params RadioButton[] radioButtons)
    {
        foreach (var rb in radioButtons)
            ResetRadioButton(rb);
    }

    /// <summary>
    /// Resets multiple CheckBoxes to unchecked with black forecolor
    /// </summary>
    public static void ResetCheckBoxes(params CheckBox[] checkBoxes)
    {
        foreach (var cb in checkBoxes)
            ResetCheckBox(cb);
    }

    /// <summary>
    /// Sets forecolor to black for multiple controls
    /// </summary>
    public static void ResetControlColors(params Control[] controls)
    {
        foreach (var control in controls)
            control.ForeColor = Color.Black;
    }

    /// <summary>
    /// Sets forecolor for multiple controls
    /// </summary>
    public static void SetControlColors(Color color, params Control[] controls)
    {
        foreach (var control in controls)
            control.ForeColor = color;
    }
}
