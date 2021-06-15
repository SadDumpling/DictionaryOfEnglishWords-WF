using Microsoft.Data.Sqlite;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DictionaryOfEnglishWords
{
    public partial class Form1 : Form
    {
        private const string name = "Dictionary Of English Words";
        public SqliteConnection connection = null;
        private readonly string BlackListSource = $"{Environment.CurrentDirectory}\\BlackList.json";
        public Form1()
        {
            InitializeComponent();
            MessageBox.Show("Добро пожаловать в персональный словарь. Заполняйте самостоятельно базу интересующими вас словами, проверяйте свои знания, убирая выученные слова.");
            this.Text = name;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load_1(object sender, EventArgs e)
        {

        }
        private async void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;

            await Task.Run(() =>
            {
                for (int i = 0; i <= 99; i++)
                {
                    this.Invoke(new Action(() =>
                    {
                        if (i == progressBar1.Maximum)
                        {
                            progressBar1.Maximum = i + 1;
                            progressBar1.Value = i + 1;
                            progressBar1.Minimum = i + 1;

                        }
                        else
                        {
                            progressBar1.Value = i + 1;
                        }
                        progressBar1.Value = i;
                        Thread.Sleep(10);
                    }));
                }
            });
            Form3 newForm3 = new Form3();

                newForm3.but1Fm1 = button1;
                newForm3.but2Fm1 = button2;
                newForm3.but3Fm1 = button3;
            try
            {
                newForm3.Show();
            }
            catch (Exception)
            {
                MessageBox.Show("Требуется перезапуск программы");
            }            
            progressBar1.Value = 0;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 newForm2 = new Form2();
            newForm2.Show();
            newForm2.but1Fm1 = button1;
            newForm2.but2Fm1 = button2;
            newForm2.but3Fm1 = button3;
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            File.Delete("BlackList.json");
            MessageBox.Show("Файл очищен");
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }
    }
}
