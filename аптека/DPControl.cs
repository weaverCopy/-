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
    public partial class DPControl : UserControl
    {
        public DPControl()
        {
            InitializeComponent();
        }

        private void metroButton3_Click(object sender, EventArgs e)
        {

            string ConnStr = " server = osp74.ru ; port = 33333; user = st_2_4; database = st_2_4; password = 90337436; ";
            MySqlConnection conn = new MySqlConnection(ConnStr);

            //Получение новых параметров пользователя
            DateTime dt_db;
            dt_db = Convert.ToDateTime(dateTimePicker1.Value);
            dateTimePicker1.Value = dt_db;
            string new_ID_procurment = metroTextBox1.Text;
            string new_name_of_procurment = metroTextBox2.Text;
            string new_amount_of_medication = metroTextBox3.Text;
            string new_the_cost_of_procurement = metroTextBox4.Text;
            string new_order_date = dateTimePicker1.Value.ToString("yyyy-MM-dd");
            {
                string sql_insert_post = " INSERT INTO `procurements` (`ID_procurment`, `name_of_procurment`, `amount_of_medication`, `the_cost_of_procurement`, `order_date`) " +
                "VALUES ('" + new_ID_procurment + "', '" + new_name_of_procurment + "', '" + new_amount_of_medication + "', '" + new_the_cost_of_procurement + "', '" + new_order_date + "')";

                MySqlCommand insert_post = new MySqlCommand(sql_insert_post, conn);
                try
                {
                    conn.Open();
                    insert_post.ExecuteNonQuery();
                    MessageBox.Show("Добавление поставки прошло успешно", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка добавления поставки \n\n" + ex, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Application.Exit();
                }
                finally
                {
                    conn.Close();
                }

                PControl pc = new PControl();
                MainControlClass.showControl(pc, metroPanel1);

            }
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            PControl pc = new PControl();
            MainControlClass.showControl(pc, metroPanel1);
        }

    }
}
