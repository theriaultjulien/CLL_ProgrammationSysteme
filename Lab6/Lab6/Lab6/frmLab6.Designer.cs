
namespace Lab6
{
    partial class frmLab6
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
            this.btn_lab6 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_lab6
            // 
            this.btn_lab6.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btn_lab6.Location = new System.Drawing.Point(130, 89);
            this.btn_lab6.Name = "btn_lab6";
            this.btn_lab6.Size = new System.Drawing.Size(253, 122);
            this.btn_lab6.TabIndex = 0;
            this.btn_lab6.Text = "Lab6 exercice données en mémoire";
            this.btn_lab6.UseVisualStyleBackColor = true;
            this.btn_lab6.Click += new System.EventHandler(this.btn_lab6_Click);
            // 
            // frmLab6
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(539, 301);
            this.Controls.Add(this.btn_lab6);
            this.Name = "frmLab6";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_lab6;
    }
}

