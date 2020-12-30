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
    public partial class DSControl : UserControl
    {
        public DSControl()
        {
            InitializeComponent();
        }

        private void metroButton1_Click_1(object sender, EventArgs e)
        {
            string ConnStr = " server = osp74.ru ; port = 33333; user = st_2_4; database = st_2_4; password = 90337436; ";
            MySqlConnection conn = new MySqlConnection(ConnStr);

            //Получение новых параметров пользователя
            string new_ID_staff = metroTextBox1.Text;
            string new_new_full_name_of_the_staff = metroTextBox2.Text;
            string new_login = metroTextBox3.Text;
            string new_password = metroTextBox5.Text;
            string new_job_title = metroTextBox6.Text;
            string new_wages = metroTextBox8.Text;
            {
                string sql_insert_med = " INSERT INTO `staffs` (`ID_staff`, `full_name_of_the_staff`, `login`, `password`, `job_title`, `wages`) " +
                "VALUES ('" + new_ID_staff + "', '" + new_new_full_name_of_the_staff + "', '" + new_login + "', '" + new_password + "', '" + new_job_title + "', '" + new_wages + "')";

                MySqlCommand insert_med = new MySqlCommand(sql_insert_med, conn);
                try
                {
                    conn.Open();
                    insert_med.ExecuteNonQuery();
                    MessageBox.Show("Добавление сотрудника прошло успешно", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка добавления сотрудника \n\n" + ex, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Application.Exit();
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
    }
}
