using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickUp : MonoBehaviour
{
    //how much the ammo will be worth
    public int ammoAmount = 25;
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
        if(other.tag == "Player") //checks if the collision from tagged player
        {
            //tell playerMovement 
            PlayerMovement.instance.currentAmmo += ammoAmount; //increments the ammoAmount
            PlayerMovement.instance.UpdateAmmoUI();

            AudioController.instance.PlayAmmoPickup();
            Destroy(gameObject);
        }
    }
}
