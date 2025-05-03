using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Transform[] spawnPoints;     // 생성할 위치들
    public GameObject[] obstacles;      // 생성할 오브젝트들 (장애물들)

    public bool isSpawning;             // true면 스폰 시작, false면 스폰 안 함.
    public float spawnInterval = 3f;    // 생성 간격 (초)
    private float timer = 0;            // 스폰하기 위한 타이머
    public int spawnTracker;            // 선택된 장애물 (0->선인장1, 1->선인장2, 2->선인장3, 3->선인장4, 4->익룡)

    void Update()
    {
        timer += Time.deltaTime;
        // 위치가 안 맞는 오류가 있었다. spawnPoints (부모 오브젝트)의 위치가 -0.4였음.. 그래서 전부 조금씩 내려갔던 거였다.
        if(timer >= spawnInterval)
        {
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
