using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public static AudioController instance;

    public AudioSource ammo, enemyDeath, enemyShot, gunShot, health, playerHurt; //all audio sound effects

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayAmmoPickup()
    {
        ammo.Stop(); //stop sound effect
        ammo.Play(); //resets and play sound effect from beginning
    }

    public void PlayEnemyDeath()
    {
        enemyDeath.Stop(); //stop sound effect
        enemyDeath.Play(); //resets and play sound effect from beginning
    }

    public void PlayEnemyShot()
    {
        enemyShot.Stop(); //stop sound effect
        enemyShot.Play(); //resets and play sound effect from beginning
    }

    public void PlayGunShot()
    {
        gunShot.Stop(); //stop sound effect
        gunShot.Play(); //resets and play sound effect from beginning
    }

    public void PlayHealthPickup()
    {
        health.Stop(); //stop sound effect
        health.Play(); //resets and play sound effect from beginning
    }

    public void PlayPlayerHurt()
    {
        playerHurt.Stop(); //stop sound effect
        playerHurt.Play(); //resets and play sound effect from beginning
    }
}
