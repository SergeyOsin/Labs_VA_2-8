using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace LB8_VA
{
    public partial class LB8 : Form
    {
        private Solution sol;
        public LB8() => InitializeComponent();

        private void button3_Click(object sender, EventArgs e)=> Application.Exit();

        private void Form1_Load(object sender, EventArgs e)
        { 
            button1.BackColor = Color.Indigo;
            button1.ForeColor = Color.Teal;
            Array.ForEach(new[] { dataGridView1 }, dt => dt.ReadOnly = true);
            Array.ForEach(new[] { dataGridView1 }, dt => dt.AllowUserToAddRows = false);
            Array.ForEach(new[] { dataGridView1  }, dt => dt.RowHeadersVisible = false);
            Array.ForEach(new[] {dataGridView1 }, dt => dt.ColumnHeadersVisible = false);
            dataGridView1.ColumnCount = 12;
            dataGridView1.RowCount = 3;
            Array.ForEach(new[] { dataGridView1 }, dt => dt[0, 0].Value = "i");
            dataGridView1.Rows[1].Cells[0].Value = "x(i)";
            dataGridView1.Rows[2].Cells[0].Value = "y(i)";
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            chart1.Titles.Add("График функции");
        }
        private double RoundNum(double num) => Math.Round(num, 4);
        private void Solve(DataGridView gr,int num)
        {
            sol = new Solution();
            double[] arrayX = new double[11];
            double[] arrayY = new double[11];
            (arrayX, arrayY) = (num == 1) ? sol.Solve_Eylor() : sol.Solve_RungeKutta();
            for(int k = 0; k < 11; k++)
            {
                gr[k + 1, 0].Value = k;
                gr[k + 1, 1].Value = RoundNum(arrayX[k]);
                gr[k + 1, 2].Value = RoundNum(arrayY[k]);
            }
            
        }

        private void BuildGraph()
        {
            chart1.ChartAreas[0].AxisX.Title = "X"; // Подпись оси X
            chart1.ChartAreas[0].AxisY.Title = "Y"; // Подпись оси Y
            chart1.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.LightGray;
            chart1.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.LightGray;
            // Создание и настройка серии
            Series series = new Series("График функции");
            series.ChartType = SeriesChartType.Line; // Тип графика - линейный
            series.Color = Color.Blue; // Цвет линии
            series.BorderWidth = 2; // Толщина линии
        
            for (int i = 0; i < 11; i++)
            {
                if (dataGridView1.Rows.Count > i &&
                    dataGridView1[i, 1].Value != null &&
                    dataGridView1[i, 2].Value != null)
                {
                    double x, y;
                    if (double.TryParse(dataGridView1[i, 1].Value.ToString(), out x) &&
                        double.TryParse(dataGridView1[i, 2].Value.ToString(), out y))
                    {
                        series.Points.AddXY(x, y);
                    }
                }
            }
            // Очистка предыдущих серий и добавление новой
            chart1.Series.Clear();
            chart1.Series.Add(series);
        }
        private void button1_Click(object sender, EventArgs e) => Solve(dataGridView1,1);

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Labs_VA_2_8.Form1().Show();
        }

        private void button2_Click(object sender, EventArgs e) => BuildGraph();
    }
}
