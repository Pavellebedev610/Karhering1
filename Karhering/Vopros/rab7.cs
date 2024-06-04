using Karhering.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Karhering.Vopros
{
    public partial class rab7 : Form
    {
        public rab7()
        {
            InitializeComponent();
        }

        private void rab7_Load(object sender, EventArgs e)
        {

        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button_Click(object sender, EventArgs e)
        {
            string promo = guna2TextBox1.Text;

            int bonus = new ClientRepos().GetBonusByPromo(promo);
            if (bonus != 0)
            {
                // Обновляем значение бонуса на форме Bonus
                Bonus bonusForm = (Bonus)Application.OpenForms["Bonus"];
                if (bonusForm != null)
                {
                    int currentBonus = int.Parse(bonusForm.label3.Text);
                    int newBonus = currentBonus + 1000;
                    bonusForm.label3.Text = newBonus.ToString();

                    // Обновляем бонусы в базе данных
                    new ClientRepos().UpdateUserBonus(bonusForm.UserId, newBonus);
                }
                else
                {
                    MessageBox.Show("Форма Bonus не найдена");
                }

                // Закрываем форму rab7
                this.Close();
            }
            else
            {
                MessageBox.Show("Неправильный промокод");
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
