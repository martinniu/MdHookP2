
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
//using System.Linq;
using System.Windows.Forms;
using System.Reflection;
using System.IO;
using System.Security.Cryptography;
using System.Xml.Serialization;
using System.Xml;
using CCWin;
using System.Net;
using System.Management;
using System.Threading;
using System.Diagnostics;
using Microsoft.Win32;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;


namespace Mon
{
    public partial class FrmMain : CCWin.Skin_DevExpress
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
        [DllImport("user32.dll")]
        static extern IntPtr PostMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, [MarshalAs(UnmanagedType.LPStr)] string lParam);

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

        [DllImport("user32.dll", EntryPoint = "SetParent")]//设置父窗体
        public static extern int SetParent(
        int hWndChild,
        int hWndNewParent
        );
        [DllImport("user32.dll")]
        public static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);
        [DllImport("user32.dll")]
        static extern bool EnableWindow(IntPtr hWnd, bool bEnable);

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        [DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Auto)]
        /// <summary>
        /// 该函数获得一个指定子窗口的父窗口句柄。
        /// </summary>
        public static extern IntPtr GetParent(IntPtr hWnd);
        [DllImport("user32.dll")]
        static extern int GetClassName(IntPtr Handle, [Out] StringBuilder ClassName, int MaxCount);

        //模拟键盘事件     //虚拟键值 // 一般为0   //这里是整数类型 0 为按下，2为释放   
        [DllImport("user32.dll")]
        public static extern void keybd_event(Byte bVk, Byte bScan, Int32 dwFlags, Int32 dwExtraInfo);

        /// <summary>
        /// Delegate for the EnumChildWindows method
        /// </summary>
        /// <param name="hWnd">Window handle</param>
        /// <param name="parameter">Caller-defined variable; we use it for a pointer to our list</param>
        /// <returns>True to continue enumerating, false to bail.</returns>
        public delegate bool EnumWindowProc(IntPtr hWnd, IntPtr parameter);
        public delegate bool CallBack(IntPtr hwnd, int lParam);
        [DllImport("user32.dll")]
        public static extern int EnumChildWindows(IntPtr hWndParent, CallBack lpfn, int lParam);

        [DllImport("user32")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool EnumChildWindows(IntPtr window, EnumWindowProc callback, IntPtr i);
        //给CheckBox发送信息
        //[DllImport("USER32.DLL", EntryPoint = "SendMessage", SetLastError = true, CharSet = CharSet.Auto)]
        //public static extern int SendMessage(IntPtr hwnd, UInt32 wMsg, int wParam, int lParam);
        ////给Text发送信息
        //[DllImport("USER32.DLL", EntryPoint = "SendMessage")]
        //private static extern int SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, string lParam);
        [DllImport("user32.dll")]
        public static extern IntPtr GetWindow(IntPtr hWnd, int wCmd);
        [DllImport("user32.dll", EntryPoint = "GetWindow")]//获取窗体句柄，hwnd为源窗口句柄
        /*wCmd指定结果窗口与源窗口的关系，它们建立在下述常数基础上：
        GW_CHILD
        寻找源窗口的第一个子窗口
        GW_HWNDFIRST
        为一个源子窗口寻找第一个兄弟（同级）窗口，或寻找第一个顶级窗口
        GW_HWNDLAST
        为一个源子窗口寻找最后一个兄弟（同级）窗口，或寻找最后一个顶级窗口
        GW_HWNDNEXT
        为源窗口寻找下一个兄弟窗口
        GW_HWNDPREV
        为源窗口寻找前一个兄弟窗口
        GW_OWNER
        寻找窗口的所有者
        */
        public static extern int GetWindow(int hwnd, int wCmd);
        [DllImport("user32.dll")]
        private static extern bool SetCursorPos(int X, int Y);
        [DllImport("user32.dll")]
        private static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);
        //[DllImport("user32.dll")]
        //private static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, uint dwExtraInfo);
        [DllImport("user32.dll")]
        private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndlnsertAfter, int X, int Y, int cx, int cy, uint Flags);

        //ShowWindow参数
        private const int SW_SHOWNORMAL = 1;
        private const int SW_RESTORE = 9;
        private const int SW_SHOWNOACTIVATE = 4;
        //SendMessage参数
        private const int WM_KEYDOWN = 0X100;
        private const int WM_KEYUP = 0X101;
        private const int WM_SYSCHAR = 0X106;
        private const int WM_SYSKEYUP = 0X105;
        private const int WM_SYSKEYDOWN = 0X104;
        private const int WM_CHAR = 0X102;
        private const int WM_ESC_key = 0x1B;

        const int WM_CLICK = 0xF5;
        const uint WM_SHOWWINDOW = 0x18;
        const uint WM_SETTEXT = 0xC;
        const int WM_GETTEXT = 0xd;
        const int WM_GETTEXTLENGTH = 0x000E;

        Thread thread_bd = null;
        public Options option = null;
        public string LOGPATH = Application.StartupPath + "\\日志";
        System.Timers.Timer timer_time = new System.Timers.Timer();
        System.Timers.Timer timer_bd = new System.Timers.Timer();
        System.Timers.Timer timer_search = new System.Timers.Timer();
        public static object locker = new object();//添加一个对象作为锁

        //public List<string> Folders = new List<string>();
        //public int SearchSecs = 10;
        public bool Auto = true;
        public string SerKey = "MirServer";
        public int MonSecs = 2;
        public int SearchSecs = 10;
        public List<string> Folders = new List<string>();
        public string SerSubPath = "Mir200\\Envir\\QuestDiary\\帐号担保";
        public string SPLIT = "`";
        public string ToolPath = "远程账号管理工具.exe";
        //使用公共工具
        public bool PublicTool = false;
        /// <summary>
        /// 登录信息字： 路径   登录信息
        /// </summary>
        public Dictionary<string, LoginInfo> LoginInfos = new Dictionary<string, LoginInfo>();

        /// <summary>
        /// 修改
        /// </summary>
        public string File_XG = "修改信息.txt";
        public List<string> Errors = new List<string>();
        /// <summary>
        /// 所有窗口句柄
        /// </summary>
        Dictionary<string, WinHandleMan> winHandles = new Dictionary<string, WinHandleMan>();
        //public IntPtr MAIN_HANDLE = IntPtr.Zero;
        //WinHandle winHandle = new WinHandle();
        /// <summary>
        /// 1.1 20191231 创建
        /// </summary>

        public FrmMain()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            getWinHandle();
            option = Options.GetOptions();
            Auto = option.Auto;
            if (option.MonSecs > 0)
                MonSecs = option.MonSecs;
            if (option.SearchSecs > 0)
                SearchSecs = option.SearchSecs;
            if (option.SerKey != "")
                SerKey = option.SerKey;
            if (option.ToolPath != "")
            {
                if (option.ToolPath.Contains(":"))
                {
                    if (new FileInfo(option.ToolPath).Exists)
                    {
                        PublicTool = true;
                        ToolPath = option.ToolPath;
                    }
                    else
                    {
                        SetControlPropertyDlgt(new string[] { "ListBox", "lb_bd", "ItemsAdd", "配置账号管理工具[" + option.ToolPath + "]不存在, 默认搜索根目录" });
                    }
                }
                else
                {
                    ToolPath = option.ToolPath;
                }
            }

            //string startpath = Application.StartupPath;
            //startpath = startpath.Substring(0, startpath.LastIndexOf("\\"));
            //if (new FileInfo(startpath + "\\Config.ini").Exists)
            //{
            //    DataPath = startpath;
            //}

            Folders = option.Folders;
            if (option.File_XG != "")
                File_XG = option.File_XG;
            if (option.SerSubPath != "")
            {
                SerSubPath = option.SerSubPath;
                if (SerSubPath.StartsWith("\\"))
                    SerSubPath = SerSubPath.Substring(1);
            }
            for (int i = 0; i < Folders.Count; i++)
            {
                string subpath = Folders[i];
                List<string> lginfo = getLoginInfo(subpath);
                string ip = lginfo[0];
                if (ip == "")
                {
                    if (!Errors.Contains(subpath.ToLower()))
                    {
                        Errors.Add(subpath.ToLower());
                        SetControlPropertyDlgt(new string[] { "ListBox", "lb_bd", "ItemsAdd", "配置目录下[" + subpath + "]未读取到配置IP" });
                    }
                    continue;
                }
                //LoginGate\config.ini  GatePort=7002 
                string port = lginfo[1];
                if (port == "")
                {
                    if (!Errors.Contains(subpath.ToLower()))
                    {
                        Errors.Add(subpath.ToLower());
                        SetControlPropertyDlgt(new string[] { "ListBox", "lb_bd", "ItemsAdd", "配置目录下[" + subpath + "]未读取到配置端口" });
                    }
                    continue;
                }
                //LoginSrv\Logsrv.ini  LoginPassword=wwssdw11 
                string pwd = lginfo[2];
                if (pwd == "")
                {
                    if (!Errors.Contains(subpath.ToLower()))
                    {
                        Errors.Add(subpath.ToLower());
                        SetControlPropertyDlgt(new string[] { "ListBox", "lb_bd", "ItemsAdd", "配置目录下[" + subpath + "]未读取到配置密码" });
                    }
                    continue;
                }

                if (SerSubPath.StartsWith("\\"))
                    subpath += SerSubPath;
                else
                    subpath += "\\" + SerSubPath;
                //子路径包含修改文件
                if (new FileInfo(subpath + "\\" + File_XG).Exists)
                {
                    LoginInfo linfo = new LoginInfo(Folders[i].ToLower(), ip, port, pwd);
                    if (!LoginInfos.ContainsKey(Folders[i].ToLower()))
                    {
                        LoginInfos.Add(Folders[i].ToLower(), linfo);
                    }
                    else
                    {
                        LoginInfos[Folders[i].ToLower()] = linfo;
                    }
                    tb_r.Text += Folders[i] + "\r\n";
                }
                else
                {
                    if (!Errors.Contains(subpath.ToLower()))
                    {
                        Errors.Add(subpath.ToLower());
                        SetControlPropertyDlgt(new string[] { "ListBox", "lb_bd", "ItemsAdd", "配置目录下[" + subpath + "]未读取到修改文件" });
                    }
                }
            }
            sbtn_rstop.Enabled = false;
            timer_time.Elapsed += new System.Timers.ElapsedEventHandler(SetTime);
            timer_time.AutoReset = true;
            timer_time.Interval = 1000;
            //timer_time.Enabled = true;
            timer_time.Start();

            cb_auto.Checked = Auto;
            //Dictionary<string, RowInfo> ss = GetRows(@"D:\ZBJ\sqlite\MirServer\MirServer1\Mir200\Envir\QuestDiary\担保系统\修改资料.txt");
            timer_search.Elapsed += new System.Timers.ElapsedEventHandler(SearchFolders);
            timer_search.AutoReset = true;
            timer_search.Interval = SearchSecs * 1000;

            //modifyInfo();
            //Mon_BD();
            if (Auto)
            {
                //timer_time.Enabled = true;
                timer_search.Start();
                AutoStartBD();
            }
        }


        /// <summary>
        /// Returns a list of child windows
        /// </summary>
        /// <param name="parent">Parent of the windows to return</param>
        /// <returns>List of child windows</returns>
        public static List<IntPtr> GetChildWindows(IntPtr parent)
        {
            List<IntPtr> result = new List<IntPtr>();
            GCHandle listHandle = GCHandle.Alloc(result);
            try
            {
                EnumWindowProc childProc = new EnumWindowProc(EnumWindow);
                EnumChildWindows(parent, childProc, GCHandle.ToIntPtr(listHandle));
            }
            finally
            {
                if (listHandle.IsAllocated)
                    listHandle.Free();
            }
            return result;
        }

        /// <summary>
        /// Callback method to be used when enumerating windows.
        /// </summary>
        /// <param name="handle">Handle of the next window</param>
        /// <param name="pointer">Pointer to a GCHandle that holds a reference to the list to fill</param>
        /// <returns>True to continue the enumeration, false to bail</returns>
        private static bool EnumWindow(IntPtr handle, IntPtr pointer)
        {
            GCHandle gch = GCHandle.FromIntPtr(pointer);
            List<IntPtr> list = gch.Target as List<IntPtr>;
            if (list == null)
            {
                throw new InvalidCastException("GCHandle Target could not be cast as List<IntPtr>");
            }
            list.Add(handle);
            //  You can modify this to check to see if you want to cancel the operation, then return a null here
            return true;
        }


        public void getFolders()
        {
            string[] paths = tb_r.Text.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            Folders = new List<string>();
            for (int i = 0; i < paths.Length; i++)
            {
                Folders.Add(paths[i]);
            }
        }

        public void SaveOptions()
        {
            //MSecsR = int.Parse(tb_tm_r.Text);
            option.Auto = cb_auto.Checked;
            option.Folders = Folders;
            option.SaveOptions();
        }

        private void SerFolders(List<string> rootpath)
        {
            SetControlPropertyDlgt(new string[] { "TextBox", "tb_f", "Text", "" });
            List<string> fds = new List<string>();
            string cfstr = "";
            string[] curarr = tb_r.Text.Trim().Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);// tb_r.Text.;
            List<string> alst = new List<string>();
            for (int c = 0; c < curarr.Length; c++)
            {
                alst.Add(curarr[c].ToLower());
                cfstr += curarr[c] + "\r\n";
            }

            for (int i = 0; i < rootpath.Count; i++)
            {
                DirectoryInfo di = new DirectoryInfo(rootpath[i]);
                if (di.Exists)
                {
                    DirectoryInfo[] dis = di.GetDirectories("*" + SerKey + "*");
                    for (int k = 0; k < dis.Length; k++)
                    {
                        string subpath = dis[k].FullName;
                        List<string> lginfo = getLoginInfo(subpath);
                        string ip = lginfo[0];
                        if (ip == "")
                        {
                            if (!Errors.Contains(subpath.ToLower()))
                            {
                                Errors.Add(subpath.ToLower());
                                SetControlPropertyDlgt(new string[] { "ListBox", "lb_bd", "ItemsAdd", "当前目录下[" + subpath + "]未读取到配置IP" });
                            }
                            continue;
                        }
                        //LoginGate\config.ini  GatePort=7002 
                        string port = lginfo[1];
                        if (port == "")
                        {
                            if (!Errors.Contains(subpath.ToLower()))
                            {
                                Errors.Add(subpath.ToLower());
                                SetControlPropertyDlgt(new string[] { "ListBox", "lb_bd", "ItemsAdd", "当前目录下[" + subpath + "]未读取到配置端口" });
                            }
                            continue;
                        }
                        //LoginSrv\Logsrv.ini  LoginPassword=wwssdw11 
                        string pwd = lginfo[2];
                        if (pwd == "")
                        {
                            if (!Errors.Contains(subpath.ToLower()))
                            {
                                Errors.Add(subpath.ToLower());
                                SetControlPropertyDlgt(new string[] { "ListBox", "lb_bd", "ItemsAdd", "当前目录下[" + subpath + "]未读取到配置密码" });
                            }
                            continue;
                        }

                        if (SerSubPath.StartsWith("\\"))
                            subpath += SerSubPath;
                        else
                            subpath += "\\" + SerSubPath;
                        //子路径包含修改文件
                        if (new FileInfo(subpath + "\\" + File_XG).Exists)
                        {
                            LoginInfo linfo = new LoginInfo(dis[k].FullName.ToLower(), ip, port, pwd);
                            if (!alst.Contains(dis[k].FullName.ToLower()))
                            {

                                if (!LoginInfos.ContainsKey(dis[k].FullName.ToLower()))
                                {
                                    LoginInfos.Add(dis[k].FullName.ToLower(), linfo);
                                }
                                else
                                {
                                    LoginInfos[dis[k].FullName.ToLower()] = linfo;
                                }
                                fds.Add(dis[k].FullName);
                                cfstr += dis[k].FullName + "\r\n";
                            }
                        }
                        else
                        {
                            if (!Errors.Contains(subpath.ToLower()))
                            {
                                Errors.Add(subpath.ToLower());
                                SetControlPropertyDlgt(new string[] { "ListBox", "lb_bd", "ItemsAdd", "当前目录下[" + subpath + "]未读取到修改文件" });
                            }
                        }
                    }
                }
            }
            if (fds.Count > 0)
            {
                if (cfstr.EndsWith("\r\n"))
                {
                    cfstr = cfstr.Substring(0, cfstr.Length - 2);
                }
                SetControlPropertyDlgt(new string[] { "TextBox", "tb_r", "Text", cfstr });
                string lg = "新增: [" + fds.Count + "] 个目录";
                SetControlPropertyDlgt(new string[] { "ListBox", "lb_bd", "ItemsAdd", lg });
                //SetControlPropertyDlgt(new string[] { "TextBox", "tb_f", "Text", lg });
                //SetControlPropertyDlgt(new string[] { "ListBox", "lb_sr", "ItemsAdd", lg });
                SaveOptions();
            }
            //tb_r.Text = fstr;
        }

        private List<string> getLoginInfo(string path)
        {
            //根目录下包含config.ini  ExtIPaddr=106.12.18.156   
            string ip = GetCfg(path + "\\config.ini", "ExtIPaddr");
            //LoginGate\config.ini  GatePort=7002 
            string port = GetCfg(path + "\\LoginGate\\config.ini", "GatePort");
            //LoginSrv\Logsrv.ini  LoginPassword=wwssdw11 
            string pwd = GetCfg(path + "\\LoginSrv\\Logsrv.ini", "LoginPassword");
            List<string> res = new List<string>();
            res.Add(ip);
            res.Add(port);
            res.Add(pwd);
            return res;
        }

        private void sbtn_browser_Click(object sender, EventArgs e)
        {
            if (SerKey == "")
            {
                MessageBox.Show("搜索关键字为空");
            }
            else
            {
                Search();
            }
            //if (DataPath != "" && new DirectoryInfo(DataPath).Exists)
            //{
            //    folderBrowserDialog1.SelectedPath = DataPath;
            //}
            //else
            //{
            //    folderBrowserDialog1.SelectedPath = Application.StartupPath;
            //}
            //if (folderBrowserDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            //{
            //    DataPath = folderBrowserDialog1.SelectedPath;
            //    tb_path.Text = DataPath;
            //}
            //option.DataPath = DataPath;
            //option.SaveOptions();
        }

        private void sbtn_r_Click(object sender, EventArgs e)
        {
            Mon_BD();
            //timer_search.Start();
            //AutoStartBD();
        }

        private void sbtn_rstop_Click(object sender, EventArgs e)
        {
            sbtn_r.Enabled = true;
            sbtn_rstop.Enabled = false;
            AutoStopBD();
        }


        private void AutoStartBD()
        {
            try
            {
                lb_bd.Items.Clear();

                //MAIN_HANDLE = getWinHandle(DataPath);//E:\Dev\Hook\Mirserver02\
                //string note = "";
                //if (MAIN_HANDLE == IntPtr.Zero)
                //{
                //    note = "该配置路径对应的BLUE控制台未找到，请检查";
                //    MessageBox.Show(note, "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                //    return;
                //}
                //else
                //{
                //    SetControlPropertyDlgt(new string[] { "ListBox", "lb_bd", "ItemsAdd", "查找BLUE控制台窗口完成" });
                //}
                //note = initWinHandle();
                //if (note != "")
                //{
                //    MessageBox.Show("初始化窗口组件出错: " + note, "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                //    return;
                //}
                //else
                //{
                //    SetControlPropertyDlgt(new string[] { "ListBox", "lb_bd", "ItemsAdd", "初始化窗口组件完成" });
                //}

                sbtn_r.Enabled = false;
                sbtn_rstop.Enabled = true;
                //lb_bd.Items.Add(DateTime.Now.ToString("HH:mm:ss  ") + "开始监控");
                //string a = getDialogText(MAIN_HANDLE);

                //Mon_BD();
                //return;

                thread_bd = new Thread(new ThreadStart(StartMonBDWhile));
                thread_bd.Start();
                //timer_bd.Elapsed += new System.Timers.ElapsedEventHandler(StartMonBD);
                //timer_bd.AutoReset = true;
                //timer_bd.Interval = MonSecs*1000;
                //timer_time.Enabled = true;
                //timer_bd.Start();
            }
            catch (Exception ex)
            {
                WriteLog("AutoStartR 开始监控", ex);
            }
        }

        private void AutoStopBD()
        {
            try
            {
                //timer_bd.Stop();
                timer_search.Stop();
                thread_bd.Abort();
            }
            catch (Exception ex)
            {
                //WriteLog("AutoStopR 停止监控", ex);
            }
            finally
            {
                lb_bd.Items.Add(DateTime.Now.ToString("HH:mm:ss  ") + "停止监控");
            }
        }

        private void SearchFolders(object source, System.Timers.ElapsedEventArgs e)
        {
            Search();
        }

        private void Search()
        {
            List<string> rootpath = new List<string>();
            rootpath.Add("D:\\");
            rootpath.Add("E:\\");
            rootpath.Add("F:\\");
            SerFolders(rootpath);
        }

        private void SetTime(object source, System.Timers.ElapsedEventArgs e)
        {
            string cut = DateTime.Now.ToString("HH:mm:ss");
            SetControlPropertyDlgt(new string[] { "Label", "label_time", "Text", cut });
        }

        private void StartMonBD(object source, System.Timers.ElapsedEventArgs e)
        {
            thread_bd = new Thread(new ThreadStart(Mon_BD));
            thread_bd.Start();
        }


        private void StartMonBDWhile()
        {

            try
            {
                while (true)
                {
                    Mon_BD();
                    Thread.Sleep(MonSecs * 1000);
                }
            }
            catch (Exception ex)
            {
                WriteLog("While开始监控", ex);
            }
        }

        /// <summary>
        /// 监控文件：绑定
        /// </summary>
        private void Mon_BD()
        {
            try
            {
                //closeDialog(MAIN_HANDLE);
                getFolders();
                if (Folders.Count == 0)
                {
                    //SetControlPropertyDlgt(new string[] { "ListBox", "lb_bd", "ItemsAdd", "路径列表为空" });
                    return;
                }
                for (int i = 0; i < Folders.Count; i++)
                {
                    string pstr = Folders[i];
                    if (!pstr.EndsWith("\\"))
                        pstr += "\\";

                    //修改文件处理
                    List<MdInfo> acctxg = new List<MdInfo>();
                    string file = pstr + SerSubPath + "\\" + File_XG;
                    if (new FileInfo(file).Exists)
                    {
                        //MdInfo mdinfo = new MdInfo();
                        //mdinfo.acct = "1112222";
                        //mdinfo.pwd = "hhh";
                        //mdinfo.bir = "1990/8/9";
                        //mdinfo.que1 = "问题111";
                        //mdinfo.ans1 = "答案111";
                        //mdinfo.que2 = "问题22";
                        //mdinfo.ans2 = "答案22";
                        string data = ReadFile(file).Trim();
                        string[] arr = data.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                        if (arr.Length > 0)
                        {
                            for (int k = 0; k < arr.Length; k++)
                            {
                                string line = arr[k];
                                if (line.Contains(":"))
                                {
                                    string[] tarr = line.Split(new string[] { ":" }, StringSplitOptions.None);
                                    //帐号:密码 生日 问题1 答案1 问题2 答案2
                                    if (tarr.Length >= 7)
                                    {
                                        acctxg.Add(new MdInfo(tarr[0], tarr[1], tarr[2], tarr[3], tarr[4], tarr[5], tarr[6]));
                                    }
                                }
                            }
                        }
                    }
                    int msucc = 0;
                    for (int k = 0; k < acctxg.Count; k++)
                    {
                        string res = modifyInfo(acctxg[k]);
                        if (res.Contains("帐号更新成功"))
                        {
                            res = "修改帐号[" + acctxg[k].acct + "]-成功  " + res;
                            msucc++;
                        }
                        else
                        {
                            res = "修改帐号[" + acctxg[k].acct + "]-失败  " + res;
                        }
                        SetControlPropertyDlgt(new string[] { "ListBox", "lb_bd", "ItemsAdd", res });
                        if (acctxg.Count > 1)
                            Thread.Sleep(300);
                    }
                    if (acctxg.Count > 0)
                    {
                        //WriteFile(file, "");
                        if (msucc > 0)
                            SetControlPropertyDlgt(new string[] { "ListBox", "lb_bd", "ItemsAdd", "修改账号信息完成: " + msucc.ToString() });
                    }
                    //Thread.Sleep(200);

                    //for (int k = 0; k < acctcx.Count; k++)
                    //{
                    //    string res = "";
                    //    MdInfo mdInfo = searchInfo(acctcx[k].acct, out res);
                    //    if (res == "" && mdInfo.acct != "")
                    //    {
                    //        res = "查询帐号[" + acctcx[k].acct + "]-成功  " + mdInfo.bir + "|" + mdInfo.ans1 + "|" + mdInfo.que1 + "|" + mdInfo.ans2 + "|" + mdInfo.que2;
                    //        mdInfo.role = acctcx[k].role;
                    //        acctcx_succ.Add(mdInfo);
                    //    }
                    //    else
                    //    {
                    //        res = "查询帐号[" + acctcx[k].acct + "]-失败  " + res;
                    //    }
                    //    SetControlPropertyDlgt(new string[] { "ListBox", "lb_bd", "ItemsAdd", res });
                    //    if (acctcx.Count > 1)
                    //        Thread.Sleep(300);
                    //}
                }
            }
            catch (Exception ex)
            {
                WriteLog("线程处理报错", ex);
            }
        }

        /// <summary>
        /// 获取游戏控制台窗口名称
        /// </summary>
        /// <returns></returns>
        public string getWinTitle()
        {
            string[] Day = new string[] { "星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六" };
            string week = Day[Convert.ToInt32(DateTime.Now.DayOfWeek.ToString("d"))].ToString();
            string title = "BLUE游戏控制台 [" + DateTime.Now.ToString("yyyy-MM-dd") + " " + week + "]";
            return title;
        }

        public string getHandleText(IntPtr edit)
        {
            const int buffer_size = 1024;
            StringBuilder title = new StringBuilder(buffer_size);
            SendMessage(edit, WM_GETTEXT, buffer_size, title);
            return title.ToString();
        }

        /// <summary>
        /// 根据配置获取控制台Handle
        /// </summary>
        /// <param name="serverDataPath">配置路径</param>
        /// <returns></returns>
        public IntPtr getWinHandle()
        {
            IntPtr handle = IntPtr.Zero;
            string winTitle = "远程账号管理 [www.gameofmir.com]";
            IntPtr hWnd1 = FindWindow(null, winTitle);
            while (hWnd1 != IntPtr.Zero)
            {
                //if (hWnd1 == IntPtr.Zero)
                //{
                //    note = "[控制台窗口]未找到，请确认控制台窗口标题是否正确";//, "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                //    return handle;
                //}                
                //搜索按钮
                IntPtr hWndbtnser = FindWindowEx(hWnd1, IntPtr.Zero, "TButton", "查询");
                if (hWndbtnser == IntPtr.Zero)
                    goto NEWHANDLE;
                //修改按钮
                IntPtr hWndbtnmdy = FindWindowEx(hWnd1, IntPtr.Zero, "TButton", "修改");
                if (hWndbtnmdy == IntPtr.Zero)
                    goto NEWHANDLE;
                IntPtr hWndbtnadd = FindWindowEx(hWnd1, IntPtr.Zero, "TButton", "增加");
                if (hWndbtnadd == IntPtr.Zero)
                    goto NEWHANDLE;
                IntPtr hWndbtndel = FindWindowEx(hWnd1, IntPtr.Zero, "TButton", "删除");
                if (hWndbtndel == IntPtr.Zero)
                    goto NEWHANDLE;
                IntPtr hWndphone = FindWindowEx(hWnd1, IntPtr.Zero, "TEdit", null);
                if (hWndphone == IntPtr.Zero)
                    goto NEWHANDLE;
                WinHandleMan handleMan = new WinHandleMan();
                handleMan.Main = hWnd1;
                handleMan.E_Phone = hWndphone;
                handleMan.BTN_Ser = hWndbtnser;
                handleMan.BTN_Mdy = hWndbtnmdy;
                handleMan.BTN_Add = hWndbtnadd;
                handleMan.BTN_Del = hWndbtndel;


                IntPtr hWchild2 = GetWindow(hWnd1, (int)WindowSearch.GW_HWNDPREV);
                string txt2 = getHandleText(hWchild2);

                int maxSer = 5;
                int seridx = 0;
                bool sersucc = false;
                IntPtr hWchild = GetWindow(hWnd1, (int)WindowSearch.GW_HWNDNEXT);
                while (hWchild != IntPtr.Zero)
                {
                    seridx++;
                    if (seridx >= maxSer)
                        break;
                    string txt = getHandleText(hWchild);
                    if (txt == "登陆")
                    {
                        handleMan.Login.LMain = hWchild;
                        sersucc = true;
                        break;
                    }
                    hWchild = GetWindow(hWchild, (int)WindowSearch.GW_HWNDNEXT);
                }
                if(!sersucc)
                {
                    seridx = 0;
                    hWchild = GetWindow(hWnd1, (int)WindowSearch.GW_HWNDPREV);
                    while (hWchild != IntPtr.Zero)
                    {
                        seridx++;
                        if (seridx >= maxSer)
                            break;
                        string txt = getHandleText(hWchild);
                        if (txt == "登陆")
                        {
                            handleMan.Login.LMain = hWchild;
                            sersucc = true;
                            break;
                        }
                        hWchild = GetWindow(hWchild, (int)WindowSearch.GW_HWNDPREV);
                    }
                }

                if(!sersucc)
                {
                    //未找到登陆窗口
                    goto NEWHANDLE;
                }
                hWchild = handleMan.Login.LMain;
                IntPtr hWclogin = FindWindowEx(hWchild, IntPtr.Zero, "TButton", "登陆");
                IntPtr hWcclose = FindWindowEx(hWchild, IntPtr.Zero, "TButton", "关闭");
                IntPtr hWcgroup = FindWindowEx(hWchild, IntPtr.Zero, "TGroupBox", null);
                IntPtr cedit = FindWindowEx(hWcgroup, IntPtr.Zero, "TEdit", null);
                for (int i = 1; i <= 4; i++)
                {
                    cedit = FindWindowEx(hWcgroup, cedit, "TEdit", null);
                    if (cedit != IntPtr.Zero)
                    {
                        string s = getHandleText(cedit);
                        switch (i)
                        {
                            case 1:
                                handleMan.Login.LE_IP = cedit;
                                break;
                            case 2:
                                handleMan.Login.LE_Port = cedit;
                                break;
                            case 4:
                                handleMan.Login.LE_Pwd = cedit;
                                break;
                        }
                    }
                }
                //IntPtr hWnddia = FindWindow("TFrmLogin", "登陆");
                //while (hWnddia != IntPtr.Zero)
                //{
                //    IntPtr hWowner2 = GetWindow(hWnddia, (int)WindowSearch.GW_HWNDPREV);
                //    if (hWowner2 == hWnd1)
                //    {
                //        break;
                //    }
                //    hWnddia = FindWindowEx(IntPtr.Zero, hWnddia, "TFrmLogin", "登陆");
                //}
                IntPtr hWndGroupBox = FindWindowEx(hWnd1, IntPtr.Zero, "TGroupBox", "账号");//账号组合框
                if (hWndGroupBox == IntPtr.Zero)
                {
                    goto NEWHANDLE;
                }
                IntPtr hCfg_edit = FindWindowEx(hWndGroupBox, IntPtr.Zero, "TEdit", null);
                for (int i = 1; i <= 11; i++)
                {
                    hCfg_edit = FindWindowEx(hWndGroupBox, hCfg_edit, "TEdit", null);
                    if (hCfg_edit != IntPtr.Zero)
                    {
                        //StringBuilder title = new StringBuilder(200);
                        //int len = GetWindowText(hCfg_edit, title, 200);
                        //const int buffer_size = 1024;
                        //StringBuilder title = new StringBuilder(buffer_size);
                        //SendMessage(hCfg_edit, WM_GETTEXT, buffer_size, title);
                        switch (i)
                        {
                            case 1:
                                //问题一
                                handleMan.E_Que1 = hCfg_edit;
                                break;
                            case 2:
                                handleMan.E_Pwd = hCfg_edit;
                                break;
                            case 3:
                                handleMan.E_Name = hCfg_edit;
                                break;
                            case 4:
                                //生日
                                handleMan.E_Bir = hCfg_edit;
                                break;
                            case 5:
                                //问题二
                                handleMan.E_Que2 = hCfg_edit;
                                break;
                            case 6:
                                //答案二
                                handleMan.E_Ans2 = hCfg_edit;
                                break;
                            case 7:
                                //电子邮箱
                                handleMan.E_Mail = hCfg_edit;
                                break;
                            case 8:
                                handleMan.E_Card = hCfg_edit;
                                break;
                            case 9:
                                handleMan.E_Acct = hCfg_edit;
                                break;
                            case 10:
                                break;
                            case 11:
                                handleMan.E_Ans1 = hCfg_edit;
                                break;
                        }
                    }
                }

            NEWHANDLE:
                hWnd1 = FindWindowEx(IntPtr.Zero, hWnd1, "TfrmMain", winTitle);//FindWindow(null, getWinTitle());
            }
            return handle;
        }

        //获取类名
        //StringBuilder ClassName = new StringBuilder(256);
        //GetClassName(btn_start.Handle, ClassName, ClassName.Capacity);
        //IntPtr Handle = FindWindowEx(this.Handle, IntPtr.Zero, ClassName.ToString(), String.Empty);
        //SendMessage(Handle, WM_CLICK, 0);

        public string modifyInfo(MdInfo mdinfo)
        {
            string mdres = "";
            try
            {
                SendMessage(winHandle.IN_Acct, WM_SETTEXT, IntPtr.Zero, mdinfo.acct);
                //SendMessage(winHandle.BTN_Search, WM_CLICK, IntPtr.Zero, ""); //搜索点击
                PostMessage(winHandle.BTN_Search, WM_CLICK, IntPtr.Zero, "");//点击确定更新
                Thread.Sleep(300);
                while (true)
                {
                    mdres = getDialogText(MAIN_HANDLE);
                    if (mdres != "")
                        break;
                    else
                    {
                        const int buffer_size = 256;
                        StringBuilder txt = new StringBuilder(buffer_size);
                        SendMessage(winHandle.E_Acct, WM_GETTEXT, buffer_size, txt);
                        if (txt.ToString() != "")
                            break;
                    }
                    Thread.Sleep(50);
                }
                if (mdres != "")
                    return mdres;

                SendMessage(winHandle.E_Bir, WM_SETTEXT, IntPtr.Zero, mdinfo.bir);
                SendMessage(winHandle.E_Pwd, WM_SETTEXT, IntPtr.Zero, mdinfo.pwd);
                SendMessage(winHandle.E_Que1, WM_SETTEXT, IntPtr.Zero, mdinfo.que1);
                SendMessage(winHandle.E_Que2, WM_SETTEXT, IntPtr.Zero, mdinfo.que2);
                SendMessage(winHandle.E_Ans1, WM_SETTEXT, IntPtr.Zero, mdinfo.ans1);
                SendMessage(winHandle.E_Ans2, WM_SETTEXT, IntPtr.Zero, mdinfo.ans2);
                SendMessage(winHandle.E_Two, WM_SETTEXT, IntPtr.Zero, "");

                //SendMessage(winHandle.BTN_OK, WM_CLICK, IntPtr.Zero, "");//点击确定更新
                PostMessage(winHandle.BTN_OK, WM_CLICK, IntPtr.Zero, "");//点击确定更新
                while (true)
                {
                    mdres = getDialogText(MAIN_HANDLE);
                    if (mdres != "")
                        break;
                    Thread.Sleep(50);
                }
            }
            catch (Exception ex)
            {
                return "修改错误: " + ex.Message;
            }
            return mdres;
        }

        public MdInfo searchInfo(string acct, out string mdres)
        {
            MdInfo mdinfo = new MdInfo();
            mdres = "";
            try
            {
                SendMessage(winHandle.IN_Acct, WM_SETTEXT, IntPtr.Zero, acct);
                SetForegroundWindow(MAIN_HANDLE);
                //SendMessage(winHandle.BTN_Search, WM_CLICK, IntPtr.Zero, ""); //搜索点击
                PostMessage(winHandle.BTN_Search, WM_CLICK, IntPtr.Zero, "");
                Thread.Sleep(300);
                const int buffer_size = 256;
                StringBuilder txt = new StringBuilder(buffer_size);
                while (true)
                {
                    mdres = getDialogText(MAIN_HANDLE);
                    if (mdres != "")
                        break;
                    else
                    {
                        txt = new StringBuilder(buffer_size);
                        SendMessage(winHandle.E_Acct, WM_GETTEXT, buffer_size, txt);
                        if (txt.ToString() != "")
                            break;
                    }
                    Thread.Sleep(50);
                }
                if (mdres != "")
                    return mdinfo;

                txt = new StringBuilder(buffer_size);
                SendMessage(winHandle.E_Acct, WM_GETTEXT, buffer_size, txt);
                mdinfo.acct = txt.ToString();
                txt = new StringBuilder(buffer_size);
                SendMessage(winHandle.E_Bir, WM_GETTEXT, buffer_size, txt);
                mdinfo.bir = txt.ToString();
                txt = new StringBuilder(buffer_size);
                SendMessage(winHandle.E_Que1, WM_GETTEXT, buffer_size, txt);
                mdinfo.que1 = txt.ToString();
                txt = new StringBuilder(buffer_size);
                SendMessage(winHandle.E_Que2, WM_GETTEXT, buffer_size, txt);
                mdinfo.que2 = txt.ToString();
                txt = new StringBuilder(buffer_size);
                SendMessage(winHandle.E_Ans1, WM_GETTEXT, buffer_size, txt);
                mdinfo.ans1 = txt.ToString();
                txt = new StringBuilder(buffer_size);
                SendMessage(winHandle.E_Ans2, WM_GETTEXT, buffer_size, txt);
                mdinfo.ans2 = txt.ToString();

                mdres = "";
                //SendMessage(winHandle.BTN_OK, WM_CLICK, IntPtr.Zero, "");//点击确定更新
                //PostMessage(winHandle.BTN_OK, WM_CLICK, IntPtr.Zero, "");//点击确定更新
                //while (true)
                //{
                //    mdres = getDialogText(MAIN_HANDLE);
                //    if (mdres != "")
                //        break;
                //    Thread.Sleep(50);
                //}
            }
            catch (Exception ex)
            {
                mdres = "查询错误: " + ex.Message;
            }
            return mdinfo;
        }
        public string initWinHandle()
        {
            string mdres = "";
            try
            {
                winHandle = new WinHandle();
                if (MAIN_HANDLE == IntPtr.Zero)
                {
                    mdres = "错误: 该配置路径对应的BLUE控制台未找到";
                    //SetControlPropertyDlgt(new string[] { "ListBox", "lb_bd", "ItemsAdd", mdres });
                    return mdres;
                }
                IntPtr hWndPageControl = FindWindowEx(MAIN_HANDLE, IntPtr.Zero, "TPageControl", "");
                IntPtr hWndTabSheet = FindWindowEx(hWndPageControl, IntPtr.Zero, "TTabSheet", "帐号管理");
                if (hWndTabSheet == IntPtr.Zero)
                {
                    return "[帐号管理选项卡]未找到";
                }
                IntPtr hWndaccpwd = FindWindowEx(hWndTabSheet, IntPtr.Zero, "TGroupBox", "登录帐号密码");
                if (hWndaccpwd == IntPtr.Zero)
                {
                    return "[登录帐号密码选项卡]未找到";
                }

                //文本框帐号信息
                IntPtr hWndacc = FindWindowEx(hWndaccpwd, IntPtr.Zero, "TEdit", "");
                if (hWndacc == IntPtr.Zero)
                    return "[文本框帐号]未找到";
                //搜索按钮
                IntPtr hWndbtnser = FindWindowEx(hWndaccpwd, IntPtr.Zero, "TButton", "搜索(&S)");
                if (hWndacc == IntPtr.Zero)
                    return "[搜索按钮]未找到";
                //确定按钮
                IntPtr hWndbtnok = FindWindowEx(hWndaccpwd, IntPtr.Zero, "TButton", "确定(&O)");
                if (hWndacc == IntPtr.Zero)
                    return "[确定按钮]未找到";
                winHandle.IN_Acct = hWndacc;
                winHandle.BTN_OK = hWndbtnok;
                winHandle.BTN_Search = hWndbtnser;

                //帐号信息选项组
                IntPtr hWndaccinfo = IntPtr.Zero;//FindWindowEx(hWndaccpwd, IntPtr.Zero, "", "帐号信息");//TGroupBox
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
                    return "[帐号信息选项卡]未找到";
                }
                IntPtr hW_cb_modify = FindWindowEx(hWndaccinfo, IntPtr.Zero, "TCheckBox", "修改帐号信息");
                if (hWndaccinfo == IntPtr.Zero)
                    return "[修改帐号信息选项卡]未找到"; ;

                //帐号:密码 生日 问题1 答案1 问题2 答案2   用冒号隔开的
                IntPtr edit = IntPtr.Zero;
                edit = FindWindowEx(hWndaccinfo, IntPtr.Zero, "TEdit", null);
                List<int> editidx = new List<int>();
                editidx.Add(15);//帐号
                editidx.Add(14);//密码
                editidx.Add(11);//生日
                editidx.Add(10);//问题1
                editidx.Add(9);//答案1
                editidx.Add(8);//问题2
                editidx.Add(7);//答案2
                editidx.Add(3);//两步验证更新为空

                #region 获取文本框
                for (int i = 1; i <= 17; i++)
                {
                    edit = FindWindowEx(hWndaccinfo, edit, "TEdit", null);
                    if (edit != IntPtr.Zero)
                    {
                        if (editidx.Contains(i))
                        {
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
                                    winHandle.E_Two = edit;
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
                                    winHandle.E_Ans2 = edit;
                                    break;
                                case 8:
                                    //问题二
                                    winHandle.E_Que2 = edit;
                                    break;
                                case 9:
                                    //答案一
                                    winHandle.E_Ans1 = edit;
                                    break;
                                case 10:
                                    //问题一
                                    winHandle.E_Que1 = edit;
                                    break;
                                case 11:
                                    //生日
                                    winHandle.E_Bir = edit;
                                    break;
                                case 12:
                                    //推广号
                                    break;
                                case 13:
                                    //用户名称
                                    break;
                                case 14:
                                    //密码
                                    winHandle.E_Pwd = edit;
                                    break;
                                case 15:
                                    //帐号[只读]
                                    winHandle.E_Acct = edit;
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
                        }
                    }
                }
                #endregion

                return "";

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
                return "错误: " + ex.Message;
            }
        }


        private List<IntPtr> getDialogHandle(IntPtr parentHwnd)
        {
            List<IntPtr> hds = new List<IntPtr>();
            IntPtr hWnddia = FindWindow("#32770", "错误信息");
            while (hWnddia != IntPtr.Zero)
            {
                IntPtr hWowner = GetWindow(hWnddia, (int)WindowSearch.GW_OWNER);
                if (hWowner == parentHwnd)
                {
                    hds.Add(hWnddia);
                    break;
                }
                hWnddia = FindWindowEx(IntPtr.Zero, hWnddia, "#32770", "错误信息");
            }

            if (hds.Count == 0)
            {
                hWnddia = FindWindow("#32770", "提示信息");
                while (hWnddia != IntPtr.Zero)
                {
                    IntPtr hWowner = GetWindow(hWnddia, (int)WindowSearch.GW_OWNER);
                    if (hWowner == parentHwnd)
                    {
                        hds.Add(hWnddia);
                        break;
                    }
                    hWnddia = FindWindowEx(IntPtr.Zero, hWnddia, "#32770", "提示信息");
                }
            }
            return hds;
        }


        private string getDialogText(IntPtr parentHwnd, bool closeDialg = true)
        {
            List<IntPtr> hds = getDialogHandle(parentHwnd);
            string text = "";
            for (int i = 0; i < hds.Count; i++)
            {
                const int buffer_size = 1024;
                StringBuilder title1 = new StringBuilder(buffer_size);
                SendMessage(hds[i], WM_GETTEXT, buffer_size, title1);
                IntPtr hWnddiainfo = FindWindowEx(hds[i], IntPtr.Zero, "Static", null);
                hWnddiainfo = FindWindowEx(hds[i], hWnddiainfo, "Static", null);
                StringBuilder txt = new StringBuilder(buffer_size);
                SendMessage(hWnddiainfo, WM_GETTEXT, buffer_size, txt);
                text = title1.ToString() + ":" + txt.ToString();
                if (closeDialg)
                {
                    SetForegroundWindow(hds[i]);
                    keybd_event((byte)Keys.Escape, 0, 0, 0);
                    keybd_event((byte)Keys.Escape, 0, 2, 0);
                    //IntPtr hWnddiabtn = FindWindowEx(hds[i], IntPtr.Zero, "Button", "确定");
                    //if (hWnddiabtn != IntPtr.Zero)
                    //{
                    //    SetForegroundWindow(hds[i]);
                    //    SendMessage(hWnddiabtn, WM_CLICK, IntPtr.Zero, "");
                    //}
                }
            }
            return text;
        }

        private void closeDialog(IntPtr parentHwnd)
        {
            //SetForegroundWindow(hWnd1);
            List<IntPtr> hds = getDialogHandle(parentHwnd);
            for (int i = 0; i < hds.Count; i++)
            {
                SetForegroundWindow(hds[i]);
                keybd_event((byte)Keys.Escape, 0, 0, 0);
                keybd_event((byte)Keys.Escape, 0, 2, 0);
                //IntPtr hWnddiabtn = FindWindowEx(hds[i], IntPtr.Zero, "Button", "确定");
                //if (hWnddiabtn != IntPtr.Zero)
                //{
                //    SetForegroundWindow(hds[i]);
                //    SendMessage(hWnddiabtn, WM_CLICK, IntPtr.Zero, "");     //发送点击按钮的消息
                //}
                //SetForegroundWindow(hds[i]);
                //SendMessage(hds[i], WM_SYSKEYDOWN, (IntPtr)WM_ESC_key, ""); //输入TAB（0x09）
                //SendMessage(hds[i], WM_SYSKEYUP, (IntPtr)WM_ESC_key, "");
            }
            //SendMessage(hWnddiabtn, WM_CLICK, IntPtr.Zero, "");     //发送点击按钮的消息
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


        public static bool WriteFile(string filepath, string info)
        {
            try
            {
                //DirectoryInfo dirinfo = new DirectoryInfo(filepath);
                //if (!dirinfo.Exists)
                //    dirinfo.Create();
                //string datetime = DateTime.Now.ToString("yyyyMMdd_HHmmss.fff");
                //string fname = Log.filepath + "\\错误日志" + datetime + ".txt";
                //string filebak = filepath + "bak";
                FileStream filestream = new FileStream(filepath, FileMode.Create, FileAccess.Write, FileShare.ReadWrite); //fileinfo.Open(FileMode.Open,FileAccess.Write,FileShare.ReadWrite);
                StreamWriter streamwriter = new StreamWriter(filestream, Encoding.Default);
                streamwriter.BaseStream.Seek(0, SeekOrigin.End);
                if (info == "\r\n" || info == "\r\n\r\n")
                    info = "";
                if (info.EndsWith("\r\n\r\n"))
                    info = info.Substring(0, info.Length - 2);
                streamwriter.Write(info);
                streamwriter.Flush();
                streamwriter.Dispose();
                streamwriter.Close();
                filestream.Dispose();
                filestream.Close();
                //FileInfo fbak = new FileInfo(filebak);
                //if (fbak.Exists)
                //{
                //    FileInfo fileinfo = new FileInfo(filepath);
                //    if (fileinfo.Exists)
                //        fileinfo.Delete();
                //    fbak.MoveTo(filepath);
                //}
                return true;
            }
            catch (Exception exp)
            {
                return false;
                throw exp;
            }
        }

        public string GetCfg(string path, string key)
        {
            string valstr = "";
            string filestr = ReadFile(path);
            string[] arr = filestr.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i].Replace(" ", "").StartsWith(key + "="))
                {
                    valstr = arr[i].Substring(arr[i].IndexOf("=") + 1);
                    break;
                }
            }
            return valstr;
        }

        public string ReadFile(string fname)
        {
            string line = "";
            try
            {
                FileInfo fileinfo = new FileInfo(fname);
                if (!fileinfo.Exists)
                    return "";
                StreamReader sr = new StreamReader(fname, Encoding.Default);// Encoding.Default
                line = sr.ReadToEnd();
                sr.Close();
            }
            catch (Exception exp)
            {
                return "";
            }
            return line;
        }

        public string WriteLog(string errorinfo, Exception ex)
        {
            string exinfo = "\r\n" + string.Format("{0,15}", "Message: ") + ex.Message + "\r\n" + string.Format("{0,15}", "Source: ") + ex.Source + "\r\n" + string.Format("{0,15}", "TargetSite: ") + ex.TargetSite + "\r\n\r\n" + string.Format("{0,15}", "StackTrace: ") + "\r\n" + ex.StackTrace;
            return WriteLog(errorinfo + exinfo);
        }
        public string WriteLog(string errorinfo)
        {
            try
            {
                DirectoryInfo dirinfo = new DirectoryInfo(LOGPATH);
                if (!dirinfo.Exists)
                    dirinfo.Create();
                string datetime = DateTime.Now.ToString("yyyyMMdd_HH");//mmss.fff
                string fname = LOGPATH + "\\错误日志" + datetime + ".txt";
                FileInfo fileinfo = new FileInfo(fname);
                //if (fileinfo.Exists)
                //    fileinfo.Delete();
                FileStream filestream = new FileStream(fname, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite); //fileinfo.Open(FileMode.Open,FileAccess.Write,FileShare.ReadWrite);
                StreamWriter streamwriter = new StreamWriter(filestream, Encoding.Default);
                streamwriter.BaseStream.Seek(0, SeekOrigin.End);
                streamwriter.WriteLine(DateTime.Now.ToString("mm:ss.fff") + "  " + errorinfo);
                streamwriter.Flush();
                streamwriter.Dispose();
                streamwriter.Close();
                filestream.Dispose();
                filestream.Close();
                return fname;
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        public string WriteMon(string info)
        {
            try
            {
                DirectoryInfo dirinfo = new DirectoryInfo(LOGPATH);
                if (!dirinfo.Exists)
                    dirinfo.Create();
                string datetime = DateTime.Now.ToString("yyyyMMdd_HH");
                string fname = LOGPATH + "\\监控日志" + datetime + ".txt";
                FileInfo fileinfo = new FileInfo(fname);
                //if (fileinfo.Exists)
                //    fileinfo.Delete();
                FileStream filestream = new FileStream(fname, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite); //fileinfo.Open(FileMode.Open,FileAccess.Write,FileShare.ReadWrite);
                StreamWriter streamwriter = new StreamWriter(filestream, Encoding.Default);
                streamwriter.BaseStream.Seek(0, SeekOrigin.End);
                streamwriter.WriteLine(DateTime.Now.ToString("mm:ss.fff") + "  " + info);
                streamwriter.Flush();
                streamwriter.Dispose();
                streamwriter.Close();
                filestream.Dispose();
                filestream.Close();
                return fname;
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        delegate void DgtSetCtrlPro(string[] control);
        /// <summary>
        /// Set Control Property
        /// Parameters(Array[4]): 
        /// Control Type; 
        /// Control ID; 
        /// PropertyName; 
        /// PropertyValue[Bool:True; Color:ColorName]
        /// </summary>
        /// <param name="control"></param>
        /// SetControlPropertyDlgt(new string[] { "Label", "label5", "Text", str });
        public void SetControlPropertyDlgt(string[] control)
        {
            try
            {
                string ctrltype = control[0].ToUpper().Trim();
                string ctrlid = control[1].ToUpper().Trim();
                string ptyname = control[2].ToUpper().Trim();
                string ptyvalue = control[3];
                bool ptyboolvalue = false;
                if (ptyvalue.ToUpper() == "Y" || ptyvalue.ToUpper() == "YES" || ptyvalue.ToUpper() == "1" || ptyvalue.ToUpper() == "T" || ptyvalue.ToUpper() == "TRUE")
                    ptyboolvalue = true;
                Control[] vctrl = null;
                vctrl = this.Controls.Find(ctrlid, true);
                if (vctrl.Length == 0)
                    return;

                if (vctrl[0].InvokeRequired)
                {
                    DgtSetCtrlPro d = new DgtSetCtrlPro(SetControlPropertyDlgt);
                    this.Invoke(d, new object[] { control });
                }
                else
                {
                    switch (ptyname)
                    {
                        case "ENABLED":
                            {
                                vctrl[0].Enabled = ptyboolvalue;
                                break;
                            }
                        case "VISIBLE":
                            {
                                vctrl[0].Visible = ptyboolvalue;
                                break;
                            }
                        default:
                            {
                                #region Panel
                                if (ctrltype == "PANEL")
                                {
                                    switch (ptyname)
                                    {

                                        case "LOCATION":
                                            {
                                                break;
                                            }
                                        default:
                                            {
                                                break;
                                            }
                                    }
                                }
                                #endregion

                                #region TextBox
                                if (ctrltype == "TEXTBOX" || ctrltype == "LABEL")
                                {
                                    switch (ptyname)
                                    {

                                        case "TEXT":
                                            {
                                                vctrl[0].Text = ptyvalue;
                                                break;
                                            }
                                        case "BACKCOLOR":
                                            {
                                                vctrl[0].BackColor = Color.FromName(ptyvalue);
                                                break;
                                            }
                                        default:
                                            {
                                                break;
                                            }
                                    }
                                }
                                #endregion

                                if (ctrltype == "DATETIMEPICKER")
                                {
                                    DateTimePicker dp = (DateTimePicker)vctrl[0];
                                    switch (ptyname)
                                    {
                                        case "VALUE":
                                            {
                                                dp.Value = DateTime.Parse(ptyvalue);
                                                break;
                                            }
                                        default:
                                            {
                                                break;
                                            }
                                    }
                                }


                                #region richTextBox
                                if (ctrltype == "RICHTEXTBOX")
                                {
                                    RichTextBox rtb1 = (RichTextBox)vctrl[0];
                                    switch (ptyname)
                                    {
                                        case "TEXT":
                                            {
                                                rtb1.Text = ptyvalue;
                                                break;
                                            }
                                        case "APPENDTEXT":
                                            {
                                                rtb1.AppendText(ptyvalue);
                                                break;
                                            }
                                        case "APPENDTEXT2":
                                            {
                                                rtb1.Text = rtb1.Text.Substring(0, rtb1.Text.LastIndexOf("\n") + 1) + ptyvalue;
                                                break;
                                            }
                                        default:
                                            {
                                                break;
                                            }
                                    }
                                    rtb1.AppendText("");
                                    //rtb1.ScrollToCaret();
                                }
                                #endregion

                                #region ListBox
                                if (ctrltype == "LISTBOX")
                                {
                                    switch (ptyname)
                                    {
                                        case "ITEMSADD":
                                            {
                                                ListBox lb = (ListBox)vctrl[0];
                                                if (ptyvalue == "")
                                                    lb.Items.Add(ptyvalue);
                                                else
                                                    lb.Items.Add(DateTime.Now.ToString("HH:mm:ss  ") + ptyvalue);
                                                //else
                                                //    lb.Items.Add(DateTime.Now.AddSeconds(TSDT.TotalSeconds).ToString("HH:mm:ss  ") + );
                                                if (lb.Items.Count > 46)
                                                    lb.Items.RemoveAt(0);
                                                lb.SelectedIndex = lb.Items.Count - 1;
                                                break;
                                            }
                                        case "ITEMSCLEAR":
                                            {
                                                ((ListBox)vctrl[0]).Items.Clear();
                                                break;
                                            }
                                        case "SELECTEDINDEX":
                                            {
                                                ((ListBox)vctrl[0]).SelectedIndex = int.Parse(ptyvalue);
                                                break;
                                            }
                                        default:
                                            {
                                                break;
                                            }
                                    }
                                }
                                #endregion

                                #region ListView
                                if (ctrltype == "LISTVIEW")
                                {
                                    ListView lv0 = (ListView)vctrl[0];
                                    switch (ptyname)
                                    {
                                        case "ITEMSADD":
                                            {
                                                lv0.Items.Add(new ListViewItem(ptyvalue.Split('`')));
                                                break;
                                            }
                                        case "ITEMSCLEAR":
                                            {
                                                lv0.Items.Clear();
                                                break;
                                            }
                                        case "SUBITEMUPDATE":
                                            {
                                                int idx = int.Parse(ptyvalue.Substring(0, ptyvalue.IndexOf("`")));
                                                string ve = ptyvalue.Substring(ptyvalue.IndexOf("`") + 1);
                                                //lv0.Items[idx].Cells[6].Value.ToString() = ve;
                                                break;
                                            }
                                        default:
                                            {
                                                break;
                                            }
                                    }
                                }
                                #endregion

                                #region SkinDataGridView
                                if (ctrltype == "SKINDATAGRIDVIEW")
                                {
                                    CCWin.SkinControl.SkinDataGridView lv0 = (CCWin.SkinControl.SkinDataGridView)vctrl[0];
                                    switch (ptyname)
                                    {
                                        case "ROWSADD":
                                            {
                                                string[] vs = ptyvalue.Split('`');
                                                lv0.Rows.Add(vs);
                                                if (vs[1].Contains("失败"))
                                                    lv0.Rows[lv0.Rows.Count - 1].DefaultCellStyle = new DataGridViewCellStyle() { BackColor = Color.LightSalmon };
                                                break;
                                            }
                                        case "ROWSCLEAR":
                                            {
                                                lv0.Rows.Clear();
                                                break;
                                            }
                                        case "SUBITEMUPDATE":
                                            {
                                                int idx = int.Parse(ptyvalue.Substring(0, ptyvalue.IndexOf("`")));
                                                string ve = ptyvalue.Substring(ptyvalue.IndexOf("`") + 1);
                                                lv0.Rows[idx].Cells["区域个数"].Value = ve;
                                                break;
                                            }
                                        case "SUBITEMUPDATE2":
                                            {
                                                string idx = ptyvalue.Substring(0, ptyvalue.IndexOf("`"));
                                                string ve = ptyvalue.Substring(ptyvalue.IndexOf("`") + 1);
                                                //for (int n = 0; n < sdgv1.Rows.Count; n++)
                                                //{
                                                //    if (sdgv1.Rows[n].Cells["商户ID"].Value.ToString() == idx)
                                                //    {
                                                //        sdgv1.Rows[n].Cells["营业状态"].Value = ve;
                                                //        break;
                                                //    }
                                                //}
                                                break;
                                            }
                                        case "SUBITEMUPDATE3":
                                            {
                                                string[] arr = ptyvalue.Split(new string[] { "`" }, StringSplitOptions.None);
                                                string idx = arr[0];
                                                string ve = arr[1];
                                                string ve2 = arr[2];
                                                //for (int n = 0; n < sdgv1.Rows.Count; n++)
                                                //{
                                                //    if (sdgv1.Rows[n].Cells["商户ID"].Value.ToString() == idx)
                                                //    {
                                                //        sdgv1.Rows[n].Cells["店铺状态"].Value = ve;
                                                //        sdgv1.Rows[n].Cells["营业状态"].Value = ve2;
                                                //        break;
                                                //    }
                                                //}
                                                break;
                                            }
                                        default:
                                            {
                                                break;
                                            }
                                    }
                                }
                                #endregion


                                #region ComboBox
                                if (ctrltype == "COMBOBOX")
                                {
                                    switch (ptyname)
                                    {
                                        case "ITEMSADD":
                                            {
                                                ((ComboBox)vctrl[0]).Items.Add(ptyvalue);
                                                break;
                                            }
                                        case "ITEMSCLEAR":
                                            {
                                                ((ComboBox)vctrl[0]).Items.Clear();
                                                break;
                                            }
                                        default:
                                            {
                                                break;
                                            }
                                    }
                                }
                                #endregion

                                #region ProgressBar
                                if (ctrltype == "PROGRESSBAR")
                                {
                                    switch (ptyname)
                                    {

                                        case "VALUE":
                                            {
                                                ((ProgressBar)vctrl[0]).Value = Convert.ToInt32(ptyvalue);
                                                break;
                                            }
                                        case "MAXIMUM":
                                            {
                                                ((ProgressBar)vctrl[0]).Maximum = Convert.ToInt32(ptyvalue);
                                                break;
                                            }
                                        case "MINIMUM":
                                            {
                                                ((ProgressBar)vctrl[0]).Minimum = Convert.ToInt32(ptyvalue);
                                                break;
                                            }
                                        default:
                                            {
                                                break;
                                            }
                                    }
                                }
                                #endregion

                                #region CheckBox
                                if (ctrltype == "CHECKBOX")
                                {
                                    switch (ptyname)
                                    {
                                        case "CHECKED":
                                            {
                                                ((CheckBox)vctrl[0]).Checked = ptyboolvalue;
                                                break;
                                            }
                                        default:
                                            {
                                                break;
                                            }
                                    }
                                }
                                #endregion

                                #region RadioButton
                                if (ctrltype == "RADIOBUTTON")
                                {
                                    switch (ptyname)
                                    {
                                        case "CHECKED":
                                            {
                                                ((RadioButton)vctrl[0]).Checked = ptyboolvalue;
                                                break;
                                            }
                                        default:
                                            {
                                                break;
                                            }
                                    }
                                }
                                #endregion

                                #region RadioButton
                                if (ctrltype == "WEBBROWSER")
                                {
                                    switch (ptyname)
                                    {
                                        case "NAVIGATE":
                                            {
                                                ((WebBrowser)vctrl[0]).Navigate(ptyvalue);
                                                while (((WebBrowser)vctrl[0]).ReadyState != WebBrowserReadyState.Complete)
                                                    Application.DoEvents();
                                                break;
                                            }
                                        default:
                                            {
                                                break;
                                            }
                                    }
                                }
                                #endregion

                                break;
                            }
                    }
                }
            }
            catch (Exception ex)
            {
                //throw ex;
            }
        }

        //string data = ReadFile(pathlog + "\\" + File_XGZL).Trim();
        //string[] arr = data.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
        //List<string> datas = new List<string>();
        //for (int i = 0; i < arr.Length; i++)
        //{
        //    datas.Add(arr[i]);
        //}

        //List<RowInfo> rows = new List<RowInfo>();
        //for (int i = 0; i < accarr.Length; i++)
        //{
        //    string acc = "[" + accarr[i] + "]";
        //    int sx = datas.IndexOf(acc);
        //    if (sx >= 0)
        //    {
        //        RowInfo ri = new RowInfo();
        //        for (int k = 1; k <= 8; k++)
        //        {
        //            if (datas.Count < sx + k)
        //                continue;
        //            string line = datas[sx + k];
        //            string key = line.Substring(0, line.IndexOf("=")).Trim();
        //            string value = line.Substring(line.IndexOf("=") + 1).Trim();
        //            if (key == "帐号")
        //                ri.acc = value;
        //            else if (key == "密码")
        //                ri.pwd = value;
        //            else if (key == "生日")
        //                ri.bir = value;
        //            else if (key == "问题1")
        //                ri.qu1 = value;
        //            else if (key == "答案1")
        //                ri.an1 = value;
        //            else if (key == "问题2")
        //                ri.qu2 = value;
        //            else if (key == "答案2")
        //                ri.an2 = value;
        //            else if (key == "二级密码")
        //                ri.pwd2 = value;
        //        }
        //        rows.Add(ri);
        //    }
        //}

    }

    public class Options
    {
        public bool Auto = true;
        public int MonSecs = 5;
        public int SearchSecs = 20;
        //帐号担保路径
        public string SerSubPath = "";
        /// <summary>
        /// 数据引擎目录
        /// </summary>
        public string SerKey = "";
        /// <summary>
        /// 修改信息
        /// </summary>
        public string File_XG = "";
        /// <summary>
        /// 账号管理软件路径，如果为空默认搜索根目录下：远程账号管理工具.exe
        /// </summary>
        public string ToolPath = "";

        public List<string> Folders = new List<string>();
        public void SaveOptions()
        {
            string path = Application.StartupPath + "\\" + OptionsFileName;
            try
            {
                StreamWriter writer = new StreamWriter(path);
                new XmlSerializer(typeof(Options)).Serialize((TextWriter)writer, this);
                writer.Close();
            }
            catch (Exception ex)
            {

            }
        }

        public static Options GetOptions()
        {
            Options options;
            string path = Application.StartupPath + "\\" + OptionsFileName;//=Path.GetDirectoryName( System.Windows.Forms.Application.UserAppDataPath
            DirectoryInfo dirinfo = new DirectoryInfo(path.Substring(0, path.LastIndexOf("\\")));
            if (!dirinfo.Exists)
                dirinfo.Create();
            if (!File.Exists(path))
            {
                return new Options();
            }
            try
            {
                StreamReader textReader = new StreamReader(path);
                XmlSerializer serializer = new XmlSerializer(typeof(Options));
                options = (Options)serializer.Deserialize(textReader);
                textReader.Close();
            }
            catch (Exception ex)
            {
                options = new Options();
            }
            return options;
        }

        public static string OptionsFileName
        {
            get
            {
                return "Cfg.xml";
            }
        }
    }

    public class LoginInfo
    {
        public string Path = "";
        public string IP = "";
        public string Port = "";
        public string Pwd = "";

        public LoginInfo()
        {

        }

        public LoginInfo(string vpath, string vip, string vport, string vpwd)
        {
            Path = vpath;
            IP = vip;
            Port = vport;
            Pwd = vpwd;
        }

    }

    public class WinHandleMan
    {
        public HandleLogin Login = new HandleLogin();
        public IntPtr Main = IntPtr.Zero;
        public IntPtr IN_Acct = IntPtr.Zero;
        public IntPtr BTN_Ser = IntPtr.Zero;
        public IntPtr BTN_Mdy = IntPtr.Zero;
        public IntPtr BTN_Add = IntPtr.Zero;
        public IntPtr BTN_Del = IntPtr.Zero;
        /// <summary>
        /// 两步验证
        /// </summary>
        public IntPtr E_Acct = IntPtr.Zero;
        public IntPtr E_Pwd = IntPtr.Zero;
        public IntPtr E_Bir = IntPtr.Zero;
        public IntPtr E_Name = IntPtr.Zero;
        public IntPtr E_Card = IntPtr.Zero;

        public IntPtr E_Que1 = IntPtr.Zero;
        public IntPtr E_Que2 = IntPtr.Zero;
        public IntPtr E_Ans1 = IntPtr.Zero;
        public IntPtr E_Ans2 = IntPtr.Zero;

        public IntPtr E_Mail = IntPtr.Zero;
        public IntPtr E_Phone = IntPtr.Zero;

        public class HandleLogin
        {

            public IntPtr LMain = IntPtr.Zero;
            public IntPtr LBTN_Login = IntPtr.Zero;
            public IntPtr LBTN_Close = IntPtr.Zero;

            public IntPtr LE_IP = IntPtr.Zero;
            public IntPtr LE_Port = IntPtr.Zero;
            public IntPtr LE_Pwd = IntPtr.Zero;
        }

    }

    public class WinHandle
    {
        public IntPtr Main = IntPtr.Zero;
        public IntPtr IN_Acct = IntPtr.Zero;
        public IntPtr BTN_Search = IntPtr.Zero;
        public IntPtr BTN_OK = IntPtr.Zero;
        /// <summary>
        /// 两步验证
        /// </summary>
        public IntPtr E_Acct = IntPtr.Zero;
        public IntPtr E_Two = IntPtr.Zero;
        public IntPtr E_Que1 = IntPtr.Zero;
        public IntPtr E_Que2 = IntPtr.Zero;
        public IntPtr E_Ans1 = IntPtr.Zero;
        public IntPtr E_Ans2 = IntPtr.Zero;
        public IntPtr E_Bir = IntPtr.Zero;
        public IntPtr E_Pwd = IntPtr.Zero;

    }

    public class RowInfo
    {
        public string acc = "";
        public string pwd = "";
        public string bir = "";
        public string qu1 = "";
        public string an1 = "";
        public string qu2 = "";
        public string an2 = "";
        public string pwd2 = "";

        public RowInfo()
        {

        }
        public RowInfo(string vacc, string vpwd, string vbir, string vqu1, string van1, string vqu2, string van2, string vpwd2)
        {
            acc = vacc;
            pwd = vpwd;
            bir = vqu1;
            qu1 = vqu1;
            an1 = van1;
            qu2 = vqu2;
            an2 = van2;
            pwd2 = vpwd2;
        }

        public string ToOutStr()
        {
            string[] arr = new string[9];
            arr[0] = "[" + acc + "]";
            arr[1] = "帐号=" + acc;
            arr[2] = "密码=" + pwd;
            arr[3] = "生日=" + bir;
            arr[4] = "问题1=" + qu1;
            arr[5] = "答案1=" + an1;
            arr[6] = "问题2=" + qu2;
            arr[7] = "答案2=" + an2;
            arr[8] = "二级密码=" + pwd2;
            return String.Join("\r\n", arr);
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
        //帐号:密码 生日 问题1 答案1 问题2 答案2   用冒号隔开的
        public string acct = "";
        public string pwd = "";
        public string bir = "";
        public string que1 = "";
        public string ans1 = "";
        public string que2 = "";
        public string ans2 = "";
        public string role = "";

        public MdInfo()
        {

        }
        public MdInfo(string vacct, string vpwd, string vbir, string vque1, string vans1, string vque2, string vans2)
        {
            acct = vacct;
            pwd = vpwd;
            bir = vbir;
            que1 = vque1;
            ans1 = vans1;
            que2 = vque2;
            ans2 = vans2;
        }
    }

    public class SelInfo
    {
        public string acct = "";
        public string role = "";
        public SelInfo(string vacct, string vrole)
        {
            acct = vacct;
            role = vrole;
        }
    }
    public class Sys
    {
        public static string DateFormatStr = "yyyy-MM-dd HH:mm:ss";
        public static string DateFormatStrF = "yyyy-MM-dd HH:mm:ss.fff";

        public class UseOptions
        {
            public List<string> UPS = new List<string>();
            /// <summary>
            /// Times
            /// </summary>
            public string PUYSDTWQ = "";
            /// <summary>
            /// reg time
            /// </summary>
            public string KDMSINDS = "";
            public int RTEMW = 1;

            public void SaveOptions()
            {
                try
                {
                    StreamWriter writer = new StreamWriter(UseOptionsFileName);
                    new XmlSerializer(typeof(UseOptions)).Serialize((TextWriter)writer, this);
                    writer.Close();
                }
                catch (Exception ex)
                {
                }
            }

            public static UseOptions GetOptions()
            {
                UseOptions options;
                DirectoryInfo dirinfo = new DirectoryInfo(UseOptionsFileName.Substring(0, UseOptionsFileName.LastIndexOf("\\")));
                if (!dirinfo.Exists)
                    dirinfo.Create();
                if (!File.Exists(UseOptionsFileName))
                {
                    return new UseOptions();
                }
                try
                {
                    StreamReader textReader = new StreamReader(UseOptionsFileName);
                    XmlSerializer serializer = new XmlSerializer(typeof(UseOptions));
                    options = (UseOptions)serializer.Deserialize(textReader);
                    textReader.Close();
                }
                catch (Exception ex)
                {
                    options = new UseOptions();
                }
                return options;
            }

            public static string UseOptionsFileName
            {
                get
                {
                    return Environment.GetFolderPath(Environment.SpecialFolder.Favorites) + "\\" + Sys.SoftRegister.OPTIONNAME;
                }
            }
        }

        public static class EncodeHelper
        {
            private static string DEFAULTKEY = "a0e1b83f5k2p9y7m";

            public static string ImageToBase64String(string imageFileFullName)
            {
                string strbaser64 = "";
                try
                {
                    Bitmap bmp = new Bitmap(imageFileFullName);
                    MemoryStream ms = new MemoryStream();
                    bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    byte[] arr = new byte[ms.Length];
                    ms.Position = 0;
                    ms.Read(arr, 0, (int)ms.Length);
                    ms.Close();
                    strbaser64 = Convert.ToBase64String(arr);
                }
                catch (Exception ex)
                {
                    return strbaser64;
                    //MessageBox.Show("ImgToBase64String 转换失败\nException:" + ex.Message);
                }
                return strbaser64;
            }

            /// <summary>
            /// AES 加密
            /// </summary>
            /// <param name="str"></param>
            /// <returns></returns>
            public static string AesEncrypt(string str)
            {
                return AesEncrypt(str, DEFAULTKEY);
            }

            /// <summary>
            ///  AES 加密
            /// </summary>
            /// <param name="str"></param>
            /// <param name="key"></param>
            /// <returns></returns>
            public static string AesEncrypt(string str, string key)
            {
                if (string.IsNullOrEmpty(str))
                    return null;
                Byte[] toEncryptArray = Encoding.UTF8.GetBytes(str);
                System.Security.Cryptography.RijndaelManaged rm = new System.Security.Cryptography.RijndaelManaged
                {
                    Key = Encoding.UTF8.GetBytes(key),
                    Mode = System.Security.Cryptography.CipherMode.ECB,
                    Padding = System.Security.Cryptography.PaddingMode.PKCS7
                };
                System.Security.Cryptography.ICryptoTransform cTransform = rm.CreateEncryptor();
                Byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
                return Convert.ToBase64String(resultArray, 0, resultArray.Length);
            }

            /// <summary>
            /// AES 解密
            /// </summary>
            /// <param name="str"></param>
            /// <returns></returns>
            public static string AesDecrypt(string str)
            {
                return AesDecrypt(str, DEFAULTKEY);
            }

            /// <summary>
            ///  AES 解密
            /// </summary>
            /// <param name="str"></param>
            /// <param name="key"></param>
            /// <returns></returns>
            public static string AesDecrypt(string str, string key)
            {
                if (string.IsNullOrEmpty(str))
                    return null;
                Byte[] toEncryptArray = Convert.FromBase64String(str);
                System.Security.Cryptography.RijndaelManaged rm = new System.Security.Cryptography.RijndaelManaged
                {
                    Key = Encoding.UTF8.GetBytes(key),
                    Mode = System.Security.Cryptography.CipherMode.ECB,
                    Padding = System.Security.Cryptography.PaddingMode.PKCS7
                };
                System.Security.Cryptography.ICryptoTransform cTransform = rm.CreateDecryptor();
                Byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
                return Encoding.UTF8.GetString(resultArray);
            }

            /// <summary>
            ///  DES加密 同JAVA
            /// </summary>
            /// <param name="pToEncrypt">加密的字符串</param>
            /// <returns>加密后的字符串</returns>
            public static string DESEnCode(string pToEncrypt)
            {
                return DESEnCode(pToEncrypt, DEFAULTKEY);
            }

            #region DESEnCode DES加密
            /// <summary>
            /// DES加密 同JAVA
            /// </summary>
            /// <param name="pToEncrypt">加密的字符串</param>
            /// <param name="sKey">key</param>
            /// <returns>加密后的字符串</returns>
            public static string DESEnCode(string pToEncrypt, string sKey)
            {
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                byte[] inputByteArray = Encoding.GetEncoding("UTF-8").GetBytes(pToEncrypt);
                //建立加密对象的密钥和偏移量
                //原文使用ASCIIEncoding.ASCII方法的GetBytes方法
                //使得输入密码必须输入英文文本
                des.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
                des.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                StringBuilder ret = new StringBuilder();
                foreach (byte b in ms.ToArray())
                {
                    ret.AppendFormat("{0:X2}", b);
                }
                ret.ToString();
                return ret.ToString();
            }
            #endregion

            /// <summary>
            ///  DES解密 同JAVA
            /// </summary>
            /// <param name="pToDecrypt">解密字符串</param>
            /// <returns>解密后的字符串</returns>
            public static string DESDeCode(string pToDecrypt)
            {
                return DESDeCode(pToDecrypt, DEFAULTKEY);
            }

            #region DESDeCode DES解密
            /// <summary>
            /// DES解密 同JAVA
            /// </summary>
            /// <param name="pToDecrypt">解密字符串</param>
            /// <param name="sKey">key</param>
            /// <returns>解密后的字符串</returns>
            public static string DESDeCode(string pToDecrypt, string sKey)
            {
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                byte[] inputByteArray = new byte[pToDecrypt.Length / 2];
                for (int x = 0; x < pToDecrypt.Length / 2; x++)
                {
                    int i = (Convert.ToInt32(pToDecrypt.Substring(x * 2, 2), 16));
                    inputByteArray[x] = (byte)i;
                }
                des.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
                des.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                StringBuilder ret = new StringBuilder();
                // return HttpContext.Current.Server.UrlDecode(System.Text.Encoding.Default.Getstring(ms.ToArray()));
                return System.Text.Encoding.Default.GetString(ms.ToArray());
            }
            #endregion
        }

        public class Log
        {
            //private string filepath = ConfigurationSettings.AppSettings["DataFilePath"].ToString().Trim();
            public static string filepath = Application.StartupPath + "\\日志";//Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            public static string name1 = "1.倒计时";
            public static string name2 = "2.开奖数据";
            public static string name3 = "3.所有开奖数据";
            public static string name4 = "最新开奖信息\\";

            public Log()
            {
                if (filepath.Substring(filepath.Length - 2) == "\\")
                    filepath = filepath.Substring(0, filepath.Length - 2);
            }

            public static void DelLogs()
            {
                DirectoryInfo dirinfo = new DirectoryInfo(filepath);
                if (!dirinfo.Exists)
                    return;
                string curdt = DateTime.Today.AddDays(-2).ToString("yyyyMMdd");
                int curin = int.Parse(curdt);
                foreach (FileInfo item in dirinfo.GetFiles())
                {
                    int fi = int.Parse(item.Name.Substring(4, 8));
                    if (fi < curin)
                        item.Delete();
                }
            }

            public static string WriteLog12(string info, string type)
            {
                try
                {
                    string name = name1;
                    if (type == "2")
                    {
                        name = name2;
                    }
                    string fname = Log.filepath + "\\" + name + ".txt";
                    DirectoryInfo dirinfo = new DirectoryInfo(filepath);
                    if (!dirinfo.Exists)
                        dirinfo.Create();
                    File.WriteAllText(fname, info, Encoding.Default);
                    return fname;
                }
                catch (Exception exp)
                {
                    throw exp;
                }
            }

            public static string WriteLog12_(string info, string type)
            {
                try
                {
                    string name = name1;
                    if (type == "2")
                    {
                        name = name2;
                    }
                    string fname = Log.filepath + "\\" + name + ".txt";
                    FileInfo fileinfo = new FileInfo(fname);
                    DirectoryInfo dirinfo = new DirectoryInfo(filepath);
                    if (!dirinfo.Exists)
                        dirinfo.Create();
                    if (fileinfo.Exists)
                        fileinfo.Delete();
                    FileStream filestream = new FileStream(fname, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite); //fileinfo.Open(FileMode.Open,FileAccess.Write,FileShare.ReadWrite);
                    StreamWriter streamwriter = new StreamWriter(filestream, Encoding.Default);
                    streamwriter.BaseStream.Seek(0, SeekOrigin.End);
                    streamwriter.WriteLine(info);
                    streamwriter.Flush();
                    streamwriter.Dispose();
                    streamwriter.Close();
                    filestream.Dispose();
                    filestream.Close();
                    return fname;
                }
                catch (Exception exp)
                {
                    throw exp;
                }
            }


            public static string WriteLog4_(string qihao, string info)
            {
                try
                {
                    string fname = Log.filepath + "\\" + name4 + qihao + ".txt";
                    DirectoryInfo dirinfo = new DirectoryInfo(filepath + "\\" + name4);
                    if (!dirinfo.Exists)
                        dirinfo.Create();
                    if (new FileInfo(fname).Exists)
                        return "";
                    File.WriteAllText(fname, info, Encoding.Default);
                    return fname;
                }
                catch (Exception exp)
                {
                    throw exp;
                }
            }

            public static string WriteLog4(string qihao, string info)
            {
                try
                {
                    string fname = Log.filepath + "\\" + name4 + qihao + ".txt";
                    DirectoryInfo dirinfo = new DirectoryInfo(filepath + "\\" + name4);
                    if (!dirinfo.Exists)
                        dirinfo.Create();
                    FileInfo fileinfo = new FileInfo(fname);
                    if (fileinfo.Exists)
                        return "";
                    FileStream filestream = new FileStream(fname, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite); //fileinfo.Open(FileMode.Open,FileAccess.Write,FileShare.ReadWrite);
                    StreamWriter streamwriter = new StreamWriter(filestream, Encoding.Default);
                    streamwriter.BaseStream.Seek(0, SeekOrigin.End);
                    streamwriter.WriteLine(info);
                    streamwriter.Flush();
                    streamwriter.Dispose();
                    streamwriter.Close();
                    filestream.Dispose();
                    filestream.Close();
                    return fname;
                }
                catch (Exception exp)
                {
                    throw exp;
                }
            }

            public static string WriteLog(string errorinfo, Exception ex)
            {
                string exinfo = "\r\n" + string.Format("{0,15}", "Message: ") + ex.Message + "\r\n" + string.Format("{0,15}", "Source: ") + ex.Source + "\r\n" + string.Format("{0,15}", "TargetSite: ") + ex.TargetSite + "\r\n\r\n" + string.Format("{0,15}", "StackTrace: ") + "\r\n" + ex.StackTrace;
                return WriteLog(errorinfo + exinfo);
            }

            public static string WriteLog(string errorinfo)
            {
                try
                {
                    DirectoryInfo dirinfo = new DirectoryInfo(filepath);
                    if (!dirinfo.Exists)
                        dirinfo.Create();
                    string datetime = DateTime.Now.ToString("yyyyMMdd_HHmmss.fff");
                    string fname = Log.filepath + "\\错误日志" + datetime + ".txt";
                    FileInfo fileinfo = new FileInfo(fname);
                    //if (fileinfo.Exists)
                    //    fileinfo.Delete();

                    FileStream filestream = new FileStream(fname, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite); //fileinfo.Open(FileMode.Open,FileAccess.Write,FileShare.ReadWrite);
                    StreamWriter streamwriter = new StreamWriter(filestream);
                    streamwriter.BaseStream.Seek(0, SeekOrigin.End);
                    streamwriter.WriteLine(errorinfo);
                    streamwriter.Flush();
                    streamwriter.Dispose();
                    streamwriter.Close();
                    filestream.Dispose();
                    filestream.Close();
                    return fname;
                }
                catch (Exception exp)
                {
                    throw exp;
                    //string v = "";
                    //v = DateTime.Today.ToFileTime().ToString();//129584160000000000
                    //v = DateTime.Today.ToShortTimeString();//0:00
                    //v = DateTime.Today.ToLongTimeString();//0:00:00
                    //v = DateTime.Today.ToShortDateString();//2011/8/22
                    //v = DateTime.Today.ToLongDateString();//2011年8月22日
                    //v = DateTime.Today.ToUniversalTime().ToLongDateString();//2011年8月21日
                    //v = DateTime.Now.ToFileTime().ToString();//129584547017062874
                    //v = DateTime.Now.ToShortTimeString();//10:45
                    //v = DateTime.Now.ToLongTimeString();//10:45:19
                    //v = DateTime.Now.ToShortDateString();//2011/8/22
                    //v = DateTime.Now.ToLongDateString();//2011年8月22日
                    //v = DateTime.Now.ToUniversalTime().ToLongTimeString();//2:46:00

                }
            }

            public void Record(string className, Exception ex)
            {
                try
                {
                    FileStream fs = null;
                    if (!File.Exists("error.txt"))
                    {
                        fs = new FileStream("error.txt", FileMode.Create);
                    }
                    else
                    {
                        fs = new FileStream("error.txt", FileMode.Append);
                    }
                    StackTrace st = new StackTrace(true);
                    string transferMethod = st.GetFrame(1).GetMethod().Name.ToString();

                    StreamWriter sw = new StreamWriter(fs);
                    sw.WriteLine("—————————————————————————————–");
                    sw.WriteLine("日    期：" + DateTime.Now.ToString("G"));
                    sw.WriteLine("类    名：" + className);
                    sw.WriteLine("调用错误记录的方法：" + transferMethod);
                    sw.WriteLine("引发错误的方法：" + ex.TargetSite.ToString());
                    sw.WriteLine("错误消息：" + ex.Message.ToString());
                    sw.Close();
                }
                catch (IOException ioe)
                {
                    //MessageBox.Show("错误未被记录！原因：" + ioe.Message.ToString());
                    string ss = ioe.Message;
                }
            }
        }

        /// <summary>
        /// 解析JSON，仿Javascript风格
        /// </summary>
        //public static class JSON
        //{
        //    public static T parse<T>(string jsonString)
        //    {
        //        using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString)))
        //        {
        //            return (T)new DataContractJsonSerializer(typeof(T)).ReadObject(ms);
        //        }
        //    }

        //    public static string stringify(object jsonObject)
        //    {
        //        using (var ms = new MemoryStream())
        //        {
        //            new DataContractJsonSerializer(jsonObject.GetType()).WriteObject(ms, jsonObject);
        //            return Encoding.UTF8.GetString(ms.ToArray());
        //        }
        //    }
        //}

        public static class SystemInfo
        {
            [DllImport("wininet")]
            private extern static bool InternetGetConnectedState(out int connectionDescription, int reservedValue);
            /// <summary>
            /// 检测本机是否联网
            /// </summary>
            /// <returns></returns>
            public static bool IsConnectedInternet()
            {
                int i = 0;
                if (InternetGetConnectedState(out i, 0))
                {
                    //已联网
                    return true;
                }
                else
                {
                    //未联网
                    return false;
                }

            }

            public static bool IsLocalAreaConnected = NetworkInterface.GetIsNetworkAvailable();
            public static bool IsConn()
            {
                System.Net.NetworkInformation.Ping ping;
                System.Net.NetworkInformation.PingReply res;
                ping = new System.Net.NetworkInformation.Ping();
                try
                {
                    res = ping.Send("www.baidu.com");
                    if (res.Status != System.Net.NetworkInformation.IPStatus.Success)
                    {
                        res = ping.Send("www.baidu.com");
                        if (res.Status != System.Net.NetworkInformation.IPStatus.Success)
                        {
                            return false;
                        }
                        else
                            return true;
                    }
                    else
                        return true;
                }
                catch (Exception er)
                {
                    return false;
                }
            }

            /// <summary> 
            /// 获取标准北京时间，读取http://www.beijing-time.org/time.asp 
            /// </summary> 
            /// <returns>返回网络时间</returns> 
            public static DateTime GetBeijingTime()
            {
                DateTime dt;
                WebRequest wrt = null;
                WebResponse wrp = null;
                try
                {
                    wrt = WebRequest.Create("http://www.beijing-time.org/time15.asp");//http://www.beijing-time.org/time.asp
                    wrp = wrt.GetResponse();
                    string html = string.Empty;
                    using (Stream stream = wrp.GetResponseStream())
                    {
                        using (StreamReader sr = new StreamReader(stream, Encoding.UTF8))
                        {
                            html = sr.ReadToEnd();
                        }
                    }
                    //t0=new Date().getTime(); nyear=2017; nmonth=6; nday=8; nwday=4; nhrs=10; nmin=34; nsec=32;
                    string[] tempArray = html.Split(';');
                    for (int i = 0; i < tempArray.Length; i++)
                    {
                        tempArray[i] = tempArray[i].Replace("\r\n", "");
                    }
                    string year = tempArray[1].Split('=')[1];
                    string month = tempArray[2].Split('=')[1];
                    string day = tempArray[3].Split('=')[1];
                    string hour = tempArray[5].Split('=')[1];
                    string minite = tempArray[6].Split('=')[1];
                    string second = tempArray[7].Split('=')[1];
                    dt = DateTime.Parse(year + "-" + month + "-" + day + " " + hour + ":" + minite + ":" + second);
                }
                catch (WebException)
                {
                    return DateTime.Parse("2011-1-1");
                }
                catch (Exception)
                {
                    return DateTime.Parse("2011-1-1");
                }
                finally
                {
                    if (wrp != null)
                        wrp.Close();
                    if (wrt != null)
                        wrt.Abort();
                }
                return dt;
            }

            public static DateTime GetNowTime()
            {
                WebRequest wrt = null;
                WebResponse wrp = null;
                wrt = WebRequest.Create("http://www.hko.gov.hk/cgi-bin/gts/time5a.pr?a=2");
                wrt.Method = "GET";
                wrp = wrt.GetResponse();
                string html = string.Empty;
                using (Stream stream = wrp.GetResponseStream())
                {
                    using (StreamReader sr = new StreamReader(stream, Encoding.UTF8))
                    {
                        html = sr.ReadToEnd();
                    }
                }
                System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"0=(?<timestamp>\d{10})\d+");
                System.Text.RegularExpressions.Match match = regex.Match(html);
                if (match.Success)
                {
                    return GetTime(match.Groups["timestamp"].Value);
                }
                return DateTime.Now;
            }

            public static DateTime GetNetTime()
            {
                DateTime dt = Sys.SystemInfo.GetNowTime();
                string nettime = dt.ToString(Sys.DateFormatStr);
                if (nettime == "" || (nettime.Length > 4 && int.Parse(nettime.Substring(0, 4)) < 2017))
                {
                    dt = Sys.SystemInfo.GetBeijingTime();
                    nettime = dt.ToString(Sys.DateFormatStr);
                    if (nettime == "" || (nettime.Length > 4 && int.Parse(nettime.Substring(0, 4)) < 2017))
                    {
                        dt = DateTime.Now;
                    }
                }
                return dt;
            }

            /// <summary>
            /// 时间戳转为C#格式时间
            /// </summary>
            /// <param name=”timeStamp”></param>
            /// <returns></returns>
            public static DateTime GetTime(string timeStamp)
            {
                DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
                long lTime = long.Parse(timeStamp + "0000000");
                TimeSpan toNow = new TimeSpan(lTime);
                return dtStart.Add(toNow);
            }

            /// <summary>
            /// 时间戳转为C#格式时间
            /// </summary>
            /// <param name=”timeStamp”></param>
            /// <returns></returns>
            public static DateTime GetTime2(string timeStamp)
            {
                DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
                long lTime = long.Parse(timeStamp);
                DateTime dt = dtStart.AddMilliseconds(lTime);
                return dt;
            }

            /// <summary>
            /// 时间戳转为C#格式时间戳1497228134645
            /// </summary>
            /// <param name=”timeStamp”></param>
            /// <returns></returns>
            public static string GetTimeStamp()
            {
                DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
                return ((DateTime.Now - startTime).TotalMilliseconds).ToString("F0");
            }


            /// <summary>
            /// 更新系统时间
            /// </summary>
            public static class UpdateSystemTime
            {
                //设置系统时间的API函数
                [DllImport("kernel32.dll")]
                private static extern bool SetLocalTime(ref SYSTEMTIME time);
                [StructLayout(LayoutKind.Sequential)]
                private struct SYSTEMTIME
                {
                    public short year;
                    public short month;
                    public short dayOfWeek;
                    public short day;
                    public short hour;
                    public short minute;
                    public short second;
                    public short milliseconds;
                }
                /// <summary>
                /// 设置系统时间
                /// </summary>
                /// <param name="dt">需要设置的时间</param>
                /// <returns>返回系统时间设置状态，true为成功，false为失败</returns>
                public static bool SetDate(DateTime dt)
                {
                    SYSTEMTIME st;
                    st.year = (short)dt.Year;
                    st.month = (short)dt.Month;
                    st.dayOfWeek = (short)dt.DayOfWeek;
                    st.day = (short)dt.Day;
                    st.hour = (short)dt.Hour;
                    st.minute = (short)dt.Minute;
                    st.second = (short)dt.Second;
                    st.milliseconds = (short)dt.Millisecond;
                    bool rt = SetLocalTime(ref st);
                    return rt;
                }
            }

            public static bool SetAutoRun(string keyName, string filePath)
            {
                try
                {
                    RegistryKey runKey = Registry.LocalMachine.OpenSubKey(@"\SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
                    if (runKey == null)
                        runKey = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run");
                    if (runKey.GetValue(keyName) == null)
                    {
                        runKey.SetValue(keyName, filePath);
                    }
                    runKey.Close();
                }
                catch
                {
                    return false;
                }
                return true;
            }
            public static bool DeleteAutoRun(string keyName, string filePath)
            {
                try
                {
                    RegistryKey runKey = Registry.LocalMachine.OpenSubKey(@"\SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
                    runKey.DeleteValue(keyName, false);
                    runKey.Close();
                }
                catch
                {
                    return false;
                }
                return true;
            }

            static string URL138 = "http://www.ip138.com/";
            static string URLKLY = "http://tool.keleyi.com/ip/visitoriphost/";  //"http://tool.keleyi.com/ip/wodeip.htm"
            static int TryMaxGetResponseTimes = 2;
            private static AdressInfo adress = null;
            public static AdressInfo Adress
            {
                get
                {
                    if (adress == null)
                    {
                        adress = GetAdress();
                    }
                    return adress;
                }
                set { adress = value; }
            }

            /// <summary>
            /// 柯乐义获取IP地址
            /// </summary>
            /// <returns></returns>
            public static string GetIPKLY()
            {
                string ip = "";
                try
                {
                    if (!Sys.SystemInfo.IsConn())
                    {
                        return ip;
                    }
                    string webcontent = "";
                    for (int k = 1; k <= TryMaxGetResponseTimes; k++)
                    {
                        webcontent = GetWebContent(URLKLY);
                        if (webcontent.Trim().Length > 0)
                        {
                            break;
                        }
                        else
                        {
                            //WriteLogFile(FileName_SystemLog, timestr + " The " + k.ToString() + " attempt failed.");
                        }
                    }
                    if (webcontent.Trim().Length == 0)
                    {
                        return ip;
                    }
                    //window.onload = function () {document.getElementById("keleyivisitorip").innerHTML="111.113.12.194"}
                    ip = webcontent.Substring(webcontent.IndexOf("innerHTML=") + 11).Replace("\"", "").Replace("}", "").Replace("\r\n", "");
                    return ip;
                }
                catch (Exception ex)
                {
                    return ip;
                }
            }
            public static AdressInfo GetAdress()
            {
                AdressInfo ai = new AdressInfo();
                try
                {
                    if (!Sys.SystemInfo.IsConn())
                    {
                        return ai;
                    }
                    string webcontent = "";
                    for (int k = 1; k <= TryMaxGetResponseTimes; k++)
                    {
                        webcontent = GetWebContent(URL138);
                        if (webcontent.Trim().Length > 0)
                        {
                            break;
                        }
                        else
                        {
                            //WriteLogFile(FileName_SystemLog, timestr + " The " + k.ToString() + " attempt failed.");
                        }
                    }
                    if (webcontent.Trim().Length == 0)
                    {
                        return ai;
                    }
                    string acturl = "";
                    string sc1 = "ip138.com/ic.asp";   //http://1212.ip138.com/ic.asp
                    int idx = webcontent.IndexOf(sc1);
                    if (idx > 0)
                    {
                        acturl = webcontent.Substring(0, idx + sc1.Length);
                        acturl = acturl.Substring(acturl.LastIndexOf("\"http") + 1);
                    }
                    if (acturl == "" || !acturl.ToLower().Contains(sc1))
                    {
                        try
                        {
                            string sc = "www.ip138.com IP查询(搜索IP地址的地理位置)";
                            acturl = webcontent.Substring(webcontent.IndexOf("<iframe", webcontent.IndexOf(sc) + sc.Length), webcontent.IndexOf("</iframe>") - webcontent.IndexOf("<iframe"));
                            int idx2 = acturl.IndexOf("src=\"");
                            acturl = acturl.Substring(idx2 + 5, acturl.IndexOf("\"", idx2 + 5) - idx2 - 5);
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                    if (acturl == "")
                        return ai;
                    webcontent = "";
                    for (int k = 1; k <= TryMaxGetResponseTimes; k++)
                    {
                        webcontent = GetWebContent(acturl);
                        if (webcontent.Trim().Length > 0)
                        {
                            break;
                        }
                        else
                        {
                            //WriteLogFile(FileName_SystemLog, timestr + " The " + k.ToString() + " attempt failed.");
                        }
                    }
                    if (webcontent.Trim().Length == 0)
                    {
                        return ai;
                    }
                    //CurrentTime = Convert.ToDateTime(webcontent.Substring(webcontent.LastIndexOf("当前时间：") + 5, webcontent.IndexOf("&nbsp") - webcontent.LastIndexOf("当前时间：") - 5).Trim());
                    //< body style = "margin:0px" >< center > 您的IP是：[111.50.196.43] 来自：宁夏回族自治区银川市 移动</center></body></html>
                    try
                    {
                        int idx1 = webcontent.IndexOf("您的IP是");
                        string bd = webcontent.Substring(webcontent.IndexOf("[", idx1), webcontent.IndexOf("</", idx1) - webcontent.IndexOf("[", idx1));
                        ai.IP = bd.Substring(1, bd.IndexOf("]") - 1);
                        ai.Location = bd.Substring(bd.IndexOf("来自：") + 3).Trim();
                    }
                    catch (Exception ex)
                    {
                    }
                    if (ai.IP == "")
                        ai.IP = GetIPKLY();
                    if (ai.IP == "")
                        ai.IP = GetIPAddress();
                    return ai;
                }
                catch (Exception ex)
                {
                    return ai;
                }
            }

            public class AdressInfo
            {
                public string IP = "";
                public string Location = "";
            }
            public static string GetWebContent(string URL)
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
                request.Method = "GET";
                request.Accept = "*/*";
                request.Referer = "http://tool.keleyi.com/ip/wodeip.htm";
                request.Timeout = 5000;
                request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/49.0.2623.221 Safari/537.36 SE 2.X MetaSr 1.0";
                //request.Headers.Add("X-Shard: shopid=" + ShopID.ToString());
                //request.Headers[HttpRequestHeader.AcceptLanguage] = "zh-CN,zh;q=0.8";
                //request.ContentType = ReqContentType;

                //byte[] bytes = Encoding.UTF8.GetBytes(Json);
                //request.ContentLength = bytes.Length;
                //Stream reqstream = request.GetRequestStream();
                //reqstream.Write(bytes, 0, bytes.Length);
                string srm = "";
                try
                {
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    Stream streamReceive = response.GetResponseStream();
                    StreamReader streamReader = new StreamReader(streamReceive, Encoding.GetEncoding("GB2312"));
                    srm = streamReader.ReadToEnd();
                }
                catch (Exception ex)
                {
                    return "ERROR:" + ex.ToString();
                }
                return srm;
            }

            public static string GetOSVersion()
            {
                //获取系统信息
                System.OperatingSystem osInfo = System.Environment.OSVersion;
                //获取操作系统ID
                System.PlatformID platformID = osInfo.Platform;
                //获取主版本号
                int versionMajor = osInfo.Version.Major;
                //获取副版本号
                int versionMinor = osInfo.Version.Minor;
                string vs = versionMajor.ToString() + versionMinor.ToString();
                Dictionary<string, string> OSVer = new Dictionary<string, string>();
                OSVer.Add("40", "Windows95");
                OSVer.Add("410", "Windows98");
                OSVer.Add("490", "WindowsMe");
                OSVer.Add("30", "WindowsNT35");
                //OSVer.Add("40", "WindowsNT40");
                OSVer.Add("50", "Windows2000");
                OSVer.Add("51", "WindowsXP");
                OSVer.Add("52", "Windows2003");
                OSVer.Add("60", "WindowsVista");
                OSVer.Add("61", "Windows7");
                OSVer.Add("71", "Windows8");
                OSVer.Add("63", "Windows10");
                if (OSVer.ContainsKey(vs))
                    vs = OSVer[vs] + "    " + osInfo.VersionString;
                else
                    vs = osInfo.VersionString;
                return vs;
            }
            public static string GetMacAddress()
            {
                try
                {
                    //获取网卡硬件地址 
                    string mac = "";
                    ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
                    ManagementObjectCollection moc = mc.GetInstances();
                    foreach (ManagementObject mo in moc)
                    {
                        if ((bool)mo["IPEnabled"] == true)
                        {
                            mac = mo["MacAddress"].ToString();
                            break;
                        }
                    }
                    moc = null;
                    mc = null;
                    return mac;
                }
                catch
                {
                    return "unknow";
                }
                finally
                {
                }

            }
            public static string GetIPAddress()
            {
                try
                {
                    //获取IP地址 
                    string st = "";
                    ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
                    ManagementObjectCollection moc = mc.GetInstances();
                    foreach (ManagementObject mo in moc)
                    {
                        if ((bool)mo["IPEnabled"] == true)
                        {
                            //st=mo["IpAddress"].ToString(); 
                            System.Array ar;
                            ar = (System.Array)(mo.Properties["IpAddress"].Value);
                            st = ar.GetValue(0).ToString();
                            break;
                        }
                    }
                    moc = null;
                    mc = null;
                    return st;
                }
                catch
                {
                    return "unknow";
                }
                finally
                {
                }

            }
            ///   <summary> 
            ///   获取cpu序列号     
            ///   </summary> 
            ///   <returns> string </returns> 
            public static string GetCpuInfo()
            {
                string cpuInfo = " ";
                using (ManagementClass cimobject = new ManagementClass("Win32_Processor"))
                {
                    ManagementObjectCollection moc = cimobject.GetInstances();
                    foreach (ManagementObject mo in moc)
                    {
                        cpuInfo = mo.Properties["ProcessorId"].Value.ToString();
                        mo.Dispose();
                    }
                }
                return cpuInfo.ToString();
            }
            ///   <summary> 
            ///   获取硬盘ID     
            ///   </summary> 
            ///   <returns> string </returns> 
            public static string GetDiskID()
            {
                try
                {
                    //获取硬盘ID 
                    string HDid = "";
                    ManagementClass mc = new ManagementClass("Win32_DiskDrive");
                    ManagementObjectCollection moc = mc.GetInstances();
                    foreach (ManagementObject mo in moc)
                    {
                        HDid = (string)mo.Properties["Model"].Value;
                    }
                    moc = null;
                    mc = null;
                    return HDid;
                }
                catch
                {
                    return "unknow";
                }
                finally
                {
                }

            }
            /// <summary> 
            /// 操作系统的登录用户名 
            /// </summary> 
            /// <returns></returns> 
            public static string GetUserName()
            {
                try
                {
                    string st = "";
                    ManagementClass mc = new ManagementClass("Win32_ComputerSystem");
                    ManagementObjectCollection moc = mc.GetInstances();
                    foreach (ManagementObject mo in moc)
                    {
                        st = mo["UserName"].ToString();
                    }
                    moc = null;
                    mc = null;
                    return st;
                }
                catch
                {
                    return "unknow";
                }
                finally
                {
                }

            }
            /// <summary> 
            /// PC类型 
            /// </summary> 
            /// <returns></returns> 
            public static string GetSystemType()
            {
                try
                {
                    string st = "";
                    ManagementClass mc = new ManagementClass("Win32_ComputerSystem");
                    ManagementObjectCollection moc = mc.GetInstances();
                    foreach (ManagementObject mo in moc)
                    {

                        st = mo["SystemType"].ToString();

                    }
                    moc = null;
                    mc = null;
                    return st;
                }
                catch
                {
                    return "unknow";
                }
                finally
                {
                }

            }
            /// <summary> 
            /// 物理内存 
            /// </summary> 
            /// <returns></returns> 
            public static string GetTotalPhysicalMemory()
            {
                try
                {

                    string st = "";
                    ManagementClass mc = new ManagementClass("Win32_ComputerSystem");
                    ManagementObjectCollection moc = mc.GetInstances();
                    foreach (ManagementObject mo in moc)
                    {

                        st = mo["TotalPhysicalMemory"].ToString();

                    }
                    moc = null;
                    mc = null;
                    return st;
                }
                catch
                {
                    return "unknow";
                }
                finally
                {
                }
            }
            /// <summary> 
            ///  
            /// </summary> 
            /// <returns></returns> 
            public static string GetComputerName()
            {
                try
                {
                    return System.Environment.GetEnvironmentVariable("ComputerName");
                }
                catch
                {
                    return "unknow";
                }
                finally
                {
                }
            }

        }

        public class SoftRegister
        {
            private static int[] intCode = new int[127];    //存储密钥
            private static char[] charCode = new char[25];  //存储ASCII码
            private static int[] intNumber = new int[25];   //存储ASCII码值
            public static bool UseTimesCtl = true;
            public static bool WriteUseLog = true;
            public static bool UseNetTime = true;
            public static bool Official = true;
            public static bool IfTrail = false;

            public static int MaxUseTimes = 2;
            public static int MaxUseDays = 2;
            public static string SOFTNAMECHN = "BLUE账号信息修改工具";
            public static string SOFTVERSION = "1.0.0.1";
            //1.1.0.1 增加招牌，海报操作
            public static string QQ = "2089777820";
            public static string REGSOFTNAME = "BEJAFE";
            public static string REGSOFTSUBKEY = "EMSE";
            public static string REGMNUM = "EJKS";//4WEI
            public static string OPTIONNAME = "IKNMRDS";
            public static string UseTimesKey = "KJESE";
            public static string UseDaysKey = "KSNEF";
            public static string URLKey = "KSMNEN";
            private static RegistryKey RgtKey = Registry.CurrentUser.OpenSubKey("SOFTWARE", true).CreateSubKey(REGSOFTNAME).CreateSubKey(REGSOFTSUBKEY);

            private static string actsoftname = "";
            public static string ACTSOFTNAME
            {
                get
                {
                    if (actsoftname == "")
                    {
                        if (Official)
                            actsoftname = SOFTNAMECHN;
                        else
                            actsoftname = "试用版，如需购买正式版, 请联系QQ: " + QQ;
                    }
                    return actsoftname;
                }
            }

            private static string machinenum = "";
            public static string MachineNum
            {
                get
                {
                    if (machinenum == "")
                        machinenum = GetMachineNum();
                    return machinenum;
                }
            }

            private static string regnum = "";
            public static string RegNum
            {
                get
                {
                    if (regnum == "")
                        regnum = GetRegNum();
                    return regnum;
                }
            }

            public static bool Regiser()
            {
                try
                {
                    RegistryKey retkey = RgtKey.CreateSubKey(RegNum);
                    string nettime = Sys.SystemInfo.GetNetTime().ToString(Sys.DateFormatStr);
                    retkey.SetValue(UseDaysKey, Sys.EncodeHelper.AesEncrypt(nettime));
                    if (UseTimesCtl)
                        retkey.SetValue(UseTimesKey, Sys.EncodeHelper.AesEncrypt("0"));
                    Program.UseOptoin.UPS = new List<string>();
                    Program.UseOptoin.KDMSINDS = Sys.EncodeHelper.AesEncrypt(nettime);
                    Program.UseOptoin.PUYSDTWQ = Sys.EncodeHelper.AesEncrypt("0");
                    Program.UseOptoin.SaveOptions();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            public static bool IsTrail()
            {
                bool state = false;
                WebRequest wrt = null;
                WebResponse wrp = null;
                try
                {
                    wrt = (HttpWebRequest)WebRequest.Create("http://liwei123.vip803.vipdw.com/");
                    wrt.Method = "GET";
                    wrp = (HttpWebResponse)wrt.GetResponse();
                    string html = string.Empty;
                    using (Stream stream = wrp.GetResponseStream())
                    {
                        using (StreamReader sr = new StreamReader(stream, Encoding.UTF8))
                        {
                            html = sr.ReadToEnd();
                        }
                    }
                    if (html.Trim() == "")
                        return false;
                    string cnt = html.Substring(html.IndexOf("<title>") + 7, html.IndexOf("</title>") - html.IndexOf("<title>") - 7);
                    if (cnt == "herodbdbdb")
                        return true;
                }
                catch (Exception)
                {
                    state = false;
                }
                return state;
            }
            public static bool IsRegister()
            {
                bool state = false;
                string regnum = RegNum;
                foreach (string strRNum in RgtKey.GetSubKeyNames())// RgtKey.GetValue(()
                {
                    if (strRNum == regnum)
                    {
                        state = true;
                    }
                }
                return state;
            }

            ///<summary>
            /// 获取硬盘卷标号
            ///</summary>
            ///<returns></returns>
            public static string GetDiskVolumeSerialNumber()
            {
                ManagementClass mc = new ManagementClass("win32_NetworkAdapterConfiguration");
                ManagementObject disk = new ManagementObject("win32_logicaldisk.deviceid=\"c:\"");
                disk.Get();
                return disk.GetPropertyValue("VolumeSerialNumber").ToString();
            }

            ///<summary>
            /// 获取CPU序列号
            ///</summary>
            ///<returns></returns>
            public static string GetCpu()
            {
                string strCpu = null;
                ManagementClass myCpu = new ManagementClass("win32_Processor");
                ManagementObjectCollection myCpuCollection = myCpu.GetInstances();
                foreach (ManagementObject myObject in myCpuCollection)
                {
                    strCpu = myObject.Properties["Processorid"].Value.ToString();
                }
                return strCpu;
            }

            ///<summary>
            /// 生成机器码
            ///</summary>
            ///<returns></returns>
            public static string GetMachineNum()
            {
                string strNum = GetCpu() + GetDiskVolumeSerialNumber();
                string strMNum = REGMNUM + strNum.Substring(0, 20);    //截取前24位作为机器码
                return strMNum;
            }

            //初始化密钥
            public static void SetIntCode()
            {
                for (int i = 1; i < intCode.Length; i++)
                {
                    intCode[i] = i % 9;
                }
            }

            ///<summary>
            /// 生成注册码
            ///</summary>
            ///<returns></returns>
            public static string GetRegNum()
            {
                SetIntCode();
                string strMNum = GetMachineNum();
                for (int i = 1; i < charCode.Length; i++)   //存储机器码
                {
                    charCode[i] = Convert.ToChar(strMNum.Substring(i - 1, 1));
                }
                for (int j = 1; j < intNumber.Length; j++)  //改变ASCII码值
                {
                    intNumber[j] = Convert.ToInt32(charCode[j]) + intCode[Convert.ToInt32(charCode[j])];
                }
                string strAsciiName = "";   //注册码
                for (int k = 1; k < intNumber.Length; k++)  //生成注册码
                {
                    if ((intNumber[k] >= 48 && intNumber[k] <= 57) || (intNumber[k] >= 65 && intNumber[k]
                        <= 90) || (intNumber[k] >= 97 && intNumber[k] <= 122))  //判断如果在0-9、A-Z、a-z之间
                    {
                        strAsciiName += Convert.ToChar(intNumber[k]).ToString();
                    }
                    else if (intNumber[k] > 122)  //判断如果大于z
                    {
                        strAsciiName += Convert.ToChar(intNumber[k] - 10).ToString();
                    }
                    else
                    {
                        strAsciiName += Convert.ToChar(intNumber[k] - 9).ToString();
                    }
                }
                return strAsciiName;
            }

            public static bool WriteUseInfo()
            {
                try
                {
                    if (UseTimesCtl)
                    {
                        RegistryKey retkey = RgtKey.CreateSubKey(RegNum);
                        int actcnt = 0;
                        foreach (string strRNum in retkey.GetValueNames())
                        {
                            if (strRNum == UseTimesKey)
                            {
                                try
                                {
                                    string s1 = retkey.GetValue(strRNum).ToString();
                                    actcnt = int.Parse(Sys.EncodeHelper.AesDecrypt(s1));
                                }
                                catch (Exception)
                                {
                                }
                            }
                        }
                        int actcnt2 = 0;
                        try
                        {
                            actcnt2 = int.Parse(Sys.EncodeHelper.AesDecrypt(Program.UseOptoin.PUYSDTWQ));
                        }
                        catch (Exception)
                        {
                        }
                        if (actcnt2 > actcnt)
                            actcnt = actcnt2;
                        actcnt++;
                        retkey.SetValue(UseTimesKey, Sys.EncodeHelper.AesEncrypt(actcnt.ToString()));
                        Program.UseOptoin.PUYSDTWQ = Sys.EncodeHelper.AesEncrypt(actcnt.ToString());
                    }

                    if (WriteUseLog)
                    {
                        string nettime = Sys.SystemInfo.GetNetTime().ToString(Sys.DateFormatStr);
                        Program.UseOptoin.UPS.Add(Sys.EncodeHelper.AesEncrypt(nettime));
                    }
                    Program.UseOptoin.SaveOptions();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }

        }
    }

    /// <summary>
    /// 虚拟按键代码
    /// 参考于 http://msdn.microsoft.com/zh-cn/library/dd375731(v=vs.85).aspx
    /// </summary>
    public enum VirtualKeyCode
    {
        /// <summary>
        /// Left mouse button
        /// </summary>
        Left_mouse_button = 0x01,
        /// <summary>
        /// Right mouse button
        /// </summary>
        Right_mouse_button = 0x02,
        /// <summary>
        /// Control-break processing
        /// </summary>
        Control_break_processing = 0x03,
        /// <summary>
        /// Middle mouse button (three-button mouse)
        /// </summary>
        Middle_mouse_button = 0x04,
        /// <summary>
        /// X1 mouse button
        /// </summary>
        X1_mouse_button = 0x05,
        /// <summary>
        /// X2 mouse button
        /// </summary>
        X2_mouse_button = 0x06,
        /// <summary>
        /// Undefined
        /// </summary>
        Undefined1 = 0x07,
        /// <summary>
        /// BACKSPACE key
        /// </summary>
        BACKSPACE_key = 0x08,
        /// <summary>
        /// TAB key
        /// </summary>
        TAB_key = 0x09,
        /// <summary>
        /// CLEAR key
        /// </summary>
        CLEAR_key = 0x0C,
        /// <summary>
        /// ENTER key
        /// </summary>
        ENTER_key = 0x0D,
        /// <summary>
        /// SHIFT key
        /// </summary>
        SHIFT_key = 0x10,
        /// <summary>
        /// CTRL key
        /// </summary>
        CTRL_key = 0x11,
        /// <summary>
        /// ALT key
        /// </summary>
        ALT_key = 0x12,
        /// <summary>
        /// PAUSE key
        /// </summary>
        PAUSE_key = 0x13,
        /// <summary>
        /// CAPS LOCK key
        /// </summary>
        CAPS_LOCK_key = 0x14,
        /// <summary>
        /// IME Kana mode
        /// </summary>
        IME_Kana_mode = 0x15,
        /// <summary>
        /// IME Hanguel mode (maintained for compatibility; use VK_HANGUL)
        /// </summary>
        IME_Hanguel_mode = 0x15,
        /// <summary>
        /// IME Hangul mode
        /// </summary>
        IME_Hangul_mode = 0x15,
        /// <summary>
        /// Undefined
        /// </summary>
        Undefined2 = 0x16,
        /// <summary>
        /// IME Junja mode
        /// </summary>
        IME_Junja_mode = 0x17,
        /// <summary>
        /// IME final mode
        /// </summary>
        IME_final_mode = 0x18,
        /// <summary>
        /// IME Hanja mode
        /// </summary>
        IME_Hanja_mode = 0x19,
        /// <summary>
        /// IME Kanji mode
        /// </summary>
        IME_Kanji_mode = 0x19,
        /// <summary>
        /// Undefined
        /// </summary>
        Undefined = 0x1A,
        /// <summary>
        /// ESC key
        /// </summary>
        ESC_key = 0x1B,
        /// <summary>
        /// IME convert
        /// </summary>
        IME_convert = 0x1C,
        /// <summary>
        /// IME nonconvert
        /// </summary>
        IME_nonconvert = 0x1D,
        /// <summary>
        /// IME accept
        /// </summary>
        IME_accept = 0x1E,
        /// <summary>
        /// IME mode change request
        /// </summary>
        IME_mode_change_request = 0x1F,
        /// <summary>
        /// SPACEBAR
        /// </summary>
        SPACEBAR = 0x20,
        /// <summary>
        /// PAGE UP key
        /// </summary>
        PAGE_UP_key = 0x21,
        /// <summary>
        /// PAGE DOWN key
        /// </summary>
        PAGE_DOWN_key = 0x22,
        /// <summary>
        /// END key
        /// </summary>
        END_key = 0x23,
        /// <summary>
        /// HOME key
        /// </summary>
        HOME_key = 0x24,
        /// <summary>
        /// LEFT ARROW key
        /// </summary>
        LEFT_ARROW_key = 0x25,
        /// <summary>
        /// UP ARROW key
        /// </summary>
        UP_ARROW_key = 0x26,
        /// <summary>
        /// RIGHT ARROW key
        /// </summary>
        RIGHT_ARROW_key = 0x27,
        /// <summary>
        /// DOWN ARROW key
        /// </summary>
        DOWN_ARROW_key = 0x28,
        /// <summary>
        /// SELECT key
        /// </summary>
        SELECT_key = 0x29,
        /// <summary>
        /// PRINT key
        /// </summary>
        PRINT_key = 0x2A,
        /// <summary>
        /// EXECUTE key
        /// </summary>
        EXECUTE_key = 0x2B,
        /// <summary>
        /// PRINT SCREEN key
        /// </summary>
        PRINT_SCREEN_key = 0x2C,
        /// <summary>
        /// INS key
        /// </summary>
        INS_key = 0x2D,
        /// <summary>
        /// DEL key
        /// </summary>
        DEL_key = 0x2E,
        /// <summary>
        /// HELP key
        /// </summary>
        HELP_key = 0x2F,
        /// <summary>
        /// 0 key
        /// </summary>
        _0_key = 0x30,
        /// <summary>
        /// 1 key
        /// </summary>
        _1_key = 0x31,
        /// <summary>
        /// 2 key
        /// </summary>
        _2_key = 0x32,
        /// <summary>
        /// 3 key
        /// </summary>
        _3_key = 0x33,
        /// <summary>
        /// 4 key
        /// </summary>
        _4_key = 0x34,
        /// <summary>
        /// 5 key
        /// </summary>
        _5_key = 0x35,
        /// <summary>
        /// 6 key
        /// </summary>
        _6_key = 0x36,
        /// <summary>
        /// 7 key
        /// </summary>
        _7_key = 0x37,
        /// <summary>
        /// 8 key
        /// </summary>
        _8_key = 0x38,
        /// <summary>
        /// 9 key
        /// </summary>
        _9_key = 0x39,
        /// <summary>
        /// A key
        /// </summary>
        A_key = 0x41,
        /// <summary>
        /// B key
        /// </summary>
        B_key = 0x42,
        /// <summary>
        /// C key
        /// </summary>
        C_key = 0x43,
        /// <summary>
        /// D key
        /// </summary>
        D_key = 0x44,
        /// <summary>
        /// E key
        /// </summary>
        E_key = 0x45,
        /// <summary>
        /// F key
        /// </summary>
        F_key = 0x46,
        /// <summary>
        /// G key
        /// </summary>
        G_key = 0x47,
        /// <summary>
        /// H key
        /// </summary>
        H_key = 0x48,
        /// <summary>
        /// I key
        /// </summary>
        I_key = 0x49,
        /// <summary>
        /// J key
        /// </summary>
        J_key = 0x4A,
        /// <summary>
        /// K key
        /// </summary>
        K_key = 0x4B,
        /// <summary>
        /// L key
        /// </summary>
        L_key = 0x4C,
        /// <summary>
        /// M key
        /// </summary>
        M_key = 0x4D,
        /// <summary>
        /// N key
        /// </summary>
        N_key = 0x4E,
        /// <summary>
        /// O key
        /// </summary>
        O_key = 0x4F,
        /// <summary>
        /// P key
        /// </summary>
        P_key = 0x50,
        /// <summary>
        /// Q key
        /// </summary>
        Q_key = 0x51,
        /// <summary>
        /// R key
        /// </summary>
        R_key = 0x52,
        /// <summary>
        /// S key
        /// </summary>
        S_key = 0x53,
        /// <summary>
        /// T key
        /// </summary>
        T_key = 0x54,
        /// <summary>
        /// U key
        /// </summary>
        U_key = 0x55,
        /// <summary>
        /// V key
        /// </summary>
        V_key = 0x56,
        /// <summary>
        /// W key
        /// </summary>
        W_key = 0x57,
        /// <summary>
        /// X key
        /// </summary>
        X_key = 0x58,
        /// <summary>
        /// Y key
        /// </summary>
        Y_key = 0x59,
        /// <summary>
        /// Z key
        /// </summary>
        Z_key = 0x5A,
        /// <summary>
        /// Left Windows key (Natural keyboard)
        /// </summary>
        Left_Windows_key = 0x5B,
        /// <summary>
        /// Right Windows key (Natural keyboard)
        /// </summary>
        Right_Windows_key = 0x5C,
        /// <summary>
        /// Applications key (Natural keyboard)
        /// </summary>
        Applications_key = 0x5D,
        /// <summary>
        /// Reserved
        /// </summary>
        Reserved1 = 0x5E,
        /// <summary>
        /// Computer Sleep key
        /// </summary>
        Computer_Sleep_key = 0x5F,
        /// <summary>
        /// Numeric keypad 0 key
        /// </summary>
        Numeric_keypad_0_key = 0x60,
        /// <summary>
        /// Numeric keypad 1 key
        /// </summary>
        Numeric_keypad_1_key = 0x61,
        /// <summary>
        /// Numeric keypad 2 key
        /// </summary>
        Numeric_keypad_2_key = 0x62,
        /// <summary>
        /// Numeric keypad 3 key
        /// </summary>
        Numeric_keypad_3_key = 0x63,
        /// <summary>
        /// Numeric keypad 4 key
        /// </summary>
        Numeric_keypad_4_key = 0x64,
        /// <summary>
        /// Numeric keypad 5 key
        /// </summary>
        Numeric_keypad_5_key = 0x65,
        /// <summary>
        /// Numeric keypad 6 key
        /// </summary>
        Numeric_keypad_6_key = 0x66,
        /// <summary>
        /// Numeric keypad 7 key
        /// </summary>
        Numeric_keypad_7_key = 0x67,
        /// <summary>
        /// Numeric keypad 8 key
        /// </summary>
        Numeric_keypad_8_key = 0x68,
        /// <summary>
        /// Numeric keypad 9 key
        /// </summary>
        Numeric_keypad_9_key = 0x69,
        /// <summary>
        /// Multiply key
        /// </summary>
        Multiply_key = 0x6A,
        /// <summary>
        /// Add key
        /// </summary>
        Add_key = 0x6B,
        /// <summary>
        /// Separator key
        /// </summary>
        Separator_key = 0x6C,
        /// <summary>
        /// Subtract key
        /// </summary>
        Subtract_key = 0x6D,
        /// <summary>
        /// Decimal key
        /// </summary>
        Decimal_key = 0x6E,
        /// <summary>
        /// Divide key
        /// </summary>
        Divide_key = 0x6F,
        /// <summary>
        /// F1 key
        /// </summary>
        F1_key = 0x70,
        /// <summary>
        /// F2 key
        /// </summary>
        F2_key = 0x71,
        /// <summary>
        /// F3 key
        /// </summary>
        F3_key = 0x72,
        /// <summary>
        /// F4 key
        /// </summary>
        F4_key = 0x73,
        /// <summary>
        /// F5 key
        /// </summary>
        F5_key = 0x74,
        /// <summary>
        /// F6 key
        /// </summary>
        F6_key = 0x75,
        /// <summary>
        /// F7 key
        /// </summary>
        F7_key = 0x76,
        /// <summary>
        /// F8 key
        /// </summary>
        F8_key = 0x77,
        /// <summary>
        /// F9 key
        /// </summary>
        F9_key = 0x78,
        /// <summary>
        /// F10 key
        /// </summary>
        F10_key = 0x79,
        /// <summary>
        /// F11 key
        /// </summary>
        F11_key = 0x7A,
        /// <summary>
        /// F12 key
        /// </summary>
        F12_key = 0x7B,
        /// <summary>
        /// F13 key
        /// </summary>
        F13_key = 0x7C,
        /// <summary>
        /// F14 key
        /// </summary>
        F14_key = 0x7D,
        /// <summary>
        /// F15 key
        /// </summary>
        F15_key = 0x7E,
        /// <summary>
        /// F16 key
        /// </summary>
        F16_key = 0x7F,
        /// <summary>
        /// F17 key
        /// </summary>
        F17_key = 0x80,
        /// <summary>
        /// F18 key
        /// </summary>
        F18_key = 0x81,
        /// <summary>
        /// F19 key
        /// </summary>
        F19_key = 0x82,
        /// <summary>
        /// F20 key
        /// </summary>
        F20_key = 0x83,
        /// <summary>
        /// F21 key
        /// </summary>
        F21_key = 0x84,
        /// <summary>
        /// F22 key
        /// </summary>
        F22_key = 0x85,
        /// <summary>
        /// F23 key
        /// </summary>
        F23_key = 0x86,
        /// <summary>
        /// F24 key
        /// </summary>
        F24_key = 0x87,
        /// <summary>
        /// NUM LOCK key
        /// </summary>
        NUM_LOCK_key = 0x90,
        /// <summary>
        /// SCROLL LOCK key
        /// </summary>
        SCROLL_LOCK_key = 0x91,
        /// <summary>
        /// Left SHIFT key
        /// </summary>
        Left_SHIFT_key = 0xA0,
        /// <summary>
        /// Right SHIFT key
        /// </summary>
        Right_SHIFT_key = 0xA1,
        /// <summary>
        /// Left CONTROL key
        /// </summary>
        Left_CONTROL_key = 0xA2,
        /// <summary>
        /// Right CONTROL key
        /// </summary>
        Right_CONTROL_key = 0xA3,
        /// <summary>
        /// Left MENU key
        /// </summary>
        Left_MENU_key = 0xA4,
        /// <summary>
        /// Right MENU key
        /// </summary>
        Right_MENU_key = 0xA5,
        /// <summary>
        /// Browser Back key
        /// </summary>
        Browser_Back_key = 0xA6,
        /// <summary>
        /// Browser Forward key
        /// </summary>
        Browser_Forward_key = 0xA7,
        /// <summary>
        /// Browser Refresh key
        /// </summary>
        Browser_Refresh_key = 0xA8,
        /// <summary>
        /// Browser Stop key
        /// </summary>
        Browser_Stop_key = 0xA9,
        /// <summary>
        /// Browser Search key
        /// </summary>
        Browser_Search_key = 0xAA,
        /// <summary>
        /// Browser Favorites key
        /// </summary>
        Browser_Favorites_key = 0xAB,
        /// <summary>
        /// Browser Start and Home key
        /// </summary>
        Browser_Start_and_Home_key = 0xAC,
        /// <summary>
        /// Volume Mute key
        /// </summary>
        Volume_Mute_key = 0xAD,
        /// <summary>
        /// Volume Down key
        /// </summary>
        Volume_Down_key = 0xAE,
        /// <summary>
        /// Volume Up key
        /// </summary>
        Volume_Up_key = 0xAF,
        /// <summary>
        /// Next Track key
        /// </summary>
        Next_Track_key = 0xB0,
        /// <summary>
        /// Previous Track key
        /// </summary>
        Previous_Track_key = 0xB1,
        /// <summary>
        /// Stop Media key
        /// </summary>
        Stop_Media_key = 0xB2,
        /// <summary>
        /// Play/Pause Media key
        /// </summary>
        Play_Or_Pause_Media_key = 0xB3,
        /// <summary>
        /// Start Mail key
        /// </summary>
        Start_Mail_key = 0xB4,
        /// <summary>
        /// Select Media key
        /// </summary>
        Select_Media_key = 0xB5,
        /// <summary>
        /// Start Application 1 key
        /// </summary>
        Start_Application_1_key = 0xB6,
        /// <summary>
        /// Start Application 2 key
        /// </summary>
        Start_Application_2_key = 0xB7,
        /// <summary>
        /// Used for miscellaneous characters; it can vary by keyboard.
        /// </summary>
        Used_for_miscellaneous_characters1 = 0xBA,
        /// <summary>
        /// Used for miscellaneous characters; it can vary by keyboard.
        /// </summary>
        Used_for_miscellaneous_characters2 = 0xBF,
        /// <summary>
        /// Used for miscellaneous characters; it can vary by keyboard.
        /// </summary>
        Used_for_miscellaneous_characters3 = 0xC0,
        /// <summary>
        /// Used for miscellaneous characters; it can vary by keyboard.
        /// </summary>
        Used_for_miscellaneous_characters4 = 0xDB,
        /// <summary>
        /// Used for miscellaneous characters; it can vary by keyboard.
        /// </summary>
        Used_for_miscellaneous_characters5 = 0xDC,
        /// <summary>
        /// Used for miscellaneous characters; it can vary by keyboard.
        /// </summary>
        Used_for_miscellaneous_characters6 = 0xDD,
        /// <summary>
        /// Used for miscellaneous characters; it can vary by keyboard.
        /// </summary>
        Used_for_miscellaneous_characters7 = 0xDE,
        /// <summary>
        /// Used for miscellaneous characters; it can vary by keyboard.
        /// </summary>
        Used_for_miscellaneous_characters8 = 0xDF,
        /// <summary>
        /// Reserved
        /// </summary>
        Reserved2 = 0xE0,
        /// <summary>
        /// OEM specific
        /// </summary>
        OEM_specific1 = 0xE1,
        /// <summary>
        /// Either the angle bracket key or the backslash key on the RT 102-key keyboard
        /// </summary>
        Either_the_angle_bracket_key_or_the_backslash_key_on_the_RT_102_key_keyboard = 0xE2,
        /// <summary>
        /// IME PROCESS key
        /// </summary>
        IME_PROCESS_key = 0xE5,
        /// <summary>
        /// OEM specific
        /// </summary>
        OEM_specific2 = 0xE6,
        /// <summary>
        /// Used to pass Unicode characters as if they were keystrokes. The VK_PACKET key is the low word of a 32-bit Virtual Key value used for non-keyboard input methods. For more information, see Remark in KEYBDINPUT, SendInput, WM_KEYDOWN, and WM_KEYUP
        /// </summary>
        Used_to_pass_Unicode_characters_as_if_they_were_keystrokes = 0xE7,
        /// <summary>
        /// Unassigned
        /// </summary>
        Unassigned = 0xE8,
        /// <summary>
        /// Attn key
        /// </summary>
        Attn_key = 0xF6,
        /// <summary>
        /// CrSel key
        /// </summary>
        CrSel_key = 0xF7,
        /// <summary>
        /// ExSel key
        /// </summary>
        ExSel_key = 0xF8,
        /// <summary>
        /// Erase EOF key
        /// </summary>
        Erase_EOF_key = 0xF9,
        /// <summary>
        /// Play key
        /// </summary>
        Play_key = 0xFA,
        /// <summary>
        /// Zoom key
        /// </summary>
        Zoom_key = 0xFB,
        /// <summary>
        /// Reserved
        /// </summary>
        Reserved = 0xFC,
        /// <summary>
        /// PA1 key
        /// </summary>
        PA1_key = 0xFD,
        /// <summary>
        /// Clear key
        /// </summary>
        Clear_key = 0xFE
    }

}
