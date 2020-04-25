
[System.Serializable]
public class Damage 
{
    public Unit Unit;
    public DamageType Type;
    public float DamagePower;
}

public enum DamageType
{
    Physic,
    Magic
}
