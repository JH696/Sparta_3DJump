using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{
    public void RetryButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainScene");
    }
}
