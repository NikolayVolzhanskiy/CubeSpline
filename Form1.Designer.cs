namespace CubeSpline
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Frame = new System.Windows.Forms.PictureBox();
            this.CreateSpline = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.Frame)).BeginInit();
            this.SuspendLayout();
            // 
            // Frame
            // 
            this.Frame.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Frame.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.Frame.Location = new System.Drawing.Point(12, 12);
            this.Frame.Name = "Frame";
            this.Frame.Size = new System.Drawing.Size(896, 530);
            this.Frame.TabIndex = 0;
            this.Frame.TabStop = false;
            this.Frame.Paint += new System.Windows.Forms.PaintEventHandler(this.Frame_Paint);
            this.Frame.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Frame_MouseDown);
            this.Frame.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Frame_MouseMove);
            this.Frame.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Frame_MouseUp);
            // 
            // CreateSpline
            // 
            this.CreateSpline.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CreateSpline.Location = new System.Drawing.Point(914, 41);
            this.CreateSpline.Name = "CreateSpline";
            this.CreateSpline.Size = new System.Drawing.Size(122, 501);
            this.CreateSpline.TabIndex = 1;
            this.CreateSpline.Text = "Построить сплайн";
            this.CreateSpline.UseVisualStyleBackColor = true;
            this.CreateSpline.Click += new System.EventHandler(this.CreateSpline_Click);
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Location = new System.Drawing.Point(914, 12);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(122, 23);
            this.textBox1.TabIndex = 2;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1048, 554);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.CreateSpline);
            this.Controls.Add(this.Frame);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.Frame)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private PictureBox Frame;
        private Button CreateSpline;
        private TextBox textBox1;
    }
}