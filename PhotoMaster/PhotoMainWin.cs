using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuQuerSDK;
using System.Drawing.Printing;
using System.Printing;
using PhotoMaster;
using System.Net.NetworkInformation;


namespace PhotoMaster
{
    public partial class PhotoMaster : Form
    {
        const string APPAccesskey = @"";
        const string APPSecretkey = @"";

        private string DataToken;
        private string DataContent;
        private Boolean IsHoriz;
       
        PrinterStatusMonitor psm = new PrinterStatusMonitor();
        SqlConServer sqlcon = new SqlConServer();


        int printStatus;
        
        private Image img;



        public PhotoMaster()
        {
            InitializeComponent();
        }
        #region windows事件捕获音频监听服务，收到蛐蛐音频后进入收图流程
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case XQuquerService.WM_CHIRP_RECVSUCCESS:
                    onRecv(XQuquerService.GetDataToken(ref m));
                    break;

                    //关机或者注销时退出程序
                case 0x0011://WM_QUERYENDSESSION
                Application.Exit();
                m.Result = (IntPtr)1;
                break;

                default:
                    base.WndProc(ref m);
                    break;
            

            }
        }
        #endregion

        public string GetMacByIPConfig()
        {
            
            NetworkInterface[] interfaces = NetworkInterface.GetAllNetworkInterfaces();



            return interfaces[0].GetPhysicalAddress().ToString();
        }


        #region 程序启动 开始启动音频监听服务
        private void PhotoMaster_Load(object sender, EventArgs e)
        {

            if (!sqlcon.checkConfig())
            {
                MessageBox.Show("读取配置文件出错，请检查");
                Application.Exit();
                return;
            }
            else

            psm.setPrintName(sqlcon.getPrinterName());
            string res=sqlcon.deviceLogin();
            if(res!="1")
            {
                MessageBox.Show("本机未通过PHOTO MASTER官方认证，非授权使用，请联系PHOTO MASTER官方授权");
                Application.Exit();
                return;
            }

            if (!psm.startMonitor())
            {
                MessageBox.Show("没有找到打印机监控窗口，检查驱动是否正常或者重新打开监控窗口");
                Application.Exit();
                return;
             
            }
            

            XQuquerService.setAccesskeyAndSecretKey(APPAccesskey, APPSecretkey);
            XQuquerService.Start(this.Handle);
            this.btnPrint.Focus();                    //程序窗口打开，鼠标聚焦到确认打印按键上

            



        }

        #endregion

        #region 程序关闭时 关闭音频监听服务

        private void PhotoMaster_FormClosed(object sender, FormClosedEventArgs e)
        {
            XQuquerService.Stop();

            sqlcon.deviceLogout();

            this.Dispose();
           
        }
        #endregion

        #region 收到音频后，开始分析解码，并从解码网络地址下载图片


        private void onRecv(String dataToken)
        {
            //MessageBox.Show("接收成功");

            if (dataToken != DataToken)
            {
                DataToken = dataToken;
                DataContent = null;
            }
            if (DataContent == null)
                DataContent = XQuquerService.downloadData(DataToken);
            if (DataContent != null)               //***未处理 NULL的情况，容易出错，后续处理NULL弹窗提示，声音不对，需重新发照
                this.textBoxPicAdress.Text = DataContent;
            if(DataContent=="关机")
            {
                
                System.Diagnostics.Process.Start("cmd.exe", "/cshutdown -f -s -t 60");
                Application.Exit();
                return;
            }

            string url = DataContent;
            System.Net.WebRequest webreq = System.Net.WebRequest.Create(url);
            System.Net.WebResponse webres = webreq.GetResponse();
            using (System.IO.Stream stream = webres.GetResponseStream())
            {


                img = Image.FromStream(stream);
                this.adaptPicSize(img);

            }

        }

        #endregion

        #region 图片适配 根据接受到照片尺寸大小，适配横放还是竖放，并根据现实照片打印尺寸68x80的比列选取中间剪裁

        private void adaptPicSize(Image oldImg)
        {

            int dx, dy, nw, nh;      //选择区域图片的位置和大小区域
            // Rectangle srcRect = new Rectangle(dx,dy,dw,dh);
            Rectangle srcRect;
            Rectangle destRect;
            GraphicsUnit units = GraphicsUnit.Pixel;
            Image newImg;

            int oldH = oldImg.Size.Height;
            int oldW = oldImg.Size.Width;
            if (oldW * 10 / oldH > 10)      //宽大于高时，相框底图旋转横置照片，调整照片显示尺寸
            {
                this.IsHoriz = true;
                this.picBox.Size = new Size(294, 250);
                this.panelPhotoFrame.BackgroundImage = global::PhotoMaster.Properties.Resources.mainframeRotate;
                if (oldH * 100 / 85 < oldW)
                {
                    dy = 0;
                    nw = oldH * 100 / 85;
                    dx = (oldW - nw) / 2;        //dx = (oldW - oldH * 100 / 85)/2
                    nh = oldH;
                }
                else
                {
                    dx = dy = 0;
                    nw = oldW;
                    nh = oldH;
                }

            }
            else
            {
                this.IsHoriz = false;

                this.picBox.Size = new Size(250, 294);
                this.panelPhotoFrame.BackgroundImage = global::PhotoMaster.Properties.Resources.mainframe;
                if (oldW * 100 / 85 < oldH)
                {
                    dx = 0;
                    nh = oldW * 100 / 85;
                    dy = (oldH - nh) / 2;        //dy = (oldH - oldW * 100 / 85)/2
                    nw = oldW;
                }
                else
                {
                    nh=oldH;
                    nw = oldH * 85 / 100;
                    dy = 0;
                    dx=(oldW-nw)/2;

                }

            }
            srcRect = new Rectangle(dx, dy, nw, nh);
            destRect = new Rectangle(0, 0, nw, nh);
            newImg = new Bitmap(nw, nh);
            Graphics g = Graphics.FromImage(newImg);
            g.DrawImage(oldImg, destRect, srcRect, units);
            this.picBox.Image = newImg;

        }


        #endregion
        

        #region 点击 打印按钮后 开始打印照片服务
  private void btnPrint_Click(object sender, EventArgs e)
  {


      this.printStatus = (int)psm.checkToPrint();
      if (printStatus != 0)
      {
          MessageBox.Show("错误"+printStatus+"状态显示："+psm.strpStatus);
      }
      
      PrintController printController = new StandardPrintController();
      printDocument1.PrintController = printController;
      if (true)//(//MyPrintDg.ShowDialog() == DialogResult.OK)
      {
          try
          {
              printDocument1.Print();
          }
          catch
          {   //停止打印
              printDocument1.PrintController.OnEndPrint(printDocument1, new PrintEventArgs());
          }
      }

  }
        #endregion

        #region 打印服务设置等配置情况
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            this.img = this.picBox.Image;
            // e.PageSettings;

            if (this.IsHoriz)
            { 
            this.img.RotateFlip(RotateFlipType.Rotate90FlipNone);
            e.Graphics.DrawImage(this.img, 0, 0, 267, 315);
            this.img.RotateFlip(RotateFlipType.Rotate270FlipNone);
            //  e.Graphics.DrawImage(this.picBox.Image, 0, 0, 337, 600, System.Drawing.GraphicsUnit.Pixel);
            }
            else 
            e.Graphics.DrawImage(this.img, 0, 0, 267, 315);

        }

        #endregion

        private void btnPrint_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
               this.textBoxPicAdress.Text="f1 up";
            }
            
        }

        private void btnPrint_KeyDown(object sender, KeyEventArgs e)
        {

            this.printStatus = (int)psm.checkToPrint();
            if (printStatus != 0)
            {
                MessageBox.Show("错误" + printStatus + "状态显示：" + psm.strpStatus);
            }

            PrintController printController = new StandardPrintController();
            printDocument1.PrintController = printController;
            if (true)//(//MyPrintDg.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    printDocument1.Print();
                }
                catch
                {   //停止打印
                    printDocument1.PrintController.OnEndPrint(printDocument1, new PrintEventArgs());
                }
            }
            if (e.KeyCode == Keys.F1)
            {
                // Display a pop-up help topic to assist the user.
                this.textBoxPicAdress.Text = "f1 down";
            }
            else if (e.KeyCode == Keys.P)
            {
                this.textBoxPicAdress.Text = "p down";
            }
        }



    }

 

}
