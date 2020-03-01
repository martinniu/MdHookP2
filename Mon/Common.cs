using System;
using System.Text;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Net;
using System.IO;
using System.Data;
using System.Runtime.InteropServices;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.Serialization.Json;
using Microsoft.Win32;
using System.Management;
using System.Web;
using System.Drawing;
using System.Windows.Forms;

namespace Mon
{
    public static class SystemConfig
    {
        //public static string SoftwareName = "";
        //public static string NoticeDesc = "";
        //public static string WebSite = "";
        //public static string SpreadDesc = "";
        public static string BitDescSite = "";
        public static string BitYLSite = "";
        public static string VideoSite = "";
        public static string SpreadVideoSite = "";
        public static string OpenVipSite = "";
        public static bool isShow = false;
        public static string ShowMsg = "";
        //public static string Version = "1.1";

        public static string DateFormatStr = "yyyy-MM-dd HH:mm:ss";
        public static string DateFormatStrF = "yyyy-MM-dd HH:mm:ss.fff";

        //20191218 1.0
        //*        1.创建
        //20200105 1.2
        //         发版，测试
        //20200110 2.0
        //         增加注册机

    }
    public class K8HDJS6NMX
    {
        public static List<Dictionary<string, string>> GetListByJson(string jsonstr)
        {
            //{"Name":"TxEndTime","Value":"08:00:00","Description":提现结束时间},{}
            //{\"RES\":\"1\"}
            List<Dictionary<string, string>> diclst = new List<Dictionary<string, string>>();
            if (jsonstr.Length == 0 || jsonstr == "[]")
                return diclst;
            jsonstr = jsonstr.Substring(1);
            jsonstr = jsonstr.Substring(0, jsonstr.Length - 1);
            if (jsonstr.StartsWith("{\"RES\":"))
            {
                if (jsonstr.StartsWith("{\"RES\":\""))
                {
                    jsonstr = jsonstr.Replace("{\"RES\":\"", "");
                    jsonstr = jsonstr.Replace("\"}", "");
                }
                else
                {
                    jsonstr = jsonstr.Replace("{\"RES\":", "");
                    jsonstr = jsonstr.Replace("}", "");
                }
                Dictionary<string, string> dic = new Dictionary<string, string>();
                dic.Add("RES", jsonstr);
                diclst.Add(dic);
                return diclst;
            }
            jsonstr = jsonstr.Replace("},{", "}},{{");
            string[] arr1 = jsonstr.Split(new string[] { "},{" }, StringSplitOptions.None);
            for (int i = 0; i < arr1.Length; i++)
            {
                Dictionary<string, string> dic = new Dictionary<string, string>();
                arr1[i] = arr1[i].Replace("{\"", "").Replace("\"}", "");
                if (arr1[i].EndsWith("}"))
                {
                    arr1[i] = arr1[i].Substring(0, arr1[i].Length - 1);
                }
                arr1[i] = arr1[i].Replace("\",\"", "@$#");
                arr1[i] = arr1[i].Replace("\",", "@$#");
                arr1[i] = arr1[i].Replace(",\"", "@$#");
                string[] arr2 = arr1[i].Split(new string[] { "@$#" }, StringSplitOptions.None);
                for (int k = 0; k < arr2.Length; k++)
                {
                    arr2[k] = arr2[k].Replace("\":\"", "*&$");
                    arr2[k] = arr2[k].Replace("\":", "*&$");
                    arr2[k] = arr2[k].Replace(":\"", "*&$");
                    dic.Add(arr2[k].Substring(0, arr2[k].IndexOf("*&$")).ToUpper(), arr2[k].Substring(arr2[k].IndexOf("*&$") + 3));
                }
                diclst.Add(dic);
            }
            return diclst;
        }

        public static string LKDNSD = "";


