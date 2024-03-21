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
        
        public void UpdateTemnplatePath(string entry)
        {
            TemplatePathTextBox.Text = entry;
        }

        private void billingBookText_TextChanged(object sender, EventArgs e)
        {
            
        }

     
    }
    
    
    
}