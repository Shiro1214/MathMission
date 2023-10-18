using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float verticalInput;
    private Rigidbody playerRb;
    private SphereCollider sCollider;
    private GameObject focalPoint;
    public GameObject shuriken;
    public float speed;
    private AudioSource playerAudio;
    public AudioClip shootSound;
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("FocalPoint");
        sCollider= GetComponent<SphereCollider>();
        playerAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerAudio.PlayOneShot(shootSound);
            var s = Instantiate(shuriken, transform.position,shuriken.transform.rotation);
            
            Destroy(s,5);
        }
        verticalInput = Input.GetAxis("Vertical");
        var horizontalInput = Input.GetAxis("Horizontal");
        playerRb.AddForce(focalPoint.transform.forward * speed * verticalInput * Time.deltaTime);
       // transform.Rotate(Vector3.up, Time.deltaTime * 200f * horizontalInput);
    }
}
