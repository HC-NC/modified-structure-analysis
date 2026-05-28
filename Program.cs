using modified_structure_analysis.Forms;
using modified_structure_analysis.Config;
using OSGeo.GDAL;
using MaxRev.Gdal.Core;
using System.Globalization;
using System.Threading;

namespace modified_structure_analysis
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            GdalBase.ConfigureAll();
            Gdal.AllRegister();

            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            // Apply saved language before any form is created
            var lang = AppSettings.Instance.Language;
            if (!string.IsNullOrEmpty(lang))
            {
                try
                {
                    var culture = new CultureInfo(lang);
                    Thread.CurrentThread.CurrentCulture = culture;
                    Thread.CurrentThread.CurrentUICulture = culture;
                    CultureInfo.DefaultThreadCurrentCulture = culture;
                    CultureInfo.DefaultThreadCurrentUICulture = culture;
                }
                catch { }
            }

            Application.Run(new MainForm());
        }
    }
}