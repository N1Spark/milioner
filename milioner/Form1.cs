using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace milioner
{
    public partial class Form1 : Form
    {
        string cor_answ;
        string[] lines;
        List<string> answers, shuffle;
        Random rand = new Random();
        int count = 0, answ = 14;
        List<Button> buttons;
        List<Label> labels;
        public Form1()
        {
            InitializeComponent();
            panel2.Visible = false;
            panel1.Visible = false;
            buttons = new List<Button> { button7, button8, button9, button10 };
            labels = new List<Label> { label3, label4, label5, label6 };
            string[] strings = { "15 - 1 000 000", "14 - 500 000", "13 - 250 000" , "12 - 125 000", "11 - 64 000" , "10 - 32 000",
            "9 - 16 000", "8 - 8 000", "7 - 4 000", "6 - 2 000", "5 - 1 000", "4 - 500", "3 - 300", "2 - 200", "1 - 100"};
            listBox1.Items.AddRange(strings);
            lines = File.ReadAllLines("C:\\VS Projects\\milioner\\milioner\\question.txt");
            Start();
        }
        private void button_Click(object sender, EventArgs e)
        {
            bool check;
            foreach (Button x in buttons)
                x.Enabled = false;
            Button button = (Button)sender;
            if (button.Text == cor_answ)
            {
                button.BackColor = Color.Green;
                check = true;
            }
            else
            {
                button.BackColor = Color.Red;
                foreach (Button x in buttons)
                    if (x.Text == cor_answ)
                        button = (Button)x;
                button.BackColor = Color.Green;
                check = false;
            }
            Game(check);
        }
        private void Game(bool check)
        {
            panel1.Visible = false;
            panel2.Visible = false;
            foreach (Button x in buttons)
            {
                x.Enabled = true;
                x.BackColor = Color.Black;
            }
            Thread.Sleep(2000);
            if (answ < 0)
            {
                button3_Click(null, null);
                return;
            }
            if (check)
            {
                textBox1.Text = lines[count];
                cor_answ = lines[count + 1];
                answers = new List<string> { lines[count + 1], lines[count + 2], lines[count + 3], lines[count + 4] };
                shuffle = answers.OrderBy(x => rand.Next()).ToList();
                button7.Text = shuffle[0];
                button8.Text = shuffle[1];
                button9.Text = shuffle[2];
                button10.Text = shuffle[3];
                listBox1.SelectedIndex = answ;
                answ--;
                count += 5;
            }
            else
            {
                groupBox1.Enabled = false;
                foreach (Button x in buttons)
                    x.Enabled = false;
                textBox1.Text = "You lose";
                listBox1.SelectedIndex = -1;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
            label2.Text = "Я думаю это...\n";
            int rand_a = rand.Next(1, 100);
            if (rand_a <= 30)
                label2.Text += cor_answ;
            else
            {
                rand_a = rand.Next(0, 3);
                label2.Text += buttons[rand_a].Text;
            }
            button5.Enabled = false;
            button5.BackgroundImage = Properties.Resources._5;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Button cor_but = new Button();
            foreach (Button x in buttons)
                if (x.Text == cor_answ)
                    cor_but = (Button)x;
            foreach (Button x in buttons)
                x.Enabled = false;
            cor_but.Enabled = true;
            while (true)
            {
                int rand_b = rand.Next(0, 3);
                if (buttons[rand_b].Text != cor_answ)
                {
                    buttons[rand_b].Enabled = true;
                    break;
                }
            }
           button4.BackgroundImage = Properties.Resources._4;
            button4.Enabled = false;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            panel2.Visible = true;
            int chance = 100;
            int cor_anws_c = rand.Next(30, 80);
            chance -= cor_anws_c;
            int rand_answ;
            for (int i = 0; i < 4; i++)
            {
                if (buttons[i].Text == cor_answ)
                    labels[i].Text = $"{buttons[i].Text} - {cor_anws_c}%";
                else
                {
                    rand_answ = rand.Next(0, chance);
                    labels[i].Text = $"{buttons[i].Text} - {rand_answ}%";
                    if (chance - rand_answ < 0)
                        chance = 0;
                    else
                        chance -= rand_answ;
                }
            }
            button6.Enabled = false;
           button6.BackgroundImage = Properties.Resources._6;
            button4.Select();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            foreach (Button x in buttons)
                x.Enabled = false;
            string text = "Выйгрыш ";
            string temp = listBox1.SelectedItem.ToString();
            int ind = temp.IndexOf(" - ");
            textBox1.Text = text;
            for (int i = ind; i < temp.Length; i++)
                textBox1.Text += temp[i];
            textBox1.Text += "$";
        }
        private void Start()
        {
            textBox1.Text = lines[0];
            var item = listBox1.Items[14];
            cor_answ = lines[1];
            answers = new List<string> { lines[1], lines[2], lines[3], lines[4] };
            shuffle = answers.OrderBy(x => rand.Next()).ToList();
            button7.Text = shuffle[0];
            button8.Text = shuffle[1];
            button9.Text = shuffle[2];
            button10.Text = shuffle[3];
            listBox1.SelectedIndex = answ;
            answ--;
            count += 5;
        }
    }
}
