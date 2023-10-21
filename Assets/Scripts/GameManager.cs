using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public GameObject enemy;
    private int level = 5;
    public float MathLevel = 10.0f;
    private float a,b;
    private TextMeshProUGUI mathQuestion;
    private bool hasRightAnswer;
    private List<string> operators;
    private float answer;
    private int curOper;
    // Start is called before the first frame update
    void Start()
    {
        mathQuestion = GameObject.Find("MathQuestion").GetComponent<TextMeshProUGUI>();
        mathQuestion.text = "";
        operators = new List<string>
        {
            "+",
            "-",
            "*",
            "/"
        };
    }

    // Update is called once per frame
    void Update()
    {
        if(GameObject.FindGameObjectsWithTag("Enemy").Length == 0){
            spawnEnemies();
        }
        mathQuestion.text = "" + a + " " + operators[curOper] + " " + b + " = ?";
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
