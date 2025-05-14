using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LB8_VA
{
    public partial class LB8 : Form
    {
        private Solution sol;
        public LB8() => InitializeComponent();

        private void button3_Click(object sender, EventArgs e)=> Application.Exit();

        private void Form1_Load(object sender, EventArgs e)
        {
            tabPage1.Text = "Метод Эйлера";
            tabPage2.Text = "Метод Рунге-Кутта";
            button1.BackColor = Color.Indigo;
            button1.ForeColor = Color.Teal;
            button2.BackColor = Color.Aqua;
            button2.ForeColor = Color.LightCoral;
            Array.ForEach(new[] { dataGridView1, dataGridView2 }, dt => dt.ReadOnly = true);
            Array.ForEach(new[] { dataGridView1, dataGridView2 }, dt => dt.AllowUserToAddRows = false);
            Array.ForEach(new[] { dataGridView1, dataGridView2 }, dt => dt.RowHeadersVisible = false);
            Array.ForEach(new[] { dataGridView2, dataGridView1 }, dt => dt.ColumnHeadersVisible = false);
            dataGridView1.ColumnCount = 12;
            dataGridView1.RowCount = 3;
            dataGridView2.ColumnCount = 12;
            dataGridView2.RowCount = 3;
            dataGridView1.Rows[1].Cells[0].Value = "x(i)";
            dataGridView1.Rows[2].Cells[0].Value = "y(i)";
            dataGridView2.Rows[1].Cells[0].Value = "x(i)";
            dataGridView2[0,2].Value = "y(i)";
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
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
                gr[k+1,0].Value = $"y(x{k})";
                gr[k + 1, 1].Value = RoundNum(arrayX[k]);
                gr[k + 1, 2].Value = RoundNum(arrayY[k]);
            }
        }
        private void button1_Click(object sender, EventArgs e) => Solve(dataGridView1,1);

        private void button2_Click(object sender, EventArgs e) => Solve(dataGridView2,2);

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Labs_VA_2_8.Form1().Show();
        }
    }
}
