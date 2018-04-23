using System;
using DevExpress.Xpo;
using DevExpress.ExpressApp;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.Reports;
using DevExpress.ExpressApp.Updating;

namespace WinSolution.Module.Win {
    public class Updater : ModuleUpdater {
        public Updater(Session session, Version currentDBVersion) : base(session, currentDBVersion) { }
        public override void UpdateDatabaseAfterUpdateSchema() {
            base.UpdateDatabaseAfterUpdateSchema();
            string reportName = "TestReport";
            string[] resources = GetType().Assembly.GetManifestResourceNames();
            string reportResourceName = Array.Find<string>(resources, IsTestReport);
            ReportData reportdata = Session.FindObject<ReportData>(new BinaryOperator("Name", reportName));
            if (reportdata == null && !string.IsNullOrEmpty(reportResourceName)) {
                reportdata = new ReportData(Session);
                XafReport rep = new XafReport();
                rep.ReportName = reportName;
                rep.ObjectSpace = new ObjectSpace(new UnitOfWork(Session.DataLayer), XafTypesInfo.Instance);
                rep.LoadLayout(GetType().Assembly.GetManifestResourceStream(reportResourceName));
                reportdata.SaveXtraReport(rep);
                reportdata.Save();
            }
        }
        private bool IsTestReport(string reportName) {
            return reportName.EndsWith("TestReport.repx");
        }
    }
}