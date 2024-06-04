using Karhering.Repository;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Karhering
{
    public partial class CreateCar : Form
    {
        Baza dataBases = new Baza();
        public Client ClientInfo { get; set; }
        public int UserId { get; set; }

        public CreateCar()
        {
            InitializeComponent();
        }

        private void guna2Button7_Click(object sender, EventArgs e)
        {
            Hub hubForm = new Hub();
            hubForm.UserId = UserId;
            this.Hide();
            hubForm.Show();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog efd = new OpenFileDialog();
                if (efd.ShowDialog() == DialogResult.OK)
                    Car.Image = Image.FromFile(efd.FileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CreateCar_Load(object sender, EventArgs e)
        {

        }

        private void ButtonGo_Click(object sender, EventArgs e)
        {
            {
                try
                {
                    var marka_auto = guna2TextBox1.Text;
                    var model_auto = guna2TextBox2.Text;
                    var number = maskedTextBox1.Text;
                    var god_vipuska = guna2TextBox5.Text;
                    var probeg = guna2TextBox3.Text;
                    var toplivo = guna2TextBox4.Text;

                    var coordinat_x = guna2TextBox6.Text;
                    var coordinat_y = guna2TextBox7.Text;
                    MemoryStream memoryStream = new MemoryStream();
                    Car.Image.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Jpeg);
                    byte[] Photo = new byte[memoryStream.Length];
                    memoryStream.Position = 0;
                    memoryStream.Read(Photo, 0, Photo.Length);
                    string querystring = $"insert into car(marka_auto, model_auto, number, god_vipuska, probeg,toplivo,cordinat_x,cordinat_y, PhotoCar) values (@marka_auto,@model_auto,@number,@god_vipuska,@probeg,@toplivo,@cordinat_x,@cordinat_y,@PhotoCar)";
                    SqlCommand command = new SqlCommand(querystring, dataBases.getConnection());
                    command.Parameters.AddWithValue("marka_auto", marka_auto);
                    command.Parameters.AddWithValue("model_auto", model_auto);
                    command.Parameters.AddWithValue("number", number);
                    command.Parameters.AddWithValue("god_vipuska", god_vipuska);
                    command.Parameters.AddWithValue("probeg", probeg);
                    command.Parameters.AddWithValue("toplivo", toplivo);
                    command.Parameters.AddWithValue("cordinat_x", coordinat_x);
                    command.Parameters.AddWithValue("cordinat_y", coordinat_y);
                    command.Parameters.AddWithValue("@PhotoCar", Photo);
                    dataBases.openConnection();
                    command.ExecuteNonQuery();
                    dataBases.closeConnection();
                    MessageBox.Show("Машина успешно добавлена");

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Messenge", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
        }
    }
}
