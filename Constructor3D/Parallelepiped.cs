using System;
using System.Collections.Generic;
using System.Text;

namespace Constructor3D
{
    class Parallelepiped: Primitive
    {
        public double height;
        public double width;
        public double length;

        public Vector3 start;
        public Vector3 end;

        // Углы поворота в радианах
        public double rotationX;
        public double rotationY;
        public double rotationZ;
        public double[,] rotation;

        public Parallelepiped(Vector3 position, Vector3 color, double height, double width, double length, double specular, double reflective)
        {
            this.position = position;
            this.color = color;
            this.height = height;
            this.width = width;
            this.length = length;
            this.specular = specular;
            this.reflective = reflective;

            this.start = position - new Vector3(height / 2, width / 2, length / 2);
            this.end = position + new Vector3(height / 2, width / 2, length / 2);
            //Console.WriteLine(start.x + " " + start.y + " " + start.z);
            //Console.WriteLine(end.x + " " + end.y + " " + end.z);
        }
        public void ApplyRotation()
        {
            // Создаем матрицу поворота
            double[,] rotationMatrix = CalculateRotationMatrix();

            // Применяем поворот к начальной и конечной точкам
            start = MultiplyVectorByMatrix(start - position, rotationMatrix) + position;
            end = MultiplyVectorByMatrix(end - position, rotationMatrix) + position;
            //Console.WriteLine(start.x + " " + start.y + " " + start.z);
            //Console.WriteLine(end.x + " " + end.y + " " + end.z);
        }

        private double[,] CalculateRotationMatrix()
        {
            double[,] x, y, z;
            x = new double[4, 4] { { 1, 0, 0, 0 },
                           { 0, Math.Cos(rotationX * Math.PI / 180), -Math.Sin(rotationX * Math.PI / 180), 0 },
                           { 0, Math.Sin(rotationX * Math.PI / 180), Math.Cos(rotationX * Math.PI / 180), 0 },
                           { 0, 0, 0, 1 } };
            y = new double[4, 4] { { Math.Cos(rotationY * Math.PI / 180), 0, Math.Sin(rotationY * Math.PI / 180), 0 },
                           { 0, 1, 0, 0 },
                           { -Math.Sin(rotationY * Math.PI / 180), 0, Math.Cos(rotationY * Math.PI / 180), 0 },
                           { 0, 0, 0, 1 } };
            z = new double[4, 4] { { Math.Cos(rotationZ * Math.PI / 180), -Math.Sin(rotationZ * Math.PI / 180), 0, 0 },
                           { Math.Sin(rotationZ * Math.PI / 180), Math.Cos(rotationZ * Math.PI / 180), 0, 0 },
                           { 0, 0, 1, 0 },
                           { 0, 0, 0, 1 } };

            // Перемножаем матрицы
            double[,] rotationMatrix = multMatrix(x, y);
            rotationMatrix = multMatrix(rotationMatrix, z);

            return rotationMatrix;
        }

        private double[,] multMatrix(double[,] A, double[,] B)
        {
            double[,] result = new double[4, 4] { { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 } };
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    for (int k = 0; k < 4; k++)
                        result[i, j] += A[i, k] * B[k, j];
                }

            }
            return result;
        }

        private Vector3 MultiplyVectorByMatrix(Vector3 vector, double[,] matrix)
        {
            double[] result = new double[3];

            for (int i = 0; i < 3; i++)
            {
                result[i] = 0;
                for (int j = 0; j < 3; j++)
                {
                    switch (j)
                    {
                        case 0:
                            result[i] += vector.x * matrix[i, j];
                            break;
                        case 1:
                            result[i] += vector.y * matrix[i, j];
                            break;
                        case 2:
                            result[i] += vector.z * matrix[i, j];
                            break;
                            // Дополните, если у вас больше компонентов в векторе
                    }
                }
            }

            return new Vector3(result[0], result[1], result[2]);
        }
    }
}
