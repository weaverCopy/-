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
    public partial class DPRControl : UserControl
    {
        public DPRControl()
        {
            InitializeComponent();
        }

        private void metroButton1_Click_1(object sender, EventArgs e)
        {
            string ConnStr = " server = osp74.ru ; port = 33333; user = st_2_4; database = st_2_4; password = 90337436; ";
            MySqlConnection conn = new MySqlConnection(ConnStr);

            //Получение новых параметров пользователя
            DateTime dt_db;
            dt_db = Convert.ToDateTime(dateTimePicker1.Value);
            dateTimePicker1.Value = dt_db;
            string new_ID_sales = metroTextBox1.Text;
            string new_sold_of_medicaments = metroTextBox2.Text;
            string new_quantity = metroTextBox3.Text;
            string new_date_of_sale = dateTimePicker1.Value.ToString("yyyy-MM-dd");
            {
                string sql_insert_post = " INSERT INTO `sales` (`ID_sales`, `sold_of_medicaments`, `quantity`, `date_of_sale`) " +
                "VALUES ('" + new_ID_sales + "', '" + new_sold_of_medicaments + "', '" + new_quantity + "', '" + new_date_of_sale + "')";

                MySqlCommand insert_post = new MySqlCommand(sql_insert_post, conn);
                try
                {
                    conn.Open();
                    insert_post.ExecuteNonQuery();
                    MessageBox.Show("Добавление продажи прошло успешно", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка добавления продажи \n\n" + ex, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Application.Exit();
                }
                finally
                {
                    conn.Close();
                }

                PRControl prc = new PRControl();
                MainControlClass.showControl(prc, metroPanel1);

            }
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            PRControl prc = new PRControl();
            MainControlClass.showControl(prc, metroPanel1);
        }
    }
}
