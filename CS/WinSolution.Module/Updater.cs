using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Updating;
using DevExpress.ExpressApp.Security;

namespace WinSolution.Module {
    public class Updater : ModuleUpdater {
        public Updater(Session session, Version currentDBVersion) : base(session, currentDBVersion) { }
        public override void UpdateDatabaseAfterUpdateSchema() {
            base.UpdateDatabaseAfterUpdateSchema();
            if (Session.FindObject<Person>(CriteriaOperator.Parse("FullName == 'Mary Tellitson'")) == null) {
                Person person1 = new Person(Session);
                person1.FirstName = "Mary";
                person1.LastName = "Tellitson";
                person1.Email = "tellitson@conpany.com";
                person1.Save();
            }
            if (Session.FindObject<Person>(CriteriaOperator.Parse("FullName == 'Robert King'")) == null)
            {
                Person person2 = new Person(Session);
                person2.FirstName = "Robert";
                person2.LastName = "King";
                person2.Email = "king@conpany.com";
                person2.Save();
            }
        }
    }
}
