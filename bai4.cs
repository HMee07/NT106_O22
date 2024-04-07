using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Linq;
namespace lab2
{
    public partial class bai44 : Form
    {
        public bai44()
        {
            InitializeComponent();
        }

        private List<string> dataList = new List<string>();

        private void Bai4_Load(object sender, EventArgs e)
        {
            tbMSSV.Enabled = false;
            tbHoTen.Enabled = false;
            tbDienThoai.Enabled = false;
            tbDiemVan.Enabled = false;
            tbDiemToan.Enabled = false;
            tbInput.Enabled = false;
            tbOuput.Enabled = false;
            btnWriteInput.Enabled = false;
            btnRead.Enabled = false;
        }

        private void Nhap()
        {

        }
        private bool CheckInput()
        {
            return float.TryParse(tbDiemToan.Text, out _)
                && float.TryParse(tbDiemVan.Text, out _)
                && tbDienThoai.Text != ""
                && tbHoTen.Text != ""
                && tbMSSV.Text != "";



        }
        private void btnWriteInput_Click(object sender, EventArgs e)
        {

            if (dataList.Count > 0)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Text Files (*.txt)|*.txt";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = saveFileDialog.FileName;

                    try
                    {
                        using (StreamWriter sw = new StreamWriter(filePath))
                        {
                            foreach (string data in dataList)
                            {
                                sw.WriteLine(data);
                            }
                        }

                        tbPathInput.Text = saveFileDialog.FileName;
                        tbInput.Text = File.ReadAllText(saveFileDialog.FileName);
                        MessageBox.Show("Data has been written to the file successfully!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"An error occurred: {ex.Message}", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
                    }
                }
            }


            else
            {
                MessageBox.Show("Xin Nhập đủ và đúng định dạng thông tin", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
                return;
            }
            if (tbNumOfSV.Text == "0")
            {
                bntBatDauNhap.Enabled = true;

                tbNumOfSV.Enabled = true;
                tbMSSV.Enabled = false;
                tbHoTen.Enabled = false;
                tbDienThoai.Enabled = false;
                tbDiemVan.Enabled = false;
                tbDiemToan.Enabled = false;

                btnWriteInput.Enabled = false;
                btnRead.Enabled = true;
                BeNothing();
            }

        }
        private void BeNothing()
        {
            tbDiemToan.Text = "";
            tbDiemVan.Text = "";
            tbDienThoai.Text = "";
            tbHoTen.Text = "";
            tbMSSV.Text = "";

        }
        private void bntBatDauNhap_Click(object sender, EventArgs e)
        {


            if (int.TryParse(tbNumOfSV.Text, out _))
            {
                bntBatDauNhap.Enabled = false;
                tbNumOfSV.Enabled = true;
                tbMSSV.Enabled = true;
                tbHoTen.Enabled = true;
                tbDienThoai.Enabled = true;
                tbDiemVan.Enabled = true;
                tbDiemToan.Enabled = true;
                tbInput.Enabled = true;
                tbOuput.Enabled = true;
                btnWriteInput.Enabled = true;
                btnRead.Enabled = false;
                tbNumOfSV.Enabled = false;
            }
            else
            {
                MessageBox.Show("Xin Nhập số lượng sinh viên", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
            }
            if (tbNumOfSV.Text == "0")
            {
                bntBatDauNhap.Enabled = true;

                tbNumOfSV.Enabled = true;
                tbMSSV.Enabled = false;
                tbHoTen.Enabled = false;
                tbDienThoai.Enabled = false;
                tbDiemVan.Enabled = false;
                tbDiemToan.Enabled = false;

                btnWriteInput.Enabled = false;
                btnRead.Enabled = true;
                BeNothing();
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            tbPathOutput.Text = "";
            tbPathInput.Text = "";
            tbInput.Text = "";
            tbOuput.Text = "";
            BeNothing();
            tbMSSV.Enabled = false;
            tbHoTen.Enabled = false;
            tbDienThoai.Enabled = false;
            tbDiemVan.Enabled = false;
            tbDiemToan.Enabled = false;
            btnWriteInput.Enabled = false;
            btnRead.Enabled = false;
            tbNumOfSV.Enabled = true;
            bntBatDauNhap.Enabled = true;
        }

        private void btnRead_Click(object sender, EventArgs e)

        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string content = File.ReadAllText(openFileDialog.FileName);
                content = content.Replace("Họ và tên: ", "");
                content = content.Replace("MSSV: ", "");
                content = content.Replace("Số điện thoại: ", "");
                content = content.Replace("Điểm toán: ", "");
                content = content.Replace("Điểm văn: ", "");

                string[] Lines = content.Split(new string[] { "\r\n" }, StringSplitOptions.None);
                if (Lines.Length != 0 && Lines[0] != "")
                {
                    for (int i = 0; i < Lines.Length; i++)
                    {
                        if (Lines[i] == "" && float.TryParse(Lines[i - 1], out _)
                                           && float.TryParse(Lines[i - 2], out _)
                                           && float.TryParse(Lines[i - 3], out _)
                                           && float.TryParse(Lines[i - 4], out _)
                                           && !float.TryParse(Lines[i - 5], out _))
                        {
                            Lines[i] = ((float.Parse(Lines[i - 1]) + float.Parse(Lines[i - 2])) / 2).ToString();
                            i++;
                            Lines[i - 6] = $"Họ và tên: {Lines[i - 6]}";
                            Lines[i - 5] = $"MSSV: {Lines[i - 5]}";
                            Lines[i - 4] = $"Số điện thoại: {Lines[i - 4]}";
                            Lines[i - 3] = $"Điểm toán: {Lines[i - 3]}";
                            Lines[i - 2] = $"Điểm văn: {Lines[i - 2]}";
                            Lines[i - 1] = $"Trung bình: {Lines[i - 1]}\r\n";
                        }
                    }



                    string outputFile = openFileDialog.FileName.Replace(".txt", "_output.txt");
                    File.AppendAllLines(outputFile, Lines);
                    tbPathOutput.Text = outputFile;
                    tbOuput.Text = File.ReadAllText(outputFile);


                }
            }
        }

