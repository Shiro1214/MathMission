using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class EnemyScript : MonoBehaviour
{
    private GameObject entrance;
    public GameObject answerCanvas;
    private Rigidbody enemyRb;
    private Quaternion fixedRotation;
    public bool hasRightAnswer, isHit;
    public float speedForce, answer;
    private TextMeshProUGUI textUi;
    private Vector3 accelerate,lastVelocity;
    private GameManager gm;
    public ScoreUI scoreUI;
    AudioSource audioSource;
    public AudioClip correctSound, incorrectSound;

    // Start is called before the first frame update
    void Start()
    {
        entrance = GameObject.Find("Entrance");
        enemyRb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        fixedRotation = transform.rotation;
        textUi  = answerCanvas.transform.Find("MathAnswer").gameObject.GetComponent<TextMeshProUGUI>();
        Debug.Log(textUi==null);
        scoreUI = GameObject.Find("Score").GetComponent<ScoreUI>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        if(transform.position.y < -10){
            Destroy(gameObject);
        }

        answerCanvas.transform.position = transform.position + new Vector3(0,1,0);
        answerCanvas.transform.rotation = fixedRotation;
        if (answer!=0){
            textUi.text = answer.ToString();
        }
        
        var direction = entrance.transform.position - transform.position;
        direction = direction.normalized;
        enemyRb.AddForce(direction * speedForce * Time.deltaTime);
        accelerate = (enemyRb.velocity - lastVelocity) / Time.fixedDeltaTime;
        lastVelocity = enemyRb.velocity;
        
        //if accerlerate is low then increase force
        if (accelerate.magnitude < 0.05f){

            //Debug.Log(answer + " is stucked with acc "+ accelerate.magnitude);
            enemyRb.AddForce(direction* speedForce*0.1f * Time.deltaTime, ForceMode.Impulse);
        } 
        //enemyRb.AddForce(direction * speedForce * Time.deltaTime);
        //enemyRb.AddForce(direction * speedForce * Time.deltaTime, ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == entrance && !isHit)
        {
            scoreUI.score -= 10;
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Shuriken") && !isHit)
        {
            isHit = true;
            Destroy(other.gameObject);
            if (hasRightAnswer){
                audioSource.PlayOneShot(correctSound);
                scoreUI.score += 10;
                Destroy(gameObject, correctSound.length + 0.1f);
                gm.clearEnemies();
            } else 
            {
                audioSource.PlayOneShot(incorrectSound);
                scoreUI.score -= 10;
                Destroy(gameObject, incorrectSound.length + 0.1f);
            }
        }
    }


    //if the object speed or accelerate is low then add force
   


}
