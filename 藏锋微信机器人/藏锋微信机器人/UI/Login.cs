using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace 藏锋微信机器人
{
    public partial class Login : MetroFramework.Forms.MetroForm
    {
        public robot wxRobot;
        System.Timers.Timer timer;
        public Login()
        {
            wxRobot = new robot();
            timer = new System.Timers.Timer(1000);
            timer.Elapsed += timer_Elapsed;
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            GetLoginQRCode();
            
            timer.Start();
        }

        public void ShowStatus(string text)
        {
            //无参数,但是返回值为bool类型
            this.Invoke(new Func<bool>(delegate()
            {
                metroLabel1.Text = text;
                return true; //返回值
            }));
        }

        void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            //耗时操作在后台进程进行
            Thread worker = new Thread(delegate()
            {
                var loginStatusStr = wxRobot.wait4login();
                
                
                ShowStatus(loginStatusStr);
            });
            worker.IsBackground = true;
            worker.Start();
        }

        private void GetLoginQRCode()
        {
            
            //获取uuid
            if (!wxRobot.get_uuid())
            {
                MessageBox.Show("登录失败：uuid获取失败");
            }
            else
            {
                //获取登录二维码
                pbxQRCode.BackgroundImage = wxRobot.gen_qr_code();
            }
        }
        int count = 0;
        private void metroLabel1_TextChanged(object sender, EventArgs e)
        {
            if (metroLabel1.Text == wxRobot.SUCCESS)
            {
                timer.Stop();
                if (wxRobot.login())
                {
                    Console.WriteLine("[INFO] Web WeChat login succeed .");
                }
                else
                {
                    ShowStatus("[ERROR] Web WeChat login failed .");
                    return;

                }
                if (count == 0)
                {
                    //初始化
                    if (wxRobot.init())
                    {
                        Console.WriteLine("[INFO] Web WeChat init succeed .");
                    }
                    else
                    {
                        ShowStatus("[INFO] Web WeChat init failed .");
                        return;
                    }

                    this.Hide();
                    Main m = new Main(wxRobot);
                    m.Show();
                }
                count++;
            }
        }
    }
}
