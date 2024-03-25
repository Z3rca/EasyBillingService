using System;
using System.Windows.Forms;

namespace EasyBillingService
{
    public partial class SettingsWindow : Form
    {
        public SettingsWindow()
        {
            InitializeComponent();
        }
        
        private void returnButton_Click(object sender, EventArgs e)
        {
            ApplicationManager.Instance.OpenMainWindow();
        }
        
        private void billingButton_Click(object sender, EventArgs e)
        {
            ApplicationManager.Instance.SetBillingPath();
        }
        
        private void BillingBookAdressPathClicked(object sender, EventArgs e)
        {
            ApplicationManager.Instance.ChangeBillingBookAdress();
        }
        private void TemplatePathButtonClicked(object sender, EventArgs e)
        {
            ApplicationManager.Instance.ChangeTemplateFolder();
        }

        private void TemplatePathTextChanged(object sender, EventArgs e)
        {
            
        }
        
        public void UpdateBillingBookPath(string entry)
        {
            billingBookPathText.Text = entry;
        }
        
        public void UpdateTemplatePath(string entry)
        {
            TemplatePathTextBox.Text = entry;
        }

        private void billingBookText_TextChanged(object sender, EventArgs e)
        {
            
        }


        public void UpdateBillingPath(string path)
        {
            BillingPathTextBox.Text = path;
        }
    }
    
    
    
}