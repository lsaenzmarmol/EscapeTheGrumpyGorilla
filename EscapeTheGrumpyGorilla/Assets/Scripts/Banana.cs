using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Banana : MonoBehaviour
{
    // Start is called before the first frame update
    public PlayerMovement pm;
    private void Start() {
        pm = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    private void OnCollisionEnter(Collision other) {
            if(other.gameObject.tag == "Player")
            {
                pm.GetBanana();
                Destroy(this.gameObject);
            }
            Debug.Log("Banana");
            
            
        
    }
}
