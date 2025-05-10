using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    public float moveSpeedX;
    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        rb.velocity = new Vector2(moveSpeedX, rb.velocity.y);
    }

    // 게임 ㅇ오브젝트가 카메라에 뷰 밖으로 나갔을 때 실행되는 함수.     visible함수 또한 있다!
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