        public static DataTable GetListByResultSet(string jsonstr)
        {
            DataTable dt = null;
            try
            {
                if (jsonstr.Length == 0)
                    return dt;
                jsonstr = GetListByJson(jsonstr)[0]["RES"];
                string colstr = jsonstr.Substring(0, jsonstr.IndexOf("{`}"));
                string[] colarr = colstr.Split(new string[] { "|`#" }, StringSplitOptions.None);
                dt = new DataTable();
                for (int i = 0; i < colarr.Length; i++)
                {
                    dt.Columns.Add(colarr[i]);
                }
                if (jsonstr.EndsWith("{`}"))
                    return dt;
                string datastr = jsonstr.Substring(jsonstr.IndexOf("{`}") + 4);
                datastr = datastr.Substring(0, datastr.Length - 1);
                string[] dataarr = datastr.Split(new string[] { "},{" }, StringSplitOptions.None);
                for (int i = 0; i < dataarr.Length; i++)
                {
                    DataRow dr = dt.NewRow();
                    string ds = dataarr[i];
                    string[] tarr = ds.Split(new string[] { "|`$" }, StringSplitOptions.None);
                    dr.ItemArray = tarr;
                    dt.Rows.Add(dr);
                }
            }
            catch (Exception)
            {
                return null;
            }
            return dt;
        }
        public static string HK3D89HSM24X(string method, string para)
        {
            string res = "";
            try
            {
                long millis = DateTime.Now.Ticks;
                string jsonStr = "{ \"METHOD\":\"" + method + "\", \"PARA\":\"" + para + "\"}";
                //string jsonStr =  //JSON.stringify(reqinfo);
                jsonStr = EncodeHelper.AesEncrypt(jsonStr, SignatureUtil.KEY);
                string ct = SignatureUtil.Digest(jsonStr, "MD5");
                string signature = SignatureUtil.GenerateSignature(ct, millis);
                List<KeyValuePair<string, string>> paraMap = new List<KeyValuePair<string, string>>();
                paraMap.Add(new KeyValuePair<string, string>("s", signature));
                paraMap.Add(new KeyValuePair<string, string>("a", SignatureUtil.APPID));
                paraMap.Add(new KeyValuePair<string, string>("t", millis.ToString()));
                paraMap.Add(new KeyValuePair<string, string>("m", ct));
                paraMap.Add(new KeyValuePair<string, string>("v", jsonStr));
                byte[] bytes = Encoding.UTF8.GetBytes(jsonStr);
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(SignatureUtil.BuildUri(LKDNSD, paraMap));
                //request.Headers.Add("AccAgnt", "zh-cn");
                request.Method = "GET";
                //request.ContentLength = bytes.Length;
                request.ContentType = "text/xml";
                //Stream reqstream = request.GetRequestStream();
                //reqstream.Write(bytes, 0, bytes.Length);
                request.Timeout = 10000;
                //设置连接超时时间  
                //request.Headers.Set("Pragma", "no-cache");
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream streamReceive = response.GetResponseStream();
                StreamReader streamReader = new StreamReader(streamReceive, Encoding.UTF8);
                res = EncodeHelper.AesDecrypt(streamReader.ReadToEnd(), SignatureUtil.KEY);
                streamReceive.Dispose();
                streamReader.Dispose();
            }
            catch (Exception e)
            {
                if(e.Message.Contains("超时"))
                    res = "ERROR:网络请求出错, 连接超时";
                else
                    res = "ERROR:网络请求出错";
            }
            return res;
        }
    }

    public static class SignatureUtil
    {
        private static string encryptionAlgorithm = "SHA1";
        public static string APPID = "COM.ABANT.WX";
        public static string TOKEN = "#K7WQ8D12T@55MSA";
        public static string KEY = "ND#MlksdKNFURREQ";

