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
    public partial class SControl : UserControl
    {
        string ConnStr = " server = osp74.ru ; port = 33333; user = st_2_4; database = st_2_4; password = 90337436; ";
        //Переменная соединения
        MySqlConnection conn;
        //DataAdapter представляет собой объект Command , получающий данные из источника данных.
        private MySqlDataAdapter MyDA = new MySqlDataAdapter();
        //Объявление BindingSource, основная его задача, это обеспечить унифицированный доступ к источнику данных.
        private BindingSource bSource = new BindingSource();
        //DataSet - расположенное в оперативной памяти представление данных, обеспечивающее согласованную реляционную программную 
        //модель независимо от источника данных.DataSet представляет полный набор данных, включая таблицы, содержащие, упорядочивающие 
        //и ограничивающие данные, а также связи между таблицами.
        private DataSet ds = new DataSet();
        //Представляет одну таблицу данных в памяти.
        private DataTable table = new DataTable();
        //Переменная для ID записи в БД, выбранной в гриде
        string id_selected_rows;

        public SControl()
        {
            InitializeComponent();
        }

        //Метод получения ID выделенной строки, для последующего вызова его в нужных методах
        public void GetSelectedIDString()
        {
            //Переменная для индекс выбранной строки в гриде
            string index_selected_rows;
            //Индекс выбранной строки
            index_selected_rows = metroGrid1.SelectedCells[0].RowIndex.ToString();
            //ID конкретной записи в Базе данных, на основании индекса строки
            id_selected_rows = metroGrid1.Rows[Convert.ToInt32(index_selected_rows)].Cells[0].Value.ToString();

        }

        public void reload_list()
        {
            //Чистим виртуальную таблицу
            table.Clear();
            //Вызываем метод получения записей, который вновь заполнит таблицу
            GetListstaffs();
        }

        private void SControl_Load(object sender, EventArgs e)
        {
            //Переменная соединения
            conn = new MySqlConnection(ConnStr);
            //Вызываем метод для заполнение метро Грида
            GetListstaffs();
            //Видимость полей в гриде
            metroGrid1.Columns[0].Visible = true;
            metroGrid1.Columns[1].Visible = true;
            metroGrid1.Columns[2].Visible = true;
            metroGrid1.Columns[3].Visible = true;
            metroGrid1.Columns[4].Visible = true;
            metroGrid1.Columns[5].Visible = true;
            //Ширина полей
            metroGrid1.Columns[0].FillWeight = 15;
            metroGrid1.Columns[1].FillWeight = 80;
            metroGrid1.Columns[2].FillWeight = 40;
            metroGrid1.Columns[3].FillWeight = 30;
            metroGrid1.Columns[4].FillWeight = 70;
            metroGrid1.Columns[5].FillWeight = 35;
            //Режим для полей "Только для чтения"
            //dataGridView1.Columns[0].ReadOnly = true;
            //dataGridView1.Columns[1].ReadOnly = true;
            //dataGridView1.Columns[2].ReadOnly = true;
            //dataGridView1.Columns[3].ReadOnly = true;
            //Растягивание полей грида
            metroGrid1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            metroGrid1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            metroGrid1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            metroGrid1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            metroGrid1.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            metroGrid1.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            //Убираем заголовки строк
            metroGrid1.RowHeadersVisible = false;
            //Показываем заголовки столбцов
            metroGrid1.ColumnHeadersVisible = true;
        }
        public void GetListstaffs()
        {
            //Запрос для вывода строк в БД
            string sql_sget_list = "SELECT ID_staff AS 'Код', full_name_of_the_staff AS 'ФИО', login AS 'логин', password AS 'пароль', job_title AS 'должность', wages AS 'зарплата' FROM staffs";
            //Открываем соединение
            conn.Open();
            //Объявляем команду, которая выполнить запрос в соединении conn
            MyDA.SelectCommand = new MySqlCommand(sql_sget_list, conn);
            //Заполняем таблицу записями из БД
            MyDA.Fill(table);
            //Указываем, что источником данных в bindingsource является заполненная выше таблица
            bSource.DataSource = table;
            //Указываем, что источником данных ДатаГрида является bindingsource 
            metroGrid1.DataSource = bSource;
            //Закрываем соединение
            conn.Close();
        }
        private void metroGrid1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //dataGridView1.CurrentCell = dataGridView1[e.ColumnIndex, e.RowIndex];
            //dataGridView1.CurrentRow.Selected = true;

            //Метод получения ID выделенной строки в глобальную переменную
            GetSelectedIDString();
        }
        private void metroGrid1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (!e.RowIndex.Equals(-1) && !e.ColumnIndex.Equals(-1) && e.Button.Equals(MouseButtons.Right))
            {
                metroGrid1.CurrentCell = metroGrid1[e.ColumnIndex, e.RowIndex];
                //dataGridView1.CurrentRow.Selected = true;
                metroGrid1.CurrentCell.Selected = true;
                //Метод получения ID выделенной строки в глобальную переменную
                GetSelectedIDString();
            }
        }

        private void metroGrid1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //Переменная для индекс выбранной строки в гриде
            string index_selected_rows;
            //Индекс выбранной строки
            index_selected_rows = metroGrid1.SelectedCells[0].RowIndex.ToString();
            //ID конкретной записи в Базе данных, на основании индекса строки
            id_selected_rows = metroGrid1.Rows[Convert.ToInt32(index_selected_rows)].Cells[0].Value.ToString();
            MessageBox.Show(id_selected_rows);
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            //Формируем строку запроса на добавление строк
            string sql_delete_staff = "DELETE FROM staffs WHERE ID_staff ='" + id_selected_rows + "'";
            //Посылаем запрос на обновление данных
            MySqlCommand delete_st = new MySqlCommand(sql_delete_staff, conn);
            try
            {
                conn.Open();
                delete_st.ExecuteNonQuery();
                MessageBox.Show("Удаление продажи прошло успешно", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка удаления продажи \n\n" + ex, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
            finally
            {
                conn.Close();
                //Вызов метода обновления ДатаГрида
                reload_list();
            }
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            DSControl dsc = new DSControl();
            MainControlClass.showControl(dsc, panel1);
        }

        private void metroButton3_Click(object sender, EventArgs e)
        {
            RSControl rsc = new RSControl();
            MainControlClass.showControl(rsc, panel1);
        }
    }
}
