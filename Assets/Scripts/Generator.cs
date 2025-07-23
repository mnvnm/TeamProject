using UnityEngine;

public class Generator : InteractableObject
{
    const float SUM_GENERATOR_GAUGE = 10.0f;
    const float DECREASE_GENERATOR_GAUGE = 0.5f;
    const float MAX_GENERATOR_GAUGE = 100;
    void Start()
    {
        Init();
    }

    void Update()
    {
        DecreaseGeneratorGauge();
        Interactive();
    }

    public override void Init()
    {
        base.Init();
        MissionManager.Inst.GeneratorGauge = MAX_GENERATOR_GAUGE;
    }

    public override void Interactive()
    {
        if (MissionManager.Inst.GeneratorGauge < MAX_GENERATOR_GAUGE && isInteractContinue)
        {
            base.Interactive();
            MissionManager.Inst.GeneratorGauge += SUM_GENERATOR_GAUGE * Time.deltaTime;
        }

    }

    void DecreaseGeneratorGauge()
    {
        if (MissionManager.Inst.GeneratorGauge <= 0) return;
        if (!isInteractContinue)
        {
            MissionManager.Inst.GeneratorGauge -= DECREASE_GENERATOR_GAUGE * Time.deltaTime;
        }
    }
}
