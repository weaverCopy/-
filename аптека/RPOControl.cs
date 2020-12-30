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
    public partial class RPOControl : UserControl
    {
        public RPOControl()
        {
            InitializeComponent();
        }

        private void metroButton1_Click_1(object sender, EventArgs e)
        {
            string ConnStr = " server = osp74.ru ; port = 33333; user = st_2_4; database = st_2_4; password = 90337436; ";
            MySqlConnection conn = new MySqlConnection(ConnStr);
            //Новые параметры из полей формы
            string new_id = metroTextBox1.Text;
            string new_name_of_suppliers = metroTextBox2.Text;
            string new_country_suppliers = metroTextBox3.Text;
            if (metroTextBox1.Text.Length > 0)
            {
                //Формируем строку запроса на добавление строк
                string sql_update_sup = "UPDATE suppliers SET " +
                "id = '" + new_id + "', " +
                "name_of_suppliers = '" + new_name_of_suppliers + "', " +
                "country_suppliers = '" + new_country_suppliers + "' " +
                "WHERE id = " + metroTextBox1.Text;
                //Посылаем запрос на обновление данных
                MySqlCommand update_sup = new MySqlCommand(sql_update_sup, conn);
                try
                {
                    conn.Open();
                    update_sup.ExecuteNonQuery();
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
