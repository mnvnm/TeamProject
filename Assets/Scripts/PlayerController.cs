using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float m_speed = 8;
    private float m_jumpForce = 380;

    private bool m_isGrounded = true;

    [SerializeField] SpriteRenderer carryingBarrelSpr;
    private Rigidbody2D m_rigid;
    private InteractableObject m_interactableObj;
    [SerializeField] GameObject WeldingPoingIndicatedObjPrefab; // 용접 해야할 장소를 가리키는 오브젝트 프리팹
    private List<GameObject> WeldingPoingIndicatedObjs = new List<GameObject>(); // 용접 해야할 장소를 가리키는 오브젝트
    private Animator m_animator;

    bool isInteractive = false;

    void Start()
    {
        m_rigid = GetComponent<Rigidbody2D>();
        m_animator = GetComponent<Animator>();
        m_interactableObj = null;
    }

    public void Init()
    {
        m_interactableObj = null;
        WeldingPointIndicatedObjCreate();
    }

    void Update()
    {
        Move();
        Jump();
        Interactive();
        transform.rotation = Quaternion.identity;
        carryingBarrelSpr.enabled = MissionManager.Inst.IsCarryingOxygen;
        ShowIndicatedObjs();
    }

    private void Move()
    {
        if (isInteractive) return;
        float xAxis = Input.GetAxisRaw("Horizontal");

        m_rigid.linearVelocity = new Vector2(xAxis * m_speed, m_rigid.linearVelocity.y);
    }

    private void Jump()
    {
        if (isInteractive) return;
        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space)) && m_isGrounded)
        {
            m_rigid.linearVelocity = new Vector2(m_rigid.linearVelocity.x, 0);
            m_rigid.AddForce(gameObject.transform.up * m_jumpForce);
        }
    }

    void Interactive()
    {
        if (m_interactableObj != null && Input.GetKeyDown(KeyCode.F))
        {
            m_rigid.linearVelocity = new Vector2(0, 0);
            m_interactableObj.isInteractContinue = true;
            isInteractive = true;
            m_interactableObj.Interactive();
        }
        if (m_interactableObj != null && Input.GetKeyUp(KeyCode.F))
        {
            m_interactableObj.isInteractContinue = false;
            isInteractive = false;
        }
    }

    void WeldingPointIndicatedObjCreate()
    {
        for (int i = 0; i < MissionManager.Inst.weldingPoints.Length; i++)
        {
            var obj = Instantiate(WeldingPoingIndicatedObjPrefab, transform);
            WeldingPoingIndicatedObjs.Add(obj);
        }
    }

    void ShowIndicatedObjs()
    {
        if (WeldingPoingIndicatedObjs.Count == 0 || WeldingPoingIndicatedObjs == null) return;
        for (int i = 0; i < MissionManager.Inst.weldingPoints.Length; i++)
        {
            WeldingPoingIndicatedObjs[i].SetActive(!MissionManager.Inst.weldingPoints[i].activeSelf);
            if (MissionManager.Inst.weldingPoints[i].activeSelf) continue;
            Vector3 direction = MissionManager.Inst.weldingPoints[i].transform.position - WeldingPoingIndicatedObjs[i].transform.position;
            WeldingPoingIndicatedObjs[i].transform.up = direction;
            WeldingPoingIndicatedObjs[i].transform.localPosition = WeldingPoingIndicatedObjs[i].transform.up * 1f;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.contacts[0].normal.y > 0.7f || collision.gameObject.CompareTag("Ground"))
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
        m_interactableObj = collision.GetComponent<InteractableObject>();

        if (m_interactableObj != null) MissionManager.Inst.IsInteractable = true;
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        m_interactableObj = collision.GetComponent<InteractableObject>();

        if (m_interactableObj != null)
        {
            m_interactableObj = null;
            MissionManager.Inst.IsInteractable = false;
        }
    }
}
