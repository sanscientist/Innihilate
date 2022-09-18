using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public int damageAmount;

    //moving bullet
    public float bulletSpeed;
    public Rigidbody2D rbBullet; //add physics to bullet - does not crash into walls etc
    private Vector3 direction; //move towards player (know where player is)
    // Start is called before the first frame update
    void Start()
    {
        direction = PlayerMovement.instance.transform.position - transform.position;
        direction.Normalize();
        direction = direction * bulletSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        rbBullet.velocity = direction * bulletSpeed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            PlayerMovement.instance.takeDamage(damageAmount);

            Destroy(gameObject);
        }
    }

}
