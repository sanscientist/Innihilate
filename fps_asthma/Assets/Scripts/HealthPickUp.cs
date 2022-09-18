using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickUp : MonoBehaviour
{
    //how much the ammo will be worth
    public int healthAmount = 25;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    //if player hits ammoBox trigger collider
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player") //checks if the collision from tagged player
        {
            //tell playerMovement 
            PlayerMovement.instance.healAmount(healthAmount);
            AudioController.instance.PlayHealthPickup();
            Destroy(gameObject);

            
        }
    }
}
