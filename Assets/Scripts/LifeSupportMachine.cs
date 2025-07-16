using UnityEngine;

public class LifeSupportMachine : InteractableObject
{
    const float SUM_LIFE_SUPPORT_GAUGE = 4.0f;
    const float DECREASE_LIFE_SUPPORT_GAUGE = 2.0f;
    const float MAX_LIFE_SUPPORT_GAUGE = 100;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        DecreaseLifeSupportGauge();
        Interactive();
    }

    public override void Init()
    {
        base.Init();
        MissionManager.Inst.LifeSupportGauge = MAX_LIFE_SUPPORT_GAUGE;
    }

    public override void Interactive()
    {
        isInteractContinue = false;
        if (MissionManager.Inst.LifeSupportGauge >= MAX_LIFE_SUPPORT_GAUGE || !MissionManager.Inst.IsInteractable) return;

        if (Input.GetMouseButton(0) || Input.GetKey(KeyCode.F))
        {
            if (MissionManager.Inst.LifeSupportGauge < MAX_LIFE_SUPPORT_GAUGE)
            {
                base.Interactive();
                MissionManager.Inst.LifeSupportGauge += SUM_LIFE_SUPPORT_GAUGE * Time.deltaTime;
            }
        }

    }

    void DecreaseLifeSupportGauge()
    {
        if(!isInteractContinue)
            MissionManager.Inst.LifeSupportGauge -= DECREASE_LIFE_SUPPORT_GAUGE * Time.deltaTime;
    }
}
