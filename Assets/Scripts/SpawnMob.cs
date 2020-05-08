using System.Collections;
using UnityEngine;
using Random = System.Random;

public class SpawnMob : MonoBehaviour
{
    public GameObject playerObject;
    public PlayerStats player;// = playerObject.GetComponent<PlayerStats>();
    public Transform Spawner;



    //Slime
    [System.Serializable]
    public class Mob {
        public string name;
        public GameObject mob1;
        public int power;
    }

    public Mob[] mobs;

    //Fly
    //public GameObject mob2;

    private int currentTotalMobHP = 0;
    private int mobcounter = 0;
    private int deadmobcounter = 0;

    [SerializeField]
    public int maximalMobCountInRow = 2;
    [SerializeField]
    public int mobsLimit = 999;
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

        // playerObject = GameObject.Find("Player");
        //player = playerObject.GetComponent<PlayerStats>();

        string Wave = CreateWave(player.gameLvl);

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
            GameObject mob = Instantiate(mobs[0].mob1, Spawner.transform.position, Quaternion.identity);
            
//            GameObject mob = Instantiate(mob1, new Vector2 (10f, randomY), Quaternion.identity);
            mob.transform.parent = this.transform;
            
            yield return new WaitForSeconds(mobSpawnDelay);
        }
    }

    public void buttonhuj()
    {
        CreateWave(player.gameLvl);
    }

    public string CreateWave(int gameLvl)
    {
        //0 - slime
        //1 - fly
        string wave = "";
        int totalpower = gameLvl * 10;

        //fly
        int counrt = 0;
        while (totalpower > 1 || counrt>=100) { 
            for (int i = mobs.Length-1; i >= 0; i--) { 
                int mobcount = (totalpower - 20*i)/ mobs[i].power;
                //максимум * моба могут заспавниться под ряд
                Debug.Log("Mob[" + i + "] before row nerf counts: " + mobcount);
                mobcount = (mobcount > maximalMobCountInRow) ? maximalMobCountInRow : mobcount;


                Debug.Log("Mob["+i+"] counts: "+mobcount);

                totalpower -= mobs[i].power* mobcount;
                wave += CreateWaveString(mobcount, i);
            }
            Debug.Log("countr: "+counrt);
            counrt++;
        }
        Debug.Log("UUUUUUUUU Wve: " + wave);
        return wave;
    }

    public string CreateWaveString(int count, int num)
    {
        string _wave = "";
        for (int i = 0; i < count; i++)
        {
            _wave +=num;
        }
        return _wave;
    }

}
