using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace lab2
{
    public partial class bai3 : Form
    {
        public bai3()
        {
            InitializeComponent();
        }
        private void btread_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.ShowDialog();
                FileStream fs = new FileStream(ofd.FileName, FileMode.Open);
                StreamReader sr = new StreamReader(fs);
                string content = sr.ReadToEnd();
                sr.Close();
                fs.Close();

                // Tách các phép tính thành các phần riêng biệt bằng ký tự xuống dòng (\n)
                string[] calculations = content.Split('\n');

                // Tạo một chuỗi mới để hiển thị các phép tính với dạng đúng định
                string formattedContent = string.Join("\r\n", calculations);

                // Hiển thị chuỗi đã được định dạng lại trên textBox1
                textBox1.Text = formattedContent;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Vui lòng chọn file");
            }
        }

       
        private void btghi_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            // openFileDialog.ShowDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                decimal result;
                string input = openFileDialog.FileName;
                string outputFile = openFileDialog.FileName.Replace(".txt", "_output.txt");
                try
                {
                    string[] lines = File.ReadAllLines(input);

                    using (StreamWriter writer = new StreamWriter(outputFile, false))
                    {
                        foreach (string line in lines)
                        {
                            string[] parts = SplitLine(line);

                            List<decimal> operands = new List<decimal>();
                            List<char> operators = new List<char>();

                            foreach (string part in parts)
                            {
                                if (decimal.TryParse(part, out decimal operand))
                                {
                                    operands.Add(operand);
                                }
                                else if (IsOperator(part))
                                {
                                    operators.Add(part[0]);
                                }
                            }

                            if (operands.Count == operators.Count + 1)
                            {
                                result = EvaluateExpression(operands, operators);
                                writer.WriteLine($"{line}= {result}");
                            }
                            else
                            {
                                MessageBox.Show("Dữ liệu đầu vào không hợp lệ!");
                            }
                        }
                    }
                    string resultText = File.ReadAllText(outputFile);
                    rtbnhap.Text = resultText;
                    //rtbnhap.Text = result.ToString();

                    //MessageBox.Show("Vui lòng kiểm tra kết quả trong 'output_bai3.txt'", "Tính toán xong", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}");
                }

            }


        }


        private string[] SplitLine(string line)
        {
            // Regex pattern to match numbers and operators
            Regex regex = new Regex("[0-9]+|[+\\-\\*/]");

            return regex.Matches(line).Select(m => m.Value).ToArray();
        }

        private bool IsOperator(string value)
        {
            return value == "+" || value == "-" || value == "*" || value == "/";
        }
        private decimal EvaluateExpression(List<decimal> operands, List<char> operators)
        {
            if (operators.Contains('*') || operators.Contains('/'))
            {
                while (operators.Contains('*') || operators.Contains('/'))
                {
                    int index = operators.FindIndex(op => op == '*' || op == '/');

                    decimal operand1 = operands[index];
                    decimal operand2 = operands[index + 1];
                    char op = operators[index];

                    decimal result = 0;

                    if (op == '*')
                    {
                        result = operand1 * operand2;
                    }
                    else if (op == '/')
                    {
                        if (operand2 != 0)
                        {
                            result = operand1 / operand2;
                        }
                        else
                        {
                            MessageBox.Show("Không thể chia cho 0!");
                            return decimal.MinValue;
                        }
                    }

                    operands[index] = result;
                    operands.RemoveAt(index + 1);
                    operators.RemoveAt(index);
                }
            }

            decimal finalResult = operands[0];

            for (int i = 1; i < operands.Count; i++)
            {
                char op = operators[i - 1];
                decimal operand = operands[i];

                if (op == '+')
                {
                    finalResult += operand;
                }
                else if (op == '-')
                {
                    finalResult -= operand;
                }
            }

            return finalResult;
        }

        private void rtbkq_TextChanged(object sender, EventArgs e)
        {

        }

        private void rtbnhap_TextChanged(object sender, EventArgs e)
        {

        }

        private void btd_Click(object sender, EventArgs e)
        {
            rtbnhap.Text = "";
            textBox1.Text = "";
        }

        private void bte_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

       
    }
}
