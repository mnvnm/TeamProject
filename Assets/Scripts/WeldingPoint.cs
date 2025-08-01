using System.Collections;
using UnityEngine;

public class WeldingPoint : InteractableObject
{
    [SerializeField] GameObject SuccessObj;
    [SerializeField] GameObject NeedWeldingObj;
    const float INTERACTIVE_SECOND = 3f;
    float interactiveCurTime = 0;

    AudioSource Audio;
   [SerializeField] AudioClip boomClip;
   [SerializeField] AudioClip fixClip;

    public bool activeSelf = true; // 평상시에는 비활성화 되어있다가 랜덤한 시간에 활성화를 위한 논리변수
    void Start()
    {
        Audio = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (GameManager.Inst.IsGameOver)
        {
            StopCoroutine(Active());
            return;
        }
        Interactive();
    }
    public override void Init()
    {
        base.Init();
        activeSelf = true;
        StartCoroutine(Active());
        SuccessObj.SetActive(true);
        NeedWeldingObj.SetActive(false);
    }

    public override void Interactive()
    {
        base.Interactive();
        if (isInteractContinue)
        {
            interactiveCurTime += Time.deltaTime;
            GameManager.Inst.game.player.SetSliderToWeldingPointInteract(interactiveCurTime, INTERACTIVE_SECOND);
        }
        else interactiveCurTime = 0;


        if (interactiveCurTime >= INTERACTIVE_SECOND && isInteractContinue)
        {
            StopCoroutine(Active());
        if (Audio != null) Audio.PlayOneShot(fixClip);
            GameManager.Inst.game.player.HideInteractSlider();
            activeSelf = true;
            isInteractContinue = false;
            StartCoroutine(Active());
        }
        SuccessObj.SetActive(activeSelf);
        NeedWeldingObj.SetActive(!activeSelf);
    }

    IEnumerator Active()
    {
        float randomTime = 30f;
        if (activeSelf)
        {
            randomTime = Random.Range(30f, 600f);
            Debug.Log(randomTime + "초 후에 용접 생성");
            yield return new WaitForSeconds(randomTime);
        }
        if (Audio != null) Audio.PlayOneShot(boomClip);
        activeSelf = false;
        GameManager.Inst.CameraShakeWelding();
        yield return null;
    }
}
