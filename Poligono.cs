using Newtonsoft.Json;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using ProgramacionGrafica;
using System.Collections.Generic;
using System.Drawing;

namespace ProGrafica
{
    internal class Poligono
    {
        [JsonProperty("vertices")]
        private List<Point> vertices;
        [JsonProperty("color")]
        private Color color { get; set; }
        [JsonProperty("centroC")]
        private Point centroC;
        private Point centroAcarreado;
        public Matrix matriz { get; set; }


        public Poligono()
        {
            vertices = new List<Point>();
            this.color = new Color();
            this.centroC = new Point(0, 0, 0);
            this.centroAcarreado= new Point(0, 0, 0);
            this.matriz = new Matrix(centroC);
    }
        public Poligono(Color color) {
            vertices = new List<Point>();
            this.color = color;
            this.centroC = new Point(0, 0, 0);
            this.centroAcarreado = new Point(0, 0, 0);
            this.matriz = new Matrix(centroC);
        }

        public Poligono(Color color, Point centroC) //Constructor para un poligono en caso de que sea un circulo
        {
            vertices = new List<Point>();
            this.color = color;
            this.centroC = centroC;
            this.centroAcarreado = new Point(0, 0, 0);
            this.matriz = new Matrix(centroC);
            setCentroAcarreado(new Point(0, 0, 0));
        }
        public Point CentroC
        {
            get { return this.centroC; }
            set { this.centroC = value; }
        }
        public void setCentroAcarreado(Point centroAcarreado)
        {
            this.centroAcarreado = centroAcarreado + centroC;
            this.matriz.SetCentro(this.centroAcarreado.X, this.centroAcarreado.Y, this.centroAcarreado.Z);
        }
        public void addVertice(float x, float y, float z)
        {
            vertices.Add(new Point(x, y, z));
        }
        public void removeVertice(int index)
        {
            vertices.RemoveAt(index);
        }
        public void UpdateVertices()
        {
                for (int i = 0; i < vertices.Count; i++)
                {
                    vertices[i] = new Point(vertices[i].X, vertices[i].Y, vertices[i].Z);
                }
        }
        public void draw()
        {
            draw(new Point(0, 0, 0));
        }
        public void draw(Point centros)
        {
            Point centroResto = centros+centroC;
                GL.Begin(PrimitiveType.Polygon);
                GL.Color3(color);
                foreach (Point v in vertices)
                {
                    Point vertexToDraw = v;
                    vertexToDraw *= matriz.GetMatrix();
                    GL.Vertex3(vertexToDraw.X, vertexToDraw.Y, vertexToDraw.Z);
                }
                GL.End();
        }
        public void translate(string axis, float transaleValue)
        {
            switch (axis)
            {
                case "x":
                    this.matriz.SetTraslacion(transaleValue, 0, 0);
                    break;
                case "y":
                    this.matriz.SetTraslacion(0, transaleValue, 0);
                    break;
                case "z":
                    this.matriz.SetTraslacion(0, 0, transaleValue);
                    break;
            }
        }
        public void scale(string axis, float scaleValue,Point transformacion)
        {
            this.matriz.SetCentroAcarreado(transformacion.X, transformacion.Y, transformacion.Z);
            switch (axis)
            {
                case "x":
                    this.matriz.SetEscalacion(scaleValue, 0, 0);
                    break;
                case "y":
                    this.matriz.SetEscalacion(0, scaleValue, 0);
                    break;
                case "z":
                    this.matriz.SetEscalacion(0, 0, scaleValue);
                    break;
            }

        }
        public void scale(string axis, float scaleValue)
        {
            this.matriz.SetCentroAcarreado(centroAcarreado.X, centroAcarreado.Y, centroAcarreado.Z);
            switch (axis)
            {
                case "x":
                    this.matriz.SetEscalacion(scaleValue, 0, 0);
                    break;
                case "y":
                    this.matriz.SetEscalacion(0, scaleValue, 0);
                    break;
                case "z":
                    this.matriz.SetEscalacion(0, 0, scaleValue);
                    break;
            }

        }
        public void rotate(string axis,float angle, Point transformacion)
        {
            this.matriz.SetCentroAcarreado(transformacion.X, transformacion.Y, transformacion.Z);
            switch (axis) {
                case "x":
                    this.matriz.SetRotacion(angle, 0, 0);
                    break;
                case "y":
                    this.matriz.SetRotacion(0, angle, 0);
                    break;
                case "z":
                    this.matriz.SetRotacion(0, 0, angle);
                    break;          
            }
                

        }
        public void rotate(string axis, float angle)
        {
            this.matriz.SetCentroAcarreado(centroAcarreado.X, centroAcarreado.Y, centroAcarreado.Z);
            switch (axis)
            {
                case "x":
                    this.matriz.SetRotacion(angle, 0, 0);
                    break;
                case "y":
                    this.matriz.SetRotacion(0, angle, 0);
                    break;
                case "z":
                    this.matriz.SetRotacion(0, 0, angle);
                    break;
            }
        }

        public void limpiar()
        {
            this.matriz.Limpiar();
        }
    }
}
