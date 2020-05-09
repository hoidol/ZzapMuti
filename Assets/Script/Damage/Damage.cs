
[System.Serializable]
public class Damage 
{
    public Unit ResourceUnit; // 데미지를 주는 유닛
    public DamageType Type;
    public float DamagePower;
}

public enum DamageType
{
    Physic,
    Magic
}
