using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPart : MonoBehaviour
{
    public UnityEngine.Vector3 offset;
    private GameObject body;
    // Start is called before the first frame update
    void Start()
    {
        body =  GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = body.transform.position + offset;
    }
}
