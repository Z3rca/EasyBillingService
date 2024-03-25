using System;
using System.Collections.Generic;
using System.Configuration;
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
        private TADBillingBookConfiguration _tadBillingBookConfiguration = new TADBillingBookConfiguration();

        private DataCacheManager _cacheManager = new DataCacheManager();
        private ValidationModel _validationManager = new ValidationModel();
        
        public string BillingBookPath => ApplicationConfiguration.ConfigurationFile.BillingBookPath;
        public string TemplatePath
        {
            get => ApplicationConfiguration.ConfigurationFile.TemplatePath;
            set => ApplicationConfiguration.ConfigurationFile.TemplatePath = value;
        }

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
                    _billingAdressData = RetrieveBillingEntriesFromExcelsheet(BillingBookPath);
                    return _billingAdressData;
                }  
            }
        }

        public string BillingPath
        {
            get => ApplicationConfiguration.ConfigurationFile.BillingPath;
            set => ApplicationConfiguration.ConfigurationFile.BillingPath = value;
        }

        public ApplicationModel()
        {
            RefreshValues();
        }
        

        private static List<Entry> RetrieveBillingEntriesFromExcelsheet(string path)
        {
            var entries = new List<Entry>();
            if (string.IsNullOrEmpty(path))
            {
                
                return entries;
            }
            
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

                var cellAddress = "A" + i; // this is dirty, I think there needs to be a better solution
                var entry = new Entry(value, date, (string)grid[i, 3], cellAddress);
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
            var path = BillingBookPath;

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
            if(Directory.Exists(TemplatePath))
            {
                var filePaths = Directory.GetFiles(TemplatePath);

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
            ApplicationConfiguration.ConfigurationFile.BillingBookPath = path;
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
            var billingBook = excelApplication.Workbooks.Add(BillingBookPath);
            var billingBookSheet = billingBook.ActiveSheet as Worksheet;

            var cell = _tadBillingBookConfiguration.RetrieveCell(billingBookSheet, _billingAdressData, CurrentBillingAddress, date);
            var structure = _tadBillingBookConfiguration.GetRowStructure();
            EnterNewAddressToBillingBook(billingBookSheet,cell,date,CurrentBillingAddress, structure);
            
           
            
            billingBook.SaveAs(BillingBookPath);
            billingBook.Close(false);
            newWorkbook.SaveAs(path);
            newWorkbook.Close( false);
            newWorkbook = null;
            excelApplication.Quit();
            excelApplication = null;
        }

        private void EnterNewAddressToBillingBook(Worksheet billingBookSheet,string target, DateTime dateTime, double billingId, (string billingID, string date, string sum, string recipient) structure)
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
            var address = CurrentBillingAddress;
            CreateNewWorkBook(path);
            _cacheManager.EnterAdress(address,path);
            WaitForCleanUp();
            RefreshValues();
        }

        private void RefreshValues()
        {
            _billingAdressData = RetrieveBillingEntriesFromExcelsheet(BillingBookPath);
        }


        public void ValidateData()
        {
            List<ValidatedEntry> results = new List<ValidatedEntry>();
            var entries = _billingAdressData;
            foreach (var entry in entries)
            {
                var address = entry.Id;
                
                var filePath = _cacheManager.GetDataPath(address);

                bool pathIsValid = File.Exists(filePath);

                if (!pathIsValid)
                {
                    var result = new ValidatedEntry(address, false, false, false, false, false);
                    results.Add(result);
                    return;
                }
                
                


            }
        }
    }

    public struct ValidatedEntry
    {
        public ValidatedEntry(double id, bool pathIsValid, bool wasModifiedInTheMeanTime, bool sumsAreMatching, bool recipientWasChanged, bool datesAreMatching)
        {
            Id = id;
            PathIsValid = pathIsValid;
            WasModifiedInTheMeanTime = wasModifiedInTheMeanTime;
            SumsAreMatching = sumsAreMatching;
            RecipientWasChanged = recipientWasChanged;
            DatesAreMatching = datesAreMatching;
        }
        public double Id;
        public bool PathIsValid;
        public bool WasModifiedInTheMeanTime;
        public bool SumsAreMatching;
        public bool RecipientWasChanged;
        public bool DatesAreMatching;
        
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
