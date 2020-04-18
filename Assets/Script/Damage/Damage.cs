
[System.Serializable]
public class Damage 
{
    public DamageType Type;
    public float DamagePower;
}

public enum DamageType
{
    Physic,
    Magic
}
