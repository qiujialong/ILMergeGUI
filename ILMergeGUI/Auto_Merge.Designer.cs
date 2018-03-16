namespace ILMergeGUI
{
    partial class Auto_Merge
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
            this.btnMergeSFL = new System.Windows.Forms.Button();
            this.btnMerge_commonlib = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.btnLog = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnMergeSFL
            // 
            this.btnMergeSFL.Location = new System.Drawing.Point(397, 174);
            this.btnMergeSFL.Name = "btnMergeSFL";
            this.btnMergeSFL.Size = new System.Drawing.Size(104, 41);
            this.btnMergeSFL.TabIndex = 9;
            this.btnMergeSFL.Text = "合并SFL";
            this.btnMergeSFL.UseVisualStyleBackColor = true;
            this.btnMergeSFL.Click += new System.EventHandler(this.btnMergeSFL_Click);
            // 
            // btnMerge_commonlib
            // 
            this.btnMerge_commonlib.Location = new System.Drawing.Point(142, 174);
            this.btnMerge_commonlib.Name = "btnMerge_commonlib";
            this.btnMerge_commonlib.Size = new System.Drawing.Size(104, 41);
            this.btnMerge_commonlib.TabIndex = 8;
            this.btnMerge_commonlib.Text = "合并Commonlib";
            this.btnMerge_commonlib.UseVisualStyleBackColor = true;
            this.btnMerge_commonlib.Click += new System.EventHandler(this.btnMerge_commonlib_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progressBar1.Location = new System.Drawing.Point(0, 534);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(638, 24);
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBar1.TabIndex = 3;
            this.progressBar1.Visible = false;
            // 
            // btnLog
            // 
            this.btnLog.Location = new System.Drawing.Point(259, 351);
            this.btnLog.Name = "btnLog";
            this.btnLog.Size = new System.Drawing.Size(104, 41);
            this.btnLog.TabIndex = 4;
            this.btnLog.Text = "查看日志";
            this.btnLog.UseVisualStyleBackColor = true;
            this.btnLog.Click += new System.EventHandler(this.btnLog_Click);
            // 
            // Auto_Merge
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(638, 558);
            this.Controls.Add(this.btnMergeSFL);
            this.Controls.Add(this.btnLog);
            this.Controls.Add(this.btnMerge_commonlib);
            this.Controls.Add(this.progressBar1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Auto_Merge";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ILMergeGUI";
            this.Load += new System.EventHandler(this.Auto_Merge_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button btnLog;
        private System.Windows.Forms.Button btnMerge_commonlib;
        private System.Windows.Forms.Button btnMergeSFL;
    }
}

