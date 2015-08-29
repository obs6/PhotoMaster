using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace PhotoMaster
{
    public class SqlConServer
    {
        //网卡mac地址用于验证设备登陆id
        private string macAdress;
        
        //sql服务器地址
        private string sqlServAdress;
        //提交请求消息类型标志位
        private string msgtype;

        private string configPath="";
        private string configini="\\pmconfig.ini";

        internal static class NativeMethods
        {
            [DllImport("kernel32", CharSet = CharSet.Unicode )]
            public static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);
        }

        public SqlConServer()
        {
            this.macAdress = this.GetMacByIPConfig();
            this.configPath = System.Windows.Forms.Application.StartupPath;//获取当前根目录
        }

        public Boolean checkConfig()
        {
            
            StringBuilder temp = new StringBuilder(100);
            NativeMethods.GetPrivateProfileString("sys", "serverurl", "读取出错", temp, 100, configPath + configini);

            //读取的serverurl 为空0或者读取出错为4的时候 报错
            if (temp.Length != 0 && !temp.ToString().Equals("读取出错"))
            {
                this.sqlServAdress = temp.ToString() + "saedb/conclient_saemysql.php";
                return true;
            }
            else
                return false;
                
            
        }
        public string getPrinterName()
        {
            StringBuilder temp = new StringBuilder(20);
            NativeMethods.GetPrivateProfileString("sys", "printdevice", "读取出错", temp, 20, configPath + configini);
            

            if (temp.Length != 0 && !temp.ToString().Equals("读取出错"))
            {
                
                return temp.ToString();
            }
            else
                return "打印机名读取出错";
        }




        private string GetMacByIPConfig()
        {

            NetworkInterface[] interfaces = NetworkInterface.GetAllNetworkInterfaces();
            //返回第一张最靠前的网卡mac
            return interfaces[0].GetPhysicalAddress().ToString();                           
        }

        public string deviceLogin()
        {
            this.msgtype = "?msgtype=login&mac="+this.macAdress;

            return this.runSqlServer(this.msgtype);


        }
        public void deviceLogout()
        {
            this.msgtype = "?msgtype=logout&mac=" + this.macAdress;
            this.runSqlServer(this.msgtype);


        }
        public void printPhotoSql()
        {
            this.msgtype = "?msgtype=printevent&mac=" + this.macAdress;
            this.runSqlServer(this.msgtype);
            
        }

        private String runSqlServer(string msg)
        {
            WebClient wc = new WebClient();
            string reslut="";
            try
            {
                reslut = wc.DownloadString(this.sqlServAdress+msg);

            }
            catch (Exception ex)
            {
                return ex.ToString();

               
            }

            return reslut;
        }


    }
}
