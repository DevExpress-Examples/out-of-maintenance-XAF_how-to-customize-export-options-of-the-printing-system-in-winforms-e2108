Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports DevExpress.Persistent.BaseImpl
Imports DevExpress.ExpressApp.Updating
Imports DevExpress.ExpressApp.Security

Namespace WinSolution.Module
    Public Class Updater
        Inherits ModuleUpdater

        Public Sub New(ByVal objectSpace As DevExpress.ExpressApp.IObjectSpace, ByVal currentDBVersion As Version)
            MyBase.New(objectSpace, currentDBVersion)
        End Sub
        Public Overrides Sub UpdateDatabaseAfterUpdateSchema()
            MyBase.UpdateDatabaseAfterUpdateSchema()
            If ObjectSpace.FindObject(Of Person)(New BinaryOperator("FullName", "Mary Tellitson")) Is Nothing Then
                Dim person1 As Person = ObjectSpace.CreateObject(Of Person)()
                person1.FirstName = "Mary"
                person1.LastName = "Tellitson"
                person1.Email = "tellitson@conpany.com"
            End If
            If ObjectSpace.FindObject(Of Person)(New BinaryOperator("FullName", "Robert King")) Is Nothing Then
                Dim person2 As Person = ObjectSpace.CreateObject(Of Person)()
                person2.FirstName = "Robert"
                person2.LastName = "King"
                person2.Email = "king@conpany.com"
            End If
        End Sub
    End Class
End Namespace
