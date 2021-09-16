using DocumentFormat.OpenXml.Drawing.Charts;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Sudoku_Chekunkov_Alexandr
{
  public partial class Form3 : Form
  {
    public Form3()
    {
      InitializeComponent();
      //Анимация появления формы:
      Opacity = 0;
      Timer timer = new Timer();
      timer.Tick += new EventHandler((sender, e) =>
      {
        if ((Opacity += 0.05d) == 1) timer.Stop();
      });
      timer.Interval = 20;
      timer.Start();
      //Стандартные и неизмняемые настройки для формы 
      //(расширение формы, заголовок, задний фон, иконка):
      MaximizeBox = false;
      this.Text = "Судоку - головоломка с числами.";
      Icon icon = new Icon(@"pic/icon.ico");
      this.Icon = icon;
      this.BackgroundImage = Image.FromFile(@"pic/background_002.png");

      //Вызов метода получения данных из файлов:
      TakeInformation();
    }

    //Метод получения данных из файлов:
    public void TakeInformation()
    {
      string path = Environment.CurrentDirectory;
      //Считываем статистику о уровнях сложности:
      string[] lines_level = File.ReadAllLines($"{path}\\statistic_level.txt");
      //Считываем статистику о минутах:
      string[] lines_minute = File.ReadAllLines($"{path}\\statistic_minute.txt");
      //Считываем статистику о секундах:
      string[] lines_second = File.ReadAllLines($"{path}\\statistic_second.txt");
      //Считываем статистику об ошибках:
      string[] lines_mistake = File.ReadAllLines($"{path}\\statistic_mistake.txt");
      //Считываем статистику о дате:
      string[] lines_date = File.ReadAllLines($"{path}\\statistic_date.txt");

      int kol_statistic = lines_date.Length;

      //Добавление в таблицу статистики заголовки:
      for (int i = 1; i <= 5; i++)
      {
        Label lb = new Label();
        switch (i)
        {
          case 1:
            lb.Text = "№";
            break;
          case 2:
            lb.Text = "Сложность";
            break;
          case 3:
            lb.Text = "Время";
            break;
          case 4:
            lb.Text = "Ошибки";
            break;
          case 5:
            lb.Text = "Дата";
            break;
        }
        lb.Location = new Point(60 + 125 * i, 80);
        lb.AutoSize = false;
        lb.Size = new System.Drawing.Size(125, 25);
        lb.BackColor = Color.Transparent;
        lb.TextAlign = ContentAlignment.MiddleCenter;
        lb.BorderStyle = BorderStyle.FixedSingle;
        lb.Font = new Font("", 10F, FontStyle.Regular);
        this.Controls.Add(lb);
      }

      //Вывод в таблицу статистики номер строки:
      for (int i = 1; i <= lines_level.Length; i++)
      {
        if(i <= 8)
        {
          Label lb = new Label();
          lb.Text = i.ToString();
          lb.AutoSize = false;
          lb.Size = new System.Drawing.Size(125, 50);
          lb.Location = new Point(185, 50 + 50 * i);
          lb.TextAlign = ContentAlignment.MiddleCenter;
          lb.BorderStyle = BorderStyle.FixedSingle;
          lb.Font = new Font("", 10F, FontStyle.Regular);
          this.Controls.Add(lb);
        }

      }

      //Вывод в таблицу статистики уровень сложности:
      for (int i = 0; i < lines_level.Length; i++)
      {
        int isLevel = -1;
        Label lb = new Label();
        if(i < 8)
        {
          isLevel = Convert.ToInt32(lines_level[lines_level.Length - i - 1]);
          switch (isLevel)
          {
            case 1:
              lb.Text = "Легко";
              break;
            case 2:
              lb.Text = "Нормально";
              break;
            case 3:
              lb.Text = "Трудно";
              break;
          }
          lb.AutoSize = false;
          lb.Size = new System.Drawing.Size(125, 50);
          lb.Location = new Point(310, 100 + 50 * i);
          lb.TextAlign = ContentAlignment.MiddleCenter;
          lb.BorderStyle = BorderStyle.FixedSingle;
          lb.Font = new Font("", 10F, FontStyle.Regular);
          this.Controls.Add(lb);
        }
      }

      //Вывод в таблицу статистики время:
      for (int i = 0; i < lines_minute.Length; i++)
      {
        if(i < 8)
        {
          Label lb = new Label();
          string time = " ";
          if (Convert.ToInt32(lines_minute[lines_minute.Length - 1 - i]) < 10)
          {
            time = "0" + lines_minute[lines_minute.Length - 1 - i];
          }
          else
          {
            time = lines_minute[lines_minute.Length - 1 - i];
          }
          time = time + " : ";
          if (Convert.ToInt32(lines_second[lines_minute.Length - 1 - i]) < 10)
          {
            time = time + "0" + lines_second[lines_minute.Length - 1 - i];
          }
          else
          {
            time = time + lines_second[lines_minute.Length - 1 - i];
          }
          lb.Text = time;
          lb.AutoSize = false;
          lb.Size = new System.Drawing.Size(125, 50);
          lb.Location = new Point(435, 100 + 50 * i);
          lb.TextAlign = ContentAlignment.MiddleCenter;
          lb.BorderStyle = BorderStyle.FixedSingle;
          lb.Font = new Font("", 10F, FontStyle.Regular);
          this.Controls.Add(lb);
        }
      }

      //Вывод в таблицу статистики количество ошибок:
      for (int i = 0; i < lines_mistake.Length; i++)
      {
        if(i < 8)
        {
          Label lb = new Label();
          lb.Text = lines_mistake[lines_mistake.Length - 1 - i];
          lb.AutoSize = false;
          lb.Size = new System.Drawing.Size(125, 50);
          lb.Location = new Point(560, 100 + 50 * i);
          lb.TextAlign = ContentAlignment.MiddleCenter;
          lb.BorderStyle = BorderStyle.FixedSingle;
          lb.Font = new Font("", 10F, FontStyle.Regular);
          this.Controls.Add(lb);
        }
      }

      //Вывод в таблицу статистики дату:
      for (int i = 0; i < lines_mistake.Length; i++)
      {
        if(i < 8)
        {
          Label lb = new Label();
          lb.Text = lines_date[lines_date.Length - 1 - i];
          lb.AutoSize = false;
          lb.Size = new System.Drawing.Size(125, 50);
          lb.Location = new Point(685, 100 + 50 * i);
          lb.TextAlign = ContentAlignment.MiddleCenter;
          lb.BorderStyle = BorderStyle.FixedSingle;
          lb.Font = new Font("", 10F, FontStyle.Regular);
          this.Controls.Add(lb);
        }
      }
    }

    //Метод, в котором отрабатывается нажатие на кнопку "НАЗАД":
    private void button_back_Click(object sender, EventArgs e)
    {
      //Анимация исчезновения формы, и возращение в главное меню:
      for (int i = 0; i < 10; i++)
      {
        System.Threading.Thread.Sleep(20);
        this.Opacity = this.Opacity - 0.1;
      }
      this.Hide();
      Form1 FormMenu = new Form1();
      FormMenu.StartPosition = FormStartPosition.Manual;
      FormMenu.Location = Location;
      FormMenu.Show();
    }

    //Метод, в котором отрабатывается нажатие на кнопку "ИНФОРМАЦИЯ":
    private void button_info_Click(object sender, EventArgs e)
    {
      //Вывод диалогового окна с информацией для игрока:
      MessageBox.Show("Добро пожаловать в статистику! " + "\r\n" +
        "Здесь вы можете посмотреть статистику своих последних 8 игр: сложность игры, время, " +
        "затраченное на игру и количетсво ошибок.",
        "Статистика");
    }

    //Метод, в котором отрабатывается нажатие на кнопку "КРЕСТИК" 
    //и закрытие всего приложения:
    private void Form3_FormClosed(object sender, FormClosedEventArgs e)
    {
      Application.Exit();
    }

    private void Form3_Load(object sender, EventArgs e)
    {

    }
  }
}
