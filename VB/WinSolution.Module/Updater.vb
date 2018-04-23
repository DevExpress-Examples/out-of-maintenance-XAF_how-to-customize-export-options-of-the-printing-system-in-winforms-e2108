Imports Microsoft.VisualBasic
Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports DevExpress.Persistent.BaseImpl
Imports DevExpress.ExpressApp.Updating
Imports DevExpress.ExpressApp.Security

Namespace WinSolution.Module
    Public Class Updater
        Inherits ModuleUpdater
        Public Sub New(ByVal session As Session, ByVal currentDBVersion As Version)
            MyBase.New(session, currentDBVersion)
        End Sub
        Public Overrides Sub UpdateDatabaseAfterUpdateSchema()
            MyBase.UpdateDatabaseAfterUpdateSchema()
            If Session.FindObject(Of Person)(CriteriaOperator.Parse("FullName == 'Mary Tellitson'")) Is Nothing Then
                Dim person1 As New Person(Session)
                person1.FirstName = "Mary"
                person1.LastName = "Tellitson"
                person1.Email = "tellitson@conpany.com"
                person1.Save()
            End If
            If Session.FindObject(Of Person)(CriteriaOperator.Parse("FullName == 'Robert King'")) Is Nothing Then
                Dim person2 As New Person(Session)
                person2.FirstName = "Robert"
                person2.LastName = "King"
                person2.Email = "king@conpany.com"
                person2.Save()
            End If
        End Sub
    End Class
End Namespace
