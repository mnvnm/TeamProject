using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float m_speed = 8;
    private float m_jumpForce = 380;

    private bool m_isGrounded = true;
    private int m_jumpCount = 0;
    private int m_jumpCapacity = 2;

    private Rigidbody2D m_rigid;
    private Animator m_animator;

    void Start()
    {
        m_rigid = GetComponent<Rigidbody2D>();
        m_animator = GetComponent<Animator>();
    }

    public void Init()
    {

    }

    void Update()
    {
        Move();
        Jump();
    }

    private void Move()
    {
        float xAxis = Input.GetAxisRaw("Horizontal");

        m_rigid.linearVelocity = new Vector2(xAxis * m_speed, m_rigid.linearVelocity.y);
    }

    private void Jump()
    {
        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space)) && m_jumpCount < m_jumpCapacity)
        {
            m_rigid.linearVelocity = new Vector2(m_rigid.linearVelocity.x, 0);
            m_rigid.AddForce(new Vector2(0, m_jumpForce));
            m_jumpCount++;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.contacts[0].normal.y > 0.99f && !m_isGrounded)
        {
            m_isGrounded = true;
            m_jumpCount = 0;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            m_isGrounded = false;
            m_jumpCount = m_jumpCount < 1 ? 1 : m_jumpCount;
        }
    }
}
