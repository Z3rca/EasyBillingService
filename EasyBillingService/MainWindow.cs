using System;
using System.Windows.Forms;

namespace EasyBillingService
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        
        private void ConfigurationButton_Click(object sender, EventArgs e)
        {
            ApplicationManager.Instance.OpenConfiguration();
        }

        public void UpdateLastBillingEntry(Entry entry)
        {
            LastBillingNumberText.Text = entry.Id.ToString();
            RecipientText.Text = entry.Recipient;
            DateText.Text = entry.dateText;
        }
        

        private void MainWindow_Load(object sender, EventArgs e)
        {
            
        }

        private void BillingNumber_TextChanged(object sender, EventArgs e)
        {
            
        }

        public void SetNewAdress(string currentAdress)
        {
            NewAdressField.Text = currentAdress;
        }
    }
}