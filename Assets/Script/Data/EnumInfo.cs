﻿
public class EnumInfo 
{
    public enum TargetTeam
    {
        Null,
        SameTeam,
        OppositeTeam,
        Both
    }

    public enum TeamType
    {
        Player,
        Opposite,
        Draw
    }

    public enum State
    {
        Null,
        Fire,
        Provoke,
        AttakSpeed,
        MoveSpeed,
        AttackPower,
        Ice,
        RepeatDamage,
        DodgeRate
    }
    public enum DamageType
    {
        Physic,
        Magic
    }
    public enum SelectUnitType
    {
        Null,
        Random
    }
    public enum ClassType
    {
        Null,
        Warrior,
        Archer,
        Wizard,
        Assassin,
        Supporter
    }

    public enum AIPlayType //AI Play 성향
    {
        Balance,
        Attack,
        Defence,
        CC,
        Buff,
        Reinforce
    }

    public enum AILevel
    {
        Low,
        MiddleLow,
        Middle,
        MIddleHight,
        Hight
    }

    public const int UnitFeatureTypeCount =8;
    public enum UnitFeatureType
    {
        Feature_PhysicDamage,
        Feature_MasicDamage,
        Feature_PhysicDefence,
        Feature_MagicResistance,
        Feature_CC,
        Feature_Buff,
        Feature_Distance,
        Feature_Range
    }

    public enum RangeExpression
    {
        Same, // 같은지확인
        Less, // 미만
        OrLess, // 이하
        OrMore, // 이상
        Over, //초과
        Default

    }

    public enum UnitStat
    {
        Hp,
        Defence,
        MagicResistance,
        Damage,
        SkillDamage,
        AttackSpeed,
        AttackDistance,
        DodgeRate,
        RecoverRate,
        CriticalRate,
        CriticalDamage,
        MaxMana,
        ManaChargeAmount,
        InitMana
    }

    public enum Arithmetic
    {
        Add,
        Multi,
        Subtraction,
        Division
    }

    public enum UnitBehaviourType
    {
        NORMAL, // 일반 행동 (공격)
        SKILL, // 마나 소비 행동 (스킬)
        PASSIVE, // 특정 조건 시 발동 되는 행동 (페시브)
        COMMON // 공용으로 많이 사용되는 행동
    }
    public enum EntityAnimType
    {
        NONE,
        LOOKAT
    }
}
