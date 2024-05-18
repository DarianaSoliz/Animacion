using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProGrafica
{
    class Ejecucion
    {
        Libreto libreto;
        public Boolean pause;
        public Boolean finish;
        public int tiempoInicial;
        public Ejecucion(Libreto libreto)
        {
            this.libreto = libreto;
            this.pause = false;
        }

        public void Execute()
        {
            tiempoInicial = Environment.TickCount;
            int tiempoTotal = 10000;
            List<Acciones> listaDeAcciones = libreto.listaDeAcciones;

            while (true)
            {
                int tiempoTranscurrido = Environment.TickCount - tiempoInicial;

                if (tiempoTranscurrido >= tiempoTotal)
                {
                    this.finish = true;
                    tiempoInicial = Environment.TickCount;
                }

                while (finish)
                {
                    Thread.Sleep(500);
                    tiempoInicial += 500;
                }

                while (this.pause)
                {
                    
                    Thread.Sleep(500);
                    tiempoInicial += 500;
                }

                foreach (Acciones accion in listaDeAcciones)
                {
                    int tiempoActual = Environment.TickCount - tiempoInicial;
                    Objeto objeto = libreto.escenario.buscarObjeto(accion.keyObjeto);
                    Partes parte = null;
                    if (accion.keyParte != "")
                    {
                        parte = objeto.buscarPartes(accion.keyParte);
                    }
                    List<Transformacion> listaDeTransformaciones = accion.listaDeTransformaciones;
                    foreach (Transformacion transformacion in listaDeTransformaciones)
                    {
                        if (tiempoActual >= transformacion.tiempoInicio && tiempoActual <= (transformacion.tiempoInicio + transformacion.duracion))
                        {
                            if (transformacion.LastExecutionTime == 0 || (Environment.TickCount - transformacion.LastExecutionTime) >= 42)
                            {
                                //Console.WriteLine(Environment.TickCount - transformacion.LastExecutionTime);

                                RunConversion(objeto, transformacion, parte, accion);
                                transformacion.LastExecutionTime = Environment.TickCount;
                            }
                        }
                    }

                    //Thread.Sleep(1); // Pequeña pausa para evitar uso intensivo de CPU
                }
            }
        }

        public void Pause()
        {
            this.pause = true;
        }
        public void UnPause()
        {
            this.pause = false;
        }

        public void Stop()
        {
            this.pause = true;
            this.finish = false;
            tiempoInicial = Environment.TickCount;
            libreto.escenario.limpiar();
        }


        private void RunConversion(Objeto objeto, Transformacion transformacion, Partes parte, Acciones accion)
        {
            float diferencial = (float)transformacion.diferencial;
            if (accion.keyParte == "")
            {
                switch (transformacion.transformacion)
                {
                    case "Traslacion":
                        objeto.translate(transformacion.eje, diferencial);
                        break;
                    case "Rotacion":
                        objeto.rotate(transformacion.eje, diferencial);
                        break;
                    case "Escalacion":
                        objeto.scale(transformacion.eje, diferencial);
                        break;
                }
            }
            else
            {
                switch (transformacion.transformacion)
                {
                    case "Traslacion":
                        parte.translate(transformacion.eje, diferencial);
                        break;
                    case "Rotacion":
                        parte.rotate(transformacion.eje, diferencial);
                        break;
                    case "Escalacion":
                        parte.scale(transformacion.eje, diferencial);
                        break;
                }
            }

        }
    }
}
