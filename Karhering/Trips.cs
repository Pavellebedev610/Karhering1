using Guna.UI2.WinForms;
using Karhering.Repository;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.ComponentModel.Design.ObjectSelectorEditor;

namespace Karhering
{
    public partial class Trips : Form
    {
        public Trips()
        {
            InitializeComponent();

        }
        Baza baza = new Baza();

        public Client ClientInfo { get; set; }
        public int UserId { get; set; }

        private void guna2Button7_Click(object sender, EventArgs e)
        {
            Hub hubForm = new Hub();
            hubForm.UserId = UserId;
            this.Hide();
            hubForm.Show();
        }

        private void Trips_Load(object sender, EventArgs e)
        {
            carlist history = new carlist();
            Controls.Add(history);
            history.BringToFront();
            history.Location = new Point(12, 75);
            history.BringToFront();
            PrintListAirport(history);
        }

        private void PrintListAirport(carlist history)
        {
            if (UserId != 0)
            {
                if (PanelInfo.Controls.Count > 0)
                    PanelInfo.Controls.RemoveAt(0);

                baza.openConnection();

                // Заменяем запрос на получение поездок только для конкретного пользователя
                string query = "SELECT arenda.end_time, arenda.cost, arenda.car_id, car.marka_auto " +
                               "FROM arenda JOIN car ON arenda.car_id = car.id_car " +
                               "WHERE arenda.id_client = @userId";

                using (SqlCommand command = new SqlCommand(query, baza.getConnection()))
                {
                    command.Parameters.AddWithValue("@userId", UserId);

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        HistoryCar Item = new HistoryCar();
                        Item.label2.Text = reader["end_time"].ToString();
                        Item.label3.Text = reader["marka_auto"].ToString();
                        Item.label4.Text = reader["cost"].ToString(); // Предполагается, что end_time - это столбец в вашей базе данных
                        history.flowLayoutPanel1.Controls.Add(Item);
                    }

                    reader.Close();
                }

                baza.closeConnection();
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
