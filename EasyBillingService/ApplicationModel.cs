using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using EasyBillingService.BillingBookConfiguration;
using EasyBillingService.Extensions;
using Microsoft.Office.Interop.Excel;

namespace EasyBillingService
{
    public class ApplicationModel
    {
        private string _billingBookPath;
        private string _templatePath;
        public const string CONFIGURATIONFILEPATH = "..\\..\\configuration.cfg";

        private string _lastOpenedFileText = "lastOpenedFile = ";
        private string _templatePathText = "templatePath = ";

        private TADBillingBookConfiguration _tadBillingBookConfiguration = new TADBillingBookConfiguration();


        public string LastOpenedBillingAdressBook =>  String.IsNullOrEmpty(_billingBookPath) ? "":_billingBookPath;
        public string TemplatePath =>  String.IsNullOrEmpty(_templatePath) ? "":_templatePath;
        private List<Entry> _billingAdressData =new List<Entry>();
        public double CurrentBillingAddress { get; private set; }

        public FileEntry SelectedTemplate { get; private set; }

        public List<Entry> BillingAddressData
        {
            get
            {
                if (_billingAdressData != null)
                {
                    return _billingAdressData;
                }
                else
                {
                    _billingAdressData = RetrieveBillingEntriesFromExcelsheet(_billingBookPath);
                    return _billingAdressData;
                }  
            }
        }

        public ApplicationModel()
        {
            InitializeFormerFilePaths();
            RefreshValues();
        }
        
        private void InitializeFormerFilePaths()
        {
            if (!File.Exists(CONFIGURATIONFILEPATH))
            { 
                //TODO handle exception
                return;
            } 
                
            using (File.OpenRead(CONFIGURATIONFILEPATH))
            {
                var lines = File.ReadLines(CONFIGURATIONFILEPATH);

                foreach (var line in lines)
                {
                    if (line.StartsWith(_lastOpenedFileText))
                    {
                        _billingBookPath = line.Substring(_lastOpenedFileText.ToCharArray().Length);
                    }
                    if (line.StartsWith(_templatePathText))
                    {
                        _templatePath = line.Substring(_templatePathText.ToCharArray().Length);
                    }
                        
                }
            }
        }
        

        private static List<Entry> RetrieveBillingEntriesFromExcelsheet(string path)
        {
            var entries = new List<Entry>();
            var excelApplication = new Microsoft.Office.Interop.Excel.Application();
            Workbook workbook = excelApplication.Workbooks.Open(path);
            var sheet = workbook.ActiveSheet as Worksheet;
            if (sheet == null)
            {
                //TODO handle exception
                
                return entries;
            }
            var grid = sheet.Range["A1", "C1000"].Value as object[,];
            
            for (int  i = 1;  i < grid.GetLength(0); i++)
            {
                double value = 0;
                if(grid[i,1] is DateTime) continue;
                if (grid[i, 1] is double)
                {
                    value = (double)grid[i, 1];
                }
                else
                {
                    if(!double.TryParse((String)grid[i, 1], out value))
                    {
                        continue;
                    }
                }
                    
                
                var date = (DateTime)grid[i, 2];

                var cellAdress = "A" + i; // this is dirty, I think there needs to be a better solution
                var entry = new Entry(value, date, (string)grid[i, 3], cellAdress);
                entries.Add(entry);
            }

            entries = entries.OrderByDescending(x => x.Id).ToList();
            grid = null;
            sheet = null;
            // workbook.Save();
            workbook.Close( false);
            workbook = null;
            excelApplication.Quit();
            excelApplication = null;
            return entries;
        }

        public Entry? RetrieveLastBillingEntry()
        {
            var path = _billingBookPath;

            if (string.IsNullOrEmpty(path))
            {
                return null;
            }

            var entries = _billingAdressData;
            WaitForCleanUp();


            if (!entries.Any())
            {
                return null;
            }

            var newestEntry = entries[0];
            CurrentBillingAddress = newestEntry.Id + 1;
            
            return newestEntry;

        }

        private static bool WaitForCleanUp()
        {
            do
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
            while (Marshal.AreComObjectsAvailableForCleanup());

            return true;
        }
        
        public List<FileEntry> RetrieveTemplateList()
        {
            var entries = new List<FileEntry>();
            if(Directory.Exists(_templatePath))
            {
                var filePaths = Directory.GetFiles(_templatePath);

                foreach (var path in filePaths)
                {
                    var entry = new FileEntry(path);
                    entries.Add(entry);
                }
            }

            return entries;
        }
        
        public void SetBillingBookPath(string path)
        {
            var newValue = _lastOpenedFileText+ path;
            var oldValue = _lastOpenedFileText + (_billingBookPath);
            if (File.Exists(CONFIGURATIONFILEPATH))
            {
                var text = "";
                using (var stream = File.OpenRead(CONFIGURATIONFILEPATH))
                {
                    text = File.ReadAllText(CONFIGURATIONFILEPATH);
                }
                
                if (text.Contains(oldValue))
                {
                    text = text.Replace(oldValue,newValue);
                }
                else
                {
                    text += "\n"+ newValue;
                }
                File.WriteAllText(CONFIGURATIONFILEPATH,text);
            }

            _billingBookPath = path;
        }
        
