namespace Mon
{
    partial class FrmMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.sbtn_r = new CCWin.SkinControl.SkinButton();
            this.sbtn_rstop = new CCWin.SkinControl.SkinButton();
            this.label_time = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lb_bd = new System.Windows.Forms.ListBox();
            this.cb_auto = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tb_r = new System.Windows.Forms.TextBox();
            this.sbtn_browser = new CCWin.SkinControl.SkinButton();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.panel1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // sbtn_r
            // 
            this.sbtn_r.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.sbtn_r.BackColor = System.Drawing.Color.Transparent;
            this.sbtn_r.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.sbtn_r.DownBack = null;
            this.sbtn_r.Location = new System.Drawing.Point(718, 6);
            this.sbtn_r.MouseBack = null;
            this.sbtn_r.Name = "sbtn_r";
            this.sbtn_r.NormlBack = null;
            this.sbtn_r.Size = new System.Drawing.Size(71, 23);
            this.sbtn_r.TabIndex = 0;
            this.sbtn_r.Text = "开始监控";
            this.sbtn_r.UseVisualStyleBackColor = false;
            this.sbtn_r.Click += new System.EventHandler(this.sbtn_r_Click);
            // 
            // sbtn_rstop
            // 
            this.sbtn_rstop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.sbtn_rstop.BackColor = System.Drawing.Color.Transparent;
            this.sbtn_rstop.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.sbtn_rstop.DownBack = null;
            this.sbtn_rstop.Location = new System.Drawing.Point(795, 6);
            this.sbtn_rstop.MouseBack = null;
            this.sbtn_rstop.Name = "sbtn_rstop";
            this.sbtn_rstop.NormlBack = null;
            this.sbtn_rstop.Size = new System.Drawing.Size(70, 23);
            this.sbtn_rstop.TabIndex = 0;
            this.sbtn_rstop.Text = "停止监控";
            this.sbtn_rstop.UseVisualStyleBackColor = false;
            this.sbtn_rstop.Click += new System.EventHandler(this.sbtn_rstop_Click);
            // 
            // label_time
            // 
            this.label_time.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label_time.AutoSize = true;
            this.label_time.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_time.ForeColor = System.Drawing.Color.Blue;
            this.label_time.Location = new System.Drawing.Point(561, 12);
            this.label_time.Name = "label_time";
            this.label_time.Size = new System.Drawing.Size(63, 13);
            this.label_time.TabIndex = 7;
            this.label_time.Text = "12:00:00";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "具体区服路径";
            // 
            // lb_bd
            // 
            this.lb_bd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lb_bd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lb_bd.FormattingEnabled = true;
            this.lb_bd.ItemHeight = 12;
            this.lb_bd.Location = new System.Drawing.Point(0, 0);
            this.lb_bd.Name = "lb_bd";
            this.lb_bd.Size = new System.Drawing.Size(512, 518);
            this.lb_bd.TabIndex = 3;
            // 
            // cb_auto
            // 
            this.cb_auto.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cb_auto.AutoSize = true;
            this.cb_auto.Location = new System.Drawing.Point(636, 10);
            this.cb_auto.Name = "cb_auto";
            this.cb_auto.Size = new System.Drawing.Size(72, 16);
            this.cb_auto.TabIndex = 8;
            this.cb_auto.Text = "自动监控";
            this.cb_auto.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.splitContainer1);
            this.panel1.Controls.Add(this.sbtn_browser);
            this.panel1.Controls.Add(this.sbtn_r);
            this.panel1.Controls.Add(this.cb_auto);
            this.panel1.Controls.Add(this.sbtn_rstop);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label_time);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(4, 34);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(888, 555);
            this.panel1.TabIndex = 3;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitContainer1.Location = new System.Drawing.Point(0, 35);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tb_r);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.lb_bd);
            this.splitContainer1.Size = new System.Drawing.Size(886, 518);
            this.splitContainer1.SplitterDistance = 370;
            this.splitContainer1.TabIndex = 9;
            // 
            // tb_r
            // 
            this.tb_r.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tb_r.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tb_r.Location = new System.Drawing.Point(0, 0);
            this.tb_r.Multiline = true;
            this.tb_r.Name = "tb_r";
            this.tb_r.Size = new System.Drawing.Size(370, 518);
            this.tb_r.TabIndex = 3;
            // 
            // sbtn_browser
            // 
            this.sbtn_browser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.sbtn_browser.BackColor = System.Drawing.Color.Transparent;
            this.sbtn_browser.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.sbtn_browser.DownBack = null;
            this.sbtn_browser.Location = new System.Drawing.Point(97, 6);
            this.sbtn_browser.MouseBack = null;
            this.sbtn_browser.Name = "sbtn_browser";
            this.sbtn_browser.NormlBack = null;
            this.sbtn_browser.Size = new System.Drawing.Size(88, 23);
            this.sbtn_browser.TabIndex = 0;
            this.sbtn_browser.Text = "搜索目录";
            this.sbtn_browser.UseVisualStyleBackColor = false;
            this.sbtn_browser.Click += new System.EventHandler(this.sbtn_browser_Click);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.HighlightText;
            this.ClientSize = new System.Drawing.Size(896, 593);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "账号信息修改工具";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private CCWin.SkinControl.SkinButton sbtn_r;
        private System.Windows.Forms.Label label1;
        private CCWin.SkinControl.SkinButton sbtn_rstop;
        private System.Windows.Forms.Label label_time;
        private System.Windows.Forms.CheckBox cb_auto;
        private System.Windows.Forms.ListBox lb_bd;
        private System.Windows.Forms.Panel panel1;
        private CCWin.SkinControl.SkinButton sbtn_browser;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TextBox tb_r;
    }
}

