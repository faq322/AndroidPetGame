using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    //public Transform bullet;
    public Rigidbody2D rb;
    [SerializeField]
    public int bulletSpeed = 6;
    [SerializeField]
    public int bulletDamage = 15;
    //для отдачи и разброса
    [SerializeField]
    public int bulletPower = 5;


    void Start()
    {
        rb.velocity = transform.right * bulletSpeed;
       
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        //Debug.Log(hitInfo.name);
        MoveForward enemy = hitInfo.GetComponent<MoveForward>();
        if (enemy != null)
        {
            enemy.TakeDamage(bulletDamage);
        }
        Destroy(gameObject);
    }
    void Update()
    {
        OutOfMap();
    }
    void OutOfMap()
    {

        if (transform.position.x > 11f)
        {
            Destroy(gameObject);
        }
        if (transform.position.y > 10f)
        {
            Destroy(gameObject);
        }
        if (transform.position.y < -2.7f)
        {
            Destroy(gameObject);
        }
    }
}

