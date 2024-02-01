using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenceManager : MonoBehaviour
{
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Gọi hàm LoadPlayerData của CharItem khi scene mới được tải
        //CharCollide.instance.LoadPlayer();
    }

    public void LoadNextScene()
    {
        // Chuyển đến scene mới
        SceneManager.LoadScene("LV2");
        MovePlayerSpawm();
    }

    private void MovePlayerSpawm()
    {
        GameObject spawmPoint = GameObject.Find("spawmPoint");

        if (spawmPoint != null)
        {
            transform.position = spawmPoint.transform.position;
        }
        else
        {
            Debug.Log("Khong tim thay diem spawm");
        }
    }
}
