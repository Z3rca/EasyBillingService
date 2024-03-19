using Microsoft.Office.Interop.Excel;

namespace EasyBillingService.Extensions
{
    public static class ExcelSheetExtensions
    {
        public static T GetCellValue<T>(this Worksheet worksheet, string cellID)
        {
            var cellObject = (T)worksheet.Range[cellID, cellID].Value;

            return cellObject;

        }

        public static void SetCellValue<T>(this Worksheet worksheet, string cellID, object value)
        {
            var typedObject = (T)value;
            
            worksheet.Range[cellID, cellID].Value = typedObject;
        }
    }
}