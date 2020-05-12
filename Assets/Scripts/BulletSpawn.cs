using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class BulletSpawn : MonoBehaviour
{
    public Transform firePoint;
    public Transform target;
    public Animator anim;

    public GameObject bullet;
    public BulletMove bulletMove;


    private bool startMobSpawn;

    

    void Start()
    {
        StartCoroutine(Spawn());
        //.
    }

    IEnumerator Spawn()
    {
        while (PlayerStats.inGame)
        {
            yield return new WaitForSeconds(0.7f);
            startMobSpawn = PlayerStats.start;
            if (startMobSpawn) { 
            Debug.Log(PlayerStats.start);
                anim = GetComponent<Animator>();
                anim.SetInteger("State", 2);
                yield return new WaitForSeconds(0.1f);
                var rotation = Quaternion.Euler(0, 0, getFirePointAngle());
                Instantiate(bullet, firePoint.position, rotation);
                yield return new WaitForSeconds(0.3f);
                anim.SetInteger("State", 1);
            //Debug.Log("Animation : " + anim.GetInteger("State"));
            }
        
        }

    }

    float getFirePointAngle()
    {
        var x = target.position.x - firePoint.position.x;
        var y = target.position.y - firePoint.position.y;
        //чуть увеличиваем угол с расчетом на дальность прицела
        var bonusAngleFromDistance = x / ((float)bulletMove.bulletSpeed-3f)*6f;
        //угол для разброса в зависимости от силы отдачи
        Random rand = new Random();
        //var randomAngle = rand.Next(-1*BulletMove.bulletPower,BulletMove.bulletPower);
        var randomAngle = 0;
        float angle = (float)System.Math.Atan(y / x) * 180f / 3.14f + bonusAngleFromDistance + randomAngle;
        Debug.Log("Bullet info: x="+x+" y="+y+" angle="+ angle+" +bonus="+ bonusAngleFromDistance);
        return angle;
    }
}
