using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    //Common
    [Header("Common")]
    public static string playerName;
    public static string playerPassword;
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

   // [Header("Pocket")]
    [System.Serializable]
    public class Pocket
    {
        public int money;
        public Text moneyAmount;
        public int diamonds;
        public Text diamondsAmount;
    }

    public Pocket pocket;



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
            AddDiamonds(1);
        }
    }
    public int PlayerLvl { get => playerLvl; set => playerLvl = value; }
    public int PlayerExpToNextLvl { get => playerExpToNextLvl; set => playerExpToNextLvl = value; }

    public void AddMoney(int amount)
    {
        this.pocket.money += amount;
        this.pocket.moneyAmount.text = "";
        this.pocket.moneyAmount.text += this.pocket.money;
    }

    public void AddDiamonds(int amount)
    {
        this.pocket.diamonds += amount;
        this.pocket.diamondsAmount.text = "";
        this.pocket.diamondsAmount.text += this.pocket.diamonds;
    }

       
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
            userID = data.playerid;
            playerName = data.playerName;
            playerPassword = data.playerPassword;

            hp = data.hp;
            MaxHP = data.maxHP;

            playerLvl = data.playerLvl;
            playerExp = data.playerExp;
            PlayerExpToNextLvl = data.playerExpToNextLvl;
            gameLvl = data.gameLvl;

            pocket.money = data.money;
            pocket.diamonds = data.diamonds;
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
        AddDiamonds(0);
        AddMoney(0);
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
        AddDiamonds(1);
        SaveSystem.SavePlayer(this);
        yield return new WaitForSeconds(3);
        Debug.Log("Go to menu!");
        
        Application.LoadLevel("main");

    }


    //если игрок вышел, закидываем его информацию на сервер
    void OnApplicationQuit()
    //public void quit()
    {
        SaveSystem.SavePlayer(this);
        StartCoroutine(SaveSystem.SavePlayerOnServer(this));
        Debug.Log("Application ending after " + Time.time + " seconds");
    }

    public void testLoad()
    {
        //SaveSystem.SavePlayer(this);
        StartCoroutine(SaveSystem.SavePlayerOnServer(this));

    }

}
