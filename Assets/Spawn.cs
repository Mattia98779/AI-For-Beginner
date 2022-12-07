using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject patienPrefab;

    public int nPatient;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < nPatient; i++)
        {
            Instantiate(patienPrefab, this.transform.position, Quaternion.identity);
        }

        Invoke("SpawnPatient", 5);

    }

    void SpawnPatient()
    {
        Instantiate(patienPrefab, this.transform.position, Quaternion.identity);
        Invoke("SpawnPatient", Random.Range(2,10));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
