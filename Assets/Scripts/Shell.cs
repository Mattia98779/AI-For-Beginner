using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour
{
    public GameObject explosion;

    private Rigidbody rb;
    /*private float mass = 10;
    private float force = 30;
    private float acceleration;
    private float speedZ;

    private float speedY;
    private float gravity = -9.8f;
    private float gAccel;*/

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "tank")
        {
            GameObject exp = Instantiate(explosion, this.transform.position, Quaternion.identity);
            Destroy(exp, 0.5f);
            Destroy(this.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        this.transform.forward = rb.velocity;
        /*
        acceleration = force / mass;
        speedZ += acceleration * Time.deltaTime;
        this.transform.Translate(0,0,speedZ);

        gAccel = gravity / mass;
        speedY += gAccel * Time.deltaTime;
        this.transform.Translate(0, speedY, 0);

        force = 0;
        */
    }
}
