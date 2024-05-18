using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProGrafica
{
    class Acciones
    {
        public string keyObjeto;
        public string keyParte;
        public List<Transformacion> listaDeTransformaciones;

        public Acciones(string keyObjeto, List<Transformacion> listaDeTransformaciones)
        {
            this.keyObjeto = keyObjeto;
            this.keyParte = "";
            this.listaDeTransformaciones = listaDeTransformaciones;
        }
        public Acciones(string keyObjeto, string keyParte, List<Transformacion> listaDeTransformaciones)
        {
            this.keyObjeto = keyObjeto;
            this.keyParte = keyParte;
            this.listaDeTransformaciones = listaDeTransformaciones;
        }
    }
}
