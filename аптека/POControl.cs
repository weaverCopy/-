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
    public partial class POControl : UserControl
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

        public POControl()
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
            GetListsuppliers();
        }

        //список производителей
        private void POControl_Load(object sender, EventArgs e)
        {
            //Переменная соединения
            conn = new MySqlConnection(ConnStr);
            //Вызываем метод для заполнение метро Грида
            GetListsuppliers();
            //Видимость полей в гриде
            metroGrid1.Columns[0].Visible = true;
            metroGrid1.Columns[1].Visible = true;
            metroGrid1.Columns[2].Visible = true;
            //Ширина полей
            metroGrid1.Columns[0].FillWeight = 15;
            metroGrid1.Columns[1].FillWeight = 50;
            metroGrid1.Columns[2].FillWeight = 50;
            //Растягивание полей грида
            metroGrid1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            metroGrid1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            metroGrid1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            //Убираем заголовки строк
            metroGrid1.RowHeadersVisible = false;
            //Показываем заголовки столбцов
            metroGrid1.ColumnHeadersVisible = true;
        }

        //Метод наполнения дата-грида
        public void GetListsuppliers()
        {
            //Запрос для вывода строк в БД
            string sql_sget_list = "SELECT id AS 'Код', name_of_suppliers AS 'Название производителя', country_suppliers AS 'Страна производителя' FROM suppliers";
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

        //кнопка "удаления производителя"
        private void metroButton2_Click(object sender, EventArgs e)
        {
            //перед тем как нажимать на кнопку "удалить производителя" необходимо выбрать ID производителя которого вы хотите удалить
            //Формируем строку запроса на добавление строк
            string sql_delete_sup = "DELETE FROM suppliers WHERE id ='" + id_selected_rows + "'";
            //Посылаем запрос на обновление данных
            MySqlCommand delete_sup = new MySqlCommand(sql_delete_sup, conn);
            try
            {
                conn.Open();
                delete_sup.ExecuteNonQuery();
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
        //кнопка "добавления производителя"
        private void metroButton1_Click(object sender, EventArgs e)
        {
            //при нажатитии на кнопку "добавить производителя" выходит контрол на котором находятся текстбоксы для редактирования даннных поставщика
            DPOControl dpoc = new DPOControl();
            MainControlClass.showControl(dpoc, panel1);
        }

        //кнопка "редактирования поставщика"
        private void metroButton3_Click(object sender, EventArgs e)
        {
            //при нажатитии на кнопку "редактировать производителя" выходит контрол на котором находятся текстбоксы для редактирования данных поставщика
            RPOControl rpoc = new RPOControl();
            MainControlClass.showControl(rpoc, panel1);
        }
    }
}
