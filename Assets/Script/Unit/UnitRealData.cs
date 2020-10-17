
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class UnitRealData //시너지 등등의 영향으로 바뀌는 캐릭터의 능력값
{
    public Unit _curUnit;

    public float Hp;
    public float Defence;
    public float MagicResistance;
    
    public float AttackSpeed;
    public float AttackDistance;

    public float DodgeRate;
    public float RecoverRate;
    public float CriticalRate;
    public float CriticalDamage;
    public float MaxMana;
    public float ManaChargeAmount;
    public float InitMana;


    public Damage normalDamage;
    public Damage skillDamage;

    public string Character;


    public void InitUnitRealData(Unit _u)
    {
        _curUnit = _u;
        Hp = _u._unitData.Hp;
        Defence = _u._unitData.Defence;
        MagicResistance = _u._unitData.MagicResistance;


        AttackSpeed = _u._unitData.AttackSpeed;
        AttackDistance = _u._unitData.AttackDistance;

        DodgeRate = _u._unitData.DodgeRate;
        RecoverRate = _u._unitData.RecoverRate;

        CriticalRate = _u._unitData.CriticalRate;
        CriticalDamage = _u._unitData.CriticalDamage;

        MaxMana = _u._unitData.MaxMana;
        ManaChargeAmount = _u._unitData.ManaChargeAmount;

        InitMana = _u._unitData.InitMana;

        if (_u._unitData.DamageType.Equals("Physic"))
            normalDamage.Type = EnumInfo.DamageType.Physic;
        else
            normalDamage.Type = EnumInfo.DamageType.Magic;
        normalDamage.DamagePower = _u._unitData.Damage;
        normalDamage.ResourceUnit = _u;

        if (_u._unitData.SkillDamageType.Equals("Physic"))
            skillDamage.Type = EnumInfo.DamageType.Physic;
        else
            skillDamage.Type = EnumInfo.DamageType.Magic;

        skillDamage.DamagePower = _u._unitData.SkillDamage;
        skillDamage.ResourceUnit = _u;
        unitStageChange.Clear();
    }

    public List<UnitStatChangeInfo> unitStageChange = new List<UnitStatChangeInfo>();
    public void ApplyUnitStatSynergyChange(UnitStatChangeInfo _uStat)
    {
        Debug.Log("ApplyUnitStatSynergyChange : " + _uStat.UnitStat + " Value : " + _uStat.Value);
        unitStageChange.Add(_uStat);
    }

    public void StartBattle()
    {
        Debug.Log("UnitRealData StartBattle()");
        for(int i =0;i< unitStageChange.Count; i++)
        {
            Debug.Log("UnitRealData StartBattle() for unitStageChange " + i+ "unitStageChange[i].UnitStat " + unitStageChange[i].UnitStat + " Value : " + unitStageChange[i].Value);
            switch (unitStageChange[i].UnitStat)
            {
                case EnumInfo.UnitStat.AttackDistance:
                    AttackDistance = CaluteArlthmethic(AttackDistance, unitStageChange[i]);
                    break;
                case EnumInfo.UnitStat.Hp:
                    Hp = CaluteArlthmethic(Hp, unitStageChange[i]);
                    break;
                case EnumInfo.UnitStat.Defence:
                    
                    Defence = CaluteArlthmethic(Defence, unitStageChange[i]);
                    break;
                case EnumInfo.UnitStat.MagicResistance:
                    MagicResistance = CaluteArlthmethic(MagicResistance, unitStageChange[i]);
                    break;
                case EnumInfo.UnitStat.Damage:
                    normalDamage.DamagePower = CaluteArlthmethic(normalDamage.DamagePower, unitStageChange[i]);
                    break;
                case EnumInfo.UnitStat.SkillDamage:
                    skillDamage.DamagePower = CaluteArlthmethic(skillDamage.DamagePower, unitStageChange[i]);
                    break;
                case EnumInfo.UnitStat.AttackSpeed:
                    AttackSpeed = CaluteArlthmethic(AttackSpeed, unitStageChange[i]);
                    break;
                case EnumInfo.UnitStat.DodgeRate:
                    DodgeRate = CaluteArlthmethic(DodgeRate, unitStageChange[i]);
                    break;
                case EnumInfo.UnitStat.RecoverRate:
                    RecoverRate = CaluteArlthmethic(RecoverRate, unitStageChange[i]);
                    break;
                case EnumInfo.UnitStat.CriticalRate:
                    CriticalRate = CaluteArlthmethic(CriticalRate, unitStageChange[i]);
                    break;
                case EnumInfo.UnitStat.CriticalDamage:
                    CriticalDamage = CaluteArlthmethic(CriticalDamage, unitStageChange[i]);
                    break;
                case EnumInfo.UnitStat.MaxMana:
                    MaxMana = CaluteArlthmethic(MaxMana, unitStageChange[i]);
                    break;
                case EnumInfo.UnitStat.ManaChargeAmount:
                    ManaChargeAmount = CaluteArlthmethic(ManaChargeAmount, unitStageChange[i]);
                    break;
                case EnumInfo.UnitStat.InitMana:
                    InitMana = CaluteArlthmethic(InitMana, unitStageChange[i]);
                    break;
            }
        }
    }


    public float CaluteArlthmethic(float _sV,UnitStatChangeInfo _info)
    {
        switch (_info.Arithmetic)
        {
            case EnumInfo.Arithmetic.Add:
                _sV += _info.Value;
                break;
            case EnumInfo.Arithmetic.Multi:
                _sV *= _info.Value;
                break;
            case EnumInfo.Arithmetic.Subtraction:
                _sV -= _info.Value;
                break;
            case EnumInfo.Arithmetic.Division:
                _sV /= _info.Value;
                break;
        }

        return _sV;
    }
}
[System.Serializable]
public class UnitStatChangeInfo{
    public EnumInfo.UnitStat UnitStat;
    public EnumInfo.Arithmetic Arithmetic;
    public float Value;
}
