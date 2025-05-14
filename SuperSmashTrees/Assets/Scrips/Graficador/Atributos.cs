using UnityEngine;
using BinaryTree;
using PlayerC;
using System;
using UnityEngine;


namespace Atributo
{
    [DisallowMultipleComponent]
    public class Atributos : MonoBehaviour
    {
        public static Atributos Instance { get; private set; } // Cambiado a private set
        public IBinaryTree[] playerTrees;

        // Componentes y propiedades del desafío
        private PlayerCounter playerCounter;
        public string CurrentChallenge { get; private set; }
        public int RequiredLevels { get; private set; }
        public string CurrentTreeType { get; private set; }

        void Awake()
        {
            // Singleton corregido
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;

            // Verificar componente PlayerCounter
            playerCounter = GetComponent<PlayerCounter>();
            if (playerCounter == null)
            {
                Debug.LogError("PlayerCounter no asignado en el GameObject");
                return;
            }
        }

        // Método público para inicialización manual
        public void     InicializarAtributos()
        {
            GeneraAtrubutos();
            AsignaArboles();
        }

        // Métodos de entrega pública
        public IBinaryTree[] EntregaLista() => playerTrees;
        public string EntregaDesafio() => CurrentChallenge;
        public int EntregaNiveles() => RequiredLevels;
        public string EntregaTipo() => CurrentTreeType;

public void AsignaArboles()
{
    if (playerTrees == null || playerTrees.Length == 0)
    {
        Debug.LogError("Lista de árboles no inicializada");
        return;
    }

    for (int i = 0; i < playerTrees.Length; i++)
    {
        switch (CurrentTreeType)
        {
            case "BST":
                playerTrees[i] = new PureLogicBST();
                break;
            case "AVL":
                playerTrees[i] = new PureLogicAVL();
                break;
            default:
                throw new ArgumentException("Tipo de árbol no válido");
        }
    }
}

// Atributos.cs (modificado)
        public void GeneraAtrubutos()
        {
            if (playerCounter == null)
            {
                Debug.LogError("PlayerCounter es null");
                return;
            }

            playerCounter.ActualizarConteo(); // Forzar recuento
            int num = playerCounter.NumJugadores();
            
            if (num == 0)
            {
                Debug.LogError("¡No hay jugadores en la escena!");
                return;
            }

            playerTrees = new IBinaryTree[num];
            GenerateChallenge(); // Generar desafío después de contar jugadores
        }

        private void GenerateChallenge()
        {
            CurrentTreeType = UnityEngine.Random.Range(0, 2) == 0 ? "BST" : "AVL";
            RequiredLevels = UnityEngine.Random.Range(3, 6);
            CurrentChallenge = $"Haz un árbol {CurrentTreeType} de {RequiredLevels} niveles";
            Debug.Log(CurrentChallenge);
        }
    }
}