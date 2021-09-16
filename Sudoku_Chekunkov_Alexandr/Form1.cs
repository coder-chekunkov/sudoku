using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace Sudoku_Chekunkov_Alexandr
{
  public partial class Form1 : Form
  {
    //Объявление нужных переменных для работы программы:
    int level = 0;
    public Form1()
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
      var rand = new Random();
      this.BackgroundImage = Image.FromFile(@"pic/background_001.jpg");
    }

    //Метод, в котором отрабатывается нажатие на кнопку "ИГРАТЬ":
    private void pictureBox5_Click(object sender, EventArgs e)
    {
      //Установка уровня сложности, взависимости от выбора игрока
      //(легкий уровень сложности, нормальный уровень сложности, трудный уровень сложности):
      if (radioButton_easy.Checked) level = 1;
      if (radioButton_normal.Checked) level = 2;
      if (radioButton_hard.Checked) level = 3;

      //Анимация исчезновения формы, и переход на форму игры:
      for (int i = 0; i < 10; i++)
      {
        Thread.Sleep(20);
        this.Opacity = this.Opacity - 0.1;
      }
      this.Hide();
      Form2 FormGame = new Form2(level);
      FormGame.StartPosition = FormStartPosition.Manual;
      FormGame.Location = Location;
      FormGame.Show();
    }

    //Метод, в котором отрабатывается нажатие на кнопку "СТАТИСТИКА":
    private void button_statistic_Click(object sender, EventArgs e)
    {
      //Анимация исчезновения формы, и переход на форму статистика:
      for (int i = 0; i < 10; i++)
      {
        Thread.Sleep(20);
        this.Opacity = this.Opacity - 0.1;
      }
      this.Hide();
      Form3 FormStatistic = new Form3();
      FormStatistic.StartPosition = FormStartPosition.Manual;
      FormStatistic.Location = Location;
      FormStatistic.Show();
    }

    //Метод, в котором отрабатывается нажатие на кнопку "ИНФОРМАЦИЯ":
    private void button_info_Click(object sender, EventArgs e)
    {
      //Вывод диалогового окна с информацией для игрока:
      MessageBox.Show("Для того, чтобы начать игру выберите уровень сложности: " + "\r\n" +
        " " + "\r\n" +
        "                           один кулак - легкий уровень" + "\r\n" +
        "                           два кулака - нормальный уровень" + "\r\n" +
        "                           три кулака - сложный уровень" + "\r\n" +
        " " + "\r\n" +
        "Затем нажмите кнопку \u0022Играть\u0022 и начните игру.",
        "Как начать игру?");
    }

    //Метод, в котором обрабатывается нажатие на картинку с легким уровнем сложности:
    private void pictureBox1_Click(object sender, EventArgs e)
    {
      radioButton_easy.Checked = true;
    }

    //Метод, в котором обрабатывается нажатие на картинку с нормальным уровнем сложности:
    private void pictureBox2_Click(object sender, EventArgs e)
    {
      radioButton_normal.Checked = true;
    }

    //Метод, в котором обрабатывается нажатие на картинку с трудным уровнем сложности:
    private void pictureBox3_Click(object sender, EventArgs e)
    {
      radioButton_hard.Checked = true;
    }

    //Метод, в котором отрабатывается нажатие на кнопку "КРЕСТИК" 
    //и закрытие всего приложения:
    private void Form1_FormClosed(object sender, FormClosedEventArgs e)
    {
      Application.Exit();
    }

    private void Form1_Load(object sender, EventArgs e)
    {

    }
  }
}
