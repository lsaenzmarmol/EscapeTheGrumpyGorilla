using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
  public CharacterController controller;
  
  public float speed = 12f;
  public bool hasBanana;
  public bool peel { get; private set; }
  public GameObject peeledHud, normalHud;
  public LayerMask gorillaLayer;
  public GameObject doorObj, winObj;
  public Rigidbody rb;
  public AudioManager am;

    // Update is called once per frame
    void Update()
    {
        

        float x = Input.GetAxis ("Horizontal");
        float z = Input.GetAxis ("Vertical");
        if(!PauseMenu.isPaused && !PauseMenu.gameOver)
        {  
            CheckInputs();
            
            Vector3 move = transform.right * x + transform.forward * z;
            controller.Move(move * speed * Time.deltaTime);
        }
        if(PauseMenu.gameOver)
        {
            am.TurnOffMusic();
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

        }
            

    }
    public void GetBanana()
    {
        hasBanana = true;
        normalHud.SetActive(true);
    }
    private void SlowDown()
    {
        speed = 12f;
    }
    public void HideBanana(){
        normalHud.SetActive(false);
        peeledHud.SetActive(false);
        hasBanana = false;
    }

    private void CheckInputs()
    {
        if(hasBanana && Input.GetKeyDown(KeyCode.E))
        {
            normalHud.SetActive(false);
            peeledHud.SetActive(true);
            speed = 24f;
            hasBanana = false;
            Invoke(nameof(SlowDown), 8f);
            peel = true;
        }

    }
    public void SetPeel(bool b)
    {
        peel = b;
    }
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Key")
        {
            doorObj.SetActive(false);
            Destroy(other.gameObject);
        }
        if(other.gameObject.tag == "Win")
        {
            PauseMenu.GameOver();
            
            winObj.SetActive(true);
        }
    }
}