        public void SetTemplateFolderPath(string path)
        {
            var newValue = _templatePathText+ path;
            var oldValue = _templatePathText + (_templatePath);
            if (File.Exists(CONFIGURATIONFILEPATH))
            {
                var text = "";
                using (var stream = File.OpenRead(CONFIGURATIONFILEPATH))
                {
                    text = File.ReadAllText(CONFIGURATIONFILEPATH);
                }

                if (text.Contains(oldValue))
                {
                    text = text.Replace(oldValue,newValue);
                }
                else
                {
                    text += "\n"+ newValue;
                }
                
                File.WriteAllText(CONFIGURATIONFILEPATH,text);
            }

            _templatePath = path;
        }

        public void setSelectedTemplate(FileEntry selectedItem)
        {
            SelectedTemplate = selectedItem;
        }
        
        private void CreateNewWorkBook(string path)
        {
            
            var entries = new List<Entry>();
            var excelApplication = new Microsoft.Office.Interop.Excel.Application();
            Workbook newWorkbook = excelApplication.Workbooks.Add(SelectedTemplate.Path);

            var sheet = newWorkbook.ActiveSheet as Worksheet;
            var date = DateTime.Now;
            modifyWorksheet(ref sheet, date,CurrentBillingAddress);
            var billingBook = excelApplication.Workbooks.Add(_billingBookPath);
            var billingBookSheet = billingBook.ActiveSheet as Worksheet;

            var cell = _tadBillingBookConfiguration.RetrieveCell(billingBookSheet, _billingAdressData, CurrentBillingAddress, date);
            var structure = _tadBillingBookConfiguration.GetRowStructure();
            EnterNewAdressToBillingBook(billingBookSheet,cell,date,CurrentBillingAddress, structure);
            billingBook.SaveAs(_billingBookPath);
            billingBook.Close(false);
            newWorkbook.SaveAs(path);
            newWorkbook.Close( false);
            newWorkbook = null;
            excelApplication.Quit();
            excelApplication = null;
        }

        private void EnterNewAdressToBillingBook(Worksheet billingBookSheet,string target, DateTime dateTime, double billingId, (string billingID, string date, string sum, string recipient) structure)
        {
            var rowNumber = 0;
            if (!int.TryParse(target.Substring(1), out rowNumber))
            {
                //Handle exception
                return;
            }

            var content = new String[4]{(structure.billingID + rowNumber),(structure.date + rowNumber),(structure.sum + rowNumber),(structure.recipient + rowNumber)};

            var containsValues = false;
            foreach (var cell in content)
            {
                var obj = billingBookSheet.GetCellValue<object>(cell);
                if (obj != null)
                {
                    containsValues = true;
                    break;
                }
            }
            
            if (containsValues)
            {
                var row = (Range) billingBookSheet.Rows[rowNumber];
                row.Insert(); 
            }
            billingBookSheet.SetCellValue<double>(structure.billingID+rowNumber,billingId);
            billingBookSheet.SetCellValue<DateTime>(structure.date+rowNumber,dateTime);
        }


        private void modifyWorksheet(ref Worksheet sheet, DateTime dateTime, double address)
        {
            
            sheet.SetCellValue<double>("B17",address);
            sheet.SetCellValue<DateTime>("B18",dateTime);
            
        }

        public void CreateNewBilling(string path)
        {
            CreateNewWorkBook(path);
            WaitForCleanUp();
            RefreshValues();
        }

        private void RefreshValues()
        {
            _billingAdressData = RetrieveBillingEntriesFromExcelsheet(_billingBookPath);
        }
    }
    
    
    public struct Entry
    {
        public string CellId;
        public int RowNumber;
        public Double Id;
        public DateTime DateTime;
        public String Recipient;

        public String dateText;
        public Entry(double id, DateTime date, string recipient, string cellId)
        {
            CellId = cellId;
            this.Id = id;
            DateTime = date;
            this.Recipient = recipient;
            var rowNumber = 0;
            int.TryParse(cellId.Substring(1),out rowNumber);
            RowNumber = rowNumber;
            
            DateTimeFormatInfo fmt = (new CultureInfo("hr-HR")).DateTimeFormat;
            dateText = DateTime.ToString("d", fmt);
        }
    }

    public struct FileEntry
    {
        public string Path;
        public string FileName;
        public string FileType;
        
        public FileEntry(string path)
        {
            Path = path;
            FileType = path.Split('.')[1];
            var array = path.Split('\\');
            FileName = array[array.Length - 1].Split('.')[0];
        }

        public string FullFileName()
        {
            return FileName + "." + FileType;
        }
        public override string ToString()
        {
            return this.FileName;
        }
    }
}
