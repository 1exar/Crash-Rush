using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RestartButton : MonoBehaviour
{
    
    public void RestartLevel()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void Next()
    {
        int level = PlayerPrefs.GetInt("level", 0);
        if (level == 2)
        {
            level = 0;
        }
        else
        {
            level++;
        }

        PlayerPrefs.SetInt("level", level);
        RestartLevel();
    }
    
    public void ToGameScene()
    {
        SceneManager.LoadScene(1);
    }
}
