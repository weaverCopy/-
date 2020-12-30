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
    public partial class DPOControl : UserControl
    {
        public DPOControl()
        {
            InitializeComponent();
        }

        private void metroButton1_Click_1(object sender, EventArgs e)
        {
            string ConnStr = " server = osp74.ru ; port = 33333; user = st_2_4; database = st_2_4; password = 90337436; ";
            MySqlConnection conn = new MySqlConnection(ConnStr);

            //Получение новых параметров пользователя
            string new_id = metroTextBox1.Text;
            string new_name_of_suppliers = metroTextBox2.Text;
            string new_country_suppliers = metroTextBox3.Text;

            {
                string sql_insert_post = " INSERT INTO `suppliers` (`id`, `name_of_suppliers`, `country_suppliers`) " +
                "VALUES ('" + new_id + "', '" + new_name_of_suppliers + "', '" + new_country_suppliers + "')";

                MySqlCommand insert_post = new MySqlCommand(sql_insert_post, conn);
                try
                {
                    conn.Open();
                    insert_post.ExecuteNonQuery();
                    MessageBox.Show("Добавление поставщика прошло успешно", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка добавления поставщика \n\n" + ex, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Application.Exit();
                }
                finally
                {
                    conn.Close();
                }

            }
            POControl poc = new POControl();
            MainControlClass.showControl(poc, metroPanel1);
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            POControl poc = new POControl();
            MainControlClass.showControl(poc, metroPanel1);
        }
    }
}
