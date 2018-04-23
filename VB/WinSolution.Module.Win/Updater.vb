Imports DevExpress.Xpo
Imports DevExpress.ExpressApp
Imports DevExpress.Data.Filtering
Imports DevExpress.ExpressApp.Reports
Imports DevExpress.ExpressApp.Updating
Imports DevExpress.Persistent.BaseImpl

Namespace WinSolution.Module.Win
    Public Class Updater
        Inherits ModuleUpdater

        Public Sub New(ByVal objectSpace As DevExpress.ExpressApp.IObjectSpace, ByVal currentDBVersion As Version)
            MyBase.New(objectSpace, currentDBVersion)
        End Sub
        Public Overrides Sub UpdateDatabaseAfterUpdateSchema()
            MyBase.UpdateDatabaseAfterUpdateSchema()
            Dim reportName As String = "TestReport"
            Dim resources() As String = Me.GetType().Assembly.GetManifestResourceNames()
            Dim reportResourceName As String = Array.Find(Of String)(resources, AddressOf IsTestReport)
            Dim reportdata As ReportData = ObjectSpace.FindObject(Of ReportData)(New BinaryOperator("Name", reportName))
            If reportdata Is Nothing AndAlso (Not String.IsNullOrEmpty(reportResourceName)) Then
                reportdata = ObjectSpace.CreateObject(Of ReportData)()
                Dim rep As New XafReport()
                rep.ReportName = reportName
                rep.ObjectSpace = ObjectSpace
                rep.LoadLayout(Me.GetType().Assembly.GetManifestResourceStream(reportResourceName))
                reportdata.SaveReport(rep)
                reportdata.Save()
            End If
        End Sub
        Private Function IsTestReport(ByVal reportName As String) As Boolean
            Return reportName.EndsWith("TestReport.repx")
        End Function
    End Class
End Namespace