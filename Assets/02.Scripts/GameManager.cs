using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;     // �̱��� �ν��Ͻ��� ������ ���� ����


    public Transform[] spawnPoints;     // ������ ��ġ��
    public GameObject[] obstacles;      // ������ ������Ʈ�� (��ֹ���)

    public bool isSpawning;             // true�� ���� ����, false�� ���� �� ��.
    public float spawnInterval;    // ���� ���� (��)
    private float timer = 0;            // �����ϱ� ���� Ÿ�̸�
    public int spawnTracker;            // ���õ� ��ֹ� (0->������1, 1->������2, 2->������3, 3->������4, 4->�ͷ�)

    public TextMeshProUGUI scoreText;
    private int score = 0;

    public GameObject gameOverPanel;
    public TextMeshProUGUI bestScoreText;
    public int bestScore = 0;
    public TextMeshProUGUI nowScoreText;

    private void Awake()
    {
        // �̱��� ������. �ܿ쵵��.
        if(instance != null)
        {
            Destroy(gameObject);    // �ν��Ͻ��� �����Ѵٸ� ���� ������Ʈ�� ����
        }
        else
        {
            instance = this;    // �ν��Ͻ��� �������� �ʴٸ� ���� ������Ʈ�� �ν��Ͻ��� ����
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
            // ��ġ�� �� �´� ������ �־���. spawnPoints (�θ� ������Ʈ)�� ��ġ�� -0.4����.. �׷��� ���� ���ݾ� �������� �ſ���.
            if (timer >= spawnInterval)
            {
                spawnInterval = Random.Range(0.5f, 1.5f);
                timer = 0;
                spawnTracker = Random.Range(0, obstacles.Length);

                if (spawnTracker.Equals(4))     // �ͷ�
                {
                    int randPoint = 2 + Random.Range(0, 3);
                    Instantiate(obstacles[spawnTracker], spawnPoints[randPoint].position, spawnPoints[randPoint].rotation);
                }
                else if (spawnTracker >= 1 && spawnTracker <= 3)    // ������ 2,3,4
                {
                    Instantiate(obstacles[spawnTracker], spawnPoints[1].position, spawnPoints[1].rotation);
                }
                else    // ������ 1
                {
                    Instantiate(obstacles[spawnTracker], spawnPoints[spawnTracker].position, spawnPoints[spawnTracker].rotation);
                }
            }
        }
    }

    // ������ ��� �Լ�
    public void GetScore()
    {
        score++;
        scoreText.text = "score: " + score.ToString();
    }

    // GameOver �� ����Ǵ� �Լ�
    public void GameOver()
    {
        Time.timeScale = 0f; // �ð� ����

        // ���� ������ ����Ǿ� �ִ� �ְ� �������� ũ�� ����
        if (score >= PlayerPrefs.GetInt("BestScore", 0))
        {
            PlayerPrefs.SetInt("BestScore", score);
        }

        // ����Ǿ� �ִ� �ְ� ���� ���� �ҷ��ͼ� ���
        bestScoreText.text = "Best Score: " + PlayerPrefs.GetInt("BestScore", 0).ToString();

        // ���� ������ ���
        nowScoreText.text = "Score: " + score.ToString();

        //GameOverPanel Ȱ��ȭ
        gameOverPanel.SetActive(true);

        /*
        restart�� ���� ���� ��ε� �ϴ� ������� �����߾,
        �̷� ������ ����Ʈ ���ھ� ������Ʈ�� �����ϸ� ����� ������ �ȵ�.
        �̷� �� or ���� �뷮�� �����͸� ������ �� or ������ �߿��� �����Ͱ� �ƴ� ��
        PlayerPrefs�� ����Ѵ�!!!!

        if(score >= bestScore)
        {
            bestScore = score;
        }
        bestScoreText.text = "Best Score: " + bestScore.ToString();
        */
    }

    // Restart ��ư ������ �� ����Ǵ� �Լ�
    public void Restart()
    {
        Time.timeScale = 1f; // �ð� �ٽ� ���� �帧
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // ���� �� �ٽ� �ε�
    }

}