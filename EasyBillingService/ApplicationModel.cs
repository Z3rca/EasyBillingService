using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
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

        private FileEntry _selectedTemplate;


        public string LastOpenedBillingAdressBook =>  String.IsNullOrEmpty(_lastOpenedFile) ? "":_lastOpenedFile;
        public string TemplatePath =>  String.IsNullOrEmpty(_templatePath) ? "":_templatePath;
        public double CurrentBillingAddress { get; private set; }

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

        public Entry? RetrieveLastBillingNumber()
        {
           
            var excelApplication = new Microsoft.Office.Interop.Excel.Application();
            
            
            var path = _lastOpenedFile;

            if (string.IsNullOrEmpty(path))
            {
                return null;
            }
            Workbook workbook = excelApplication.Workbooks.Open(path);

            

            var sheet = workbook.ActiveSheet as Worksheet;
            var billingIds = new List<(double idNumber, String date, String reciepient)>(); //ids are set here as numbers 

            var entries = new List<Entry>();
            
            
            
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
            

            double maximum = 0;

            if (!entries.Any())
            {
                return null;
            }
            
            
            
            
            var newestEntry = entries.OrderByDescending(x => x.Id).First();


            

            CurrentBillingAddress = newestEntry.Id + 1;
            
            
           // workbook.Save();
            workbook.Close(SaveChanges: false);
            workbook = null;
            excelApplication.Quit();
            excelApplication = null;

            return newestEntry;

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
            _selectedTemplate = selectedItem;
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

        public override string ToString()
        {
            return this.FileName;
        }
    }
}
