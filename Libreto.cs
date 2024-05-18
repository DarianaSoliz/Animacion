using System;
using System.Collections.Generic;


namespace ProGrafica
{
    class Libreto
    {
        public List<Acciones> listaDeAcciones;
        public Escena escenario;

        public Libreto(List<Acciones> listaDeAcciones, Escena escenario)
        {
            this.listaDeAcciones = listaDeAcciones;
            this.escenario = escenario;
        }
    }
}
