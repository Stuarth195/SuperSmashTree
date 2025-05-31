using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace texteditor
{
    public class EditorDeLineas
    {
        private string rutaAbsoluta;

        public EditorDeLineas()
        {
#if UNITY_EDITOR
            rutaAbsoluta = Path.Combine(Application.dataPath, "Resources", "puntajes.txt");
#else
            rutaAbsoluta = Path.Combine(Directory.GetParent(Application.dataPath).FullName, "SuperSmashTrees_Data", "Resources", "puntajes.txt");
#endif

            if (!File.Exists(rutaAbsoluta))
            {
                File.WriteAllText(rutaAbsoluta, "");
            }
        }

        public void EscribirEnLinea(string texto, int linea)
        {
            List<string> lineas = new List<string>();

            if (File.Exists(rutaAbsoluta))
                lineas.AddRange(File.ReadAllLines(rutaAbsoluta));

            while (lineas.Count <= linea)
                lineas.Add("");

            lineas[linea] = texto;

            File.WriteAllLines(rutaAbsoluta, lineas);
        }

        public string LeerTodo()
        {
            if (File.Exists(rutaAbsoluta))
                return File.ReadAllText(rutaAbsoluta);
            return "";
        }

        public string ObtenerRuta()
        {
            return rutaAbsoluta;
        }

        // 🔹 GET Puntaje - devuelve string
        public string ObtenerPuntaje(int linea)
        {
            var datos = LeerDatos(linea);
            return datos.puntaje;  // ahora es string
        }

        // 🔹 SET Puntaje - recibe string
        public void SetPuntaje(int linea, string nuevoPuntaje)
        {
            var datos = LeerDatos(linea);
            EscribirDatos(linea, datos.nombre, nuevoPuntaje, datos.posicion);
        }

        // 🔹 GET Posición - devuelve string
        public string ObtenerPosicion(int linea)
        {
            var datos = LeerDatos(linea);
            return datos.posicion;  // ahora es string
        }

        // 🔹 SET Posición - recibe string
        public void SetPosicion(int linea, string nuevaPosicion)
        {
            var datos = LeerDatos(linea);
            EscribirDatos(linea, datos.nombre, datos.puntaje, nuevaPosicion);
        }

        // 🔧 Método auxiliar para leer y dividir la línea, ahora puntaje y posicion son string
        private (string nombre, string puntaje, string posicion) LeerDatos(int linea)
        {
            if (!File.Exists(rutaAbsoluta)) return ("", "", "");

            var lineas = new List<string>(File.ReadAllLines(rutaAbsoluta));

            if (linea < 0 || linea >= lineas.Count) return ("", "", "");

            string contenido = lineas[linea].Trim();

            if (contenido.Contains(";"))
            {
                string[] partes = contenido.Split(';');
                string nombre = partes[0].Trim();
                string[] valores = partes[1].Trim().Split(' ');

                string puntaje = valores.Length > 0 ? valores[0] : "";
                string posicion = valores.Length > 1 ? valores[1] : "";

                return (nombre, puntaje, posicion);
            }

            return ("", "", "");
        }

        // 🔧 Método auxiliar para reescribir la línea con strings
        private void EscribirDatos(int linea, string nombre, string puntaje, string posicion)
        {
            var lineas = new List<string>(File.ReadAllLines(rutaAbsoluta));

            if (linea < 0 || linea >= lineas.Count) return;

            lineas[linea] = $"{nombre}; {puntaje} {posicion}";

            File.WriteAllLines(rutaAbsoluta, lineas);
        }
    }
}
