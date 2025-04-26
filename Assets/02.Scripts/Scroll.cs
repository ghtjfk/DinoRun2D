using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroll : MonoBehaviour
{
    public float scrollSpeedX = 2f;     // X축 스크롤 속도.
    private Renderer groundRenderer;

    public bool isCloud;
    public float cloudScrollSpeedX;

    void Start()
    {
        groundRenderer = GetComponent<Renderer>();  // Quad의 Renderer 속성 가져오기.
    }

    void Update()
    {
        if (isCloud)
        {
            // 구름은 직접적으로 절대 위치를 변경하여 구현
            transform.Translate(-cloudScrollSpeedX * Time.deltaTime, 0, 0);

            if (transform.position.x <= -11f)
            {
                transform.position = new Vector2(11f, Random.Range(1f, 4f));
            }
        }
        else
        {
            // 시간의 흐름에 따라 오프셋 값을 계산
            float offsetX = Time.time * scrollSpeedX;

            // Material의 메인  텍스처의 오프셋을 조정
            groundRenderer.material.mainTextureOffset = new Vector2(offsetX, 0);
        }
    }
}
