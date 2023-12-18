using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public void StartNewGame()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadGame()
    {
        string path = Application.dataPath + "/Data.json";
        if (File.Exists(path))
        {
            var dataStr = File.ReadAllText(path);
            var data = JsonUtility.FromJson<PlayerData>(dataStr);
            SceneManager.LoadScene(data.IndexScene);
        }
        else
        {
            Debug.Log("Сохранных игр нет");
            StartNewGame();
        }
    }
}
