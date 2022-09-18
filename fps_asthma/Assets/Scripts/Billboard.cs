using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    private SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.flipX = true; //reflects the object (flips)
     
    }

    // Update is called once per frame
    void Update()
    {
        //always looks up to the player
        transform.LookAt(PlayerMovement.instance.transform.position, -Vector3.forward); //up value
    }
}
