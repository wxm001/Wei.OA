namespace WFWindowsForms
{
    partial class MainForm
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
            this.btnStart = new System.Windows.Forms.Button();
            this.btnContinue = new System.Windows.Forms.Button();
            this.txtBookMarkName = new System.Windows.Forms.TextBox();
            this.txtMoney = new System.Windows.Forms.TextBox();
            this.btnStateStart = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(113, 49);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "启动工作流";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnContinue
            // 
            this.btnContinue.Location = new System.Drawing.Point(113, 135);
            this.btnContinue.Name = "btnContinue";
            this.btnContinue.Size = new System.Drawing.Size(75, 23);
            this.btnContinue.TabIndex = 1;
            this.btnContinue.Text = "继续工作流";
            this.btnContinue.UseVisualStyleBackColor = true;
            this.btnContinue.Click += new System.EventHandler(this.btnContinue_Click);
            // 
            // txtBookMarkName
            // 
            this.txtBookMarkName.Location = new System.Drawing.Point(265, 50);
            this.txtBookMarkName.Name = "txtBookMarkName";
            this.txtBookMarkName.Size = new System.Drawing.Size(185, 21);
            this.txtBookMarkName.TabIndex = 2;
            this.txtBookMarkName.Text = "book";
            // 
            // txtMoney
            // 
            this.txtMoney.Location = new System.Drawing.Point(265, 137);
            this.txtMoney.Name = "txtMoney";
            this.txtMoney.Size = new System.Drawing.Size(185, 21);
            this.txtMoney.TabIndex = 3;
            this.txtMoney.Text = "120";
            // 
            // btnStateStart
            // 
            this.btnStateStart.Location = new System.Drawing.Point(265, 250);
            this.btnStateStart.Name = "btnStateStart";
            this.btnStateStart.Size = new System.Drawing.Size(117, 23);
            this.btnStateStart.TabIndex = 4;
            this.btnStateStart.Text = "启动状态工作流";
            this.btnStateStart.UseVisualStyleBackColor = true;
            this.btnStateStart.Click += new System.EventHandler(this.btnStateStart_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnStateStart);
            this.Controls.Add(this.txtMoney);
            this.Controls.Add(this.txtBookMarkName);
            this.Controls.Add(this.btnContinue);
            this.Controls.Add(this.btnStart);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnContinue;
        private System.Windows.Forms.TextBox txtBookMarkName;
        private System.Windows.Forms.TextBox txtMoney;
        private System.Windows.Forms.Button btnStateStart;
    }
}