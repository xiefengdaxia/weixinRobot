using MetroFramework.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace 藏锋微信机器人
{
    public partial class Main : MetroFramework.Forms.MetroForm
    {
		string path = Application.StartupPath + "\\xf.ini";
		robot wxrb;
        public Main(robot rb)
        {
            wxrb = rb;
            wxrb.rTextbox=richTextBox1;
			wxrb.MsgEvent += new robot.MsgHandle(appendText);
			wxrb.bot = new bot();

			InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
			
			mcbTulingRobot.Checked = CSHelper.ReadINI("setup","robot_switch", path) == "1";
			txtApiKey.Text = CSHelper.ReadINI("setup", "tuling_key",  path);
			txtBotName.Text = CSHelper.ReadINI("setup", "bot_name", path);
			lbMeInfo.Text = wxrb._me.NickName + "\n\r" + wxrb._me.UserName;
            
            //System.Net.WebRequest webreq = System.Net.WebRequest.Create( wxrb._me.HeadImgUrl);
            //System.Net.WebResponse webres = webreq.GetResponse();
            //using(System.IO.Stream stream = webres.GetResponseStream())
            //{
            //    pbMeImg.Image = Image.FromStream(stream);
            //}
            //获取联系人
           wxrb.get_contact();
           for (int i = 0; i <wxrb.contact_list.Count; i++)
            {
                MetroTile tile = new MetroTile();
                tile.Width = 280;
                tile.Height = 60;
                tile.Name = wxrb.contact_list[i].UserName;
                var remarkName = wxrb.contact_list[i].RemarkName;
                var nickName = wxrb.contact_list[i].NickName;

				tile.Text = remarkName==""?nickName:remarkName;
                tile.Location = new Point(0, (tile.Height + 5) * i);
                tile.TileCount = i + 1;
				tile.Click += TileContactList_Click;
                metroPanel2.Controls.Add(tile);
            }
           backgroundWorker1.RunWorkerAsync();
        }

		private void TileContactList_Click(object sender, EventArgs e)
		{
			
		}

		private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dr = MessageBox.Show("您确定要退出吗？", "友情提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
				//释放窗体，不然会闪出两次退出
				Dispose();
				Application.Exit();
				e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {

                wxrb.proc_msg();
				
            }
            catch (Exception ex)
            {
                richTextBox1.AppendText(ex.Message + "\n\r" + ex.StackTrace);
            }
        }


		public void appendText(string msg)
		{
			try
			{
				
				msg = DateTime.Now.ToString("yyyy-MM-dd HH:mm:sss") + ":\n" + msg+"\n\r";
				if (richTextBox1 == null)
				{
					Console.WriteLine(msg);
				}
				else
				{
					//无参数,但是返回值为bool类型
					richTextBox1.Invoke(new Func<bool>(delegate ()
					{
						richTextBox1.AppendText(msg);
						return true; //返回值
					}));
				}
			}
			catch (Exception ex)
			{

			}
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			try
			{
				CSHelper.WriteINI("robot_switch",mcbTulingRobot.Checked?"1":"0" , path, "setup");
				CSHelper.WriteINI("tuling_key", txtApiKey.Text.Trim(), path,"setup");
				CSHelper.WriteINI("bot_name", txtBotName.Text.Trim(), path, "setup");
				MessageBox.Show("保存成功！");
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message + "\n\r" + ex.StackTrace);
			}

		}
	}
}
