using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Constructor3D
{
    class Pyramid: Primitive
    {
        public double height;
        public double topWidth;
        public double bottomWidth;

        public double rotationX;
        public double rotationY;
        public double rotationZ;
        public double[,] rotation;

        public Triangle t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12;

        public Pyramid(Vector3 position, Vector3 color, double height, double topWidth, double bottomWidth, double specular, double reflective)
        {
            this.position = position;
            this.color = color;
            this.height = height;
            this.topWidth = topWidth;
            this.bottomWidth = bottomWidth;
            this.specular = specular;
            this.reflective = reflective;

            Vector3 pTop = position + new Vector3(0, 0, 1) * height;
            Vector3 pBottom = position;

            // Измененные координаты вершин для усеченной формы
            Vector3 aTop = position + new Vector3(-topWidth / 2, height, -topWidth / 2);
            Vector3 bTop = position + new Vector3(-topWidth / 2, height, topWidth / 2);
            Vector3 cTop = position + new Vector3(topWidth / 2, height, topWidth / 2);
            Vector3 dTop = position + new Vector3(topWidth / 2, height, -topWidth / 2);

            Vector3 aBottom = position + new Vector3(-bottomWidth / 2, 0, -bottomWidth / 2);
            Vector3 bBottom = position + new Vector3(-bottomWidth / 2, 0, bottomWidth / 2);
            Vector3 cBottom = position + new Vector3(bottomWidth / 2, 0, bottomWidth / 2);
            Vector3 dBottom = position + new Vector3(bottomWidth / 2, 0, -bottomWidth / 2);

            // Измените координаты вершин для усеченной формы
            double truncation = 0.5; // Параметр усечения
            Vector3[] truncatedVerticesTop = TruncateVertices(new Vector3[] { aTop, bTop, cTop, dTop }, truncation);
            Vector3[] truncatedVerticesBottom = TruncateVertices(new Vector3[] { aBottom, bBottom, cBottom, dBottom }, truncation);

            this.t1 = new Triangle(aTop, color, aBottom, bBottom, specular, reflective);
            this.t2 = new Triangle(aTop, color, bBottom, bTop, specular, reflective);
            this.t3 = new Triangle(aTop, color, bTop, cTop, specular, reflective);
            this.t4 = new Triangle(aTop, color, cTop, dTop, specular, reflective);

            this.t5 = new Triangle(bTop, color, bBottom, cBottom, specular, reflective);
            this.t6 = new Triangle(bTop, color, cBottom, cTop, specular, reflective);

            this.t7 = new Triangle(cTop, color, cBottom, dBottom, specular, reflective);
            this.t8 = new Triangle(cTop, color, dBottom, dTop, specular, reflective);

            this.t9 = new Triangle(dTop, color, dBottom, aBottom, specular, reflective);
            this.t10 = new Triangle(dTop, color, aBottom, aTop, specular, reflective);

            this.t11 = new Triangle(aBottom, color, bBottom, cBottom, specular, reflective);
            this.t12 = new Triangle(aBottom, color, cBottom, dBottom, specular, reflective);
        }

        private Vector3[] TruncateVertices(Vector3[] vertices, double truncation)
        {
            Vector3[] truncatedVertices = new Vector3[vertices.Length];

            for (int i = 0; i < vertices.Length; i++)
            {
                // Усекаем только верхние вершины (aTop, bTop, cTop, dTop)
                if (i >= 0 && i <= 3)
                {
                    // Перемещаем вершину ближе к центру пирамиды
                    truncatedVertices[i] = new Vector3(
                        vertices[i].x * (1 - truncation),
                        vertices[i].y * (1 - truncation),
                        vertices[i].z
                    );
                }
                else
                {
                    // Нижние вершины оставляем без изменений
                    truncatedVertices[i] = vertices[i];
                }
            }

            return truncatedVertices;
        }

        public void ApplyRotation()
        {
            double[,] rotationMatrix = CalculateRotationMatrix();
            for (int i = 0;i < rotationMatrix.GetLength(0);i++)
                for (int j = 0;j < rotationMatrix.GetLength(1);j++)
                    Console.Write(rotationMatrix[i,j]);
            // Применяем поворот ко всем вершинам
            t1.ApplyRotation(rotationMatrix);
            t2.ApplyRotation(rotationMatrix);
            t3.ApplyRotation(rotationMatrix);
            t4.ApplyRotation(rotationMatrix);
            t5.ApplyRotation(rotationMatrix);
            t6.ApplyRotation(rotationMatrix);
            t7.ApplyRotation(rotationMatrix);
            t8.ApplyRotation(rotationMatrix);
            t9.ApplyRotation(rotationMatrix);
            t10.ApplyRotation(rotationMatrix);
            t11.ApplyRotation(rotationMatrix);
            t12.ApplyRotation(rotationMatrix);
        }

        private double[,] CalculateRotationMatrix()
        {
            // Создаем матрицы поворота
            double[,] x = new double[4, 4] { { 1, 0, 0, 0 },
                                         { 0, Math.Cos(rotationX * Math.PI / 180), -Math.Sin(rotationX * Math.PI / 180), 0 },
                                         { 0, Math.Sin(rotationX * Math.PI / 180), Math.Cos(rotationX * Math.PI / 180), 0 },
                                         { 0, 0, 0, 1 } };

            double[,] y = new double[4, 4] { { Math.Cos(rotationY * Math.PI / 180), 0, Math.Sin(rotationY * Math.PI / 180), 0 },
                                         { 0, 1, 0, 0 },
                                         { -Math.Sin(rotationY * Math.PI / 180), 0, Math.Cos(rotationY * Math.PI / 180), 0 },
                                         { 0, 0, 0, 1 } };

            double[,] z = new double[4, 4] { { Math.Cos(rotationZ * Math.PI / 180), -Math.Sin(rotationZ * Math.PI / 180), 0, 0 },
                                         { Math.Sin(rotationZ * Math.PI / 180), Math.Cos(rotationZ * Math.PI / 180), 0, 0 },
                                         { 0, 0, 1, 0 },
                                         { 0, 0, 0, 1 } };

            // Перемножаем матрицы
            double[,] rotationMatrix = MultMatrix(x, y);
            rotationMatrix = MultMatrix(rotationMatrix, z);

            return rotationMatrix;
        }

        private double[,] MultMatrix(double[,] A, double[,] B)
        {
            double[,] result = new double[4, 4];

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    result[i, j] = 0;
                    for (int k = 0; k < 4; k++)
                    {
                        result[i, j] += A[i, k] * B[k, j];
                    }
                }
            }

            return result;
        }
    }
    class Triangle : Primitive
    {
        public Vector3 A;
        public Vector3 B;

        public Triangle(Vector3 position, Vector3 color, Vector3 A, Vector3 B, double specular, double reflective)
        {
            this.position = position;
            this.color = color;
            this.A = A;
            this.B = B;
            this.specular = specular;
            this.reflective = reflective;
        }
        public void ApplyRotation(double[,] rotationMatrix)
        {
            // Применяем поворот к вершинам треугольника
            Vector3 offsetA = A - position;
            Vector3 offsetB = B - position;

            A.x = offsetA.x * rotationMatrix[0, 0] + offsetA.y * rotationMatrix[1, 0] + offsetA.z * rotationMatrix[2, 0] + position.x;
            A.y = offsetA.x * rotationMatrix[0, 1] + offsetA.y * rotationMatrix[1, 1] + offsetA.z * rotationMatrix[2, 1] + position.y;
            A.z = offsetA.x * rotationMatrix[0, 2] + offsetA.y * rotationMatrix[1, 2] + offsetA.z * rotationMatrix[2, 2] + position.z;

            B.x = offsetB.x * rotationMatrix[0, 0] + offsetB.y * rotationMatrix[1, 0] + offsetB.z * rotationMatrix[2, 0] + position.x;
            B.y = offsetB.x * rotationMatrix[0, 1] + offsetB.y * rotationMatrix[1, 1] + offsetB.z * rotationMatrix[2, 1] + position.y;
            B.z = offsetB.x * rotationMatrix[0, 2] + offsetB.y * rotationMatrix[1, 2] + offsetB.z * rotationMatrix[2, 2] + position.z;


        }
    }
}
