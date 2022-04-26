using Magicodes.ExporterAndImporter.Core;
using Magicodes.ExporterAndImporter.Excel;
using Magicodes.ExporterAndImporter.Html;
using Magicodes.ExporterAndImporter.Pdf;
using Magicodes.ExporterAndImporter.Word;

namespace Blazor100.Service
{
    /// <summary>
    /// 通用导入导出服务类
    /// </summary>
    public class ImportExportsService
    {
        public enum ExportType
        {
            Excel,
            Pdf,
            Word,
            Html
        }

        public async Task<string> ExportToExcel<T>(string filePath, List<T>? items = null, ExportType exportType = ExportType.Excel) where T : class, new()
        {
            switch (exportType)
            {
                case ExportType.Pdf:
                    var exporterPdf = new PdfExporter();
                    items = items ?? new List<T>();
                    var resultPdf = await exporterPdf.ExportListByTemplate(filePath + ".pdf", items);
                    return resultPdf.FileName;
                case ExportType.Word:
                    var exporterWord = new WordExporter();
                    items = items ?? new List<T>();
                    var resultWord = await exporterWord.ExportListByTemplate(filePath + ".docx", items);
                    return resultWord.FileName;
                case ExportType.Html:
                    var exporterHtml = new HtmlExporter();
                    items = items ?? new List<T>();
                    var resultHtml = await exporterHtml.ExportListByTemplate(filePath + ".html", items);
                    return resultHtml.FileName;
                default:
                    IExporter exporter = new ExcelExporter();
                    items = items ?? new List<T>();
                    var result = await exporter.Export(filePath + ".xlsx", items);
                    return result.FileName;
            }
        }

        public async Task<(IEnumerable<T>? items,string error)> ImportFormExcel<T>(string filePath) where T : class, new()
        {
            IExcelImporter Importer = new ExcelImporter();
            var import = await Importer.Import<T>(filePath);
            if (import.Data == null ) 
            {
                return (null, import.Exception.Message);
            }
            return (import.Data!.ToList(),""); 
        }

    }
}
