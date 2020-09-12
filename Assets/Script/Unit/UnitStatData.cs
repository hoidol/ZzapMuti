
[System.Serializable]
public class UnitStatData //시너지 등등의 영향으로 바뀌는 캐릭터의 능력값
{
    public Unit _curUnit;

    public float Hp;
    public float Defence;
    public float MagicResistance;

    public string DamageType;
    public float Damage;

    public string SkillDamageType;
    public float SkillDamage;

    public float AttackSpeed;
    public float AttackDistance;

    public float DodgeRate;
    public float RecoverRate;
    public float CriticalRate;
    public float CriticalDamage;
    public float MaxMana;
    public float ManaChargeAmount;
    public float InitMana;


    public Damage _normalDamage;
    public Damage _skillDamage;

    public string Character;


    public void InitUnitStatData(Unit _u)
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
            _normalDamage.Type = EnumInfo.DamageType.Physic;
        else
            _normalDamage.Type = EnumInfo.DamageType.Magic;
        _normalDamage.DamagePower = _u._unitData.Damage;
        _normalDamage.ResourceUnit = _u;

        if (_u._unitData.SkillDamageType.Equals("Physic"))
            _skillDamage.Type = EnumInfo.DamageType.Physic;
        else
            _skillDamage.Type = EnumInfo.DamageType.Magic;

        _skillDamage.DamagePower = _u._unitData.SkillDamage;
        _skillDamage.ResourceUnit = _u;
    }
}
