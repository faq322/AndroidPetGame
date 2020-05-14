using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //Для работы со сценами
using UnityEngine.UI;
using Random = System.Random;

public class MoveForward : MonoBehaviour
{
    private bool canAttack = false;

    [Header("Mob parameters")]
    [SerializeField]
    private float mobSpeed;
    [SerializeField]
    public int mobHP, currentMobHP;
    [SerializeField]
    private int damage;
    [SerializeField]
    private int exp;
    [SerializeField]
    private int moneyReward;


    public float attackDistance;
    public float randomDistance;

    //PlayerStats player;
    public GameObject playerObject;
    public PlayerStats player;// = playerObject.GetComponent<PlayerStats>();

    public GameObject mobSpawnerObject;
    public SpawnMob mobSpawner;

    [Header("Unity Stuff")]
    public Image healthBar;

    void Start()
    {
        playerObject = GameObject.Find("Player");
        player = playerObject.GetComponent<PlayerStats>();

        mobSpawnerObject = GameObject.Find("MobSpawner");
        mobSpawner = mobSpawnerObject.GetComponent<SpawnMob>();

        //увеличиваем общее число здоровья мобов
        mobSpawner.PlusTotalMob(mobHP);
        Debug.Log("Total mob HP = "+mobSpawner.CurrentTotalMobHP());

        Random rand = new Random();
        randomDistance = (float)rand.Next(0,20)/100;
        Debug.Log("Random distance: " + randomDistance);
        attackDistance += randomDistance;


    }

    void Update ()
    {
        if (!canAttack) { 
            if (transform.position.x>=attackDistance)
            {
                transform.position -= new Vector3(mobSpeed * Time.deltaTime, 0, 0);
            } else
            {
                canAttack = true;
                StartCoroutine(Attack());
            }
        }
        if (!PlayerStats.inGame)
        {
            exp = 0;
            Die();
        }
    }

    IEnumerator Attack()
    {
        while (PlayerStats.inGame)
        {
            //анимация стоит -<
            yield return new WaitForSeconds(0.1f);

            //анимация удара -<
            yield return new WaitForSeconds(2.7f);
            Debug.Log("MOB ATTACK: ");
            //PlayerStats.HP -= damage;

            //PlayerStats.TakeDamage(damage);
            player.PlusHP(-damage);
        }
    }


    public void TakeDamage(int damage)
    {
        currentMobHP -= damage;

        //Healthbar
        var div = (float)currentMobHP / (float)mobHP;
        healthBar.fillAmount = div;

        if (currentMobHP <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);

        player.AddPlayerExp(exp);
        player.pocket.AddMoney(moneyReward);
        

        mobSpawner.MinusMob();
        mobSpawner.MinusTotalMobHP(mobHP); 
    }

    


}
