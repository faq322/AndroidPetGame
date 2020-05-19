using System.Collections;
using UnityEngine;
using Random = System.Random;

public class SpawnMob : MonoBehaviour
{
    public GameObject playerObject;
    public PlayerStats player;// = playerObject.GetComponent<PlayerStats>();
    public Transform Spawner;
    public GameObject winLose;


    //Slime
    [System.Serializable]
    public class Mob {
        public string name;
        public GameObject mob1;
        public int power;
        public float spawnHeight;
        public int spawnCount;
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
       // mobcounter++;
    }

    public void MinusTotalMobHP(int HP)
    {
        currentTotalMobHP -= HP;
    }

    public void MinusMob()
    {
        if (++deadmobcounter >= mobcounter && PlayerStats.inGame)
        {
            //Wictory
            PlayerStats.win = true;
            PlayerStats.inGame = false;
            Debug.Log("You win!");
            winLose.SetActive(true);
            // DBgetset.StartCoroutine(SaveResults());
            StartCoroutine(player.FinishWin());
        }
        Debug.Log("MOB ID DEAD. total mobs: " + mobcounter + "; dead mobs: " + deadmobcounter+". mobs[].length = "+mobs.Length);
        
    }

    void Start ()
    {

        // playerObject = GameObject.Find("Player");
        //player = playerObject.GetComponent<PlayerStats>();

        winLose.SetActive(false);

        StartCoroutine(Spawn());
    }

    IEnumerator Spawn ()
    {
        Random rand = new Random();
        yield return new WaitForSeconds(2);
        string wave = CreateWave(player.gameLvl);
        Debug.Log("player game level:    " + player.gameLvl);
        
        mobsLimit = player.gameLvl;
        Debug.Log("Lets GO!");
        PlayerStats.start = true;

        foreach(char c_mobNum in wave)
        {
            int i_mobNum = c_mobNum - '0';

            //создание моба

            //место(высота) спавна
            float randomY = (float)rand.Next(-5, 3) / 10;

            Vector3 spawnPoint = Spawner.transform.position;
            spawnPoint.y += mobs[i_mobNum].spawnHeight + randomY;
            Debug.Log(spawnPoint);

            for (int j = 0; j < mobs[i_mobNum].spawnCount; j++) 
            { 
             GameObject mob = Instantiate(mobs[i_mobNum].mob1, spawnPoint, Quaternion.identity);
                //закидываем мобов в папку енеми
                if (mobs[i_mobNum].spawnCount > 1)
                {
                    yield return new WaitForSeconds(mobSpawnDelay / 2);
                    randomY = (float)rand.Next(-5, 5) / 10;
                    spawnPoint.y += randomY;
                }
                mob.GetComponent<SpriteRenderer>().sortingOrder+=(int)randomY*10;
                mob.transform.parent = this.transform;
            }


            mobcounter += mobs[i_mobNum].spawnCount - 1;
            yield return new WaitForSeconds(mobSpawnDelay);
            if (!PlayerStats.inGame) break;
        }
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

                mobcount = (mobcount > maximalMobCountInRow) ? maximalMobCountInRow : mobcount;

                totalpower -= mobs[i].power* mobcount;
                wave += CreateWaveString(mobcount, i);
            }
            Debug.Log("countr: "+counrt);
            counrt++;
        }
        Debug.Log("UUUUUUUUU Wve: " + wave);


        mobcounter = wave.Length;
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
