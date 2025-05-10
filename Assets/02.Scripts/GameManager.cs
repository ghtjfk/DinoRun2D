using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;     // 싱글톤 인스턴스를 저장할 정적 변수


    public Transform[] spawnPoints;     // 생성할 위치들
    public GameObject[] obstacles;      // 생성할 오브젝트들 (장애물들)

    public bool isSpawning;             // true면 스폰 시작, false면 스폰 안 함.
    public float spawnInterval;    // 생성 간격 (초)
    private float timer = 0;            // 스폰하기 위한 타이머
    public int spawnTracker;            // 선택된 장애물 (0->선인장1, 1->선인장2, 2->선인장3, 3->선인장4, 4->익룡)

    public TextMeshProUGUI scoreText;
    private int score = 0;

    public GameObject gameOverPanel;
    public TextMeshProUGUI bestScoreText;
    public int bestScore = 0;
    public TextMeshProUGUI nowScoreText;

    private void Awake()
    {
        // 싱글톤 패턴임. 외우도록.
        if(instance != null)
        {
            Destroy(gameObject);    // 인스턴스가 존재한다면 현재 오브젝트를 삭제
        }
        else
        {
            instance = this;    // 인스턴스가 존재하지 않다면 현재 오브젝트를 인스턴스로 설정
        }
    }

    private void Start()
    {
        spawnInterval = Random.Range(0.5f,1.5f);
    }
    void Update()
    {
        if (isSpawning)
        {
            timer += Time.deltaTime;
            // 위치가 안 맞는 오류가 있었다. spawnPoints (부모 오브젝트)의 위치가 -0.4였음.. 그래서 전부 조금씩 내려갔던 거였다.
            if (timer >= spawnInterval)
            {
                spawnInterval = Random.Range(0.5f, 1.5f);
                timer = 0;
                spawnTracker = Random.Range(0, obstacles.Length);

                if (spawnTracker.Equals(4))     // 익룡
                {
                    int randPoint = 2 + Random.Range(0, 3);
                    Instantiate(obstacles[spawnTracker], spawnPoints[randPoint].position, spawnPoints[randPoint].rotation);
                }
                else if (spawnTracker >= 1 && spawnTracker <= 3)    // 선인장 2,3,4
                {
                    Instantiate(obstacles[spawnTracker], spawnPoints[1].position, spawnPoints[1].rotation);
                }
                else    // 선인장 1
                {
                    Instantiate(obstacles[spawnTracker], spawnPoints[spawnTracker].position, spawnPoints[spawnTracker].rotation);
                }
            }
        }
    }

    // 점수를 얻는 함수
    public void GetScore()
    {
        score++;
        scoreText.text = "score: " + score.ToString();
    }

    // GameOver 시 실행되는 함수
    public void GameOver()
    {
        Time.timeScale = 0f; // 시간 정지

        // 현재 점수가 저장되어 있던 최고 점수보다 크면 저장
        if (score >= PlayerPrefs.GetInt("BestScore", 0))
        {
            PlayerPrefs.SetInt("BestScore", score);
        }

        // 저장되어 있던 최고 점수 값을 불러와서 출력
        bestScoreText.text = "Best Score: " + PlayerPrefs.GetInt("BestScore", 0).ToString();

        // 현재 점수를 출력
        nowScoreText.text = "Score: " + score.ToString();

        //GameOverPanel 활성화
        gameOverPanel.SetActive(true);

        /*
        restart를 현재 씬을 재로드 하는 방법으로 구현했어서,
        이런 식으로 베스트 스코어 업데이트를 구현하면 제대로 저장이 안됨.
        이럴 때 or 적은 용량의 데이터를 저장할 때 or 보안이 중요한 데이터가 아닐 때
        PlayerPrefs를 사용한다!!!!

        if(score >= bestScore)
        {
            bestScore = score;
        }
        bestScoreText.text = "Best Score: " + bestScore.ToString();
        */
    }

    // Restart 버튼 눌렀을 때 실행되는 함수
    public void Restart()
    {
        Time.timeScale = 1f; // 시간 다시 정상 흐름
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // 현재 씬 다시 로드
    }

}