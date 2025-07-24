using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ImageChanger : MonoBehaviour
{
    [SerializeField] List<Sprite> images = new List<Sprite>();
    [SerializeField] Image curImg;
    [SerializeField] Image fadePanel; // 검정 패널 UI 연결

    int curImgCount = 0;
    bool isTransitioning = false;

    public void OnClickNextImage()
    {
        if (isTransitioning) return;

        if (curImgCount < images.Count - 1)
        {
            StartCoroutine(FadeTransition());
        }
        else
        {
            StartCoroutine(FadeOutAndLoadScene("GameScene")); // ✅ 수정된 부분
        }
    }

    IEnumerator FadeTransition()
    {
        isTransitioning = true;

        yield return StartCoroutine(Fade(0f, 1f, 0.5f));

        curImgCount++;
        curImg.sprite = images[curImgCount];

        yield return StartCoroutine(Fade(1f, 0f, 0.5f));

        isTransitioning = false;
    }

    IEnumerator FadeOutAndLoadScene(string sceneName)
    {
        isTransitioning = true;

        yield return StartCoroutine(Fade(0f, 1f, 1.0f)); // 페이드 아웃

        SceneManager.LoadScene(sceneName); // 씬 전환
    }

    IEnumerator Fade(float fromAlpha, float toAlpha, float duration)
    {
        float time = 0f;
        Color color = fadePanel.color;

        while (time < duration)
        {
            time += Time.deltaTime;
            float alpha = Mathf.Lerp(fromAlpha, toAlpha, time / duration);
            fadePanel.color = new Color(color.r, color.g, color.b, alpha);
            yield return null;
        }

        fadePanel.color = new Color(color.r, color.g, color.b, toAlpha);
    }
}
