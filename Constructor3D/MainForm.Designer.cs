﻿namespace Constructor3D
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.combobox_obj = new System.Windows.Forms.ComboBox();
            this.combobox_add = new System.Windows.Forms.ComboBox();
            this.button_add = new System.Windows.Forms.Button();
            this.button_raytrace = new System.Windows.Forms.Button();
            this.button_mod_raytrace = new System.Windows.Forms.Button();
            this.dgv = new System.Windows.Forms.DataGridView();
            this.del_btn = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox2
            // 
            this.pictureBox2.Location = new System.Drawing.Point(12, 36);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(720, 720);
            this.pictureBox2.TabIndex = 0;
            this.pictureBox2.TabStop = false;
            // 
            // combobox_obj
            // 
            this.combobox_obj.FormattingEnabled = true;
            this.combobox_obj.Location = new System.Drawing.Point(755, 59);
            this.combobox_obj.Name = "combobox_obj";
            this.combobox_obj.Size = new System.Drawing.Size(316, 50);
            this.combobox_obj.TabIndex = 1;
            this.combobox_obj.SelectedIndexChanged += new System.EventHandler(this.combobox_obj_SelectedIndexChanged);
            // 
            // combobox_add
            // 
            this.combobox_add.FormattingEnabled = true;
            this.combobox_add.Items.AddRange(new object[] {
            "Сфера",
            "Параллелепипед",
            "Куб",
            "Усеченная пирамида",
            "Источник света"});
            this.combobox_add.Location = new System.Drawing.Point(1090, 59);
            this.combobox_add.Name = "combobox_add";
            this.combobox_add.Size = new System.Drawing.Size(207, 50);
            this.combobox_add.TabIndex = 2;
            this.combobox_add.SelectedIndexChanged += new System.EventHandler(this.combobox_add_SelectedIndexChanged);
            // 
            // button_add
            // 
            this.button_add.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button_add.BackgroundImage")));
            this.button_add.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button_add.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button_add.Location = new System.Drawing.Point(1090, 123);
            this.button_add.Name = "button_add";
            this.button_add.Size = new System.Drawing.Size(207, 58);
            this.button_add.TabIndex = 3;
            this.button_add.Text = "Добавить";
            this.button_add.UseVisualStyleBackColor = true;
            this.button_add.Click += new System.EventHandler(this.button_add_Click);
            // 
            // button_raytrace
            // 
            this.button_raytrace.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button_raytrace.BackgroundImage")));
            this.button_raytrace.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button_raytrace.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button_raytrace.Location = new System.Drawing.Point(755, 698);
            this.button_raytrace.Name = "button_raytrace";
            this.button_raytrace.Size = new System.Drawing.Size(313, 58);
            this.button_raytrace.TabIndex = 5;
            this.button_raytrace.Text = "Отрисовать";
            this.button_raytrace.UseVisualStyleBackColor = true;
            this.button_raytrace.Click += new System.EventHandler(this.button_raytrace_Click);
            // 
            // button_mod_raytrace
            // 
            this.button_mod_raytrace.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button_mod_raytrace.BackgroundImage")));
            this.button_mod_raytrace.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button_mod_raytrace.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button_mod_raytrace.Location = new System.Drawing.Point(755, 595);
            this.button_mod_raytrace.Name = "button_mod_raytrace";
            this.button_mod_raytrace.Size = new System.Drawing.Size(316, 97);
            this.button_mod_raytrace.TabIndex = 6;
            this.button_mod_raytrace.Text = "Трасировка лучей";
            this.button_mod_raytrace.UseVisualStyleBackColor = true;
            this.button_mod_raytrace.Click += new System.EventHandler(this.button_mod_raytrace_Click);
            // 
            // dgv
            // 
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Location = new System.Drawing.Point(755, 211);
            this.dgv.Name = "dgv";
            this.dgv.RowHeadersWidth = 82;
            this.dgv.Size = new System.Drawing.Size(316, 150);
            this.dgv.TabIndex = 7;
            this.dgv.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CellContentClick);
            this.dgv.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CellValueChanged);
            // 
            // del_btn
            // 
            this.del_btn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("del_btn.BackgroundImage")));
            this.del_btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.del_btn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.del_btn.Location = new System.Drawing.Point(755, 123);
            this.del_btn.Name = "del_btn";
            this.del_btn.Size = new System.Drawing.Size(316, 58);
            this.del_btn.TabIndex = 8;
            this.del_btn.Text = "Удалить";
            this.del_btn.UseVisualStyleBackColor = true;
            this.del_btn.Click += new System.EventHandler(this.button1_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.saveFileDialog1_FileOk);
            // 
            // MainForm
            // 
            this.ClientSize = new System.Drawing.Size(1309, 772);
            this.Controls.Add(this.del_btn);
            this.Controls.Add(this.dgv);
            this.Controls.Add(this.button_mod_raytrace);
            this.Controls.Add(this.button_raytrace);
            this.Controls.Add(this.button_add);
            this.Controls.Add(this.combobox_add);
            this.Controls.Add(this.combobox_obj);
            this.Controls.Add(this.pictureBox2);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Name = "MainForm";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.ComboBox combobox_obj;
        private System.Windows.Forms.ComboBox combobox_add;
        private System.Windows.Forms.Button button_add;
        private System.Windows.Forms.Button button_raytrace;
        private System.Windows.Forms.Button button_mod_raytrace;
        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.Button del_btn;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
    }
}

