using System.Collections;
using UnityEngine;
using Random = System.Random;

public class SpawnMob : MonoBehaviour
{
    public GameObject playerObject;
    public PlayerStats player;// = playerObject.GetComponent<PlayerStats>();

    public GameObject mob1;
    public GameObject mob2;
    private int currentTotalMobHP = 0;
    private int mobcounter = 0;
    private int deadmobcounter = 0;
    [SerializeField]
    public int mobsLimit = 10;
    public float mobSpawnDelay = 1.5f;

    public int CurrentTotalMobHP()
    {
        return currentTotalMobHP;
    }
    public void PlusTotalMob(int mobHP)
    {
        currentTotalMobHP += mobHP;
        mobcounter++;
    }

    public void MinusTotalMobHP(int HP)
    {
        currentTotalMobHP -= HP;
    }

    public void MinusMob()
    {
        if (++deadmobcounter >= mobsLimit && PlayerStats.inGame)
        {
            //Wictory
            PlayerStats.win = true;
            PlayerStats.inGame = false;
            Debug.Log("You win!");
            // DBgetset.StartCoroutine(SaveResults());
            StartCoroutine(player.FinishWin());
        }
    }

    void Start ()
    {
        
        playerObject = GameObject.Find("Player");
        player = playerObject.GetComponent<PlayerStats>();



        StartCoroutine(Spawn ());
    }

    IEnumerator Spawn ()
    {
        Random rand = new Random();
        yield return new WaitForSeconds(2);
        Debug.Log("player game level:    " + player.gameLvl);
        mobsLimit = player.gameLvl;
        Debug.Log("Lets GO!");
        PlayerStats.start = true;
        while (PlayerStats.inGame && mobcounter<mobsLimit)
        {
            float randomY = (float)rand.Next(-19, -16)/10;
            //создание моба
            GameObject mob = Instantiate(mob1, new Vector2 (10f, randomY), Quaternion.identity);
            mob.transform.parent = this.transform;
            yield return new WaitForSeconds(mobSpawnDelay);
        }
    }

/*    //если игрок выиграл
    static IEnumerator FinishWin()
    {
        yield return new WaitForSeconds(3);
        Debug.Log("Go to menu!");
        Application.LoadLevel("main");
    }*/




}
