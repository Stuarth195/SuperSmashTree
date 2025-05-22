using System;
using System.Collections.Generic;

namespace DLLForUnityStandart
{
    // Clase Nodo: representa cada elemento de la lista genérica
    public class Nodo<T> // Nodo genérico
    {
        public T dato; // Dato del nodo
        public Nodo<T> siguiente; // Referencia al siguiente nodo

        public Nodo(T valor) // Constructor del nodo
        { 
            dato = valor;     // Asigna el valor al dato
            siguiente = null; // Inicializa el siguiente nodo como null
        }
    }

    // Clase ListaSimple: lista enlazada simple genérica
    public class ListaSimple<T>
    {
        private Nodo<T> primero;

        public ListaSimple()
        {
            primero = null;
        }

        // Inserta al final por defecto
        public void Insertar(T valor)
        {
            AgregarFinal(valor);
        }

        // Inserta un nodo al final de la lista
        public void AgregarFinal(T valor)
        {
            Nodo<T> nuevo = new Nodo<T>(valor);

            if (primero == null)
            {
                primero = nuevo;
            }
            else
            {
                Nodo<T> actual = primero;
                while (actual.siguiente != null)
                {
                    actual = actual.siguiente;
                }
                actual.siguiente = nuevo;
            }
        }

        // Devuelve todos los elementos como lista (recorrido)
        public List<T> Recorrer()
        {
            List<T> elementos = new List<T>();
            Nodo<T> actual = primero;
            while (actual != null)
            {
                elementos.Add(actual.dato);
                actual = actual.siguiente;
            }
            return elementos;
        }

        // Retorna el elemento en el índice especificado
        public T ElementoEn(int indice)
        {
            if (indice < 0)
                throw new ArgumentOutOfRangeException();

            Nodo<T> actual = primero;
            int i = 0;
            while (actual != null)
            {
                if (i == indice)
                    return actual.dato;
                actual = actual.siguiente;
                i++;
            }

            throw new ArgumentOutOfRangeException("Índice fuera de rango.");
        }

        // Devuelve el índice del primer elemento igual a 'valor'
        public int IndiceDe(T valor)
        {
            Nodo<T> actual = primero;
            int i = 0;
            while (actual != null)
            {
                if (actual.dato.Equals(valor))
                    return i;
                actual = actual.siguiente;
                i++;
            }
            return -1;
        }

        // Verifica si está vacía
        public bool EstaVacia()
        {
            return primero == null;
        }

        // Devuelve el tamaño
        public int Tamano()
        {
            int contador = 0;
            Nodo<T> actual = primero;
            while (actual != null)
            {
                contador++;
                actual = actual.siguiente;
            }
            return contador;
        }

        // Limpia la lista
        public void Limpiar()
        {
            primero = null;
        }

        // Elimina nodo en índice específico
        public void EliminarEn(int indice)
        {
            if (indice < 0 || EstaVacia())
                throw new ArgumentOutOfRangeException();

            if (indice == 0)
            {
                primero = primero.siguiente;
                return;
            }

            Nodo<T> actual = primero;
            int i = 0;

            while (actual != null && i < indice - 1)
            {
                actual = actual.siguiente;
                i++;
            }

            if (actual == null || actual.siguiente == null)
                throw new ArgumentOutOfRangeException("Índice fuera de rango.");

            actual.siguiente = actual.siguiente.siguiente;
        }

        // Elimina el primer nodo con el valor especificado
        public bool EliminarElemento(T valor)
        {
            if (EstaVacia()) return false;

            if (primero.dato.Equals(valor))
            {
                primero = primero.siguiente;
                return true;
            }

            Nodo<T> actual = primero;
            while (actual.siguiente != null)
            {
                if (actual.siguiente.dato.Equals(valor))
                {
                    actual.siguiente = actual.siguiente.siguiente;
                    return true;
                }
                actual = actual.siguiente;
            }

            return false;
        }
    }


    // Lista ejecutable cm colas 
    public class Cola<T>
    {
        private Nodo<T> frente;
        private Nodo<T> fin;

        public Cola()
        {
            frente = null;
            fin = null;
        }

        public bool EstaVacia()
        {
            return frente == null;
        }

        public void Encolar(T valor)
        {
            Nodo<T> nuevo = new Nodo<T>(valor);
            if (EstaVacia())
            {
                frente = nuevo;
                fin = nuevo;
            }
            else
            {
                fin.siguiente = nuevo;
                fin = nuevo;
            }
        }

        public T Desencolar()
        {
            if (EstaVacia())
                throw new InvalidOperationException("La cola está vacía.");

            T valor = frente.dato;
            frente = frente.siguiente;

            if (frente == null)
                fin = null;

            return valor;
        }

        public T Consultar()
        {
            if (EstaVacia())
                throw new InvalidOperationException("La cola está vacía.");

            return frente.dato;
        }
    }

    // lista ejecutable cm pilas

    public class Pila<T>
    {
        private Nodo<T> tope;

        public Pila()
        {
            tope = null;
        }

        public bool EstaVacia()
        {
            return tope == null;
        }

        public void Apilar(T valor)
        {
            Nodo<T> nuevo = new Nodo<T>(valor);
            nuevo.siguiente = tope;
            tope = nuevo;
        }

        public T Desapilar()
        {
            if (EstaVacia())
                throw new InvalidOperationException("La pila está vacía.");

            T valor = tope.dato;
            tope = tope.siguiente;
            return valor;
        }

        public T Consultar()
        {
            if (EstaVacia())
                throw new InvalidOperationException("La pila está vacía.");

            return tope.dato;
        }
    }

    
    // Nodo para lista doblemente enlazada
    public class NodoDoble<T>
    {
        public T dato;
        public NodoDoble<T> siguiente;
        public NodoDoble<T> anterior;

