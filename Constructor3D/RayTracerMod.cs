﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Constructor3D
{
    class RayTracerMod
    {
        public Bitmap bmp;
        private Scene scene;

        private int viewport_width = 1;
        private int viewport_height = 1;
        private int projection_plane_d = 1;

        private Color[,] buffer;

        public RayTracerMod(Scene scene)
        {
            this.scene = scene;
            this.bmp = new Bitmap(scene.size.Width, scene.size.Height);
            this.buffer = new Color[scene.size.Width, scene.size.Height];
        }

        private void IntersectRaySphere(Vector3 O, Vector3 D, Sphere sphere, ref double t1, ref double t2)
        {
            Vector3 CO = O - sphere.position;

            double a = Vector3.ScalarMultiplication(D, D);
            double b = 2 * Vector3.ScalarMultiplication(CO, D);
            double c = Vector3.ScalarMultiplication(CO, CO) - sphere.radius * sphere.radius;


            double discriminant = b * b - 4 * a * c;

            if (discriminant < 0)
            {
                t1 = Double.PositiveInfinity;
                t2 = Double.PositiveInfinity;
                return;
            }

            t1 = (-b + Math.Sqrt(discriminant)) / (2 * a); // Вычисляет первый корень квадратного уравнения,
            t2 = (-b - Math.Sqrt(discriminant)) / (2 * a); // Вычисляет второй корень квадратного уравнения,
            //функция IntersectRaySphere вычисляет параметры t1 и t2, которые представляют собой
            //расстояния от начальной точки луча до точек пересечения луча с сферой.
        }

        private void IntersectRayParallelepiped(Vector3 O, Vector3 D, Parallelepiped parallelepiped, ref double t1ret, ref double t2ret)
        {
            double t1, t2; // Объявляеm переменные t1 и t2 для хранения временных значений параметров пересечения.
            double tnear = Double.NegativeInfinity;
            double tfar = Double.PositiveInfinity;

            if (Math.Abs(D.x) < 0.001)
            {
                if (O.x < parallelepiped.start.x || O.x > parallelepiped.end.x)
                {
                    t1ret = Double.PositiveInfinity;
                    t2ret = Double.NegativeInfinity;
                    return;
                }

            }
            else
            {

                t1 = (parallelepiped.start.x - O.x) / D.x;
                t2 = (parallelepiped.end.x - O.x) / D.x;
                if (t1 > t2)
                {
                    double tmp = t1;
                    t1 = t2;
                    t2 = tmp;
                }
                if (t1 > tnear) tnear = t1;
                if (t2 < tfar) tfar = t2;
                if (tnear > tfar)
                {
                    t1ret = Double.PositiveInfinity;
                    t2ret = Double.NegativeInfinity;
                    return;
                }
                if (tfar < 0)
                {
                    t1ret = Double.PositiveInfinity;
                    t2ret = Double.NegativeInfinity;
                    return;
                }
            }

            if (Math.Abs(D.y) < 0.001)
            {
                if (O.y < parallelepiped.start.y || O.y > parallelepiped.end.y)
                {
                    t1ret = Double.PositiveInfinity;
                    t2ret = Double.NegativeInfinity;
                    return;
                }
            }
            else
            {

                t1 = (parallelepiped.start.y - O.y) / D.y;
                t2 = (parallelepiped.end.y - O.y) / D.y;
                if (t1 > t2)
                {
                    double tmp = t1;
                    t1 = t2;
                    t2 = tmp;
                }
                if (t1 > tnear) tnear = t1;
                if (t2 < tfar) tfar = t2;
                if (tnear > tfar)
                {
                    t1ret = Double.PositiveInfinity;
                    t2ret = Double.NegativeInfinity;
                    return;
                }
                if (tfar < 0)
                {
                    t1ret = Double.PositiveInfinity;
                    t2ret = Double.NegativeInfinity;
                    return;
                }
            }
            if (Math.Abs(D.z) < 0.001)
            {
                if (O.z < parallelepiped.start.z || O.z > parallelepiped.end.z)
                {
                    t1ret = Double.PositiveInfinity;
                    t2ret = Double.NegativeInfinity;
                    return;
                }
            }
            else
            {

                t1 = (parallelepiped.start.z - O.z) / D.z;
                t2 = (parallelepiped.end.z - O.z) / D.z;
                if (t1 > t2)
                {
                    double tmp = t1;
                    t1 = t2;
                    t2 = tmp;
                }
                if (t1 > tnear) tnear = t1;
                if (t2 < tfar) tfar = t2;
                if (tnear > tfar)
                {
                    t1ret = Double.PositiveInfinity;
                    t2ret = Double.NegativeInfinity;
                    return;
                }
                if (tfar < 0)
                {
                    t1ret = Double.PositiveInfinity;
                    t2ret = Double.NegativeInfinity;
                    return;
                }
            }
            t1ret = tnear;
            t2ret = tfar;
        }

        private void IntersectRayTriangle(Vector3 O, Vector3 D, Vector3 v0, Vector3 v1, Vector3 v2, ref double t1)
        {
            Vector3 e1 = v1 - v0;
            Vector3 e2 = v2 - v0;

            Vector3 pvec = new Vector3(D.y * e2.z - D.z * e2.y, D.z * e2.x - D.x * e2.z, D.x * e2.y - D.y * e2.x);
            double det = Vector3.ScalarMultiplication(e1, pvec);

            // Луч параллелен плоскости
            if (det < 1e-8 && det > -1e-8)
            {
                t1 = Double.PositiveInfinity;
                return;
            }

            double inv_det = 1 / det;
            Vector3 tvec = O - v0;
            double u = Vector3.ScalarMultiplication(tvec, pvec) * inv_det;
            if (u < 0 || u > 1)
            {
                t1 = Double.PositiveInfinity;
                return;
            }
            Vector3 qvec = new Vector3(tvec.y * e1.z - tvec.z * e1.y, tvec.z * e1.x - tvec.x * e1.z, tvec.x * e1.y - tvec.y * e1.x);

            double v = Vector3.ScalarMultiplication(D, qvec) * inv_det;

            if (v < 0 || u + v > 1)
            {
                t1 = Double.PositiveInfinity;
                return;
            }

            t1 = Vector3.ScalarMultiplication(e2, qvec) * inv_det;
        }
        public void IntersectRayPyramid(ref Primitive closest_object, ref double closest_t, Vector3 camera, Vector3 dir, double t_min, double t_max, Pyramid pyramid, ref double t1, ref double t2)
        {
            double[] tValues = new double[2];
            Triangle[] triangles = new Triangle[12] { pyramid.t1, pyramid.t2, pyramid.t3, pyramid.t4, pyramid.t5, pyramid.t6, pyramid.t7, pyramid.t8, pyramid.t9, pyramid.t10, pyramid.t11, pyramid.t12 };

            for (int i = 0; i < triangles.Length; i++)
            {
                IntersectRayTriangle(camera, dir, triangles[i].position, triangles[i].A, triangles[i].B, ref tValues[0]);
                tValues[1] = tValues[0];

                for (int j = 0; j < 2; j++)
                {
                    if (tValues[j] < closest_t && t_min < tValues[j] && tValues[j] < t_max)
                    {
                        closest_t = tValues[j];
                        closest_object = triangles[i];
                    }
                }
            }
            t1 = tValues[0];
            t2 = tValues[1];
        }

        private void IntersectRayCube(Vector3 O, Vector3 D, Cube cube, ref double t1ret, ref double t2ret)
        {
            double t1, t2;
            double tnear = Double.NegativeInfinity;
            double tfar = Double.PositiveInfinity;

            if (Math.Abs(D.x) < 0.001)
            {
                if (O.x < cube.start.x || O.x > cube.end.x)
                {
                    t1ret = Double.PositiveInfinity;
                    t2ret = Double.NegativeInfinity;
                    return;
                }

            }
            else
            {

                t1 = (cube.start.x - O.x) / D.x;
                t2 = (cube.end.x - O.x) / D.x;
                if (t1 > t2)
                {
                    double tmp = t1;
                    t1 = t2;
                    t2 = tmp;
                }
                if (t1 > tnear) tnear = t1;
                if (t2 < tfar) tfar = t2;
                if (tnear > tfar)
                {
                    t1ret = Double.PositiveInfinity;
                    t2ret = Double.NegativeInfinity;
                    return;
                }
                if (tfar < 0)
                {
                    t1ret = Double.PositiveInfinity;
                    t2ret = Double.NegativeInfinity;
                    return;
                }
            }

            if (Math.Abs(D.y) < 0.001)
            {
                if (O.y < cube.start.y || O.y > cube.end.y)
                {
                    t1ret = Double.PositiveInfinity;
                    t2ret = Double.NegativeInfinity;
                    return;
                }
            }
            else
            {

                t1 = (cube.start.y - O.y) / D.y;
                t2 = (cube.end.y - O.y) / D.y;
                if (t1 > t2)
                {
                    double tmp = t1;
                    t1 = t2;
                    t2 = tmp;
                }
                if (t1 > tnear) tnear = t1;
                if (t2 < tfar) tfar = t2;
                if (tnear > tfar)
                {
                    t1ret = Double.PositiveInfinity;
                    t2ret = Double.NegativeInfinity;
                    return;
                }
                if (tfar < 0)
                {
                    t1ret = Double.PositiveInfinity;
                    t2ret = Double.NegativeInfinity;
                    return;
                }
            }
            if (Math.Abs(D.z) < 0.001)
            {
                if (O.z < cube.start.z || O.z > cube.end.z)
                {
                    t1ret = Double.PositiveInfinity;
                    t2ret = Double.NegativeInfinity;
                    return;
                }
            }
            else
            {

                t1 = (cube.start.z - O.z) / D.z;
                t2 = (cube.end.z - O.z) / D.z;
                if (t1 > t2)
                {
                    double tmp = t1;
                    t1 = t2;
                    t2 = tmp;
                }
                if (t1 > tnear) tnear = t1;
                if (t2 < tfar) tfar = t2;
                if (tnear > tfar)
                {
                    t1ret = Double.PositiveInfinity;
                    t2ret = Double.NegativeInfinity;
                    return;
                }
                if (tfar < 0)
                {
                    t1ret = Double.PositiveInfinity;
                    t2ret = Double.NegativeInfinity;
                    return;
                }
            }
            t1ret = tnear;
            t2ret = tfar;
        }

        public void ClosestIntersection(ref Primitive closest_object, ref double closest_t, Vector3 camera, Vector3 dir, double t_min, double t_max)
        {
            List<Primitive> scene_object = scene.primitives;
            double t1 = 0;
            double t2 = 0;

            for (int i = 0; i < scene_object.Count; i++)
            {
                if (scene_object[i] is Sphere)
                {
                    IntersectRaySphere(camera, dir, (Sphere)scene_object[i], ref t1, ref t2);
                }
                else if (this.scene.primitives[i] is Parallelepiped)
                {
                    IntersectRayParallelepiped(camera, dir, (Parallelepiped)this.scene.primitives[i], ref t1, ref t2);

                }
                else if (this.scene.primitives[i] is Cube)
                {
                    IntersectRayCube(camera, dir, (Cube)this.scene.primitives[i], ref t1, ref t2);

                }
                else if (this.scene.primitives[i] is Pyramid)
                {
                    IntersectRayPyramid(ref closest_object, ref closest_t, camera, dir, t_min, t_max, (Pyramid)this.scene.primitives[i], ref t1, ref t2);
                }

                if (t1 < closest_t && t_min < t1 && t1 < t_max)
                {
                    closest_t = t1;
                    closest_object = scene_object[i];
                }
                if (t2 < closest_t && t_min < t2 && t2 < t_max)
                {
                    closest_t = t2;
                    closest_object = scene_object[i];
                }
            }
        }
        private Vector3 ReflectRay(Vector3 R, Vector3 N)
        {
            double n_dot_r = Vector3.ScalarMultiplication(N, R);
            return new Vector3(2 * n_dot_r * N.x - R.x, 2 * n_dot_r * N.y - R.y, 2 * n_dot_r * N.z - R.z);
        }

        private Vector3 TraceRay(Vector3 camera, Vector3 dir, double t_min, double t_max, int depth)
        {
            double closest_t = Double.PositiveInfinity;
            Primitive closest_object = null;

            ClosestIntersection(ref closest_object, ref closest_t, camera, dir, t_min, t_max);

            if (closest_object == null)
                return new Vector3();

            Vector3 P = camera + closest_t * dir;  // вычисление пересечения
            Vector3 N;
            if (closest_object is Parallelepiped)
                N = Vec3dNormalParallelepiped(P, (Parallelepiped)closest_object);
            else if (closest_object is Cube)
                N = Vec3dNormalCube(P, (Cube)closest_object);
            else if (closest_object is Triangle)
                N = Vec3dNormalTriangle((Triangle)closest_object);
            else
                N = P - closest_object.position; // вычисление нормали сферы в точке пересечения
            N = N / Vector3.Length(N);

            double intensity = ComputeLighting(P, N, -dir, closest_object.specular);

            Vector3 localColor = intensity * closest_object.color;

            double r = closest_object.reflective;

            if (depth <= 0 || r <= 0)
                return localColor;

            Vector3 R = ReflectRay(-dir, N);
            Vector3 reflectedColor = TraceRay(P, R, 0.001, Double.PositiveInfinity, depth - 1);

            Vector3 kLocalColor = (1 - r) * localColor;
            Vector3 rReflectedColor = r * reflectedColor;

            return kLocalColor + rReflectedColor;
        }

        private Vector3 Vec3dNormalParallelepiped(Vector3 P, Parallelepiped parallelepiped)
        {
            Vector3 size = parallelepiped.end - parallelepiped.start;
            Vector3 C = parallelepiped.end + parallelepiped.start;
            C = C * 0.5;


            Vector3 localPoint = P - C;
            Vector3 normal = new Vector3(1, 0, 0);

            normal.x = normal.x * Math.Sign(localPoint.x);
            double distance = Math.Abs(size.x - Math.Abs(localPoint.x));
            double min = distance;

            distance = Math.Abs(size.y - Math.Abs(localPoint.y));

            if (distance < min)
            {
                min = distance;

                normal = new Vector3(0, 1, 0);

                normal.y = normal.y * Math.Sign(localPoint.y);

            }
            distance = Math.Abs(size.z - Math.Abs(localPoint.z));
            if (distance < min)
            {
                min = distance;
                normal = new Vector3(0, 0, 1);

                normal.z = normal.z * Math.Sign(localPoint.z);
            }
            return normal;

        }

        private Vector3 Vec3dNormalCube(Vector3 P, Cube cube)
        {
            Vector3 size = cube.end - cube.start;
            Vector3 C = cube.end + cube.start;
            C = C * 0.5;


            Vector3 localPoint = P - C;
            Vector3 normal = new Vector3(1, 0, 0);

            normal.x = normal.x * Math.Sign(localPoint.x);
            double distance = Math.Abs(size.x - Math.Abs(localPoint.x));
            double min = distance;

            distance = Math.Abs(size.y - Math.Abs(localPoint.y));

            if (distance < min)
            {
                min = distance;

                normal = new Vector3(0, 1, 0);

                normal.y = normal.y * Math.Sign(localPoint.y);

            }
            distance = Math.Abs(size.z - Math.Abs(localPoint.z));
            if (distance < min)
            {
                min = distance;
                normal = new Vector3(0, 0, 1);

                normal.z = normal.z * Math.Sign(localPoint.z);
            }
            return normal;

        }
        private Vector3 Vec3dNormalTriangle(Triangle closest_object)
        {
            Vector3 e1 = closest_object.A - closest_object.position;
            Vector3 e2 = closest_object.B - closest_object.position;
            Vector3 normal = new Vector3(e1.y * e2.z - e1.z * e2.y, e1.z * e2.x - e1.x * e2.z, e1.x * e2.y - e1.y * e2.x);
            double len_n = Vector3.Length(normal);
            normal = normal / len_n;
            return normal;
        }

        private double ComputeLighting(Vector3 P, Vector3 N, Vector3 V, double specular)
        {
            double intensity = scene.ambient_light.intensity;
            List<LightSource> sceneLight = scene.light_sources;

            for (int i = 0; i < sceneLight.Count; i++)
            {
                Vector3 L;
                double t_max;
                L = sceneLight[i].position - P;
                t_max = Double.PositiveInfinity;

                double shadow_t = Double.PositiveInfinity;
                Primitive shadow_object = null;
                ClosestIntersection(ref shadow_object, ref shadow_t, P, L, 0.001, t_max);
                if (shadow_object != null)
                    continue;

                double n_dot_l = Vector3.ScalarMultiplication(N, L);

                if (n_dot_l > 0)
                {
                    intensity += sceneLight[i].intensity * n_dot_l / (Vector3.Length(N) * Vector3.Length(L));
                }

                if (specular != -1)
                {

                    Vector3 R = 2 * N * n_dot_l - L;
                    double r_dot_v = Vector3.ScalarMultiplication(R, V);

                    if (r_dot_v > 0)
                    {
                        intensity += sceneLight[i].intensity * Math.Pow(r_dot_v / (Vector3.Length(R) * Vector3.Length(V)), specular);
                    }
                }
            }
            return intensity;
        }
        private void PutPixel(int x, int y, Color color)
        {
            int x_ = scene.size.Width / 2 + x;
            int y_ = scene.size.Height / 2 - y - 1;

            if (x_ < 0 || x_ >= scene.size.Width || y_ < 0 || y_ >= scene.size.Height)
            {
                return;
            }

            this.buffer[x_, y_] = color;
        }

        private Color Clamp(Vector3 color)
        {
            int color_x = Math.Min(255, Math.Max(0, (int)color.x));
            int color_y = Math.Min(255, Math.Max(0, (int)color.y));
            int color_z = Math.Min(255, Math.Max(0, (int)color.z));
            return Color.FromArgb(color_x, color_y, color_z);
        }


        private void rendering(object obj)
        {
            Params p = (Params)obj;
            Camera camera = scene.camera;
            Vector3 D;
            Vector3 color;
            int recursion_depth = 3;
            for (int x = p.start_x; x < p.start_x + p.width; x++)
            {
                for (int y = p.start_y; y < p.start_y + p.height; y++)
                {
                    D = CanvasToViewport(x, y) * scene.camera.rotation;
                    color = TraceRay(camera.position, D, 1, Double.PositiveInfinity, recursion_depth);
                    PutPixel(x, y, Clamp(color));

                }
            }

        }
        public Bitmap render()
        {
            scene.camera.calc_rotation();
            Thread[] threads = new Thread[4];
            for (int i = 0; i < 4; i++)
            {
                Params p = new Params(scene.size.Width / 4, scene.size.Height, -scene.size.Width / 2 + scene.size.Height / 4 * i, -scene.size.Height / 2);
                threads[i] = new Thread(rendering);
                threads[i].Start(p);
            }
            foreach (Thread thread in threads)
            {
                thread.Join();
            }

            for (int i = 0; i < scene.size.Width; i++)
                for (int j = 0; j < scene.size.Height; j++)
                    this.bmp.SetPixel(i, j, buffer[i, j]);
            return this.bmp;
        }


        private Vector3 CanvasToViewport(int x, int y)
        {
            return new Vector3(x * (double)viewport_width / scene.size.Width, y * (double)viewport_height / scene.size.Height, projection_plane_d);
        }
    }
}
