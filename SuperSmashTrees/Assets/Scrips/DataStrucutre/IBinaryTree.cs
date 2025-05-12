using UnityEngine;

namespace BinaryTree
{
    public interface IBinaryTree
    {
        void Insert(int value); // Método para insertar un valor en el árbol
        IBinaryTreeNode GetRoot(); // Método para obtener la raíz del árbol
    }
}