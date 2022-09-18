using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMovement : MonoBehaviour
{
    public int health; //enemies health

    //for explosion effect
    public GameObject explosion;

    //enemy movement
    public float playerRange; //how close player needs to be for enemy to move
    //move enemy using physics - avoid moving through walls/objects
    public Rigidbody2D enemyRB;
    //enemy speed
    public float moveSpeed;

    //enemy shoots out bullets
    public bool shouldShoot;
    public float fireRate = .5f; //shotting rate speed
    public float shootCounter; //delay between bullet
    public GameObject bullet; //which enemyBullet is being shot
    public Transform firePoint; //where we firing from
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //compares the distance between enemy position and player's position
        if(Vector3.Distance(transform.position, PlayerMovement.instance.transform.position) < playerRange)
        {
            Vector3 PlayerDirection = PlayerMovement.instance.transform.position - transform.position;
            enemyRB.velocity = PlayerDirection.normalized * moveSpeed;

            if (shouldShoot)
            {
                shootCounter -= Time.deltaTime; //counting down all the time
                if(shootCounter <= 0)
                {
                    //create new bullet
                    Instantiate(bullet, firePoint.position, firePoint.rotation);
                    shootCounter = fireRate; //reset the shootcounter
                }
            }
        }
        else
        {
            enemyRB.velocity = Vector2.zero;
        }
    }

    public void TakeDamage()
    {
        health--; //decrement health
        if(health <= 0)
        {
            Destroy(gameObject);
            Instantiate(explosion, transform.position, transform.rotation);

            AudioController.instance.PlayEnemyDeath();
        }
        else
        {
            AudioController.instance.PlayEnemyShot();
        }
    }
}
