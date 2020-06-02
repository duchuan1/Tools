namespace CsCallScriptDemo
{
    partial class Form1
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btnExec = new System.Windows.Forms.Button();
            this.cmbScriptType = new System.Windows.Forms.ComboBox();
            this.rtbScript = new System.Windows.Forms.RichTextBox();
            this.rtbResult = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.nudLeft = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.nudRight = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.rtbTime = new System.Windows.Forms.RichTextBox();
            this.btnExecLoop = new System.Windows.Forms.Button();
            this.nudLoop = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nudLeft)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLoop)).BeginInit();
            this.SuspendLayout();
            // 
            // btnExec
            // 
            this.btnExec.Location = new System.Drawing.Point(218, 26);
            this.btnExec.Name = "btnExec";
            this.btnExec.Size = new System.Drawing.Size(75, 23);
            this.btnExec.TabIndex = 0;
            this.btnExec.Text = "执行1次";
            this.btnExec.UseVisualStyleBackColor = true;
            this.btnExec.Click += new System.EventHandler(this.btnExec_Click);
            // 
            // cmbScriptType
            // 
            this.cmbScriptType.FormattingEnabled = true;
            this.cmbScriptType.Location = new System.Drawing.Point(91, 29);
            this.cmbScriptType.Name = "cmbScriptType";
            this.cmbScriptType.Size = new System.Drawing.Size(121, 20);
            this.cmbScriptType.TabIndex = 1;
            this.cmbScriptType.SelectedIndexChanged += new System.EventHandler(this.cmbScriptType_SelectedIndexChanged);
            // 
            // rtbScript
            // 
            this.rtbScript.Location = new System.Drawing.Point(33, 147);
            this.rtbScript.Name = "rtbScript";
            this.rtbScript.Size = new System.Drawing.Size(238, 239);
            this.rtbScript.TabIndex = 2;
            this.rtbScript.Text = "";
            // 
            // rtbResult
            // 
            this.rtbResult.Location = new System.Drawing.Point(388, 147);
            this.rtbResult.Name = "rtbResult";
            this.rtbResult.Size = new System.Drawing.Size(100, 74);
            this.rtbResult.TabIndex = 3;
            this.rtbResult.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(113, 114);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "脚本";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(419, 113);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "结果";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(29, 34);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "脚本类型";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(30, 81);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "Left";
            // 
            // nudLeft
            // 
            this.nudLeft.Location = new System.Drawing.Point(68, 71);
            this.nudLeft.Maximum = new decimal(new int[] {
            1316134912,
            2328,
            0,
            0});
            this.nudLeft.Name = "nudLeft";
            this.nudLeft.Size = new System.Drawing.Size(120, 21);
            this.nudLeft.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(247, 81);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 12);
            this.label5.TabIndex = 7;
            this.label5.Text = "Right";
            // 
            // nudRight
            // 
            this.nudRight.Location = new System.Drawing.Point(298, 71);
            this.nudRight.Maximum = new decimal(new int[] {
            1316134912,
            2328,
            0,
            0});
            this.nudRight.Name = "nudRight";
            this.nudRight.Size = new System.Drawing.Size(120, 21);
            this.nudRight.TabIndex = 8;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(419, 255);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 12);
            this.label6.TabIndex = 9;
            this.label6.Text = "运行时间(ms)";
            // 
            // rtbTime
            // 
            this.rtbTime.Location = new System.Drawing.Point(388, 270);
            this.rtbTime.Name = "rtbTime";
            this.rtbTime.Size = new System.Drawing.Size(100, 96);
            this.rtbTime.TabIndex = 10;
            this.rtbTime.Text = "";
            // 
            // btnExecLoop
            // 
            this.btnExecLoop.Location = new System.Drawing.Point(456, 27);
            this.btnExecLoop.Name = "btnExecLoop";
            this.btnExecLoop.Size = new System.Drawing.Size(75, 23);
            this.btnExecLoop.TabIndex = 11;
            this.btnExecLoop.Text = "循环执行";
            this.btnExecLoop.UseVisualStyleBackColor = true;
            this.btnExecLoop.Click += new System.EventHandler(this.btnExecLoop_Click);
            // 
            // nudLoop
            // 
            this.nudLoop.Location = new System.Drawing.Point(376, 29);
            this.nudLoop.Maximum = new decimal(new int[] {
            1316134912,
            2328,
            0,
            0});
            this.nudLoop.Name = "nudLoop";
            this.nudLoop.Size = new System.Drawing.Size(72, 21);
            this.nudLoop.TabIndex = 12;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(317, 37);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 13;
            this.label7.Text = "循环次数";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(543, 397);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.nudLoop);
            this.Controls.Add(this.btnExecLoop);
            this.Controls.Add(this.rtbTime);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.nudRight);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.nudLeft);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.rtbResult);
            this.Controls.Add(this.rtbScript);
            this.Controls.Add(this.cmbScriptType);
            this.Controls.Add(this.btnExec);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.nudLeft)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLoop)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnExec;
        private System.Windows.Forms.ComboBox cmbScriptType;
        private System.Windows.Forms.RichTextBox rtbScript;
        private System.Windows.Forms.RichTextBox rtbResult;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown nudLeft;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown nudRight;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.RichTextBox rtbTime;
        private System.Windows.Forms.Button btnExecLoop;
        private System.Windows.Forms.NumericUpDown nudLoop;
        private System.Windows.Forms.Label label7;
    }
}

