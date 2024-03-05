using System.Windows.Forms;

namespace Utility
{
    internal static class ToolTips
    {
        public static Control AddToolTip(this Control control, string title, string text)
        {
            ToolTip toolTip = new ToolTip
            {
                ToolTipIcon = ToolTipIcon.Info,
                IsBalloon = false,
                ShowAlways = true,
                ToolTipTitle = title,
                AutomaticDelay = 2000,
            };

            toolTip.SetToolTip(control, text);

            return control;
        }
    }
}