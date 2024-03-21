using System.Collections.Generic;
using System.Windows.Forms;

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
            _view.UpdateBillingAdressPath(_model.LastOpenedBillingAdressBook);
            _view.UpdateTemplatePath(_model.TemplatepPath);
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
        
        public void Init()
        {
            var lastEntry = _model.RetrieveLastBillingNumber();

            if (lastEntry != null)
            {
                _view.SetLastEntryText((Entry)lastEntry);
            } 
            
            Application.Run(_view.MainWindow);
        }


        public void OpenConfiguration()
        {
           _view.ShowSettings();
        }

        public void OpenMainWindow()
        {
            _view.ShowMainWindow();
        }

        public void ChangeBillingBookAdress()
        {
            var dialog = new System.Windows.Forms.OpenFileDialog();
            dialog.Filter= "Excel Datein(*.xlsx)|*.xlsx";
            dialog.ShowDialog();
            
            var newPath = dialog.FileName;
            _model.SetBillingBookPath(newPath);
            _view.UpdateBillingAdressPath(newPath);
            var entry = _model.RetrieveLastBillingNumber();
            if (entry != null)
            {
                _view.SetLastEntryText((Entry)entry);
            }
            
        }
        
        public void ChangeTemplateFolder()
        {
            var dialog = new System.Windows.Forms.FolderBrowserDialog();
            dialog.ShowDialog();
            
            var newPath = dialog.SelectedPath;
            _model.SetTemplatePath(newPath);
            
            _view.UpdateTemplatePath(newPath);
        }
    }
}