using UnityEngine;
using BinaryTree;
using Atributo;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private TreeVisualizer[] visualizers; 
    public IBinaryTree[] playerTrees; 
    private string currentChallenge; 
    private int requiredLevels; 
    private string currentTreeType; 
    

    private void Awake()
    {
        // Singleton
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        // Inicializar desde Atributos
        if (Atributos.Instance != null)
        {
            playerTrees = Atributos.Instance.EntregaLista();
            currentChallenge = Atributos.Instance.EntregaDesafio();
            requiredLevels = Atributos.Instance.EntregaNiveles();
            currentTreeType = Atributos.Instance.EntregaTipo();
        }
        else
        {
            Debug.LogError("¡Atributos no encontrado!");
        }
    }

    private void Start()
    {
        GenerateChallenge();
    }

    public void PlayerTouchedNumber(string playerTag, int number)
    {
        int playerIndex = IndiceTag(playerTag);
    
        if (playerIndex < 0 || playerIndex >= playerTrees.Length)
        {
            Debug.LogError($"Índice inválido: {playerIndex}");
            return;
        }
    
        playerTrees[playerIndex].Insert(number);
    
        // Actualizar la visualización del árbol
        if (visualizers != null && playerIndex < visualizers.Length)
        {
            visualizers[playerIndex].VisualizeTree(playerTrees[playerIndex].GetRoot());
        }
        else
        {
            Debug.LogError("visualizers es null o el índice está fuera de rango.");
        }
    
        if (VerifyChallenge(playerIndex))
        {
            Debug.Log($"¡Desafío completado por jugador {playerIndex + 1}!");
            ResetTrees();
        }
    }

    private int IndiceTag(string tag)
    {
        if (string.IsNullOrEmpty(tag)) return -1;
        char ultimoCaracter = tag[tag.Length - 1]; 
        return char.IsDigit(ultimoCaracter) ? int.Parse(ultimoCaracter.ToString()) - 1 : -1;
    }

    private void GenerateChallenge()
    {
        if (Atributos.Instance != null)
        {
            currentTreeType = Atributos.Instance.EntregaTipo();
            requiredLevels = Atributos.Instance.EntregaNiveles();
            currentChallenge = Atributos.Instance.EntregaDesafio();
            Debug.Log(currentChallenge);
        }
    }

    private bool VerifyChallenge(int playerIndex)
    {
        if (playerTrees[playerIndex].GetRoot() == null) return false;
        int currentLevel = playerTrees[playerIndex].GetRoot().MedirNivel();
        bool completado = currentLevel >= requiredLevels;

        if (completado)
        {
            Atributos.Instance.GeneraAtrubutos();
            Atributos.Instance.AsignaArboles();
            GenerateChallenge();
        }

        return completado;
    }

    private void ResetTrees()
    {
        for (int i = 0; i < playerTrees.Length; i++)
        {
            playerTrees[i] = currentTreeType == "BST" ? new PureLogicBST() : new PureLogicAVL();
            visualizers[i].ClearVisualization();
        }
    }
}