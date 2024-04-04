using LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Globalization;

namespace Lab4Vector
{
    internal class Loader
    {
        public MathVector[] LoadFile()
        {
            string[] lines = File.ReadAllLines("iris.csv");
            MathVector[] mathVectors = new MathVector[3];
            int k = 0;
            for(int i = 1; i < lines.Length; )
            {
                string[] a = lines[i].Split(',');
                double[] mas = new double[4];
                string typeiris = a[4];
                int count = 0;
                while(i < lines.Length)
                {
                    for (int j = 0; j < mas.Length; j++)
                    {
                        mas[j] += double.Parse(a[j], CultureInfo.InvariantCulture);
                    }
                    i++;
                    count++;
                    if (i < lines.Length)
                    {
                        a = lines[i].Split(',');
                        if (typeiris != a[4])
                            break;
                    }
                }
                for (int j = 0; j < mas.Length; j++)
                {
                    mas[j] /= count;
                }
                MathVector mathvector = new MathVector(mas);
                mathVectors[k] = mathvector;
                k++;
            }

            return mathVectors;
        }
    }
}
