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
    public partial class RPControl : UserControl
    {
        public RPControl()
        {
            InitializeComponent();
        }

        private void metroButton3_Click_1(object sender, EventArgs e)
        {
            string ConnStr = " server = osp74.ru ; port = 33333; user = st_2_4; database = st_2_4; password = 90337436; ";
            MySqlConnection conn = new MySqlConnection(ConnStr);
            //Новые параметры из полей формы
            DateTime dt_db;
            dt_db = Convert.ToDateTime(dateTimePicker1.Value);
            dateTimePicker1.Value = dt_db;
            string new_ID_procurment = metroTextBox1.Text;
            string new_name_of_procurment = metroTextBox2.Text;
            string new_amount_of_medication = metroTextBox3.Text;
            string new_the_cost_of_procurement = metroTextBox4.Text;
            string new_order_date = dateTimePicker1.Value.ToString("yyyy-MM-dd");
            if (metroTextBox1.Text.Length > 0)
            {
                //Формируем строку запроса на добавление строк
                string sql_update_procur = "UPDATE procurements SET " +
                "ID_procurment = '" + new_ID_procurment + "', " +
                "name_of_procurment = '" + new_name_of_procurment + "', " +
                "amount_of_medication = '" + new_amount_of_medication + "', " +
                "the_cost_of_procurement = '" + new_the_cost_of_procurement + "', " +
                "order_date = '" + new_order_date + "' " +
                "WHERE ID_procurment = " + metroTextBox1.Text;
                //Посылаем запрос на обновление данных
                MySqlCommand update_procur = new MySqlCommand(sql_update_procur, conn);
                try
                {
                    conn.Open();
                    update_procur.ExecuteNonQuery();
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
            PControl pc = new PControl();
            MainControlClass.showControl(pc, metroPanel1);
        }

       

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            PControl pc = new PControl();
            MainControlClass.showControl(pc, metroPanel1);
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
