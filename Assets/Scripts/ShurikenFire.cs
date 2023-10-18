using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShurikenFire : MonoBehaviour
{
    public Vector3 rotateVector;
    private Rigidbody shurikenRb;
    private GameObject focalPoint;
    // Start is called before the first frame update
    private Vector3 flyDir;
    void Start()
    {
        shurikenRb= GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("FocalPoint");
        flyDir = focalPoint.transform.forward;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotateVector * 10.0f * Time.deltaTime);
        shurikenRb.AddForce(flyDir * 1000f * Time.deltaTime);
        //shurikenRb.AddForce(Vector3.forward * 5.0f);
    }
}
