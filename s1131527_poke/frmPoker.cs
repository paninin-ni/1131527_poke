using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace s1131527_poke
{
    public partial class frmPoker : Form
    {
        PictureBox[] pic = new PictureBox[5];

        int[] allPoker = new int[52];
        int[] playerPoker = new int[5];
        public frmPoker()
        {
            InitializeComponent();
            InitializePoker();
        }
        private void InitializePoker()
        {
            for (int i = 0; i < pic.Length; i++)
            {
                pic[i] = new PictureBox();
                pic[i].Image = GetImage("back");
                pic[i].Name = "pic" + i;
                pic[i].SizeMode = PictureBoxSizeMode.AutoSize;
                pic[i].Top = 30;
                pic[i].Left = 10 + ((pic[i].Width + 10) * i);
                pic[i].Visible = true;
                pic[i].Enabled = false;
                pic[i].Tag = "back";

                // 將 pic 丟至到 grpPorker 內
                this.grpPoker.Controls.Add(pic[i]);

                pic[i].Click += Pic_Click;
            }
        }

        private void Pic_Click(object sender, EventArgs e)
        {
            PictureBox pic = (PictureBox)sender;
            int index = int.Parse(pic.Name.Replace("pic", ""));
            // 如果 pic 的 Tag 為 back，則將顯示撲克牌
            if (pic.Tag.ToString() == "back")
            {
                pic.Tag = "front";
                pic.Image = GetImage("pic" + (playerPoker[index] + 1));
            }
            else
            {
                pic.Tag = "back";
                pic.Image = GetImage("back");
            }
        }

        private Image GetImage(string name)
        {
            return Properties.Resources.ResourceManager.GetObject(name) as Image;
        }
        
        private void grpPoker_Enter(object sender, EventArgs e)
        {

        }

        private async void btnDealCard_Click(object sender, EventArgs e)
        {
            // 先將牌面蓋掉
            for (int i = 0; i < pic.Length; i++)
            {
                pic[i].Image = GetImage("back");
            }
            // 初始化52張牌
            for (int i = 0; i < allPoker.Length; i++)
            {
                allPoker[i] = i;
            }
            // 洗牌
            Shuffle();
            await Task.Delay(500);

            // 發牌
            for (int i = 0; i < playerPoker.Length; i++)
            {
                pic[i].Image = GetImage("pic" + (allPoker[i] + 1));
                playerPoker[i] = allPoker[i];
            }

            for (int i = 0; i < pic.Length; i++)
            {
                pic[i].Enabled = true;
                pic[i].Tag = "front";
            }

            btnChangeCard.Enabled = true;
        }
        private void Shuffle()
        {
            Random rand = new Random();
            for (int i = 0; i < allPoker.Length; i++)
            {
                int r = rand.Next(allPoker.Length);
                int temp = allPoker[r];
                allPoker[r] = allPoker[0];
                allPoker[0] = temp;
            }
        }
    }
}
