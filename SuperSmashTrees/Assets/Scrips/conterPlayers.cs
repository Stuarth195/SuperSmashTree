using UnityEngine;

// PlayerCounter.cs (modificado)
namespace PlayerC
{
    public class PlayerCounter : MonoBehaviour
    {
        [Tooltip("Tags de jugadores (ej: Player1, Player2)")]
        public string[] playerTags;
        private int totalPlayers = 0;

        // Método público para forzar un recuento
        public void ActualizarConteo()
        {
            totalPlayers = 0;
            foreach (string tag in playerTags)
            {
                GameObject[] taggedObjects = GameObject.FindGameObjectsWithTag(tag);
                totalPlayers += taggedObjects.Length;
            }
            Debug.Log($"Total de jugadores: {totalPlayers}");
        }

        public int NumJugadores() => totalPlayers;
    }
}