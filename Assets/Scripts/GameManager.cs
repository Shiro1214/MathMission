using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject enemy;
    public int level = 2;
    public float MathLevel = 10.0f;
    private float a,b;
    private TextMeshProUGUI mathQuestion;
    private TextMeshProUGUI TimerText;
    public float time = 10.0f;
    private bool hasRightAnswer;
    private List<string> operators;
    private float answer;
    private int curOper;
    public bool isGameActive = true;
    private AudioSource backgroundMusic;

    public GameObject levelComplete;
    bool musicFadeIn = false;
    // Start is called before the first frame update
    void Start()
    {

        if (GameSettings.Instance != null)
        {
            MathLevel = GameSettings.Instance.mathLevel;
            time = GameSettings.Instance.timer;
        }
        backgroundMusic = GameObject.Find("Main Camera").GetComponent<AudioSource>();
        
        TimerText = GameObject.Find("Timer").GetComponent<TextMeshProUGUI>();
        TimerText.text = "Time: " + ((int)GameSettings.Instance.timer).ToString();
        mathQuestion = GameObject.Find("MathQuestion").GetComponent<TextMeshProUGUI>();
        mathQuestion.text = "";
        operators = new List<string>
        {
            "+",
            "-",
            "*",
            "/"
        };
        StartCoroutine(FadeIn(backgroundMusic, 2f));

    }
    
    // Update is called once per frame
    void Update()
    {
        if (isGameActive){
            time -= Time.deltaTime;
            if (time <= 0){
                GameOver();
            }
            TimerText.text = "Time: " + ((int)time).ToString();
            
            if(GameObject.FindGameObjectsWithTag("Enemy").Length == 0){
                spawnEnemies();
                level++;
            }
            
        }

    }
    public void GameOver(){
        isGameActive = false;
        //Invoke("goNext", 2.0f);
        levelComplete.SetActive(true);
        StartCoroutine(FadeOut(backgroundMusic, 2f));
    }
    void goNext(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    IEnumerator FadeOut(AudioSource audioSource, float fadeTime)
{
    float startVolume = audioSource.volume;

    while (audioSource.volume > 0)
    {
        audioSource.volume -= startVolume * Time.deltaTime / fadeTime;

        yield return null;
    }
    audioSource.Stop();
    audioSource.volume = startVolume;
    goNext();
    }
    IEnumerator FadeIn(AudioSource audioSource, float fadeTime)
    {
        
        while (audioSource.volume < 0.25f){
            audioSource.volume +=  0.25f * Time.deltaTime / fadeTime;
            yield return null;
        }
    }

    void spawnEnemies(){
        hasRightAnswer = false;
        for (int i = 0; i<level; i++){ // enemy.transform.position + new UnityEngine.Vector3(getXpos(),0,0)
            var e = Instantiate(enemy,randomPos(),enemy.transform.rotation);
            var enemyScript = e.GetComponent<EnemyScript>();
            
            if (!hasRightAnswer){
                enemyScript.hasRightAnswer = true;
                float c = RandomMathProblem();
                answer = c;
                enemyScript.answer = c;
                mathQuestion.text = "" + a + " " + operators[curOper] + " " + b + " = ?";
                
            } else {
                enemyScript.hasRightAnswer = false;
                float c = RandomMathProblem();
                while (c==answer){
                    c = RandomMathProblem();
                }
                enemyScript.answer = c;
               
            }
            
        }
    }
    public void clearEnemies(){
        var enemies = GameObject.FindGameObjectsWithTag("Enemy").Select(x => x.GetComponent<EnemyScript>()).ToArray();
        for (int i = 0; i < enemies.Length; i++){
            if (enemies[i].hasRightAnswer == false){
                Destroy(enemies[i].gameObject);
            }
        }
    }

    private float getXpos(){
        return Random.Range(-5.0f,15.0f);
    }
    private float getZpos(){
        return Random.Range(40.0f,50.0f);
    }
    private Vector3 randomPos(){
        return new Vector3(getXpos(),0,getZpos());
    }
    //this function generates a random math p
    // problem also return the right answer
    // it also passes it into the Enemy script
    float RandomMathProblem(){
        float c = 0;
        if(!hasRightAnswer){
            a = Random.Range(1, MathLevel);
            b = Random.Range(1, MathLevel);
            a = ((int)(a * 100)) / 100;
            b = ((int)(b * 100)) / 100;
            curOper = Random.Range(0, operators.Count);
            var oper  = operators[curOper];

            switch (oper){
                case "+": c = a + b; break;
                case "-": c = a - b; break;
                case "*": c = a * b; break;
                case "/": c = a / b; break;
            }
            hasRightAnswer = true;
            
        }
        else{
            var a = Random.Range(1, MathLevel);
            var b = Random.Range(1, MathLevel);
            a = ((int)(a * 100)) / 100;
            b = ((int)(b * 100)) / 100;
            var oper  = operators[Random.Range(0, operators.Count)];

            switch (oper){
                case "+": c = a + b; break;
                case "-": c = a - b; break;
                case "*": c = a * b; break;
                case "/": c = a / b; break;
            }

        }
        //cast float to 2 decimal
        c = ((int)(c * 100)) / 100f;
        return c;
    }
}
