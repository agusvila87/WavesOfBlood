using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitGameButton : MonoBehaviour
{
    public void QuitGame()
    {
        Debug.Log("Volver al Menu del juego");
        SceneManager.LoadScene("MainMenu");
    }
}
