using UnityEngine;
using BinaryTree;
public class Insert
{
    private void AgregarNodo(int Value, string tipo)
    {
        if (tipo == "BST")
        {
            PureLogicBST bst = new PureLogicBST();
            bst.Insert(Value);
            
        }
        
        else if (tipo == "AVL")
        {
            PureLogicAVL avl = new PureLogicAVL();
            avl.Insert(Value);
            
        }
    }
}
