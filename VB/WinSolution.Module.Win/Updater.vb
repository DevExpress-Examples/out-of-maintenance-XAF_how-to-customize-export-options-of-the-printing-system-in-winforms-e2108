Imports Microsoft.VisualBasic
Imports System
Imports DevExpress.Xpo
Imports DevExpress.ExpressApp
Imports DevExpress.Data.Filtering
Imports DevExpress.ExpressApp.Reports
Imports DevExpress.ExpressApp.Updating

Namespace WinSolution.Module.Win
    Public Class Updater
        Inherits ModuleUpdater
        Public Sub New(ByVal session As Session, ByVal currentDBVersion As Version)
            MyBase.New(session, currentDBVersion)
        End Sub
        Public Overrides Sub UpdateDatabaseAfterUpdateSchema()
            MyBase.UpdateDatabaseAfterUpdateSchema()
            Dim reportName As String = "TestReport"
            Dim resources() As String = Me.GetType().Assembly.GetManifestResourceNames()
            Dim reportResourceName As String = Array.Find(Of String)(resources, AddressOf IsTestReport)
            Dim reportdata As ReportData = Session.FindObject(Of ReportData)(New BinaryOperator("Name", reportName))
            If reportdata Is Nothing AndAlso (Not String.IsNullOrEmpty(reportResourceName)) Then
                reportdata = New ReportData(Session)
                Dim rep As New XafReport()
                rep.ReportName = reportName
                rep.ObjectSpace = New ObjectSpace(New UnitOfWork(Session.DataLayer), XafTypesInfo.Instance)
                rep.LoadLayout(Me.GetType().Assembly.GetManifestResourceStream(reportResourceName))
                reportdata.SaveXtraReport(rep)
                reportdata.Save()
            End If
        End Sub
        Private Function IsTestReport(ByVal reportName As String) As Boolean
            Return reportName.EndsWith("TestReport.repx")
        End Function
    End Class
End Namespace