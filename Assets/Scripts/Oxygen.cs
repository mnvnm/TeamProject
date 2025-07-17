using UnityEngine;

public class Oxygen : InteractableObject
{
    public override void Init()
    {
        base.Init();
    }

    public override void Interactive()
    {
        if (isInteractContinue)
        {
            MissionManager.Inst.IsCarryingOxygen = true;
        }
        isInteractContinue = false;
    }
}
