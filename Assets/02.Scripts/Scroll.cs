using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroll : MonoBehaviour
{
    public float scrollSpeedX = 2f;     // X�� ��ũ�� �ӵ�.
    private Renderer groundRenderer;

    public bool isCloud;
    public float cloudScrollSpeedX;

    void Start()
    {
        groundRenderer = GetComponent<Renderer>();  // Quad�� Renderer �Ӽ� ��������.
    }

    void Update()
    {
        if (isCloud)
        {
            // ������ ���������� ���� ��ġ�� �����Ͽ� ����
            transform.Translate(-cloudScrollSpeedX * Time.deltaTime, 0, 0);

            if (transform.position.x <= -11f)
            {
                transform.position = new Vector2(11f, Random.Range(1f, 4f));
            }
        }
        else
        {
            // �ð��� �帧�� ���� ������ ���� ���
            float offsetX = Time.time * scrollSpeedX;

            // Material�� ����  �ؽ�ó�� �������� ����
            groundRenderer.material.mainTextureOffset = new Vector2(offsetX, 0);
        }
    }
}
