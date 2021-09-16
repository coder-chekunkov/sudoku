using System;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace Sudoku_Chekunkov_Alexandr
{
  //Основной класс работы:
  public partial class Form2 : Form
  {
    //Объявление нужных переменных для работы программы:
    const int size = 3; int kol_hide = 0; int Ni, Nj;
    const int sizeField = 50; bool isFastCheck = false;
    int minute = 0, second = 0; int boof_level; int kol_mistake_fcp = 0;
      //Двумерный массив int для создания виртуального полей:
      int[,] field = new int[size * size, size * size];
      //Двумерный массив кнопок для создания и вывода полей:
      Button[,] fields = new Button[size * size, size * size];
    Font fc = new Font("", 20F, FontStyle.Italic);

    public Form2(int Nlevel)
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
      boof_level = Nlevel;
      //Включение таймера:
      timer_of_game.Interval = 1000;
      timer_of_game.Enabled = true;

      //Изменение картинки сложности игры и нужные настройки для каждой из сложностей:
      switch (Nlevel)
      {
        //Легкий уровень сложности:
        case 1:
          pictureBox_level.BackgroundImage = Image.FromFile(@"pic/hand.png");
          pictureBox_level.Location = new Point(485, 0);
          pictureBox_level.Size = new Size(25, 50);
          kol_hide = 30;
          break;
        //Нормальный уровень сложности:
        case 2:
          pictureBox_level.BackgroundImage = Image.FromFile(@"pic/two_hand.png");
          pictureBox_level.Location = new Point(475, 0);
          pictureBox_level.Size = new Size(50, 50);
          kol_hide = 40;
          break;
        //Трудный уровень сложности:
        case 3:
          pictureBox_level.BackgroundImage = Image.FromFile(@"pic/three_hand.png");
          pictureBox_level.Location = new Point(460, 0);
          pictureBox_level.Size = new Size(75, 50);
          kol_hide = 50;
          break;
        //Исключение в случае ошибки (ставим нормальный уровень сложности):
        default:
          pictureBox_level.BackgroundImage = Image.FromFile(@"pic/two_hand.png");
          pictureBox_level.Location = new Point(475, 0);
          pictureBox_level.Size = new Size(50, 50);
          kol_hide = 40;
          break;
      }
      //Вызов метода, в котором происходит генерация и мешка полей:
      GenerateField();
    }

    //Метод, в котором реализован таймер:
    private void timer_of_game_Tick(object sender, EventArgs e)
    {
      if(second < 59)
      {
        second++;
        if(second < 10 && minute < 10)
        {
          text_timer.Text = "0" + minute + " : 0" + second.ToString();
        }
        else
        {
          if(minute < 10)
          {
            text_timer.Text = "0" + minute + " : " + second.ToString();
          }
        }
      }
      else
      {
        if(minute < 59)
        {
          minute++;
          second = 0;
          if (second < 10 && minute < 10)
          {
            text_timer.Text = "0" + minute + " : 0" + second.ToString();
          }
          else
          {
            if (minute < 10)
            {
              text_timer.Text = "0" + minute + " : " + second.ToString();
            }
          }
        }
        else
        {
          minute = 0;
        }
      }
    }

    //Метод, в котором происходит генерация и перемешка полей:
    public void GenerateField()
    {
      for (int i = 0; i < size * size; i++)
      {
        for (int j = 0; j < size * size; j++)
        {
          //Заполняем двумерный массив виртуальных полей:
          field[i, j] = (i * size + i / size + j) % (size * size) + 1;
          //Добавляем в реальный двумерный массив полей кнопку с нужным id:
          fields[i, j] = new Button();
        }
      }
      //Перемешка полей (для лучшей перемешки используем цикл, который
      //мешает строки, столбцы и квадраты 10 раз):
      for(int i = 0; i < 10; i++)
      {
        TransponitionField();
        StringReplacementField();
        ColumnReplacementField();
        BlocksReplacementField();
      }
      //Вызов метода, который выводит на форму созданные поля:
      CreateField();
      //Вызов метода, который прячет поля для игры:
      HideField();
    }

    //Метод, в котором происходит транспанирование двумерного массива:
    public void TransponitionField()
    {
      int[,] Nfield = new int[size * size, size * size];
      for(int i = 0; i < size * size; i++)
      {
        for(int j = 0; j < size * size; j++)
        {
          Nfield[i, j] = field[j, i];
        }
      }
      field = Nfield;
    }

    //Метод, в котором мешаются две рядом стоящие строки:
    public void StringReplacementField()
    {
      Random rand = new Random();
      int block = rand.Next(0, size);
      int row1 = rand.Next(0, size);
      int row2 = rand.Next(0, size);
      int line1 = block * size + row1;
      while (row1 == row2)
        row2 = rand.Next(0, size);
      int line2 = block * size + row2;
      for(int i = 0; i < size * size; i++)
      {
        var t = field[line1, i];
        field[line1, i] = field[line2, i];
        field[line2, i] = t;
      }
    }
    //Метод, в котором мешаются два рядом стоящие столбца:
    public void ColumnReplacementField()
    {
      Random rand = new Random();
      int block = rand.Next(0, size);
      int row1 = rand.Next(0, size);
      int row2 = rand.Next(0, size);
      int line1 = block * size + row1;
      while (row1 == row2)
        row2 = rand.Next(0, size);
      int line2 = block * size + row2;
      for (int i = 0; i < size * size; i++)
      {
        var t = field[i, line1];
        field[i, line1] = field[i, line2];
        field[i, line2] = t;
      }
    }

    //Метод, в котором мешаются два рядом стоящие квадрата (3*3):
    public void BlocksReplacementField()
    {
      Random r = new Random();
      var block1 = r.Next(0, size);
      var block2 = r.Next(0, size);
      while (block1 == block2)
        block2 = r.Next(0, size);
      block1 *= size;
      block2 *= size;
      for (int i = 0; i < size * size; i++)
      {
        var k = block2;
        for (int j = block1; j < block1 + size; j++)
        {
          var t = field[j, i];
          field[j, i] = field[k, i];
          field[k, i] = t;
          k++;
        }
      }
    }

    //Метод, в котором происходит вывод на форму созданные поля:
    public void CreateField()
    {
      for (int i = 0; i < size * size; i++)
      {
        for (int j = 0; j < size * size; j++)
        {
          //Создание поля и его характеристики (цифра для игры, стиль поля,
          //задний фон, цвет цифры, шрифт цифры, изначальное расположение первого поля,
          //границы поля, их цвет и размер):
          Button button_field = new Button();
          fields[i, j] = button_field;
          fields[i, j].Name = Convert.ToString(i) + " " + Convert.ToString(j);
          fields[i, j].Click += new System.EventHandler(this.onFieldClick);
          button_field.Size = new Size(sizeField, sizeField);
          button_field.Text = field[i, j].ToString();
          button_field.FlatStyle = FlatStyle.Flat;
          button_field.ForeColor = Color.FromArgb(0, 0, 0);
          button_field.BackColor = Color.FromArgb(250, 250, 250);
          button_field.Font = new Font("", 18F, FontStyle.Bold);
          button_field.Location = new Point(j * sizeField + 175, i * sizeField + 65);
          button_field.FlatAppearance.BorderColor = Color.FromArgb(51, 51, 51);
          button_field.FlatAppearance.BorderSize = 1;
          this.Controls.Add(button_field);
        }
      }
    }

    //Метод, который прячет поля для игры:
    public void HideField()
    {
      Random r = new Random();
      //Идем до тех пор, пока не спрячется нужное количество полей
      //(легкий уровень сложности - 30, нормальный уровень сложности - 40, трудный уровень сложности - 50):
      while (kol_hide > 0)
      {
        for (int i = 0; i < size * size; i++)
        {
          for (int j = 0; j < size * size; j++)
          {
            if (!string.IsNullOrEmpty(fields[i, j].Text))
            {
              int a = r.Next(0, 3);
              fields[i, j].Text = a == 0 ? "" : fields[i, j].Text;
              if (a == 0)
                kol_hide--;
              if (kol_hide <= 0)
                break;
            }
          }
          if (kol_hide <= 0)
            break;
        }
      }

      //Цикл, который делает не спрятанный поля не кликабельными:
      for (int i = 0; i < size * size; i++)
      {
        for (int j = 0; j < size * size; j++)
        {
          if (fields[i, j].Text != "")
          {
            fields[i, j].Enabled  = false;
          }
        }
      }
    }

    //Метод, в котором происходит проверка игры:
    public void CheckGame()
    {
      //Покарска всех полей в стандартный цвет:
      for (int i = 0; i < size * size; i++)
      {
        for (int j = 0; j < size * size; j++)
        {
          fields[i, j].BackColor = Color.FromArgb(250, 250, 250);
        }
      }

      //Остановка таймера:
      timer_of_game.Enabled = false;

      //Цикл, который подсчитывает количетсво ошибок и
      //Окаршивает в нужный цвет поле, в зависимости от ответа
      //(неправильно - красный, правильно - зеленый):
      int kol_mistake = 0;
      for(int i = 0; i < size * size; i++)
      {
        for(int j = 0; j < size * size; j++)
        {
          if(fields[i, j].Text != "")
          {
            int iF = Convert.ToInt32(fields[i, j].Text);
            //Неправильный ответ:
            if (field[i, j] != iF)
            {
              kol_mistake++;
              fields[i, j].Enabled = false;
              fields[i, j].BackColor = Color.FromArgb(227, 109, 109);
              fields[i, j].Text = Convert.ToString(field[i, j]);
            }
            //Правильный ответ:
            if (field[i, j] == iF && fields[i,j].Enabled != false)
            {
              fields[i, j].Enabled = false;
              fields[i, j].BackColor = Color.FromArgb(109, 227, 117);
              fields[i, j].Text = Convert.ToString(field[i, j]);
            }

          }
          else
          {
            //Неправильный ответ, если поле осталось пустым:
            kol_mistake++;
            fields[i, j].Enabled = false;
            fields[i, j].BackColor = Color.FromArgb(227, 109, 109);
            fields[i, j].Text = Convert.ToString(field[i, j]);
          }
        }
      }
      //Использвуем количетсво ошибок, если было включена функция "Быстрая проверка прогресса":
      if(kol_mistake_fcp != 0)
      {
        kol_mistake = kol_mistake_fcp;
      }

      //Если все ответы правильные, вывод окна с поздравлениями,
      //и кнопкой, которая предлагает вернуться в главное меню:
      if(kol_mistake == 0)
      {
       DialogResult result = MessageBox.Show(
      "Поздравляем! Вы решили все правильно." + "\r\n" + "Нажмите " + '\u0022' +"ОК" + '\u0022' +" и начните новую игру."
      + "\r\n" + "Ваше время: " + minute.ToString() + " минут(а) и " + second.ToString() + " секунд(а)", 
      "Поздравляем!",
      MessageBoxButtons.OK,
      MessageBoxIcon.Exclamation);
        if (result == DialogResult.OK)
        {
          //Анимация исчезновения формы, и возращение в главное меню:
          for (int i = 0; i < 10; i++)
          {
            Thread.Sleep(20);
            this.Opacity = this.Opacity - 0.1;
          }
          this.Hide();
          Form1 FormMenu = new Form1();
          FormMenu.StartPosition = FormStartPosition.Manual;
          FormMenu.Location = Location;
          FormMenu.Show();
        }
      }
      //Если есть неправильные ответы, вывод окна с поздравлениями,
      //и кнопкой, которая предлагает вернуться в главное меню:
      else
      {
       DialogResult result = MessageBox.Show(
      "Вы ошиблись  " + Convert.ToString(kol_mistake) + " раз." + "\r\n" + "Нажмите " 
      + '\u0022' + "ОК" + '\u0022' + ", изучите свои ошибки и начните новую игру."
      + "\r\n" + "Ваше время: " + minute.ToString() + " минут(а) и " + second.ToString() + " секунд(а)", 
      "У Вас ошибки!",
      MessageBoxButtons.OK,
      MessageBoxIcon.Exclamation);
        if (result == DialogResult.OK)
        {
          //Анимация исчезновения формы, и возращение в главное меню:
          for (int i = 0; i < 10; i++)
          {
            Thread.Sleep(20);
            this.Opacity = this.Opacity - 0.1;
          }
          this.Hide();
          Form1 FormMenu = new Form1();
          FormMenu.StartPosition = FormStartPosition.Manual;
          FormMenu.Location = Location;
          FormMenu.Show();
        }
      }
      //Создаем статистику:
      MakeNewStatistics mks = new MakeNewStatistics(boof_level, minute, second, kol_mistake);
      mks.MakeFiles(); 
    }

    //Метод, в котором происходит выделение строки, столбца и квадрата, 
    //взависимости от выбранного поля:
    public void HighlightingFields()
    {
      //Выделяем все поля, находящиеся в нужном столбце:
      for(int i = 0; i < size * size; i++)
      {
        fields[i, Nj].BackColor = Color.FromArgb(46, 137, 201);
      }
      //Выделяем все поля, находящиеся в нужной строке:
      for (int j = 0; j < size * size; j++)
      {
        fields[Ni, j].BackColor = Color.FromArgb(201, 194, 46);
      }

      //Выделяем квадрат под номером "1":
      if((Ni == 0 || Ni == 1 || Ni == 2) && (Nj == 0 || Nj == 1 || Nj == 2))
      {
        for(int i = 0; i < 3; i++)
        {
          for (int j = 0; j < 3; j++)
          {
            fields[i, j].BackColor = Color.FromArgb(222, 102, 51);
          }
        }
      }

      //Выделяем квадрат под номером "2":
      if ((Ni == 0 || Ni == 1 || Ni == 2) && (Nj == 3 || Nj == 4 || Nj == 5))
      {
        for (int i = 0; i < 3; i++)
        {
          for (int j = 3; j < 6; j++)
          {
            fields[i, j].BackColor = Color.FromArgb(222, 102, 51);
          }
        }
      }

      //Выделяем квадрат под номером "3":
      if ((Ni == 0 || Ni == 1 || Ni == 2) && (Nj == 6 || Nj == 7 || Nj == 8))
      {
        for (int i = 0; i < 3; i++)
        {
          for (int j = 6; j < 9; j++)
          {
            fields[i, j].BackColor = Color.FromArgb(222, 102, 51);
          }
        }
      }

      //Выделяем квадрат под номером "4":
      if ((Ni == 3 || Ni == 4 || Ni == 5) && (Nj == 0 || Nj == 1 || Nj == 2))
      {
        for (int i = 3; i < 6; i++)
        {
          for (int j = 0; j < 3; j++)
          {
            fields[i, j].BackColor = Color.FromArgb(222, 102, 51);
          }
        }
      }

      //Выделяем квадрат под номером "5":
      if ((Ni == 3 || Ni == 4 || Ni == 5) && (Nj == 3 || Nj == 4 || Nj == 5))
      {
        for (int i = 3; i < 6; i++)
        {
          for (int j = 3; j < 6; j++)
          {
            fields[i, j].BackColor = Color.FromArgb(222, 102, 51);
          }
        }
      }

      //Выделяем квадрат под номером "6":
      if ((Ni == 3 || Ni == 4 || Ni == 5) && (Nj == 6 || Nj == 7 || Nj == 8))
      {
        for (int i = 3; i < 6; i++)
        {
          for (int j = 6; j < 9; j++)
          {
            fields[i, j].BackColor = Color.FromArgb(222, 102, 51);
          }
        }
      }

      //Выделяем квадрат под номером "7":
      if ((Ni == 6 || Ni == 7 || Ni == 8) && (Nj == 0 || Nj == 1 || Nj == 2))
      {
        for (int i = 6; i < 9; i++)
        {
          for (int j = 0; j < 3; j++)
          {
            fields[i, j].BackColor = Color.FromArgb(222, 102, 51);
          }
        }
      }

      //Выделяем квадрат под номером "8":
      if ((Ni == 6 || Ni == 7 || Ni == 8) && (Nj == 3 || Nj == 4 || Nj == 5))
      {
        for (int i = 6; i < 9; i++)
        {
          for (int j = 3; j < 6; j++)
          {
            fields[i, j].BackColor = Color.FromArgb(222, 102, 51);
          }
        }
      }

      //Выделяем квадрат под номером "9":
      if ((Ni == 6 || Ni == 7 || Ni == 8) && (Nj == 6 || Nj == 7 || Nj == 8))
      {
        for (int i = 6; i < 9; i++)
        {
          for (int j = 6; j < 9; j++)
          {
            fields[i, j].BackColor = Color.FromArgb(222, 102, 51);
          }
        }
      }
    }

    //Метод, который выводит информацию о том, что такое "Быстрая проверка прогресса",
    //в случае если была нажата нужная кнопка:
    private void button_information_fast_check_Click(object sender, EventArgs e)
    {
      DialogResult result = MessageBox.Show(
      "Быстрая проверка прогресса игры позволяет Вам сразу узнать, правильно ли Вы решили " +
      "выбранное поле. " + "\r\n" + " " + "\r\n" + "Если Вы включили данную функцию, Вы не сможете закончить игру, " +
      "пока все поля не будут заполнены." + "\r\n" + " " + "\r\n" +
      "В любой момент игры Вы можете отключить данную функцию, но проверка всех заполненных полей исчезнит.",
      "Быстрая проверка прогресса.",
      MessageBoxButtons.OK,
      MessageBoxIcon.Information);
    }

    //Метод, в котором происходит включение и выключение "Быстрой проверки прогресса":
    private void button_fast_check_CheckedChanged_1(object sender, EventArgs e)
    {
      //"Быстрая проверка прогресса" включена,
      //исчезновение основной кнопки "ПРОВЕРИТЬ":
      if (button_fast_check.Checked == true)
      {
        button_check_game.Visible = false;
        isFastCheck = true;
      }
      //"Быстрая проверка прогресса" выключена,
      //появление основной кнопки "ПРОВЕРИТЬ" и покраска всех полей в стандартный цвет:
      else
      {
        button_check_game.Visible = true;
        isFastCheck = false;
        for (int i = 0; i < size * size; i++)
        {
          for (int j = 0; j < size * size; j++)
          {
            fields[i, j].BackColor = Color.FromArgb(250, 250, 250);
          }
        }
      }
    }

    //Метод, в котором происходит "Быстрая проверка прогресса":
    public void FastCheck()
    {
      //Выбранное число для поля верно:
      if(fields[Ni, Nj].Text == field[Ni, Nj].ToString())
      {
        //Покраска поля в зеленый цвет:
        fields[Ni, Nj].BackColor = Color.FromArgb(109, 227, 117);
      }
      //Выбранное число для поля неверно:
      if (fields[Ni, Nj].Text != field[Ni, Nj].ToString())
      {
        //Покраска поля в красный цвет:
        fields[Ni, Nj].BackColor = Color.FromArgb(227, 109, 109);
        kol_mistake_fcp++;
      }
      //Введена пустота:
      if(fields[Ni, Nj].Text == "")
      {
        //Покраска поля в стандртный цвет:
        fields[Ni, Nj].BackColor = Color.FromArgb(200, 200, 200);
      }

      //Проверка остались ли пустные поля:
      bool isEmptyFields = false;
      for (int i = 0; i < size * size; i++)
      {
        for (int j = 0; j < size * size; j++)
        {
          if (fields[i, j].Text == "")
          {
            isEmptyFields = true;
          }
        }
      }

      //Если пустых полей нет - вызов метода проверки всей игры:
      if(isEmptyFields == false)
      {
        CheckGame();
      }
    }

    //Метод, в котором происходит запоминание id выбранной кнопки:
    public void onFieldClick(object sender, EventArgs e)
    {
      //Покраска всех полей в стандартный цвет:
      for (int i = 0; i < size * size; i++)
      {
        for (int j = 0; j < size * size; j++)
        {
          fields[i, j].BackColor = Color.FromArgb(250, 250, 250);
        }
      }
      //Получение id выбранной кнопки:
      string s = ((Button)sender).Name;
      string[] mas_s = new string[2];
      mas_s = s.Split(' '); 
      //Координаты по Y:
      Ni = Convert.ToInt32(mas_s[0]);
      //Координаты по X:
      Nj = Convert.ToInt32(mas_s[1]);
      HighlightingFields();
    }

    //Метод, в котором записывается "1" в выбранное поле,
    //и вызов метода "Быстрой проверки прогресса", если он включен:
    private void button_one_Click(object sender, EventArgs e)
    {
      fields[Ni, Nj].Text = "1";
      fields[Ni, Nj].Font = fc;
      if(isFastCheck == true) FastCheck();
    }

    //Метод, в котором записывается "2" в выбранное поле,
    //и вызов метода "Быстрой проверки прогресса", если он включен:
    private void button_two_Click(object sender, EventArgs e)
    {
      fields[Ni, Nj].Text = "2";
      fields[Ni, Nj].Font = fc;
      if (isFastCheck == true) FastCheck();
    }

    //Метод, в котором записывается "3" в выбранное поле,
    //и вызов метода "Быстрой проверки прогресса", если он включен:
    private void button_three_Click(object sender, EventArgs e)
    {
      fields[Ni, Nj].Text = "3";
      fields[Ni, Nj].Font = fc;
      if (isFastCheck == true) FastCheck();
    }

    //Метод, в котором записывается "4" в выбранное поле,
    //и вызов метода "Быстрой проверки прогресса", если он включен:
    private void button_four_Click(object sender, EventArgs e)
    {
      fields[Ni, Nj].Text = "4";
      fields[Ni, Nj].Font = fc;
      if (isFastCheck == true) FastCheck();
    }

    //Метод, в котором записывается "5" в выбранное поле,
    //и вызов метода "Быстрой проверки прогресса", если он включен:
    private void button_five_Click(object sender, EventArgs e)
    {
      fields[Ni, Nj].Text = "5";
      fields[Ni, Nj].Font = fc;
      if (isFastCheck == true) FastCheck();
    }

    //Метод, в котором записывается "6" в выбранное поле,
    //и вызов метода "Быстрой проверки прогресса", если он включен:
    private void button_six_Click(object sender, EventArgs e)
    {
      fields[Ni, Nj].Text = "6";
      fields[Ni, Nj].Font = fc;
      if (isFastCheck == true) FastCheck();
    }

    //Метод, в котором записывается "7" в выбранное поле,
    //и вызов метода "Быстрой проверки прогресса", если он включен:
    private void button_eleven_Click(object sender, EventArgs e)
    {
      fields[Ni, Nj].Text = "7";
      fields[Ni, Nj].Font = fc;
      if (isFastCheck == true) FastCheck();
    }

    //Метод, в котором записывается "8" в выбранное поле,
    //и вызов метода "Быстрой проверки прогресса", если он включен:
    private void button_eight_Click(object sender, EventArgs e)
    {
      fields[Ni, Nj].Text = "8";
      fields[Ni, Nj].Font = fc;
      if (isFastCheck == true) FastCheck();
    }

    //Метод, в котором записывается "9" в выбранное поле,
    //и вызов метода "Быстрой проверки прогресса", если он включен:
    private void button_nine_Click(object sender, EventArgs e)
    {
      fields[Ni, Nj].Text = "9";
      fields[Ni, Nj].Font = fc;
      if (isFastCheck == true) FastCheck();
    }

    //Метод, в котором записывается " " в выбранное поле,
    //и вызов метода "Быстрой проверки прогресса", если он включен:
    private void button_empty_Click(object sender, EventArgs e)
    {
      fields[Ni, Nj].Text = "";
      fields[Ni, Nj].Font = fc;
      if (isFastCheck == true) FastCheck();
    }

    //Метод, в котором отрабатывается нажатие на кнопку "ПРОВЕРИТЬ", если
    //не включена "Быстрая проверка прогресса":
    private void button_check_Click_1(object sender, EventArgs e)
    {
      //Проверяем, есть ли не заполненные поля:
      int isEmpty = 0;
      for (int i = 0; i < size * size; i++)
      {
        for (int j = 0; j < size * size; j++)
        {
          if (fields[i, j].Text == "")
          {
            isEmpty = 1;
          }
        }
      }

      //Если есть не заполненные поля: 
      if (isEmpty != 0)
      {
        //Вывод диалогового окна, в котором сообщается о том,
        //что у игрока есть не заполненные поля:
        DialogResult result = MessageBox.Show(
        "У вас есть не заполненные клетки.  " + "\r\n" + "Вы уверены, что хотите проверить себя?",
        "Проверить?",
        MessageBoxButtons.YesNo,
        MessageBoxIcon.Warning);
        //Если игрок хочет все равно хочет проверить свои наработки,
        //вывод метода, который проверяет все поля, если не включена "Быстрая проверка прогресса":
        if (result == DialogResult.Yes)
        {
          CheckGame();
        }
      }
      //Если есть не заполненных полей нет: 
      else
      {
        //Вывод диалогового окна, в котором сообщается о том,
        //что у игрока есть все поля заполненны:
        DialogResult result = MessageBox.Show(
       "Вы заполнили все клетки.  " + "\r\n" + "Вы уверены, что хотите проверить себя?",
       "Проверить?",
       MessageBoxButtons.YesNo,
       MessageBoxIcon.Warning);
        //Если игрок хочет все равно хочет проверить свои наработки,
        //вывод метода, который проверяет все поля, если не включена "Быстрая проверка прогресса":
        if (result == DialogResult.Yes)
        {
          CheckGame();
        }
      }
    }

    //Метод, в котором отрабатывается нажатие на кнопку "НАЗАД":
    private void button_back_Click(object sender, EventArgs e)
    {
       //Вывод диалогового окна, в котором сообщается о том,
       //что игрок может вернуться назад:
       DialogResult result = MessageBox.Show(
       "Все ваши наработки будут утерянны.  " + "\r\n" + "Вы уверены, что хотите вернуться назад?",
       "Вернуться назад?",
       MessageBoxButtons.YesNo,
       MessageBoxIcon.Question);
      //Нажата кнопка "ВЕРНУТЬСЯ НАЗАД":
      if (result == DialogResult.Yes)
      {
        //Анимация исчезновения формы, и возращение в главное меню:
        for (int i = 0; i < 10; i++)
        {
          Thread.Sleep(20);
          this.Opacity = this.Opacity - 0.1;
        }
        this.Hide();
        Form1 FormMenu = new Form1();
        FormMenu.StartPosition = FormStartPosition.Manual;
        FormMenu.Location = Location;
        FormMenu.Show();
      }
    }

    //Метод, в котором отрабатывается нажатие на кнопку "КРЕСТИК" 
    //и закрытие всего приложения:
    private void Form2_FormClosed(object sender, FormClosedEventArgs e)
    {
      Application.Exit();
    }

    private void Form2_Load(object sender, EventArgs e)
    {

    }
  }

  //Класс добавления статистики:
  class MakeNewStatistics
  {
    private int Slevel;
    private int Sminute, Ssecond;
    private int Smistakes;
    private string Sdate;

    //Переопределяем переменные для записи в статистику
    //(уровень сложности, минуты, затраченные на игру,
    //секунды, затраченные на игру, количетсво ошибок):
    public MakeNewStatistics(int a, int b, int c, int d)
    {
      Slevel = a; Sminute = b; Ssecond = c; Smistakes = d;
      Sdate = DateTime.Now.ToString();
    }

    //Метод, который создает файлы для хранения всех переменных:
    public void MakeFiles()
    {
      //Создаем файл для записи уровни сложности:
      string fileName_level = System.IO.Path.Combine(Environment.CurrentDirectory, "statistic_level.txt");
      File.AppendAllText(fileName_level, Slevel + Environment.NewLine);
      //Создаем файл для записи минут:
      string fileName_minute = System.IO.Path.Combine(Environment.CurrentDirectory, "statistic_minute.txt");
      File.AppendAllText(fileName_minute, Sminute + Environment.NewLine);
      //Создаем файл для записи секунд:
      string fileName_second = System.IO.Path.Combine(Environment.CurrentDirectory, "statistic_second.txt");
      File.AppendAllText(fileName_second, Ssecond + Environment.NewLine);
      //Создаем файл для записи количества ошибок:
      string fileName_mistake = System.IO.Path.Combine(Environment.CurrentDirectory, "statistic_mistake.txt");
      File.AppendAllText(fileName_mistake, Smistakes + Environment.NewLine);
      //Создаем файл для записи даты игры:
      string fileName_date = System.IO.Path.Combine(Environment.CurrentDirectory, "statistic_date.txt");
      File.AppendAllText(fileName_date, Sdate + Environment.NewLine);
    }
  }
}
