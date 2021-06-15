using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace DictionaryOfEnglishWords
{

    public partial class Form3 : Form
    {
        public Button but1Fm1;
        public Button but2Fm1;
        public Button but3Fm1;
        public SqliteConnection connection;
        SqliteCommand command;
        private bool findRandomNumber = false;
        private bool findRepeat = false;
        private readonly string BlackListSource = $"{Environment.CurrentDirectory}\\BlackList.json";
        private List<string> blackListNumbers;
        private string newExeptionWord;
        private string serialized;
        int ramdomInt;
        JsonHelper jsonSave;
        List<string[]> data = new List<string[]>();
        public class JsonHelper
        {
            public List<BlockedWords> blockedWords { get; set; }
        }
        public class BlockedWords
        {
            public string blockedID { get; set; }
        }
        public Form3()
        {
            InitializeComponent();
            Connector();
            Creator();
        }
        public void Connector()
        {
            try
            {
                if (connection != null) connection.Close();
                connection = new SqliteConnection("Data Source=Dictionary.db");
                connection.Open();
                command = new SqliteCommand();
                command.Connection = connection;
            }
            catch (Exception)
            {
                MessageBox.Show("Проблемы доступа к базе данных. Проверьте наличие БД в корневом каталоге");
                this.Close();
                but1Fm1.Enabled = true;
                but2Fm1.Enabled = true;
                but3Fm1.Enabled = true;
            }
        }
        public void Creator()
        {
            Connector();
            data.Clear();
            jsonSave = null;
            blackListNumbers = null;
            command.CommandText = "SELECT * FROM Words";
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                data.Add(new string[5]);
                data[data.Count - 1][0] = reader[0].ToString();
                data[data.Count - 1][1] = reader[1].ToString();
                data[data.Count - 1][2] = reader[2].ToString();
                data[data.Count - 1][3] = reader[3].ToString();
                data[data.Count - 1][4] = reader[4].ToString();
            }
            if(data.Count == 0)
            {
                data.Add(new string[5]);
                data[0][0] = "0";
                data[0][1] = " - - - ";
                data[0][2] = " - - - ";
                data[0][3] = " - - - ";
                data[0][4] = " - - - ";
                MessageBox.Show("Отсутсвуют слова в словаре");
                button1.Enabled = false;
                button2.Enabled = false;
                button4.Enabled = false;
                button5.Enabled = false;
            }
            SQLConnector();
            if (jsonSave == null)
            {
                if (!File.Exists("BlackList.json"))
                    File.Create("BlackList.json");

                JsonHelper jsonSave = new JsonHelper();
                jsonSave.blockedWords = new List<BlockedWords>();
                jsonSave.blockedWords.Add(new BlockedWords()
                {
                    blockedID = "0"
                });
                serialized = JsonConvert.SerializeObject(jsonSave);
                try
                {
                    File.WriteAllText("BlackList.json", serialized);
                }
                catch (Exception)
                {
                }                
            }
            RandomWords();
            button1.Enabled = false;
            button4.Enabled = false;
            button5.Enabled = false;
        }
        public void SQLConnector()
        {
            try
            {
                jsonSave = JsonConvert.DeserializeObject<JsonHelper>(File.ReadAllText("BlackList.json"));
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка подключения к списку выученых слов.");
                File.WriteAllText("BlackList.json", serialized);
                Closer();
            }
        }
        public void RandomWords()
        {
            Random rnd = new Random();
            blackListNumbers = new List<string>();
            if (jsonSave == null) return;
            foreach (BlockedWords item in jsonSave.blockedWords)
            {
                blackListNumbers.Add(item.blockedID);
            }
            if(blackListNumbers.Count -1 == data.Count)
            {
                MessageBox.Show("Все доступные слова добалены в список выученых слов. Очситите список или добавьте новые слова");
                Closer();
            }
            else if (blackListNumbers == null || blackListNumbers.Count == 1)
            {
                ramdomInt = rnd.Next(0, data.Count);
            }
            else
            {
                findRandomNumber = true;
                while (findRandomNumber)
                {                    
                    ramdomInt = rnd.Next(0, data.Count);
                    foreach (var item in blackListNumbers)
                    {
                        if (item == data[ramdomInt][0])
                        {
                            findRepeat = true;
                            break;
                        }
                        findRepeat = false;
                    }
                    if (findRepeat) findRandomNumber = true;
                    else
                    {
                        findRandomNumber = false;
                        break;
                    }
                }
            }
            if(data.Count != 0)
            {
                try
                {
                    label3.Text = data[ramdomInt][1];
                    label4.Text = data[ramdomInt][2];
                    label4.Visible = false;
                    label5.Text = data[ramdomInt][3];
                    label5.Visible = false;
                    pictureBox1.ImageLocation = (data[ramdomInt][4]);
                    pictureBox1.Visible = false;
                    newExeptionWord = data[ramdomInt][0];
                }
                catch (Exception)
                {
                    Closer();
                }
            }
        }
        public void Closer()
        {
            this.Close();
            if (but1Fm1 != null || but2Fm1 != null || but3Fm1 != null)
            {
                but1Fm1.Enabled = true;
                but2Fm1.Enabled = true;
                but3Fm1.Enabled = true;
            }
        }
        public void Cleaner()
        {
            label3.Text = "";
            label4.Text = "";
            label5.Text = "";
            pictureBox1.ImageLocation = "";
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Cleaner();
            button1.Enabled = false;
            button2.Enabled = true;
            button4.Enabled = false;
            button5.Enabled = false;
            RandomWords();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            label4.Visible = true;
            label5.Visible = true;
            pictureBox1.Visible = true;
            button1.Enabled = true;
            button2.Enabled = false;
            button4.Enabled = true;
            button5.Enabled = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Closer();
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            Connector();
            command.CommandText = $"DELETE FROM Words WHERE id = {data[ramdomInt][0]}";
            var reader = command.ExecuteReader();
            MessageBox.Show("Слово удалено из базы");
            button2.Enabled = true;
            Creator();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (!File.Exists("BlackList.json"))            
                File.Create("BlackList.json");
            jsonSave = JsonConvert.DeserializeObject<JsonHelper>(File.ReadAllText("BlackList.json"));
            jsonSave.blockedWords.Add(new BlockedWords(){ blockedID = newExeptionWord });
            string serialized = JsonConvert.SerializeObject(jsonSave);
            File.WriteAllText("BlackList.json", serialized);
            
        }
    }
}
