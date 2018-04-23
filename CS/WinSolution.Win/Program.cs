using System;
using System.Windows.Forms;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.Xpo;

namespace WinSolution.Win {
    static class Program {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            EditModelPermission.AlwaysGranted = System.Diagnostics.Debugger.IsAttached;
            MyDetailsController.CanGenerateMyDetailsNavigationItem = true;
            ObjectAccessComparer.AllowModifyCurrentUserObjectDefault = true;
            ObjectAccessComparer.AllowNavigateToCurrentUserObjectDefault = true;
            WinSolutionWindowsFormsApplication winApplication = new WinSolutionWindowsFormsApplication();
            try {
                InMemoryDataStoreProvider.Register();
                winApplication.ConnectionString = InMemoryDataStoreProvider.ConnectionString;
                winApplication.Setup();
                winApplication.Start();
            }
            catch (Exception e) {
                winApplication.HandleException(e);
            }
        }
    }
}
