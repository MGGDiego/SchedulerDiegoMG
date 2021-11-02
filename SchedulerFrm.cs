using SchedulerClass;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SchedulerWindows
{
    public partial class SchedulerFrm : Form
    {
        private Scheduler scheduler;
        private bool hasInputDate;
        private bool hasEndDate;

        public SchedulerFrm()
        {
            this.scheduler = new Scheduler();
            this.hasInputDate = false;
            this.hasEndDate = false;
            InitializeComponent();
            InstanceComponet();
        }

        private void InstanceComponet()
        {
            this.dtpCurrentDate.CustomFormat = "dd/MM/yyyy";
            this.dtpInputTime.CustomFormat = " ";
            this.dtpStartDate.CustomFormat = "dd/MM/yyyy";
            this.dtpStartDate.Value = new DateTime(DateTime.Today.Year, 01, 01);
            this.dtpEndDate.CustomFormat = " ";
            this.dtpOutputDateTime.CustomFormat = " ";
            this.dtpOutputDateTime.Enabled = false;
            this.tbOutputDescription.Enabled = false;
            this.chlbWeek.Visible = false;
            this.chlbWeek.CheckOnClick = true;
            this.nupDays.Size = new Size(170, 22);
        }

        private void UploadPropierties()
        {
            this.scheduler.Type = this.cbType.Text;
            this.scheduler.Occurs = this.cbOccurs.Text;
            this.scheduler.CurrentDate = this.dtpCurrentDate.Value;
            this.scheduler.InputDate = this.hasInputDate ? this.dtpInputTime.Value : new DateTime?();
            this.scheduler.StartDate = this.dtpStartDate.Value;
            this.scheduler.EndDate = this.hasEndDate ? this.dtpEndDate.Value : new DateTime?();
            this.scheduler.OccursValue = Convert.ToInt32(this.nupDays.Value);
            this.scheduler.TimeConfiguration = null;
            if (this.chbOccursOnce.Checked)
            {
                this.scheduler.TimeConfiguration =
                    new TimeConfiguration(this.dtpStartHour.Value, this.dtpEndHour.Value, false);
                this.scheduler.TimeConfiguration.OnceTime = this.dtpOccursOnceTime.Value;
            }
            else if (this.chbOccursEvery.Checked)
            {
                this.scheduler.TimeConfiguration =
                    new TimeConfiguration(this.dtpStartHour.Value, this.dtpEndHour.Value, true);
                this.scheduler.TimeConfiguration.OccursTime = this.cbOccursEvery.Text;
                this.scheduler.TimeConfiguration.OccursTimeValue = Convert.ToInt32(this.nupHours.Value);
            }
            this.scheduler.WeekValue = this.chlbWeek.CheckedItems.OfType<string>().ToArray();
        }

        private void BtCalculate_Click(object sender, EventArgs e)
        {
            this.UploadPropierties();

            try
            {
                this.dtpOutputDateTime.Value = this.scheduler.CalculateDates();
                this.tbOutputDescription.Text = this.scheduler.OutDescription;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        private void CbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cbType.Text.Equals("Once"))
            {
                this.dtpInputTime.Enabled = true;
                this.cbOccurs.Enabled = false;
                this.nupDays.Enabled = false;
            }
            else
            {
                this.dtpInputTime.Enabled = false;
                this.cbOccurs.Enabled = true;
                this.nupDays.Enabled = true;
            }
        }

        private void DateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            if (sender == this.dtpInputTime)
            {
                this.dtpInputTime.CustomFormat = "dd/MM/yyyy";
                this.hasInputDate = true;
            }
            if (sender == this.dtpEndDate)
            {
                this.dtpEndDate.CustomFormat = "dd/MM/yyyy";
                this.hasEndDate = true;
            }
            if (sender == this.dtpOutputDateTime)
            {
                this.dtpOutputDateTime.CustomFormat = "dd/MM/yyyy";
            }
        }

        private void CbOccurs_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (this.cbOccurs.Text)
            {
                case "Daily":
                    this.nupDays.Size = new Size(170, 22);
                    this.lbDays.Text = "Day(s)";
                    this.chlbWeek.Visible = false;
                    break;
                case "Weekly":
                    this.nupDays.Size = new Size(36, 22);
                    this.lbDays.Text = "Week(s)";
                    this.chlbWeek.Visible = true;
                    break;
                case "Monthly":
                    this.nupDays.Size = new Size(170, 22);
                    this.lbDays.Text = "Month(s)";
                    this.chlbWeek.Visible = false;
                    break;
                case "Yearly":
                    this.nupDays.Size = new Size(170, 22);
                    this.lbDays.Text = "Year(s)";
                    this.chlbWeek.Visible = false;
                    break;
                default:
                    this.lbDays.Text = "Days(s)";
                    break;
            }
        }

        private void chbOccursOnce_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chbOccursOnce.Checked == true)
            {
                this.chbOccursEvery.Checked = false;
            }
            else if (this.chbOccursOnce.Checked == false)
            {
                this.chbOccursOnce.Checked = false;
            }
        }

        private void chbOccursEvery_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chbOccursEvery.Checked == true)
            {
                this.chbOccursOnce.Checked = false;
            }
            else if (this.chbOccursEvery.Checked == false)
            {
                this.chbOccursEvery.Checked = false;
            }
        }
    }
}
