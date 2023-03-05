using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GorillaController : MonoBehaviour
{
    public NavMeshAgent agent;
    public AudioManager am;
    public GameObject playerObj, loseObj, keyObj;
    public Transform[] protectPositions;
    int posSpot, timer, spinTimer, DazedTimer, tempDistance;
    public int playerThreshold, nanaThreshold;
    float nanaDistance, playerDistance;
    public float normalSpeed = 3.5f, chaseSpeed = 7f;
    GameObject closestNana;
    public Animator anim;

    public int spinDuration, dazedDuration;

    enum BehaviorState {Patrol, Spin, Protect, Chase, Dazed};
    BehaviorState rillaState;

    // Start is called before the first frame update
    void Start()
    {
        agent.speed = normalSpeed;
        rillaState = BehaviorState.Protect;
    }

    // Update is called once per frame
    void Update()
    {
        ComputeState();
        if(Input.GetKeyDown(KeyCode.K))
        {
            Debug.Log(rillaState.ToString());
        }
    }

    public void ComputeState()
    {

        if(keyObj == null)
        {
            rillaState = BehaviorState.Chase;
        }
        //check distances
        GameObject[] nanaPeels = GameObject.FindGameObjectsWithTag("Banana");
        float previous = 100;
        if(nanaPeels!=null)
        {
            foreach(GameObject nanaPeel in nanaPeels)
            {
                nanaDistance = Vector3.Distance(nanaPeel.transform.position, transform.position);
                if(nanaDistance < previous)
                {
                    closestNana = nanaPeel;
                    previous = nanaDistance;
                }
                
            }
        }
             
        if(playerObj != null)
            playerDistance = Vector3.Distance(playerObj.transform.position, transform.position);
        //check spin state
        if(rillaState == BehaviorState.Spin)
        {
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
            if(closestNana!=null)
                agent.SetDestination(closestNana.transform.position);
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
            anim.SetTrigger("Spin");
            am.PlaySlip();
            Debug.Log("GotPeeled");
            spinTimer = 0;
            rillaState = BehaviorState.Spin;
            nanaDistance = 100;
            Destroy(other.gameObject);
        }
        if(other.gameObject.tag == "Banana")
        {
            DazedTimer = 0;
            rillaState = BehaviorState.Dazed;
            nanaDistance = 100;
            Destroy(other.gameObject);
        }
        if(other.gameObject.tag == "Player" && !PauseMenu.gameOver)
        {
            PauseMenu.GameOver();
            loseObj.SetActive(true);
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
