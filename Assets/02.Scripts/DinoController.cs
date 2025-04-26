using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinoController : MonoBehaviour
{
    public float jumpForce;
    public bool isGrounded;
    public bool isDown;

    private Rigidbody2D rb;
    private Animator anim;

    public Transform groundCheckPoint;  // 발바닥 센서 위치
    public LayerMask whatIsGround;  // Ground인지 비교할 LayerMask

    private Vector2 savedOffset;    // 서 있을 때 값
    private Vector2 savedSize;      // 서 있을 때 값
    private BoxCollider2D boxCollider;

    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        anim.SetBool("isGround", true);
        anim.SetBool("isDown", false);

        SaveColliderSettings();     // 서 있을 때 collider값 저장 
    }

    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, 0.2f, whatIsGround);
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded.Equals(true) && isDown.Equals(false))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
        anim.SetBool("isGround", isGrounded);

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            SetDownArrowDown();
        }
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            SetDownArrowUp();
        }

        // y축 힘을 이용해 더블 점프 방지
        /*if (Input.GetKeyDown(KeyCode.Space) && rb.velocity.y==0)
        {
            anim.SetBool("isGround", false);
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            anim.SetBool("isGround", true);
        }*/
    }

    void SaveColliderSettings()
    {
        // 서 있을 때 collider 값 저장
        savedOffset = boxCollider.offset;
        savedSize = boxCollider.size;
    }

    void LoadColliderSettings()
    {
        // 서 있을 때 collider 값 로드
        boxCollider.offset = savedOffset;
        boxCollider.size = savedSize;
    }

    void SetDownArrowDown()
    {
        isDown = true;
        anim.SetBool("isDown", true);
        // 앉아있을 때 collider 값으로 지정
        boxCollider.offset = new Vector2(0, -0.25f);
        boxCollider.size = new Vector2(1.39f, 0.76f);
    }

    void SetDownArrowUp()
    {
        isDown = false;
        anim.SetBool("isDown", false);
        LoadColliderSettings();     // 저장했던 서 있을 때 collider 값 로드 
    }

    void OnDrawGizmos()     // 레이저 범위 그리기 (Unity에서 자동으로 호출)
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheckPoint.position, 0.2f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("충돌함");
    }
}
