namespace SpeedLimitTaller
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.LogoLogin = new System.Windows.Forms.PictureBox();
            this.textoIniciarSesion = new System.Windows.Forms.Label();
            this.textoContrasena = new System.Windows.Forms.Label();
            this.CajaUsuariosLogin = new System.Windows.Forms.TextBox();
            this.PassLogin = new System.Windows.Forms.TextBox();
            this.btnIniciarSesion = new System.Windows.Forms.Button();
            this.btnSalir = new System.Windows.Forms.Button();
            this.imgUser = new System.Windows.Forms.PictureBox();
            this.imgPass = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.LogoLogin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgUser)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgPass)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // LogoLogin
            // 
            this.LogoLogin.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.LogoLogin.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("LogoLogin.BackgroundImage")));
            this.LogoLogin.Location = new System.Drawing.Point(320, -29);
            this.LogoLogin.Name = "LogoLogin";
            this.LogoLogin.Size = new System.Drawing.Size(497, 300);
            this.LogoLogin.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.LogoLogin.TabIndex = 1;
            this.LogoLogin.TabStop = false;
            this.LogoLogin.MouseDown += new System.Windows.Forms.MouseEventHandler(this.LogoLogin_MouseDown);
            // 
            // textoIniciarSesion
            // 
            this.textoIniciarSesion.AutoSize = true;
            this.textoIniciarSesion.Font = new System.Drawing.Font("Russo One", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textoIniciarSesion.Location = new System.Drawing.Point(533, 298);
            this.textoIniciarSesion.Name = "textoIniciarSesion";
            this.textoIniciarSesion.Size = new System.Drawing.Size(133, 29);
            this.textoIniciarSesion.TabIndex = 2;
            this.textoIniciarSesion.Text = "USUARIO:";
            // 
            // textoContrasena
            // 
            this.textoContrasena.AutoSize = true;
            this.textoContrasena.Font = new System.Drawing.Font("Russo One", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textoContrasena.Location = new System.Drawing.Point(509, 419);
            this.textoContrasena.Name = "textoContrasena";
            this.textoContrasena.Size = new System.Drawing.Size(185, 29);
            this.textoContrasena.TabIndex = 3;
            this.textoContrasena.Text = "CONTRASEÑA:";
            // 
            // CajaUsuariosLogin
            // 
            this.CajaUsuariosLogin.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.CajaUsuariosLogin.Font = new System.Drawing.Font("Russo One", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CajaUsuariosLogin.Location = new System.Drawing.Point(376, 349);
            this.CajaUsuariosLogin.Multiline = true;
            this.CajaUsuariosLogin.Name = "CajaUsuariosLogin";
            this.CajaUsuariosLogin.Size = new System.Drawing.Size(441, 35);
            this.CajaUsuariosLogin.TabIndex = 4;
            // 
            // PassLogin
            // 
            this.PassLogin.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.PassLogin.Font = new System.Drawing.Font("Russo One", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PassLogin.Location = new System.Drawing.Point(376, 463);
            this.PassLogin.Multiline = true;
            this.PassLogin.Name = "PassLogin";
            this.PassLogin.PasswordChar = '*';
            this.PassLogin.Size = new System.Drawing.Size(441, 35);
            this.PassLogin.TabIndex = 5;
            // 
            // btnIniciarSesion
            // 
            this.btnIniciarSesion.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(157)))), ((int)(((byte)(157)))));
            this.btnIniciarSesion.FlatAppearance.BorderSize = 0;
            this.btnIniciarSesion.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnIniciarSesion.Font = new System.Drawing.Font("Russo One", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnIniciarSesion.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnIniciarSesion.Location = new System.Drawing.Point(376, 552);
            this.btnIniciarSesion.Name = "btnIniciarSesion";
            this.btnIniciarSesion.Size = new System.Drawing.Size(200, 40);
            this.btnIniciarSesion.TabIndex = 6;
            this.btnIniciarSesion.Text = "INGRESAR";
            this.btnIniciarSesion.UseVisualStyleBackColor = false;
            this.btnIniciarSesion.Click += new System.EventHandler(this.btnIniciarSesion_Click);
            // 
            // btnSalir
            // 
            this.btnSalir.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(157)))), ((int)(((byte)(157)))));
            this.btnSalir.FlatAppearance.BorderSize = 0;
            this.btnSalir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSalir.Font = new System.Drawing.Font("Russo One", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSalir.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnSalir.Location = new System.Drawing.Point(617, 552);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(200, 40);
            this.btnSalir.TabIndex = 7;
            this.btnSalir.Text = "SALIR";
            this.btnSalir.UseVisualStyleBackColor = false;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // imgUser
            // 
            this.imgUser.Image = ((System.Drawing.Image)(resources.GetObject("imgUser.Image")));
            this.imgUser.Location = new System.Drawing.Point(270, 322);
            this.imgUser.Name = "imgUser";
            this.imgUser.Size = new System.Drawing.Size(77, 79);
            this.imgUser.TabIndex = 8;
            this.imgUser.TabStop = false;
            // 
            // imgPass
            // 
            this.imgPass.Image = ((System.Drawing.Image)(resources.GetObject("imgPass.Image")));
            this.imgPass.Location = new System.Drawing.Point(270, 431);
            this.imgPass.Name = "imgPass";
            this.imgPass.Size = new System.Drawing.Size(77, 79);
            this.imgPass.TabIndex = 9;
            this.imgPass.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 641);
            this.panel1.TabIndex = 10;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.Location = new System.Drawing.Point(-13, -7);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(226, 654);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.Black;
            this.pictureBox2.Location = new System.Drawing.Point(199, -3);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(10, 680);
            this.pictureBox2.TabIndex = 11;
            this.pictureBox2.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(86)))), ((int)(((byte)(86)))));
            this.panel2.Location = new System.Drawing.Point(376, 383);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(441, 5);
            this.panel2.TabIndex = 12;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(86)))), ((int)(((byte)(86)))));
            this.panel3.Location = new System.Drawing.Point(376, 496);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(441, 5);
            this.panel3.TabIndex = 13;
            // 
            // Form1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(884, 641);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.imgPass);
            this.Controls.Add(this.imgUser);
            this.Controls.Add(this.btnSalir);
            this.Controls.Add(this.btnIniciarSesion);
            this.Controls.Add(this.PassLogin);
            this.Controls.Add(this.CajaUsuariosLogin);
            this.Controls.Add(this.textoContrasena);
            this.Controls.Add(this.textoIniciarSesion);
            this.Controls.Add(this.LogoLogin);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Iniciar Sesión";
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            ((System.ComponentModel.ISupportInitialize)(this.LogoLogin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgUser)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgPass)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox LogoLogin;
        private System.Windows.Forms.Label textoIniciarSesion;
        private System.Windows.Forms.Label textoContrasena;
        private System.Windows.Forms.TextBox CajaUsuariosLogin;
        private System.Windows.Forms.TextBox PassLogin;
        private System.Windows.Forms.Button btnIniciarSesion;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.PictureBox imgUser;
        private System.Windows.Forms.PictureBox imgPass;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
    }
}

