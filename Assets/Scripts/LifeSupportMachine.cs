using UnityEngine;

public class LifeSupportMachine : InteractableObject
{
    const float SUM_LIFE_SUPPORT_GAUGE = 40f;
    const float DECREASE_LIFE_SUPPORT_GAUGE = 0.6f;
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
    }

    public override void Init()
    {
        base.Init();
        MissionManager.Inst.LifeSupportGauge = MAX_LIFE_SUPPORT_GAUGE;
    }

    public override void Interactive()
    {
        if (MissionManager.Inst.IsCarryingOxygen)
        {
            MissionManager.Inst.LifeSupportGauge += SUM_LIFE_SUPPORT_GAUGE;
            if (MissionManager.Inst.LifeSupportGauge >= 100f) MissionManager.Inst.LifeSupportGauge = MAX_LIFE_SUPPORT_GAUGE;
            MissionManager.Inst.IsCarryingOxygen = false;
        }
    }

    void DecreaseLifeSupportGauge()
    {
        if (MissionManager.Inst.LifeSupportGauge <= 0f) return;
        MissionManager.Inst.LifeSupportGauge -= DECREASE_LIFE_SUPPORT_GAUGE * Time.deltaTime;
    }
}
