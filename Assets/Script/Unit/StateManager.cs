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
        _curHp = _unit._unitData.Hp;
        _curMana = 0;
        _hpBar.fillAmount = 1;
        _manaBar.fillAmount = 0;
    }

    public void StartBattle()
    {
        _curHp = _unit._unitData.Hp;
        _curMana = 0;
        _hpBar.fillAmount = 1;
        _manaBar.fillAmount = 0;
    }
    public void TakeDamage(Damage _d)
    {
        float _realDamage = 0;
        if(_d.Type == DamageType.Physic)
        {
            _realDamage = _d.DamagePower - _unit._unitData.Defence;
        }
        else if (_d.Type == DamageType.Magic)
        {
            _realDamage = _d.DamagePower - _unit._unitData.MagicResistance;

        }
        if (_realDamage <= 0)
            _realDamage = 0;

        _curHp -= _realDamage;

        Debug.Log("현재 체력 : " + _curHp + " 받은 데미지  : " + _d);
        
        if(_curHp <=0)
        {
            _curHp = 0;
            _unit.Die();
            return;
        }

        Debug.Log("맞아서 마나 충전하기");
        ChargeMana();
        _hpBar.fillAmount = _curHp/ _unit._unitData.Hp;
    }

    public void FinishBattle()
    {

        _curHp = _unit._unitData.Hp;
        _curMana = 0;
        _hpBar.fillAmount = 1;
        _manaBar.fillAmount = 0;
    }
    public void ChargeMana()
    {
        _curMana += _unit._unitData.ManaChargeAmount;
        if (_curMana >= _unit._unitData.MaxMana)
            _curMana = _unit._unitData.MaxMana;

        _manaBar.fillAmount = _curMana / _unit._unitData.MaxMana;
    }

    public void Die()
    {
        _hpBar.fillAmount = 0;
    }
}
