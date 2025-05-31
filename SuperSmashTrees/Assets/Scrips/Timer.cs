using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement; // <-- Agrega esto


public class Timer : MonoBehaviour
{
    [SerializeField] private TMP_Text timerText;

    private float timeRemaining = 600f; // 10 minutos en segundos
    private int minutes, seconds, cents;
    private bool timerRunning = false; // Ahora inicia en false

    public void IniciarCronometro()
    {
        if (!timerRunning)
        {
            timerRunning = true;
            StartCoroutine(StartTimer());
        }
    }

    private IEnumerator StartTimer()
    {
        while (timerRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                if (timeRemaining < 0) timeRemaining = 0;

                minutes = (int)(timeRemaining / 60);
                seconds = (int)(timeRemaining % 60);
                cents = (int)((timeRemaining - (int)timeRemaining) * 100f);
                timerText.text = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, cents);
            }
            else
            {
                timerRunning = false;
                timerText.text = "00:00:00";
                SceneManager.LoadScene("Menu"); // Cambia a la escena de menú
            }
            yield return null; // Espera al siguiente frame
        }
    }
}