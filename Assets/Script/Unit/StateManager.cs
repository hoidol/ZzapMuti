﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StateManager : MonoBehaviour
{
    Unit _unit;

    public bool _isLiving;
    public float _curHp;
    public float _curMana;
    public float _curShield;
    public Text _unitInfoText;

    public Image _hpBar;
    public Image _manaBar;
    public Image _shieldBar;

    public State[] _states;
    
    public void InitStateMgr(Unit _u)
    {
        _unit = _u;
        _curHp = _unit._unitData.Hp;
        _curMana = 0;
        _hpBar.fillAmount = 1;
        _manaBar.fillAmount = 0;
        _isLiving = true;
        _states = GetComponentsInChildren<State>();

        for (int i = 0; i < _states.Length; i++)
            _states[i].InitState(_u);

        _unitInfoText.text = "Lv." + _unit._unitData.ReinforceLv + " " + _unit._unitData.UnitName;
    }

    public void StartBattle()
    {
        _curHp = _unit._unitData.Hp;
        _isLiving = true;
        _curMana = 0;
        _hpBar.fillAmount = 1;
        _manaBar.fillAmount = _unit._unitData.InitMana;
        _shieldBar.fillAmount = 0;

        for (int i = 0; i < _states.Length; i++)
            _states[i].StartBattle();
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


        
        if(_curHp <=0)
        {
            _curHp = 0;
            _isLiving = false;
            _unit.Die();
            return;
        }

        ChargeMana();
        _hpBar.fillAmount = _curHp/ _unit._unitData.Hp;
    }


    public void ChangeState(ChangeState _cS)
    {
        GetState(_cS._changeState).ChangeState(_cS);
    }

    public State GetState(EnumInfo.State _state)
    {
        for(int i =0;i< _states.Length; i++)
        {
            if (_states[i]._state.Equals(_state))
            {
                return _states[i];
            }
        }
        return null;
    }



    public void HealHp(float _h)
    {
        _curHp += _h;

        if (_curHp > _unit._unitData.Hp)
            _curHp = _unit._unitData.Hp;

        _hpBar.fillAmount = _curHp / _unit._unitData.Hp;
    }

    public void ChargeShield(float _s)
    {
        _curShield += _s;
        if (_curShield > _unit._unitData.Hp)
            _curShield = _unit._unitData.Hp;

        _shieldBar.fillAmount = _s / _unit._unitData.Hp;
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
