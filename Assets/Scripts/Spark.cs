using UnityEngine;
using System.Collections;

public class Spark : MonoBehaviour
{
    [SerializeField] private float stunDuration    = 1f;   // 스턴 시간
    [SerializeField] private float immunityDuration = 0.5f; // 면역 시간

    // 면역 상태 관리 (전역)
    private static bool isPlayerImmune = false;

    // 플레이어 충돌 감지
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isPlayerImmune)
        {
            PlayerController pc = other.GetComponent<PlayerController>();
            if (pc != null)
                StartCoroutine(HandleStun(pc));
        }
    }

    // 스턴 + 면역 처리 코루틴
    private IEnumerator HandleStun(PlayerController pc)
    {
        // 1) 스턴 시작: 이동 스크립트 비활성화
        pc.enabled = false;
        yield return new WaitForSeconds(stunDuration);

        // 2) 스턴 해제: 이동 스크립트 활성화
        pc.enabled = true;

        // 3) 면역 시작
        isPlayerImmune = true;
        yield return new WaitForSeconds(immunityDuration);

        // 4) 면역 해제
        isPlayerImmune = false;
    }
}