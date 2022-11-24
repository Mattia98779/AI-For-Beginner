using UnityEngine;
using UnityEngine.AI;

public class FollowPath : MonoBehaviour {

    
    public GameObject wpManager;
    GameObject[] wps;
    private UnityEngine.AI.NavMeshAgent agent;
    
    

    // Use this for initialization
    void Start() {

        // Get hold of wpManager and Graph scripts
        wps = wpManager.GetComponent<WPManager>().waypoints;
        agent = this.GetComponent<NavMeshAgent>();
    }

    public void GoToHeli()
    {
        agent.SetDestination(wps[4].transform.position);
    }

    public void GoToRuin() {
        agent.SetDestination(wps[0].transform.position);
    }

    public void GoBehindHeli() {
        
    }

    // Update is called once per frame
    void LateUpdate() {
        
    }
}