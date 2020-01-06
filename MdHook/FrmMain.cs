using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace MdHook
{
    public partial class FrmMain : Form
    {
        /// <summary>
        /// 获取窗体的句柄函数
        /// </summary>
        /// <param name="lpClassName">窗口类名</param>
        /// <param name="lpWindowName">窗口标题名</param>
        /// <returns>返回句柄</returns>
        [DllImport("user32.dll", EntryPoint = "FindWindow", CharSet = CharSet.Auto)]
        private extern static IntPtr FindWindow(string classname, string captionName);
        [DllImport("user32.dll", EntryPoint = "FindWindowEx", CharSet = CharSet.Auto)]
        private extern static IntPtr FindWindowEx(IntPtr parent, IntPtr child, string classname, string captionName);
        [DllImport("user32.dll")]
        static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, [MarshalAs(UnmanagedType.LPStr)] string lParam);

        [DllImport("user32.dll", EntryPoint = "SendMessageA")]
        static extern int SendMessage(IntPtr hwnd, int wMsg, int wParam, StringBuilder lParam);
        //[DllImport("user32.dll", EntryPoint = "SendMessageA")]
        //public static extern int SendMessage(int hwnd, int wMsg, int wParam, Byte[] lParam);

        //[DllImport("user32.dll", EntryPoint = "SendMessageA")]
        //public static extern int SendMessage(int hwnd, int wMsg, int wParam, int lParam);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32", EntryPoint = "EnableWindow")]
        public static extern int EnableWindow(int hwnd, int fEnable);
        [DllImport("user32.dll")]
        public static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);
        [DllImport("user32.dll")]
        static extern bool EnableWindow(IntPtr hWnd, bool bEnable);

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll")]
        static extern int GetClassName(IntPtr Handle, [Out] StringBuilder ClassName, int MaxCount);

        //模拟键盘事件         
        [DllImport("user32.dll")]
        public static extern void keybd_event(Byte bVk, Byte bScan, Int32 dwFlags, Int32 dwExtraInfo);
        public delegate bool CallBack(IntPtr hwnd, int lParam);
        [DllImport("user32.dll")]
        public static extern int EnumChildWindows(IntPtr hWndParent, CallBack lpfn, int lParam);

        //给CheckBox发送信息
        //[DllImport("USER32.DLL", EntryPoint = "SendMessage", SetLastError = true, CharSet = CharSet.Auto)]
        //public static extern int SendMessage(IntPtr hwnd, UInt32 wMsg, int wParam, int lParam);
        ////给Text发送信息
        //[DllImport("USER32.DLL", EntryPoint = "SendMessage")]
        //private static extern int SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, string lParam);
        [DllImport("user32.dll")]
        public static extern IntPtr GetWindow(IntPtr hWnd, int wCmd);

        const int WM_CLICK = 0xF5;
        const uint WM_SHOWWINDOW = 0x18;
        const uint WM_SETTEXT = 0xC;
        const int WM_GETTEXT = 0xd;
        const int WM_GETTEXTLENGTH = 0x000E;
        public FrmMain()
        {
            InitializeComponent();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            //modifyInfo();
        }

        public string getWinTitle()
        {
            string[] Day = new string[] { "星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六" };
            string week = Day[Convert.ToInt32(DateTime.Now.DayOfWeek.ToString("d"))].ToString();
            string title = "BLUE游戏控制台 [" + DateTime.Now.ToString("yyyy-MM-dd") + " " + week + "]";
            return title;
        }

        public IntPtr getWinHandle(string serverDataPath)
        {
            IntPtr handle = IntPtr.Zero;

            IntPtr hWnd1 = FindWindow(null, getWinTitle());//GameOfMir微信控制台[正式]
            while (hWnd1 != IntPtr.Zero)
            {
                //if (hWnd1 == IntPtr.Zero)
                //{
                //    note = "[控制台窗口]未找到，请确认控制台窗口标题是否正确";//, "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                //    return handle;
                //}
                //SetForegroundWindow(hWnd1);
                //IntPtr hWnddia = FindWindow("#32770", "错误信息");
                //while (hWnddia != IntPtr.Zero)
                //{
                //    IntPtr hWowner = GetWindow(hWnddia, (int)WindowSearch.GW_OWNER);
                //    if (hWowner == hWnd1)
                //    {

                //    }
                //    hWnddia = FindWindowEx(IntPtr.Zero, hWnddia, "#32770", "错误信息");
                //}

                IntPtr hWndPageControl = FindWindowEx(hWnd1, IntPtr.Zero, "TPageControl", "");//开始
                if (hWndPageControl == IntPtr.Zero)
                {
                    goto NEWHANDLE;
                    //note = "[配置选项卡组]未找到";
                    //return handle;
                }

                IntPtr hWndTabSheetCfg = FindWindowEx(hWndPageControl, IntPtr.Zero, "TTabSheet", "配置向导");
                if (hWndTabSheetCfg == IntPtr.Zero)
                {
                    goto NEWHANDLE;
                    //note = "[配置向导选项卡]未找到";
                    //return handle;
                }
                IntPtr hCfg_pagectl = FindWindowEx(hWndTabSheetCfg, IntPtr.Zero, "TPageControl", "");
                if (hCfg_pagectl == IntPtr.Zero)
                {
                    goto NEWHANDLE;
                    //note = "[配置项目选项组]未找到";
                    //return handle;
                }
                IntPtr hCfg_tabsheet = FindWindowEx(hCfg_pagectl, IntPtr.Zero, "TTabSheet", "第一步(基本设置)");
                if (hCfg_tabsheet == IntPtr.Zero)
                {
                    goto NEWHANDLE;
                    //note = "[第一步(基本设置)]未找到";
                    //return handle;
                }
                IntPtr hCfg_grpbox = FindWindowEx(hCfg_tabsheet, IntPtr.Zero, "TGroupBox", "程序目录及物品数据库设置");
                if (hCfg_grpbox == IntPtr.Zero)
                {
                    goto NEWHANDLE;
                    //note = "[程序目录及物品数据库设置]未找到";
                    //return handle;
                }
                IntPtr hCfg_edit = FindWindowEx(hCfg_grpbox, IntPtr.Zero, "TEdit", null);
                //1 服务器名称   2 数据库名称  3引擎目录
                string cfgpath = "";
                for (int i = 1; i <= 3; i++)
                {
                    hCfg_edit = FindWindowEx(hCfg_grpbox, hCfg_edit, "TEdit", null);
                    if (hCfg_edit != IntPtr.Zero)
                    {
                        //StringBuilder title = new StringBuilder(200);
                        //int len = GetWindowText(hCfg_edit, title, 200);
                        const int buffer_size = 1024;
                        StringBuilder title = new StringBuilder(buffer_size);
                        SendMessage(hCfg_edit, WM_GETTEXT, buffer_size, title);
                        if (i == 3)
                        {
                            cfgpath = title.ToString();
                        }
                    }
                }
                if (cfgpath == "")
                {
                    goto NEWHANDLE;
                    //note = "[数据引擎所在目录]未找到";
                    //return handle;
                }
                if (!cfgpath.EndsWith("\\"))
                    cfgpath += "\\";
                if (!serverDataPath.EndsWith("\\"))
                    serverDataPath += "\\";
                if (cfgpath.ToUpper() == serverDataPath.ToUpper())
                {
                    handle = hWnd1;
                    return handle;
                }
            NEWHANDLE:
                hWnd1 = FindWindowEx(IntPtr.Zero, hWnd1, "TfrmMain", getWinTitle());//FindWindow(null, getWinTitle());
            }
            return handle;
        }

        public void modifyInfo()
        {
            try
            {
                //获取类名
                //StringBuilder ClassName = new StringBuilder(256);
                //GetClassName(btn_start.Handle, ClassName, ClassName.Capacity);
                //IntPtr Handle = FindWindowEx(this.Handle, IntPtr.Zero, ClassName.ToString(), String.Empty);
                //SendMessage(Handle, WM_CLICK, 0);
                //hasNoFound();
                //hasUpdateNote();

                IntPtr hWnd2 = getWinHandle(@"E:\Dev\Hook\Mirserver02\");
                IntPtr hWnd = getWinHandle(@"E:\Dev\Hook\MirserveR\");//E:\Dev\Hook\Mirserver02\
                if(hWnd == IntPtr.Zero)
                {
                    string note = "该配置路径对应的BLUE控制台未找到";
                    return;
                }
                IntPtr hWndPageControl = FindWindowEx(hWnd, IntPtr.Zero, "TPageControl", "");
                IntPtr hWndTabSheet = FindWindowEx(hWndPageControl, IntPtr.Zero, "TTabSheet", "帐号管理");
                if (hWndTabSheet == IntPtr.Zero)
                {
                    //MessageBox.Show("[帐号管理选项卡]未找到", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    return;
                }
                IntPtr hWndaccpwd = FindWindowEx(hWndTabSheet, IntPtr.Zero, "TGroupBox", "登录帐号密码");
                if (hWndaccpwd == IntPtr.Zero)
                {
                    //MessageBox.Show("[登录帐号密码选项卡]未找到", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    return;
                }

                MdInfo mdinfo = new MdInfo();
                mdinfo.acct = "1112222";
                mdinfo.pwd = "hhh";
                mdinfo.bir = "1990/8/9";
                mdinfo.que1 = "问题111";
                mdinfo.ans1 = "答案111";
                mdinfo.que2 = "问题22";
                mdinfo.ans2 = "答案22";

                //文本框账号信息
                IntPtr hWndacc = FindWindowEx(hWndaccpwd, IntPtr.Zero, "TEdit", "");
                if (hWndacc == IntPtr.Zero)
                    return;
                //搜索按钮
                IntPtr hWndbtnser = FindWindowEx(hWndaccpwd, IntPtr.Zero, "TButton", "搜索(&S)");
                if (hWndacc == IntPtr.Zero)
                    return;
                //确定按钮
                IntPtr hWndbtnok = FindWindowEx(hWndaccpwd, IntPtr.Zero, "TButton", "确定(&O)");
                if (hWndacc == IntPtr.Zero)
                    return;

                SendMessage(hWndacc, WM_SETTEXT, IntPtr.Zero, mdinfo.acct);
                SendMessage(hWndbtnser, WM_CLICK, IntPtr.Zero, ""); //发送点击按钮的消息

                //账号信息选项组
                IntPtr hWndaccinfo = IntPtr.Zero;//FindWindowEx(hWndaccpwd, IntPtr.Zero, "", "账号信息");//TGroupBox
                IntPtr CtrlNotifySink = IntPtr.Zero;
                CtrlNotifySink = FindWindowEx(hWndaccpwd, IntPtr.Zero, "TButton", null);
                for (int i = 1; i <= 4; i++)
                {
                    //CtrlNotifySink = FindWindowEx(hWndaccpwd, CtrlNotifySink, "TButton", null);
                    CtrlNotifySink = FindWindowEx(hWndaccpwd, CtrlNotifySink, "TGroupBox", null);
                    if (CtrlNotifySink != IntPtr.Zero)
                    {
                        hWndaccinfo = CtrlNotifySink;
                        break;
                    }
                }
                if (hWndaccinfo == IntPtr.Zero)
                {
                    //MessageBox.Show("[账号信息选项卡]未找到", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    return;
                }
                IntPtr hW_cb_modify = FindWindowEx(hWndaccinfo, IntPtr.Zero, "TCheckBox", "修改帐号信息");
                if (hWndaccinfo == IntPtr.Zero)
                    return;

                //账号:密码 生日 问题1 答案1 问题2 答案2   用冒号隔开的
                IntPtr edit = IntPtr.Zero;
                edit = FindWindowEx(hWndaccinfo, IntPtr.Zero, "TEdit", null);
                List<int> editidx = new List<int>();
                editidx.Add(14);//密码
                editidx.Add(11);//生日
                editidx.Add(10);//问题1
                editidx.Add(9);//答案1
                editidx.Add(8);//问题2
                editidx.Add(7);//答案2

                for (int i = 1; i <= 17; i++)
                {
                    edit = FindWindowEx(hWndaccinfo, edit, "TEdit", null);
                    if (edit != IntPtr.Zero)
                    {
                        if (editidx.Contains(i))
                        {
                            string upvalue = "";
                            switch (i)
                            {
                                case 1:
                                    //更新时间[只读]
                                    break;
                                case 2:
                                    //电话
                                    break;
                                case 3:
                                    //两步验证
                                    break;
                                case 4:
                                    //电子邮箱
                                    break;
                                case 5:
                                    //备注一
                                    break;
                                case 6:
                                    //移动电话
                                    break;
                                case 7:
                                    //答案二
                                    upvalue = mdinfo.ans2;
                                    break;
                                case 8:
                                    //问题二
                                    upvalue = mdinfo.que2;
                                    break;
                                case 9:
                                    //答案一
                                    upvalue = mdinfo.ans1;
                                    break;
                                case 10:
                                    //问题一
                                    upvalue = mdinfo.que1;
                                    break;
                                case 11:
                                    //生日
                                    upvalue = mdinfo.bir;
                                    break;
                                case 12:
                                    //推广号
                                    break;
                                case 13:
                                    //用户名称
                                    break;
                                case 14:
                                    //密码
                                    upvalue = mdinfo.pwd;
                                    break;
                                case 15:
                                    //账号[只读]
                                    break;
                                case 16:
                                    //
                                    break;
                                case 17:
                                    //创建时间[只读]
                                    break;
                                default:
                                    break;
                            }
                            SendMessage(edit, WM_SETTEXT, IntPtr.Zero, upvalue);
                        }
                    }
                }

                SendMessage(hWndbtnok, WM_CLICK, IntPtr.Zero, "");

                if (hasUpdateNote())
                    MessageBox.Show("帐号更新成功");


                //ShowWindow(hWnd2, 0);
                //如Button控件的类名：
                //WindowsForms10.BUTTON.app.0.14fd2b5       WindowsForms10.BUTTON.app.0.21af1a5
                //WindowsForms10.BUTTON.app.0.3ee13a2
                //Button.Text="确定";
                //其中ShowWindow(IntPtr hwnd, int nCmdShow);
                //nCmdShow的含义
                //0    关闭窗口
                //1    正常大小显示窗口
                //2    最小化窗口
                //3    最大化窗口



                //IntPtr waybill = GetWindow(waybillIntPtr, (int)WindowSearch.GW_HWNDNEXT);
                //SendMessage(waybill, WM_SETTEXT, IntPtr.Zero, waybillValue);


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        /// <summary>
        /// 判断是否有  账号未找到对话框
        /// </summary>
        /// <param name="closeDialg">找到是否关闭</param>
        /// <returns></returns>
        private bool hasNoFound(bool closeDialg = true)
        {
            return hasDialog("帐号未找到...", closeDialg);
        }

        /// <summary>
        /// 判断是否有  账号更新对话框
        /// </summary>
        /// <param name="closeDialg">找到是否关闭</param>
        /// <returns></returns>
        private bool hasUpdateNote(bool closeDialg = true)
        {
            return hasDialog("帐号更新成功...", closeDialg);
        }

        /// <summary>
        /// 判断是否有对话框
        /// </summary>
        /// <param name="caption">对话框内容</param>
        /// <param name="closeDialg">找到是否关闭</param>
        /// <returns></returns>
        private bool hasDialog(string caption, bool closeDialg)
        {
            //
            IntPtr hWnddia = FindWindow(null, "提示信息");
            if (hWnddia != IntPtr.Zero)
            {
                IntPtr hWnddiainfo = FindWindowEx(hWnddia, IntPtr.Zero, "Static", caption);
                if (hWnddiainfo != IntPtr.Zero)
                {
                    IntPtr hWnddiabtn = FindWindowEx(hWnddia, IntPtr.Zero, "Button", "确定");
                    if (hWnddiabtn != IntPtr.Zero)
                    {
                        if (closeDialg)
                        {
                            SetForegroundWindow(hWnddia);
                            SendMessage(hWnddiabtn, WM_CLICK, IntPtr.Zero, "");     //发送点击按钮的消息
                        }
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// 判断是否有对话框
        /// </summary>
        /// <param name="caption">对话框内容</param>
        /// <param name="closeDialg">找到是否关闭</param>
        /// <returns></returns>
        private bool hasErrorDialog(string caption, bool closeDialg)
        {
            //
            IntPtr hWnddia = FindWindow(null, "错误信息");
            if (hWnddia != IntPtr.Zero)
            {
                IntPtr hWnddiainfo = FindWindowEx(hWnddia, IntPtr.Zero, "Static", caption);
                if (hWnddiainfo != IntPtr.Zero)
                {
                    IntPtr hWnddiabtn = FindWindowEx(hWnddia, IntPtr.Zero, "Button", "确定");
                    if (hWnddiabtn != IntPtr.Zero)
                    {
                        if (closeDialg)
                        {
                            SetForegroundWindow(hWnddia);
                            SendMessage(hWnddiabtn, WM_CLICK, IntPtr.Zero, "");     //发送点击按钮的消息
                        }
                        return true;
                    }
                }
            }
            return false;
        }



        private void btn_start_Click(object sender, EventArgs e)
        {
            modifyInfo();
        }
    }

    public enum WindowSearch
    {
        GW_HWNDFIRST = 0, //同级别第一个
        GW_HWNDLAST = 1, //同级别最后一个
        GW_HWNDNEXT = 2, //同级别下一个
        GW_HWNDPREV = 3, //同级别上一个
        GW_OWNER = 4, //属主窗口
        GW_CHILD = 5 //子窗口}获取与指定窗口具有指定关系的窗口的句柄 
    }

    public class MdInfo
    {
        //账号:密码 生日 问题1 答案1 问题2 答案2   用冒号隔开的
        public string acct = "";
        public string pwd = "";
        public string bir = "";
        public string que1 = "";
        public string ans1 = "";
        public string que2 = "";
        public string ans2 = "";
    }
}


//public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

//函数说明：在窗口列表中寻找与指定条件相符的第一个窗口

//导入库：user32.lib
//头文件：winuser.h
//命名空间　using System.Runtime.InteropServices;
//参数说明　
//lpClassName　String，窗口类名
//lpWindowName String，窗口标题
//返回值：窗口句柄
//public static extern IntPtr FindWindowEx(IntPtr hwndParent, uint hwndChildAfter, string lpszClass, string lpszWindow);

//函数说明：在窗口列表中寻找与指定条件相符的第一个子窗口

//导入库：user32.lib
//头文件：winuser.h
//命名空间　using System.Runtime.InteropServices;
//参数说明　
//hwndParent IntPtr ，父窗口句柄，如果hwndParent为 0 ，则函数以桌面窗口为父窗口，查找桌面窗口的所有子窗口。
//hwndChildAfter IntPtr ，子窗口句柄，查找从在Z序中的下一个子窗口开始。子窗口必须为hwndParent窗口的直接子窗口而非后代窗口。如果HwndChildAfter为NULL，查找从hwndParent的第一个子窗口开始。如果hwndParent 和 hwndChildAfter同时为NULL，则函数查找所有的顶层窗口及消息窗口。
//lpszClass string ，控件类名
//lpszWindow string ，控件标题，如果该参数为 NULL，则为所有窗口全匹配。
//返回值：控件句柄。

//public static extern int EnumChildWindows(IntPtr hWndParent, CallBack lpfn, int lParam);
//函数功能：枚举一个父窗口的所有子窗口。
//导入库：user32.lib
//头文件：winuser.h
//命名空间　using System.Runtime.InteropServices;
//参数说明
//hWndParent IntPtr 父窗口句柄
//lpfn CallBack 回调函数的地址
//lParam int 自定义的参数
//注意：回调函数的返回值将会影响到这个API函数的行为。如果回调函数返回true，则枚举继续直到枚举完成；如果返回false，则将会中止枚举。
//其中CallBack是这样的一个委托：public delegate bool CallBack(IntPtr hwnd, int lParam); 如果 CallBack 返回的是true，则会继续枚举，否则就会终止枚举。
