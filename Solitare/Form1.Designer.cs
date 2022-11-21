namespace Solitare
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
            this.Stock = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.Stock)).BeginInit();
            this.SuspendLayout();
            // 
            // Stock
            // 
            this.Stock.Location = new System.Drawing.Point(15, 15);
            this.Stock.Name = "Stock";
            this.Stock.Size = new System.Drawing.Size(130, 172);
            this.Stock.TabIndex = 0;
            this.Stock.TabStop = false;
            this.Stock.Click += new System.EventHandler(this.StockBox_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 32F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1710, 1002);
            this.Controls.Add(this.Stock);
            this.Name = "Form1";
            this.Text = "Swajal";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Stock)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private PictureBox pictureBox2;
        private PictureBox Stock;
    }
}