using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace аптека
{
    public partial class RLControl : UserControl
    {
        public RLControl()
        {
            InitializeComponent();
        }

        private void metroButton3_Click(object sender, EventArgs e)
        {
            string ConnStr = " server = osp74.ru ; port = 33333; user = st_2_4; database = st_2_4; password = 90337436; ";
            MySqlConnection conn = new MySqlConnection(ConnStr);
            //Новые параметры из полей формы
            string new_ID_medicament = metroTextBox1.Text;
            string new_name_of_medicaments = metroTextBox2.Text;
            string new_fabricator = metroTextBox3.Text;
            string new_storage_location = metroTextBox5.Text;
            string new_disease = metroTextBox6.Text;
            string new_price = metroTextBox8.Text;
            if (metroTextBox1.Text.Length > 0)
            {
                //Формируем строку запроса на добавление строк
                string sql_update_med = "UPDATE medicaments SET " +
                "name_of_medicaments = '" + new_name_of_medicaments + "', " +
                "fabricator = '" + new_fabricator + "', " +
                "storage_location = '" + new_storage_location + "', " +
                "disease = '" + new_disease + "', " +
                "price = '" + new_price + "' " +
                "WHERE ID_medicament = " + metroTextBox1.Text;
                //Посылаем запрос на обновление данных
                MySqlCommand update_med = new MySqlCommand(sql_update_med, conn);
                try
                {
                    conn.Open();
                    update_med.ExecuteNonQuery();
                    MessageBox.Show("Изменение прошло успешно", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка изменения строки \n\n" + ex, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //Application.Exit();
                }
                finally
                {
                    conn.Close();
                }
            }
            LConrol lc = new LConrol();
            MainControlClass.showControl(lc, metroPanel1);
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            LConrol lc = new LConrol();
            MainControlClass.showControl(lc, metroPanel1);
        }
    }
}
