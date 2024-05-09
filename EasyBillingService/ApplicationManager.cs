using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;
using SixLabors.ImageSharp.Drawing;
using Application = System.Windows.Forms.Application;
using Path = System.IO.Path;

namespace EasyBillingService
{
    public class ApplicationManager
    {
        
        private static ApplicationManager _instance;

        private ApplicationModel _model;
        private ApplicationView _view;
        
        private ApplicationManager()
        {
            _model = new ApplicationModel();
            _view = new ApplicationView();
            _view.UpdateBillingAdressPath(_model.BillingBookPath);
            _view.UpdateTemplatePath(_model.TemplatePath);
            
            InitalizeTemplateList();
        }
        
        public static ApplicationManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ApplicationManager();
                }
                return _instance;
            }
        }


        public void StartValidation()
        {
            
        }

        public void Init()
        {
            var lastEntry = _model.RetrieveLastBillingEntry();

            if (lastEntry != null)
            {
                _view.SetLastEntryText((Entry)lastEntry);
            }

            _view.SetNewAdress(_model.CurrentBillingAddress.ToString());
                
            
            Application.Run(_view.MainWindow);
        }

        public void InitalizeTemplateList()
        {
            var list = _model.RetrieveTemplateList();
            _view.SetTemplateEntries(list);
        }

        public void OpenConfiguration()
        {
           _view.ShowSettings();
        }

        public void OpenMainWindow()
        {
            _view.ShowMainWindow();
        }
        
        public void OpenValidation()
        {
            _view.ShowValidation();
            var data =_model.BillingAddressData;
            _view.AddValidationEntries(data);
        }
        

        public void ChangeBillingBookAdress()
        {
            var dialog = new System.Windows.Forms.OpenFileDialog();
            dialog.Filter= FileFormatHelper.FileFormatDisplay("Excel Datein(*.xlsx)",".xlsx");
            dialog.ShowDialog();
            
            var newPath = dialog.FileName;
            _model.SetBillingBookPath(newPath);
            _view.UpdateBillingAdressPath(newPath);
            var entry = GetLastBillingEntry();
            if (entry != null)
            {
                _view.SetLastEntryText((Entry)entry);
            }
            _view.SetNewAdress(_model.CurrentBillingAddress.ToString());
        }
        
        public void ChangeTemplateFolder()
        {
            var dialog = new System.Windows.Forms.FolderBrowserDialog();
            dialog.ShowDialog();
            
            var newPath = dialog.SelectedPath;
            _model.TemplatePath = newPath;
            
            _view.UpdateTemplatePath(newPath);
        }

        public void TemplateSelected(FileEntry selectedItem)
        {
            _model.setSelectedTemplate(selectedItem);
            _view.EnableButton(true);
        }

        public void CreateBillingFromTemplate()
        {
            var dialog = new System.Windows.Forms.SaveFileDialog();
            var selectedItem = _model.SelectedTemplate;

            dialog.FileName = selectedItem.FullFileName();
            dialog.Filter = FileFormatHelper.FileFormatDisplay("ExcelDatei(*.xlsx)", selectedItem.FileType);
            if(dialog.ShowDialog() ==DialogResult.OK)
            {
                var path = Path.GetFullPath(dialog.FileName);
                _model.CreateNewBilling(path);
                
                UpdateValues();
                _model.SetStandby(dialog.FileName);
            }
            
        }



        private void UpdateValues()
        {
            var entry = _model.RetrieveLastBillingEntry().Value;
            _view.SetLastEntryText(entry);
            
            _view.SetNewAdress(_model.CurrentBillingAddress.ToString());
            
        }

        public Entry? GetLastBillingEntry()
        {
            //handle already set
            return _model.RetrieveLastBillingEntry();
        }

        public void SetBillingPath()
        {
            var dialog = new System.Windows.Forms.FolderBrowserDialog();
            dialog.ShowDialog();
            
            var newPath = dialog.SelectedPath;
            _model.BillingPath = newPath;
            _view.UpdateBillingPath(newPath);
        }

        public void TransferChangesFromWorksheet(Worksheet sheet)
        {
            _model.TransferDataFromWorkSheet(sheet);
            UpdateValues();
        }
    }
}