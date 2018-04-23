Imports DevExpress.ExpressApp
Imports DevExpress.XtraPrinting
Imports DevExpress.XtraReports.UI
Imports DevExpress.ExpressApp.Reports.Win
Imports DevExpress.ExpressApp.Win.SystemModule


Namespace WinSolution.Module.Win
    Public Class ConfigurePrintingSystemViewController
        Inherits ViewController

        Private reportService As WinReportServiceController
        Private printingService As PrintingController
        Protected Overrides Sub OnActivated()
            MyBase.OnActivated()
            reportService = Frame.GetController(Of WinReportServiceController)()
            printingService = Frame.GetController(Of PrintingController)()
            If reportService IsNot Nothing Then
                AddHandler reportService.CustomShowPreview, AddressOf reportService_CustomShowPreview
            End If
            If printingService IsNot Nothing Then
                AddHandler printingService.PrintingSettingsLoaded, AddressOf printingService_PrintingSettingsLoaded
            End If
        End Sub
        Private Sub printingService_PrintingSettingsLoaded(ByVal sender As Object, ByVal e As PrintableComponentLinkEventArgs)
            ConfigurePrintingSystem(e.PrintableComponentLink.PrintingSystem)
        End Sub
        Private Sub reportService_CustomShowPreview(ByVal sender As Object, ByVal e As CustomShowPreviewEventArgs)
            e.Report.CreateDocument()
            ConfigurePrintingSystem(e.Report.PrintingSystem)
            e.Report.ShowPreviewDialog()
            e.Handled = True
        End Sub
        Private Sub ConfigurePrintingSystem(ByVal printingSystem As PrintingSystemBase)
            SetHtmlOptions(printingSystem.ExportOptions.Html)
            SetPdfOptions(printingSystem.ExportOptions.Pdf)
            SetXlsOptions(printingSystem.ExportOptions.Xls)
            SetGeneralOptions(printingSystem.ExportOptions.PrintPreview)
        End Sub
        Private Shared Sub SetXlsOptions(ByVal xlsExportOptions As XlsExportOptions)
            ' XLS-specific options:
            xlsExportOptions.SheetName = "CustomXlsSheetTitle"
            xlsExportOptions.ShowGridLines = True
        End Sub
        Private Shared Sub SetPdfOptions(ByVal pdfExportOptions As PdfExportOptions)
            ' PDF-specific options:
            pdfExportOptions.DocumentOptions.Title = "CustomPdfTitle"
            pdfExportOptions.ImageQuality = PdfJpegImageQuality.Medium
        End Sub
        Private Shared Sub SetHtmlOptions(ByVal htmlExportOptions As HtmlExportOptions)
            ' HTML-specific options:
            htmlExportOptions.Title = "CustomHtmlTitle"
            htmlExportOptions.ExportMode = HtmlExportMode.SingleFilePageByPage
            htmlExportOptions.PageBorderColor = System.Drawing.Color.Gray
            htmlExportOptions.EmbedImagesInHTML = True
        End Sub
        Private Shared Sub SetGeneralOptions(ByVal printPreviewOptions As PrintPreviewOptions)
            ' General options:
            printPreviewOptions.DefaultFileName = "CustomFileName"
            ' Uncomment the following line to disable the "Export Options" dialog:
            'printPreviewOptions.ShowOptionsBeforeExport = false;
        End Sub
        Protected Overrides Sub OnDeactivated()
            If reportService IsNot Nothing Then
                RemoveHandler reportService.CustomShowPreview, AddressOf reportService_CustomShowPreview
            End If
            If printingService IsNot Nothing Then
                RemoveHandler printingService.PrintingSettingsLoaded, AddressOf printingService_PrintingSettingsLoaded
            End If
            MyBase.OnDeactivated()
        End Sub
    End Class
End Namespace