using UnityEngine;
using Mono;

public class InteractableObject : MonoBehaviour, IInteractable
{
    public float interactableDistance = 3.5f; // 플레이어가 상호작용 할 수 있는 거리

    public bool isInteractContinue = false; // 현재 상호작용 중인지 확인하는 논리 변수
    // 각 상호작용 오브젝트 마다 가지고 있으며, true라면 상호작용하도록 함

    CircleCollider2D interactable_trigger;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public virtual void Init() // 초기화 함수
    {
        isInteractContinue = false;
        interactable_trigger = GetComponent<CircleCollider2D>();
        if (interactable_trigger != null)
        {
            interactable_trigger.radius = interactableDistance;
            interactable_trigger.isTrigger = true;
        }
    }

    public virtual void Interactive()
    {
    }
}
