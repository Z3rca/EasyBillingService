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
            this.SetValue = new System.Windows.Forms.Button();
            this.LastBillingNumber = new System.Windows.Forms.TextBox();
            this.LastBillingAdressLabel = new System.Windows.Forms.Label();
            this.DateLabel = new System.Windows.Forms.Label();
            this.DateText = new System.Windows.Forms.TextBox();
            this.RecipientLabel = new System.Windows.Forms.Label();
            this.RecipientText = new System.Windows.Forms.TextBox();
            this.configurationButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // SetValue
            // 
            this.SetValue.Location = new System.Drawing.Point(98, 202);
            this.SetValue.Name = "SetValue";
            this.SetValue.Size = new System.Drawing.Size(75, 23);
            this.SetValue.TabIndex = 2;
            this.SetValue.Text = "Der Knopf";
            this.SetValue.UseVisualStyleBackColor = true;
            this.SetValue.Click += new System.EventHandler(this.ConfigurationButton_Click);
            // 
            // LastBillingNumber
            // 
            this.LastBillingNumber.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.LastBillingNumber.Enabled = false;
            this.LastBillingNumber.Location = new System.Drawing.Point(88, 93);
            this.LastBillingNumber.Name = "LastBillingNumber";
            this.LastBillingNumber.Size = new System.Drawing.Size(100, 20);
            this.LastBillingNumber.TabIndex = 4;
            this.LastBillingNumber.TextChanged += new System.EventHandler(this.BillingNumber_TextChanged);
            // 
            // LastBillingAdressLabel
            // 
            this.LastBillingAdressLabel.Location = new System.Drawing.Point(88, 67);
            this.LastBillingAdressLabel.Name = "LastBillingAdressLabel";
            this.LastBillingAdressLabel.Size = new System.Drawing.Size(186, 23);
            this.LastBillingAdressLabel.TabIndex = 1;
            this.LastBillingAdressLabel.Text = "Letzte Adresse";
            // 
            // DateLabel
            // 
            this.DateLabel.Location = new System.Drawing.Point(203, 67);
            this.DateLabel.Name = "DateLabel";
            this.DateLabel.Size = new System.Drawing.Size(186, 23);
            this.DateLabel.TabIndex = 1;
            this.DateLabel.Text = "Datum";
            // 
            // DateText
            // 
            this.DateText.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.DateText.Enabled = false;
            this.DateText.Location = new System.Drawing.Point(203, 93);
            this.DateText.Name = "DateText";
            this.DateText.Size = new System.Drawing.Size(100, 20);
            this.DateText.TabIndex = 4;
            this.DateText.TextChanged += new System.EventHandler(this.BillingNumber_TextChanged);
            // 
            // RecipientLabel
            // 
            this.RecipientLabel.Location = new System.Drawing.Point(309, 67);
            this.RecipientLabel.Name = "RecipientLabel";
            this.RecipientLabel.Size = new System.Drawing.Size(186, 23);
            this.RecipientLabel.TabIndex = 1;
            this.RecipientLabel.Text = "Rechnungs Empfänger";
            // 
            // RecipientText
            // 
            this.RecipientText.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.RecipientText.Enabled = false;
            this.RecipientText.Location = new System.Drawing.Point(309, 93);
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
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ClientSize = new System.Drawing.Size(800, 286);
            this.Controls.Add(this.configurationButton);
            this.Controls.Add(this.RecipientText);
            this.Controls.Add(this.DateText);
            this.Controls.Add(this.LastBillingNumber);
            this.Controls.Add(this.SetValue);
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

        private System.Windows.Forms.Button configurationButton;

        private System.Windows.Forms.Label RecipientLabel;
        private System.Windows.Forms.TextBox RecipientText;

        private System.Windows.Forms.Label DateLabel;
        private System.Windows.Forms.TextBox DateText;

        private System.Windows.Forms.TextBox LastBillingNumber;

        private System.Windows.Forms.Button SetValue;

        private System.Windows.Forms.Label LastBillingAdressLabel;


    

        #endregion
    }
}