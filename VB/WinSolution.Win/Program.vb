Imports DevExpress.ExpressApp.Security
Imports DevExpress.ExpressApp.Xpo

Namespace WinSolution.Win
    Friend NotInheritable Class Program

        Private Sub New()
        End Sub

        ''' <summary>
        ''' The main entry point for the application.
        ''' </summary>
        <STAThread>
        Shared Sub Main()

            Application.EnableVisualStyles()
            Application.SetCompatibleTextRenderingDefault(False)
            EditModelPermission.AlwaysGranted = System.Diagnostics.Debugger.IsAttached
            MyDetailsController.CanGenerateMyDetailsNavigationItem = True
            ObjectAccessComparer.AllowModifyCurrentUserObjectDefault = True
            ObjectAccessComparer.AllowNavigateToCurrentUserObjectDefault = True
            Dim winApplication As New WinSolutionWindowsFormsApplication()
            Try
                InMemoryDataStoreProvider.Register()
                winApplication.ConnectionString = InMemoryDataStoreProvider.ConnectionString
                winApplication.Setup()
                winApplication.Start()
            Catch e As Exception
                winApplication.HandleException(e)
            End Try
        End Sub
    End Class
End Namespace
