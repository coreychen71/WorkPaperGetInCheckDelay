﻿using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Media;
using System.Text;
using System.Net;
using System.Net.Mail;
using System.Windows.Forms;

namespace 乾膜曝光完進顯影前置時間檢查
{
    public partial class Main : Form
    {
        #region 初始宣告的參數
        string ConnEW = "server=ERP;database=EW;uid=JSIS;pwd=JSIS";
        string ConnME = "server=EWNAS;database=ME;uid=me;pwd=2dae5na";

        /// <summary>
        /// 設定的檢查間隔時間(5分鐘)
        /// </summary>
        int SetSec = 300;

        /// <summary>
        /// 供Timer計算用的值
        /// </summary>
        int SecCountDown;

        /// <summary>
        /// 警報聲效檔路徑
        /// </summary>
        string AlarmSoundPath = @"D:\WorkPaperGetInCheckDelay\AlarmSound.wav";

        /// <summary>
        /// Log檔路徑
        /// </summary>
        string LogPath = @"D:\WorkPaperGetInCheckDelay\Log.txt";

        StreamWriter writerLog;
        #endregion

        public Main()
        {
            InitializeComponent();
            Resize += Main_Resize;
            notifyIcon1.MouseDoubleClick += notifyIcon1_MouseDoubleClick;
            FormClosing += FormCloseChk;
            SecCountDown = SetSec;
            tmrSecond.Interval = 1000;
            tmrSecond.Enabled = true;
            writerLog = File.AppendText(LogPath);
        }

        #region 表單事件區塊
        //MainForm Resize Event
        private void Main_Resize(object sender, EventArgs e)
        {
            // 當縮小按鈕被按下時, 將程式隱藏於Taskbar, 並設定顯示佇列圖示
            if (WindowState == FormWindowState.Minimized)
            {
                ShowInTaskbar = false;
                notifyIcon1.Visible = true;
            }
        }


        //notifyIcon Event
        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            // 還原視窗, 隱藏佇列圖示並將程式恢復顯示Taskbar
            ShowInTaskbar = true;
            notifyIcon1.Visible = false;
            WindowState = FormWindowState.Normal;
        }

