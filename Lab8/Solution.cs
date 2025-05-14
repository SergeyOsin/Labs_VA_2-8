using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LB8_VA
{
    internal class Solution
    {
        private const double DOWN = 2.0;
        private const double UP = 3.7;
        private const double H = 0.17;
        private const double EXP = 2.71;
        private double[] arrayX;
        private double[] arrayY;
        private int Size;
        public Solution()=>Size = Convert.ToInt32((UP - DOWN) / H);
        private double F(double x, double y) => 3 * y + Math.Pow(EXP, -Math.Pow(x,2));
        public (double[],double[]) Solve_Eylor()
        {
            arrayX = new double[Size+1];
            arrayY = new double[Size+1];
            arrayY[0] = -1;
            arrayX[0] = DOWN;
            for(int i = 1; i < Size+1; i++)
            {
                arrayX[i] = arrayX[i - 1] + H;
                arrayY[i] = arrayY[i - 1] + H * F(arrayX[i - 1], arrayY[i - 1]);
            }
            return (arrayX, arrayY);
        }

        public (double[], double[]) Solve_RungeKutta()
        {
            arrayX = new double[Size+1];
            arrayY = new double[Size+1];
            arrayY[0] = -1;
            arrayX[0] = DOWN;
            for (int i = 1; i < Size+1; i++)
            {
                arrayX[i] = arrayX[i - 1] + H;
                double[] K = new double[4];
                K[0] = H * F(arrayX[i - 1], arrayY[i - 1]);
                K[1] = H * F(arrayX[i - 1] + H / 2, arrayY[i - 1] + K[0] / 2);
                K[2] = H * F(arrayX[i - 1] + H / 2, arrayY[i - 1] + K[1] / 2);
                K[3] = H * F(arrayX[i - 1] + H, arrayY[i - 1] + K[2]);
                arrayY[i] = arrayY[i - 1] +  (K[0] + 2*K[1] + 2*K[2] + K[3])/6;
            }
            return (arrayX, arrayY);
        }
    }
}
