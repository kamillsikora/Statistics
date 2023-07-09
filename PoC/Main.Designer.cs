namespace PoC
{
    partial class Statistics
    {
        /// <summary>
        /// Wymagana zmienna projektanta.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Wyczyść wszystkie używane zasoby.
        /// </summary>
        /// <param name="disposing">prawda, jeżeli zarządzane zasoby powinny zostać zlikwidowane; Fałsz w przeciwnym wypadku.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kod generowany przez Projektanta formularzy systemu Windows

        /// <summary>
        /// Metoda wymagana do obsługi projektanta — nie należy modyfikować
        /// jej zawartości w edytorze kodu.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Statistics));
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.button6 = new ePOSOne.btnProduct.Button_WOC();
            this.button4 = new ePOSOne.btnProduct.Button_WOC();
            this.button5 = new ePOSOne.btnProduct.Button_WOC();
            this.button3 = new ePOSOne.btnProduct.Button_WOC();
            this.button2 = new ePOSOne.btnProduct.Button_WOC();
            this.button1 = new ePOSOne.btnProduct.Button_WOC();
            this.SuspendLayout();
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.BackColor = System.Drawing.Color.Silver;
            this.checkedListBox1.ForeColor = System.Drawing.Color.Black;
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Location = new System.Drawing.Point(9, 46);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(551, 334);
            this.checkedListBox1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(5, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Directories:";
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.Silver;
            this.textBox1.Location = new System.Drawing.Point(108, 10);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(621, 20);
            this.textBox1.TabIndex = 13;
            // 
            // button6
            // 
            this.button6.BorderColor = System.Drawing.Color.YellowGreen;
            this.button6.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.button6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button6.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.button6.Location = new System.Drawing.Point(651, 298);
            this.button6.Name = "button6";
            this.button6.OnHoverBorderColor = System.Drawing.Color.Gray;
            this.button6.OnHoverButtonColor = System.Drawing.Color.YellowGreen;
            this.button6.OnHoverTextColor = System.Drawing.Color.Gray;
            this.button6.Size = new System.Drawing.Size(78, 78);
            this.button6.TabIndex = 20;
            this.button6.Text = "Go Back";
            this.button6.TextColor = System.Drawing.Color.White;
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button4
            // 
            this.button4.BorderColor = System.Drawing.Color.YellowGreen;
            this.button4.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.button4.Location = new System.Drawing.Point(567, 298);
            this.button4.Name = "button4";
            this.button4.OnHoverBorderColor = System.Drawing.Color.Gray;
            this.button4.OnHoverButtonColor = System.Drawing.Color.YellowGreen;
            this.button4.OnHoverTextColor = System.Drawing.Color.Gray;
            this.button4.Size = new System.Drawing.Size(78, 78);
            this.button4.TabIndex = 19;
            this.button4.Text = "Open";
            this.button4.TextColor = System.Drawing.Color.White;
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.BorderColor = System.Drawing.Color.YellowGreen;
            this.button5.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.button5.Location = new System.Drawing.Point(651, 214);
            this.button5.Name = "button5";
            this.button5.OnHoverBorderColor = System.Drawing.Color.Gray;
            this.button5.OnHoverButtonColor = System.Drawing.Color.YellowGreen;
            this.button5.OnHoverTextColor = System.Drawing.Color.Gray;
            this.button5.Size = new System.Drawing.Size(78, 78);
            this.button5.TabIndex = 18;
            this.button5.Text = "Change Disk";
            this.button5.TextColor = System.Drawing.Color.White;
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button3
            // 
            this.button3.BorderColor = System.Drawing.Color.YellowGreen;
            this.button3.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.button3.Location = new System.Drawing.Point(567, 214);
            this.button3.Name = "button3";
            this.button3.OnHoverBorderColor = System.Drawing.Color.Gray;
            this.button3.OnHoverButtonColor = System.Drawing.Color.YellowGreen;
            this.button3.OnHoverTextColor = System.Drawing.Color.Gray;
            this.button3.Size = new System.Drawing.Size(78, 78);
            this.button3.TabIndex = 17;
            this.button3.Text = "Reload";
            this.button3.TextColor = System.Drawing.Color.White;
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.BorderColor = System.Drawing.Color.YellowGreen;
            this.button2.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.button2.Location = new System.Drawing.Point(567, 130);
            this.button2.Name = "button2";
            this.button2.OnHoverBorderColor = System.Drawing.Color.Gray;
            this.button2.OnHoverButtonColor = System.Drawing.Color.YellowGreen;
            this.button2.OnHoverTextColor = System.Drawing.Color.Gray;
            this.button2.Size = new System.Drawing.Size(162, 78);
            this.button2.TabIndex = 16;
            this.button2.Text = "Go to Statistics";
            this.button2.TextColor = System.Drawing.Color.White;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.BorderColor = System.Drawing.Color.YellowGreen;
            this.button1.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.button1.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.button1.Location = new System.Drawing.Point(567, 46);
            this.button1.Name = "button1";
            this.button1.OnHoverBorderColor = System.Drawing.Color.Gray;
            this.button1.OnHoverButtonColor = System.Drawing.Color.YellowGreen;
            this.button1.OnHoverTextColor = System.Drawing.Color.DimGray;
            this.button1.Size = new System.Drawing.Size(162, 78);
            this.button1.TabIndex = 15;
            this.button1.Text = "Create Statistics";
            this.button1.TextColor = System.Drawing.Color.White;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Statistics
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(745, 401);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.checkedListBox1);
            this.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Statistics";
            this.Text = "Statistics";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.CheckedListBox checkedListBox1;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private ePOSOne.btnProduct.Button_WOC button1;
        private ePOSOne.btnProduct.Button_WOC button2;
        private ePOSOne.btnProduct.Button_WOC button3;
        private ePOSOne.btnProduct.Button_WOC button5;
        private ePOSOne.btnProduct.Button_WOC button4;
        private ePOSOne.btnProduct.Button_WOC button6;
    }
}

