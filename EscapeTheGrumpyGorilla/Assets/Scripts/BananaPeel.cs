using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BananaPeel : MonoBehaviour
{
    public GameObject projectile;
    public GameObject normalNanaObj;
    public float launchVelocity = 200f;
    public PlayerMovement pm;
    bool test;

    // Update is called once per frame
    void Update()
    {
        if(!PauseMenu.isPaused && !PauseMenu.gameOver)
            CheckInputs();
    }
    void CheckInputs()
    {
        if(Input.GetKeyDown(KeyCode.R) && pm.peel)
        {
            pm.SetPeel(true);
            pm.HideBanana();
            GameObject ball = Instantiate(projectile, transform.position,  transform.rotation);
            ball.GetComponent<Rigidbody>().AddRelativeForce((launchVelocity * Vector3.forward) + Vector3.up * 100);
        }
        if(Input.GetKeyDown(KeyCode.R) && pm.hasBanana && !pm.peel)
        {
            pm.HideBanana();
            GameObject ball = Instantiate(normalNanaObj, transform.position,  transform.rotation);
            ball.GetComponent<Rigidbody>().AddRelativeForce(launchVelocity * Vector3.forward + Vector3.up * 50);
        }
    }
}
