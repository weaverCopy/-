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
    public partial class PControl : UserControl
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

        public PControl()
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

        //автоматическое обновление дата-грида
        public void reload_list()
        {
            //Чистим виртуальную таблицу
            table.Clear();
            //Вызываем метод получения записей, который вновь заполнит таблицу
            GetListprocurements();
        }

        //список поставок
        private void PControl_Load(object sender, EventArgs e)
        {
            //Переменная соединения
            conn = new MySqlConnection(ConnStr);
            //Вызываем метод для заполнение метро Грида
            GetListprocurements();
            //Видимость полей в гриде
            metroGrid1.Columns[0].Visible = true;
            metroGrid1.Columns[1].Visible = true;
            metroGrid1.Columns[2].Visible = true;
            metroGrid1.Columns[3].Visible = true;
            metroGrid1.Columns[4].Visible = true;
            //Ширина полей
            metroGrid1.Columns[0].FillWeight = 15;
            metroGrid1.Columns[1].FillWeight = 50;
            metroGrid1.Columns[2].FillWeight = 50;
            metroGrid1.Columns[3].FillWeight = 50;
            metroGrid1.Columns[4].FillWeight = 50;
            //Растягивание полей грида
            metroGrid1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            metroGrid1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            metroGrid1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            metroGrid1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            metroGrid1.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            //Убираем заголовки строк
            metroGrid1.RowHeadersVisible = false;
            //Показываем заголовки столбцов
            metroGrid1.ColumnHeadersVisible = true;
        }

        //Метод наполнения дата-грида
        public void GetListprocurements()
        {
            //Запрос для вывода строк в БД
            string sql_sget_list = "SELECT ID_procurment AS 'Код', name_of_procurment AS 'Название', amount_of_medication AS 'Количество лекарства', the_cost_of_procurement AS 'Cтоимость закупки', order_date AS 'Дата поставки' FROM procurements";
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
        
        //Метод получения ID выделенной строки в глобальную переменную
        private void metroGrid1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            GetSelectedIDString();
        }
        //Метод получения ID выделенной строки в глобальную переменную
        private void metroGrid1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (!e.RowIndex.Equals(-1) && !e.ColumnIndex.Equals(-1) && e.Button.Equals(MouseButtons.Right))
            {
                metroGrid1.CurrentCell = metroGrid1[e.ColumnIndex, e.RowIndex];
                metroGrid1.CurrentCell.Selected = true;
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

        //кнопка "добавть лекарства"
        private void metroButton1_Click(object sender, EventArgs e)
        {
            //при нажатитии на кнопку "добавить поставку" выходит контрол на котором находятся текстбоксы для добавления поставки
            DPControl dpc = new DPControl();
            MainControlClass.showControl(dpc, panel1);
        }

        //кнопка "удалить поставку"
        private void metroButton2_Click(object sender, EventArgs e)
        {
            //перед тем как нажимать на кнопку "удалить поставку" необходимо выбрать ID поставки которое вы хотите удалить
            //Формируем строку запроса на добавление строк
            string sql_delete_procur = "DELETE FROM procurements WHERE ID_procurment ='" + id_selected_rows + "'";
            //Посылаем запрос на обновление данных
            MySqlCommand delete_procur = new MySqlCommand(sql_delete_procur, conn);
            try
            {
                conn.Open();
                delete_procur.ExecuteNonQuery();
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

        //кнопка "редактировать поставку"
        private void metroButton3_Click(object sender, EventArgs e)
        {
            //при нажатитии на кнопку "редактировать поставку" выходит контрол на котором находятся текстбоксы для редактирования поставки 
            RPControl rpc = new RPControl();
            MainControlClass.showControl(rpc, panel1);
        }
    }
}
