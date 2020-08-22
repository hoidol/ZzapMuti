
[System.Serializable]
public class UnitData 
{
    public int Idx;
    public string UnitIdx;
    public int ReinforceLv;
    public string UnitName;
    public string Class;
    public int LifeAttack;
    public int MaxReinforce;
    public float Hp;
    public float Defence;
    public float MagicResistance;
    public string DamageType;
    public float Damage;
    public float AttackSpeed;
    public float AttackDistance;
    public string SkillDamageType;
    public float SkillDamage;
    public float DodgeRate;
    public float RecoverRate;
    public float CriticalRate;
    public float CriticalDamage;
    public float MaxMana;
    public float ManaChargeAmount;
    public float InitMana;
    public int Feature_PhysicDamage;
    public int Feature_MasicDamage;
    public int Feature_PhysicDefence;
    public int Feature_MagicResistance;
    public int Feature_CC;
    public int Feature_Buff;
    public int Feature_Distance;
    public int Feature_Range;
    public int Feature_MoveSpeed;

    public string BestPosition;
}


[System.Serializable]
public class UnitDataContainer
{
    public UnitData[] UnitData;
}
