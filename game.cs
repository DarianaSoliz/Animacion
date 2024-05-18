using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System.Drawing;
using System.Diagnostics;
using System.Threading;

namespace ProGrafica
{
    class game : GameWindow
    {
        private Double theta = 0;
        private double phi = 0;
        Escena cuarto;
        public Ejecucion hilo;
        private Libreto libreto;
        Thread miHilo;



        public game(int widht, int height, string title) : base(widht, height, GraphicsMode.Default, title)
        {


            // -------------------- EL OBJETO AUTO ------------------- //

            Color colorCelestevidrio = Color.FromArgb(1, 168, 204, 215);


            Poligono vidrioDelantero = new Poligono(colorCelestevidrio, new Point(-7, 0, 0));
            vidrioDelantero.addVertice(-2, -2, 2);
            vidrioDelantero.addVertice(2, 2, 2);
            vidrioDelantero.addVertice(2, 2, -2);
            vidrioDelantero.addVertice(-2, -2, -2);


            Poligono vidrioTrasero = new Poligono(colorCelestevidrio, new Point(7, 0, 0));
            vidrioTrasero.addVertice(-2, 2, 2);
            vidrioTrasero.addVertice(2, -2, 2);
            vidrioTrasero.addVertice(2, -2, -2);
            vidrioTrasero.addVertice(-2, 2, -2);

            Color rojoAuto = Color.FromArgb(1, 170, 51, 51);

            Poligono techo = new Poligono(rojoAuto, new Point(0, 2, 0));
            techo.addVertice(-5, 0, -2);
            techo.addVertice(-5, 0, 2);
            techo.addVertice(5, 0, 2);
            techo.addVertice(5, 0, -2);


            Poligono cabinaIzq = new Poligono(rojoAuto, new Point(0, 0, 2));
            cabinaIzq.addVertice(-5, 2, 0);
            cabinaIzq.addVertice(5, 2, 0);
            cabinaIzq.addVertice(9, -2, 0);
            cabinaIzq.addVertice(-9, -2, 0);


            Poligono cabinaDer = new Poligono(rojoAuto, new Point(0, 0, -2));
            cabinaDer.addVertice(-5, 2, 0);
            cabinaDer.addVertice(5, 2, 0);
            cabinaDer.addVertice(9, -2, 0);
            cabinaDer.addVertice(-9, -2, 0);

            Dictionary<string, Poligono> poligonosChasis = new Dictionary<string, Poligono>();
            poligonosChasis.Add("vidrioDelantero", vidrioDelantero);
            poligonosChasis.Add("vidrioTrasero", vidrioTrasero);
            poligonosChasis.Add("Techo", techo);
            poligonosChasis.Add("CabinaIzquierda", cabinaIzq);
            poligonosChasis.Add("CabinaDerecha", cabinaDer);


            Partes chasisAuto = new Partes(new Point(0, 0, 0), poligonosChasis);

            Poligono ruedaCopilotoBack = new Poligono(Color.Yellow, new Point(0, 0, 0));
            ruedaCopilotoBack.addVertice(-2, 2, 0);
            ruedaCopilotoBack.addVertice(2, 2, 0);
            ruedaCopilotoBack.addVertice(2, -2, 0);
            ruedaCopilotoBack.addVertice(-2, -2, 0);

            Poligono ruedaCopiloto = new Poligono(Color.Yellow, new Point(0, 0, 0));
            ruedaCopiloto.addVertice(-2, 2, 0);
            ruedaCopiloto.addVertice(2, 2, 0);
            ruedaCopiloto.addVertice(2, -2, 0);
            ruedaCopiloto.addVertice(-2, -2, 0);

            Poligono ruedaPiloto = new Poligono(Color.Yellow, new Point(0, 0, 0));
            ruedaPiloto.addVertice(-2, 2, 0);
            ruedaPiloto.addVertice(2, 2, 0);
            ruedaPiloto.addVertice(2, -2, 0);
            ruedaPiloto.addVertice(-2, -2, 0);

            Poligono ruedaPilotoBack = new Poligono(Color.Yellow, new Point(0, 0, 0));
            ruedaPilotoBack.addVertice(-2, 2, 0);
            ruedaPilotoBack.addVertice(2, 2, 0);
            ruedaPilotoBack.addVertice(2, -2, 0);
            ruedaPilotoBack.addVertice(-2, -2, 0);

            Dictionary<string, Poligono> poligonosRueda1 = new Dictionary<string, Poligono>();
            Dictionary<string, Poligono> poligonosRueda2 = new Dictionary<string, Poligono>();
            Dictionary<string, Poligono> poligonosRueda3 = new Dictionary<string, Poligono>();
            Dictionary<string, Poligono> poligonosRueda4 = new Dictionary<string, Poligono>();
            poligonosRueda1.Add("Rueda1", ruedaPiloto);
            poligonosRueda2.Add("Rueda2", ruedaCopiloto);
            poligonosRueda3.Add("Rueda3", ruedaPilotoBack);
            poligonosRueda4.Add("Rueda4", ruedaCopilotoBack);

            Partes rueda1 = new Partes(new Point(-6, -3, 2.1f), poligonosRueda1);
            Partes rueda2 = new Partes(new Point(-6, -3, -2.1f), poligonosRueda2);
            Partes rueda3 = new Partes(new Point(6, -3, 2.1f), poligonosRueda3);
            Partes rueda4 = new Partes(new Point(6, -3, -2.1f), poligonosRueda4);


            Dictionary<string, Partes> partesAuto = new Dictionary<string, Partes>();
            partesAuto.Add("Chasis", chasisAuto);
            partesAuto.Add("Rueda1", rueda1);
            partesAuto.Add("Rueda2", rueda2);
            partesAuto.Add("Rueda3", rueda3);
            partesAuto.Add("Rueda4", rueda4);

            Objeto auto = new Objeto(new Point(0, 0, 0), partesAuto);

            // -------------------- EL OBJETO AUTO ------------------- //



            // -------------------- EL OBJETO TV ------------------- //

            Color color1 = Color.Black; 
            Color color2 = Color.FromArgb(41, 41, 41);
            Color color3 = Color.FromArgb(32, 32, 32);
            Color color4 = Color.FromArgb(23, 22, 22);

            // Definir los vértices de cada polígono
            Poligono cara1 = new Poligono(color1, new Point(0f, 0f, 1f));
            cara1.addVertice(2f, 2f, 0f);
            cara1.addVertice(48, 2f, 0f);
            cara1.addVertice(48f, 38f, 0f);
            cara1.addVertice(2f, 38f, 0f);

            Poligono cara2 = new Poligono(color2, new Point(0f, 0f, 0f));
            cara2.addVertice(22f, 0f, 0f);
            cara2.addVertice(22f, -20f, 0f);
            cara2.addVertice(26f, -20f, 0f);
            cara2.addVertice(26f, 0f, 0f);

            Poligono cara3 = new Poligono(color3, new Point(0f, 0f, 0f));
            cara3.addVertice(0f, 0f, 0f);
            cara3.addVertice(50f, 0f, 0f);
            cara3.addVertice(50f, 40f, 0f);
            cara3.addVertice(0f, 40f, 0f);

            Poligono cara4 = new Poligono(color4, new Point(0f, 0f, 0f));
            cara4.addVertice(12f, -20f, 0f);
            cara4.addVertice(36f, -20f, 0f);
            cara4.addVertice(36f, -10f, 0f);
            cara4.addVertice(12f, -10f, 0f);

            Poligono cara5 = new Poligono(color3, new Point(0f, 0f, 0f));
            cara5.addVertice(0f, 0f, -8f);
            cara5.addVertice(50f, 0f, -8f);
            cara5.addVertice(50f, 40f, -8f);
            cara5.addVertice(0f, 40f, -8f);

            Poligono cara6 = new Poligono(color2, new Point(0f, 0f, 0f));
            cara6.addVertice(22f, 0f, -8f);
            cara6.addVertice(22f, -20f, -8f);
            cara6.addVertice(26f, -20f, -8f);
            cara6.addVertice(26f, 0f, -8f);

            Poligono cara7 = new Poligono(color4, new Point(0f, 0f, 0f));
            cara7.addVertice(12f, -20f, -8f);
            cara7.addVertice(36f, -20f, -8f);
            cara7.addVertice(36f, -10f, -8f);
            cara7.addVertice(12f, -10f, -8f);

            Poligono cara8 = new Poligono(color3, new Point(0f, 0f, 0f));
            cara8.addVertice(0f, 0f, 0f);
            cara8.addVertice(0f, 0f, -8f);
            cara8.addVertice(0f, 40f, 0f);
            cara8.addVertice(0f, 40f, -8f);

            Poligono cara9 = new Poligono(color4, new Point(0f, 0f, 0f));
            cara9.addVertice(36f, -20f, 0f);
            cara9.addVertice(36f, -20f, -8f);
            cara9.addVertice(36f, -10f, -8f);
            cara9.addVertice(36f, -10f, 0f);

            Poligono cara10 = new Poligono(color4, new Point(0f, 0f, 0f));
            cara10.addVertice(12f, -20f, 0f);
            cara10.addVertice(12f, -20f, -8f);
            cara10.addVertice(12f, -10f, -8f);
            cara10.addVertice(12f, -10f, 0f);


            Dictionary<string, Poligono> poligonosTV = new Dictionary<string, Poligono>();
            poligonosTV.Add("cara1", cara1);
            poligonosTV.Add("cara2", cara2);
            poligonosTV.Add("cara3", cara3);
            poligonosTV.Add("cara4", cara4);
            poligonosTV.Add("cara5", cara5);
            poligonosTV.Add("cara6", cara6);
            poligonosTV.Add("cara7", cara7);
            poligonosTV.Add("cara8", cara8);
            poligonosTV.Add("cara9", cara9);
            poligonosTV.Add("cara10", cara10);

            Partes parteTV = new Partes(new Point(0, 0, 0), poligonosTV);

            Dictionary<string, Partes> partesTV = new Dictionary<string, Partes>();
            partesTV.Add("TV", parteTV);

            Objeto tv = new Objeto(new Point(-25, 15, -25), partesTV);

            // -------------------- EL OBJETO TV ------------------- //



            // -------------------- EL OBJETO ESFERA ------------------- //

            Color colorEsfera = Color.Blue;


            int slices = 20;
            int stacks = 20;

            Dictionary<string, Poligono> poligonosEsfera = new Dictionary<string, Poligono>();

            // Generar los vértices y polígonos de la esfera
            int contador = 0;
            for (int i = 0; i < stacks; i++)
            {
                double lat0 = Math.PI * (-0.5 + (double)(i) / stacks);
                double z0 = Math.Sin(lat0);
                double zr0 = Math.Cos(lat0);

                double lat1 = Math.PI * (-0.5 + (double)(i + 1) / stacks);
                double z1 = Math.Sin(lat1);
                double zr1 = Math.Cos(lat1);

                for (int j = 0; j < slices; j++)
                {
                    double lng = 2 * Math.PI * (double)(j) / slices;
                    double x = Math.Cos(lng);
                    double y = Math.Sin(lng);

                    double lng1 = 2 * Math.PI * (double)(j + 1) / slices;
                    double x1 = Math.Cos(lng1);
                    double y1 = Math.Sin(lng1);

                    Poligono poligono1 = new Poligono(colorEsfera, new Point(0, 0, 0));
                    poligono1.addVertice((float)(x * zr0), (float)(y * zr0), (float)z0);
                    poligono1.addVertice((float)(x * zr1), (float)(y * zr1), (float)z1);
                    poligono1.addVertice((float)(x1 * zr1), (float)(y1 * zr1), (float)z1);
                    poligonosEsfera.Add("Poligono" + contador++, poligono1);

                    Poligono poligono2 = new Poligono(colorEsfera, new Point(0, 0, 0));
                    poligono2.addVertice((float)(x * zr0), (float)(y * zr0), (float)z0);
                    poligono2.addVertice((float)(x1 * zr1), (float)(y1 * zr1), (float)z1);
                    poligono2.addVertice((float)(x1 * zr0), (float)(y1 * zr0), (float)z0);
                    poligonosEsfera.Add("Poligono" + contador++, poligono2);
                }
            }


            Partes parteEsfera = new Partes(new Point(0, 0, 0), poligonosEsfera);
            Dictionary<string, Partes> partesEsfera = new Dictionary<string, Partes>();
            partesEsfera.Add("Esfera", parteEsfera);

            Objeto esfera = new Objeto(new Point(0, 0, 0), partesEsfera);

            // -------------------- EL OBJETO ESFERA ------------------- //



            // -------------------- EL OBJETO CUBO ------------------- //

            Color colorFrontal = Color.Red;
            Color colorTrasero = Color.Blue;
            Color colorIzquierdo = Color.Green;
            Color colorDerecho = Color.Yellow;
            Color colorSuperior = Color.Orange;
            Color colorInferior = Color.Purple;


            Poligono caraFrontal = new Poligono(colorFrontal, new Point(0, 0, 0));
            caraFrontal.addVertice(-4, -4, 4);
            caraFrontal.addVertice(4, -4, 4);
            caraFrontal.addVertice(4, 4, 4);
            caraFrontal.addVertice(-4, 4, 4);

            Poligono caraTrasera = new Poligono(colorTrasero, new Point(0, 0, 0));
            caraTrasera.addVertice(-4, -4, -4);
            caraTrasera.addVertice(4, -4, -4);
            caraTrasera.addVertice(4, 4, -4);
            caraTrasera.addVertice(-4, 4, -4);

            Poligono caraIzquierda = new Poligono(colorIzquierdo, new Point(0, 0, 0));
            caraIzquierda.addVertice(-4, -4, -4);
            caraIzquierda.addVertice(-4, -4, 4);
            caraIzquierda.addVertice(-4, 4, 4);
            caraIzquierda.addVertice(-4, 4, -4);

            Poligono caraDerecha = new Poligono(colorDerecho, new Point(0, 0, 0));
            caraDerecha.addVertice(4, -4, -4);
            caraDerecha.addVertice(4, -4, 4);
            caraDerecha.addVertice(4, 4, 4);
            caraDerecha.addVertice(4, 4, -4);

            Poligono caraSuperior = new Poligono(colorSuperior, new Point(0, 0, 0));
            caraSuperior.addVertice(-4, 4, -4);
            caraSuperior.addVertice(4, 4, -4);
            caraSuperior.addVertice(4, 4, 4);
            caraSuperior.addVertice(-4, 4, 4);

            Poligono caraInferior = new Poligono(colorInferior, new Point(0, 0, 0));
            caraInferior.addVertice(-4, -4, -4);
            caraInferior.addVertice(4, -4, -4);
            caraInferior.addVertice(4, -4, 4);
            caraInferior.addVertice(-4, -4, 4);

            Dictionary<string, Poligono> poligonosCubo = new Dictionary<string, Poligono>();
            poligonosCubo.Add("CaraFrontal", caraFrontal);
            poligonosCubo.Add("CaraTrasera", caraTrasera);
            poligonosCubo.Add("CaraIzquierda", caraIzquierda);
            poligonosCubo.Add("CaraDerecha", caraDerecha);
            poligonosCubo.Add("CaraSuperior", caraSuperior);
            poligonosCubo.Add("CaraInferior", caraInferior);

            Partes parteCubo = new Partes(new Point(0, 0, 0), poligonosCubo);

            Dictionary<string, Partes> partesCubo = new Dictionary<string, Partes>();
            partesCubo.Add("Cubo", parteCubo);

            Objeto cubo = new Objeto(new Point(10, 10, 0), partesCubo);


            // -------------------- EL OBJETO CUBO ------------------- //




            // -------------------- EL OBJETO PARED ------------------- //

            Color colorPared = Color.FromArgb(1, 255, 255, 204);
            Poligono paredpoly = new Poligono(colorPared, new Point(0, 0, 0));
            paredpoly.addVertice(0, 40, 40);
            paredpoly.addVertice(0, -40, 40);
            paredpoly.addVertice(0, -40, -40);
            paredpoly.addVertice(0, 40, -40);
            Dictionary<string, Poligono> poligonosPared = new Dictionary<string, Poligono>();
            poligonosPared.Add("Pared", paredpoly);
            Partes paredPart = new Partes(new Point(20, 0, 0), poligonosPared);
            paredPart.addPoligono("CaraDeLaPared", paredpoly);

            // -------------------- EL OBJETO PARED ------------------- //



            // -------------------- EL OBJETO REPISA ------------------- //

            Color colorRepisa = Color.FromArgb(101, 56, 24);
            Poligono plataformaRepisa = new Poligono(colorRepisa, new Point(0, -5, 0));
            plataformaRepisa.addVertice(-50, 0, -50);
            plataformaRepisa.addVertice(-50, 0, 50);
            plataformaRepisa.addVertice(50, 0, 50);
            plataformaRepisa.addVertice(50, 0, -50);

            Poligono repisaIzq = new Poligono(colorRepisa, new Point(0, -7, 50));
            repisaIzq.addVertice(-50, 5, 0);
            repisaIzq.addVertice(50, 5, 0);
            repisaIzq.addVertice(50, -5, 0);
            repisaIzq.addVertice(-50, -5, 0);

            Poligono repisaDer = new Poligono(colorRepisa, new Point(0, -7, -50));
            repisaDer.addVertice(-50, 5, 0);
            repisaDer.addVertice(50, 5, 0);
            repisaDer.addVertice(50, -5, 0);
            repisaDer.addVertice(-50, -5, 0);

            Poligono repisaDel = new Poligono(colorRepisa, new Point(-50, -7, 0));
            repisaDel.addVertice(0, 5, 50);
            repisaDel.addVertice(0, 5, -50);
            repisaDel.addVertice(0, -5, -50);
            repisaDel.addVertice(0, -5, 50);

            Dictionary<string, Poligono> poligonosRepisa = new Dictionary<string, Poligono>();
            poligonosRepisa.Add("Plataforma", plataformaRepisa);
            poligonosRepisa.Add("LadoIzquierdo", repisaIzq);
            poligonosRepisa.Add("LadoDerecho", repisaDer);
            poligonosRepisa.Add("LadoDelantero", repisaDel);
            Partes repisaPart = new Partes(new Point(0, 0, 0), poligonosRepisa);
           

            Dictionary<string, Partes> partesRepisa = new Dictionary<string, Partes>();
            partesRepisa.Add("ParteRepisa", repisaPart);
            //partesRepisa.Add("Pared", paredPart);

            Objeto repisa = new Objeto(new Point(0, 0, 0), partesRepisa);

            // -------------------- EL OBJETO REPISA ------------------- //




            // -------------------- CREACION DE LA ESCENA ------------------- //

            Dictionary<string, Objeto> objetosCuarto = new Dictionary<string, Objeto>();
            objetosCuarto.Add("Repisa", repisa);
            //objetosCuarto.Add("Auto", auto);

            objetosCuarto.Add("Cubo", cubo);
            objetosCuarto.Add("TV", tv);

            // objetosCuarto.Add("Esfera", esfera);

            cuarto = new Escena(new Point(0, 0, 0), objetosCuarto);

            // -------------------- CREACION DE LA ESCENA ------------------- //





            // -------------------- CREACION DE LA ANIMACION ------------------- //
            List<Transformacion> listaDeConversiones = new List<Transformacion>();
            List<Transformacion> listaDeConversiones2 = new List<Transformacion>();
            List<Transformacion> listaDeConversiones3 = new List<Transformacion>();

            

            //eje x
            Transformacion accion1 = new Transformacion("Traslacion", -70, "x", 10000, 0);
            listaDeConversiones.Add(accion1);


            //eje z
            Transformacion accion2 = new Transformacion("Rotacion", 2000, "z", 10000, 0);
            listaDeConversiones2.Add(accion2);


            //eje y
            Transformacion accion31 = new Transformacion("Traslacion", -10, "y", 1000, 0);
            Transformacion accion32 = new Transformacion("Traslacion", 10, "y", 1000, 1000);
            Transformacion accion33 = new Transformacion("Traslacion", -10, "y", 1000, 2000);
            Transformacion accion34 = new Transformacion("Traslacion", 10, "y", 1000, 3000);
            Transformacion accion35 = new Transformacion("Traslacion", -10, "y", 1000, 4000);
            Transformacion accion36 = new Transformacion("Traslacion", 10, "y", 1000, 5000);
            Transformacion accion37 = new Transformacion("Traslacion", -10, "y", 1000, 6000);
            Transformacion accion38 = new Transformacion("Traslacion", 10, "y", 1000, 7000);
            Transformacion accion39 = new Transformacion("Traslacion", -10, "y", 1000, 8000);
            Transformacion accion310 = new Transformacion("Traslacion", 10, "y", 1000, 9000);

            listaDeConversiones3.Add(accion31);
            listaDeConversiones3.Add(accion32);
            listaDeConversiones3.Add(accion33);
            listaDeConversiones3.Add(accion34);
            listaDeConversiones3.Add(accion35);
            listaDeConversiones3.Add(accion36);
            listaDeConversiones3.Add(accion37);
            listaDeConversiones3.Add(accion38);
            listaDeConversiones3.Add(accion39);
            listaDeConversiones3.Add(accion310);


           

            Acciones acciones = new Acciones("Cubo", listaDeConversiones);
            Acciones acciones2 = new Acciones("Cubo", listaDeConversiones2);
            Acciones acciones3 = new Acciones("Cubo", listaDeConversiones3);

         

            List<Acciones> listaDeAcciones = new List<Acciones>();
            listaDeAcciones.Add(acciones);
            listaDeAcciones.Add(acciones2);
            listaDeAcciones.Add(acciones3);

          





            // -------------------- CREACION DE LA EJECUCION  ------------------- //
            libreto = new Libreto(listaDeAcciones, cuarto);
            hilo = new Ejecucion(libreto);
            miHilo = new Thread(hilo.Execute);
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);





            GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);
            GL.MatrixMode(MatrixMode.Projection);
            GL.Ortho(-70, 70, -70, 70, -70, 70);
            GL.Enable(EnableCap.DepthTest);
            GL.Rotate(15f, 0, 1, 0);
            GL.Rotate(20f, 1, 0, 0);
        }
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

        }
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit); //wea de la doc
            
            cuarto.draw();

            SwapBuffers();
            base.OnRenderFrame(e);
        }

        [Obsolete]
        protected override void OnKeyDown(KeyboardKeyEventArgs e)
        {
            if (e.Key == Key.K)
            {
                 miHilo.Start();
            }

            if (e.Key == Key.L)
            {
                if (hilo.pause)
                {
                    hilo.UnPause();
                }
                else
                {
                    hilo.Pause();
                }
            }

            if (e.Key == Key.P)
            {
                hilo.Stop();

            }

            base.OnKeyDown(e);
        }

        protected override void OnResize(EventArgs e)
        {
            GL.Viewport(0, 0, Width, Height);


            base.OnResize(e);
        }
    }
}
