using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;

public class ExitAndFade_Tween : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private string endingSceneName = "EndingScene";
    [SerializeField] private float fadeDuration = 1.2f;
    [SerializeField] private bool oneShot = true;

    [Header("References")]
    [SerializeField] private Image fadeImage;
    private Collider2D triggerCollider;

    [SerializeField] private bool isRunning;

    void Reset()
    {
        triggerCollider = GetComponent<Collider2D>();
        if (triggerCollider) triggerCollider.isTrigger = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (isRunning && oneShot) return;
        if (!other.CompareTag("Player")) return;

        StartCoroutine(Process());
    }

    IEnumerator Process()
    {
        isRunning = true;

        // 1. 페이드 아웃
        yield return fadeImage.DOFade(1f, fadeDuration).WaitForCompletion();

        // 2. 씬 로드
        AsyncOperation op = SceneManager.LoadSceneAsync(endingSceneName);
        while (!op.isDone) yield return null;

        // 3. 페이드 인
        yield return fadeImage.DOFade(0f, fadeDuration).WaitForCompletion();

        if (!oneShot) isRunning = false;
    }
}