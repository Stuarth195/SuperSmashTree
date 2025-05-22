using System;
using UnityEngine;
using Nodos;

namespace LogicTree
{
    public class PureLogicAVL
    {
        
        STModelo gamemanager = STModelo.Instance;
        // Insertar valor en árbol AVL y retornar nueva raíz
        public Nodo Insert(Nodo root, int value)
        {
            if (root == null)
                return new Nodo(value);

            if (value < root.value)
                root.Left = Insert(root.Left, value);
            else if (value > root.value)
                root.Right = Insert(root.Right, value);
            else // valores duplicados no permitidos
                return root;

            // Actualizar altura del nodo actual
            root.Height = 1 + Math.Max(GetHeight(root.Left), GetHeight(root.Right));

            // Obtener factor de balance
            int balance = GetBalance(root);

            // Rotaciones para balancear el árbol

            // Caso Izquierda Izquierda
            if (balance > 1 && value < root.Left.value)
                return RightRotate(root);

            // Caso Derecha Derecha
            if (balance < -1 && value > root.Right.value)
                return LeftRotate(root);

            // Caso Izquierda Derecha
            if (balance > 1 && value > root.Left.value)
            {
                root.Left = LeftRotate(root.Left);
                return RightRotate(root);
            }

            // Caso Derecha Izquierda
            if (balance < -1 && value < root.Right.value)
            {
                root.Right = RightRotate(root.Right);
                return LeftRotate(root);
            }

            // Retornar raíz sin cambios si ya está balanceado
            return root;
        }

        // Obtener altura segura (si nodo es null, 0)
        private int GetHeight(Nodo nodo)
        {
            if (nodo == null)
                return 0;
            return nodo.Height;
        }

        // Calcular factor de balance del nodo
        private int GetBalance(Nodo nodo)
        {
            if (nodo == null)
                return 0;
            return GetHeight(nodo.Left) - GetHeight(nodo.Right);
        }

        // Rotación a la derecha
        private Nodo RightRotate(Nodo y)
        {
            Nodo x = y.Left;
            Nodo T2 = x.Right;

            // Rotación
            x.Right = y;
            y.Left = T2;

            // Actualizar alturas
            y.Height = 1 + Math.Max(GetHeight(y.Left), GetHeight(y.Right));
            x.Height = 1 + Math.Max(GetHeight(x.Left), GetHeight(x.Right));

            // Retornar nueva raíz
            return x;
        }

        // Rotación a la izquierda
        private Nodo LeftRotate(Nodo x)
        {
            Nodo y = x.Right;
            Nodo T2 = y.Left;

            // Rotación
            y.Left = x;
            x.Right = T2;

            // Actualizar alturas
            x.Height = 1 + Math.Max(GetHeight(x.Left), GetHeight(x.Right));
            y.Height = 1 + Math.Max(GetHeight(y.Left), GetHeight(y.Right));

            // Retornar nueva raíz
            return y;
        }
    }
}
