using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace PhotoMaster
{
    class PrinterStatusMonitor                  //打印机状态监控类
    {
        private IntPtr parentWinHandle;          //父窗口即epson mointor窗口 句柄
        private IntPtr statusTextWinHandle;       //父窗口内 显示打印机状态的文本框 句柄
        private IntPtr btnWinHandle;              //窗口上 按键的句柄
        private string prWinName;
        public StringBuilder strBpStatus=new StringBuilder(30);
        public string strpStatus;
        private string strReadyToPr = "准备就绪";
        private string strOutOfPaper = "缺纸或装纸不正确";
        const int WM_CLICK = 0x00F5;          //windows 点击事件

        internal static class NativeMethods
        {
            [DllImport("User32.dll", EntryPoint = "FindWindow", CharSet = CharSet.Unicode)]
            public extern static IntPtr FindWindow(string lpClassName, string lpWindowName);
            [DllImport("User32.dll", EntryPoint = "FindWindowEx", CharSet = CharSet.Unicode)]
            public static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpClassName, string lpWindowName);
            [DllImport("user32.dll", CharSet = CharSet.Unicode)]
            public extern static int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);
            [DllImport("User32.dll", EntryPoint = "SendMessage", CharSet = CharSet.Unicode)]
            public static extern int SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, string lParam);
            
        }



        public void setPrintName(string name)
        {
            switch (name)
            {
                case "epsonr330":
                    this.prWinName = "EPSON Status Monitor 3 : EPSON R330 Series";
                    break;
                default:
                    this.prWinName = "EPSON Status Monitor 3 : EPSON R330 Series";
                    break;


            }
        }
        public Boolean openMonitor()
        {
           // NativeMethods.FindWindow(null, "EPSON R330 Series")
            return true;
        }
        public Boolean startMonitor()
        {
            this.parentWinHandle = NativeMethods.FindWindow(null, this.prWinName);
            if (parentWinHandle != IntPtr.Zero)
            {
                this.statusTextWinHandle= NativeMethods.FindWindowEx(parentWinHandle, IntPtr.Zero, "static", "准备就绪");

                return true;
            }
                
            else return false;
        }

        #region 检查是否有纸，有纸返回true，目前常，等以后改进
        
        private Boolean checkPaper()               
        {
            return true;
        }
        #endregion
        private Boolean findWndPrinting()
        {
            if (NativeMethods.FindWindow(null, "EPSON R330 Series - USB001") != IntPtr.Zero)
                return true;
            return false;
        }
        
        private Boolean findMonitor()
        {
            
            if (parentWinHandle != IntPtr.Zero)
                return true;

            else if (this.findWndPrinting())
                return true;
            else return false;
        }

        public enum PrintStatus
        {
            finetoprint = 0,
            outofpaper = 1,
            cantfindmonitor = 2,
            printing=3,
            othertrouble=4,

        };

        public PrintStatus checkToPrint()
        {
            if(!this.checkPaper())
             return PrintStatus.outofpaper;
            if (!this.findMonitor())
                return PrintStatus.cantfindmonitor;

            int i = NativeMethods.GetWindowText(this.statusTextWinHandle, this.strBpStatus, 30);

            this.strpStatus = this.strBpStatus.ToString();

            if (this.strpStatus.Equals(this.strOutOfPaper))      //目前没装检查纸张情况下，监控显示缺纸时，按键取消恢复正常状态
            {
                this.clickBtn("取消");
                return PrintStatus.finetoprint;
            }
            if (!strpStatus.Equals(strReadyToPr))
            {
                if(this.findWndPrinting())
                {
                    return PrintStatus.finetoprint;           //正在打印的情况下也是可以正常提交打印
                }
                return PrintStatus.othertrouble;
            }
            else return PrintStatus.finetoprint;

        }
        public void clickBtn(string btnName)
        {
            
            this.btnWinHandle=NativeMethods.FindWindowEx(this.parentWinHandle, IntPtr.Zero, "Button", btnName);
            NativeMethods.SendMessage(btnWinHandle, WM_CLICK, (IntPtr)0, "0");
        }

    }
}
