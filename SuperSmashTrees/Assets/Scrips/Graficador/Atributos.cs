using UnityEngine;
using BinaryTree;
using System.Linq;
using NUnit.Framework;
using System.Collections.Generic;
using System;
using PlayerC;

namespace Atributo
{
    [DisallowMultipleComponent]
    public class Atributos : MonoBehaviour
    {
       
        public static Atributos Instance { get; set; }
        // Árbol actual de cada jugador
        public IBinaryTree[] playerTrees;

        // Componentes y propiedades del desafío
        private PlayerCounter playerCounter;
        public string CurrentChallenge { get; private set; }
        public int RequiredLevels { get; private set; }
        public string CurrentTreeType { get; private set; }

        void Awake()
        {
            Instance = this;
            playerCounter = GetComponent<PlayerCounter>();
            GeneraAtrubutos();
            AsignaArboles();
        }

        // Métodos de entrega pública
        public IBinaryTree[] EntregaLista() => playerTrees;
        public string EntregaDesafio()   => CurrentChallenge;
        public int EntregaNiveles()      => RequiredLevels;
        public string EntregaTipo()      => CurrentTreeType;

        // Asigna los árboles según el tipo generado
        public void AsignaArboles()
        {
            if (CurrentTreeType == "BST")
            {
                for (int i = 0; i < playerTrees.Length; i++)
                {
                    playerTrees[i] = new PureLogicBST();
                }
            }
            else if (CurrentTreeType == "AVL")
            {
                for (int i = 0; i < playerTrees.Length; i++)
                {
                    playerTrees[i] = new PureLogicAVL();
                }
            }
        }

        // Genera el desafío y el tamaño del arreglo
        public void GeneraAtrubutos()
        {
            GenerateChallenge();
            int num = playerCounter.NumJugadores();
            playerTrees = new IBinaryTree[num];
        }

        // Lógica de creación del desafío
        private void GenerateChallenge()
        {
            int a = UnityEngine.Random.Range(0, 2);
            if (a == 0)
            {
                CurrentTreeType = "BST";
            }
            else
            {
                CurrentTreeType = "AVL";
            }

            RequiredLevels = UnityEngine.Random.Range(3, 6);
            CurrentChallenge = $"Haz un árbol {CurrentTreeType} de {RequiredLevels} niveles";
            Debug.Log(CurrentChallenge);
        }
    }
}
