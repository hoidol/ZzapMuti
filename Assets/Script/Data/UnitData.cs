﻿
[System.Serializable]
public class UnitData 
{
    public string UnitIdx;
    public int ReinforceLv;
    public string UnitName;
    public string Class;
    public float Hp;
    public float Defence;
    public float AttackSpeed;
    public float AttackDistance;
    public float SkillDamage;
    public float DodgeRate;
    public float RecoverRate;
    public float CriticalRate;
    public float CriticalDamage;
    public float MaxMana;
    public float ManaChargeAmount;
}


[System.Serializable]
public class UnitDataContainer
{
    public UnitData[] UnitData;
}