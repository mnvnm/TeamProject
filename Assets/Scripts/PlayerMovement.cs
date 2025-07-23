using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // 이동 속도

    private Rigidbody2D rb;
    private Vector2 movement;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // 키보드 입력 받기
        float moveX = Input.GetAxisRaw("Horizontal"); // A, D 또는 좌우 방향키
        movement = new Vector2(moveX, 0f);
    }

    void FixedUpdate()
    {
        // 이동 처리
        rb.linearVelocity = new Vector2(movement.x * moveSpeed, rb.linearVelocity.y);
    }
}
