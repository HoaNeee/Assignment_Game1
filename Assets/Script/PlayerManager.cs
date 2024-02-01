using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    private GameObject player;
    public int coin;
    public int hp = 5;

    public GameObject Player
    {
        
        get { return player; }
        set { player = value; }
    }
    

    public int Coin
    {
        get { return coin; }
        set { coin = value; }
    }

    public int HP
    {
        get { return hp; }
        set { hp = value; }
    }

    private static PlayerManager _instance;


    public static PlayerManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = new GameObject("PlayerManager");
                _instance = go.AddComponent<PlayerManager>();
                DontDestroyOnLoad(go);
            }
            return _instance;
        }
    }

    public bool hasLevelChanged = false;
    public void SavePlayer()
    {
        
            PlayerPrefs.SetInt("Coin", coin);
            PlayerPrefs.SetInt("HP", hp);
            PlayerPrefs.Save();
            
        
    }

    public void LevelChange()
    {
        hasLevelChanged=true;
    }
}
