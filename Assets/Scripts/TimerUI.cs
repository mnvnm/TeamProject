using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerUI : MonoBehaviour
{

    [SerializeField] GameObject exitObj;   // 출구 오브젝트
    [SerializeField] GameObject doorObj;   // 출구문 오브젝트
    [SerializeField] float exitDelay = 60f; // 지연 시간(초)
    [SerializeField] TMPro.TextMeshProUGUI exitTimerText; // 남은 시간 표시용

    void Start()
    {
    }

    public void Init()
    {
        if (exitObj != null)
            exitObj.SetActive(false);

        if (exitTimerText != null)
            exitTimerText.gameObject.SetActive(false);
    }

    public IEnumerator ShowExitAfterDelay()
    {
        float remain = exitDelay;
        if (exitTimerText != null)
            exitTimerText.gameObject.SetActive(true);

        while (remain > 0f)
        {
            // 소수점 필요 없으면 CeilToInt, 필요하면 ToString("F1") 등
            if (exitTimerText)
                exitTimerText.text = $"[ RESCUE SHIP ARRIVES : {Mathf.CeilToInt(remain) } ]";

            yield return null;               // 매 프레임 갱신
            remain -= Time.deltaTime;
        }

        exitTimerText.gameObject.SetActive(false);

        if (exitObj != null)
            exitObj.SetActive(true);
        if (doorObj != null)
            doorObj.SetActive(false);
    }

}
