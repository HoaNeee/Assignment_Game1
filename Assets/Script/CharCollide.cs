using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CharCollide : MonoBehaviour
{
    
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI hpText;
    public TextMeshProUGUI coinCollectOver;
    public TextMeshProUGUI coinCollectWin;

    public GameObject panelEnd;
    public GameObject panelWin;

    private Vector3 resestPoint;
    private Rigidbody2D rb;

    public static CharCollide instance;
    public string nextLevelName = "LV2";

    public AudioClip coinClip;
    public AudioClip hpClip;
    public AudioClip music;
    public bool isPlayingBgrMusic = true;
    public AudioClip winGame;
    private AudioSource audioSource;


    

    // Start is called before the first frame update
    void Start()
    {
        //coinText.SetText(coin.ToString());
        //hpText.SetText(hp.ToString()); 
        
        rb = GetComponent<Rigidbody2D>();
        resestPoint = transform.position;
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(music);
        isPlayingBgrMusic=true;

        PlayerManager.Instance.SavePlayer();
        LoadPlayer();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Coin"))
        {
            PlayerManager.Instance.Coin++;
            PlayerManager.Instance.SavePlayer();
            coinText.SetText(PlayerManager.Instance.Coin.ToString());
            coinCollectOver.SetText(PlayerManager.Instance.Coin.ToString());
            coinCollectWin.SetText(PlayerManager.Instance.Coin.ToString());
            Destroy(collision.gameObject);
            audioSource.PlayOneShot(coinClip);
        }
        if (collision.CompareTag("Mons"))
        {
            PlayerManager.Instance.HP--;
            PlayerManager.Instance.SavePlayer();
            hpText.SetText(PlayerManager.Instance.HP.ToString());
            audioSource.PlayOneShot(hpClip);
            if(PlayerManager.Instance.HP <= 0)
            {
                //CharDie();

                panelEnd.SetActive(true);
                Time.timeScale = 0;
                if(isPlayingBgrMusic)
                {
                    audioSource.Stop();
                    audioSource.PlayOneShot(hpClip);
                    isPlayingBgrMusic = false;
                }
            }

         
        }
        if (collision.CompareTag("Door"))
        {
            panelWin.SetActive(true);
            Time.timeScale = 0;
            if (isPlayingBgrMusic)
            {
                audioSource.Stop();
                audioSource.PlayOneShot(winGame);
                isPlayingBgrMusic = false;
            }
            
        }
        if (collision.CompareTag("Player"))
        {
            PlayerManager.Instance.Player = collision.GameObject();
            PlayerManager.Instance.Coin = PlayerManager.Instance.Coin;
            PlayerManager.Instance.HP = PlayerManager.Instance.HP;
        }
    }
    //private void OnDisable()
    //{
    //    SavePlayer();
    //}

    //public void SavePlayer()
    //{
    //    PlayerManager.Instance.Coin = PlayerManager.Instance.Coin;
    //    PlayerManager.Instance.HP = PlayerManager.Instance.HP;
    //    PlayerPrefs.SetInt("Coin", PlayerManager.Instance.Coin);
    //    PlayerPrefs.SetInt("HP", PlayerManager.Instance.HP);
    //    PlayerPrefs.Save();
    //}
    

    public void LoadPlayer()
    {
        PlayerManager.Instance.Coin = PlayerPrefs.GetInt("Coin", 0);
        PlayerManager.Instance.HP = PlayerPrefs.GetInt("HP", 5);

        coinText.SetText(PlayerManager.Instance.Coin.ToString());
        hpText.SetText(PlayerManager.Instance.HP.ToString());
    }

    public void LoadScence(string name)
    {
        //SavePlayer();
        PlayerManager.Instance.SavePlayer();
        SceneManager.LoadScene(name);
        
    }

    public void ResestPlayer()
    {
        PlayerManager.Instance.Coin = 0;
        PlayerManager.Instance.HP = 5;
        coinText.SetText(PlayerManager.Instance.Coin.ToString());
        hpText.SetText(PlayerManager.Instance.HP.ToString());
        rb.simulated = true;
        //transform.position = resestPoint;
    }
    public void RestartGame()
    {
        panelEnd.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;

        PlayerManager.Instance.Coin = 0;
        PlayerManager.Instance.HP = 5;
        coinText.SetText(PlayerManager.Instance.Coin.ToString());
        hpText.SetText(PlayerManager.Instance.HP.ToString());
        rb.simulated = true;
        //transform.position = resestPoint;
    }
    private void MovePlayerSpawm()
    {
        GameObject spawmPoint = GameObject.FindGameObjectWithTag("spawmPoint");

        if(spawmPoint != null)
        {
            transform.position = spawmPoint.transform.position;
        }   
        else
        {
            Debug.Log("Khong tim thay diem spawm");
        }
    }
    void CharDie()
    {
        rb.simulated = false;
        float rs = 2f;
        Invoke("ResestPlayer", rs);
        
    }

    public void NextLevel()
    {
        panelWin.SetActive(false);
        LevelChange levelChanger = FindObjectOfType<LevelChange>();
        if (levelChanger != null)
        {

            levelChanger.ChangeLevel(nextLevelName);

            LoadPlayer();
        }
        else
        {
            Debug.LogError("Không tìm thấy LevelChange trong Scene.");
        }
        Time.timeScale = 1f;
    }
}
