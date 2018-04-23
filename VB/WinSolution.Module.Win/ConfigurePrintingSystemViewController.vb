Imports Microsoft.VisualBasic
Imports System
Imports DevExpress.ExpressApp
Imports DevExpress.XtraPrinting
Imports DevExpress.ExpressApp.Reports.Win
Imports DevExpress.ExpressApp.Printing.Win

Namespace WinSolution.Module.Win
	Public Class ConfigurePrintingSystemViewController
		Inherits ViewController
		Private reportService As WinReportServiceController
		Private printingService As PrintingController
		Protected Overrides Overloads Sub OnActivated()
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
			ConfigurePrintingSystem(e.Report.PrintingSystem)
		End Sub
		Private Sub ConfigurePrintingSystem(ByVal printingSystem As PrintingSystem)
			SetGeneralOptions(printingSystem.ExportOptions.Html)
			SetHtmlOptions(printingSystem.ExportOptions.Html)
			SetPdfOptions(printingSystem.ExportOptions.Pdf)
			SetXlsOptions(printingSystem.ExportOptions.Xls)
		End Sub
		Private Shared Sub SetXlsOptions(ByVal xlsExportOptions As XlsExportOptions)
			'http://www.devexpress.com/Help/?document=XtraData/DevExpressXtraPrintingXlsExportOptionsMembersTopicAll.htm
		End Sub
		Private Shared Sub SetPdfOptions(ByVal pdfExportOptions As PdfExportOptions)
			'http://www.devexpress.com/Help/?document=XtraData/DevExpressXtraPrintingPdfExportOptionsMembersTopicAll.htm
		End Sub
		Private Shared Sub SetHtmlOptions(ByVal htmlExportOptions As HtmlExportOptions)
			'http://www.devexpress.com/Help/?document=XtraData/DevExpressXtraPrintingHtmlExportOptionsMembersTopicAll.htm
		End Sub
		Private Shared Sub SetGeneralOptions(ByVal htmlExportOptions As HtmlExportOptions)
			'http://www.devexpress.com/Help/?document=XtraData/DevExpressXtraPrintingXlsExportOptionsBaseMembersTopicAll.htm
			'http://www.devexpress.com/Help/?document=XtraPrinting/DevExpressXtraPrintingPrintingSystemMembersTopicAll.htm
		End Sub
		Protected Overrides Overloads Sub OnDeactivating()
			If reportService IsNot Nothing Then
				RemoveHandler reportService.CustomShowPreview, AddressOf reportService_CustomShowPreview
			End If
			If printingService IsNot Nothing Then
				RemoveHandler printingService.PrintingSettingsLoaded, AddressOf printingService_PrintingSettingsLoaded
			End If
			MyBase.OnDeactivating()
		End Sub
	End Class
End Namespace