        /// <summary>
        /// 使用指定算法生成消息摘要，默认是MD5
        /// </summary>
        /// <param name="strSrc">a string will be encrypted</param>
        /// <param name="encName">the algorithm name will be used, dafault to "MD5"</param>
        /// <returns></returns>
        public static string Digest(string strSrc, string encName)
        {
            string strDes = null;
            byte[] bt = Encoding.UTF8.GetBytes(strSrc);
            byte[] resbt = null;
            try
            {
                if (encName == null || encName == "")
                    encName = "MD5";
                if (encName == "MD5")
                {
                    MD5 md5 = System.Security.Cryptography.MD5.Create();
                    resbt = md5.ComputeHash(bt);
                }
                else if (encName == "SHA1")
                {
                    SHA1 sha1 = System.Security.Cryptography.SHA1.Create();
                    resbt = sha1.ComputeHash(bt);
                }
                else
                    return null;
                StringBuilder ret = new StringBuilder();
                foreach (byte b in resbt)
                    ret.AppendFormat("{0:X2}", b);
                strDes = ret.ToString();
            }
            catch (Exception)
            {
                return null;
            }
            return strDes;
        }

        public static string BuildUri(string uri, List<KeyValuePair<string, string>> paraMap)
        {
            StringBuilder sb = new StringBuilder();
            uri = uri.Trim();
            if (uri.EndsWith("/"))
                uri = uri.Substring(0, uri.Length - 1);
            if (uri.EndsWith("?"))
                uri = uri.Substring(0, uri.Length - 1);
            sb.Append(uri);
            if (paraMap != null && paraMap.Count > 0)
            {
                sb.Append("?");
                for (int i = 0; i < paraMap.Count; i++)
                {
                    sb.Append(paraMap[i].Key);
                    sb.Append("=");
                    sb.Append(paraMap[i].Value);
                    sb.Append("&");
                }
            }
            string newuri = sb.ToString();
            if (newuri.EndsWith("&"))
                newuri = newuri.Substring(0, newuri.Length - 1);
            return newuri;
        }

