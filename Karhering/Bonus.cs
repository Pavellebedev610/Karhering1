using GMap.NET.MapProviders;
using Karhering.Repository;
using Karhering.Vopros;
using Microsoft.VisualBasic.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Karhering
{
    public partial class Bonus : Form
    {
        public Client ClientInfo { get; set; }
        public int UserId { get; set; }
        private static Random random = new Random(); // Глобальная переменная для генерации случайных чисел

        public Bonus()
        {
            InitializeComponent();

            // Генерация случайной строки из 8 символов и установка ее в label10
            string randomString = GenerateRandomString(8);
            label10.Text = randomString;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            rab6 log = new rab6();
            log.Show();
        }

        private void label10_Click(object sender, EventArgs e)
        {
            // Генерация случайной строки из 8 символов и установка ее в label10 при клике на label10
            string randomString = GenerateRandomString(8);
            label10.Text = randomString;
        }

        // Метод для генерации случайной строки из указанного количества символов
        private string GenerateRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            char[] randomChars = new char[length];

            for (int i = 0; i < length; i++)
            {
                randomChars[i] = chars[random.Next(chars.Length)];
            }

            return new string(randomChars);
        }


        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button_Click(object sender, EventArgs e)
        {
            rab7 log = new rab7();
            log.Show();
        }

        private void label2_Click(object sender, EventArgs e)
        {

            rab6 log = new rab6();
            log.Show();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            Hub hubForm = new Hub();
            hubForm.UserId = UserId;
            this.Hide();
            hubForm.Show();
        }

        private void Bonus_Load(object sender, EventArgs e)
        {
            ClientInfo = new ClientRepos().GetUser(UserId);

            if (ClientInfo != null)
            {
                // Если информация о клиенте успешно получена, устанавливаем ФИО на форме
                label3.Text = ClientInfo.bonus;
            }
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            rab6 log = new rab6();
            log.Show();
        }
    }
}