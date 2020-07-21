using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CPUAffinity
{
    public partial class Form1 : Form
    {
        List<CheckBox> checkBoxes = new List<CheckBox>();

        public Form1()
        {
            int left = 10;
            int top = 10;

            for (int c = 0; c < 64; c++)
            {
                CheckBox box = new CheckBox();
                box.Text = "CPU " + c;
                box.Location = new System.Drawing.Point(left, top);
                box.CheckedChanged += new System.EventHandler(this.changed);
                checkBoxes.Add(box);

                top += 22;

                if ((c + 1) % 8 == 0 && c != 0)
                {
                    top = 10;
                    left += 110;
                }
                this.Controls.Add(box);
            }


            InitializeComponent();
        }

        private void changed(object sender, EventArgs e)
        {
            calculateAffinity();
        }

        private void calculateAffinity()
        {
            int mask_low = 0;
            int mask_high = 0;

            for (int c = 0; c < 64; c++)
            {
                if (checkBoxes[c].Checked)
                {
                    Console.WriteLine("been here " + c);
                    if (c < 32)
                    {
                        mask_low = mask_low | (1 << c);
                    }
                    else
                    {
                        mask_high = mask_high | (1 << c);
                    }
                }
            }

            textBox1.Text = "0x" + long2hex(mask_high).ToUpper() + long2hex(mask_low).ToUpper();
        }

        private string pad0(string num, int width)
        {
            string zeros = "";
            for (int i = 0; i < width; i++)
            {
                zeros += "0";
            }

            string zeronum = (zeros + num);
            return zeronum.Substring(zeronum.Length - width);
        }

        private string long2hex(int num)
        {
            return pad0(Convert.ToString((int)((uint)num >> 24), 16), 2) +
                    pad0(Convert.ToString(num >> 16 & 255, 16), 2) +
                    pad0(Convert.ToString(num >> 8 & 255, 16), 2) +
                    pad0(Convert.ToString(num & 255, 16), 2);
        }
    }
}