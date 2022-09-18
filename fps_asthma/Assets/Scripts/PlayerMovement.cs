using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb; //reference to player's rigid body
    public float moveSpeed; //player movement speed
    private Vector2 MoveInput; //(x,y)
    private Vector2 MouseInput; //(x,y)

    public float mouseSensitivity = 1f;

    //reference to camera to look up and down - reference to camera object
    public Camera viewCam;

    //reference to the smoke object/ammo
    public GameObject smokeImpact;

    //track the amount of ammo player has 
    public int currentAmmo;

    //related to billboarding
    public static PlayerMovement instance; //object referece true for any version of player script

    //health variables
    public int currentHealth; //player's current health
    public int maxHealth = 100; //player's max health at start
    public GameObject deadScreen; //screen pops up when you die
    private bool died; //if player is dead or not

    //updating text value
    public Text health;
    public Text Ammo;

    //animator
    //public Animator anim; //default animation system
    // Start is called before the first frame update

    public void Awake()
    {
        instance = this;
    }
    void Start()
    {
        //when start game - player has max health
        currentHealth = maxHealth;
        health.text = currentHealth.ToString() + "%";
        Ammo.text = currentAmmo.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (!died) //if not died can move
        {
            //player movement
            MoveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            Vector3 moveHorizontal = transform.up * -MoveInput.x; //moves opposite direction
            Vector3 moveVertical = transform.right * MoveInput.y;
            rb.velocity = (moveHorizontal + moveVertical) * moveSpeed;

            //player view control
            //mouse x = how much the mouse moved side to side from last frame index
            //mouse y = how much the mouse moved front to back from last frame index
            MouseInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y")) * mouseSensitivity;

            //apply mouseInput to player - how fast we rotate 
            //quaternion - 4
            //quaternion stores rotation (x,y,z,w) w - rotation
            //Euler converts Quaternion into Vector3 value
            //(x,y,z-mouseInput.x)
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z - MouseInput.x);

            //looking up and down
            viewCam.transform.localRotation = Quaternion.Euler(viewCam.transform.localRotation.eulerAngles + new Vector3(0f, MouseInput.y, 0f));


            //shooting
            if (Input.GetMouseButtonDown(0)) //clicks left button every time we click
            {
                if (currentAmmo > 0) //can only shoot if ammo is greater than 0
                {
                    //create array cast - draw line in centre of the camera - if it hits/interacts with an object it will do something
                    //something - being the effect
                    //0 - bottom, 1 - top, 0.5f - middle
                    Ray ray = viewCam.ViewportPointToRay(new Vector3(.5f, .5f, 0f)); //centre of camera x,y,z
                    RaycastHit hit; //detect if hit happens
                    if (Physics.Raycast(ray, out hit)) //if hits anything - go to if statement
                    {
                        //Debug.Log("I'm looking at " + hit.transform.name);
                        Instantiate(smokeImpact, hit.point, transform.rotation);

                        //check if it hits enemy
                        if (hit.transform.tag == "enemy")
                        {
                            //get component of enemy controller and decrease damage
                            hit.transform.parent.GetComponent<enemyMovement>().TakeDamage();
                        }

                        AudioController.instance.PlayGunShot();
                    }
                    else
                    {
                        Debug.Log("Im looking at nothing!");
                    }
                    //each time we click mouse button - ammo decreases
                    currentAmmo--; //decrement
                    UpdateAmmoUI();

                }

            }
            //if (MoveInput != Vector2.zero) //playing is hitting keyboard - moving
            //{
            //    anim.SetBool("IsMoving", true);

            //}
            //else
            //{
            //    anim.SetBool("IsMoving", false); //player not touching keyboard - not moving - no animation
            //}
        }


    }
    public void takeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        if(currentHealth <= 0)
        {
            deadScreen.SetActive(true); //game over screen
            died = true; //player has died
            currentHealth = 0;
        }
        //update healthtext
        health.text = currentHealth.ToString() + "%";

        AudioController.instance.PlayPlayerHurt();

    }

    //heal player - pick up healing item
    public void healAmount(int healValue)
    {
        currentHealth += healValue;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        health.text = currentHealth.ToString() + "%";
    }

    public void UpdateAmmoUI()
    {
        Ammo.text = currentAmmo.ToString();
    }
}
