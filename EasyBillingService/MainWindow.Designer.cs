namespace EasyBillingService
{
    partial class MainWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            this.CreateBillingButton = new System.Windows.Forms.Button();
            this.LastBillingNumberText = new System.Windows.Forms.TextBox();
            this.LastBillingAdressLabel = new System.Windows.Forms.Label();
            this.DateLabel = new System.Windows.Forms.Label();
            this.DateText = new System.Windows.Forms.TextBox();
            this.RecipientLabel = new System.Windows.Forms.Label();
            this.RecipientText = new System.Windows.Forms.TextBox();
            this.configurationButton = new System.Windows.Forms.Button();
            this.TemplateList = new System.Windows.Forms.ListBox();
            this.NewAdressLabel = new System.Windows.Forms.Label();
            this.NewAdressField = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // CreateBillingButton
            // 
            this.CreateBillingButton.Enabled = false;
            this.CreateBillingButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ( (byte)( 0 ) ));
            this.CreateBillingButton.Location = new System.Drawing.Point(65, 202);
            this.CreateBillingButton.Name = "CreateBillingButton";
            this.CreateBillingButton.Size = new System.Drawing.Size(143, 49);
            this.CreateBillingButton.TabIndex = 2;
            this.CreateBillingButton.Text = "Rechnung Erstellen";
            this.CreateBillingButton.UseVisualStyleBackColor = true;
            this.CreateBillingButton.Click += new System.EventHandler(this.CreateBillingButton_Click);
            // 
            // LastBillingNumberText
            // 
            this.LastBillingNumberText.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.LastBillingNumberText.Enabled = false;
            this.LastBillingNumberText.Location = new System.Drawing.Point(89, 35);
            this.LastBillingNumberText.Name = "LastBillingNumberText";
            this.LastBillingNumberText.Size = new System.Drawing.Size(100, 20);
            this.LastBillingNumberText.TabIndex = 4;
            this.LastBillingNumberText.TextChanged += new System.EventHandler(this.BillingNumber_TextChanged);
            // 
            // LastBillingAdressLabel
            // 
            this.LastBillingAdressLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ( (byte)( 0 ) ));
            this.LastBillingAdressLabel.Location = new System.Drawing.Point(89, 9);
            this.LastBillingAdressLabel.Name = "LastBillingAdressLabel";
            this.LastBillingAdressLabel.Size = new System.Drawing.Size(100, 23);
            this.LastBillingAdressLabel.TabIndex = 1;
            this.LastBillingAdressLabel.Text = "Letzte Adresse";
            // 
            // DateLabel
            // 
            this.DateLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ( (byte)( 0 ) ));
            this.DateLabel.Location = new System.Drawing.Point(204, 9);
            this.DateLabel.Name = "DateLabel";
            this.DateLabel.Size = new System.Drawing.Size(100, 23);
            this.DateLabel.TabIndex = 1;
            this.DateLabel.Text = "Datum";
            // 
            // DateText
            // 
            this.DateText.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.DateText.Enabled = false;
            this.DateText.Location = new System.Drawing.Point(204, 35);
            this.DateText.Name = "DateText";
            this.DateText.Size = new System.Drawing.Size(100, 20);
            this.DateText.TabIndex = 4;
            this.DateText.TextChanged += new System.EventHandler(this.BillingNumber_TextChanged);
            // 
            // RecipientLabel
            // 
            this.RecipientLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ( (byte)( 0 ) ));
            this.RecipientLabel.Location = new System.Drawing.Point(310, 9);
            this.RecipientLabel.Name = "RecipientLabel";
            this.RecipientLabel.Size = new System.Drawing.Size(137, 23);
            this.RecipientLabel.TabIndex = 1;
            this.RecipientLabel.Text = "Rechnungs Empfänger";
            // 
            // RecipientText
            // 
            this.RecipientText.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.RecipientText.Enabled = false;
            this.RecipientText.Location = new System.Drawing.Point(310, 35);
            this.RecipientText.Name = "RecipientText";
            this.RecipientText.Size = new System.Drawing.Size(100, 20);
            this.RecipientText.TabIndex = 4;
            this.RecipientText.TextChanged += new System.EventHandler(this.BillingNumber_TextChanged);
            // 
            // configurationButton
            // 
            this.configurationButton.Image = global::EasyBillingService.Properties.Resources.gearIcon;
            this.configurationButton.Location = new System.Drawing.Point(12, 5);
            this.configurationButton.Name = "configurationButton";
            this.configurationButton.Size = new System.Drawing.Size(50, 50);
            this.configurationButton.TabIndex = 5;
            this.configurationButton.UseVisualStyleBackColor = true;
            this.configurationButton.Click += new System.EventHandler(this.ConfigurationButton_Click);
            // 
            // TemplateList
            // 
            this.TemplateList.ColumnWidth = 1;
            this.TemplateList.FormattingEnabled = true;
            this.TemplateList.Location = new System.Drawing.Point(243, 156);
            this.TemplateList.Name = "TemplateList";
            this.TemplateList.Size = new System.Drawing.Size(417, 95);
            this.TemplateList.TabIndex = 6;
            this.TemplateList.SelectedValueChanged+= new System.EventHandler(this.TemplateSelectionChanged);
            // 
            // NewAdressLabel
            // 
            this.NewAdressLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ( (byte)( 0 ) ));
            this.NewAdressLabel.Location = new System.Drawing.Point(73, 131);
            this.NewAdressLabel.Name = "NewAdressLabel";
            this.NewAdressLabel.Size = new System.Drawing.Size(135, 23);
            this.NewAdressLabel.TabIndex = 7;
            this.NewAdressLabel.Text = "Neue Adresse:";
            // 
            // NewAdressField
            // 
            this.NewAdressField.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.NewAdressField.Enabled = false;
            this.NewAdressField.Location = new System.Drawing.Point(89, 157);
            this.NewAdressField.Name = "NewAdressField";
            this.NewAdressField.Size = new System.Drawing.Size(100, 20);
            this.NewAdressField.TabIndex = 4;
            this.NewAdressField.TextChanged += new System.EventHandler(this.BillingNumber_TextChanged);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ClientSize = new System.Drawing.Size(800, 286);
            this.Controls.Add(this.NewAdressLabel);
            this.Controls.Add(this.TemplateList);
            this.Controls.Add(this.configurationButton);
            this.Controls.Add(this.RecipientText);
            this.Controls.Add(this.DateText);
            this.Controls.Add(this.NewAdressField);
            this.Controls.Add(this.LastBillingNumberText);
            this.Controls.Add(this.CreateBillingButton);
            this.Controls.Add(this.RecipientLabel);
            this.Controls.Add(this.DateLabel);
            this.Controls.Add(this.LastBillingAdressLabel);
            this.Location = new System.Drawing.Point(15, 15);
            this.Name = "MainWindow";
            this.Text = "Ti Re";
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label NewAdressLabel;
        private System.Windows.Forms.TextBox NewAdressField;

        private System.Windows.Forms.ListBox TemplateList;

        private System.Windows.Forms.Button configurationButton;

        private System.Windows.Forms.Label RecipientLabel;
        private System.Windows.Forms.TextBox RecipientText;

        private System.Windows.Forms.Label DateLabel;
        private System.Windows.Forms.TextBox DateText;

        private System.Windows.Forms.TextBox LastBillingNumberText;

        private System.Windows.Forms.Button CreateBillingButton;

        private System.Windows.Forms.Label LastBillingAdressLabel;


    

        #endregion
    }
}