using UnityEngine;
using BinaryTree;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private TreeVisualizer[] visualizers; // Visualizadores para cada jugador
    public IBinaryTree[] playerTrees; // Árbol actual de cada jugador
    private string currentChallenge; // Desafío actual
    private int requiredLevels; // Niveles requeridos para el desafío
    private string currentTreeType; // Tipo de árbol del desafío actual ("BST" o "AVL")
    public string Tag; 
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }
    //Método para inicializar el juego y los árboles
private int IndiceTag(string tag)
{
    // 1. Verificar si el tag es nulo o vacío
    if (string.IsNullOrEmpty(tag))
    {
        return 0;
    }

    // 2. Obtener el último carácter
    char ultimoCaracter = tag[tag.Length - 1];

    // 3. Verificar si es un dígito
    if (char.IsDigit(ultimoCaracter))
    {
        // 4. Convertir el carácter a número
        int numero = int.Parse(ultimoCaracter.ToString());
        return numero;
    }
    else
    {
        // 5. Si no es un dígito, retornar 0
        return 0;
    }
}

public void InsertAux()
{



}
    private void Start()
    {
        // Inicializar árboles y visualizadores
        playerTrees = new IBinaryTree[visualizers.Length];

        // Asignar un árbol inicial (puede ser BST o AVL)
        for (int i = 0; i < playerTrees.Length; i++)
        {
            playerTrees[i] = new PureLogicBST(); // Por defecto, inicia con BST
        }

        // Generar el primer desafío
        GenerateChallenge();
    }
    
    //Es solo un código de test para ver que si se logra insertar un nodo en el árbol
    
    public void PlayerTouchedNumber(string playerTag, int number)
    {
        // Determinar el índice del jugador según el tag
        int playerIndex = GetPlayerIndex(playerTag);
        if (playerIndex == -1) return;

        // Insertar el número en el árbol del jugador
        playerTrees[playerIndex].Insert(number);

        // Actualizar la visualización del árbol
        visualizers[playerIndex].VisualizeTree(playerTrees[playerIndex].GetRoot());

        // Verificar si el jugador ha completado el desafío
        //Eb este método se puede agregar el llamado al método que va a agregar los puntos a los jugadores
        if (VerifyChallenge(playerIndex))
        {
            Debug.Log($"¡Jugador {playerIndex + 1} completó el desafío!");
            ResetTrees();
            // finishChallenge();
            GenerateChallenge();
        }
    }

    private int GetPlayerIndex(string playerTag)
    {
        if (playerTag.StartsWith("Player"))
        {
            if (int.TryParse(playerTag.Substring(6), out int index))
            {
                return index - 1; // Convertir a índice basado en 0
            }
        }
        return -1;
    }

    private void GenerateChallenge()
    {
        // Generar un desafío aleatorio para BST o AVL
        currentTreeType = Random.Range(0, 2) == 0 ? "BST" : "AVL";
        requiredLevels = Random.Range(3, 6);
        currentChallenge = $"Haz un árbol {currentTreeType} de {requiredLevels} niveles";
        Debug.Log($"Nuevo desafío: {currentChallenge}");

        // Cambiar el tipo de árbol de cada jugador según el desafío
        for (int i = 0; i < playerTrees.Length; i++)
        {
            if (currentTreeType == "BST")
            {
                playerTrees[i] = new PureLogicBST();
            }
            else if (currentTreeType == "AVL")
            {
                playerTrees[i] = new PureLogicAVL();
            }
        }
    }

    private bool VerifyChallenge(int playerIndex)
    {
        int currentLevel = playerTrees[playerIndex].GetRoot().MedirNivel();
        return currentLevel >= requiredLevels;
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