using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public static MainMenu instance;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    public void New_Game()
    {
        print("hi");
        SceneManager.LoadScene("SampleScene");
    }

    public void Load_Scene()
    {
        SceneManager.LoadScene("SampleScene");
        SaveGame.mainmenu_load = true;
    }

    public void AppExit()
    {
        Application.Quit();
    }
}
