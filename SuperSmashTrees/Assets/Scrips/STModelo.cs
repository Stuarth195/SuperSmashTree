using UnityEngine;
using Nodos;
using DLLForUnityStandart;
using System;
using LogicTree;    
using Random = UnityEngine.Random;
using System.Collections.Generic;
using Unity.VisualScripting;
public class STModelo : MonoBehaviour

{

    /// </summary>__________________________________________________________ Singleton _____________________________________________________________________
    /// instancia del singleton
    /// <summary>
    public static STModelo Instance { get; private set; }

    /// <summary>  ___________________________________________________________ Variables _____________________________________________________________________
    /// se inicio el juego?
    /// contididad de jugadores
    /// reto
    /// lista de tags de los jugadores
    /// lista de esferas
    /// reto finalizado?
    /// </summary>

    public bool OnGame = true;

    //cantidad de jugadores
    public int NumPlayer;

    // string de los retos
    public string Reto;

    public int NivelReto;

    public string MensajeDelReto;

    // lista de tags de los jugadores
    public ListaSimple<string> playertags;

    // Lista de esferas
    public ListaSimple<Nodo> ListaNodos;

    //reto finalizado?
    bool Terminado = false;

    private LogicTree.PureLogicBST pureLogicBST;
    private LogicTree.PureLogicAVL pureLogicAVL;



    /// <summary> ___________________________________________________________ Metodos _____________________________________________________________________
    /// Convierte el tag de un jugador a su número correspondiente
    /// logica al colisionar con una esfera
    /// contar jugadores
    /// <summary>

    private void GenerateChallenge()
    {
        // Generar un desafío aleatorio para BST o AVL
        Reto = Random.Range(0, 2) == 0 ? "BST" : "AVL";
        NivelReto = Random.Range(3, 6);
        MensajeDelReto = $"Haz un árbol {Reto} de {NivelReto} niveles";
        Debug.Log($"Nuevo desafío: {MensajeDelReto}");
    }
    private bool VerifyChallenge(int playerIndex)
    {
        int NivelRetoActual = ListaNodos.ElementoEn(playerIndex).Height;
        if (NivelRetoActual ==  NivelReto)
            Debug.LogWarning("RETO COMPLETADO ✅✅✅✅✅✅✅✅✅✅✅✅✅✅✅✅✅✅");

        return NivelRetoActual == NivelReto; 
    }
    private void ResetTrees()
    {
            ListaNodos.Limpiar();
    }
    public int PlayerNum(string tag)

    {
        if (!tag.StartsWith("Player"))
            throw new ArgumentException($"El tag '{tag}' no comienza con 'Player'.");

        string numberPart = tag.Substring("Player".Length);

        if (string.IsNullOrEmpty(numberPart))
            throw new ArgumentException($"El tag '{tag}' no tiene número al final.");

        if (!int.TryParse(numberPart, out int number))
            throw new ArgumentException($"El tag '{tag}' tiene una parte numérica inválida.");

        if (number < 1)
            throw new ArgumentException($"El número en el tag '{tag}' debe ser mayor o igual a 1.");

        return number - 1;
    }

