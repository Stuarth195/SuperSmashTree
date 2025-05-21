namespace Nodos
{
    using System.Collections;
    // Esta clase representa un nodo en un árbol binario genérico.
    public class Nodo
    {
        public int value;
        public Nodo Left;
        public Nodo Right;
        public int Height;

        // Constructor sin parámetros: nodo vacío (valor por defecto)
        public Nodo()
        {
            value = default(int);
            Left = null;
            Right = null;
            Height = 0;
        }

        // Constructor con valor específico
        public Nodo(int valor)
        {
            value = valor;
            Left = null;
            Right = null;
            Height = 1; // Altura inicial del nodo es 1
        }
    }
}
