Imports Microsoft.VisualBasic
Imports System
Imports System.Configuration
Imports System.Windows.Forms
Imports DevExpress.ExpressApp.Security
Imports WinSolution.Module

Namespace WinSolution.Win
	Friend NotInheritable Class Program
		''' <summary>
		''' The main entry point for the application.
		''' </summary>
		Private Sub New()
		End Sub
		<STAThread> _
		Shared Sub Main()
#If EASYTEST Then
			DevExpress.ExpressApp.EasyTest.WinAdapter.RemotingRegistration.Register(4100)
#End If

			Application.EnableVisualStyles()
			Application.SetCompatibleTextRenderingDefault(False)
			EditModelPermission.AlwaysGranted = System.Diagnostics.Debugger.IsAttached
			MyDetailsController.CanShowMyDetails = True
			ObjectAccessComparer.AllowModifyCurrentUserObjectDefault = True
			ObjectAccessComparer.AllowNavigateToCurrentUserObjectDefault = True
			Dim winApplication As New WinSolutionWindowsFormsApplication()
#If EASYTEST Then
			If ConfigurationManager.ConnectionStrings("EasyTestConnectionString") IsNot Nothing Then
				winApplication.ConnectionString = ConfigurationManager.ConnectionStrings("EasyTestConnectionString").ConnectionString
			End If
#End If
			winApplication.ConnectionString = CodeCentralExampleInMemoryDataStoreProvider.ConnectionString
			If ConfigurationManager.ConnectionStrings("ConnectionString") IsNot Nothing Then
				winApplication.ConnectionString = ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString
			End If
			Try
				winApplication.Setup()
				winApplication.Start()
			Catch e As Exception
				winApplication.HandleException(e)
			End Try
		End Sub
	End Class
End Namespace
