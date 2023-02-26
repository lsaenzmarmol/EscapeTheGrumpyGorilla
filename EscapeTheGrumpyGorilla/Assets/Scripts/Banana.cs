using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Banana : MonoBehaviour
{
    // Start is called before the first frame update
    public PlayerMovement pm;

    private void OnCollisionEnter(Collision other) {
            
            Debug.Log("Banana");
            pm.GetBanana();
            Destroy(this.gameObject);
        
    }
}