        public NodoDoble(T valor)
        {
            dato = valor;
            siguiente = null;
            anterior = null;
        }
    }

    // Lista doblemente enlazada genérica
    public class ListaDoble<T>
    {
        private NodoDoble<T> cabeza;
        private NodoDoble<T> cola;

        public ListaDoble()
        {
            cabeza = null;
            cola = null;
        }

        public bool EstaVacia()
        {
            return cabeza == null;
        }

        public void AgregarAlFinal(T valor)
        {
            NodoDoble<T> nuevo = new NodoDoble<T>(valor);
            if (EstaVacia())
            {
                cabeza = nuevo;
                cola = nuevo;
            }
            else
            {
                cola.siguiente = nuevo;
                nuevo.anterior = cola;
                cola = nuevo;
            }
        }

        public void AgregarAlInicio(T valor)
        {
            NodoDoble<T> nuevo = new NodoDoble<T>(valor);
            if (EstaVacia())
            {
                cabeza = nuevo;
                cola = nuevo;
            }
            else
            {
                nuevo.siguiente = cabeza;
                cabeza.anterior = nuevo;
                cabeza = nuevo;
            }
        }

        public bool Eliminar(T valor)
        {
            NodoDoble<T> actual = cabeza;
            while (actual != null)
            {
                if (actual.dato.Equals(valor))
                {
                    if (actual.anterior != null)
                        actual.anterior.siguiente = actual.siguiente;
                    else
                        cabeza = actual.siguiente;

                    if (actual.siguiente != null)
                        actual.siguiente.anterior = actual.anterior;
                    else
                        cola = actual.anterior;

                    return true;
                }
                actual = actual.siguiente;
            }
            return false;
        }

        public int Tamano()
        {
            int contador = 0;
            NodoDoble<T> actual = cabeza;
            while (actual != null)
            {
                contador++;
                actual = actual.siguiente;
            }
            return contador;
        }

        public T ElementoEn(int indice)
        {
            if (indice < 0)
                throw new ArgumentOutOfRangeException();

            NodoDoble<T> actual = cabeza;
            int i = 0;
            while (actual != null)
            {
                if (i == indice)
                    return actual.dato;
                actual = actual.siguiente;
                i++;
            }

            throw new ArgumentOutOfRangeException("Índice fuera de rango.");
        }

        public void Limpiar()
        {
            cabeza = null;
            cola = null;
        }
    }
    // Nodo para Array Dinámico
    public class NodoArray<T>
    {
        public T dato;
        public NodoArray<T> siguiente;

        public NodoArray(T valor)
        {
            dato = valor;
            siguiente = null;
        }
    }
    // Clase Array Dinámico
    public class ArrayDinamico<T>
    {
        private NodoArray<T> primero;
        private int contador;

        public ArrayDinamico()
        {
            primero = null;
            contador = 0;
        }

        public int Tamano()
        {
            return contador;
        }

        public void Agregar(T valor)
        {
            NodoArray<T> nuevo = new NodoArray<T>(valor);
            if (primero == null)
            {
                primero = nuevo;
            }
            else
            {
                NodoArray<T> actual = primero;
                while (actual.siguiente != null)
                    actual = actual.siguiente;
                actual.siguiente = nuevo;
            }
            contador++;
        }

        public T ElementoEn(int indice)
        {
            if (indice < 0 || indice >= contador)
                throw new ArgumentOutOfRangeException();

            NodoArray<T> actual = primero;
            int i = 0;
            while (actual != null)
            {
                if (i == indice)
                    return actual.dato;
                actual = actual.siguiente;
                i++;
            }

            throw new ArgumentOutOfRangeException();
        }

        public void Limpiar()
        {
            primero = null;
            contador = 0;
        }
    }

    // Nodo pera Diccionario
    public class NodoDiccionario<K, V>
    {
        public K clave;
        public V valor;
        public NodoDiccionario<K, V> siguiente;

        public NodoDiccionario(K c, V v)
        {
            clave = c;
            valor = v;
            siguiente = null;
        }
    }
    // Clase Diccionario
    public class Diccionario<K, V>
    {
        private NodoDiccionario<K, V> primero;

        public Diccionario()
        {
            primero = null;
        }

        public void AgregarOActualizar(K clave, V valor)
        {
            NodoDiccionario<K, V> actual = primero;
            while (actual != null)
            {
                if (actual.clave.Equals(clave))
                {
                    actual.valor = valor;
                    return;
                }
                actual = actual.siguiente;
            }

            NodoDiccionario<K, V> nuevo = new NodoDiccionario<K, V>(clave, valor);
            nuevo.siguiente = primero;
            primero = nuevo;
        }

        public V Obtener(K clave)
        {
            NodoDiccionario<K, V> actual = primero;
            while (actual != null)
            {
                if (actual.clave.Equals(clave))
                    return actual.valor;
                actual = actual.siguiente;
            }
            throw new KeyNotFoundException("Clave no encontrada.");
        }

        public bool Eliminar(K clave)
        {
            NodoDiccionario<K, V> actual = primero;
            NodoDiccionario<K, V> anterior = null;

            while (actual != null)
            {
                if (actual.clave.Equals(clave))
                {
                    if (anterior == null)
                        primero = actual.siguiente;
                    else
                        anterior.siguiente = actual.siguiente;
                    return true;
                }
                anterior = actual;
                actual = actual.siguiente;
            }
            return false;
        }

        public bool ContieneClave(K clave)
        {
            NodoDiccionario<K, V> actual = primero;
            while (actual != null)
            {
                if (actual.clave.Equals(clave))
                    return true;
                actual = actual.siguiente;
            }
            return false;
        }
    }


}
