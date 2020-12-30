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
    public partial class RSControl : UserControl
    {
        public RSControl()
        {
            InitializeComponent();
        }

        private void metroButton1_Click_1(object sender, EventArgs e)
        {
            string ConnStr = " server = osp74.ru ; port = 33333; user = st_2_4; database = st_2_4; password = 90337436; ";
            MySqlConnection conn = new MySqlConnection(ConnStr);
            //Новые параметры из полей формы
            string new_ID_staff = metroTextBox1.Text;
            string new_full_name_of_the_staff = metroTextBox2.Text;
            string new_login = metroTextBox3.Text;
            string new_password = metroTextBox5.Text;
            string new_job_title = metroTextBox6.Text;
            string new_wages = metroTextBox8.Text;
            if (metroTextBox1.Text.Length > 0)
            {
                //Формируем строку запроса на добавление строк
                string sql_update_staff = "UPDATE staffs SET " +
                "ID_staff = '" + new_ID_staff + "', " +
                "full_name_of_the_staff = '" + new_full_name_of_the_staff + "', " +
                "login = '" + new_login + "', " +
                "password = '" + new_password + "', " +
                "job_title = '" + new_job_title + "', " +
                "wages = '" + new_wages + "' " +
                "WHERE ID_staff = " + metroTextBox1.Text;
                //Посылаем запрос на обновление данных
                MySqlCommand update_staff = new MySqlCommand(sql_update_staff, conn);
                try
                {
                    conn.Open();
                    update_staff.ExecuteNonQuery();
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
            SControl sc = new SControl();
            MainControlClass.showControl(sc, metroPanel1);
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            SControl sc = new SControl();
            MainControlClass.showControl(sc, metroPanel1);
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
