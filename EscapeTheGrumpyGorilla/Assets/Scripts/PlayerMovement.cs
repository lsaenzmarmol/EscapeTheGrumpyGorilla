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

    // Update is called once per frame
    void Update()
    {
        

        
        if(!PauseMenu.isPaused && !PauseMenu.gameOver)
        {
            Debug.Log(PauseMenu.gameOver);
            CheckInputs();
            float x = Input.GetAxis ("Horizontal");
            float z = Input.GetAxis ("Vertical");
            Vector3 move = transform.right * x + transform.forward * z;
            controller.Move(move * speed * Time.deltaTime);
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
            Debug.Log(PauseMenu.gameOver);
            winObj.SetActive(true);
        }
    }
}