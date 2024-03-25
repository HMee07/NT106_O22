using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
// Thu ne

namespace lab1
{
    public partial class bai2 : Form
    {
        public bai2()
        {
            InitializeComponent();
        }

        private void btDel_Click(object sender, EventArgs e)
        {
            tbMax.Text = "";
            tbMin.Text = "";
            tbs1.Text = "";
            tbs2.Text = "";
            tbs3.Text = "";
        }

        private void btExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tbs1_TextChanged(object sender, EventArgs e)
        {
            double txt;
            bool so = Double.TryParse(tbs1.Text, out txt);
            if (so == false && tbs1.Text != "" && tbs1.Text != "-")
            {     
                MessageBox.Show("Vui lòng nhập đúng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tbs1.Text = "";
            }
            //if(tbs1.Text.Contains(','))
            //{
            //    MessageBox.Show("Vui lòng sử dụng dấu chấm '.' cho số thập phân.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    tbs1.Text = "";
            //}    
        }

        private void tbs2_TextChanged(object sender, EventArgs e)
        {

            double txt;
            bool so = Double.TryParse(tbs2.Text, out txt);
            if (so == false && tbs2.Text!= "" && tbs2.Text != "-")
            {
                MessageBox.Show("Vui lòng nhập đúng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tbs2.Text = "";
            }
            //if (tbs2.Text.Contains(','))
            //{
            //    MessageBox.Show("Vui lòng sử dụng dấu chấm '.' cho số thập phân.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    tbs1.Text = "";
            //}

        }

        private void tbs3_TextChanged(object sender, EventArgs e)
        {
            string text = tbs3.Text.Replace(",", ".");

            double txt;
            bool so = Double.TryParse(text, out txt);
            if (so == false && tbs3.Text != "" && tbs3.Text != "-")
            {
                MessageBox.Show("Vui lòng nhập đúng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tbs3.Text = "";
            }
            //if (tbs3.Text.Contains(','))
            //{
            //    MessageBox.Show("Vui lòng sử dụng dấu chấm '.' cho số thập phân.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    tbs1.Text = "";
            //}
        }

        private void btTim_Click(object sender, EventArgs e)
        {
            //tbs1.Text = tbs1.Text.Replace(',', '.');
            //tbs2.Text = tbs2.Text.Replace(',', '.');
            //tbs3.Text = tbs3.Text.Replace(',', '.');

            double num1 = 0, num2 = 0, num3 = 0, Max = 0, Min = 0;
            num1=Convert.ToDouble(tbs1.Text.Replace(',', '.'));
            num2=Convert.ToDouble(tbs2.Text.Replace(',', '.'));
            num3=Convert.ToDouble(tbs3.Text.Replace(',', '.'));
            Max=Fmax(num1, num2, num3);
            Min=Fmin(num1, num2, num3);
            tbMax.Text = Convert.ToString(Max);
            tbMin.Text = Convert.ToString(Min);
        }

        private double Fmax(double num1, double num2, double num3) 
        {
            return Math.Max(num1, Math.Max(num2 , num3));
        }

        private double Fmin(double num1, double num2, double num3)
        {
            return Math.Min(num1, Math.Min(num2 , num3));
        }
    }
}

