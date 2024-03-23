using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using Microsoft.Office.Interop.Excel;

namespace EasyBillingService
{
    public class ApplicationModel
    {
        private string _path;
        private string _lastOpenedFile;
        private string _templatePath;
        public const string CONFIGURATIONFILEPATH = "..\\..\\configuration.cfg";

        private string _lastOpenedFileText = "lastOpenedFile = ";
        private string _templatePathText = "templatePath = ";


        public string LastOpenedBillingAdressBook =>  String.IsNullOrEmpty(_lastOpenedFile) ? "":_lastOpenedFile;
        public string TemplatePath =>  String.IsNullOrEmpty(_templatePath) ? "":_templatePath;
        public double CurrentBillingAddress { get; private set; }

        public FileEntry SelectedTemplate { get; private set; }

        public ApplicationModel()
        {
            InitializeFormerFilePaths(); 
            RetrieveLastBillingNumber();
        }
        
        private void InitializeFormerFilePaths()
        {
            var configurationPath = ApplicationModel.CONFIGURATIONFILEPATH;
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
                        }
                        if (line.StartsWith(_templatePathText))
                        {
                            _templatePath = line.Substring(_templatePathText.ToCharArray().Length);
                        }
                        
                    }
                }
            }
        }
        

        internal List<Entry> RetrieveBillingEntriesFromExcelsheet(string path)
        {
            var entries = new List<Entry>();
            var excelApplication = new Microsoft.Office.Interop.Excel.Application();
            Workbook workbook = excelApplication.Workbooks.Open(path);
            var sheet = workbook.ActiveSheet as Worksheet;
            var grid = sheet.Range["A1", "C1000"].Value as object[,];
            
            for (int  i = 1;  i < grid.GetLength(0); i++)
            {
                
                double value = 0;
                if(!double.TryParse((String)grid[i, 1], out value))
                {
                    continue;
                }
                
                var date = (DateTime)grid[i, 2];
                var entry = new Entry(value, date, (string)grid[i, 3]);
                entries.Add(entry);
            }
            grid = null;
            sheet = null;
            // workbook.Save();
            workbook.Close( false);
           
            workbook = null;
            excelApplication.Quit();
            excelApplication = null;
            return entries;
        }

        public Entry? RetrieveLastBillingNumber()
        {
           
            
            
            
            var path = _lastOpenedFile;

            if (string.IsNullOrEmpty(path))
            {
                return null;
            }
            
            var entries = RetrieveBillingEntriesFromExcelsheet(path);

            WaitForCleanUp();

            double maximum = 0;

          
            if (!entries.Any())
            {
                return null;
            }
            var newestEntry = entries.OrderByDescending(x => x.Id).First();
            
            CurrentBillingAddress = newestEntry.Id + 1;

            return newestEntry;

        }

        private bool WaitForCleanUp()
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
            var oldValue = _lastOpenedFileText + (_lastOpenedFile);
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

            _lastOpenedFile = path;
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

        public void CreateNewBilling(string path)
        {
            var excelApplication = new Microsoft.Office.Interop.Excel.Application();
            excelApplication.Visible = false;
            Workbook newWorkbook = excelApplication.Workbooks.Add(SelectedTemplate.Path);
            
            Workbook workbook = excelApplication.Workbooks.Open(path);
            
            
                
            workbook.Close(SaveChanges: false);
            workbook = null;
            excelApplication.Quit();
            excelApplication = null;
        }
    }
    
        
    
    public struct Entry
    {
        public Double Id;
        private DateTime DateTime;
        public String Recipient;

        public String dateText;
        public Entry(double id, DateTime date, string recipient)
        {
            this.Id = id;
            DateTime = date;
            this.Recipient = recipient;
            
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
