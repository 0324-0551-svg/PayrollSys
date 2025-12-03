namespace PayrollSystem.Forms
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuEmployees;
        private System.Windows.Forms.ToolStripMenuItem menuAttendance;
        private System.Windows.Forms.ToolStripMenuItem menuPayroll;
        private System.Windows.Forms.ToolStripMenuItem menuReports;
        private System.Windows.Forms.ToolStripMenuItem menuSettings;
        private System.Windows.Forms.ToolStripMenuItem menuLogout;

        private System.Windows.Forms.Label lblTotalEmployeesLabel;
        private System.Windows.Forms.Label lblPayrollProcessedLabel;
        private System.Windows.Forms.Label lblTotalEmployees;
        private System.Windows.Forms.Label lblPayrollProcessed;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }


        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuEmployees = new System.Windows.Forms.ToolStripMenuItem();
            this.menuAttendance = new System.Windows.Forms.ToolStripMenuItem();
            this.menuPayroll = new System.Windows.Forms.ToolStripMenuItem();
            this.menuReports = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.menuLogout = new System.Windows.Forms.ToolStripMenuItem();
            this.lblTotalEmployeesLabel = new System.Windows.Forms.Label();
            this.lblPayrollProcessedLabel = new System.Windows.Forms.Label();
            this.lblTotalEmployees = new System.Windows.Forms.Label();
            this.lblPayrollProcessed = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuEmployees,
            this.menuAttendance,
            this.menuPayroll,
            this.menuReports,
            this.menuSettings,
            this.menuLogout});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(682, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menuEmployees
            // 
            this.menuEmployees.Name = "menuEmployees";
            this.menuEmployees.Size = new System.Drawing.Size(91, 24);
            this.menuEmployees.Text = "Employees";
            this.menuEmployees.Click += new System.EventHandler(this.menuEmployees_Click);
            // 
            // menuAttendance
            // 
            this.menuAttendance.Name = "menuAttendance";
            this.menuAttendance.Size = new System.Drawing.Size(94, 24);
            this.menuAttendance.Text = "Attendance";
            this.menuAttendance.Click += new System.EventHandler(this.menuAttendance_Click);
            // 
            // menuPayroll
            // 
            this.menuPayroll.Name = "menuPayroll";
            this.menuPayroll.Size = new System.Drawing.Size(66, 24);
            this.menuPayroll.Text = "Payroll";
            this.menuPayroll.Click += new System.EventHandler(this.menuPayroll_Click);
            // 
            // menuReports
            // 
            this.menuReports.Name = "menuReports";
            this.menuReports.Size = new System.Drawing.Size(72, 24);
            this.menuReports.Text = "Reports";
            this.menuReports.Click += new System.EventHandler(this.menuReports_Click);
            // 
            // menuSettings
            // 
            this.menuSettings.Name = "menuSettings";
            this.menuSettings.Size = new System.Drawing.Size(74, 24);
            this.menuSettings.Text = "Settings";
            this.menuSettings.Click += new System.EventHandler(this.menuSettings_Click);
            // 
            // menuLogout
            // 
            this.menuLogout.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.menuLogout.Name = "menuLogout";
            this.menuLogout.Size = new System.Drawing.Size(66, 24);
            this.menuLogout.Text = "Logout";
            this.menuLogout.Click += new System.EventHandler(this.menuLogout_Click);
            // 
            // lblTotalEmployeesLabel
            // 
            this.lblTotalEmployeesLabel.AutoSize = true;
            this.lblTotalEmployeesLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTotalEmployeesLabel.Location = new System.Drawing.Point(30, 60);
            this.lblTotalEmployeesLabel.Name = "lblTotalEmployeesLabel";
            this.lblTotalEmployeesLabel.Size = new System.Drawing.Size(124, 23);
            this.lblTotalEmployeesLabel.TabIndex = 1;
            this.lblTotalEmployeesLabel.Text = "Total Employees:";
            // 
            // lblPayrollProcessedLabel
            // 
            this.lblPayrollProcessedLabel.AutoSize = true;
            this.lblPayrollProcessedLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblPayrollProcessedLabel.Location = new System.Drawing.Point(30, 100);
            this.lblPayrollProcessedLabel.Name = "lblPayrollProcessedLabel";
            this.lblPayrollProcessedLabel.Size = new System.Drawing.Size(150, 23);
            this.lblPayrollProcessedLabel.TabIndex = 2;
            this.lblPayrollProcessedLabel.Text = "Payroll Processed:";
            // 
            // lblTotalEmployees
            // 
            this.lblTotalEmployees.AutoSize = true;
            this.lblTotalEmployees.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblTotalEmployees.Location = new System.Drawing.Point(190, 60);
            this.lblTotalEmployees.Name = "lblTotalEmployees";
            this.lblTotalEmployees.Size = new System.Drawing.Size(18, 23);
            this.lblTotalEmployees.TabIndex = 3;
            this.lblTotalEmployees.Text = "0";
            // 
            // lblPayrollProcessed
            // 
            this.lblPayrollProcessed.AutoSize = true;
            this.lblPayrollProcessed.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblPayrollProcessed.Location = new System.Drawing.Point(190, 100);
            this.lblPayrollProcessed.Name = "lblPayrollProcessed";
            this.lblPayrollProcessed.Size = new System.Drawing.Size(18, 23);
            this.lblPayrollProcessed.TabIndex = 4;
            this.lblPayrollProcessed.Text = "0";
            // 
            // MainForm
            // 
            this.ClientSize = new System.Drawing.Size(682, 153);
            this.Controls.Add(this.lblPayrollProcessed);
            this.Controls.Add(this.lblTotalEmployees);
            this.Controls.Add(this.lblPayrollProcessedLabel);
            this.Controls.Add(this.lblTotalEmployeesLabel);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "Payroll System - Dashboard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
