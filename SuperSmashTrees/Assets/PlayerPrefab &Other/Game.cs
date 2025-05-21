using System.Collections;
using UnityEngine;

public class Game : MonoBehaviour
{
    // Método llamado cuando un nuevo jugador se une
    public void PlayerJoined(Player p)
    {
        // Asignar un color aleatorio
        Color randomColor = new Color(Random.value, Random.value, Random.value);
        p.ChangeColor(randomColor);
    }
}