        /// <summary>
        /// 根据appid、token、lol以及时间戳来生成签名
        /// </summary>
        /// <param name="lol"></param>
        /// <param name="millis"></param>
        /// <returns></returns>
        public static string GenerateSignature(string content, long millis)
        {
            string timestamp = millis.ToString();
            string signature = null;
            if (timestamp != "")
            {
                List<string> srcList = new List<string>();
                srcList.Add(timestamp);
                srcList.Add(APPID);
                srcList.Add(TOKEN);
                srcList.Add(content);
                srcList.Sort();
                srcList.Reverse();
                // 按照字典序逆序拼接参数
                //Collections.sort(srcList);
                //Collections.reverse(srcList);
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < srcList.Count; i++)
                {
                    sb.Append(srcList[i]);
                }
                signature = Digest(sb.ToString(), encryptionAlgorithm);
                srcList.Clear();
                srcList = null;
            }
            return signature;
        }
    }


    public static class RQDSABMLS
    {
        private static string isefk9s9fs = "";//ab  "";//  "";//"4V/L7vMjIYWvHzTM9Ku6KgXVEJK4nUGwyg/x+W/QK68=";//209       
        public static string SHES782SJK = "1649S6n6ybEm0NfqpwTMHQ==";

        public static string ISEFK9S9FS
        {
            set
            {
                isefk9s9fs = value;
            }

            get
            {
                //if (isefk9s9fs == "")
                //{
                //    string url = "http://" + SystemInfo.GetIPAddress() + ":80";
                //    isefk9s9fs = EncodeHelper.AesEncrypt(url);
                //}
                return isefk9s9fs;
            }
        }

        public static string getMsg()
        {
            return K8HDJS6NMX.HK3D89HSM24X("GET_MSG", "");
        }


    }

    public static class EncodeHelper
    {
        private static string DEFAULTKEY = "kj2dkn7kwef4bhj8";

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

        //base64编码的文本转为    图片
        public static void Base64StringToImageFile(string baseString, string fileFullName)
        {
            try
            {
                Bitmap bmp = Base64StringToImage(baseString);
                bmp.Save(fileFullName, System.Drawing.Imaging.ImageFormat.Png);
                //bmp.Save(txtFileName + ".bmp", ImageFormat.Bmp);
                //bmp.Save(txtFileName + ".gif", ImageFormat.Gif);
                //bmp.Save(txtFileName + ".png", ImageFormat.Png);
                //this.pictureBox1.Image = bmp;
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Base64StringToImage 转换失败\nException：" + ex.Message);
            }
        }

        public static Bitmap Base64StringToImage(string baseString)
        {
            Bitmap bmp = null;
            try
            {
                string inputStr = baseString.Replace("\\r", "").Replace("\\n", "");
                byte[] arr = Convert.FromBase64String(inputStr);
                MemoryStream ms = new MemoryStream(arr);
                bmp = new Bitmap(ms);
            }
            catch (Exception ex)
            {
                return null;
                //MessageBox.Show("Base64StringToImage 转换失败\nException：" + ex.Message);
            }
            return bmp;
        }

    }


    /// <summary>
    /// 解析JSON，仿Javascript风格
    /// </summary>
    public static class JSON
    {
        public static T parse<T>(string jsonString)
        {
            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString)))
            {
                return (T)new DataContractJsonSerializer(typeof(T)).ReadObject(ms);
            }
        }

        public static string stringify(object jsonObject)
        {
            using (var ms = new MemoryStream())
            {
                new DataContractJsonSerializer(jsonObject.GetType()).WriteObject(ms, jsonObject);
                return Encoding.UTF8.GetString(ms.ToArray());
            }
        }
    }

    public static class SystemInfo
    {
        public static string IPAdress = "";
        public static string MacAdress = "";
        //public static string MachineName = "";
        public static string UserName = "";
        public static string OSVersion = "";
        //public static string SystemType = "";
        public static string CpuNum = "";
        public static string DiskID = "";
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

        public static void GetInfo()
        {
            MacAdress = GetMacAddress();
            IPAdress = Adress.IP;
            //MachineName = GetComputerName();
            UserName = GetUserName().Replace("\\", "-");
            OSVersion = GetOSVersion();
            //SystemType = GetSystemType();
            CpuNum = GetCpuInfo();
            DiskID = GetDiskID();
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
            DateTime dt = SystemInfo.GetNowTime();
            string nettime = dt.ToString(SystemConfig.DateFormatStr);
            if (nettime == "" || (nettime.Length > 4 && int.Parse(nettime.Substring(0, 4)) < 2017))
            {
                dt = SystemInfo.GetBeijingTime();
                nettime = dt.ToString(SystemConfig.DateFormatStr);
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
        private static DateTime GetTime(string timeStamp)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = long.Parse(timeStamp + "0000000");
            TimeSpan toNow = new TimeSpan(lTime); return dtStart.Add(toNow);
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
                if (!SystemInfo.IsConn())
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
                if (webcontent.StartsWith("ERROR:"))
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
                if (!SystemInfo.IsConn())
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
                if (webcontent.StartsWith("ERROR:"))
                {
                    return ai;
                }
                //CurrentTime = Convert.ToDateTime(webcontent.Substring(webcontent.LastIndexOf("当前时间：") + 5, webcontent.IndexOf("&nbsp") - webcontent.LastIndexOf("当前时间：") - 5).Trim());
                //< body style = "margin:0px" >< center > 您的IP是：[111.50.196.43] 来自：宁夏回族自治区银川市 移动</center></body></html>
                try
                {
                    int bidx = webcontent.LastIndexOf("<body>");
                    webcontent = webcontent.Substring(bidx, webcontent.LastIndexOf("</body>") - bidx);
                    int idx1 = webcontent.IndexOf("您的IP地址是");
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
                StreamReader streamReader = new StreamReader(streamReceive, Encoding.GetEncoding("UTF-8"));//GB2312
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
            //获取操作系统UserName
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
            string mip = "";
            try
            {
                List<string> iips = new List<string>();
                NetworkInterface[] interfaces = System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces();
                int len = interfaces.Length;
                for (int i = 0; i < len; i++)
                {
                    NetworkInterface ni = interfaces[i];
                    if (ni.Name.Contains("本地连接") || ni.Name.Contains("无线网络连接"))
                    {
                        IPInterfaceProperties property = ni.GetIPProperties();
                        foreach (UnicastIPAddressInformation ip in property.UnicastAddresses)
                        {
                            if (ip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                            {
                                mip = ip.Address.ToString();
                                iips.Add(mip);
                            }
                        }
                    }
                }
                mip = "";
                string actip = "";
                IPHostEntry IpEntry = Dns.GetHostEntry(Dns.GetHostName());
                foreach (IPAddress ipa in IpEntry.AddressList)
                {
                    if (ipa.AddressFamily == AddressFamily.InterNetwork)
                    {
                        mip = ipa.ToString();
                        if (iips.Contains(mip))
                        {
                            actip = mip;
                            break;
                        }
                    }
                }
                if (actip == "" && iips.Count > 0)
                {
                    actip = iips[0];
                }
                return actip;
                //return lstIPAddress;
                //获取IP地址 
                //string st = "";
                //ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
                //ManagementObjectCollection moc = mc.GetInstances();
                //foreach (ManagementObject mo in moc)
                //{
                //    if ((bool)mo["IPEnabled"] == true)
                //    {
                //        //st=mo["IpAddress"].ToString(); 
                //        System.Array ar;
                //        ar = (System.Array)(mo.Properties["IpAddress"].Value);
                //        st = ar.GetValue(0).ToString();
                //        //break;
                //    }
                //}
                //moc = null;
                //mc = null;
                //return st;
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
        ///   获取硬盘UserName     
        ///   </summary> 
        ///   <returns> string </returns> 
        public static string GetDiskID()
        {
            try
            {
                //获取硬盘UserName 
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
        //20171105 1.0.0.1 修复获取商铺列表错误信息
        //20180223 1.1.0.2 新增批量等于
        //20180308 1.2.0.1 起配送范围改代码
        //20180328 1.2.1.1 增加定时、写日志功能
        //20180622 1.2.1.2 增加复制店铺信息功能
        //20180809 1.2.1.3 修复获取上铺列表Bug（饿了么升级代码）
        //20180814 1.2.1.4 修改获取修改结果类
        //20181209 1.3.1.1 修复缓存登录、修复获取商铺列表
        //20190111 1.4.1.1 修改获取短信验证码登录
        //20190214 1.5.1.1 服务端验证（除店铺状态2个）；增加限制登录轩辕账户数；去掉推广
        //20190217 1.5.2.1 限制测试账户登录轩辕个数1个；增加保存分组ID
        //20190311 1.6.1.1 增加通知公告；优化软件效率；完善会话管理
        //20190812 1.6.1.2 修复获取IP逻辑
        //public static string QQ = "2089777820";
        public static string SOFTNAME = "EL_QPS_KGD";
        public static string SOFTNAMECN = "饿了么修改配送费起送价";
        public static string SOFTVERSION = "1.6.1.2";
        public static string SOFTDESC = "";
        public static string SOFTSUBKEY = "PDSM";
        public static string REGMNUM = "PUYW";//4WEI
        public static string OPTIONNAME = "OMNSDH";
        public static string UseTimesKey = "TMDOKDMS";
        public static string UseDaysKey = "DYESDSND";

        private static RegistryKey RgtKey = Registry.CurrentUser.OpenSubKey("SOFTWARE", true).CreateSubKey(SOFTNAME).CreateSubKey(SOFTSUBKEY);

        private static string actsoftname = "";
        public static string ACTSOFTNAME
        {
            get
            {
                if (actsoftname == "")
                {
                    if (Official)
                        actsoftname = SOFTNAMECN;
                    else
                        actsoftname = "试用中，如需开通会员, 请联系QQ: ";
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
                                actcnt = int.Parse(EncodeHelper.AesDecrypt(s1));
                            }
                            catch (Exception)
                            {
                            }
                        }
                    }
                    int actcnt2 = 0;
                    try
                    {
                        actcnt2 = int.Parse(EncodeHelper.AesDecrypt(Program.UseOptoin.PUYSDTWQ));
                    }
                    catch (Exception)
                    {
                    }
                    if (actcnt2 > actcnt)
                        actcnt = actcnt2;
                    actcnt++;
                    retkey.SetValue(UseTimesKey, EncodeHelper.AesEncrypt(actcnt.ToString()));
                    Program.UseOptoin.PUYSDTWQ = EncodeHelper.AesEncrypt(actcnt.ToString());
                }

                if (WriteUseLog)
                {
                    string nettime = SystemInfo.GetNetTime().ToString(SystemConfig.DateFormatStr);
                    Program.UseOptoin.UPS.Add(EncodeHelper.AesEncrypt(nettime));
                }
                Program.UseOptoin.SaveOptions();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool Regiser()
        {
            try
            {
                RegistryKey retkey = RgtKey.CreateSubKey(RegNum);
                string nettime = SystemInfo.GetNetTime().ToString(SystemConfig.DateFormatStr);
                retkey.SetValue(UseDaysKey, EncodeHelper.AesEncrypt(nettime));
                if (UseTimesCtl)
                    retkey.SetValue(UseTimesKey, EncodeHelper.AesEncrypt("0"));
                Program.UseOptoin.UPS = new List<string>();
                Program.UseOptoin.KDMSINDS = EncodeHelper.AesEncrypt(nettime);
                Program.UseOptoin.PUYSDTWQ = EncodeHelper.AesEncrypt("0");
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
            foreach (string strRNum in RgtKey.GetSubKeyNames())
            {
                if (strRNum == regnum)
                {
                    state = true;
                }
            }
            return state;
        }

        public static bool IsOverUseTimes()
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
                        actcnt = int.Parse(EncodeHelper.AesDecrypt(s1));
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            int actcnt2 = 0;
            try
            {
                actcnt2 = int.Parse(EncodeHelper.AesDecrypt(Program.UseOptoin.PUYSDTWQ));
            }
            catch (Exception)
            {
            }
            if (actcnt2 > actcnt)
                actcnt = actcnt2;
            if (actcnt > MaxUseTimes)
                return true;
            else
                return false;
        }

        public static bool IsOverUseDays()
        {
            RegistryKey retkey = RgtKey.CreateSubKey(RegNum);
            string actcnt = "";
            foreach (string strRNum in retkey.GetValueNames())
            {
                if (strRNum == UseDaysKey)
                {
                    try
                    {
                        string s1 = retkey.GetValue(strRNum).ToString();
                        actcnt = EncodeHelper.AesDecrypt(s1);
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            string actcnt2 = "";
            try
            {
                actcnt2 = EncodeHelper.AesDecrypt(Program.UseOptoin.KDMSINDS);
            }
            catch (Exception)
            {
            }
            DateTime dbtime = DateTime.Parse(actcnt2);
            DateTime wdtime = DateTime.Parse(actcnt);
            TimeSpan ts = dbtime - wdtime;
            if (ts.TotalMinutes > 0)
                actcnt = actcnt2;

            DateTime t1 = SystemInfo.GetNetTime();
            DateTime t2 = DateTime.Parse(actcnt);
            TimeSpan ts2 = t1 - t2;
            if (ts2.Days + 1 > MaxUseDays)
                return true;
            else
                return false;
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


    }
}
