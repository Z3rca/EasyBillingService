using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Windows.Forms;
using EasyBillingService.Extensions;
using Microsoft.Office.Interop.Excel;

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
            LastBillingNumber.Text = entry.Id.ToString();
            RecipientText.Text = entry.Recipient;
            DateText.Text = entry.dateText;
        }
        

        private void MainWindow_Load(object sender, EventArgs e)
        {
            
        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void BillingNumber_TextChanged(object sender, EventArgs e)
        {
            
        }
    }
}