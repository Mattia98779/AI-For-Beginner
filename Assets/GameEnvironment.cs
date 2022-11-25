using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public sealed class GameEnvironment : MonoBehaviour
{
    private static GameEnvironment istance;

    private List<GameObject> checkpoints = new List<GameObject>();
    
    public List<GameObject> CheckPoints
    {
        get { return checkpoints; }
    }

    public static GameEnvironment Singleton
    {
        get
        {
            if (istance==null)
            {
                istance = new GameEnvironment();
                istance.CheckPoints.AddRange(GameObject.FindGameObjectsWithTag("Checkpoint"));
                istance.checkpoints = istance.checkpoints.OrderBy(waypoint => waypoint.name).ToList();
            }

            return istance;
        }
    }
}
