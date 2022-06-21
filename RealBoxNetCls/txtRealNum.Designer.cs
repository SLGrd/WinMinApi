namespace RealBoxNetCls
{
    partial class txtRealNum
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
            this.Caption = new System.Windows.Forms.Label();
            this.Footer = new System.Windows.Forms.Label();
            this.pnl = new System.Windows.Forms.Panel();
            this.tBox = new System.Windows.Forms.TextBox();
            this.pnl.SuspendLayout();
            this.SuspendLayout();
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
            this.Caption.TabIndex = 0;
            this.Caption.Text = "label1";
            // 
            // Footer
            // 
            this.Footer.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Footer.ForeColor = System.Drawing.Color.Firebrick;
            this.Footer.Location = new System.Drawing.Point(0, 125);
            this.Footer.Name = "Footer";
            this.Footer.Size = new System.Drawing.Size(425, 25);
            this.Footer.TabIndex = 1;
            this.Footer.Text = "label2";
            this.Footer.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // pnl
            // 
            this.pnl.BackColor = System.Drawing.Color.LightGray;
            this.pnl.Controls.Add(this.tBox);
            this.pnl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl.Location = new System.Drawing.Point(0, 32);
            this.pnl.Name = "pnl";
            this.pnl.Padding = new System.Windows.Forms.Padding(6);
            this.pnl.Size = new System.Drawing.Size(425, 93);
            this.pnl.TabIndex = 2;
            // 
            // tBox
            // 
            this.tBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tBox.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.tBox.Location = new System.Drawing.Point(6, 6);
            this.tBox.Multiline = true;
            this.tBox.Name = "tBox";
            this.tBox.Size = new System.Drawing.Size(413, 81);
            this.tBox.TabIndex = 0;
            this.tBox.TextChanged += new System.EventHandler(this.TxtBox_TextChanged);
            this.tBox.Enter += new System.EventHandler(this.TxtRealBoxControl_Enter);
            this.tBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnKeyDown);
            this.tBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.OnKeyPress);
            this.tBox.Leave += new System.EventHandler(this.TxtRealBoxControl_Leave);
            // 
            // txtRealNum
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.pnl);
            this.Controls.Add(this.Footer);
            this.Controls.Add(this.Caption);
            this.Name = "txtRealNum";
            this.Size = new System.Drawing.Size(425, 150);
            this.pnl.ResumeLayout(false);
            this.pnl.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label Caption;
        private Label Footer;
        private Panel pnl;
        private TextBox tBox;
    }
}
