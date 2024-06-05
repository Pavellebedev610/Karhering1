using Karhering.Repository;
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

namespace Karhering
{
    public partial class Auto : Form
    {
        Baza baza = new Baza();
        public Client ClientInfo { get; set; }
        public int UserId { get; set; }
        public Auto()
        {
            InitializeComponent();
            LoadData();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            guna2DataGridView1.Rows.Clear();
        }
        private void LoadData()
        {
            // Очистка существующих строк в таблице
            guna2DataGridView1.Rows.Clear();

            string connectString = (@"Data Source = DESKTOP-8BRSR7L; Initial Catalog=karshering;Integrated Security=True");
            SqlConnection myConnection = new SqlConnection(connectString);
            myConnection.Open();
            string query = "SELECT * FROM car ORDER BY id_car";
            SqlCommand command = new SqlCommand(query, myConnection);

            SqlDataReader reader = command.ExecuteReader();
            List<string[]> data = new List<string[]>();

            while (reader.Read())
            {
                data.Add(new string[7]);
                data[data.Count - 1][0] = reader[0].ToString();
                data[data.Count - 1][1] = reader[1].ToString();
                data[data.Count - 1][2] = reader[2].ToString();
                data[data.Count - 1][3] = reader[3].ToString();
                data[data.Count - 1][4] = reader[4].ToString();
                data[data.Count - 1][5] = reader[5].ToString();
                data[data.Count - 1][6] = reader[6].ToString();
            }
            reader.Close();
            myConnection.Close();

            foreach (string[] s in data)
                guna2DataGridView1.Rows.Add(s);
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

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void DeleteSelectedRow_Click(object sender, EventArgs e)
        {
            if (guna2DataGridView1.SelectedRows.Count > 0)
            {
                string carId = guna2DataGridView1.SelectedRows[0].Cells["id_car"].Value.ToString();

                string queryDeleteCar = "DELETE FROM car WHERE id_car = @CarId";

                using (SqlCommand command = new SqlCommand(queryDeleteCar, baza.getConnection()))
                {
                    command.Parameters.AddWithValue("@CarId", carId);

                    try
                    {
                        baza.openConnection();
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Автомобиль успешно удален");
                            LoadData(); // Обновляем данные в таблице после удаления
                        }
                        else
                        {
                            MessageBox.Show("Автомобиль не найден");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка при удалении автомобиля: " + ex.Message);
                    }
                    finally
                    {
                        baza.closeConnection();
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите строку для удаления");
            }
        }

        private void button_Click(object sender, EventArgs e)
        {
            button.Click += new EventHandler(DeleteSelectedRow_Click);
        }
    }
}
