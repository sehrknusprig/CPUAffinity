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
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            for (int c = 0; c < 64; c++)
            {
                checkedListBox1.Items.Add("CPU " + c);
            }
        }

        private void checkedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            this.BeginInvoke(new Action(() =>
            {
                calculateAffinity();
            }));
        }

        private void calculateAffinity()
        {
            int mask_low = 0;
            int mask_high = 0;

            for (int c = 0; c < checkedListBox1.Items.Count; c++)
            {
                if (checkedListBox1.GetItemChecked(c))
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

        private void checkedListBox1_SelectedValueChanged(object sender, EventArgs e)
        {
        }
    }
}

