using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameStarter : MonoBehaviour
{
    [SerializeField] GameObject fadePanelObj; // <- 패널 오브젝트 자체
    [SerializeField] Image fadePanel;         // <- UI 이미지 (Image 컴포넌트)

    void Start()
    {
        Button startButton = GameObject.Find("GameStart").GetComponent<Button>();

        if (fadePanelObj != null)
            fadePanelObj.SetActive(false); // 시작 시 안 보이게

        startButton.onClick.AddListener(() =>
        {
            StartCoroutine(FadeOutAndLoadScene("TestJhin"));
        });
    }

    IEnumerator FadeOutAndLoadScene(string sceneName)
    {
        if (fadePanelObj != null)
            fadePanelObj.SetActive(true); // 버튼 클릭 시 패널 활성화

        float duration = 1.0f;
        float time = 0f;

        Color color = fadePanel.color;
        fadePanel.color = new Color(color.r, color.g, color.b, 0f); // 투명으로 시작

        while (time < duration)
        {
            time += Time.deltaTime;
            float alpha = Mathf.Lerp(0f, 1f, time / duration);
            fadePanel.color = new Color(color.r, color.g, color.b, alpha);
            yield return null;
        }

        fadePanel.color = new Color(color.r, color.g, color.b, 1f); // 완전 검정
        SceneManager.LoadScene(sceneName);
    }
}
