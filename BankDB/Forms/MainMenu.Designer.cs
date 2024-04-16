namespace BankDB
{
    partial class MainMenu
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label = new Label();
            buttonClient = new Button();
            buttonStatus = new Button();
            buttonCountry = new Button();
            buttonDepartment = new Button();
            buttonCurrency = new Button();
            buttonExchangeRate = new Button();
            buttonAccount = new Button();
            buttonTypeTransaction = new Button();
            buttonTransactions = new Button();
            SuspendLayout();
            // 
            // label
            // 
            label.AutoSize = true;
            label.Font = new Font("Verdana", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label.ForeColor = SystemColors.ControlLightLight;
            label.Location = new Point(15, 32);
            label.Name = "label";
            label.Size = new Size(474, 23);
            label.TabIndex = 0;
            label.Text = "Виберіть таблицю в якій ви хочет змінити дані";
            // 
            // buttonClient
            // 
            buttonClient.Location = new Point(17, 88);
            buttonClient.Name = "buttonClient";
            buttonClient.Size = new Size(214, 52);
            buttonClient.TabIndex = 1;
            buttonClient.Text = "Client";
            buttonClient.UseVisualStyleBackColor = true;
            buttonClient.Click += buttonClient_Click;
            // 
            // buttonStatus
            // 
            buttonStatus.Location = new Point(17, 146);
            buttonStatus.Name = "buttonStatus";
            buttonStatus.Size = new Size(214, 52);
            buttonStatus.TabIndex = 2;
            buttonStatus.Text = "Status";
            buttonStatus.UseVisualStyleBackColor = true;
            buttonStatus.Click += buttonStatus_Click;
            // 
            // buttonCountry
            // 
            buttonCountry.Location = new Point(17, 204);
            buttonCountry.Name = "buttonCountry";
            buttonCountry.Size = new Size(214, 52);
            buttonCountry.TabIndex = 3;
            buttonCountry.Text = "Country";
            buttonCountry.UseVisualStyleBackColor = true;
            buttonCountry.Click += buttonCountry_Click;
            // 
            // buttonDepartment
            // 
            buttonDepartment.Location = new Point(17, 262);
            buttonDepartment.Name = "buttonDepartment";
            buttonDepartment.Size = new Size(214, 52);
            buttonDepartment.TabIndex = 4;
            buttonDepartment.Text = "Department";
            buttonDepartment.UseVisualStyleBackColor = true;
            buttonDepartment.Click += buttonDepartment_Click;
            // 
            // buttonCurrency
            // 
            buttonCurrency.Location = new Point(17, 320);
            buttonCurrency.Name = "buttonCurrency";
            buttonCurrency.Size = new Size(214, 52);
            buttonCurrency.TabIndex = 5;
            buttonCurrency.Text = "Currency";
            buttonCurrency.UseVisualStyleBackColor = true;
            buttonCurrency.Click += buttonCurrency_Click;
            // 
            // buttonExchangeRate
            // 
            buttonExchangeRate.Location = new Point(270, 88);
            buttonExchangeRate.Name = "buttonExchangeRate";
            buttonExchangeRate.Size = new Size(214, 52);
            buttonExchangeRate.TabIndex = 6;
            buttonExchangeRate.Text = "Exchange Rate";
            buttonExchangeRate.UseVisualStyleBackColor = true;
            buttonExchangeRate.Click += buttonExchangeRate_Click;
            // 
            // buttonAccount
            // 
            buttonAccount.Location = new Point(270, 146);
            buttonAccount.Name = "buttonAccount";
            buttonAccount.Size = new Size(214, 52);
            buttonAccount.TabIndex = 7;
            buttonAccount.Text = "Account";
            buttonAccount.UseVisualStyleBackColor = true;
            buttonAccount.Click += buttonAccount_Click;
            // 
            // buttonTypeTransaction
            // 
            buttonTypeTransaction.Location = new Point(270, 204);
            buttonTypeTransaction.Name = "buttonTypeTransaction";
            buttonTypeTransaction.Size = new Size(214, 52);
            buttonTypeTransaction.TabIndex = 8;
            buttonTypeTransaction.Text = "Type Transaction";
            buttonTypeTransaction.UseVisualStyleBackColor = true;
            buttonTypeTransaction.Click += buttonTypeTransaction_Click;
            // 
            // buttonTransactions
            // 
            buttonTransactions.Location = new Point(270, 262);
            buttonTransactions.Name = "buttonTransactions";
            buttonTransactions.Size = new Size(214, 52);
            buttonTransactions.TabIndex = 9;
            buttonTransactions.Text = "Transactions";
            buttonTransactions.UseVisualStyleBackColor = true;
            buttonTransactions.Click += buttonTransactions_Click;
            // 
            // MainMenu
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.WindowFrame;
            ClientSize = new Size(501, 381);
            Controls.Add(buttonTransactions);
            Controls.Add(buttonTypeTransaction);
            Controls.Add(buttonAccount);
            Controls.Add(buttonExchangeRate);
            Controls.Add(buttonCurrency);
            Controls.Add(buttonDepartment);
            Controls.Add(buttonCountry);
            Controls.Add(buttonStatus);
            Controls.Add(buttonClient);
            Controls.Add(label);
            Name = "MainMenu";
            Text = "MainMenu";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label;
        private Button buttonClient;
        private Button buttonStatus;
        private Button buttonCountry;
        private Button buttonDepartment;
        private Button buttonCurrency;
        private Button buttonExchangeRate;
        private Button buttonAccount;
        private Button buttonTypeTransaction;
        private Button buttonTransactions;
    }
}
