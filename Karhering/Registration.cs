using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Karhering
{
    public partial class Registration : Form
    {
        public Registration()
        {
            InitializeComponent();
        }

        private void label7_Click(object sender, EventArgs e)
        {
        }

        private void button_Click(object sender, EventArgs e)
        {
            if (guna2TextBox1.Text.Trim() == "" || guna2TextBox2.Text.Trim() == "" || guna2TextBox3.Text.Trim() == "" || guna2TextBox4.Text.Trim() == "" || guna2TextBox5.Text.Trim() == "")
            {
                MessageBox.Show("Пожалуйста, заполните все обязательные поля");
                return; // Прерываем выполнение метода, если не все поля заполнены
            }
            Random random = new Random();
            Baza dataBases = new Baza();
            var fio = guna2TextBox1.Text;
            var mail = guna2TextBox3.Text;
            var password = guna2TextBox2.Text;
            var number_prav = guna2TextBox4.Text;
            var telefon = guna2TextBox5.Text;
            var role = 2;
            var rating = 5;
            var promo = random.Next().ToString();

            string querystring = $"insert into client(FIO, mail, password, number_prav, telefon,role,promo,rating) values (@fio,@mail,@password,@number_prav,@telefon,@role,@promo,@rating)";

            SqlCommand command = new SqlCommand(querystring, dataBases.getConnection());

            command.Parameters.AddWithValue("fio", fio);
            command.Parameters.AddWithValue("mail", mail);
            command.Parameters.AddWithValue("password", password);
            command.Parameters.AddWithValue("number_prav", number_prav);
            command.Parameters.AddWithValue("telefon", telefon);
            command.Parameters.AddWithValue("role", role);
            command.Parameters.AddWithValue("promo", promo);
            command.Parameters.AddWithValue("rating", rating);
            dataBases.openConnection();

            if (command.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("accaunt succesful create!");
                Login frmlgn = new Login();
                this.Hide();
                frmlgn.ShowDialog();
            }
            else
            {
                MessageBox.Show("akkaunt ne sozdan");
            }
            dataBases.closeConnection();
        }

        private void guna2Button7_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void guna2TextBox5_TextChanged(object sender, EventArgs e)
        {
            Guna.UI2.WinForms.Guna2TextBox textBox = sender as Guna.UI2.WinForms.Guna2TextBox;

            // Удаление всех символов, кроме цифр и знака +
            textBox.Text = Regex.Replace(textBox.Text, @"[^\d+]", "");

            // Установка максимальной длины текста
            if (textBox.Text.Length > 12)
            {
                textBox.Text = textBox.Text.Substring(0, 12);
            }
        }

        private void Registration_Load(object sender, EventArgs e)
        {
            guna2TextBox1.PlaceholderText = "Лебедев Павел Игоревич";
            guna2TextBox3.PlaceholderText = "maksim@mail.ru";
            guna2TextBox4.PlaceholderText = "9523215645";
            guna2TextBox5.PlaceholderText = "89142980850";
          
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Login frmlgn = new Login();
            this.Hide();
            frmlgn.ShowDialog();
        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {
            Guna.UI2.WinForms.Guna2TextBox textBox = sender as Guna.UI2.WinForms.Guna2TextBox;

            // Ограничение на максимальное количество символов
            if (textBox.TextLength > 50)
            {
                textBox.Text = textBox.Text.Substring(0, 50);
                textBox.SelectionStart = textBox.Text.Length;
            }

            // Проверка на наличие только русских букв
            textBox.Text = Regex.Replace(textBox.Text, @"[^а-яА-ЯёЁ\s]", "");

        }

        private void guna2TextBox3_TextChanged(object sender, EventArgs e)
        {
            Guna.UI2.WinForms.Guna2TextBox textBox = sender as Guna.UI2.WinForms.Guna2TextBox;
            if (textBox.TextLength > 50)
            {
                textBox.Text = textBox.Text.Substring(0, 50);
                textBox.SelectionStart = textBox.Text.Length;
            }

            // Удаляем все символы, кроме допустимых (буквы, цифры, @, точка, минус, подчеркивание)
            textBox.Text = Regex.Replace(textBox.Text, @"[^a-zA-Z0-9@.\-_]", "");

            // Проверка наличия допустимого текста в формате @mail.ru
            if (!Regex.IsMatch(textBox.Text, @"^.*@mail\.ru$"))
            {
                
            }
        }

        private void guna2TextBox4_TextChanged(object sender, EventArgs e)
        {
            Guna.UI2.WinForms.Guna2TextBox textBox = sender as Guna.UI2.WinForms.Guna2TextBox;

            // Удаляем все символы, кроме цифр
            textBox.Text = Regex.Replace(textBox.Text, @"[^\d]", "");

            // Проверка на длину текста (ровно 10 символов)
            if (textBox.Text.Length > 10)
            {
                textBox.Text = textBox.Text.Substring(0, 10);
            }
        }
    }
}
