using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement; // Agrega esto

public class Timer : MonoBehaviour
{
    [SerializeField] private TMP_Text timerText;
    [SerializeField, Tooltip("Tiempo en segundos")] private float timerTime;

    private int minutes, seconds, cents;

    private void Start()
    {
        StartCoroutine(StartTimer());
    }

    private IEnumerator StartTimer()
    {
        while (timerTime > 0)
        {
            timerTime -= Time.deltaTime;
            minutes = Mathf.FloorToInt(timerTime / 60);
            seconds = Mathf.FloorToInt(timerTime % 60);
            cents = Mathf.FloorToInt((timerTime * 100) % 100);

            timerText.text = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, cents);
            yield return null;
        }

        timerText.text = "00:00:00";
        yield return new WaitForSeconds(1f); // Espera opcional antes de cambiar de escena
        SceneManager.LoadScene("Menu"); // Cambia "Menu" por el nombre exacto de tu escena de menú si es diferente
    }
}