using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StateManager : MonoBehaviour
{
    Unit _unit;

    public float _curHp;
    public float _curMana;

    public Image _hpBar;
    public Image _manaBar;

    public void InitStateMgr(Unit _u)
    {
        _unit = _u;
        _curHp = _u._unitData.Hp;
        _curMana = 0;
    }

    public void TakeDamage(float _d)
    {
        _curHp -= _d;
        if(_curHp <=0)
        {
            _curHp = 0;
        }

        _hpBar.fillAmount = _curHp/ _unit._unitData.Hp;
    }


    public void ChargeMana()
    {
        _curMana += _unit._unitData.ManaChargeAmount;
        if (_curMana >= _unit._unitData.MaxMana)
            _curMana = _unit._unitData.MaxMana;

        _manaBar.fillAmount = _curMana / _unit._unitData.MaxMana;
    }
}
