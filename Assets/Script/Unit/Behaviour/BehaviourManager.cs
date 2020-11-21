using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourManager : MonoBehaviour
{
    public Unit unit;

    BehaviourContainer[] behaviourContainers;
    public BehaviourContainer curBehaviourContainer;
    [SerializeField] bool _ableToCallNormalBehaviour = false;
    public virtual void InitBehaviourMgr(Unit _u)
    {
        unit = _u;
        behaviourContainers = GetComponentsInChildren<BehaviourContainer>();
        for (int i = 0; i < behaviourContainers.Length; i++)
        {
            behaviourContainers[i].InitBehaviourContainer(_u, i + 1);
            behaviourContainers[i].gameObject.SetActive(false);
        }
    }

    public virtual void SetUserData(UnitData _uData, EnumInfo.TeamType _tType)
    {
        curBehaviourContainer = behaviourContainers[_uData.ReinforceLv - 1];
        curBehaviourContainer.SetUserData(_uData, _tType);
        curBehaviourContainer.gameObject.SetActive(true);
    }

    public virtual void StartBattle()
    {
        _ableToCallNormalBehaviour = false;
        curBehaviourContainer.StartBattle();
    }


    public virtual void StartBehaviour()
    {

        //NonTargetBehaviour있으면 돌리고 없으면 돌리지마
        curBehaviourContainer.StartBehaviour();
    }
    
    public virtual void NormalBehaviour(Unit _targetUnit = null)
    {
        curBehaviourContainer.NormalBehaviour(_targetUnit);
    }

    public virtual void SkillBehaviour(Unit _targetUnit = null)
    {
        curBehaviourContainer.SkillBehaviour(_targetUnit);
    }

    public virtual void PassiveBehaviour(Unit _targetUnit = null)
    {
        curBehaviourContainer.PassiveBehaviour(_targetUnit);
    }

    void ResetCoolTime()
    {
        _ableToCallNormalBehaviour = true;
    }


    public void Die()
    {
        StopAllCoroutines();
    }




    public void FinishBattle()
    {

    }
}
