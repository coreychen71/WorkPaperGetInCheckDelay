namespace 乾膜曝光完進顯影前置時間檢查
{
    partial class Main
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.lblSecondCountDown = new System.Windows.Forms.Label();
            this.lblSecond = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.tmrSecond = new System.Windows.Forms.Timer(this.components);
            this.lblLastWorkPaper = new System.Windows.Forms.Label();
            this.lblLastLotNum = new System.Windows.Forms.Label();
            this.btnManual = new System.Windows.Forms.Button();
            this.btnAlarm = new System.Windows.Forms.Button();
            this.lblLastPartNum = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "乾膜曝光結束時間檢查";
            // 
            // lblSecondCountDown
            // 
            this.lblSecondCountDown.AutoSize = true;
            this.lblSecondCountDown.Font = new System.Drawing.Font("新細明體", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblSecondCountDown.Location = new System.Drawing.Point(19, 9);
            this.lblSecondCountDown.Name = "lblSecondCountDown";
            this.lblSecondCountDown.Size = new System.Drawing.Size(226, 24);
            this.lblSecondCountDown.TabIndex = 0;
            this.lblSecondCountDown.Text = "距離下次檢查時間：";
            // 
            // lblSecond
            // 
            this.lblSecond.AutoSize = true;
            this.lblSecond.Font = new System.Drawing.Font("新細明體", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblSecond.Location = new System.Drawing.Point(234, 9);
            this.lblSecond.Name = "lblSecond";
            this.lblSecond.Size = new System.Drawing.Size(82, 24);
            this.lblSecond.TabIndex = 1;
            this.lblSecond.Text = "300 Sec";
            // 
            // btnClose
            // 
            this.btnClose.Font = new System.Drawing.Font("新細明體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnClose.Location = new System.Drawing.Point(223, 126);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(100, 30);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "關閉程式";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // tmrSecond
            // 
            this.tmrSecond.Tick += new System.EventHandler(this.tmrSecond_Tick);
            // 
            // lblLastWorkPaper
            // 
            this.lblLastWorkPaper.AutoSize = true;
            this.lblLastWorkPaper.Font = new System.Drawing.Font("新細明體", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblLastWorkPaper.Location = new System.Drawing.Point(19, 45);
            this.lblLastWorkPaper.Name = "lblLastWorkPaper";
            this.lblLastWorkPaper.Size = new System.Drawing.Size(202, 24);
            this.lblLastWorkPaper.TabIndex = 3;
            this.lblLastWorkPaper.Text = "上次通知的工單：";
            // 
            // lblLastLotNum
            // 
            this.lblLastLotNum.AutoSize = true;
            this.lblLastLotNum.Font = new System.Drawing.Font("新細明體", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblLastLotNum.Location = new System.Drawing.Point(207, 45);
            this.lblLastLotNum.Name = "lblLastLotNum";
            this.lblLastLotNum.Size = new System.Drawing.Size(86, 24);
            this.lblLastLotNum.TabIndex = 4;
            this.lblLastLotNum.Text = "LotNum";
            // 
            // btnManual
            // 
            this.btnManual.Font = new System.Drawing.Font("新細明體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnManual.Location = new System.Drawing.Point(117, 126);
            this.btnManual.Name = "btnManual";
            this.btnManual.Size = new System.Drawing.Size(100, 30);
            this.btnManual.TabIndex = 5;
            this.btnManual.Text = "手動查詢";
            this.btnManual.UseVisualStyleBackColor = true;
            this.btnManual.Click += new System.EventHandler(this.btnManual_Click);
            // 
            // btnAlarm
            // 
            this.btnAlarm.Font = new System.Drawing.Font("新細明體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnAlarm.Location = new System.Drawing.Point(11, 126);
            this.btnAlarm.Name = "btnAlarm";
            this.btnAlarm.Size = new System.Drawing.Size(100, 30);
            this.btnAlarm.TabIndex = 6;
            this.btnAlarm.Text = "警報測試";
            this.btnAlarm.UseVisualStyleBackColor = true;
            this.btnAlarm.Click += new System.EventHandler(this.btnAlarm_Click);
            // 
            // lblLastPartNum
            // 
            this.lblLastPartNum.AutoSize = true;
            this.lblLastPartNum.Font = new System.Drawing.Font("新細明體", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblLastPartNum.Location = new System.Drawing.Point(207, 78);
            this.lblLastPartNum.Name = "lblLastPartNum";
            this.lblLastPartNum.Size = new System.Drawing.Size(92, 24);
            this.lblLastPartNum.TabIndex = 7;
            this.lblLastPartNum.Text = "PartNum";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(334, 161);
            this.Controls.Add(this.lblLastPartNum);
            this.Controls.Add(this.lblLastLotNum);
            this.Controls.Add(this.lblLastWorkPaper);
            this.Controls.Add(this.btnAlarm);
            this.Controls.Add(this.btnManual);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lblSecond);
            this.Controls.Add(this.lblSecondCountDown);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "乾膜顯影進站逾時檢查";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.Label lblSecondCountDown;
        private System.Windows.Forms.Label lblSecond;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Timer tmrSecond;
        private System.Windows.Forms.Label lblLastWorkPaper;
        private System.Windows.Forms.Label lblLastLotNum;
        private System.Windows.Forms.Button btnManual;
        private System.Windows.Forms.Button btnAlarm;
        private System.Windows.Forms.Label lblLastPartNum;
    }
}

