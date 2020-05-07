using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    //Common
    [Header("Common")]
    public static string userLogin;
    public static int userID;
    public int playerLvl;
    public int playerExp;
    public int playerExpToNextLvl;
    public Image expBar;
    public int gameLvl;

    //InGame
    [Header("In Game")]
    public static bool inGame; //игровая сцена
    public static bool alive; //жив ли игрок
    public static bool win; //победа
    public static bool lose; //проигрыш
    public static bool start; //начало волны

    [Header("Health")]
    public Image healthBar;
    public int hp;
    public int maxHP;





    //InMenu


    [SerializeField]
    public static string gun;

    public int HP()
    {
        return hp;
    }
    public void PlusHP(int change)
    {
        hp += change;
        healthBar.fillAmount = (float)hp / (float)maxHP;

        if (hp <= 0)
        {
            lose = true;
            inGame = false;
            Debug.Log("You lose!");
            //DBgetset.StartCoroutine(SaveResults());
            StartCoroutine(FinishDefeat());
        }
    }
    //public int HP { get => hp; set => hp = value; }
    public int MaxHP { get => maxHP; set => maxHP = value; }
    public int GameLvl { get => gameLvl; set => gameLvl = value; }
    public int PlayerExp { get => playerExp; set => playerExp = value; }

        //Exp increase and level increase
    public void AddPlayerExp(int amount)
    {
        playerExp += amount;
        expBar.fillAmount = (float)playerExp / (float)PlayerExpToNextLvl;
        if (playerExp >= PlayerExpToNextLvl)
        {
            playerLvl++;
            playerExp -= PlayerExpToNextLvl;
            PlayerExpToNextLvl += 20;

        }
    }
    public int PlayerLvl { get => playerLvl; set => playerLvl = value; }
    public int PlayerExpToNextLvl { get => playerExpToNextLvl; set => playerExpToNextLvl = value; }


    //void Update()
    // {
    //healthBar.fillAmount = (float)HP/(float)MaxHP;
    //}




    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Mob")
        {
            hp -= 10;
            Debug.Log("Player take damage! HP left: " + hp);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        //Load data from storage
        PlayerData data = SaveSystem.LoadPlayer();
        
        if (data != null && data.playerid == userID) { 
            hp = data.hp;
            MaxHP = data.maxHP;

            playerLvl = data.playerLvl;
            playerExp = data.playerExp;
            PlayerExpToNextLvl = data.playerExpToNextLvl;
            gameLvl = data.gameLvl;
        } else
        {
            hp = 100;
            MaxHP = 100;
            playerExp = 0;
            playerLvl = 1;
            PlayerExpToNextLvl = 10;
            gameLvl = 1;
        }
        gun = "rock";

        //Obnovitj healthbar
        PlusHP(0);
        AddPlayerExp(0);
    }



    //если игрок проиграл
    public IEnumerator FinishDefeat()
    {
        SaveSystem.SavePlayer(this);
        yield return new WaitForSeconds(3);
        Debug.Log("Go to menu!");
        Application.LoadLevel("main");

    }

    //если игрок выиграл
    public IEnumerator FinishWin()
    {
        this.GameLvl++;
        SaveSystem.SavePlayer(this);
        yield return new WaitForSeconds(3);
        Debug.Log("Go to menu!");
        
        Application.LoadLevel("main");

    }


    //если игрок вышел, закидываем его информацию на сервер
    void OnApplicationQuit()
    //public void quit()
    {
        StartCoroutine(SaveSystem.SavePlayerOnServer(this));
        Debug.Log("Application ending after " + Time.time + " seconds");
    }

    public void testLoad()
    {
        StartCoroutine(SaveSystem.SavePlayerOnServer(this));

    }

}
