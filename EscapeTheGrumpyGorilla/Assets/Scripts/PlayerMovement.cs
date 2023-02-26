using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
  public CharacterController controller;
  public float speed = 12f;
  bool hasBanana;
  public bool peel { get; private set; }
  public GameObject bananaHud;

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis ("Horizontal");
        float z = Input.GetAxis ("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);

        if(hasBanana && Input.GetKeyDown(KeyCode.E))
        {
            speed = 24f;
            hasBanana = false;
            Invoke(nameof(SlowDown), 8f);
            peel = true;
        }
    }
    public void GetBanana()
    {
        hasBanana = true;
        bananaHud.SetActive(true);
    }
    private void SlowDown()
    {
        speed = 12f;
    }
    public void TEST(){
        bananaHud.SetActive(false);
    }
}