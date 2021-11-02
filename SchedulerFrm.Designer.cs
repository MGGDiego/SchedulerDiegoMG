
namespace SchedulerWindows
{
    partial class SchedulerFrm
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.tbOutputDescription = new System.Windows.Forms.TextBox();
            this.lbImput = new System.Windows.Forms.Label();
            this.lbType = new System.Windows.Forms.Label();
            this.lbDateTime = new System.Windows.Forms.Label();
            this.lbOccurs = new System.Windows.Forms.Label();
            this.lbStarDate = new System.Windows.Forms.Label();
            this.lbEndDate = new System.Windows.Forms.Label();
            this.lbNextExecutionTime = new System.Windows.Forms.Label();
            this.lbDescription = new System.Windows.Forms.Label();
            this.lbDays = new System.Windows.Forms.Label();
            this.cbType = new System.Windows.Forms.ComboBox();
            this.cbOccurs = new System.Windows.Forms.ComboBox();
            this.btCalculate = new System.Windows.Forms.Button();
            this.dtpCurrentDate = new System.Windows.Forms.DateTimePicker();
            this.dtpInputTime = new System.Windows.Forms.DateTimePicker();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.dtpOutputDateTime = new System.Windows.Forms.DateTimePicker();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.chlbWeek = new System.Windows.Forms.CheckedListBox();
            this.nupDays = new System.Windows.Forms.NumericUpDown();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel7 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.panel8 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.panel9 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbHours = new System.Windows.Forms.Label();
            this.lbStartHour = new System.Windows.Forms.Label();
            this.lbEndHour = new System.Windows.Forms.Label();
            this.dtpEndHour = new System.Windows.Forms.DateTimePicker();
            this.dtpStartHour = new System.Windows.Forms.DateTimePicker();
            this.dtpOccursOnceTime = new System.Windows.Forms.DateTimePicker();
            this.nupHours = new System.Windows.Forms.NumericUpDown();
            this.cbOccursEvery = new System.Windows.Forms.ComboBox();
            this.chbOccursEvery = new System.Windows.Forms.CheckBox();
            this.chbOccursOnce = new System.Windows.Forms.CheckBox();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nupDays)).BeginInit();
            this.panel4.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nupHours)).BeginInit();
            this.SuspendLayout();
            // 
            // tbOutputDescription
            // 
            this.tbOutputDescription.Location = new System.Drawing.Point(20, 70);
            this.tbOutputDescription.Margin = new System.Windows.Forms.Padding(4);
            this.tbOutputDescription.Multiline = true;
            this.tbOutputDescription.Name = "tbOutputDescription";
            this.tbOutputDescription.Size = new System.Drawing.Size(747, 64);
            this.tbOutputDescription.TabIndex = 6;
            // 
            // lbImput
            // 
            this.lbImput.AutoSize = true;
            this.lbImput.Location = new System.Drawing.Point(17, 20);
            this.lbImput.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbImput.Name = "lbImput";
            this.lbImput.Size = new System.Drawing.Size(89, 17);
            this.lbImput.TabIndex = 7;
            this.lbImput.Text = "Current Date";
            // 
            // lbType
            // 
            this.lbType.AutoSize = true;
            this.lbType.Location = new System.Drawing.Point(445, 21);
            this.lbType.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbType.Name = "lbType";
            this.lbType.Size = new System.Drawing.Size(40, 17);
            this.lbType.TabIndex = 8;
            this.lbType.Text = "Type";
            // 
            // lbDateTime
            // 
            this.lbDateTime.AutoSize = true;
            this.lbDateTime.Location = new System.Drawing.Point(17, 23);
            this.lbDateTime.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbDateTime.Name = "lbDateTime";
            this.lbDateTime.Size = new System.Drawing.Size(69, 17);
            this.lbDateTime.TabIndex = 9;
            this.lbDateTime.Text = "DateTime";
            // 
            // lbOccurs
            // 
            this.lbOccurs.AutoSize = true;
            this.lbOccurs.Location = new System.Drawing.Point(17, 51);
            this.lbOccurs.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbOccurs.Name = "lbOccurs";
            this.lbOccurs.Size = new System.Drawing.Size(53, 17);
            this.lbOccurs.TabIndex = 10;
            this.lbOccurs.Text = "Occurs";
            // 
            // lbStarDate
            // 
            this.lbStarDate.AutoSize = true;
            this.lbStarDate.Location = new System.Drawing.Point(17, 51);
            this.lbStarDate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbStarDate.Name = "lbStarDate";
            this.lbStarDate.Size = new System.Drawing.Size(72, 17);
            this.lbStarDate.TabIndex = 11;
            this.lbStarDate.Text = "Start Date";
            // 
            // lbEndDate
            // 
            this.lbEndDate.AutoSize = true;
            this.lbEndDate.Location = new System.Drawing.Point(445, 51);
            this.lbEndDate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbEndDate.Name = "lbEndDate";
            this.lbEndDate.Size = new System.Drawing.Size(67, 17);
            this.lbEndDate.TabIndex = 12;
            this.lbEndDate.Text = "End Date";
            // 
            // lbNextExecutionTime
            // 
            this.lbNextExecutionTime.AutoSize = true;
            this.lbNextExecutionTime.Location = new System.Drawing.Point(17, 21);
            this.lbNextExecutionTime.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbNextExecutionTime.Name = "lbNextExecutionTime";
            this.lbNextExecutionTime.Size = new System.Drawing.Size(136, 17);
            this.lbNextExecutionTime.TabIndex = 13;
            this.lbNextExecutionTime.Text = "Next Execution Time";
            // 
            // lbDescription
            // 
            this.lbDescription.AutoSize = true;
            this.lbDescription.Location = new System.Drawing.Point(17, 49);
            this.lbDescription.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbDescription.Name = "lbDescription";
            this.lbDescription.Size = new System.Drawing.Size(79, 17);
            this.lbDescription.TabIndex = 14;
            this.lbDescription.Text = "Description";
            // 
            // lbDays
            // 
            this.lbDays.AutoSize = true;
            this.lbDays.Location = new System.Drawing.Point(445, 51);
            this.lbDays.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbDays.Name = "lbDays";
            this.lbDays.Size = new System.Drawing.Size(50, 17);
            this.lbDays.TabIndex = 15;
            this.lbDays.Text = "Day(s)";
            // 
            // cbType
            // 
            this.cbType.FormattingEnabled = true;
            this.cbType.Items.AddRange(new object[] {
            "Once",
            "Recurring"});
            this.cbType.Location = new System.Drawing.Point(538, 14);
            this.cbType.Margin = new System.Windows.Forms.Padding(4);
            this.cbType.Name = "cbType";
            this.cbType.Size = new System.Drawing.Size(227, 24);
            this.cbType.TabIndex = 16;
            this.cbType.SelectedIndexChanged += new System.EventHandler(this.CbType_SelectedIndexChanged);
            // 
            // cbOccurs
            // 
            this.cbOccurs.FormattingEnabled = true;
            this.cbOccurs.Items.AddRange(new object[] {
            "Daily",
            "Weekly",
            "Monthly",
            "Yearly"});
            this.cbOccurs.Location = new System.Drawing.Point(175, 48);
            this.cbOccurs.Margin = new System.Windows.Forms.Padding(4);
            this.cbOccurs.Name = "cbOccurs";
            this.cbOccurs.Size = new System.Drawing.Size(228, 24);
            this.cbOccurs.TabIndex = 17;
            this.cbOccurs.SelectedIndexChanged += new System.EventHandler(this.CbOccurs_SelectedIndexChanged);
            // 
            // btCalculate
            // 
            this.btCalculate.Location = new System.Drawing.Point(570, 814);
            this.btCalculate.Margin = new System.Windows.Forms.Padding(4);
            this.btCalculate.Name = "btCalculate";
            this.btCalculate.Size = new System.Drawing.Size(229, 28);
            this.btCalculate.TabIndex = 18;
            this.btCalculate.Text = "Calculate Next Date";
            this.btCalculate.UseVisualStyleBackColor = true;
            this.btCalculate.Click += new System.EventHandler(this.BtCalculate_Click);
            // 
            // dtpCurrentDate
            // 
            this.dtpCurrentDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpCurrentDate.Location = new System.Drawing.Point(175, 16);
            this.dtpCurrentDate.Margin = new System.Windows.Forms.Padding(4);
            this.dtpCurrentDate.Name = "dtpCurrentDate";
            this.dtpCurrentDate.Size = new System.Drawing.Size(228, 22);
            this.dtpCurrentDate.TabIndex = 19;
            // 
            // dtpInputTime
            // 
            this.dtpInputTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpInputTime.Location = new System.Drawing.Point(175, 18);
            this.dtpInputTime.Margin = new System.Windows.Forms.Padding(4);
            this.dtpInputTime.Name = "dtpInputTime";
            this.dtpInputTime.Size = new System.Drawing.Size(228, 22);
            this.dtpInputTime.TabIndex = 20;
            this.dtpInputTime.ValueChanged += new System.EventHandler(this.DateTimePicker_ValueChanged);
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpStartDate.Location = new System.Drawing.Point(175, 46);
            this.dtpStartDate.Margin = new System.Windows.Forms.Padding(4);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(228, 22);
            this.dtpStartDate.TabIndex = 21;
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEndDate.Location = new System.Drawing.Point(537, 46);
            this.dtpEndDate.Margin = new System.Windows.Forms.Padding(4);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(230, 22);
            this.dtpEndDate.TabIndex = 22;
            this.dtpEndDate.ValueChanged += new System.EventHandler(this.DateTimePicker_ValueChanged);
            // 
            // dtpOutputDateTime
            // 
            this.dtpOutputDateTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpOutputDateTime.Location = new System.Drawing.Point(175, 16);
            this.dtpOutputDateTime.Margin = new System.Windows.Forms.Padding(4);
            this.dtpOutputDateTime.Name = "dtpOutputDateTime";
            this.dtpOutputDateTime.Size = new System.Drawing.Size(590, 22);
            this.dtpOutputDateTime.TabIndex = 23;
            this.dtpOutputDateTime.ValueChanged += new System.EventHandler(this.DateTimePicker_ValueChanged);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel2.Controls.Add(this.dtpEndDate);
            this.panel2.Controls.Add(this.dtpCurrentDate);
            this.panel2.Controls.Add(this.lbImput);
            this.panel2.Controls.Add(this.dtpStartDate);
            this.panel2.Controls.Add(this.cbType);
            this.panel2.Controls.Add(this.lbEndDate);
            this.panel2.Controls.Add(this.lbStarDate);
            this.panel2.Controls.Add(this.lbType);
            this.panel2.Location = new System.Drawing.Point(32, 200);
            this.panel2.Margin = new System.Windows.Forms.Padding(4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(789, 87);
            this.panel2.TabIndex = 25;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel3.Controls.Add(this.chlbWeek);
            this.panel3.Controls.Add(this.nupDays);
            this.panel3.Controls.Add(this.lbDateTime);
            this.panel3.Controls.Add(this.lbOccurs);
            this.panel3.Controls.Add(this.lbDays);
            this.panel3.Controls.Add(this.dtpInputTime);
            this.panel3.Controls.Add(this.cbOccurs);
            this.panel3.Location = new System.Drawing.Point(32, 342);
            this.panel3.Margin = new System.Windows.Forms.Padding(4);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(789, 90);
            this.panel3.TabIndex = 26;
            // 
            // chlbWeek
            // 
            this.chlbWeek.FormattingEnabled = true;
            this.chlbWeek.Items.AddRange(new object[] {
            "Monday",
            "Tuesday",
            "Wednesday",
            "Thursday",
            "Friday",
            "Saturday",
            "Sunday"});
            this.chlbWeek.Location = new System.Drawing.Point(593, 16);
            this.chlbWeek.Name = "chlbWeek";
            this.chlbWeek.Size = new System.Drawing.Size(174, 72);
            this.chlbWeek.TabIndex = 40;
            // 
            // nupDays
            // 
            this.nupDays.Location = new System.Drawing.Point(538, 49);
            this.nupDays.Margin = new System.Windows.Forms.Padding(4);
            this.nupDays.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nupDays.Name = "nupDays";
            this.nupDays.Size = new System.Drawing.Size(48, 22);
            this.nupDays.TabIndex = 29;
            this.nupDays.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel4.Controls.Add(this.tbOutputDescription);
            this.panel4.Controls.Add(this.lbNextExecutionTime);
            this.panel4.Controls.Add(this.dtpOutputDateTime);
            this.panel4.Controls.Add(this.lbDescription);
            this.panel4.Location = new System.Drawing.Point(32, 646);
            this.panel4.Margin = new System.Windows.Forms.Padding(4);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(789, 150);
            this.panel4.TabIndex = 27;
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.panel6.Location = new System.Drawing.Point(32, 180);
            this.panel6.Margin = new System.Windows.Forms.Padding(4);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(789, 12);
            this.panel6.TabIndex = 31;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label1.Location = new System.Drawing.Point(43, 152);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(141, 25);
            this.label1.TabIndex = 30;
            this.label1.Text = "Configuration";
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.panel7.Location = new System.Drawing.Point(32, 323);
            this.panel7.Margin = new System.Windows.Forms.Padding(4);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(789, 12);
            this.panel7.TabIndex = 33;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label2.Location = new System.Drawing.Point(43, 294);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 25);
            this.label2.TabIndex = 32;
            this.label2.Text = "Input";
            // 
            // panel8
            // 
            this.panel8.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.panel8.Location = new System.Drawing.Point(32, 626);
            this.panel8.Margin = new System.Windows.Forms.Padding(4);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(789, 12);
            this.panel8.TabIndex = 35;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label3.Location = new System.Drawing.Point(43, 598);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 25);
            this.label3.TabIndex = 34;
            this.label3.Text = "Output";
            // 
            // panel9
            // 
            this.panel9.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.panel9.Location = new System.Drawing.Point(32, 67);
            this.panel9.Margin = new System.Windows.Forms.Padding(4);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(789, 12);
            this.panel9.TabIndex = 37;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label4.Location = new System.Drawing.Point(43, 31);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(153, 32);
            this.label4.TabIndex = 36;
            this.label4.Text = "Scheduler";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(46, 92);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(450, 34);
            this.label5.TabIndex = 38;
            this.label5.Text = "This program calculates the next execution date from the current date.\n You can u" +
    "se two types of settings: Once and Recurring.";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel1.Controls.Add(this.lbHours);
            this.panel1.Controls.Add(this.lbStartHour);
            this.panel1.Controls.Add(this.lbEndHour);
            this.panel1.Controls.Add(this.dtpEndHour);
            this.panel1.Controls.Add(this.dtpStartHour);
            this.panel1.Controls.Add(this.dtpOccursOnceTime);
            this.panel1.Controls.Add(this.nupHours);
            this.panel1.Controls.Add(this.cbOccursEvery);
            this.panel1.Controls.Add(this.chbOccursEvery);
            this.panel1.Controls.Add(this.chbOccursOnce);
            this.panel1.Location = new System.Drawing.Point(32, 484);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(789, 111);
            this.panel1.TabIndex = 39;
            // 
            // lbHours
            // 
            this.lbHours.AutoSize = true;
            this.lbHours.Location = new System.Drawing.Point(445, 47);
            this.lbHours.Name = "lbHours";
            this.lbHours.Size = new System.Drawing.Size(56, 17);
            this.lbHours.TabIndex = 8;
            this.lbHours.Text = "Hour(s)";
            // 
            // lbStartHour
            // 
            this.lbStartHour.AutoSize = true;
            this.lbStartHour.Location = new System.Drawing.Point(17, 74);
            this.lbStartHour.Name = "lbStartHour";
            this.lbStartHour.Size = new System.Drawing.Size(74, 17);
            this.lbStartHour.TabIndex = 7;
            this.lbStartHour.Text = "Starting At";
            // 
            // lbEndHour
            // 
            this.lbEndHour.AutoSize = true;
            this.lbEndHour.Location = new System.Drawing.Point(445, 74);
            this.lbEndHour.Name = "lbEndHour";
            this.lbEndHour.Size = new System.Drawing.Size(50, 17);
            this.lbEndHour.TabIndex = 6;
            this.lbEndHour.Text = "End At";
            // 
            // dtpEndHour
            // 
            this.dtpEndHour.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpEndHour.Location = new System.Drawing.Point(538, 69);
            this.dtpEndHour.Name = "dtpEndHour";
            this.dtpEndHour.ShowUpDown = true;
            this.dtpEndHour.Size = new System.Drawing.Size(229, 22);
            this.dtpEndHour.TabIndex = 5;
            // 
            // dtpStartHour
            // 
            this.dtpStartHour.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpStartHour.Location = new System.Drawing.Point(175, 74);
            this.dtpStartHour.Name = "dtpStartHour";
            this.dtpStartHour.ShowUpDown = true;
            this.dtpStartHour.Size = new System.Drawing.Size(228, 22);
            this.dtpStartHour.TabIndex = 4;
            // 
            // dtpOccursOnceTime
            // 
            this.dtpOccursOnceTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpOccursOnceTime.Location = new System.Drawing.Point(175, 16);
            this.dtpOccursOnceTime.Name = "dtpOccursOnceTime";
            this.dtpOccursOnceTime.ShowUpDown = true;
            this.dtpOccursOnceTime.Size = new System.Drawing.Size(228, 22);
            this.dtpOccursOnceTime.TabIndex = 3;
            // 
            // nupHours
            // 
            this.nupHours.Location = new System.Drawing.Point(538, 42);
            this.nupHours.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nupHours.Name = "nupHours";
            this.nupHours.Size = new System.Drawing.Size(229, 22);
            this.nupHours.TabIndex = 0;
            this.nupHours.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // cbOccursEvery
            // 
            this.cbOccursEvery.FormattingEnabled = true;
            this.cbOccursEvery.Items.AddRange(new object[] {
            "Second",
            "Minute",
            "Hour"});
            this.cbOccursEvery.Location = new System.Drawing.Point(175, 44);
            this.cbOccursEvery.Name = "cbOccursEvery";
            this.cbOccursEvery.Size = new System.Drawing.Size(228, 24);
            this.cbOccursEvery.TabIndex = 2;
            // 
            // chbOccursEvery
            // 
            this.chbOccursEvery.AutoSize = true;
            this.chbOccursEvery.Location = new System.Drawing.Point(20, 46);
            this.chbOccursEvery.Name = "chbOccursEvery";
            this.chbOccursEvery.Size = new System.Drawing.Size(115, 21);
            this.chbOccursEvery.TabIndex = 1;
            this.chbOccursEvery.Text = "Occurs Every";
            this.chbOccursEvery.UseVisualStyleBackColor = true;
            this.chbOccursEvery.CheckedChanged += new System.EventHandler(this.chbOccursEvery_CheckedChanged);
            // 
            // chbOccursOnce
            // 
            this.chbOccursOnce.AutoSize = true;
            this.chbOccursOnce.Location = new System.Drawing.Point(20, 20);
            this.chbOccursOnce.Name = "chbOccursOnce";
            this.chbOccursOnce.Size = new System.Drawing.Size(130, 21);
            this.chbOccursOnce.TabIndex = 0;
            this.chbOccursOnce.Text = "Occurs Once At";
            this.chbOccursOnce.UseVisualStyleBackColor = true;
            this.chbOccursOnce.CheckedChanged += new System.EventHandler(this.chbOccursOnce_CheckedChanged);
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.panel5.Location = new System.Drawing.Point(32, 465);
            this.panel5.Margin = new System.Windows.Forms.Padding(4);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(789, 12);
            this.panel5.TabIndex = 41;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label6.Location = new System.Drawing.Point(41, 436);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(167, 25);
            this.label6.TabIndex = 40;
            this.label6.Text = "Daily Frecuency";
            // 
            // SchedulerFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(852, 859);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btCalculate);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.panel9);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.panel8);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.panel7);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "SchedulerFrm";
            this.Text = "Scheduler";
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nupDays)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nupHours)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox tbOutputDescription;
        private System.Windows.Forms.Label lbImput;
        private System.Windows.Forms.Label lbType;
        private System.Windows.Forms.Label lbDateTime;
        private System.Windows.Forms.Label lbOccurs;
        private System.Windows.Forms.Label lbStarDate;
        private System.Windows.Forms.Label lbEndDate;
        private System.Windows.Forms.Label lbNextExecutionTime;
        private System.Windows.Forms.Label lbDescription;
        private System.Windows.Forms.Label lbDays;
        private System.Windows.Forms.ComboBox cbType;
        private System.Windows.Forms.ComboBox cbOccurs;
        private System.Windows.Forms.Button btCalculate;
        private System.Windows.Forms.DateTimePicker dtpCurrentDate;
        private System.Windows.Forms.DateTimePicker dtpInputTime;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private System.Windows.Forms.DateTimePicker dtpOutputDateTime;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.NumericUpDown nupDays;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox chbOccursEvery;
        private System.Windows.Forms.CheckBox chbOccursOnce;
        private System.Windows.Forms.ComboBox cbOccursEvery;
        private System.Windows.Forms.DateTimePicker dtpOccursOnceTime;
        private System.Windows.Forms.NumericUpDown nupHours;
        private System.Windows.Forms.DateTimePicker dtpEndHour;
        private System.Windows.Forms.DateTimePicker dtpStartHour;
        private System.Windows.Forms.Label lbStartHour;
        private System.Windows.Forms.Label lbEndHour;
        private System.Windows.Forms.Label lbHours;
        private System.Windows.Forms.CheckedListBox chlbWeek;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label6;
    }
}

