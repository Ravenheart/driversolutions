using DriverSolutions.BOL.Managers.ModuleDispatches;
using DriverSolutions.BOL.Managers.ModuleFinance;
using DriverSolutions.BOL.Managers.ModuleSystem;
using DriverSolutions.Core;
using DriverSolutions.ModuleDispatches;
using DriverSolutions.ModuleFinance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DriverSolutions
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            DevExpress.UserSkins.BonusSkins.Register();
            DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle(GLOB.Settings.Get<string>(2));

            GLOB.ConfigureMap();

            if (Updater.CheckVersion())
                Updater.UpdateApplication();

            if (XF_Login.ShowWindow() == DialogResult.Yes)
            {
                var dispManager = DispatchManager.CreateEmpty();
                Application.Run(new XF_Dispatches(dispManager));
            }

            //XF_TestForm.ShowWindow();
        }
    }
}
