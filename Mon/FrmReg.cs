using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Mon
{
    public partial class FrmReg : Form
    {
        public FrmReg()
        {
            InitializeComponent();
            this.Text = Sys.SoftRegister.SOFTNAMECHN + "注册认证";
        }

        public static bool state = false;  //软件是否为可用状态
        private void btn_reg_Click(object sender, EventArgs e)
        {

        }

        private void btn_close_Click(object sender, EventArgs e)
        {
        }

        private void FrmReg_Load(object sender, EventArgs e)
        {
            tb_mac.Text = Sys.SoftRegister.MachineNum;
        }

        private void sbtn_reg_Click(object sender, EventArgs e)
        {
            try
            {
                if (tb_regnum.Text == "")
                {
                    MessageBox.Show("注册码不能为空", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (tb_regnum.Text == Sys.SoftRegister.RegNum)
                {
                    state = Sys.SoftRegister.Regiser();
                    if (state)
                    {
                        MessageBox.Show("注册成功！重启软件后生效！", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                    else
                        MessageBox.Show("注册失败！请重试！", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("注册码错误！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    tb_regnum.SelectAll();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void sbtn_close_Click(object sender, EventArgs e)
        {

            Application.Exit();
        }
    }
}
