using System.Collections;
using UnityEngine;

public class WeldingPoint : InteractableObject
{
    [SerializeField] GameObject SuccessObj;
    const float INTERACTIVE_SECOND = 3f;
    float interactiveCurTime = 0;

    public bool activeSelf = false; // 평상시에는 비활성화 되어있다가 랜덤한 시간에 활성화를 위한 논리변수
    void Start()
    {
        Init();
    }

    void Update()
    {
        Interactive();
    }
    public override void Init()
    {
        base.Init();
        activeSelf = true;
        StartCoroutine(Active());
        SuccessObj.SetActive(true);
    }

    public override void Interactive()
    {
        base.Interactive();
        if (isInteractContinue) interactiveCurTime += Time.deltaTime;
        else interactiveCurTime = 0;

        if (interactiveCurTime >= INTERACTIVE_SECOND && isInteractContinue)
        {
            activeSelf = true;
        }
        SuccessObj.SetActive(activeSelf);
    }

    IEnumerator Active()
    {
        if (activeSelf)
        {
            float randomTime = Random.Range(15f, 60f);
            yield return new WaitForSeconds(randomTime);
        }
        activeSelf = false;
        while (!activeSelf)
        {
            yield return new WaitForSeconds(10f);
        }
    }
}
