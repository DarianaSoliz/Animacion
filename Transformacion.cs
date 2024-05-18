using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProGrafica
{
    class Transformacion
    {
        public string transformacion, eje;
        public int cantidadATransformar;
        public long duracion;
        public long tiempoInicio;
        public decimal diferencial;
        public long segundos = 0;
        public long LastExecutionTime = 0;

        public Transformacion(string transformacion, int cantidadATransformar, string eje, long duracion, long tiempoInicio)
        {
            this.transformacion = transformacion;
            this.cantidadATransformar = cantidadATransformar;
            this.duracion = duracion;
            this.eje = eje;
            this.tiempoInicio = tiempoInicio;
            segundos = this.duracion / 1000;
            diferencial = (decimal)this.cantidadATransformar / (22 * segundos);
        }
    }
}
