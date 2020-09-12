using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brave : Synergy
{
    public override void InitSynergy()
    {
        base.InitSynergy();
        _synergyIdx = "Brave";
    }

    public override void ApplySynergy(Unit _u)
    {
        switch (_synergyCount)
        {
            case 2:
                
                break;
            case 4:
                
                break;
            case 6:

                break;
        }
    }
}
