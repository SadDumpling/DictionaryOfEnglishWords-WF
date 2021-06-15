using Microsoft.Data.Sqlite;
using System;
using System.Windows.Forms;

namespace DictionaryOfEnglishWords
{
    public partial class Form2 : Form
    {
        public Button but1Fm1;
        public Button but2Fm1;
        public Button but3Fm1;
        public SqliteConnection connection;
        public Form2()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text == "" || textBox2.Text == "")
            {
                MessageBox.Show("Необходимо заполнить поля слово на английском и слово на русском");
            }
            else
            {
                try
                {
                    var connection = new SqliteConnection("Data Source=Dictionary.db");
                    connection.Open();
                    SqliteCommand command = new SqliteCommand();
                    command.Connection = connection;
                    command.CommandText = $"INSERT INTO Words (EngName, RusName, Context, ImageLink) VALUES ('{textBox1.Text}','{textBox2.Text}','{textBox3.Text}','{textBox4.Text}')";
                    command.ExecuteNonQuery();
                    MessageBox.Show("Слово добавлено!");
                    textBox1.Text = "";
                    textBox2.Text = "";
                    textBox3.Text = "";
                    textBox4.Text = "";
                }
                catch (Exception)
                {
                    MessageBox.Show("Проблемы доступа к базе данных. Проверьте наличие БД в корневом каталоге");
                }
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            but1Fm1.Enabled = true;
            but2Fm1.Enabled = true;
            but3Fm1.Enabled = true;
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
