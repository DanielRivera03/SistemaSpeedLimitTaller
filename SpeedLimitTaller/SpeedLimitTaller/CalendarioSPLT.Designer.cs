namespace SpeedLimitTaller
{
    partial class CalendarioSPLT
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CalendarioSPLT));
            this.CalendatioSp = new WindowsFormsCalendar.MonthView();
            this.CerrarVentana = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.CerrarVentana)).BeginInit();
            this.SuspendLayout();
            // 
            // CalendatioSp
            // 
            this.CalendatioSp.ArrowsColor = System.Drawing.Color.Maroon;
            this.CalendatioSp.ArrowsSelectedColor = System.Drawing.Color.Gold;
            this.CalendatioSp.BackColor = System.Drawing.Color.Black;
            this.CalendatioSp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.CalendatioSp.DayBackgroundColor = System.Drawing.Color.Empty;
            this.CalendatioSp.DayGrayedText = System.Drawing.SystemColors.GrayText;
            this.CalendatioSp.DayNamesLength = 3;
            this.CalendatioSp.DaySelectedBackgroundColor = System.Drawing.Color.DarkSlateBlue;
            this.CalendatioSp.DaySelectedColor = System.Drawing.SystemColors.WindowText;
            this.CalendatioSp.DaySelectedTextColor = System.Drawing.SystemColors.ControlLightLight;
            this.CalendatioSp.Font = new System.Drawing.Font("Russo One", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CalendatioSp.ItemPadding = new System.Windows.Forms.Padding(2);
            this.CalendatioSp.Location = new System.Drawing.Point(-1, 47);
            this.CalendatioSp.MonthTitleColor = System.Drawing.SystemColors.ActiveCaption;
            this.CalendatioSp.MonthTitleColorInactive = System.Drawing.SystemColors.InactiveCaption;
            this.CalendatioSp.MonthTitleTextColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.CalendatioSp.MonthTitleTextColorInactive = System.Drawing.SystemColors.InactiveCaptionText;
            this.CalendatioSp.Name = "CalendatioSp";
            this.CalendatioSp.SelectionMode = WindowsFormsCalendar.MonthViewSelection.Day;
            this.CalendatioSp.Size = new System.Drawing.Size(700, 353);
            this.CalendatioSp.TabIndex = 0;
            this.CalendatioSp.Text = "Calendario SpeedLimit Taller S.A de C.V";
            this.CalendatioSp.TodayBorderColor = System.Drawing.Color.DarkRed;
            // 
            // CerrarVentana
            // 
            this.CerrarVentana.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CerrarVentana.BackColor = System.Drawing.Color.Transparent;
            this.CerrarVentana.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("CerrarVentana.BackgroundImage")));
            this.CerrarVentana.Cursor = System.Windows.Forms.Cursors.Hand;
            this.CerrarVentana.Location = new System.Drawing.Point(658, 8);
            this.CerrarVentana.Name = "CerrarVentana";
            this.CerrarVentana.Size = new System.Drawing.Size(32, 33);
            this.CerrarVentana.TabIndex = 48;
            this.CerrarVentana.TabStop = false;
            this.CerrarVentana.Click += new System.EventHandler(this.CerrarVentana_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Russo One", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(10, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(304, 18);
            this.label2.TabIndex = 47;
            this.label2.Text = "Calendario SpeedLimit Taller S.A de C.V";
            // 
            // CalendarioSPLT
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(3)))), ((int)(((byte)(60)))));
            this.ClientSize = new System.Drawing.Size(700, 400);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.CerrarVentana);
            this.Controls.Add(this.CalendatioSp);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CalendarioSPLT";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Calendario";
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.CalendarioSPLT_MouseDown);
            ((System.ComponentModel.ISupportInitialize)(this.CerrarVentana)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private WindowsFormsCalendar.MonthView CalendatioSp;
        private System.Windows.Forms.PictureBox CerrarVentana;
        private System.Windows.Forms.Label label2;
    }
}