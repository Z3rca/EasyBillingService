using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using EasyBillingService.Extensions;
using Microsoft.Office.Interop.Excel;

namespace EasyBillingService
{
    public class ExcelStandbyManager
    {
        private Microsoft.Office.Interop.Excel.Application ExcelApplication;
        private bool _isStandBy;
        private bool _isClosed;
        private Worksheet _currentSheet;
        private string _currentPath;
        public async void ConnectToWorksheet(string path)
        {
            if(_isStandBy) return;
            _currentPath = path;
            ExcelApplication = new Microsoft.Office.Interop.Excel.Application();
            ExcelApplication.Workbooks.Open(path);
            ExcelApplication.Visible = true;
            var _currentWorkBook = ExcelApplication.ActiveWorkbook;
            
            _isStandBy = true;

            _currentWorkBook.AfterSave+= WorkBookAfterSave;
            _currentWorkBook.BeforeClose+= WorkbookBeforeClose;

            _currentSheet = _currentWorkBook.ActiveSheet as Worksheet;

            var done = ExcelRunningAsync();

            
            _currentWorkBook.AfterSave-= WorkBookAfterSave;
            
     
            
            
            

            _isStandBy = false;


            WaitForCleanUp();
            _currentWorkBook.BeforeClose-= WorkbookBeforeClose;
            
            _currentWorkBook.Close();
            _currentWorkBook = null;
            ExcelApplication.Quit();
            ExcelApplication = null;
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

        private void WorkBookAfterSave(bool success)
        {
         
            
            var application =  new Microsoft.Office.Interop.Excel.Application();

            application.Workbooks.Open(_currentPath);
            
            var sheet = application.ActiveSheet as Worksheet;
            
            ApplicationManager.Instance.TransferChangesFromWorksheet(sheet);
            
            
        }

        private void WorkbookBeforeClose(ref bool cancel)
        {
            _isClosed = true;
        }

        private async Task<bool> ExcelRunningAsync()
        {
            while (!_isClosed)
            {
                Thread.Sleep(1000);
            }
            return true;
        }
        
    }
    
}