using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Bot : MonoBehaviour
{
    NavMeshAgent agent;

    public GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
    }

    void Seek(Vector3 location)
    {
        agent.SetDestination(location);
    }

    void Flee(Vector3 location)
    {
        Vector3 fleeVector = location - this.transform.position;
        agent.SetDestination(this.transform.position - fleeVector);
    }

    void Pursue()
    {
        Vector3 targetDir = target.transform.position - this.transform.position;

        float relativeHeading =
            Vector3.Angle(this.transform.forward, this.transform.TransformVector(target.transform.forward));
        float toTarget = Vector3.Angle(this.transform.forward, this.transform.TransformVector(targetDir));

        if ((toTarget > 90 && relativeHeading <20 ) || target.GetComponent<Drive>().currentSpeed<0.01f)
        {
            Seek(target.transform.position);
            return;
        }
        float lookAhead = targetDir.magnitude / (agent.speed + target.GetComponent<Drive>().currentSpeed);
        Seek(target.transform.position + target.transform.forward*lookAhead);
    }

    void Flee()
    {
        Vector3 targetDir = target.transform.position - this.transform.position;
        float lookAhead = targetDir.magnitude / (agent.speed + target.GetComponent<Drive>().currentSpeed);
        Flee(target.transform.position + target.transform.forward*lookAhead);
    }

    private Vector3 wanderTarget = Vector3.zero;
    void Wander()
    {
        float wanderRadius = 10;
        float wanderDistance = 20;
        float wanderJitter = 1;

        wanderTarget += new Vector3(Random.Range(-1.0f, 1.0f) * wanderJitter, 0, Random.Range(-1.0f, 1.0f) * wanderJitter);
        
        wanderTarget.Normalize();
        wanderTarget *= wanderRadius;

        Vector3 targetLocal = wanderTarget + new Vector3(0, 0, wanderDistance);
        Vector3 targetWorld = this.gameObject.transform.InverseTransformVector(targetLocal);
        
        Seek(targetWorld);

    }

    void hide()
    {
        float dist = Mathf.Infinity;
        Vector3 chosenSpot = Vector3.zero;

        for (int i = 0; i < World.Istance.GetHidingSpots().Length; i++)
        {
            Vector3 hideDir = World.Istance.GetHidingSpots()[i].transform.position - target.transform.position;
            Vector3 hidePos = World.Istance.GetHidingSpots()[i].transform.position + hideDir.normalized*5;

            if (Vector3.Distance(this.transform.position, hidePos)<dist)
            {
                chosenSpot = hidePos;
                dist = Vector3.Distance(this.transform.position, hidePos);
            }
        }

        Seek(chosenSpot);
    }
    
    void CleverHide()
    {
        float dist = Mathf.Infinity;
        Vector3 chosenSpot = Vector3.zero;
        Vector3 chosenDir = Vector3.zero;
        GameObject chosenGO = World.Istance.GetHidingSpots()[0];

        for (int i = 0; i < World.Istance.GetHidingSpots().Length; i++)
        {
            Vector3 hideDir = World.Istance.GetHidingSpots()[i].transform.position - target.transform.position;
            Vector3 hidePos = World.Istance.GetHidingSpots()[i].transform.position + hideDir.normalized*5;

            if (Vector3.Distance(this.transform.position, hidePos)<dist)
            {
                chosenSpot = hidePos;
                chosenDir = hideDir;
                chosenGO = World.Istance.GetHidingSpots()[i];
                dist = Vector3.Distance(this.transform.position, hidePos);
                
            }
        }

        Collider hideCol = chosenGO.GetComponent<Collider>();
        Ray backRay = new Ray(chosenSpot, -chosenDir.normalized);
        RaycastHit info;
        float distance = 100;
        hideCol.Raycast(backRay, out info, distance);
        
        Seek(info.point + chosenDir.normalized * 2);
    }

    bool canSeeTarget()
    {
        RaycastHit raycastInfo;
        Vector3 rayToTarget = target.transform.position - this.transform.position;
        if (Physics.Raycast(this.transform.position, rayToTarget, out raycastInfo))
        {
            if (raycastInfo.transform.gameObject.tag == "cop")
            {
                return true;
            }
        }

        return false;
    }

    bool TargetCanSeeMe()
    {
        Vector3 toAgent = this.transform.position - target.transform.position;
        float lookingAngle = Vector3.Angle(target.transform.forward, toAgent);
        if (lookingAngle<60)
        {
            return true;
        }

        return false;
    }

    private bool coolDown = false;

    void BehaviourCollDown()
    {
        coolDown = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!coolDown)
            {
                if (Vector3.Distance(this.transform.position, target.transform.position) > 10 && !coolDown)
                {
                    Wander();
                }else if (canSeeTarget() && TargetCanSeeMe())
                {
                    CleverHide();
                    coolDown = true;
                    Invoke("BehaviourCollDown",5);
                }
                else
                {
                    Pursue();
                } 
                
            }

    }
}
