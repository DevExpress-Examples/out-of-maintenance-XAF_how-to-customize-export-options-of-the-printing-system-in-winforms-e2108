using System;
using DevExpress.ExpressApp;
using DevExpress.XtraPrinting;
using DevExpress.ExpressApp.Reports.Win;
using DevExpress.ExpressApp.Win.SystemModule;

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
        private void printingService_PrintingSettingsLoaded(
            object sender, PrintableComponentLinkEventArgs e) {
            ConfigurePrintingSystem(e.PrintableComponentLink.PrintingSystem);
        }
        private void reportService_CustomShowPreview(
            object sender, CustomShowPreviewEventArgs e) {
            e.Report.CreateDocument();
            ConfigurePrintingSystem(e.Report.PrintingSystem);
            e.Report.ShowPreviewDialog();
            e.Handled = true;
        }
        private void ConfigurePrintingSystem(PrintingSystemBase printingSystem) {
            SetHtmlOptions(printingSystem.ExportOptions.Html);
            SetPdfOptions(printingSystem.ExportOptions.Pdf);
            SetXlsOptions(printingSystem.ExportOptions.Xls);
            SetGeneralOptions(printingSystem.ExportOptions.PrintPreview);
        }
        private static void SetXlsOptions(XlsExportOptions xlsExportOptions) {
            // XLS-specific options:
            xlsExportOptions.SheetName = "CustomXlsSheetTitle";
            xlsExportOptions.ShowGridLines = true;
        }
        private static void SetPdfOptions(PdfExportOptions pdfExportOptions) {
            // PDF-specific options:
            pdfExportOptions.DocumentOptions.Title = "CustomPdfTitle";
            pdfExportOptions.ImageQuality = PdfJpegImageQuality.Medium;
        }
        private static void SetHtmlOptions(HtmlExportOptions htmlExportOptions) {
            // HTML-specific options:
            htmlExportOptions.Title = "CustomHtmlTitle";
            htmlExportOptions.ExportMode = HtmlExportMode.SingleFilePageByPage;
            htmlExportOptions.PageBorderColor = System.Drawing.Color.Gray;
            htmlExportOptions.EmbedImagesInHTML = true;
        }
        private static void SetGeneralOptions(PrintPreviewOptions printPreviewOptions) {
            // General options:
            printPreviewOptions.DefaultFileName = "CustomFileName";
            // Uncomment the following line to disable the "Export Options" dialog:
            //printPreviewOptions.ShowOptionsBeforeExport = false;
        }
        protected override void OnDeactivated() {
            if (reportService != null)
                reportService.CustomShowPreview -= reportService_CustomShowPreview;
            if (printingService != null)
                printingService.PrintingSettingsLoaded -= printingService_PrintingSettingsLoaded;
            base.OnDeactivated();
        }
    }
}