        //Check CloseForm Password
        private bool ChkClosePW()
        {
            var result = false;
            InputText it = new InputText();
            it.Text = "請輸入關閉程式的密碼！";
            if (it.ShowDialog() == DialogResult.OK)
            {
                var pw = it.txtInput.Text;
                if (pw == "ewpcbmis")
                {
                    result = true;
                }
                else
                {
                    MessageBox.Show("密碼輸入錯誤！", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                it.Dispose();
            }
            return result;
        }

        //FormClosing Event
        private void FormCloseChk(object sender, FormClosingEventArgs e)
        {
            //判斷若是否由系統進行關機，若不是就要進行密碼確認
            if (e.CloseReason != CloseReason.WindowsShutDown)
            {
                if (ChkClosePW())
                {
                    e.Cancel = false;
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }
        #endregion

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnManual_Click(object sender, EventArgs e)
        {
            ChkWorkPaper();
        }

        private void btnAlarm_Click(object sender, EventArgs e)
        {
            SoundPlayer testAlarm = new SoundPlayer(AlarmSoundPath);
            testAlarm.PlayLooping();
            Repeat:
            if (MessageBox.Show("注意！下述工單已從乾膜曝光出站逾 X 分鐘，請盡速安排上顯影線生產！" + Environment.NewLine +
                            "Warning！The information following already overtime X minute, "+
                            "Please to arrange productment at once." + Environment.NewLine +
                            "批號：LXXXXXXXXXX" + Environment.NewLine +
                            "料號：AXX-XXXX-XX" + Environment.NewLine +
                            "曝光結束時間：1999/01/01 PM 02:00", "警報測試",
                            MessageBoxButtons.OK, MessageBoxIcon.Exclamation) == DialogResult.OK)
            {
                InputText it = new InputText();
                it.Text = "請輸入您的工號！ Please Keyin your Employee ID！";
                if (it.ShowDialog() == DialogResult.OK)
                {
                    var EmpId = it.txtInput.Text.ToUpper();
                    var EmpName = GetEmpName(EmpId);
                    if (EmpName != string.Empty)
                    {
                        MessageBox.Show("測試結束！", "測試", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        SecCountDown = SetSec;
                        //InsertDissolutionLog(
                        //    "LXXXXXXXXX", "AXX-XXXX-XX", "1000", "FF", "曝光機", 
                        //    DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "印刷機",
                        //    "60", EmpId, EmpName, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        //testAlarm.Stop();
                        //testAlarm.Dispose();
                        //it.Dispose();
                    }
                    else
                    {
                        MessageBox.Show("工號輸入錯誤，請確認！" + Environment.NewLine + "Employee Error, Please Confirm！",
                            "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                        goto Repeat;
                    }
                }
                else
                {
                    goto Repeat;
                }
            }
        }

        private void tmrSecond_Tick(object sender, EventArgs e)
        {
            SecCountDown--;
            lblSecond.Text = Convert.ToString(SecCountDown) + " Sec";
            if (SecCountDown == 0)
            {
                ChkWorkPaper();
                SecCountDown = SetSec;
            }
        }

        /// <summary>
        /// 檢查有無逾時進站工單
        /// </summary>
        private void ChkWorkPaper()
        {
            var stdTime = 0;
            var srcData = Get稽核乾膜曝光進顯影時間();
            foreach (DataRow row in srcData.Rows)
            {
                SoundPlayer AlarmSound = new SoundPlayer(AlarmSoundPath);
                AlarmSound.PlayLooping();
                lblLastLotNum.Text = row["lotnum"].ToString().Trim();
                lblLastPartNum.Text = row["partnum"].ToString().Trim();
                if (row["potypename"].ToString().Trim() == "量產")
                {
                    stdTime = 60;
                }
                else if (row["potypename"].ToString().Trim() == "樣品")
                {
                    stdTime = 15;
                }
                if (MessageBox.Show("注意！下述工單已從乾膜曝光出站達" + stdTime + "分鐘，請盡速安排上顯影線生產！" +
                    Environment.NewLine +
                    "Warning！The information following already overtime " + stdTime + " minute, Please to arrange productment " +
                    "at once." + Environment.NewLine +
                    "批號：" + row["lotnum"].ToString().Trim() + Environment.NewLine +
                    "料號：" + row["partnum"].ToString().Trim() + Environment.NewLine +
                    "訂單：" + row["potypename"].ToString().Trim() + Environment.NewLine +
                    "曝光結束時間：" + row["endtime"].ToString(), "注意",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation) == DialogResult.OK)
                {
                    InputText it = new InputText();
                    it.Text = "請輸入您的工號！ Please Keyin your Employee ID！";
                    if (it.ShowDialog() == DialogResult.OK)
                    {
                        var EmpId = it.txtInput.Text.ToUpper();
                        var EmpName = GetEmpName(EmpId);
                        if (EmpName != string.Empty)
                        {
                            InsertDissolutionLog(
                                row["lotnum"].ToString().Trim(),
                                row["partnum"].ToString().Trim(),
                                row["workqnty"].ToString().Trim(), "FF", "曝光機",
                                Convert.ToDateTime(row["endtime"]).ToString("yyyy-MM-dd HH:mm:ss"), "顯影機",
                                stdTime.ToString(), EmpId, EmpName, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                            //SendMail(
                            //"sm4@ewpcb.com.tw",
                            //"Ewproject System",
                            //"workpaperdelay@ewpcb.com.tw",
                            //"防焊出前處理進印刷房逾時通知！",
                            //"批號：" + row["lotnum"].ToString().Trim() + "<br/>" +
                            //"料號：" + row["partnum"].ToString().Trim() + "<br/>" +
                            //"前處理結束時間：" + row["endtime"].ToString() + "<br/>" +
                            //"防焊油墨顏色：" + LsmColor + "<br/>" +
                            //"已逾時時間：" + Convert.ToString(Convert.ToInt32(Minute)) + " 分<br/>" +
                            //"警報解除人員：" + EmpId + " " + EmpName + "<br/><br/>" +
                            //"-----此封郵件由系統所寄出，請勿直接回覆！-----");
                            AlarmSound.Stop();
                            AlarmSound.Dispose();
                            it.Dispose();
                        }
                        else
                        {
                            MessageBox.Show("工號輸入錯誤，請確認！" + Environment.NewLine +
                                "Employee Error, Please Confirm！",
                                "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                        }
                    }
                    else
                    {
                        it.Dispose();
                    }
                }
            }
        }

        #region Connection SQL 
        /// <summary>
        /// 取得稽核結果
        /// </summary>
        /// <returns></returns>
        private DataTable Get稽核乾膜曝光進顯影時間()
        {
            var result = new DataTable();
            using (SqlConnection sqlcon = new SqlConnection(ConnME))
            {
                SqlCommand sqlcomm = new SqlCommand(string.Empty, sqlcon);
                sqlcomm.CommandType = CommandType.StoredProcedure;
                sqlcomm.CommandText = "稽核乾膜曝光進顯影時間";
                try
                {
                    sqlcon.Open();
                    SqlDataReader read = sqlcomm.ExecuteReader();
                    result.Load(read);
                }
                catch (Exception ex)
                {
                    writerLog.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " Get稽核乾膜曝光進顯影時間()-" + ex.Message);
                    writerLog.Flush();
                }
            }
            return result;
        }

        /// <summary>
        /// 取得使用者姓名
        /// </summary>
        /// <param name="EmpId"></param>
        /// <returns></returns>
        private string GetEmpName(string EmpId)
        {
            var result = string.Empty;
            var strComm = "select EmpName from HPSdEmpInfo where EmpStatus=1 and EmpId='" + EmpId + "'";
            using (SqlConnection sqlcon = new SqlConnection(ConnEW))
            {
                SqlCommand sqlcomm = new SqlCommand(strComm, sqlcon);
                try
                {
                    sqlcon.Open();
                    SqlDataReader reader = sqlcomm.ExecuteReader();
                    if (reader.HasRows)
                    {
                        reader.Read();
                        result = reader.GetString(0);
                    }
                }
                catch (Exception ex)
                {
                    writerLog.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " GetEmpName()-" + ex.Message);
                    writerLog.Flush();
                }
            }
            return result;
        }

        /// <summary>
        /// 寫入解除逾時通知的Log
        /// </summary>
        /// <param name="LotNum">批號</param>
        /// <param name="PartNum">料號</param>
        /// <param name="Qnty">數量</param>
        /// <param name="Department">部門代碼</param>
        /// <param name="ExportProcess">上一站工序</param>
        /// <param name="ExportTime">上一站工序的結束時間</param>
        /// <param name="Process">未上線生產的工序</param>
        /// <param name="DelayTime">逾時的時間(以分鐘為單位)</param>
        /// <param name="EmpId">解除逾時通知的作業人員工號</param>
        /// <param name="EmpName">解除逾時通知的作業人員姓名</param>
        /// <param name="DissolutionTime">解除逾時通知的時間</param>
        /// <returns></returns>
        private int InsertDissolutionLog(string LotNum, string PartNum, string Qnty, string Department,
            string ExportProcess, string ExportTime, string Process, string DelayTime, string EmpId, string EmpName,
            string DissolutionTime)
        {
            var result = 0;
            var strComm = string.Format("insert into WorkPaperDelayLog(LotNum,PartNum,Qnty,Department,ExportProcess," +
                "ExportTime,Process,DelayTime,EmpId,EmpName,DissolutionTime) values('{0}','{1}','{2}','{3}','{4}'," +
                "'{5}','{6}','{7}','{8}','{9}','{10}')", LotNum, PartNum, Qnty, Department, ExportProcess, ExportTime,
                Process, DelayTime, EmpId, EmpName, DissolutionTime);
            using (SqlConnection sqlcon = new SqlConnection(ConnME))
            {
                SqlCommand sqlcomm = new SqlCommand(strComm, sqlcon);
                try
                {
                    sqlcon.Open();
                    result = sqlcomm.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    writerLog.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " InsertDissolutionLog()-" +
                        ex.Message);
                    writerLog.Flush();
                    SendMail("sm4@ewpcb.com.tw", "Ewproject System", "ewa05@ewpcb.com.tw",
                                "發生例外錯誤！", "防焊前處理出站進印刷房逾時檢查寫入SQL，發生不可預期之錯誤！<br/>" +
                                "請查看Log紀錄。<br/>發生時間：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                }
            }
            return result;
        }
        #endregion

        #region Send Mail
        /// <summary>
        /// 寄出電子郵件
        /// </summary>
        /// <param name="from">寄件者地址</param>
        /// <param name="display">寄件者名稱</param>
        /// <param name="to">收件人地址</param>
        /// <param name="sub">郵件主旨</param>
        /// <param name="body">郵件內容</param>
        private void SendMail(string from, string display, string to, string sub, string body)
        {
            //建立寄件者地址與名稱
            MailAddress ReceiverAddress = new MailAddress(from, display);
            //建立收件者地址
            MailAddress SendAddress = new MailAddress(to);
            //建立E-MAIL相關設定與訊息
            MailMessage SendMail = new MailMessage(ReceiverAddress, SendAddress);
            //Mail以HTML格式寄送
            SendMail.IsBodyHtml = true;
            //設定信件內容編碼為UTF8
            SendMail.BodyEncoding = Encoding.UTF8;
            //設定信件主旨編碼為UTF8
            SendMail.SubjectEncoding = Encoding.UTF8;
            //設定信件優先權為普通
            SendMail.Priority = MailPriority.Normal;
            SendMail.Subject = sub;//主旨
            SendMail.Body = body;//內容
            //建立一個信件通訊並設定郵件主機地址與通訊埠號
            SmtpClient MySmtp = new SmtpClient("ms1.ewpcb.com.tw", 25);
            //設定寄件者的帳號與密碼
            MySmtp.Credentials = new NetworkCredential("sm4", "sm4@ew");
            try
            {
                MySmtp.Send(SendMail);
            }
            catch (Exception ex)
            {
                writerLog.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "  SendMail()-" + ex.Message);
                writerLog.Flush();
            }
            finally
            {
                MySmtp = null;
                SendMail.Dispose();
            }
        }
        #endregion
    }
}
