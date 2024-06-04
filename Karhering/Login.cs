using Guna.UI2.WinForms;
using Microsoft.VisualBasic.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Karhering
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {


        }

        private void button_Click(object sender, EventArgs e)
        {
            Baza dataBases = new Baza();

            var mail = guna2TextBox1.Text;
            var password = guna2TextBox3.Text;

            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();
            string querystring = $"select id_polz, FIO, number_prav,role from client where mail = @mail and password =@password";


            SqlCommand command = new SqlCommand(querystring, dataBases.getConnection());

            command.Parameters.AddWithValue("mail", mail);
            command.Parameters.AddWithValue("password", password);

            adapter.SelectCommand = command;
            adapter.Fill(table);


            if (table.Rows.Count == 1)
            {
                int role = Convert.ToInt32(table.Rows[0]["role"]);
                switch (role)
                {
                    case 1:
                        DataBank.Text2 = "AdminPanel";
                        MessageBox.Show("Вы вошли в админ-панель");
                        Hub frmlgn = new Hub();
                        frmlgn.UserId = (int)table.Rows[0]["id_polz"];
                        Hub.Instance.pnlUser.Visible = false;
                        this.Hide();
                        frmlgn.ShowDialog();
                        break;
                    case 2:
                        DataBank.Text2 = guna2TextBox1.Text;
                        MessageBox.Show("Вы вошли");
                        Hub frmlgn2 = new Hub();
                        frmlgn2.UserId = (int)table.Rows[0]["id_polz"];
                        Hub.Instance.pnlAdmin.Visible = false;
                        this.Hide();
                        frmlgn2.ShowDialog();
                        break;
                    default:
                        MessageBox.Show("Такой роли не существует");
                        break;
                }
            }
            else
            {
                MessageBox.Show("Такого пользователя не существует");
            }
        }
        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Registration log = new Registration();
            this.Hide();
            log.Show();
        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2TextBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2Button7_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
