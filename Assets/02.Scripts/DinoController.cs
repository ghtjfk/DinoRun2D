using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DinoController : MonoBehaviour
{
    public float jumpForce;
    public bool isGrounded;
    public bool isDown;

    private Rigidbody2D rb;
    private Animator anim;

    public Transform groundCheckPoint;  // �߹ٴ� ���� ��ġ
    public LayerMask whatIsGround;  // Ground���� ���� LayerMask

    private Vector2 savedOffset;    // �� ���� �� ��
    private Vector2 savedSize;      // �� ���� �� ��
    private BoxCollider2D boxCollider;

    AudioSource audioSource;
    public AudioClip jumpClip;
    public AudioClip deathClip;
    public AudioClip pointClip;


    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        anim.SetBool("isGround", true);
        anim.SetBool("isDown", false);

        SaveColliderSettings();     // �� ���� �� collider�� ���� 

        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, 0.2f, whatIsGround);
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded.Equals(true))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            audioSource.PlayOneShot(jumpClip);
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

        // y�� ���� �̿��� ���� ���� ����
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
        // �� ���� �� collider �� ����
        savedOffset = boxCollider.offset;
        savedSize = boxCollider.size;
    }

    void LoadColliderSettings()
    {
        // �� ���� �� collider �� �ε�
        boxCollider.offset = savedOffset;
        boxCollider.size = savedSize;
    }

    void SetDownArrowDown()
    {
        isDown = true;
        anim.SetBool("isDown", true);
        // �ɾ����� �� collider ������ ����
        boxCollider.offset = new Vector2(0, -0.25f);
        boxCollider.size = new Vector2(1.39f, 0.76f);
    }

    void SetDownArrowUp()
    {
        isDown = false;
        anim.SetBool("isDown", false);
        LoadColliderSettings();     // �����ߴ� �� ���� �� collider �� �ε� 
    }

    void OnDrawGizmos()     // ������ ���� �׸��� (Unity���� �ڵ����� ȣ��)
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheckPoint.position, 0.2f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ���� ȹ���̶� �浹���� ��
        if (collision.CompareTag("PointSpace"))
        {
            GameManager.instance.GetScore();    // �̱������� ����.
            audioSource.PlayOneShot(pointClip);
        }

        // ��ֹ��̶� �浹���� ��
        if (collision.CompareTag("GameOverSpace"))
        {
            GameManager.instance.GameOver();
            audioSource.PlayOneShot(deathClip);
        }
    }
}
