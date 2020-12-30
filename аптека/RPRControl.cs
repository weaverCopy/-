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
    public partial class RPRControl : UserControl
    {
        public RPRControl()
        {
            InitializeComponent();
        }

        private void metroButton1_Click_1(object sender, EventArgs e)
        {
            string ConnStr = " server = osp74.ru ; port = 33333; user = st_2_4; database = st_2_4; password = 90337436; ";
            MySqlConnection conn = new MySqlConnection(ConnStr);
            //Новые параметры из полей формы
            DateTime dt_db;
            dt_db = Convert.ToDateTime(dateTimePicker1.Value);
            dateTimePicker1.Value = dt_db;
            string new_ID_sales = metroTextBox1.Text;
            string new_sold_of_medicaments = metroTextBox2.Text;
            string new_quantity = metroTextBox3.Text;
            string new_date_of_sale = dateTimePicker1.Value.ToString("yyyy-MM-dd");


            if (metroTextBox1.Text.Length > 0)
            {
                //Формируем строку запроса на добавление строк
                string sql_update_sales = "UPDATE sales SET " +
                "ID_sales = '" + new_ID_sales + "', " +
                "sold_of_medicaments = '" + new_sold_of_medicaments + "', " +
                "quantity = '" + new_quantity + "', " +
                "date_of_sale = '" + new_date_of_sale + "' " +
                "WHERE ID_sales = " + metroTextBox1.Text;
                //Посылаем запрос на обновление данных
                MySqlCommand update_sales = new MySqlCommand(sql_update_sales, conn);
                try
                {
                    conn.Open();
                    update_sales.ExecuteNonQuery();
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
            PRControl prc = new PRControl();
            MainControlClass.showControl(prc, metroPanel1);
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            PRControl prc = new PRControl();
            MainControlClass.showControl(prc, metroPanel1);
        }
    }
}
