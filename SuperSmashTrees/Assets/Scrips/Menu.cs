using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToScene0 : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0); // Cargar escena con índice 0
        }
    }
}
