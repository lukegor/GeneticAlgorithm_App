using static System.Net.Mime.MediaTypeNames;
using System.Windows.Forms;
using System.Xml.Linq;

namespace GeneticAlgorithm_App
{
    partial class GeneticAlgorithmForm
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            dgvStatistics = new DataGridView();
            lp = new DataGridViewTextBoxColumn();
            x_real = new DataGridViewTextBoxColumn();
            f_x = new DataGridViewTextBoxColumn();
            g_x = new DataGridViewTextBoxColumn();
            p_i = new DataGridViewTextBoxColumn();
            q_i = new DataGridViewTextBoxColumn();
            r = new DataGridViewTextBoxColumn();
            x_real_sel = new DataGridViewTextBoxColumn();
            x_bin = new DataGridViewTextBoxColumn();
            parents = new DataGridViewTextBoxColumn();
            p_c = new DataGridViewTextBoxColumn();
            crossing = new DataGridViewTextBoxColumn();
            mutation_points = new DataGridViewTextBoxColumn();
            after_mutation = new DataGridViewTextBoxColumn();
            after_mutation_xreal = new DataGridViewTextBoxColumn();
            new_f_x = new DataGridViewTextBoxColumn();
            label1 = new Label();
            txtLowerBound = new TextBox();
            txtUpperBound = new TextBox();
            label2 = new Label();
            label3 = new Label();
            txtPopulationSize = new TextBox();
            label4 = new Label();
            btnCalculate = new Button();
            cbxPrecision = new ComboBox();
            txtCrossoverProbability = new TextBox();
            txtMutationProbability = new TextBox();
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            txtGoal = new ComboBox();
            label8 = new Label();
            txtNoGenerations = new TextBox();
            chbIsElite = new CheckBox();
            chart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            dgvShares = new DataGridView();
            Column1 = new DataGridViewTextBoxColumn();
            Column2 = new DataGridViewTextBoxColumn();
            Column3 = new DataGridViewTextBoxColumn();
            Column4 = new DataGridViewTextBoxColumn();
            Column5 = new DataGridViewTextBoxColumn();
            dgbSimulationTopResults = new DataGridView();
            Column6 = new DataGridViewTextBoxColumn();
            Column7 = new DataGridViewTextBoxColumn();
            Column8 = new DataGridViewTextBoxColumn();
            Column9 = new DataGridViewTextBoxColumn();
            Column10 = new DataGridViewTextBoxColumn();
            Column11 = new DataGridViewTextBoxColumn();
            btnSimulation = new Button();
            mainTab = new TabControl();
            tabPage1 = new TabPage();
            tabPage2 = new TabPage();
            ((System.ComponentModel.ISupportInitialize)dgvStatistics).BeginInit();
            ((System.ComponentModel.ISupportInitialize)chart).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvShares).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgbSimulationTopResults).BeginInit();
            mainTab.SuspendLayout();
            tabPage1.SuspendLayout();
            tabPage2.SuspendLayout();
            SuspendLayout();
            // 
            // dgvStatistics
            // 
            dgvStatistics.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvStatistics.Columns.AddRange(new DataGridViewColumn[] { lp, x_real, f_x, g_x, p_i, q_i, r, x_real_sel, x_bin, parents, p_c, crossing, mutation_points, after_mutation, after_mutation_xreal, new_f_x });
            dgvStatistics.Location = new Point(-4, 3);
            dgvStatistics.Name = "dgvStatistics";
            dgvStatistics.Size = new Size(1689, 337);
            dgvStatistics.TabIndex = 0;
            // 
            // lp
            // 
            lp.HeaderText = "lp";
            lp.Name = "lp";
            // 
            // x_real
            // 
            x_real.HeaderText = "x_real";
            x_real.Name = "x_real";
            // 
            // f_x
            // 
            f_x.HeaderText = "f(x)";
            f_x.Name = "f_x";
            // 
            // g_x
            // 
            g_x.HeaderText = "g(x)";
            g_x.Name = "g_x";
            // 
            // p_i
            // 
            p_i.HeaderText = "p_i";
            p_i.Name = "p_i";
            // 
            // q_i
            // 
            q_i.HeaderText = "q_i";
            q_i.Name = "q_i";
            // 
            // r
            // 
            r.HeaderText = "r";
            r.Name = "r";
            // 
            // x_real_sel
            // 
            x_real_sel.HeaderText = "xreal po selekcji";
            x_real_sel.Name = "x_real_sel";
            // 
            // x_bin
            // 
            x_bin.HeaderText = "x_bin";
            x_bin.Name = "x_bin";
            // 
            // parents
            // 
            parents.HeaderText = "rodzice";
            parents.Name = "parents";
            // 
            // p_c
            // 
            p_c.HeaderText = "P_c";
            p_c.Name = "p_c";
            // 
            // crossing
            // 
            crossing.HeaderText = "pokolenie krzyżówkowe";
            crossing.Name = "crossing";
            // 
            // mutation_points
            // 
            mutation_points.HeaderText = "punkty mutacji";
            mutation_points.Name = "mutation_points";
            // 
            // after_mutation
            // 
            after_mutation.HeaderText = "po mutacji";
            after_mutation.Name = "after_mutation";
            // 
            // after_mutation_xreal
            // 
            after_mutation_xreal.HeaderText = "po mutacji xreal";
            after_mutation_xreal.Name = "after_mutation_xreal";
            // 
            // new_f_x
            // 
            new_f_x.HeaderText = "f(x)";
            new_f_x.Name = "new_f_x";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(21, 24);
            label1.Name = "label1";
            label1.Size = new Size(24, 15);
            label1.TabIndex = 1;
            label1.Text = "a =";
            // 
            // txtLowerBound
            // 
            txtLowerBound.Location = new Point(65, 21);
            txtLowerBound.Name = "txtLowerBound";
            txtLowerBound.Size = new Size(100, 23);
            txtLowerBound.TabIndex = 2;
            // 
            // txtUpperBound
            // 
            txtUpperBound.Location = new Point(241, 24);
            txtUpperBound.Name = "txtUpperBound";
            txtUpperBound.Size = new Size(100, 23);
            txtUpperBound.TabIndex = 4;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(197, 27);
            label2.Name = "label2";
            label2.Size = new Size(25, 15);
            label2.TabIndex = 3;
            label2.Text = "b =";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(360, 27);
            label3.Name = "label3";
            label3.Size = new Size(25, 15);
            label3.TabIndex = 5;
            label3.Text = "d =";
            // 
            // txtPopulationSize
            // 
            txtPopulationSize.Location = new Point(561, 24);
            txtPopulationSize.Name = "txtPopulationSize";
            txtPopulationSize.Size = new Size(100, 23);
            txtPopulationSize.TabIndex = 8;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(528, 29);
            label4.Name = "label4";
            label4.Size = new Size(27, 15);
            label4.TabIndex = 7;
            label4.Text = "N =";
            // 
            // btnCalculate
            // 
            btnCalculate.Location = new Point(1420, 24);
            btnCalculate.Name = "btnCalculate";
            btnCalculate.Size = new Size(75, 23);
            btnCalculate.TabIndex = 9;
            btnCalculate.Text = "START";
            btnCalculate.UseVisualStyleBackColor = true;
            btnCalculate.Click += BtnCalculate_Click;
            // 
            // cbxPrecision
            // 
            cbxPrecision.DropDownStyle = ComboBoxStyle.DropDownList;
            cbxPrecision.FormattingEnabled = true;
            cbxPrecision.Items.AddRange(new object[] { "0,1", "0,01", "0,001", "0,0001" });
            cbxPrecision.Location = new Point(390, 24);
            cbxPrecision.Name = "cbxPrecision";
            cbxPrecision.Size = new Size(121, 23);
            cbxPrecision.TabIndex = 10;
            // 
            // txtCrossoverProbability
            // 
            txtCrossoverProbability.Location = new Point(717, 24);
            txtCrossoverProbability.Name = "txtCrossoverProbability";
            txtCrossoverProbability.Size = new Size(100, 23);
            txtCrossoverProbability.TabIndex = 11;
            // 
            // txtMutationProbability
            // 
            txtMutationProbability.Location = new Point(874, 24);
            txtMutationProbability.Name = "txtMutationProbability";
            txtMutationProbability.Size = new Size(100, 23);
            txtMutationProbability.TabIndex = 12;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(675, 29);
            label5.Name = "label5";
            label5.Size = new Size(36, 15);
            label5.TabIndex = 13;
            label5.Text = "P_k =";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(827, 29);
            label6.Name = "label6";
            label6.Size = new Size(41, 15);
            label6.TabIndex = 14;
            label6.Text = "P_m =";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(1242, 29);
            label7.Name = "label7";
            label7.Size = new Size(35, 15);
            label7.TabIndex = 16;
            label7.Text = "Cel =";
            // 
            // txtGoal
            // 
            txtGoal.DropDownStyle = ComboBoxStyle.DropDownList;
            txtGoal.FormattingEnabled = true;
            txtGoal.Items.AddRange(new object[] { "Maximum" });
            txtGoal.Location = new Point(1283, 24);
            txtGoal.Name = "txtGoal";
            txtGoal.Size = new Size(121, 23);
            txtGoal.TabIndex = 17;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(994, 29);
            label8.Name = "label8";
            label8.Size = new Size(24, 15);
            label8.TabIndex = 19;
            label8.Text = "T =";
            // 
            // txtNoGenerations
            // 
            txtNoGenerations.Location = new Point(1024, 26);
            txtNoGenerations.Name = "txtNoGenerations";
            txtNoGenerations.Size = new Size(100, 23);
            txtNoGenerations.TabIndex = 18;
            // 
            // chbIsElite
            // 
            chbIsElite.AutoSize = true;
            chbIsElite.Location = new Point(1163, 28);
            chbIsElite.Name = "chbIsElite";
            chbIsElite.Size = new Size(48, 19);
            chbIsElite.TabIndex = 20;
            chbIsElite.Text = "Elita";
            chbIsElite.UseVisualStyleBackColor = true;
            // 
            // chart
            // 
            chartArea1.Name = "ChartArea1";
            chart.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            chart.Legends.Add(legend1);
            chart.Location = new Point(0, 10);
            chart.Name = "chart";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            chart.Series.Add(series1);
            chart.Size = new Size(1652, 316);
            chart.TabIndex = 21;
            chart.Text = "chart";
            // 
            // dgvShares
            // 
            dgvShares.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvShares.Columns.AddRange(new DataGridViewColumn[] { Column1, Column2, Column3, Column4, Column5 });
            dgvShares.Location = new Point(16, 448);
            dgvShares.Name = "dgvShares";
            dgvShares.Size = new Size(695, 194);
            dgvShares.TabIndex = 22;
            // 
            // Column1
            // 
            Column1.HeaderText = "lp.";
            Column1.Name = "Column1";
            Column1.Width = 50;
            // 
            // Column2
            // 
            Column2.HeaderText = "xreal";
            Column2.Name = "Column2";
            // 
            // Column3
            // 
            Column3.HeaderText = "xbin";
            Column3.Name = "Column3";
            // 
            // Column4
            // 
            Column4.HeaderText = "fx";
            Column4.Name = "Column4";
            // 
            // Column5
            // 
            Column5.HeaderText = "share (%)";
            Column5.Name = "Column5";
            Column5.Width = 50;
            // 
            // dgbSimulationTopResults
            // 
            dgbSimulationTopResults.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgbSimulationTopResults.Columns.AddRange(new DataGridViewColumn[] { Column6, Column7, Column8, Column9, Column10, Column11 });
            dgbSimulationTopResults.Location = new Point(963, 448);
            dgbSimulationTopResults.Name = "dgbSimulationTopResults";
            dgbSimulationTopResults.Size = new Size(711, 194);
            dgbSimulationTopResults.TabIndex = 23;
            // 
            // Column6
            // 
            Column6.HeaderText = "lp";
            Column6.Name = "Column6";
            Column6.Width = 50;
            // 
            // Column7
            // 
            Column7.HeaderText = "n";
            Column7.Name = "Column7";
            // 
            // Column8
            // 
            Column8.HeaderText = "pk";
            Column8.Name = "Column8";
            // 
            // Column9
            // 
            Column9.HeaderText = "pm";
            Column9.Name = "Column9";
            // 
            // Column10
            // 
            Column10.HeaderText = "T";
            Column10.Name = "Column10";
            // 
            // Column11
            // 
            Column11.HeaderText = "(Śr.) max fx";
            Column11.Name = "Column11";
            // 
            // btnSimulation
            // 
            btnSimulation.Location = new Point(1501, 16);
            btnSimulation.Name = "btnSimulation";
            btnSimulation.Size = new Size(104, 40);
            btnSimulation.TabIndex = 24;
            btnSimulation.Text = "PRZEPROWADŹ SYMULACJĘ";
            btnSimulation.UseVisualStyleBackColor = true;
            btnSimulation.Click += BtnSimulation_Click;
            // 
            // mainTab
            // 
            mainTab.Controls.Add(tabPage1);
            mainTab.Controls.Add(tabPage2);
            mainTab.Location = new Point(12, 67);
            mainTab.Name = "mainTab";
            mainTab.SelectedIndex = 0;
            mainTab.Size = new Size(1666, 360);
            mainTab.TabIndex = 25;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(dgvStatistics);
            tabPage1.Location = new Point(4, 24);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(1658, 332);
            tabPage1.TabIndex = 1;
            tabPage1.Text = "Statistics";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(chart);
            tabPage2.Location = new Point(4, 24);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(1658, 332);
            tabPage2.TabIndex = 2;
            tabPage2.Text = "Generational Chart";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // GeneticAlgorithmForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1690, 654);
            Controls.Add(btnSimulation);
            Controls.Add(dgbSimulationTopResults);
            Controls.Add(dgvShares);
            Controls.Add(chbIsElite);
            Controls.Add(label8);
            Controls.Add(txtNoGenerations);
            Controls.Add(txtGoal);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(txtMutationProbability);
            Controls.Add(txtCrossoverProbability);
            Controls.Add(cbxPrecision);
            Controls.Add(btnCalculate);
            Controls.Add(txtPopulationSize);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(txtUpperBound);
            Controls.Add(label2);
            Controls.Add(txtLowerBound);
            Controls.Add(label1);
            Controls.Add(mainTab);
            Name = "GeneticAlgorithmForm";
            Text = "GeneticAlgorithmForm";
            ((System.ComponentModel.ISupportInitialize)dgvStatistics).EndInit();
            ((System.ComponentModel.ISupportInitialize)chart).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvShares).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgbSimulationTopResults).EndInit();
            mainTab.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage2.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvStatistics;
        private Label label1;
        private TextBox txtLowerBound;
        private TextBox txtUpperBound;
        private Label label2;
        private Label label3;
        private TextBox txtPopulationSize;
        private Label label4;
        private Button btnCalculate;
        private ComboBox cbxPrecision;
        private TextBox txtCrossoverProbability;
        private TextBox txtMutationProbability;
        private Label label5;
        private Label label6;
        private Label label7;
        private ComboBox txtGoal;
        private DataGridViewTextBoxColumn lp;
        private DataGridViewTextBoxColumn x_real;
        private DataGridViewTextBoxColumn f_x;
        private DataGridViewTextBoxColumn g_x;
        private DataGridViewTextBoxColumn p_i;
        private DataGridViewTextBoxColumn q_i;
        private DataGridViewTextBoxColumn r;
        private DataGridViewTextBoxColumn x_real_sel;
        private DataGridViewTextBoxColumn x_bin;
        private DataGridViewTextBoxColumn parents;
        private DataGridViewTextBoxColumn p_c;
        private DataGridViewTextBoxColumn crossing;
        private DataGridViewTextBoxColumn mutation_points;
        private DataGridViewTextBoxColumn after_mutation;
        private DataGridViewTextBoxColumn after_mutation_xreal;
        private DataGridViewTextBoxColumn new_f_x;
        private Label label8;
        private TextBox txtNoGenerations;
        private CheckBox chbIsElite;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart;
        private DataGridView dgvShares;
        private DataGridView dgbSimulationTopResults;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn Column2;
        private DataGridViewTextBoxColumn Column3;
        private DataGridViewTextBoxColumn Column4;
        private DataGridViewTextBoxColumn Column5;
        private Button button2;
        private DataGridViewTextBoxColumn Column6;
        private DataGridViewTextBoxColumn Column7;
        private DataGridViewTextBoxColumn Column8;
        private DataGridViewTextBoxColumn Column9;
        private DataGridViewTextBoxColumn Column10;
        private DataGridViewTextBoxColumn Column11;
        private TabControl mainTab;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private Button btnSimulation;
    }
}