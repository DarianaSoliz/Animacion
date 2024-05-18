using Newtonsoft.Json;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProGrafica
{
    internal class Partes
    {
        [JsonProperty("poligonos")]
        private Dictionary<string, Poligono> poligonos;
        [JsonProperty("centro")]
        private Point centro;
        [JsonProperty("centroResto")]
        private Point centroResto { get; set; }

        public Partes()
        {
            this.poligonos = new Dictionary<string, Poligono>();
            this.centro = new Point(0,0,0);
            this.centroResto= new Point(0, 0, 0);
        }
        public Partes(Point centro, Dictionary<string, Poligono> poligonos)
        {
            this.poligonos=poligonos;
            this.centro = centro;
            this.centroResto = new Point(0, 0, 0);
            setCentroResto(this.centroResto);
        }
        public Point Centro
        {
            get { return centro; }
            set { this.centro = value; }
        }
        public void UpdateVertices()
        {
            foreach (string nombre in poligonos.Keys)
            {
                poligonos[nombre].UpdateVertices();
            }
        }
        public void setCentroResto(Point centroResto)
        {
            this.centroResto = centroResto;
            foreach (Poligono poligono in poligonos.Values)
            {
                poligono.setCentroAcarreado(centroResto + centro);
            }
        }
        public void addPoligono(string name, Poligono poligono)
        {
            poligonos.Add(name, poligono);
        }
        public void removePoligono(string name)
        {
            poligonos.Remove(name);
        }
        public Poligono buscarPoligono(string nombre)
        {
            if (poligonos.ContainsKey(nombre))
            {
                return poligonos[nombre];
            }
            else
            {
                return null;
            }
        }
        public void draw()
        {
            draw(new Point(0, 0, 0));
        }
        public void draw(Point centroUp)
        {
            Point centroResto = centro + centroUp;
            foreach(Poligono poligono in poligonos.Values) 
            { 
                poligono.draw(centroResto);
            }
        }

        internal void translate(string eje, float translateValue)
        {
            foreach (Poligono poligono in poligonos.Values)
            {
                poligono.translate(eje, translateValue);
            }
        }
        public void scale(string eje, float scaleValue)
        {
            foreach (Poligono poligono in poligonos.Values)
            {
                poligono.scale(eje, scaleValue, centro+centroResto);
            }
        }
        public void scale(string eje, float scaleValue, Point transformacion)
        {
            foreach (Poligono poligono in poligonos.Values)
            {
                poligono.scale(eje, scaleValue,transformacion);
            }
        }
        public void rotate(string eje, float angle)
        {
            foreach (Poligono poligono in poligonos.Values)
            {
                poligono.rotate(eje,angle,centro+centroResto);
            }
        }
        public void rotate(string eje, float angle, Point transformacion)
        {
            foreach (Poligono poligono in poligonos.Values)
            {
                poligono.rotate(eje, angle, transformacion);
            }
        }

        public void limpiar()
        {
            foreach (Poligono poligono in poligonos.Values)
            {
                poligono.limpiar();
            }
        }
    }
}
