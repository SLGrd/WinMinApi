namespace RealBoxNetCls
{
    partial class txtCnpjBox
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
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnl = new System.Windows.Forms.Panel();
            this.tBox = new System.Windows.Forms.TextBox();
            this.Footer = new System.Windows.Forms.Label();
            this.Caption = new System.Windows.Forms.Label();
            this.pnl.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnl
            // 
            this.pnl.BackColor = System.Drawing.Color.LightGray;
            this.pnl.Controls.Add(this.tBox);
            this.pnl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl.Location = new System.Drawing.Point(0, 32);
            this.pnl.Name = "pnl";
            this.pnl.Padding = new System.Windows.Forms.Padding(6);
            this.pnl.Size = new System.Drawing.Size(400, 93);
            this.pnl.TabIndex = 6;
            // 
            // tBox
            // 
            this.tBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tBox.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.tBox.Location = new System.Drawing.Point(6, 6);
            this.tBox.Multiline = true;
            this.tBox.Name = "tBox";
            this.tBox.Size = new System.Drawing.Size(388, 81);
            this.tBox.TabIndex = 0;
            this.tBox.Enter += new System.EventHandler(this.TxtRealBoxControl_Enter);
            this.tBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnKeyDown);
            this.tBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.OnKeyPress);
            this.tBox.Leave += new System.EventHandler(this.TxtRealBoxControl_Leave);
            // 
            // Footer
            // 
            this.Footer.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Footer.ForeColor = System.Drawing.Color.Firebrick;
            this.Footer.Location = new System.Drawing.Point(0, 125);
            this.Footer.Name = "Footer";
            this.Footer.Size = new System.Drawing.Size(400, 25);
            this.Footer.TabIndex = 5;
            this.Footer.Text = "label2";
            this.Footer.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // Caption
            // 
            this.Caption.AutoSize = true;
            this.Caption.Dock = System.Windows.Forms.DockStyle.Top;
            this.Caption.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.Caption.ForeColor = System.Drawing.Color.White;
            this.Caption.Location = new System.Drawing.Point(0, 0);
            this.Caption.Name = "Caption";
            this.Caption.Size = new System.Drawing.Size(83, 32);
            this.Caption.TabIndex = 4;
            this.Caption.Text = "label1";
            // 
            // txtCnpjBox
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.pnl);
            this.Controls.Add(this.Footer);
            this.Controls.Add(this.Caption);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "txtCnpjBox";
            this.Size = new System.Drawing.Size(400, 150);
            this.pnl.ResumeLayout(false);
            this.pnl.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Panel pnl;
        private TextBox tBox;
        private Label Footer;
        private Label Caption;
    }
}