        private bool MustBeNum(char num)
        {
            return char.IsDigit(num) || num == '.';
        }
        private bool MustBeLetter(char letter)
        {
            return char.IsLetter(letter) || char.IsWhiteSpace(letter);
        }
        private void tbMSSV_TextChanged(object sender, EventArgs e)
        {
            foreach (char item in tbMSSV.Text)
            {
                if (!MustBeNum(item)) { MessageBox.Show("MSSV phải là số", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1); tbMSSV.Text = ""; return; }
            }
        }

        private void tbHoTen_TextChanged(object sender, EventArgs e)
        {
            foreach (char item in tbHoTen.Text)
            {
                if (!MustBeLetter(item)) { MessageBox.Show("Tên không có số", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1); tbHoTen.Text = ""; return; }
            }
        }

        private void tbDienThoai_TextChanged(object sender, EventArgs e)
        {
            foreach (char item in tbDienThoai.Text)
            {
                if (!MustBeNum(item)||!tbDienThoai.Text.StartsWith("0")) { MessageBox.Show("Số điện thoại phải là số", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1); tbDienThoai.Text = ""; return; }
            }
        }

        private void tbDiemToan_TextChanged(object sender, EventArgs e)
        {
            
                foreach (char item in tbDiemToan.Text)
                {
                    if (!MustBeNum(item))
                    { MessageBox.Show("Điểm toán phải là số", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1); 
                    tbDiemToan.Text = ""; return; }
                }
        }

        private void tbDiemVan_TextChanged(object sender, EventArgs e)
        {
            foreach (char item in tbDiemVan.Text)
            {
                if (!MustBeNum(item))
                { MessageBox.Show("Điểm văn phải là số", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    tbDiemVan.Text = ""; return; }
            }

            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //        string[] content = { $"Họ và tên: {tbHoTen.Text}", $"MSSV: {tbMSSV.Text}", $"Số điện thoại: {tbDienThoai.Text}", $"Điểm toán: {tbDiemToan.Text}", $"Điểm văn: {tbDiemVan.Text}", "\r\n" };
            if (tbDienThoai.Text.Length != 10) 
            {   
                MessageBox.Show("Số điện thoại phải có 10 chữ số", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1); 
                tbDienThoai.Text = "";
                return; 
            }
            float toan = float.Parse(tbDiemToan.Text);
            if(toan<0||toan>10)
            {
                MessageBox.Show("Điểm toán từ 0-10", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                tbDiemToan.Text = "";
                return;
            }
            float van = float.Parse(tbDiemVan.Text);
            if (van < 0 || van > 10)
            {
                MessageBox.Show("Điểm văn từ 0-10", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                tbDiemVan.Text = "";
                return;
            }

            string newsv = $"Họ và tên: {tbHoTen.Text}\r\nMSSV: {tbMSSV.Text}\r\nSố điện thoại: {tbDienThoai.Text}\r\nĐiểm toán: {tbDiemToan.Text}\r\nĐiểm văn: {tbDiemVan.Text}\r\n";
            dataList.Add(newsv);
            tbNumOfSV.Text = (int.Parse(tbNumOfSV.Text) - 1).ToString();
            BeNothing();
        }

        private void btnexit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
