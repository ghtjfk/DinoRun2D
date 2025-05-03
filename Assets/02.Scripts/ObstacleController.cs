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

    // 메인 카메라에 안 보이면 없애겠다!
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