    // AL COLICIONAR CON UNA ESFERA
public void RecibirColision(int playerIndex, int numEsfera)
{
     Debug.LogWarning($"Obtuve {playerIndex}    {numEsfera}  ");
    Nodo nodoJugador = ListaNodos.ElementoEn(playerIndex);
    Debug.LogWarning($"Obtuve {playerIndex}    {numEsfera} {nodoJugador.value} ");
       
    // Si aún no hay árbol para el jugador, lo creamos y lo asignamos
        if (nodoJugador == null)
        {
            Debug.LogWarning($"primer caso  ");
            try
            {
                nodoJugador = new Nodo();
                if (playerIndex < ListaNodos.Tamano())
                {
                    ListaNodos.ReemplazaEn(playerIndex, nodoJugador);
                }
                else
                {
                    ListaNodos.AgregarFinal(nodoJugador);

                }
            }
            catch (Exception ex)
            {
                Debug.LogError("Algo salió mal al agregar el nodo: " + ex.Message);
            }
        }

    try
    {
            if (Reto == "AVL")
            {
                nodoJugador = pureLogicAVL.Insert(nodoJugador, numEsfera);
                
                Debug.LogWarning($"se inserto {nodoJugador} con logica AVL");
            }
            else if (Reto == "BST")
            {
                nodoJugador = pureLogicBST.Insert(nodoJugador, numEsfera);
                Debug.LogWarning($"se inserto {nodoJugador} con logita BST");
            }

        // 🔁 Reemplazamos la nueva raíz del árbol en la lista
        ListaNodos.ReemplazaEn(playerIndex, nodoJugador);
    }
    catch (Exception ex)
    {
        Debug.LogWarning($"Error al insertar {numEsfera}: {ex.Message}");
    }

    // Validar si cumplió el reto
    if (VerifyChallenge(playerIndex))
    {
            try
            {
                ResetTrees();
                CrearEspacioListas();
                GenerateChallenge();
                
        }
            catch (Exception ex)
            {
                Debug.LogError("Error al generar nuevo reto: " + ex.Message);
            }
    }
}

    public void StartGameButon()
    {
        OnGame = true;
        CrearEspacioListas();
        GenerateChallenge();


    }

    public void CrearEspacioListas()
    {
        NumPlayer = contarJugadores();
        for (int i = 0; i < NumPlayer; i++)
        {
            Nodo nodoJugador = new Nodo();
            ListaNodos.AgregarFinal(nodoJugador);
        }
    }

    /// CONTAR JUGADORES
    public int contarJugadores()
    {
        int totalPlayers = 0;

        // Recorremos cada tag usando tu clase ListaSimple
        foreach (string tag in playertags.Recorrer())
        {
            GameObject[] taggedObjects = GameObject.FindGameObjectsWithTag(tag);
            totalPlayers += taggedObjects.Length;
        }
        // Si el número de jugadores es mayor a 0, se inicia el juego
        Debug.Log($"Total de jugadores con los tags seleccionados: {totalPlayers}");
        return totalPlayers;

    }

    /// <summary>___________________________________________________________ Full Unity Logic _____________________________________________________________________
    /// Inicializa la lista de tags de jugadores
    /// Inicializa la lista de nodos
    /// Inicializa el número de jugadores
    /// Inicializa el reto
    /// Inicializa el estado del juego
    /// Inicializa el estado de terminado
    /// Inicializa la lista de nodos
    /// </summary>

    void Start()
    {
        // Inicializa la lista de tags de jugadores
        playertags = new ListaSimple<string>();
        playertags.AgregarFinal("Player1");
        playertags.AgregarFinal("Player2");
        playertags.AgregarFinal("Player3");
        // Inicializa la lista de nodos
        ListaNodos = new ListaSimple<Nodo>();
        // Inicializa el número de jugadores
        NumPlayer = 0;
        // Inicializa el reto
        Reto = "";
        // Inicializa el estado del juego
        OnGame = false;
        // Inicializa el estado de terminado
        Terminado = false;
        // Inicializa la lista de nodos
        pureLogicAVL = new PureLogicAVL();
        pureLogicBST = new PureLogicBST();


    }

    // Actualiza el juego
    // se llama cada frame
   

void Update()
{
    if (Input.GetKeyDown(KeyCode.M))
    {
            // Aquí va tu lógica personalizada
            StartGameButon();
        Debug.Log("Se presionó la tecla M");
    }
}



void Awake()
{
    if (Instance == null)
    {
        Instance = this;
        DontDestroyOnLoad(gameObject); // Opcional, si quieres que persista entre escenas
    }
    else
    {
        Destroy(gameObject); // Previene duplicados si es singleton
    }
}


}


    

