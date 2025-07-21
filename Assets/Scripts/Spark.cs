using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider2D))]
public class Spark : MonoBehaviour
{
    [Tooltip("충돌 대상 오브젝트의 태그")]
    public string obstacleTag = "Obstacle";
    [Tooltip("멈춤 지속 시간(초)")]
    public float stopDuration = 1f;
    [Tooltip("스턴 후 면역 시간(초)")]
    public float immunityDuration = 1f;

    private bool isStopped = false;
    private PlayerMovement movementScript;

    void Awake()
    {
        // 이동 스크립트 이름을 본인 프로젝트에 맞춰 변경하세요.
        movementScript = GetComponent<PlayerMovement>(); // 예: GetComponent<PlayerMovement>()
        if (movementScript == null)
            Debug.LogWarning("StopOnTouch: 이동 스크립트를 찾지 못했습니다.");
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (!isStopped && col.gameObject.CompareTag(obstacleTag))
            StartCoroutine(DoStop());
    }

    IEnumerator DoStop()
    {
        isStopped = true;
        movementScript.enabled = false;    // 이동 비활성화
        yield return new WaitForSeconds(stopDuration);
        movementScript.enabled = true;     // 이동 재활성화
        yield return new WaitForSeconds(immunityDuration);
        isStopped = false;
    }
}