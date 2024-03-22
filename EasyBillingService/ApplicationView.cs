using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace EasyBillingService
{
    public class ApplicationView
    {
        
        private MainWindow _mainWindow;
        private SettingsWindow _settingsWindow;
        
        public MainWindow MainWindow => _mainWindow;

        public ApplicationView()
        {
            _mainWindow =new MainWindow();
            _settingsWindow = new SettingsWindow();
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
            _mainWindow.Show();
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
    }
}