using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float m_speed = 8;
    private float m_jumpForce = 380;

    private bool m_isGrounded = true;

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
        transform.rotation = Quaternion.identity;
    }

    private void Move()
    {
        float xAxis = Input.GetAxisRaw("Horizontal");

        m_rigid.linearVelocity = new Vector2(xAxis * m_speed, m_rigid.linearVelocity.y);
    }

    private void Jump()
    {
        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space)) && m_isGrounded)
        {
            m_rigid.linearVelocity = new Vector2(m_rigid.linearVelocity.x, 0);
            m_rigid.AddForce(gameObject.transform.up * m_jumpForce);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.contacts[0].normal.y > 0.99f || collision.gameObject.CompareTag("Ground"))
        {
            m_isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        m_isGrounded = false;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        var interactiveObj = collision.GetComponent<IInteractable>();

        if (interactiveObj != null) MissionManager.Inst.IsInteractable = true;
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        var interactiveObj = collision.GetComponent<IInteractable>();

        if (interactiveObj != null) MissionManager.Inst.IsInteractable = false;
    }
}
