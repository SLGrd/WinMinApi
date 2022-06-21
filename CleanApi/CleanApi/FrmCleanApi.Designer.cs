namespace CleanApi
{
    partial class FrmCleanApi
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
            this.txrNome = new RealBoxNetCls.txtText();
            this.txtCpf = new RealBoxNetCls.txtCpfBox();
            this.txtCnpj = new RealBoxNetCls.txtCnpjBox();
            this.txmPhone = new RealBoxNetCls.txtMaskNum();
            this.txmCep = new RealBoxNetCls.txtMaskNum();
            this.txnProductId = new RealBoxNetCls.txtRealNum();
            this.txnQtty = new RealBoxNetCls.txtRealNum();
            this.txnUnitPrice = new RealBoxNetCls.txtRealNum();
            this.txnTotalValue = new RealBoxNetCls.txtRealNum();
            this.txtMsgs = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txrNome
            // 
            this.txrNome.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txrNome.FontSize = 16;
            this.txrNome.Location = new System.Drawing.Point(78, 162);
            this.txrNome.Mask = "________________________";
            this.txrNome.Msg = "Nome completo do comprador";
            this.txrNome.Name = "txrNome";
            this.txrNome.Size = new System.Drawing.Size(1382, 121);
            this.txrNome.TabIndex = 0;
            this.txrNome.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txrNome.TextCase = RealBoxNetCls.txtText.TextControl.Regular;
            this.txrNome.Title = "Nome do Comprador";
            this.txrNome.UText = "";
            // 
            // txtCpf
            // 
            this.txtCpf.BackColor = System.Drawing.Color.Transparent;
            this.txtCpf.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtCpf.FontSize = 14;
            this.txtCpf.ForeColor = System.Drawing.Color.Red;
            this.txtCpf.Location = new System.Drawing.Point(78, 297);
            this.txtCpf.Margin = new System.Windows.Forms.Padding(0);
            this.txtCpf.Mask = "___.___.___-__";
            this.txtCpf.Msg = "Informe o CPF";
            this.txtCpf.Name = "txtCpf";
            this.txtCpf.Size = new System.Drawing.Size(323, 122);
            this.txtCpf.TabIndex = 1;
            this.txtCpf.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtCpf.Title = "CPF";
            this.txtCpf.UText = "___........-..";
            // 
            // txtCnpj
            // 
            this.txtCnpj.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtCnpj.FontSize = 14;
            this.txtCnpj.Location = new System.Drawing.Point(431, 297);
            this.txtCnpj.Margin = new System.Windows.Forms.Padding(0);
            this.txtCnpj.Mask = "__.___.___/____-__";
            this.txtCnpj.Msg = "Informe o CNPJ";
            this.txtCnpj.Name = "txtCnpj";
            this.txtCnpj.Size = new System.Drawing.Size(323, 122);
            this.txtCnpj.TabIndex = 2;
            this.txtCnpj.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtCnpj.Title = "CNPJ";
            this.txtCnpj.UText = "__......../../.-./";
            // 
            // txmPhone
            // 
            this.txmPhone.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txmPhone.FontSize = 16;
            this.txmPhone.Location = new System.Drawing.Point(784, 297);
            this.txmPhone.Mask = "(__) ___ ___ ___";
            this.txmPhone.Msg = "Phone number BR";
            this.txmPhone.Name = "txmPhone";
            this.txmPhone.Size = new System.Drawing.Size(323, 122);
            this.txmPhone.TabIndex = 3;
            this.txmPhone.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txmPhone.Title = "Phone";
            this.txmPhone.UText = "((_) _)  ___  __";
            // 
            // txmCep
            // 
            this.txmCep.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txmCep.FontSize = 16;
            this.txmCep.Location = new System.Drawing.Point(1137, 297);
            this.txmCep.Mask = "__.___-___";
            this.txmCep.Msg = "CEP BR";
            this.txmCep.Name = "txmCep";
            this.txmCep.Size = new System.Drawing.Size(323, 122);
            this.txmCep.TabIndex = 4;
            this.txmCep.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txmCep.Title = "CEP";
            this.txmCep.UText = "__..__-_-_";
            // 
            // txnProductId
            // 
            this.txnProductId.DecimalPlaces = 0;
            this.txnProductId.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txnProductId.FontSize = 14;
            this.txnProductId.Location = new System.Drawing.Point(78, 422);
            this.txnProductId.MaxLength = 5;
            this.txnProductId.Msg = "Msg";
            this.txnProductId.Name = "txnProductId";
            this.txnProductId.Size = new System.Drawing.Size(323, 122);
            this.txnProductId.TabIndex = 5;
            this.txnProductId.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txnProductId.Title = "Product Id";
            this.txnProductId.UText = "0";
            // 
            // txnQtty
            // 
            this.txnQtty.DecimalPlaces = 0;
            this.txnQtty.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txnQtty.FontSize = 14;
            this.txnQtty.Location = new System.Drawing.Point(431, 422);
            this.txnQtty.MaxLength = 12;
            this.txnQtty.Msg = "Msg";
            this.txnQtty.Name = "txnQtty";
            this.txnQtty.Size = new System.Drawing.Size(323, 122);
            this.txnQtty.TabIndex = 6;
            this.txnQtty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txnQtty.Title = "Peso (g)";
            this.txnQtty.UText = "0";
            this.txnQtty._TextChanged += new System.EventHandler(this.TxrQtty__TextChanged);
            // 
            // txnUnitPrice
            // 
            this.txnUnitPrice.DecimalPlaces = 2;
            this.txnUnitPrice.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txnUnitPrice.FontSize = 14;
            this.txnUnitPrice.Location = new System.Drawing.Point(784, 422);
            this.txnUnitPrice.MaxLength = 12;
            this.txnUnitPrice.Msg = "Msg";
            this.txnUnitPrice.Name = "txnUnitPrice";
            this.txnUnitPrice.Size = new System.Drawing.Size(323, 122);
            this.txnUnitPrice.TabIndex = 7;
            this.txnUnitPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txnUnitPrice.Title = "Preço Unitario";
            this.txnUnitPrice.UText = "0.00";
            this.txnUnitPrice._TextChanged += new System.EventHandler(this.TxrQtty__TextChanged);
            // 
            // txnTotalValue
            // 
            this.txnTotalValue.DecimalPlaces = 2;
            this.txnTotalValue.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txnTotalValue.FontSize = 14;
            this.txnTotalValue.Location = new System.Drawing.Point(1137, 422);
            this.txnTotalValue.MaxLength = 12;
            this.txnTotalValue.Msg = "Msg";
            this.txnTotalValue.Name = "txnTotalValue";
            this.txnTotalValue.Size = new System.Drawing.Size(323, 122);
            this.txnTotalValue.TabIndex = 8;
            this.txnTotalValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txnTotalValue.Title = "Valor Total";
            this.txnTotalValue.UText = "0.00";
            // 
            // txtMsgs
            // 
            this.txtMsgs.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtMsgs.Location = new System.Drawing.Point(75, 584);
            this.txtMsgs.Multiline = true;
            this.txtMsgs.Name = "txtMsgs";
            this.txtMsgs.Size = new System.Drawing.Size(1384, 78);
            this.txtMsgs.TabIndex = 9;
            // 
            // FrmCleanApi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.ClientSize = new System.Drawing.Size(1540, 749);
            this.Controls.Add(this.txtMsgs);
            this.Controls.Add(this.txnTotalValue);
            this.Controls.Add(this.txnUnitPrice);
            this.Controls.Add(this.txnQtty);
            this.Controls.Add(this.txnProductId);
            this.Controls.Add(this.txmCep);
            this.Controls.Add(this.txmPhone);
            this.Controls.Add(this.txtCnpj);
            this.Controls.Add(this.txtCpf);
            this.Controls.Add(this.txrNome);
            this.Name = "FrmCleanApi";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.FrmCleanApi_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private RealBoxNetCls.txtText txrNome;
        private RealBoxNetCls.txtCpfBox txtCpf;
        private RealBoxNetCls.txtCnpjBox txtCnpj;
        private RealBoxNetCls.txtMaskNum txmPhone;
        private RealBoxNetCls.txtMaskNum txmCep;
        private RealBoxNetCls.txtRealNum txnProductId;
        private RealBoxNetCls.txtRealNum txnQtty;
        private RealBoxNetCls.txtRealNum txnUnitPrice;
        private RealBoxNetCls.txtRealNum txnTotalValue;
        private TextBox txtMsgs;
    }
}