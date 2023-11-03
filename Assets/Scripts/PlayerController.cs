using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float verticalInput;
    private Rigidbody playerRb;
    private SphereCollider sCollider;
    private GameObject focalPoint;
    public GameObject shuriken;
    private GameManager gm;
    private ScoreUI scoreUI;
    public float speed;
    private AudioSource playerAudio;
    public AudioClip shootSound;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        scoreUI = GameObject.Find("Score").GetComponent<ScoreUI>();
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("FocalPoint");
        sCollider= GetComponent<SphereCollider>();
        playerAudio = GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gm.isGameActive) {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                playerAudio.PlayOneShot(shootSound);
                var s = Instantiate(shuriken, transform.position,shuriken.transform.rotation);
                if (GameSettings.Instance.frugality){
                    Debug.Log("True");
                    GameSettings.Instance.score -= 1;
                    scoreUI.textMesh.text = "Score: " + GameSettings.Instance.score.ToString();
                }
                
                Destroy(s,5);
            }
            verticalInput = Input.GetAxis("Vertical");
            var horizontalInput = Input.GetAxis("Horizontal");
            playerRb.AddForce(focalPoint.transform.forward * speed * verticalInput * Time.deltaTime);
        // transform.Rotate(Vector3.up, Time.deltaTime * 200f * horizontalInput);
        }
    }
}
