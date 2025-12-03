using PayrollSystem.DataAccess;
using System;
using System.Linq;
using System.Windows.Forms;

namespace PayrollSystem.Forms
{
    public partial class MainForm : Form
    {
        private readonly EmployeeRepository _employeeRepo = new();
        private readonly PayrollPeriodRepository _payrollPeriodRepo = new();
        private readonly PayrollRepository _payrollRepo = new();

        public MainForm()
        {
            InitializeComponent();
            LoadDashboard();
        }

        private void LoadDashboard()
        {
            try
            {
                var employees = _employeeRepo.GetAllEmployees();
                lblTotalEmployees.Text = employees.Count.ToString();

                var currentPeriod = _payrollPeriodRepo.GetCurrentPayrollPeriod();
                if (currentPeriod != null)
                {
                    var payrolls = _payrollRepo.GetPayrollByPeriod(currentPeriod.PayrollPeriodID);
                    lblPayrollProcessed.Text = payrolls.Count.ToString();
                }
                else
                {
                    lblPayrollProcessed.Text = "0";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load dashboard data: " + ex.Message);
            }
        }

        private void menuEmployees_Click(object sender, EventArgs e)
        {
            using var employeeForm = new EmployeeForm();
            employeeForm.ShowDialog();
            LoadDashboard();
        }

        private void menuAttendance_Click(object sender, EventArgs e)
        {
            using var attendanceForm = new AttendanceForm();
            attendanceForm.ShowDialog();
        }

        private void menuPayroll_Click(object sender, EventArgs e)
        {
            using var payrollForm = new PayrollForm();
            payrollForm.ShowDialog();
            LoadDashboard();
        }

        private void menuReports_Click(object sender, EventArgs e)
        {
            using var reportForm = new ReportForm();
            reportForm.ShowDialog();
        }

        private void menuSettings_Click(object sender, EventArgs e)
        {
            using var settingsForm = new SettingsForm();
            settingsForm.ShowDialog();
        }

        private void menuLogout_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
