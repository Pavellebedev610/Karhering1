using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
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
            Baza dataBases = new Baza();
            var fio = guna2TextBox1.Text;
            var mail = guna2TextBox3.Text;
            var password = guna2TextBox2.Text;
            var number_prav = guna2TextBox4.Text;
            var telefon = guna2TextBox5.Text;
            var role = 2;

            string querystring = $"insert into client(FIO, mail, password, number_prav, telefon,role) values (@fio,@mail,@password,@number_prav,@telefon,@role)";

            SqlCommand command = new SqlCommand(querystring, dataBases.getConnection());

            command.Parameters.AddWithValue("fio", fio);
            command.Parameters.AddWithValue("mail", mail);
            command.Parameters.AddWithValue("password", password);
            command.Parameters.AddWithValue("number_prav", number_prav);
            command.Parameters.AddWithValue("telefon", telefon);
            command.Parameters.AddWithValue("role", role);

            dataBases.openConnection();

            if (command.ExecuteNonQuery() == 1)
            {
                DataBank.Text2 = guna2TextBox5.Text;
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

        }

        private void Registration_Load(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Login frmlgn = new Login();
            this.Hide();
            frmlgn.ShowDialog();
        }
    }
}
