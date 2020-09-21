namespace ServiceTest
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
            this.b_ejecutar = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.txtResponse = new System.Windows.Forms.TextBox();
            this.txtCodigoBarra = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tb_fecha = new System.Windows.Forms.TextBox();
            this.tb_mail = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cb_estados = new System.Windows.Forms.ComboBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.label6 = new System.Windows.Forms.Label();
            this.tb_url_servicio = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // b_ejecutar
            // 
            this.b_ejecutar.Location = new System.Drawing.Point(305, 168);
            this.b_ejecutar.Name = "b_ejecutar";
            this.b_ejecutar.Size = new System.Drawing.Size(75, 23);
            this.b_ejecutar.TabIndex = 0;
            this.b_ejecutar.Text = "Ejecutar Servicio";
            this.b_ejecutar.UseVisualStyleBackColor = true;
            this.b_ejecutar.Click += new System.EventHandler(this.B_ejecutar_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(41, 103);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 1;
            // 
            // txtResponse
            // 
            this.txtResponse.Location = new System.Drawing.Point(41, 205);
            this.txtResponse.Multiline = true;
            this.txtResponse.Name = "txtResponse";
            this.txtResponse.Size = new System.Drawing.Size(453, 172);
            this.txtResponse.TabIndex = 2;
            // 
            // txtCodigoBarra
            // 
            this.txtCodigoBarra.Location = new System.Drawing.Point(191, 103);
            this.txtCodigoBarra.Name = "txtCodigoBarra";
            this.txtCodigoBarra.Size = new System.Drawing.Size(100, 20);
            this.txtCodigoBarra.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(41, 74);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Servicio";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(188, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Codigo Barra";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(188, 140);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(106, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Fecha (dd/MM/yyyy)";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(41, 140);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(76, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Usuario (email)";
            // 
            // tb_fecha
            // 
            this.tb_fecha.Location = new System.Drawing.Point(191, 169);
            this.tb_fecha.Name = "tb_fecha";
            this.tb_fecha.Size = new System.Drawing.Size(100, 20);
            this.tb_fecha.TabIndex = 8;
            // 
            // tb_mail
            // 
            this.tb_mail.Location = new System.Drawing.Point(41, 169);
            this.tb_mail.Name = "tb_mail";
            this.tb_mail.Size = new System.Drawing.Size(121, 20);
            this.tb_mail.TabIndex = 11;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(305, 74);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(40, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "Estado";
            // 
            // cb_estados
            // 
            this.cb_estados.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_estados.FormattingEnabled = true;
            this.cb_estados.Location = new System.Drawing.Point(305, 103);
            this.cb_estados.Name = "cb_estados";
            this.cb_estados.Size = new System.Drawing.Size(189, 21);
            this.cb_estados.TabIndex = 12;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(397, 168);
            this.progressBar1.MarqueeAnimationSpeed = 300;
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(97, 23);
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBar1.TabIndex = 14;
            this.progressBar1.Visible = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(41, 9);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(94, 13);
            this.label6.TabIndex = 16;
            this.label6.Text = "URL Web Service";
            // 
            // tb_url_servicio
            // 
            this.tb_url_servicio.Location = new System.Drawing.Point(41, 38);
            this.tb_url_servicio.Name = "tb_url_servicio";
            this.tb_url_servicio.Size = new System.Drawing.Size(453, 20);
            this.tb_url_servicio.TabIndex = 15;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(555, 427);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.tb_url_servicio);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cb_estados);
            this.Controls.Add(this.tb_mail);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tb_fecha);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtCodigoBarra);
            this.Controls.Add(this.txtResponse);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.b_ejecutar);
            this.Name = "Form1";
            this.Text = "Prueba SWNotificacionService de Mailroom";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button b_ejecutar;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.TextBox txtResponse;
        private System.Windows.Forms.TextBox txtCodigoBarra;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tb_fecha;
        private System.Windows.Forms.TextBox tb_mail;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cb_estados;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tb_url_servicio;
    }
}

