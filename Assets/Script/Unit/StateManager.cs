using System.Collections;
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


    DodgeRateState _dodgeRateState;

    float _penaltyDamage;
    public void InitStateMgr(Unit _u)
    {
        _unit = _u;
        _curHp = _unit.unitRealData.Hp;
        _curMana = _unit.unitRealData.InitMana;
        _curShield = 0;
        _hpBar.fillAmount = 1;
        _manaBar.fillAmount = _curMana / _unit.unitRealData.MaxMana;
        _shieldBar.fillAmount = 0;

        _penaltyDamage = _unit.unitRealData.Hp * 0.1f;

        _isLiving = true;
        _states = GetComponentsInChildren<State>();

        for (int i = 0; i < _states.Length; i++)
            _states[i].InitState(_u);

        _unitInfoText.text = "Lv." + _unit._unitData.ReinforceLv + " " + _unit._unitData.UnitName;
        _dodgeRateState = (DodgeRateState)GetState(EnumInfo.State.DodgeRate);
    }

    public void StartBattle()
    {
        _isLiving = true;

        _curHp = _unit.unitRealData.Hp;
        _curMana = _unit.unitRealData.InitMana;
        _curShield = 0;

        _hpBar.fillAmount = 1;
        _manaBar.fillAmount = _curMana / _unit.unitRealData.MaxMana;
        _shieldBar.fillAmount = 0;

        for (int i = 0; i < _states.Length; i++)
            _states[i].StartBattle();
    }

    public void TakeDamage(Damage _d)
    {
        if (_d == null)
            return;

        if (!UnitManager.Instance._playingBattle)
            return;

        if (!_d.unableToDodge)
        {
            if (Random.Range(0, 100) < _unit.unitRealData.DodgeRate * _dodgeRateState.GetDodgeRate())// 1 ~ 100까지 수
            {
                // 회피
                Debug.Log("회피!!");

                return;
            }
        }
        
        float _realDamage = 0;
        if(_d.Type == EnumInfo.DamageType.Physic)
            _realDamage = _d.DamagePower - _unit.unitRealData.Defence;
        else if (_d.Type == EnumInfo.DamageType.Magic)
            _realDamage = _d.DamagePower - (_d.DamagePower *(_unit.unitRealData.MagicResistance)*0.01f);

        if (_realDamage <= 0)
            _realDamage = 0;

        if(_curShield > 0)
        {
            _curShield -= _realDamage;
            if (_curShield <= 0)
            {
                _curHp += _curShield;
                _curShield = 0;
            }
        }
        else
            _curHp -= _realDamage;
        
        if(_curHp <=0)
        {
            _curHp = 0;
            _isLiving = false;
            _unit.Die();
            return;
        }

        ChargeMana();
        UpdateBar();
    }

    void UpdateBar()
    {
        _hpBar.fillAmount = _curHp / _unit.unitRealData.Hp;
        _shieldBar.fillAmount = _curShield / _unit.unitRealData.Hp;
        _manaBar.fillAmount = _curMana / _unit.unitRealData.MaxMana;
    }

    public void ChangeState(ChangeState _cS)
    {
        if (_cS == null)
            return;

        GetState(_cS._changeState).ChangeState(_cS);
    }

    public void Penalty()
    {        
        if (_curShield > 0)
        {
            _curShield -= _penaltyDamage;
            if (_curShield <= 0)
            {
                _curHp += _curShield;
                _curShield = 0;
            }
        }
        else
            _curHp -= _penaltyDamage;

        if (_curHp <= 0)
        {
            _curHp = 0;
            _isLiving = false;
            _unit.Die();
            return;
        }
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

        if (_curHp > _unit.unitRealData.Hp)
            _curHp = _unit.unitRealData.Hp;

        _hpBar.fillAmount = _curHp / _unit.unitRealData.Hp;
    }

    public void ChargeShield(float _s)
    {
        _curShield += _s;
        if (_curShield > _unit.unitRealData.Hp)
            _curShield = _unit.unitRealData.Hp;

        _shieldBar.fillAmount = _s / _unit.unitRealData.Hp;
    }
         
    public void FinishBattle()
    {

        _curHp = _unit.unitRealData.Hp;
        _curMana = 0;
        _hpBar.fillAmount = 1;
        _manaBar.fillAmount = 0;
    }
    public void ChargeMana()
    {
        _curMana += _unit.unitRealData.ManaChargeAmount;
        if (_curMana >= _unit.unitRealData.MaxMana)
            _curMana = _unit.unitRealData.MaxMana;

        _manaBar.fillAmount = _curMana / _unit.unitRealData.MaxMana;
    }

    public void ConsumeAllMana()
    {
        _curMana = 0;
        _manaBar.fillAmount = _curMana / _unit.unitRealData.MaxMana;
    }
    public void ConsumeMana(float _m)
    {
        _curMana -= _m;
        if (_curMana <= 0)
            _curMana = 0;
        _manaBar.fillAmount = _curMana / _unit.unitRealData.MaxMana;
    }
    public void Die()
    {
        _hpBar.fillAmount = 0;
    }
}
