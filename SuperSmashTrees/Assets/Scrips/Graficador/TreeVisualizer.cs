using UnityEngine;
using TMPro;
using Nodos;
using DLLForUnityStandart;

namespace VisualizerTree
{
    public class TreeVisualizer : MonoBehaviour
    {
        [Header("Configuraci\u00f3n Visual")]
        public GameObject nodoPrefab;
        public float espaciadoHorizontal = 2f;
        public float espaciadoVertical = 1.5f;
        public float espacioEntreArboles = 5f;

        [Header("Posiciones Iniciales (por jugador)")]
        public Vector3 posicionJugador1 = new Vector3(-5f, -4f, 0f);
        public Vector3 posicionJugador2 = new Vector3(0f, -4f, 0f);
        public Vector3 posicionJugador3 = new Vector3(5f, -4f, 0f);

        private Diccionario<int, ListaSimple<GameObject>> arbolesInstanciados = new Diccionario<int, ListaSimple<GameObject>>();
        private Cola<GameObject> colaLineas = new Cola<GameObject>();

        public void GraficarArbol(Nodo raiz, int jugador)
        {
            EliminarArbol(jugador);

            Vector3 posicionInicial = CalcularPosicionInicial(jugador);

            if (raiz != null)
            {
                ListaSimple<GameObject> objetos = new ListaSimple<GameObject>();
                CalcularYInstanciar(raiz, posicionInicial, 0, objetos);
                arbolesInstanciados.AgregarOActualizar(jugador, objetos);
            }
        }

        private void CalcularYInstanciar(Nodo nodo, Vector3 posicion, int nivel, ListaSimple<GameObject> objetos)
        {
            if (nodo == null) return;

            GameObject nuevoNodo = Instantiate(nodoPrefab, posicion, Quaternion.identity, transform);
            nuevoNodo.GetComponentInChildren<TextMeshPro>().text = nodo.value.ToString();
            objetos.Insertar(nuevoNodo);

            float offsetX = espaciadoHorizontal / (nivel + 1);
            Vector3 posIzq = posicion + new Vector3(-offsetX, -espaciadoVertical, 0);
            Vector3 posDer = posicion + new Vector3(offsetX, -espaciadoVertical, 0);

            if (nodo.Left != null)
            {
                DibujarLinea(posicion, posIzq);
                CalcularYInstanciar(nodo.Left, posIzq, nivel + 1, objetos);
            }

            if (nodo.Right != null)
            {
                DibujarLinea(posicion, posDer);
                CalcularYInstanciar(nodo.Right, posDer, nivel + 1, objetos);
            }
        }

        private void DibujarLinea(Vector3 inicio, Vector3 fin)
        {
            GameObject linea = new GameObject("Linea");
            linea.transform.SetParent(transform);

            LineRenderer lr = linea.AddComponent<LineRenderer>();
            lr.startWidth = 0.1f;
            lr.endWidth = 0.1f;
            lr.material = new Material(Shader.Find("Standard")) { color = Color.gray };
            lr.SetPositions(new Vector3[] { inicio, fin });

            colaLineas.Encolar(linea);
        }

        public void EliminarArbol(int jugador)
        {
            if (arbolesInstanciados.ContieneClave(jugador))
            {
                ListaSimple<GameObject> objetos = arbolesInstanciados.Obtener(jugador);
                while (!objetos.EstaVacia())
                {
                    Destroy(objetos.ElementoEn(0));
                    objetos.EliminarEn(0);
                }
                arbolesInstanciados.Eliminar(jugador);
            }

            while (!colaLineas.EstaVacia())
            {
                Destroy(colaLineas.Desencolar());
            }
        }

        public void EliminarTodosLosArboles()
        {
            ListaSimple<int> jugadores = new ListaSimple<int>();
            foreach (var key in arbolesInstanciados.RecorrerClaves())
            {
                jugadores.Insertar(key);
            }

            while (!jugadores.EstaVacia())
            {
                EliminarArbol(jugadores.ElementoEn(0));
                jugadores.EliminarEn(0);
            }
        }

        private Vector3 CalcularPosicionInicial(int jugador)
        {
            switch (jugador)
            {
                case 1: return posicionJugador1;
                case 2: return posicionJugador2;
                case 3: return posicionJugador3;
                default: return transform.position;
            }
        }
    }
}