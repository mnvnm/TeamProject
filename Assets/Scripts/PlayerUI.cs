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

    // Update is called once per frame
    void Update()
    {
        lifeSupportGaugeBar.value = MissionManager.Inst.LifeSupportGauge / MissionManager.Inst.MaxLifeSupportGauge;
        generatorGaugeBar.value = MissionManager.Inst.GeneratorGauge / MissionManager.Inst.MaxGeneratorGauge;
    }
}
