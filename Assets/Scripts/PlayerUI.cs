using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] Slider lifeSupportGaugeBar;
    [SerializeField] Slider generatorGaugeBar;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void Init()
    {
        lifeSupportGaugeBar.value = 1;
        generatorGaugeBar.value = 1;
    }

    // Update is called once per frame
    void Update()
    {
        lifeSupportGaugeBar.value = MissionManager.Inst.LifeSupportGauge / MissionManager.Inst.MaxLifeSupportGauge;
        generatorGaugeBar.value = MissionManager.Inst.GeneratorGauge / MissionManager.Inst.MaxGeneratorGauge;
    }
}
