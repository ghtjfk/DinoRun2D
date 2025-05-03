using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Transform[] spawnPoints;     // ������ ��ġ��
    public GameObject[] obstacles;      // ������ ������Ʈ�� (��ֹ���)

    public bool isSpawning;             // true�� ���� ����, false�� ���� �� ��.
    public float spawnInterval = 3f;    // ���� ���� (��)
    private float timer = 0;            // �����ϱ� ���� Ÿ�̸�
    public int spawnTracker;            // ���õ� ��ֹ� (0->������1, 1->������2, 2->������3, 3->������4, 4->�ͷ�)

    void Update()
    {
        timer += Time.deltaTime;
        // ��ġ�� �� �´� ������ �־���. spawnPoints (�θ� ������Ʈ)�� ��ġ�� -0.4����.. �׷��� ���� ���ݾ� �������� �ſ���.
        if(timer >= spawnInterval)
        {
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
