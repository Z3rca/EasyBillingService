using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Windows.Forms;
using EasyBillingService.Extensions;
using Microsoft.Office.Interop.Excel;

namespace EasyBillingService
{
    public partial class MainWindow : Form
    {
        private string _path;

        private string _lastOpenedFile;

        private const string CONFIGURATIONFILEPATH = "..\\..\\configuration.cfg";

        private string _lastOpenedFileText = "lastOpenedFile = ";

        private double currentID;
        
        public MainWindow()
        {
            InitializeComponent();
            InitializeFormerFilePaths();
            RetrieveLastBillingNumber();
        }

        private void InitializeFormerFilePaths()
        {
            var configurationPath = CONFIGURATIONFILEPATH;
            if (File.Exists(configurationPath))
            {
                
                using (var stream = File.OpenRead(configurationPath))
                {
                    var lines = File.ReadLines(configurationPath);

                    foreach (var line in lines)
                    {
                        if (line.StartsWith(_lastOpenedFileText))
                        {
                            _lastOpenedFile = line.Substring(_lastOpenedFileText.ToCharArray().Length);
                            break;
                        }
                    }
                }
            }
            
            textBox1.Text = _lastOpenedFile;
        }

        private void SetNewFilePath(string path)
        {
            var newValue = _lastOpenedFileText+ path;
            var oldValue = _lastOpenedFileText + (_lastOpenedFile);
            if (File.Exists(CONFIGURATIONFILEPATH))
            {
                var text = "";
                using (var stream = File.OpenRead(CONFIGURATIONFILEPATH))
                {
                    text = File.ReadAllText(CONFIGURATIONFILEPATH);
                }
                
                text = text.Replace(oldValue,newValue);
                File.WriteAllText(CONFIGURATIONFILEPATH,text);
            }

            _lastOpenedFile = path;
        }
        
        void RetrieveLastBillingNumber()
        {
           
            var excelApplication = new Microsoft.Office.Interop.Excel.Application();
            
            
            object missing = Missing.Value;
            var path = _lastOpenedFile;
            
            Workbook workbook = excelApplication.Workbooks.Open(path);

            DateTimeFormatInfo fmt = (new CultureInfo("hr-HR")).DateTimeFormat;

            var sheet = workbook.ActiveSheet as Worksheet;
            var billingIds = new List<(double idNumber, String date, String reciepient)>(); //ids are set here as numbers 
            
            var grid = sheet.Range["A1", "C1000"].Value as object[,];

            
            for (int  i = 1;  i < grid.GetLength(0); i++)
            {
                
                double value = 0;
                if(!double.TryParse((String)grid[i, 1], out value))
                {
                    continue;
                }
                
                var date = (DateTime)grid[i, 2];
                
                var entry = (value,date.ToString("d", fmt),(String)grid[i,3]);
                
                billingIds.Add(entry);
            }

            double maximum = 0;

            if (!billingIds.Any())
            {
                return;
            }
            var newestEntry = billingIds.OrderByDescending(x => x.idNumber).First();

            LastBillingNumber.Text = newestEntry.idNumber.ToString();

            RecipientText.Text = newestEntry.reciepient;

            DateText.Text = newestEntry.date;
            

            currentID = newestEntry.idNumber + 1;
            
            
            workbook.Save();
            workbook.Close(SaveChanges: false);
            workbook = null;
            excelApplication.Quit();
            excelApplication = null;
            
            
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
            var dialog = new System.Windows.Forms.OpenFileDialog();

            dialog.ShowDialog();
            
            textBox1.Text = dialog.FileName;
            

           SetNewFilePath(dialog.FileName);
        }

        private void SetValue_Click(object sender, EventArgs e)
        {
            RetrieveLastBillingNumber();
        }


        public class Entry
        {
            public String Name { get; set; }
            public int Value { get; set; }
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            
        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void BillingNumber_TextChanged(object sender, EventArgs e)
        {
            
        }
    }
}