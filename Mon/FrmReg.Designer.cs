namespace Mon
{
    partial class FrmReg
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tb_mac = new System.Windows.Forms.TextBox();
            this.tb_regnum = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.sbtn_reg = new CCWin.SkinControl.SkinButton();
            this.sbtn_close = new CCWin.SkinControl.SkinButton();
            this.SuspendLayout();
            // 
            // tb_mac
            // 
            this.tb_mac.Location = new System.Drawing.Point(78, 16);
            this.tb_mac.Name = "tb_mac";
            this.tb_mac.Size = new System.Drawing.Size(283, 21);
            this.tb_mac.TabIndex = 2;
            // 
            // tb_regnum
            // 
            this.tb_regnum.Location = new System.Drawing.Point(78, 49);
            this.tb_regnum.Name = "tb_regnum";
            this.tb_regnum.Size = new System.Drawing.Size(283, 21);
            this.tb_regnum.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "机器码";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(28, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "注册码";
            // 
            // sbtn_reg
            // 
            this.sbtn_reg.BackColor = System.Drawing.Color.Transparent;
            this.sbtn_reg.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.sbtn_reg.DownBack = null;
            this.sbtn_reg.Location = new System.Drawing.Point(140, 80);
            this.sbtn_reg.MouseBack = null;
            this.sbtn_reg.Name = "sbtn_reg";
            this.sbtn_reg.NormlBack = null;
            this.sbtn_reg.Size = new System.Drawing.Size(75, 23);
            this.sbtn_reg.TabIndex = 5;
            this.sbtn_reg.Text = "注册";
            this.sbtn_reg.UseVisualStyleBackColor = false;
            this.sbtn_reg.Click += new System.EventHandler(this.sbtn_reg_Click);
            // 
            // sbtn_close
            // 
            this.sbtn_close.BackColor = System.Drawing.Color.Transparent;
            this.sbtn_close.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.sbtn_close.DownBack = null;
            this.sbtn_close.Location = new System.Drawing.Point(244, 80);
            this.sbtn_close.MouseBack = null;
            this.sbtn_close.Name = "sbtn_close";
            this.sbtn_close.NormlBack = null;
            this.sbtn_close.Size = new System.Drawing.Size(75, 23);
            this.sbtn_close.TabIndex = 5;
            this.sbtn_close.Text = "关闭";
            this.sbtn_close.UseVisualStyleBackColor = false;
            this.sbtn_close.Click += new System.EventHandler(this.sbtn_close_Click);
            // 
            // FrmReg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(373, 115);
            this.ControlBox = false;
            this.Controls.Add(this.sbtn_close);
            this.Controls.Add(this.sbtn_reg);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tb_regnum);
            this.Controls.Add(this.tb_mac);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "FrmReg";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "软件注册";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.FrmReg_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tb_mac;
        private System.Windows.Forms.TextBox tb_regnum;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private CCWin.SkinControl.SkinButton sbtn_reg;
        private CCWin.SkinControl.SkinButton sbtn_close;
    }
}