using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowWP : MonoBehaviour
{
    public GameObject[] waypoints;
    private int currentWP;
    public float speed = 20;
    public float rotSpeed = 10f;
    public float lookAhead = 10;

    private GameObject tracker;
    // Start is called before the first frame update
    void Start()
    {
        tracker = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        DestroyImmediate(tracker.GetComponent<Collider>());
        tracker.transform.position = this.transform.position;
        tracker.transform.rotation = this.transform.rotation;
        tracker.GetComponent<MeshRenderer>().enabled=false;
    }

    void ProgressTracker()
    {
        if (Vector3.Distance(tracker.transform.position, this.transform.position)>lookAhead)
        {
            return;
        }
        if (Vector3.Distance(tracker.transform.position, waypoints[currentWP].transform.position) < 1)
        {
            currentWP++;
        }

        if (currentWP >= waypoints.Length)
        {
            currentWP = 0;
        }
        
        tracker.transform.LookAt(waypoints[currentWP].transform.position);
        tracker.transform.Translate(0,0,Time.deltaTime*(speed+20));
    }

    // Update is called once per frame
    void Update()
    {
        ProgressTracker();
        Quaternion lookatWP = Quaternion.LookRotation(tracker.transform.position-this.transform.position);
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, lookatWP, Time.deltaTime*rotSpeed);
        this.transform.Translate(0,0,speed*Time.deltaTime);
    }
}
