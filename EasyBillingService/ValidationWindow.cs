using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace EasyBillingService
{
    public partial class ValidationWindow : Form
    {
        public ValidationWindow()
        {
            InitializeComponent();
        }
        
        private void returnButton_Click(object sender, EventArgs e)
        {
            ApplicationManager.Instance.OpenMainWindow();
        }

        public void AddEntries(List<Entry> entries)
        {
            foreach (var entry in entries)
            {
                var id = entry.Id;
                var recipient = entry.Recipient;
                var date = entry.dateText; 
               // string[] textEntry = {id.ToString(),date};
                var textEntry = new ListViewItem(id.ToString());
                textEntry.SubItems.Add(date);
                textEntry.SubItems.Add(recipient);
                
                listView1.Items.Add(textEntry);
            }
        }

       
    }
}