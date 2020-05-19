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
    public int gameLvl;

    //InGame
    [Header("In Game")]
    public static bool inGame; //игровая сцена
    public static bool alive; //жив ли игрок
    public static bool win; //победа
    public static bool lose; //проигрыш
    public static bool start; //начало волны

    [System.Serializable]
    public class Guns
    {
        public string name;
        public int price;
       // public GameObject gunObject;
        public bool purchised;
    }

    public Guns[] guns;

    [System.Serializable]
    public class Caves
    {
        public string name;
        public int price;
        public GameObject caveObject;
        public bool purchised;

        public void ShowCave()
        {
            if (purchised) caveObject.SetActive(true); else caveObject.SetActive(false);
        }
    }
        //public Caves CavesScript;
        public Caves[] caves;
    //public bool[] cavePurchised;

    [System.Serializable]
    public class Indicators
    {
        public Health health;
        public Experiece experience;
    }
    public Indicators indicators;
    
    [System.Serializable]
    public class Health
    {
        public Image healthBar;
        public int hp;
        public Text currentHpText;
        public int maxHP;
        public Text maxHpText;
    }
    [System.Serializable]
    public class Experiece
    {
        public int playerLvl;
        public Text playerLvlText;
        public int playerExp;
        public int playerExpToNextLvl;
        public Image expBar;
    }


    // [Header("Pocket")]
    [System.Serializable]
    public class Pocket
    {
        public int money;
        public int diamonds;

        public Text moneyAmount;
        public Text diamondsAmount;

        public void AddDiamonds(int amount)
        {
            diamonds += amount;
            diamondsAmount.text = "";
            diamondsAmount.text += diamonds;
        }

        public void AddMoney(int amount)
        {
            money += amount;
            moneyAmount.text = "";
            moneyAmount.text += money;
        }
    }

    public Pocket pocket;



    //InMenu


    [SerializeField]
    public static int gun;

    public int HP()
    {
        return indicators.health.hp;
    }
    public void PlusHP(int change)
    {
        indicators.health.hp += change;
        indicators.health.healthBar.fillAmount = (float)indicators.health.hp / (float)indicators.health.maxHP;
        indicators.health.currentHpText.text = indicators.health.hp.ToString();
        indicators.health.maxHpText.text = indicators.health.maxHP.ToString();
        if (indicators.health.hp <= 0)
        {
            lose = true;
            inGame = false;
            Debug.Log("You lose!");
            //DBgetset.StartCoroutine(SaveResults());
            StartCoroutine(FinishDefeat());
        }
    }
    //public int HP { get => hp; set => hp = value; }
    public int MaxHP { get => indicators.health.maxHP; set => indicators.health.maxHP = value; }
    public int GameLvl { get => gameLvl; set => gameLvl = value; }
    public int PlayerExp { get => indicators.experience.playerExp; set => indicators.experience.playerExp = value; }

        //Exp increase and level increase
    public void AddPlayerExp(int amount)
    {
        indicators.experience.playerExp += amount;
        indicators.experience.expBar.fillAmount = (float)indicators.experience.playerExp / (float)PlayerExpToNextLvl;
        if (indicators.experience.playerExp >= PlayerExpToNextLvl)
        {
            indicators.experience.playerLvl++;
            indicators.experience.playerLvlText.text = indicators.experience.playerLvl.ToString();
            indicators.experience.playerExp -= PlayerExpToNextLvl;
            PlayerExpToNextLvl += 20;
            pocket.AddDiamonds(1);
        }
    }
    public int PlayerLvl { get => indicators.experience.playerLvl; set => indicators.experience.playerLvl = value; }
    public int PlayerExpToNextLvl { get => indicators.experience.playerExpToNextLvl; set => indicators.experience.playerExpToNextLvl = value; }





       
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Mob")
        {
            indicators.health.hp -= 10;
            Debug.Log("Player take damage! HP left: " + indicators.health.hp);
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

            indicators.health.hp = data.maxHP;//data.hp;
            MaxHP = data.maxHP;

            indicators.experience.playerLvl = data.playerLvl;
            indicators.experience.playerLvlText.text = indicators.experience.playerLvl.ToString();
            indicators.experience.playerExp = data.playerExp;
            PlayerExpToNextLvl = data.playerExpToNextLvl;
            gameLvl = data.gameLvl;

            pocket.money = data.money;
            pocket.diamonds = data.diamonds;


            caves[0].purchised = data.cave_healing;

            guns[0].purchised = true;
            guns[1].purchised = data.gun_coin;
        } else
        {
            indicators.health.hp = 100;
            MaxHP = 100;
            indicators.experience.playerExp = 0;
            indicators.experience.playerLvl = 1;
            indicators.experience.playerLvlText.text = indicators.experience.playerLvl.ToString();
            PlayerExpToNextLvl = 10;
            gameLvl = 1;
        }
        //gun = 0;

        //Obnovitj healthbar
        PlusHP(0);
        AddPlayerExp(0);
        pocket.AddDiamonds(0);
        pocket.AddMoney(0);
        caves[0].ShowCave();
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
        pocket.AddDiamonds(1);
        SaveSystem.SavePlayer(this);
        yield return new WaitForSeconds(3);
        Debug.Log("Go to menu!");
        
        Application.LoadLevel("main");

    }


    //если игрок вышел, закидываем его информацию на сервер
    void OnApplicationQuit()
    //public void quit()
    {
        //TimeSystem.CheckTime();
        SaveSystem.SavePlayer(this);
        StartCoroutine(SaveSystem.SavePlayerOnServer(this));
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            //SaveSystem.SavePlayer(this);
            //StartCoroutine(SaveSystem.SavePlayerOnServer(this));
            Debug.Log("Game paused");
            
        } else
        {
            //Application.LoadLevel("Login");
            Debug.Log("Game unpaused");
        }
    }


    public void testLoad()
    {
        SaveSystem.SavePlayer(this);
        StartCoroutine(SaveSystem.SavePlayerOnServer(this));

    }

}
