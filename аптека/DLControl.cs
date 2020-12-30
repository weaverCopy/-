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
    public partial class DLControl : UserControl
    {
        public DLControl()
        {
            InitializeComponent();
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {

            string ConnStr = " server = osp74.ru ; port = 33333; user = st_2_4; database = st_2_4; password = 90337436; ";
            MySqlConnection conn = new MySqlConnection(ConnStr);

            //Получение новых параметров пользователя
            string new_ID_medicament = metroTextBox1.Text;
            string new_name_of_medicaments = metroTextBox2.Text;
            string new_fabricator = metroTextBox3.Text;
            string new_storage_location = metroTextBox5.Text;
            string new_disease = metroTextBox6.Text;
            string new_price = metroTextBox8.Text;
            {
                string sql_insert_med = " INSERT INTO `medicaments` (`ID_medicament`, `name_of_medicaments`, `fabricator`, `storage_location`, `disease`, `price`) " +
                "VALUES ('" + new_ID_medicament + "', '" + new_name_of_medicaments + "', '" + new_fabricator + "', '" + new_storage_location + "', '" + new_disease + "',  '" + new_price + "')";

                MySqlCommand insert_med = new MySqlCommand(sql_insert_med, conn);
                try
                {
                    conn.Open();
                    insert_med.ExecuteNonQuery();
                    MessageBox.Show("Добавление лекарства прошло успешно", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка добавления лекарства \n\n" + ex, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Application.Exit();
                }
                finally
                {
                    conn.Close();
                }
            }

            LConrol lc = new LConrol();
            MainControlClass.showControl(lc, metroPanel1);

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            LConrol lc = new LConrol();
            MainControlClass.showControl(lc, metroPanel1);
        }

        private void metroButton2_Click_1(object sender, EventArgs e)
        {
            LConrol lc = new LConrol();
            MainControlClass.showControl(lc, metroPanel1);
        }
    }
}
