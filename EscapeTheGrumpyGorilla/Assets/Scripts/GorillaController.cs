using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GorillaController : MonoBehaviour
{
    public NavMeshAgent agent;
    public AudioManager am;
    public GameObject playerObj, loseObj;
    public Transform[] protectPositions;
    int posSpot, timer, spinTimer, DazedTimer, spotTemp;
    public int playerThreshold, nanaThreshold;
    float nanaDistance, playerDistance, nanaTemp;
    public float normalSpeed = 3.5f, chaseSpeed = 7f;
    
    Animator anim;

    GameObject nanaFinal;

    

    public int spinDuration, dazedDuration;

    enum BehaviorState {Patrol, Spin, Protect, Chase, Dazed};
    BehaviorState rillaState;
    
    // Start is called before the first frame update
    void Start()
    {
        agent.speed = normalSpeed;
        rillaState = BehaviorState.Protect;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.LookAt(protectPositions[1]);
        
        ComputeState();
        if(Input.GetKeyDown(KeyCode.K))
        {
            Debug.Log(rillaState.ToString());
        }
    }

    public void ComputeState()
    {

        
        //check distances
        nanaDistance = 100;
        GameObject[] nanaPeels = GameObject.FindGameObjectsWithTag("Banana");
        if(nanaPeels!=null)
        {
            foreach (GameObject nanaPeel in nanaPeels)
            {
                nanaTemp = Vector3.Distance(nanaPeel.transform.position, transform.position); 
                if(nanaTemp < nanaDistance)
                {
                    nanaDistance = nanaTemp;
                    nanaFinal = nanaPeel;
                }
            }
            
        }
            
        if(playerObj != null)
            playerDistance = Vector3.Distance(playerObj.transform.position, transform.position);
        //check spin state
        //Debug.Log(nanaDistance); 
        GameObject keyObj = GameObject.FindGameObjectWithTag("Key");
        //Debug.Log(keyObj);
        if(keyObj==null)
        {
            rillaState = BehaviorState.Chase;
        } 
        if(rillaState == BehaviorState.Spin)
        {
            anim.SetTrigger("Spin");
            spinTimer++;

            if(spinTimer > spinDuration)
            {
                rillaState = BehaviorState.Protect;
            }
            //dont move, just spin for a second
        }

        //check dazed state
        if(rillaState == BehaviorState.Dazed)
        {
            DazedTimer++;

            if(DazedTimer > dazedDuration)
            {
                rillaState = BehaviorState.Protect;
            }
        }



        //check the distance to the patrol object
        
        if(rillaState == BehaviorState.Patrol)
        {
            if(nanaFinal!=null)
                agent.SetDestination(nanaFinal.transform.position);
        }
    
        if(rillaState == BehaviorState.Protect)
        {        

        agent.speed = normalSpeed;
        //other state checks   
        //check chase        
      
        if(playerDistance < playerThreshold)
        {
            rillaState = BehaviorState.Chase;
        }
        else 
        {
            agent.speed = normalSpeed;
        }
        if(nanaDistance < nanaThreshold)
        {
            rillaState = BehaviorState.Patrol;
        }
        playerDistance = Vector3.Distance(playerObj.transform.position, transform.position);
        if(playerDistance < playerThreshold)
        {
            rillaState = BehaviorState.Chase;
        }
            ComputeProtect();
            agent.SetDestination(protectPositions[posSpot].position);
        }
        if(rillaState == BehaviorState.Chase)
        {
            am.PlayExplosion();
            agent.speed = chaseSpeed;
            agent.SetDestination(playerObj.transform.position);
            if(playerDistance > playerThreshold)
            {
                rillaState = BehaviorState.Protect;
            }
            //chase player faster than normal
        }

 

        
    }

    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "Peeled")
        {
            am.PlaySlip();
            Debug.Log("GotPeeled");
            spinTimer = 0;
            rillaState = BehaviorState.Spin;
            nanaDistance = 50;
            Destroy(other.gameObject);
        }
        if(other.gameObject.tag == "Banana")
        {
            DazedTimer = 0;
            rillaState = BehaviorState.Dazed;
            nanaDistance = 50;
            Destroy(other.gameObject);
        }
        if(other.gameObject.tag == "Player" && !PauseMenu.gameOver)
        {
            PauseMenu.GameOver();
            loseObj.SetActive(true);
            am.DestroyMusic();
        }
    }
    private void ComputeProtect()
    {
        timer++;
        if(timer > 2400)
        {
            timer = 0;
            if(posSpot<(protectPositions.Length - 1))
            {
                posSpot++;
            }
            else 
            {
                posSpot = 0;
            }
        }
    }
}
