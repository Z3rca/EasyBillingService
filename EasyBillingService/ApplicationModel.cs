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
        private ExcelStandbyManager _standbyManager = new ExcelStandbyManager();
        
        public string BillingBookPath => ApplicationConfiguration.ConfigurationFile.BillingBookPath;
        public string TemplatePath
        {
            get => ApplicationConfiguration.ConfigurationFile.TemplatePath;
            set => ApplicationConfiguration.ConfigurationFile.TemplatePath = value;
        }

        private List<Entry> _billingAddressData =new List<Entry>();
        public double CurrentBillingAddress { get; private set; }

        public FileEntry SelectedTemplate { get; private set; }

        public List<Entry> BillingAddressData
        {
            get
            {
                if (_billingAddressData != null)
                {
                    return _billingAddressData;
                }
                else
                {
                    _billingAddressData = RetrieveBillingEntriesFromExcelsheet(BillingBookPath);
                    return _billingAddressData;
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
                var entry = new Entry(value, date, (string)grid[i, 3],0, cellAddress);
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

            var entries = _billingAddressData;
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

            excelApplication.DisplayAlerts = false;
            
            Workbook newWorkbook = excelApplication.Workbooks.Add(SelectedTemplate.Path);

            var sheet = newWorkbook.ActiveSheet as Worksheet;
            var date = DateTime.Now;
            modifyWorksheet(ref sheet, date,CurrentBillingAddress);
            var billingBook = excelApplication.Workbooks.Add(BillingBookPath);
            var billingBookSheet = billingBook.ActiveSheet as Worksheet;

            var cell = _tadBillingBookConfiguration.RetrieveCell(billingBookSheet, _billingAddressData, CurrentBillingAddress, date);
            var structure = _tadBillingBookConfiguration.GetRowStructure();
            var sum = 0;
            EnterNewAddressToBillingBook(billingBookSheet, cell, date, sum, CurrentBillingAddress,  structure);
            
           //TODO handle billing book is already open
            
            billingBook.SaveAs(BillingBookPath);
            billingBook.Close(false);
            newWorkbook.SaveAs(path);
            newWorkbook.Close( false);
            newWorkbook = null;
            excelApplication.Quit();
            excelApplication = null;

            WaitForCleanUp();
        }

        private void EnterNewAddressToBillingBook(Worksheet billingBookSheet,string target, DateTime dateTime, double sum, double billingId, (string billingID, string date, string sum, string recipient) structure)
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
            billingBookSheet.SetCellValue<double>(structure.sum+rowNumber,sum);
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
            _billingAddressData = RetrieveBillingEntriesFromExcelsheet(BillingBookPath);
        }


        public void ValidateData()
        {
            List<ValidatedEntry> results = new List<ValidatedEntry>();
            var entries = _billingAddressData;
            foreach (var entry in entries)
            {
                var address = entry.Id;
                var filePath = _cacheManager.GetDataPath(address);

                bool pathIsValid = false;
                bool fileNotFound = false;
                bool wasModifiedInTheMeanTime = false;
                bool addressMatching=true;
                bool sumsAreMatching = true;
                bool datesAreMatching=true;
                

                pathIsValid = File.Exists(filePath);

                if (!pathIsValid)
                {
                    string path;
                    if (SearchFile(BillingPath, out path))
                    {
                        pathIsValid = true;
                        //StorePath()
                    }
                }

                if (pathIsValid)
                {
                    var lastModifiedTimeStamp = _cacheManager.GetLastModified(address);
                    if (!lastModifiedTimeStamp.HasValue)
                    {
                        //throw exception
                        continue;
                    }
                    if (File.GetLastAccessTime(filePath) != lastModifiedTimeStamp)
                    { 
                        wasModifiedInTheMeanTime = true;
                            
                        var excelApplication = new Microsoft.Office.Interop.Excel.Application();
                        Workbook newWorkbook = excelApplication.Workbooks.Add(filePath);

                        var sheet = newWorkbook.ActiveSheet as Worksheet;
                        var retrievedBillingAddress = sheet.GetCellValue<double>("B17");
                        if (retrievedBillingAddress != entry.Id)
                        {
                            addressMatching = false;
                        }
                        var retrievedDates = sheet.GetCellValue<double>("B18");
                        if (retrievedBillingAddress != entry.Id)
                        {
                            datesAreMatching = false;
                        }

                        var retrievedSum = sheet.GetCellValue<double>("F22");
                        if (entry.Sum != retrievedSum)
                        {
                            sumsAreMatching = false;
                        }
                    }
                }
                var result = new ValidatedEntry(address, pathIsValid,fileNotFound,wasModifiedInTheMeanTime, addressMatching, sumsAreMatching, true, datesAreMatching);
                results.Add(result);
            }
        }

        private bool SearchFile(string billingPath, out string pathToFile)
        {
            pathToFile = "";
            var chunk = billingPath.Split('\\');
            var fileName = chunk[chunk.Length - 1];
            var files = Directory.EnumerateFiles(billingPath, fileName, SearchOption.AllDirectories);

            if (files.Count() == 1)
            {
                pathToFile = files.First();
                return true;
            }

            var fileType = billingPath.Split('.')[1];
            
            files = Directory.EnumerateFiles(billingPath, "*."+fileType, SearchOption.AllDirectories);

            foreach (var file in files)
            {
                //search each excel document
            }
            

            return false;
        }

        public void SetStandby(string path)
        {
            _standbyManager.ConnectToWorksheet(path);
        }

        public void TransferDataFromWorkSheet(Worksheet sheet)
        {
            var entries = new List<Entry>();
            var excelApplication = new Microsoft.Office.Interop.Excel.Application();
            excelApplication.DisplayAlerts = false;
            var billingBook = excelApplication.Workbooks.Add(BillingBookPath);
            var billingBookSheet = billingBook.ActiveSheet as Worksheet;
            
            var date = sheet.GetCellValue<DateTime>("B18");
            var sum = sheet.GetCellValue<double>("F22");
            var address = sheet.GetCellValue<double>("B17");
            var cell = _tadBillingBookConfiguration.RetrieveCell(billingBookSheet, _billingAddressData, address, date);
            var structure = _tadBillingBookConfiguration.GetRowStructure();

            
            EnterNewAddressToBillingBook(billingBookSheet, cell, date, sum, address,  structure);
            
           
            //todo handle RAB open
            billingBook.SaveAs(BillingBookPath);
            billingBook.Close(false);
            excelApplication.Quit();
            excelApplication = null;
            WaitForCleanUp();
        }
    }

    public struct ValidatedEntry
    {
        public bool FileIsNotFound;
        public double Id;
        public bool PathIsValid;
        public bool WasModifiedInTheMeanTime;
        public bool SumsAreMatching;
        public bool RecipientWasChanged;
        public bool DatesAreMatching;
        public  bool AddressMatching;
        public ValidatedEntry(double id, bool pathIsValid, bool fileIsNotFound, bool wasModifiedInTheMeanTime, bool addressMatching, bool sumsAreMatching, bool recipientMatching, bool datesAreMatching)
        {
            Id = id;
            PathIsValid = pathIsValid;
            FileIsNotFound = fileIsNotFound;
            WasModifiedInTheMeanTime = wasModifiedInTheMeanTime;
            AddressMatching = addressMatching;
            SumsAreMatching = sumsAreMatching;
            RecipientWasChanged = recipientMatching;
            DatesAreMatching = datesAreMatching;
        }

        
    }
    
    public struct Entry
    {
        public string CellId;
        public int RowNumber;
        public Double Id;
        public DateTime DateTime;
        public String Recipient;
        public double Sum;
        public String dateText;
        public Entry(double id, DateTime date, string recipient,double sum, string cellId)
        {
            CellId = cellId;
            this.Id = id;
            DateTime = date;
            this.Recipient = recipient;
            var rowNumber = 0;
            int.TryParse(cellId.Substring(1),out rowNumber);
            RowNumber = rowNumber;
            Sum = sum;
            
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
