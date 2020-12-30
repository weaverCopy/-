using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace аптека
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            //при запуске программы сайдпанель автоматически находится на первой кнопке
            InitializeComponent();
            SidePanel.Height = metroButton1.Height;
            SidePanel.Top = metroButton1.Top;
        }

        public void metroButton1_Click(object sender, EventArgs e)
        {
            //при нажатии на кнопку "Главная" сайдпанель пермещается на нее
            SidePanel.Height = metroButton1.Height;
            SidePanel.Top = metroButton1.Top;
            //при нажатитии на кнопку "Главная" выходит контрол с информацией об аптеке
            MControl mc = new MControl();
            MainControlClass.showControl(mc, Content);
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            //при нажатии на кнопку "Лекарства" сайдпанель пермещается на нее
            SidePanel.Height = metroButton2.Height;
            SidePanel.Top = metroButton2.Top;
            //при нажатитии на кнопку "Лекарства" выходит контрол с дата-гридом где находистся вся информация о лекарствах в которой можно редактировать лекарства, удалять и добавлять новые
            LConrol lc = new LConrol();
            MainControlClass.showControl(lc, Content);
        }

        private void metroButton3_Click(object sender, EventArgs e)
        {
            //при нажатии на кнопку "Поставки" сайдпанель пермещается на нее
            SidePanel.Height = metroButton3.Height;
            SidePanel.Top = metroButton3.Top;
            //при нажатитии на кнопку "Поставки" выходит контрол с дата-гридом где находистся вся информация о поставках в которой можно редактировать поставки, удалять и добавлять новые
            PControl pc = new PControl();
            MainControlClass.showControl(pc, Content);
        }

        private void metroButton4_Click(object sender, EventArgs e)
        {
            //при нажатии на кнопку "Продажи" сайдпанель пермещается на нее
            SidePanel.Height = metroButton4.Height;
            SidePanel.Top = metroButton4.Top;
            //при нажатитии на кнопку "Продажи" выходит контрол с дата-гридом где находистся вся информация о продажах в которой можно редактировать продажи, удалять и добавлять новые
            PRControl prc = new PRControl();
            MainControlClass.showControl(prc, Content);
        }

        private void metroButton5_Click(object sender, EventArgs e)
        {
            //при нажатии на кнопку "Сотрудники" сайдпанель пермещается на нее
            SidePanel.Height = metroButton5.Height;
            SidePanel.Top = metroButton5.Top;
            //при нажатитии на кнопку "Сотрудники" выходит контрол с дата-гридом где находистся вся информация о сотрудниках в которой можно редактировать профиль сотрудника, удалить и добавить нового
            SControl sc = new SControl();
            MainControlClass.showControl(sc, Content);
        }

        private void metroButton6_Click(object sender, EventArgs e)
        {
            //при нажатии на кнопку "Поставщики" сайдпанель пермещается на нее
            SidePanel.Height = metroButton6.Height;
            SidePanel.Top = metroButton6.Top;
            //при нажатитии на кнопку "поставщики" выходит контрол с дата-гридом где находистся вся информация о поставщиках в которой можно редактировать постащиков, удалять и добавлять новых
            POControl poc = new POControl();
            MainControlClass.showControl(poc, Content);
        }

        //кнопка "Закрыть"
        private void metroButton7_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //кнопка "Свернуть"
        private void metroButton8_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void metroButton9_Click(object sender, EventArgs e)
        {

        }

        private void metroButton9_Click_1(object sender, EventArgs e)
        {

        }

        private void metroButton10_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                this.WindowState = FormWindowState.Maximized;
            }
            else
            {
                this.WindowState = FormWindowState.Normal;
            }
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                notifyIcon1.Icon = SystemIcons.Application;
                notifyIcon1.BalloonTipText = "Програма свернута";
                notifyIcon1.ShowBalloonTip(1000);
            }
            else if (this.WindowState == FormWindowState.Normal)
            {
                notifyIcon1.BalloonTipText = "Программа развернута";
                notifyIcon1.ShowBalloonTip(1000);
            }
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ShowInTaskbar = true;
            notifyIcon1.Visible = false;
            this.WindowState = FormWindowState.Normal;
        }

        private void SidePanel_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
