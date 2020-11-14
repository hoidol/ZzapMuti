using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReinforceBehaviour : MonoBehaviour
{
    public int reinforce;
    public UnitBehaviour[] unitBehaviours;
    BehaviourManager behaviourMgr;
    public void InitReinforceBehaviour(BehaviourManager _bMgr)
    {
        behaviourMgr = _bMgr;
        unitBehaviours = GetComponentsInChildren<UnitBehaviour>();
        for(int i=0;i< unitBehaviours.Length; i++)
        {
            unitBehaviours[i].InitUnitBehaviour(behaviourMgr.unit);
        }
    }

    public void StartBattle()
    {

    }
}
