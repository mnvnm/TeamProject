using UnityEngine;

public class CameraScroller : MonoBehaviour
{
    public float scrollSpeed = 2f; // 이동 속도
    public float endX = 50f;       // 이동 끝나는 x 위치

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        if (transform.position.x < endX)
        {
            transform.position += Vector3.right * scrollSpeed * Time.deltaTime;
        }
    }
}
