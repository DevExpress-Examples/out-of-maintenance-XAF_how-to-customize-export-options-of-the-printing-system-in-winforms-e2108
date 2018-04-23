using System;
using DevExpress.Xpo;
using DevExpress.ExpressApp;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.Reports;
using DevExpress.ExpressApp.Updating;
using DevExpress.Persistent.BaseImpl;

namespace WinSolution.Module.Win {
    public class Updater : ModuleUpdater {
        public Updater(DevExpress.ExpressApp.IObjectSpace objectSpace, Version currentDBVersion) : base(objectSpace, currentDBVersion) { }
        public override void UpdateDatabaseAfterUpdateSchema() {
            base.UpdateDatabaseAfterUpdateSchema();
            string reportName = "TestReport";
            string[] resources = GetType().Assembly.GetManifestResourceNames();
            string reportResourceName = Array.Find<string>(resources, IsTestReport);
            ReportData reportdata = ObjectSpace.FindObject<ReportData>(new BinaryOperator("Name", reportName));
            if (reportdata == null && !string.IsNullOrEmpty(reportResourceName)) {
                reportdata = ObjectSpace.CreateObject<ReportData>();
                XafReport rep = new XafReport();
                rep.ReportName = reportName;
                rep.ObjectSpace = ObjectSpace;
                rep.LoadLayout(GetType().Assembly.GetManifestResourceStream(reportResourceName));
                reportdata.SaveReport(rep);
                reportdata.Save();
            }
        }
        private bool IsTestReport(string reportName) {
            return reportName.EndsWith("TestReport.repx");
        }
    }
}