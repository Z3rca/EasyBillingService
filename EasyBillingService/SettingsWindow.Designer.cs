using System.ComponentModel;

namespace EasyBillingService
{
    partial class SettingsWindow
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
            this.billingBookPathText = new System.Windows.Forms.TextBox();
            this.OpenBillingAdress = new System.Windows.Forms.Button();
            this.BillingBookLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.templatePathButton = new System.Windows.Forms.Button();
            this.TemplatePathTextBox = new System.Windows.Forms.TextBox();
            this.Return = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // billingBookPathText
            // 
            this.billingBookPathText.Location = new System.Drawing.Point(208, 49);
            this.billingBookPathText.Name = "billingBookPathText";
            this.billingBookPathText.Size = new System.Drawing.Size(418, 20);
            this.billingBookPathText.TabIndex = 6;
            this.billingBookPathText.TextChanged += new System.EventHandler(this.billingBookText_TextChanged);
            // 
            // OpenBillingAdress
            // 
            this.OpenBillingAdress.Location = new System.Drawing.Point(108, 49);
            this.OpenBillingAdress.Name = "OpenBillingAdress";
            this.OpenBillingAdress.Size = new System.Drawing.Size(75, 21);
            this.OpenBillingAdress.TabIndex = 5;
            this.OpenBillingAdress.Text = "Öffnen...";
            this.OpenBillingAdress.UseVisualStyleBackColor = true;
            this.OpenBillingAdress.Click += new System.EventHandler(this.BillingBookAdressPathClicked);
            // 
            // BillingBookLabel
            // 
            this.BillingBookLabel.Location = new System.Drawing.Point(208, 26);
            this.BillingBookLabel.Name = "BillingBookLabel";
            this.BillingBookLabel.Size = new System.Drawing.Size(280, 23);
            this.BillingBookLabel.TabIndex = 4;
            this.BillingBookLabel.Text = "Rechnungsadressbuch";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(208, 127);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(280, 23);
            this.label1.TabIndex = 4;
            this.label1.Text = "Vorlagenpfad";
            // 
            // templatePathButton
            // 
            this.templatePathButton.Location = new System.Drawing.Point(108, 153);
            this.templatePathButton.Name = "templatePathButton";
            this.templatePathButton.Size = new System.Drawing.Size(75, 21);
            this.templatePathButton.TabIndex = 5;
            this.templatePathButton.Text = "Öffnen...";
            this.templatePathButton.UseVisualStyleBackColor = true;
            this.templatePathButton.Click += new System.EventHandler(this.TemplatePathButtonClicked);
            // 
            // TemplatePathTextBox
            // 
            this.TemplatePathTextBox.Location = new System.Drawing.Point(208, 153);
            this.TemplatePathTextBox.Name = "TemplatePathTextBox";
            this.TemplatePathTextBox.Size = new System.Drawing.Size(418, 20);
            this.TemplatePathTextBox.TabIndex = 6;
            this.TemplatePathTextBox.TextChanged += new System.EventHandler(this.TemplatePathTextChanged);
            // 
            // Return
            // 
            this.Return.Location = new System.Drawing.Point(12, 21);
            this.Return.Name = "Return";
            this.Return.Size = new System.Drawing.Size(75, 23);
            this.Return.TabIndex = 7;
            this.Return.Text = "Zurück";
            this.Return.UseVisualStyleBackColor = true;
            this.Return.Click += new System.EventHandler(this.returnButton_Click);
            // 
            // SettingsWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.Return);
            this.Controls.Add(this.TemplatePathTextBox);
            this.Controls.Add(this.templatePathButton);
            this.Controls.Add(this.billingBookPathText);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.OpenBillingAdress);
            this.Controls.Add(this.BillingBookLabel);
            this.Location = new System.Drawing.Point(15, 15);
            this.Name = "SettingsWindow";
            this.Text = "Einstellungen";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Button Return;

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button templatePathButton;
        private System.Windows.Forms.TextBox TemplatePathTextBox;

        private System.Windows.Forms.TextBox billingBookPathText;
        private System.Windows.Forms.Button OpenBillingAdress;
        private System.Windows.Forms.Label BillingBookLabel;

        #endregion
    }
}