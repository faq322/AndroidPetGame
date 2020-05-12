using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guns : MonoBehaviour
{
    public string name;
    public GameObject bullet;

    public float reload;
    public int maxBulletCount;
    public int currentBulletCount;

    public BulletMove bulletMove;

    private int speed;
    public int damage;
    public int power;

/*
    void Start()
    {
        currentBulletCount = maxBulletCount;
    }

    IEnumerator Shot()
    {
        anim = GetComponent<Animator>();
        anim.SetInteger("State", 2);
        var rotation = Quaternion.Euler(0, 0, getFirePointAngle());
        Instantiate(bullet, firePoint.position, rotation);
        yield return new WaitForSeconds(0.3f);
        anim.SetInteger("State", 1);
    }

    public void Reload()
    {

    }



    public void Reload()
    {

    }
*/

}
