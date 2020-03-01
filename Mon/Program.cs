using System;
using System.Collections.Generic;
//using System.Linq;
using System.Windows.Forms;
using System.Threading;

namespace Mon
{
    static class Program
    {
        public static Sys.UseOptions UseOptoin = null;
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new FrmMain());
            UseOptoin = Sys.UseOptions.GetOptions();
            if (Sys.SoftRegister.IfTrail)
            {
                if (Sys.SoftRegister.IsTrail())
                {
                    Sys.SoftRegister.Official = false;

                    //if (UseOptoin.RTEMW == 1)
                    //Sys.SoftRegister.SetReturn(Sys.SoftRegister.REGSOFTNAME + "Trail回执");
                    //if (Sys.SoftRegister.UseTimesCtl || Sys.SoftRegister.WriteUseLog)
                    //    Sys.SoftRegister.WriteUseInfo();
                    Thread.Sleep(300);
                    Application.Run(new FrmMain());
                }
                else
                {
                    Sys.SoftRegister.Official = false;

                }
            }
            else
            {
                if (Sys.SoftRegister.IsRegister())
                {
                    Sys.SoftRegister.Official = true;
                    //if (UseOptoin.RTEMW == 1)
                    //    Sys.SoftRegister.SetReturn(Sys.SoftRegister.REGSOFTNAME + "回执");
                    if (Sys.SoftRegister.UseTimesCtl || Sys.SoftRegister.WriteUseLog)
                        Sys.SoftRegister.WriteUseInfo();
                    Thread.Sleep(100);
                    Application.Run(new FrmMain());
                }
                else
                {
                    Sys.SoftRegister.Official = false;
                    Application.Run(new FrmReg());
                }
            }
        }
    }
}
