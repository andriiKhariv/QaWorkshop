using System;
using System.IO;
using System.Windows.Forms;


namespace tspp3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            textBox1.AcceptsTab = true;
            openFileDialog1.Filter = "Text files(*.txt)|*.txt";
            saveFileDialog1.Filter = "Text files(*.txt)|*.txt";

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            fileToolStripMenuItem.Enabled = false;
            infoToolStripMenuItem.Enabled = false;
            tabControl1.Visible = false;
        }
        Form2 form2 = new Form2();
        private void ToolStripComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ToolStripComboBox.Text == "Режим роботи адміністратора")
            {
                textBox1.Text = "";
                dataGridView1.Rows.Clear();
                form2.button1.Click += (senderForm, eForm) =>
                {
                    ButtonClick();
                };
                form2.Show();
            }
            else
            {
                infoToolStripMenuItem.Enabled = true;
                tabControl1.Visible = true;
            }
        }
        public void ButtonClick()
        {
            if (form2.maskedTextBox1.Text == "1221pas")
            {
                this.fileToolStripMenuItem.Enabled = true;
                this.infoToolStripMenuItem.Enabled = true;
                this.tabControl1.Visible = true;
                form2.Close();
            }
            else
            {
                MessageBox.Show("Пароль введено неправильно!");
            }
        }

        private void DataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.ReadOnly = true;
            dataGridView1.ReadOnly = true;
            if (tabControl1.SelectedTab == tabControl1.TabPages["tabPage1"])
            {
                if (openFileDialog1.ShowDialog() == DialogResult.Cancel) return;
                string fileName = openFileDialog1.FileName;

                if (fileName != @"C:\Users\User\Desktop\products_1.txt")
                {
                    MessageBox.Show("Файл недоступний!");
                }
                else
                {
                    string fileText = File.ReadAllText(fileName);
                    textBox1.Text = fileText;
                }
            }
            else
            {
                
                if (openFileDialog1.ShowDialog() == DialogResult.Cancel) return;
                string fileName = openFileDialog1.FileName;
                string[] lines = new string[fileName.Length];
                if (fileName != @"C:\Users\User\Desktop\products_2.txt")
                {
                    MessageBox.Show("Файл недоступний!");
                }
                else
                {
                    lines = File.ReadAllLines(fileName);
                }

                string[] values;
                for (int i = 0; i < lines.Length; i++)
                {
                    values = lines[i].ToString().Split('\t');

                    string[] row = new string[values.Length];
                    for (int j = 0; j < values.Length; j++)
                    {
                        row[j] = values[j].Trim();
                    }
                    dataGridView1.Rows.Add(row);
                }

            }
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (Convert.ToInt32(row.Cells[2].Value) != 0)
                {
                    row.Cells[3].Value = true; 
                }
                else {
                    row.Cells[3].Value = false;  
                }
                
            }
        }

        private void EditToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.ReadOnly = false;
            button1.Enabled = true;
            dataGridView1.ReadOnly = false;
        }

        private void TextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.S)
            {
                if (saveFileDialog1.ShowDialog() == DialogResult.Cancel) return;
                string filename = saveFileDialog1.FileName;
                File.WriteAllText(filename, textBox1.Text);
                MessageBox.Show("File is saved!");
            }

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel) return;
            string filename = saveFileDialog1.FileName;
            string result = "";
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                for (int j = 0; j < dataGridView1.Columns.Count - 1; j++)
                {
                    result += dataGridView1.Rows[i].Cells[j].Value.ToString() + "\t";
                }
                result += "\n";
            }
            File.WriteAllText(filename, result);
            MessageBox.Show("File is saved!");
        }

        private void DataInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.ReadOnly = true;
            dataGridView1.ReadOnly = true;
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel) return;
            string fileName = openFileDialog1.FileName;
            
            if (fileName != @"C:\Users\User\Desktop\products_1.txt")
            {
                MessageBox.Show("Файл недоступний!");
            } else
            {
                string fileText = File.ReadAllText(fileName);
                textBox1.Text = fileText;
            }
            
        }

        private void DataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (Convert.ToInt32(row.Cells[2].Value) != 0)
                {
                    row.Cells[3].Value = true;
                }
                else { row.Cells[3].Value = false; }
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Escape)
            {
                Exit();
            }
        }
        public void Exit()
        {
            DialogResult res = MessageBox.Show("Exit?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (res == DialogResult.Yes)
            {
                ToolStripComboBox.Text = "";
                fileToolStripMenuItem.Enabled = false;
                infoToolStripMenuItem.Enabled = false;
                tabControl1.Visible = false;
            }
        }

        private void TabControl1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Exit();
            }
        }
    }
}
