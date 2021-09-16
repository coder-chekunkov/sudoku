
namespace Sudoku_Chekunkov_Alexandr
{
  partial class Form3
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

      #region Windows Form Designer generated code

      /// <summary>
      /// Required method for Designer support - do not modify
      /// the contents of this method with the code editor.
      /// </summary>
      private void InitializeComponent()
    {
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form3));
      this.button_back = new System.Windows.Forms.PictureBox();
      this.pictureBox1 = new System.Windows.Forms.PictureBox();
      this.button_info = new System.Windows.Forms.PictureBox();
      ((System.ComponentModel.ISupportInitialize)(this.button_back)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.button_info)).BeginInit();
      this.SuspendLayout();
      // 
      // button_back
      // 
      this.button_back.BackColor = System.Drawing.Color.Transparent;
      this.button_back.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button_back.BackgroundImage")));
      this.button_back.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
      this.button_back.Location = new System.Drawing.Point(12, 12);
      this.button_back.Name = "button_back";
      this.button_back.Size = new System.Drawing.Size(25, 25);
      this.button_back.TabIndex = 2;
      this.button_back.TabStop = false;
      this.button_back.Click += new System.EventHandler(this.button_back_Click);
      // 
      // pictureBox1
      // 
      this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
      this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
      this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
      this.pictureBox1.Location = new System.Drawing.Point(460, 12);
      this.pictureBox1.Name = "pictureBox1";
      this.pictureBox1.Size = new System.Drawing.Size(80, 50);
      this.pictureBox1.TabIndex = 3;
      this.pictureBox1.TabStop = false;
      // 
      // button_info
      // 
      this.button_info.BackColor = System.Drawing.Color.Transparent;
      this.button_info.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button_info.BackgroundImage")));
      this.button_info.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
      this.button_info.Location = new System.Drawing.Point(947, 12);
      this.button_info.Name = "button_info";
      this.button_info.Size = new System.Drawing.Size(25, 25);
      this.button_info.TabIndex = 13;
      this.button_info.TabStop = false;
      this.button_info.Click += new System.EventHandler(this.button_info_Click);
      // 
      // Form3
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(984, 561);
      this.Controls.Add(this.button_info);
      this.Controls.Add(this.pictureBox1);
      this.Controls.Add(this.button_back);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.Name = "Form3";
      this.Text = "Form3";
      this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form3_FormClosed);
      this.Load += new System.EventHandler(this.Form3_Load);
      ((System.ComponentModel.ISupportInitialize)(this.button_back)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.button_info)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.PictureBox button_back;
    private System.Windows.Forms.PictureBox pictureBox1;
    private System.Windows.Forms.PictureBox button_info;
  }
}