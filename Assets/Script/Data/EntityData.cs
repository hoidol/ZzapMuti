[System.Serializable]
public class EntityData 
{
    public string EntityIdx;
    public string EntityName;

    public int ReinforceLv;
    public float Duration;
    public float MoveSpeed;
}



[System.Serializable]
public class EntityDataContainer
{
    public EntityData[] EntityData;
}
