using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace EasyBillingService
{
    public class ApplicationView
    {
        
        private MainWindow _mainWindow;
        private SettingsWindow _settingsWindow;
        private readonly ValidationWindow _validationWindow;

        public MainWindow MainWindow => _mainWindow;

        public ApplicationView()
        {
            _mainWindow =new MainWindow();
            _settingsWindow = new SettingsWindow();
            _validationWindow = new ValidationWindow();


        }

        public void ShowSettings()
        {
            _settingsWindow.Activate();
            _mainWindow.Hide();
            _settingsWindow.Show();
        }
        
        public void ShowMainWindow()
        {
            _mainWindow.Activate();
            _settingsWindow.Hide();
            _validationWindow.Hide();
            _mainWindow.Show();
        }
        
        public void ShowValidation()
        {
            _validationWindow.Activate();
            _mainWindow.Hide();
            _validationWindow.Show();
        }
        
        
        public void SetLastEntryText(Entry entry)
        {
            _mainWindow.UpdateLastBillingEntry(entry);
        }
        
        public void UpdateBillingAdressPath(string path)
        {
            _settingsWindow.UpdateBillingBookPath(path);
        }
        
        public void UpdateTemplatePath(string path)
        {
            _settingsWindow.UpdateTemnplatePath(path);
        }


        public void SetNewAdress(String currentAdress)
        {
            _mainWindow.SetNewAdress(currentAdress);
        }

        public void SetTemplateEntries(List<FileEntry> list)
        {
            foreach (var entry in list)
            {
                _mainWindow.setNewTemplateEntry(entry);
            }
            
        }

        public void EnableButton(bool b)
        {
            _mainWindow.EnableCreateButton(b);
        }


        public void AddValidationEntries(List<Entry> data)
        {
            _validationWindow.AddEntries(data);
        }
    }
}