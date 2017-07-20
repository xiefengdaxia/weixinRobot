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
        robot wxrb;
        public Main(robot rb)
        {
            wxrb = rb;
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
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
                metroPanel2.Controls.Add(tile);
            }
           backgroundWorker1.RunWorkerAsync();
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dr = MessageBox.Show("您确定要退出吗？", "友情提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                //释放窗体，不然会闪出两次退出
                //Dispose();
                //Application.Exit();
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
    }
}
