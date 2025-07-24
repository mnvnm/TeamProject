using UnityEngine;
using System.Collections;

public class Spark : MonoBehaviour
{
    [SerializeField] private float stunDuration    = 1f;   // 스턴 시간
    [SerializeField] private float immunityDuration = 0.5f; // 면역 시간
    [SerializeField] private bool freezeRigidbodyDuringStun = true;

    // 면역 상태 관리 (전역)
    private static bool isPlayerImmune = false;
    private static bool isPlayerStunned = false;

    // 플레이어 충돌 감지
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        if (isPlayerImmune || isPlayerStunned) return;

        if (other.CompareTag("Player") && !isPlayerImmune)
        {
            PlayerController pc = other.GetComponent<PlayerController>();
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
            if (pc != null && rb != null)
                StartCoroutine(HandleStun(pc, rb));
        }
    }

    // 스턴 + 면역 처리 코루틴
    private IEnumerator HandleStun(PlayerController pc, Rigidbody2D rb)
    {
        isPlayerStunned = true;

        // 1) 스턴 시작: 이동 스크립트 비활성화
        pc.enabled = false;
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;

        RigidbodyConstraints2D originalConstraints = rb.constraints;
        if (freezeRigidbodyDuringStun)
            rb.constraints = RigidbodyConstraints2D.FreezeAll;

        GameManager.Inst.game.player.StunAnimation(true);

        yield return new WaitForSeconds(stunDuration);

        // 2) 스턴 해제: 이동 스크립트 활성화
        pc.enabled = true;
        if (freezeRigidbodyDuringStun)
            rb.constraints = originalConstraints;
        GameManager.Inst.game.player.StunAnimation(false);

        // 3) 면역 시작
        isPlayerImmune = true;
        isPlayerStunned = false;
        yield return new WaitForSeconds(immunityDuration);

        // 4) 면역 해제
        isPlayerImmune = false;
    }
}