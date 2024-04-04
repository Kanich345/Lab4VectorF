using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinearAlgebra
{   /// <inherotdoc cref="IMathVector"/>   
    public class MathVector : IMathVector
    {
        private double[] _mas;

        public MathVector(double[] mas)
        {
            this._mas = new double[mas.Length];
            Array.Copy(mas, this._mas, mas.Length);
        }

        public double this[int i]
        {
            get => _mas[i];
            set => _mas[i] = value;
        }
        /// <summary>
        /// Возвращает размерность вектора (Количество координат)
        /// </summary>
        public int Dimensions => _mas.Length;

        /// <summary>
        /// Возвращает математическую длину вектора
        /// </summary>
        public double Length
        {
            get
            {
                double sum = 0;
                for (int i = 0; i < _mas.Length; i++)
                {
                    sum += _mas[i] * _mas[i];
                }
                return Math.Sqrt(sum);
            }
        }
        /// <summary>
        /// Вычисляет Евклидово расстояние до заданного вектора
        /// </summary>
        /// <param name="vector">Вектор, расстояние до которого необходимо вычислить</param>
        /// <returns>Евклидово расстояние между векторами</returns>
        public double CalcDistance(IMathVector vector)
        {
            if (Dimensions != vector.Dimensions)
                throw new ArgumentOutOfRangeException("Разные размеры векторов");

            double res = 0;
            for (int i = 0; i < Dimensions; i++)
            {
                res += Math.Pow(_mas[i] - vector[i], 2);
            }
            return Math.Sqrt(res);
        }
        /// <summary>
        /// Метод для получения перечислителя по вектору
        /// </summary>
        /// <returns>IEnumerator для данного вектора</returns>
        public IEnumerator GetEnumerator()
        {
            return _mas.GetEnumerator();
        }
        /// <summary>
        /// Поэлементно умнажает вектор на данный
        /// </summary>
        /// <param name="vector">Вектор, на который необходимо умножить</param>
        /// <returns>Новый вектор, являющийся поэлементным произведением векторов</returns>
        public IMathVector Multiply(IMathVector vector)
        {
            if (Dimensions != vector.Dimensions)
                throw new ArgumentOutOfRangeException("Разные размеры векторов");

            IMathVector result = new MathVector(_mas);
            for (int i = 0; i < vector.Dimensions; i++)
            {
                result[i] *= vector[i];
            }
            return result;
        }
        /// <summary>
        /// Поэлементно умножает вектор на данное число
        /// </summary>
        /// <param name="number">Число, на которое необходимо умножить</param>
        /// <returns>Новый вектор, в котором каждый элемент умножен на данное число</returns>
        public IMathVector MultiplyNumber(double number)
        {
            IMathVector vector = new MathVector(_mas);
            for (int i = 0; i < _mas.Length; i++)
            {
                vector[i] *= number;
            }
            return vector;
        }
        /// <summary>
        /// Вычисляет скалярное произведение векторов
        /// </summary>
        /// <param name="vector">Вектор, скалярное произведение с которым необходимо вычислить</param>
        /// <returns>Скалярное произведение векторов</returns>
        public double ScalarMultiply(IMathVector vector)
        {
            if (Dimensions != vector.Dimensions)
                throw new ArgumentOutOfRangeException("Разные размеры векторов");
            double sum = 0;
            for (int i = 0; i < Dimensions; i++)
            {
                sum += _mas[i] * vector[i];
            }
            return sum;
        }
        /// <summary>
        /// Поэлементно прибавляет к вектору данный вектор
        /// </summary>
        /// <param name="vector">Вектор, который необходимо прибавить</param>
        /// <returns>Новый вектор, являющийся поэлементной суммой векторов</returns>
        public IMathVector Sum(IMathVector vector)
        {
            if (Dimensions != vector.Dimensions)
                throw new ArgumentOutOfRangeException("Разные размеры векторов");

            IMathVector result = new MathVector(_mas);
            for (int i = 0; i < vector.Dimensions; i++)
            {
                result[i] += vector[i];
            }
            return result;
        }
        /// <summary>
        /// Поэлементно прибавляет к вектору данное число
        /// </summary>
        /// <param name="number">Число, которое необходимо прибавить</param>
        /// <returns>Новый вектор, в котором к каждому элементу прибавлено данное число</returns>
        public IMathVector SumNumber(double number)
        {
            IMathVector vector = new MathVector(_mas);
            for (int i = 0; i < _mas.Length; i++)
            {
                vector[i] += number;
            }
            return vector;
        }

        public static IMathVector operator +(MathVector vector, double number)
        {
            return vector.SumNumber(number);
        }

        public static IMathVector operator +(double number, MathVector vector)
        {
            return vector.SumNumber(number);
        }

        public static IMathVector operator +(MathVector vector1, MathVector vector2)
        {
            return vector1.Sum(vector2);
        }

        public static IMathVector operator -(MathVector vector, double number)
        {
            return vector.SumNumber(-number);
        }

        public static IMathVector operator -(MathVector vector1, MathVector vector2)
        {
            IMathVector other = vector2.MultiplyNumber(-1);
            return vector1.Sum(other);
        }

        public static IMathVector operator *(MathVector vector1, double number)
        {
            return vector1.MultiplyNumber(number);
        }

        public static IMathVector operator *(double number, MathVector vector1)
        {
            return vector1.MultiplyNumber(number);
        }

        public static IMathVector operator *(MathVector vector1, MathVector vector2)
        {
            return vector1.Multiply(vector2);
        }

        public static IMathVector operator /(MathVector vector1, double number)
        {
            if (number == 0)
                throw new DivideByZeroException();
            return vector1.MultiplyNumber(1 / number);
        }

        public static IMathVector operator /(MathVector vector1, MathVector vector2)
        {
            MathVector result = new MathVector(vector2._mas);
            for (int i = 0; i < result.Dimensions; i++)
            {
                if (result[i] == 0)
                    throw new DivideByZeroException();
                result[i] = 1 / result[i];
            }

            return vector1.Multiply(result);
        }

        public static double operator %(MathVector vector1, MathVector vector2)
        {
            return vector1.ScalarMultiply(vector2);
        }
    }
}

