using Guna.UI2.WinForms;
using Karhering.Repository;
using Microsoft.VisualBasic.ApplicationServices;
using Microsoft.VisualBasic.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Karhering
{
    public partial class Nastroiki : Form
    {
        Baza baza = new Baza();
        public Client ClientInfo { get; set; }
        public int UserId { get; set; }
        public Nastroiki()
        {
            InitializeComponent();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            Hub hubForm = new Hub();
            hubForm.UserId = UserId;
            this.Hide();
            hubForm.Show();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Login log = new Login();
            this.Hide();
            log.Show();
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            Delete log = new Delete();
            log.Show();
        }

        private void Nastroiki_Load(object sender, EventArgs e)
        {
            ClientInfo = new ClientRepos().GetUser(UserId);

            if (ClientInfo != null)
            {
                // Если информация о клиенте успешно получена, устанавливаем ФИО на форме
                label3.Text = ClientInfo.telefon;
                label6.Text = ClientInfo.mail;
            }
        }

        private void label3_Click_1(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }
    }
}

