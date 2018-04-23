using System;
using DevExpress.ExpressApp;
using DevExpress.XtraPrinting;
using DevExpress.ExpressApp.Reports.Win;
using DevExpress.ExpressApp.Printing.Win;

namespace WinSolution.Module.Win {
    public class ConfigurePrintingSystemViewController : ViewController {
        private WinReportServiceController reportService;
        private PrintingController printingService;
        protected override void OnActivated() {
            base.OnActivated();
            reportService = Frame.GetController<WinReportServiceController>();
            printingService = Frame.GetController<PrintingController>();
            if (reportService != null)
                reportService.CustomShowPreview += reportService_CustomShowPreview;
            if (printingService != null)
                printingService.PrintingSettingsLoaded += printingService_PrintingSettingsLoaded;
        }
        private void printingService_PrintingSettingsLoaded(object sender, PrintableComponentLinkEventArgs e) {
            ConfigurePrintingSystem(e.PrintableComponentLink.PrintingSystem);
        }
        private void reportService_CustomShowPreview(object sender, CustomShowPreviewEventArgs e) {
            ConfigurePrintingSystem(e.Report.PrintingSystem);
        }
        private void ConfigurePrintingSystem(PrintingSystem printingSystem) {
            SetGeneralOptions(printingSystem.ExportOptions.Html);
            SetHtmlOptions(printingSystem.ExportOptions.Html);
            SetPdfOptions(printingSystem.ExportOptions.Pdf);
            SetXlsOptions(printingSystem.ExportOptions.Xls);
        }
        private static void SetXlsOptions(XlsExportOptions xlsExportOptions) {
            //http://www.devexpress.com/Help/?document=XtraData/DevExpressXtraPrintingXlsExportOptionsMembersTopicAll.htm
        }
        private static void SetPdfOptions(PdfExportOptions pdfExportOptions) {
            //http://www.devexpress.com/Help/?document=XtraData/DevExpressXtraPrintingPdfExportOptionsMembersTopicAll.htm
        }
        private static void SetHtmlOptions(HtmlExportOptions htmlExportOptions) {
            //http://www.devexpress.com/Help/?document=XtraData/DevExpressXtraPrintingHtmlExportOptionsMembersTopicAll.htm
        }
        private static void SetGeneralOptions(HtmlExportOptions htmlExportOptions) {
            //http://www.devexpress.com/Help/?document=XtraData/DevExpressXtraPrintingXlsExportOptionsBaseMembersTopicAll.htm
            //http://www.devexpress.com/Help/?document=XtraPrinting/DevExpressXtraPrintingPrintingSystemMembersTopicAll.htm
        }
        protected override void OnDeactivating() {
            if (reportService != null)
                reportService.CustomShowPreview -= reportService_CustomShowPreview;
            if (printingService != null)
                printingService.PrintingSettingsLoaded -= printingService_PrintingSettingsLoaded;
            base.OnDeactivating();
        }
    }
}