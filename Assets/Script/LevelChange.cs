using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChange : MonoBehaviour
{
    

    public void ChangeLevel(string levelName)
    {
        PlayerManager.Instance.SavePlayer();  
        SceneManager.LoadScene(levelName);
    }
}
