using System.ComponentModel;
using System.Windows.Forms;

namespace EasyBillingService
{
    partial class ValidationWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && ( components != null ))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.ListViewGroup listViewGroup1 = new System.Windows.Forms.ListViewGroup("ListViewGroup", System.Windows.Forms.HorizontalAlignment.Left);
            this.AddressListGrid = new System.Windows.Forms.ListView();
            this.BillingAdressHeader = new System.Windows.Forms.ColumnHeader();
            this.DateHeader = new System.Windows.Forms.ColumnHeader();
            this.RecipientHeader = new System.Windows.Forms.ColumnHeader();
            this.DataPathHeader = new System.Windows.Forms.ColumnHeader();
            this.Return = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.AddressListGrid.AllowColumnReorder = true;
            this.AddressListGrid.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] { this.BillingAdressHeader, this.DateHeader, this.RecipientHeader, this.DataPathHeader });
            this.AddressListGrid.FullRowSelect = true;
            listViewGroup1.Header = "ListViewGroup";
            listViewGroup1.Name = "listViewGroup1";
            this.AddressListGrid.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] { listViewGroup1 });
            this.AddressListGrid.HideSelection = false;
            this.AddressListGrid.Location = new System.Drawing.Point(12, 100);
            this.AddressListGrid.Name = "AddressListGrid";
            this.AddressListGrid.ShowGroups = false;
            this.AddressListGrid.Size = new System.Drawing.Size(776, 280);
            this.AddressListGrid.TabIndex = 0;
            this.AddressListGrid.UseCompatibleStateImageBehavior = false;
            this.AddressListGrid.View = System.Windows.Forms.View.Details;
            // 
            // BillingAdressHeader
            // 
            this.BillingAdressHeader.Text = "Rechnungsnummer";
            this.BillingAdressHeader.Width = 150;
            // 
            // DateHeader
            // 
            this.DateHeader.DisplayIndex = 2;
            this.DateHeader.Text = "Datum";
            this.DateHeader.Width = 80;
            // 
            // RecipientHeader
            // 
            this.RecipientHeader.DisplayIndex = 1;
            this.RecipientHeader.Text = "Empfänger";
            this.RecipientHeader.Width = 200;
            // 
            // DataPathHeader
            // 
            this.DataPathHeader.Text = "Dateipfad";
            this.DataPathHeader.Width = 400;
            // 
            // Return
            // 
            this.Return.Location = new System.Drawing.Point(12, 12);
            this.Return.Name = "Return";
            this.Return.Size = new System.Drawing.Size(75, 23);
            this.Return.TabIndex = 8;
            this.Return.Text = "Zurück";
            this.Return.UseVisualStyleBackColor = true;
            this.Return.Click += returnButton_Click;
            // 
            // ValidationWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.Return);
            this.Controls.Add(this.AddressListGrid);
            this.Name = "ValidationWindow";
            this.Text = "ValidationWindow";
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Button Return;

        private System.Windows.Forms.ColumnHeader DataPathHeader;

        private System.Windows.Forms.ColumnHeader BillingAdressHeader;
        private System.Windows.Forms.ColumnHeader DateHeader;
        private System.Windows.Forms.ColumnHeader RecipientHeader;

        private System.Windows.Forms.ListView AddressListGrid;

        private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn1;
        private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn2;

        #endregion
    }
}