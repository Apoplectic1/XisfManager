using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Windows.Forms;

[assembly: SuppressMessage("Microsoft.Globalization", "CA1304:SpecifyIFormatProvider", Justification = "This code is for personal use and does not require culture-specific formatting.")]
[assembly: SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", Justification = "This code is for personal use and does not require culture-specific formatting.")]
[assembly: SuppressMessage("Microsoft.Globalization", "CA1309:SpecifyIFormatProvider", Justification = "This code is for personal use and does not require culture-specific formatting.")]
[assembly: SuppressMessage("Microsoft.Globalization", "CA1310:SpecifyIFormatProvider", Justification = "This code is for personal use and does not require culture-specific formatting.")]
[assembly: SuppressMessage("Microsoft.Globalization", "CA1311:SpecifyIFormatProvider", Justification = "This code is for personal use and does not require culture-specific formatting.")]
[assembly: SuppressMessage("Microsoft.Globalization", "CA1707:SpecifyIFormatProvider", Justification = "This code is for personal use and does not require culture-specific formatting.")]


namespace XisfFileManager
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en-US");
            CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("en-US");

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}

