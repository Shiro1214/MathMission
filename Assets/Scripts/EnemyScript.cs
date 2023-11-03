using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
[RequireComponent(typeof(AudioSource))]
public class EnemyScript : MonoBehaviour
{
    private GameObject entrance;
    public GameObject answerCanvas;
    private Rigidbody enemyRb;
    private Quaternion fixedRotation;
    public bool hasRightAnswer, isHit;
    public float speedForce, answer;
    public TextMeshProUGUI textUi;
    private Vector3 accelerate,lastVelocity;
    private GameManager gm;
    public ScoreUI scoreUI;
    private ScoreUI enemyIn;
    AudioSource audioSource;
    public AudioClip correctSound, incorrectSound;
    private ParticleSystem particleObj;
    private bool setAnswer = false;
    private Vector3 entrancePos;
    private Collider entranceCollider;

    // Start is called before the first frame update
    void Start()
    {
        entrance = GameObject.Find("Entrance");
        enemyRb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        fixedRotation = transform.rotation;
        textUi  = answerCanvas.transform.Find("MathAnswer").gameObject.GetComponent<TextMeshProUGUI>();
        scoreUI = GameObject.Find("Score").GetComponent<ScoreUI>();
        enemyIn = GameObject.Find("EnemyIn").GetComponent<ScoreUI>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        particleObj = transform.Find("Explosion_Purple").gameObject.GetComponent<ParticleSystem>();
        entranceCollider = entrance.GetComponent<Collider>();
        entrancePos = entrance.transform.position  + randomEntranceOffset();
        speedForce = GameSettings.Instance.enemySpeed;
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        particleObj.transform.position = transform.position;

        if(transform.position.y < -10){
            Destroy(gameObject);
        }
        answerCanvas.transform.position = transform.position + new Vector3(0,1,0);
        answerCanvas.transform.rotation = fixedRotation;
        if (setAnswer==false){
            textUi.text = answer.ToString();
        }
        if (gm.isGameActive){
            var direction = entrancePos - transform.position;
            direction = direction.normalized;
            enemyRb.AddForce(direction * speedForce * Time.deltaTime);
        }

       
    }
    private Vector3 randomEntranceOffset(){
        return new Vector3(Random.Range(entranceCollider.bounds.min.x, entranceCollider.bounds.max.x) - entrance.transform.position.x,0,0);
    }
    private void OnCollisionEnter(Collision other)
    {
        if (gm.isGameActive){
            if (other.gameObject == entrance){
                if (hasRightAnswer) {
                GameSettings.Instance.score -= 10;
                GameSettings.Instance.enemyIn += 1;
            
                scoreUI.textMesh.text = "Score: " + GameSettings.Instance.score.ToString();
                enemyIn.textMesh.text = "Enemy in: " + GameSettings.Instance.enemyIn.ToString();
                gm.clearEnemies();
            }
            Destroy(gameObject);
            }
            if (other.gameObject.CompareTag("Shuriken") && !isHit)
            {
            //explosionParticle.Play();
            isHit = true;
            particleObj.Play();
            Destroy(particleObj.gameObject, 2f);
            Destroy(other.gameObject);
            //particleObj= Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
            if (hasRightAnswer){

                audioSource.PlayOneShot(correctSound);
                GameSettings.Instance.score += 10;
                if (GameSettings.Instance.frugality){
                    GameSettings.Instance.score += 1;
                }
                scoreUI.textMesh.text = "Score: " + GameSettings.Instance.score.ToString();
                Destroy(gameObject, correctSound.length + 0.1f);
                gm.clearEnemies();
            } else 
            {
                audioSource.PlayOneShot(incorrectSound);
                GameSettings.Instance.score -= 10;
                scoreUI.textMesh.text = "Score: " + GameSettings.Instance.score.ToString();
                Destroy(gameObject, incorrectSound.length + 0.1f);
            }
            }
        }
        
    }


    //if the object speed or accelerate is low then add force
   


}
