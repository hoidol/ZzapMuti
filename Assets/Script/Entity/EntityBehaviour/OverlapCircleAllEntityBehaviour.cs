using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverlapCircleAllEntityBehaviour : EntityBehaviour
{
    public NextEntityBehaviourWithTag _nextBehaviourWithTriggerTarget;

    public float _radius;
    public Transform _pointTr;

    public override void InitEntityBehaviour(Entity _e)
    {
        base.InitEntityBehaviour(_e);
        if (_nextBehaviourWithTriggerTarget.nextBehaviour != null)
            _nextBehaviourWithTriggerTarget.nextBehaviour.InitEntityBehaviour(_e);

    }
    public override void DoBehaviour(Vector2 _v)
    {
        ProcessOerlapCircle(Physics2D.OverlapCircleAll(_v, _radius));
    }
    public override void DoBehaviour(Unit _tUnit)
    {
        ProcessOerlapCircle(Physics2D.OverlapCircleAll(_tUnit._tr.position, _radius));
    }

    void ProcessOerlapCircle(Collider2D[] _cols)
    {
        for (int i = 0; i < _cols.Length; i++)
        {
            if (!_cols[i].CompareTag("Unit"))
                continue;

            Unit _tUnit = _cols[i].GetComponent<Unit>();

            switch (_nextBehaviourWithTriggerTarget.TargetTeam)
            {
                case EnumInfo.TargetTeam.OppositeTeam:
                    if (!entity.ownUnit._teamType.Equals(_tUnit._teamType))
                        _nextBehaviourWithTriggerTarget.nextBehaviour.DoBehaviour(_tUnit);

                    break;
                case EnumInfo.TargetTeam.SameTeam:
                    if (entity.ownUnit._teamType.Equals(_tUnit._teamType))
                        _nextBehaviourWithTriggerTarget.nextBehaviour.DoBehaviour(_tUnit);
                    break;
                case EnumInfo.TargetTeam.Both:
                    _nextBehaviourWithTriggerTarget.nextBehaviour.DoBehaviour(_tUnit);
                    break;
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, _radius);
    }
}


[System.Serializable]
public class NextEntityBehaviourWithTag
{
    public EnumInfo.TargetTeam TargetTeam;
    public EntityBehaviour nextBehaviour;

}