using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] TeamType _teamType;
    public TeamType _TeamType
    {
        get { return _teamType; }
    }

    [SerializeField] private float _maxHp;
    public float _MaxHp
    {
        get { return _maxHp; }
    }

    private float _hp;
    public float _Hp
    {
        get { return _hp; }
    }


    public void Init(TeamType _teamTy)
    {
        _teamType = _teamTy;
        _maxHp = 50;
        _hp = _maxHp;
    }
}
public enum TeamType
{
    Red,
    Blue
}
