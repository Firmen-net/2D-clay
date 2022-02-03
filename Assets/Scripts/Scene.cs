using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Scene : MonoBehaviour
{
    public void start()
    {
        SceneManager.LoadScene("Level1");
    }

    public void quit()
    {
        Application.Quit();
    }
}