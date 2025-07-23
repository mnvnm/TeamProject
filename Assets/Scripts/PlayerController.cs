using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] GroundCheck groundCheck;
    private float m_speed = 8;
    private float m_jumpForce = 800;

    private bool m_isGrounded = true;

    [SerializeField] SpriteRenderer carryingBarrelSpr;
    private Rigidbody2D m_rigid;
    private InteractableObject m_interactableObj;
    [SerializeField] GameObject WeldingPoingIndicatedObjPrefab; // 용접 해야할 장소를 가리키는 오브젝트 프리팹
    [SerializeField] TextMeshProUGUI InteractText; // 용접 해야할 장소를 가리키는 오브젝트 프리팹
    private List<GameObject> WeldingPointIndicatedObjs = new List<GameObject>(); // 용접 해야할 장소를 가리키는 오브젝트
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
        InteractText.gameObject.SetActive(false);
    }

    public void SetIsInteractive(bool isInter) // 남발 금지 강제로 바꾸기에 위험
    {
        isInteractive = isInter;
    }

    public bool GetIsInteractive()
    {
        return isInteractive;
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
        if (isInteractive)
        {
            m_rigid.linearVelocity = new Vector2(0, 0);
            return;
        }
        float xAxis = Input.GetAxisRaw("Horizontal");
        m_speed = MissionManager.Inst.IsCarryingOxygen ? 8 * 0.6f : 8;
        m_rigid.linearVelocity = new Vector2(xAxis * m_speed, m_rigid.linearVelocity.y);
        m_rigid.linearVelocity = new Vector2(Mathf.Clamp(m_rigid.linearVelocity.x, -m_speed, m_speed), Mathf.Clamp(m_rigid.linearVelocity.y, -16, m_speed));
    }

    private void Jump()
    {
        if (isInteractive)
        {
            m_rigid.linearVelocity = new Vector2(0, 0);
            return;
        }
        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space)) && groundCheck.GetIsGround())
        {
            m_animator.SetBool("Jump", groundCheck.GetIsGround());
            m_rigid.linearVelocity = new Vector2(m_rigid.linearVelocity.x, 0);
            m_rigid.AddForce(gameObject.transform.up * m_jumpForce);
        }
    }

    void Interactive()
    {
        if (m_interactableObj != null && Input.GetKeyDown(KeyCode.F))
        {
            var wire = m_interactableObj.GetComponent<Wire>();
            if (wire != null && wire.IsSuccess) return;
            var nonsense = m_interactableObj.GetComponent<Nonsense>();
            if (nonsense != null && (nonsense.IsSuccess || !MissionManager.Inst.GetIsWireAllSuccess())) return;
            var lifeSupportMachine = m_interactableObj.GetComponent<LifeSupportMachine>();
            if (lifeSupportMachine != null && !MissionManager.Inst.IsCarryingOxygen) return;
            var oxygen = m_interactableObj.GetComponent<Oxygen>();
            if (oxygen != null && MissionManager.Inst.IsCarryingOxygen) return;
            var weldingPoint = m_interactableObj.GetComponent<WeldingPoint>();
            if (weldingPoint != null && weldingPoint.activeSelf) return;
            m_rigid.linearVelocity = new Vector2(0, 0);
            m_interactableObj.isInteractContinue = true;
            m_interactableObj.Interactive();
            isInteractive = true;
        }
        if (m_interactableObj != null && Input.GetKeyUp(KeyCode.F))
        {
            var wire = m_interactableObj.GetComponent<Wire>();
            if (wire != null && !wire.IsSuccess) return;
            var nonsense = m_interactableObj.GetComponent<Nonsense>();
            if (nonsense != null && !nonsense.IsSuccess && !MissionManager.Inst.GetIsWireAllSuccess()) return;
            m_interactableObj.isInteractContinue = false;
            isInteractive = false;
        }
    }

    void WeldingPointIndicatedObjCreate()
    {
        for (int i = 0; i < MissionManager.Inst.weldingPoints.Count; i++)
        {
            var obj = Instantiate(WeldingPoingIndicatedObjPrefab, transform);
            WeldingPointIndicatedObjs.Add(obj);
        }
    }

    void ShowIndicatedObjs()
    {
        if (MissionManager.Inst.weldingPoints != null && WeldingPointIndicatedObjs != null)
        {
            for (int i = 0; i < WeldingPointIndicatedObjs.Count; i++)
            {
                WeldingPointIndicatedObjs[i].SetActive(!MissionManager.Inst.weldingPoints[i].activeSelf);
                if (MissionManager.Inst.weldingPoints[i].activeSelf) continue;
                Vector3 direction = MissionManager.Inst.weldingPoints[i].transform.position - WeldingPointIndicatedObjs[i].transform.position;
                WeldingPointIndicatedObjs[i].transform.up = direction;
                WeldingPointIndicatedObjs[i].transform.localPosition = WeldingPointIndicatedObjs[i].transform.up * 1f;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        m_interactableObj = collision.GetComponent<InteractableObject>();

        if (m_interactableObj != null)
        {
            var wire = m_interactableObj.GetComponent<Wire>(); // 와이어
            if (wire != null && wire.IsSuccess) return;
            var nonsense = m_interactableObj.GetComponent<Nonsense>(); // 넌센스
            if (nonsense != null && (nonsense.IsSuccess || !MissionManager.Inst.GetIsWireAllSuccess())) return;
            var lifeSupportMachine = m_interactableObj.GetComponent<LifeSupportMachine>(); // 생명유지장치
            if (lifeSupportMachine != null && !MissionManager.Inst.IsCarryingOxygen) return;
            var oxygen = m_interactableObj.GetComponent<Oxygen>();// 산소
            if (oxygen != null && MissionManager.Inst.IsCarryingOxygen) return;
            var weldingPoint = m_interactableObj.GetComponent<WeldingPoint>();// 용접 포인트
            if (weldingPoint != null && weldingPoint.activeSelf) return;
            InteractText.gameObject.SetActive(true);
            MissionManager.Inst.IsInteractable = true;
            StartCoroutine(InteractTextEffect());
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        m_interactableObj = collision.GetComponent<InteractableObject>();

        if (m_interactableObj != null)
        {
            m_interactableObj = null;
            StopAllCoroutines();
            InteractText.gameObject.SetActive(false);
            MissionManager.Inst.IsInteractable = false;
        }
    }

    IEnumerator InteractTextEffect()
    {
        float alpha = 0;
        int reserve = 1; // reserve가 2 나머지 값이 0이면 반대로 2를 넘어서면 -2를 하여 다시 1로 만들어 reserve가 다시 반대로
        while (MissionManager.Inst.IsInteractable)
        {
            if (alpha >= 0.99f || alpha <= 0.01f) reserve++;

            alpha = Mathf.Lerp(alpha, reserve % 2 == 0 ? 0 : 1, Time.deltaTime * 2);
            if (reserve > 2) reserve -= 2;
            Debug.Log(alpha);
            InteractText.color = new Color(InteractText.color.r, InteractText.color.g, InteractText.color.b, alpha);
            yield return null;
        }
        yield return null;
    }
}
