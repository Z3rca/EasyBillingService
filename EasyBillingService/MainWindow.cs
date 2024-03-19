using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using EasyBillingService.Extensions;
using Microsoft.Office.Interop.Excel;
using MiniExcelLibs;
using MiniExcelLibs.Attributes;
using MiniExcelLibs.OpenXml;
using Application = Microsoft.Office.Interop.Excel.Application;
using Excel = Microsoft.Office.Interop.Excel;

namespace EasyBillingService
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        
        
        static void Test()
        {
           
            var excelApplication = new Microsoft.Office.Interop.Excel.Application();
            
            
			object missing = Missing.Value;
            var path = "H:\\C# Projects\\EasyBillingService\\EasyBillingService\\bin\\Debug\\";
            
			Workbook workbook = excelApplication.Workbooks.Open(path+"test.xlsx");



            var sheet = workbook.ActiveSheet as Worksheet;

            var cell = sheet.GetCellValue<double>("B2");
            cell++;
            sheet.SetCellValue<double>("B2", cell);
            
            workbook.Save();
            
            
            workbook.Close(SaveChanges: false);
            workbook = null;
            excelApplication.Quit();
            excelApplication = null;
            
            
		}

        private void button1_Click(object sender, EventArgs e)
        {
            
            Test();
            /*label1.Text = " clicked the button";

            var table = new List<Entry>();
            
            var config = new OpenXmlConfiguration()
            {
                TableStyles = MiniExcelLibs.OpenXml.TableStyles.None,
            };
            
            using (var stream = File.OpenRead("test.xlsx"))
            { 
                table = MiniExcel.Query<Entry>("test.xlsx").ToList();
                
           


                table = table.Where(entry => entry.Name != null).ToList();

                table[1].Value = 1;
            }

            MiniExcel.SaveAs("test.xlsx", table,true,"Sheet1",ExcelType.XLSX,null,true);*/
        

           // File.Replace("test_temp.xlsx", "test.xlsx", "test.xlsx", false);
            
           // File.Delete("test_temp.xlsx");
        }


        public class Entry
        {
            public String Name { get; set; }
            public int Value { get; set; }
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            
        }
        
        
        
       
    }
}