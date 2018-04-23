using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Updating;
using DevExpress.ExpressApp.Security;

namespace WinSolution.Module {
    public class Updater : ModuleUpdater {
        public Updater(DevExpress.ExpressApp.IObjectSpace objectSpace, Version currentDBVersion) : base(objectSpace, currentDBVersion) { }
        public override void UpdateDatabaseAfterUpdateSchema() {
            base.UpdateDatabaseAfterUpdateSchema();
            if (ObjectSpace.FindObject<Person>(new BinaryOperator("FullName", "Mary Tellitson")) == null) {
                Person person1 = ObjectSpace.CreateObject<Person>();
                person1.FirstName = "Mary";
                person1.LastName = "Tellitson";
                person1.Email = "tellitson@conpany.com";
            }
            if (ObjectSpace.FindObject<Person>(new BinaryOperator("FullName", "Robert King")) == null) {
                Person person2 = ObjectSpace.CreateObject<Person>();
                person2.FirstName = "Robert";
                person2.LastName = "King";
                person2.Email = "king@conpany.com";
            }
        }
    }
}
