namespace Printer
{
    partial class login_download
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle33 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle34 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle35 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle36 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle37 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle38 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle39 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle40 = new System.Windows.Forms.DataGridViewCellStyle();
            this.download = new System.Windows.Forms.Panel();
            this.mydata = new System.Windows.Forms.DataGridView();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.use_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.filename = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.copies = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.single_double = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.color = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ppt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.operation = new System.Windows.Forms.DataGridViewButtonColumn();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.open = new System.Windows.Forms.ToolStripMenuItem();
            this.刷新ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.刷新ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.帮助ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.版本信息ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fresh = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.login = new System.Windows.Forms.Panel();
            this.exitbutton = new System.Windows.Forms.Button();
            this.password = new System.Windows.Forms.TextBox();
            this.checkbox = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.username = new System.Windows.Forms.TextBox();
            this.error = new System.Windows.Forms.Label();
            this.loginbutton = new System.Windows.Forms.Button();
            this.un = new System.Windows.Forms.Label();
            this.pw = new System.Windows.Forms.Label();
            this.download.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mydata)).BeginInit();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.login.SuspendLayout();
            this.SuspendLayout();
            // 
            // download
            // 
            this.download.BackColor = System.Drawing.Color.Transparent;
            this.download.BackgroundImage = global::Printer.Properties.Resources._20130319010100681;
            this.download.Controls.Add(this.mydata);
            this.download.Controls.Add(this.menuStrip1);
            this.download.Controls.Add(this.pictureBox1);
            this.download.Location = new System.Drawing.Point(3, 12);
            this.download.Name = "download";
            this.download.Size = new System.Drawing.Size(1300, 360);
            this.download.TabIndex = 12;
            // 
            // mydata
            // 
            this.mydata.AllowUserToAddRows = false;
            this.mydata.BackgroundColor = System.Drawing.Color.PowderBlue;
            this.mydata.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.mydata.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.mydata.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle33.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle33.BackColor = System.Drawing.Color.PowderBlue;
            dataGridViewCellStyle33.Font = new System.Drawing.Font("微软雅黑", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle33.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle33.SelectionBackColor = System.Drawing.Color.PowderBlue;
            dataGridViewCellStyle33.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.mydata.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle33;
            this.mydata.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.mydata.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id,
            this.use_id,
            this.filename,
            this.copies,
            this.single_double,
            this.color,
            this.ppt,
            this.time,
            this.status,
            this.operation});
            this.mydata.EnableHeadersVisualStyles = false;
            this.mydata.GridColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.mydata.Location = new System.Drawing.Point(153, 39);
            this.mydata.Name = "mydata";
            this.mydata.RowHeadersVisible = false;
            this.mydata.RowTemplate.Height = 23;
            this.mydata.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.mydata.Size = new System.Drawing.Size(1101, 290);
            this.mydata.TabIndex = 12;
            // 
            // id
            // 
            dataGridViewCellStyle34.BackColor = System.Drawing.Color.PowderBlue;
            dataGridViewCellStyle34.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.id.DefaultCellStyle = dataGridViewCellStyle34;
            this.id.HeaderText = "序号";
            this.id.Name = "id";
            this.id.Width = 70;
            // 
            // use_id
            // 
            dataGridViewCellStyle35.BackColor = System.Drawing.Color.PowderBlue;
            dataGridViewCellStyle35.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.use_id.DefaultCellStyle = dataGridViewCellStyle35;
            this.use_id.HeaderText = "用户名";
            this.use_id.Name = "use_id";
            // 
            // filename
            // 
            dataGridViewCellStyle36.BackColor = System.Drawing.Color.PowderBlue;
            dataGridViewCellStyle36.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.filename.DefaultCellStyle = dataGridViewCellStyle36;
            this.filename.HeaderText = "文件名";
            this.filename.Name = "filename";
            this.filename.Width = 150;
            // 
            // copies
            // 
            dataGridViewCellStyle37.BackColor = System.Drawing.Color.PowderBlue;
            dataGridViewCellStyle37.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.copies.DefaultCellStyle = dataGridViewCellStyle37;
            this.copies.HeaderText = "份数";
            this.copies.Name = "copies";
            this.copies.Width = 70;
            // 
            // single_double
            // 
            dataGridViewCellStyle38.BackColor = System.Drawing.Color.PowderBlue;
            dataGridViewCellStyle38.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.single_double.DefaultCellStyle = dataGridViewCellStyle38;
            this.single_double.HeaderText = "单双";
            this.single_double.Name = "single_double";
            this.single_double.Width = 70;
            // 
            // color
            // 
            dataGridViewCellStyle39.BackColor = System.Drawing.Color.PowderBlue;
            dataGridViewCellStyle39.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.color.DefaultCellStyle = dataGridViewCellStyle39;
            this.color.HeaderText = "是否彩印";
            this.color.Name = "color";
            this.color.Width = 120;
            // 
            // ppt
            // 
            this.ppt.HeaderText = "ppt版式";
            this.ppt.Name = "ppt";
            this.ppt.Width = 150;
            // 
            // time
            // 
            this.time.HeaderText = "上传时间";
            this.time.Name = "time";
            this.time.Width = 150;
            // 
            // status
            // 
            this.status.HeaderText = "状态";
            this.status.Name = "status";
            // 
            // operation
            // 
            dataGridViewCellStyle40.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle40.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.operation.DefaultCellStyle = dataGridViewCellStyle40;
            this.operation.HeaderText = "操作";
            this.operation.Name = "operation";
            // 
            // menuStrip1
            // 
            this.menuStrip1.AutoSize = false;
            this.menuStrip1.BackColor = System.Drawing.Color.Transparent;
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.menuStrip1.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.open,
            this.刷新ToolStripMenuItem,
            this.帮助ToolStripMenuItem,
            this.fresh});
            this.menuStrip1.Location = new System.Drawing.Point(3, 6);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(213, 30);
            this.menuStrip1.TabIndex = 12;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // open
            // 
            this.open.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.open.Name = "open";
            this.open.Size = new System.Drawing.Size(67, 26);
            this.open.Text = "打开文件";
            // 
            // 刷新ToolStripMenuItem
            // 
            this.刷新ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.刷新ToolStripMenuItem1});
            this.刷新ToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.刷新ToolStripMenuItem.Name = "刷新ToolStripMenuItem";
            this.刷新ToolStripMenuItem.Size = new System.Drawing.Size(43, 26);
            this.刷新ToolStripMenuItem.Text = "工具";
            // 
            // 刷新ToolStripMenuItem1
            // 
            this.刷新ToolStripMenuItem1.Name = "刷新ToolStripMenuItem1";
            this.刷新ToolStripMenuItem1.Size = new System.Drawing.Size(122, 22);
            this.刷新ToolStripMenuItem1.Text = "自动刷新";
            // 
            // 帮助ToolStripMenuItem
            // 
            this.帮助ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.版本信息ToolStripMenuItem});
            this.帮助ToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.帮助ToolStripMenuItem.Name = "帮助ToolStripMenuItem";
            this.帮助ToolStripMenuItem.Size = new System.Drawing.Size(43, 26);
            this.帮助ToolStripMenuItem.Text = "帮助";
            // 
            // 版本信息ToolStripMenuItem
            // 
            this.版本信息ToolStripMenuItem.Name = "版本信息ToolStripMenuItem";
            this.版本信息ToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.版本信息ToolStripMenuItem.Text = "版本信息";
            // 
            // fresh
            // 
            this.fresh.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.fresh.Name = "fresh";
            this.fresh.Size = new System.Drawing.Size(43, 26);
            this.fresh.Text = "刷新";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Printer.Properties.Resources.未标题_1;
            this.pictureBox1.InitialImage = null;
            this.pictureBox1.Location = new System.Drawing.Point(3, 38);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(154, 131);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // login
            // 
            this.login.BackColor = System.Drawing.Color.Transparent;
            this.login.BackgroundImage = global::Printer.Properties.Resources._20130319010100681;
            this.login.Controls.Add(this.exitbutton);
            this.login.Controls.Add(this.password);
            this.login.Controls.Add(this.checkbox);
            this.login.Controls.Add(this.label1);
            this.login.Controls.Add(this.username);
            this.login.Controls.Add(this.error);
            this.login.Controls.Add(this.loginbutton);
            this.login.Controls.Add(this.un);
            this.login.Controls.Add(this.pw);
            this.login.Dock = System.Windows.Forms.DockStyle.Fill;
            this.login.Location = new System.Drawing.Point(0, 0);
            this.login.Name = "login";
            this.login.Size = new System.Drawing.Size(1284, 361);
            this.login.TabIndex = 11;
            // 
            // exitbutton
            // 
            this.exitbutton.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.exitbutton.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.exitbutton.Location = new System.Drawing.Point(171, 206);
            this.exitbutton.Name = "exitbutton";
            this.exitbutton.Size = new System.Drawing.Size(83, 35);
            this.exitbutton.TabIndex = 7;
            this.exitbutton.Text = "退出";
            this.exitbutton.UseVisualStyleBackColor = false;
            this.exitbutton.Click += new System.EventHandler(this.exitbutton_Click);
            // 
            // password
            // 
            this.password.Location = new System.Drawing.Point(125, 81);
            this.password.Name = "password";
            this.password.Size = new System.Drawing.Size(129, 21);
            this.password.TabIndex = 4;
            // 
            // checkbox
            // 
            this.checkbox.AutoSize = true;
            this.checkbox.BackColor = System.Drawing.Color.Transparent;
            this.checkbox.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.checkbox.Location = new System.Drawing.Point(26, 179);
            this.checkbox.Name = "checkbox";
            this.checkbox.Size = new System.Drawing.Size(75, 21);
            this.checkbox.TabIndex = 8;
            this.checkbox.Text = "记住密码";
            this.checkbox.UseVisualStyleBackColor = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(74, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(133, 38);
            this.label1.TabIndex = 0;
            this.label1.Text = "云印南天";
            // 
            // username
            // 
            this.username.Location = new System.Drawing.Point(125, 126);
            this.username.Name = "username";
            this.username.PasswordChar = '*';
            this.username.Size = new System.Drawing.Size(129, 21);
            this.username.TabIndex = 3;
            // 
            // error
            // 
            this.error.AutoSize = true;
            this.error.BackColor = System.Drawing.Color.Transparent;
            this.error.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.error.Location = new System.Drawing.Point(25, 155);
            this.error.Name = "error";
            this.error.Size = new System.Drawing.Size(0, 12);
            this.error.TabIndex = 5;
            // 
            // loginbutton
            // 
            this.loginbutton.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.loginbutton.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.loginbutton.Location = new System.Drawing.Point(26, 206);
            this.loginbutton.Name = "loginbutton";
            this.loginbutton.Size = new System.Drawing.Size(83, 35);
            this.loginbutton.TabIndex = 6;
            this.loginbutton.Text = "登录";
            this.loginbutton.UseVisualStyleBackColor = false;
            this.loginbutton.Click += new System.EventHandler(this.loginbutton_Click_1);
            // 
            // un
            // 
            this.un.AutoSize = true;
            this.un.BackColor = System.Drawing.Color.Transparent;
            this.un.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.un.Location = new System.Drawing.Point(23, 80);
            this.un.Name = "un";
            this.un.Size = new System.Drawing.Size(72, 27);
            this.un.TabIndex = 1;
            this.un.Text = "用户名";
            // 
            // pw
            // 
            this.pw.AutoSize = true;
            this.pw.BackColor = System.Drawing.Color.Transparent;
            this.pw.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pw.Location = new System.Drawing.Point(22, 122);
            this.pw.Name = "pw";
            this.pw.Size = new System.Drawing.Size(52, 27);
            this.pw.TabIndex = 2;
            this.pw.Text = "密码";
            // 
            // login_download
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Printer.Properties.Resources._20130319010100681;
            this.ClientSize = new System.Drawing.Size(1284, 361);
            this.Controls.Add(this.login);
            this.Controls.Add(this.download);
            this.Name = "login_download";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "云印南天";
            this.Load += new System.EventHandler(this.login_download_Load);
            this.download.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mydata)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.login.ResumeLayout(false);
            this.login.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel login;
        private System.Windows.Forms.Button exitbutton;
        private System.Windows.Forms.TextBox password;
        private System.Windows.Forms.CheckBox checkbox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox username;
        private System.Windows.Forms.Label error;
        private System.Windows.Forms.Button loginbutton;
        private System.Windows.Forms.Label un;
        private System.Windows.Forms.Label pw;
        private System.Windows.Forms.Panel download;
        private System.Windows.Forms.DataGridView mydata;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn use_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn filename;
        private System.Windows.Forms.DataGridViewTextBoxColumn copies;
        private System.Windows.Forms.DataGridViewTextBoxColumn single_double;
        private System.Windows.Forms.DataGridViewTextBoxColumn color;
        private System.Windows.Forms.DataGridViewTextBoxColumn ppt;
        private System.Windows.Forms.DataGridViewTextBoxColumn time;
        private System.Windows.Forms.DataGridViewTextBoxColumn status;
        private System.Windows.Forms.DataGridViewButtonColumn operation;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem open;
        private System.Windows.Forms.ToolStripMenuItem 刷新ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 刷新ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 帮助ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 版本信息ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fresh;
    }
}

