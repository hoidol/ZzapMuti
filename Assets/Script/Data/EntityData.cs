[System.Serializable]
public class EntityData 
{
    public string Idx;
    public float Duration;
    public float MoveSpeed;
}



[System.Serializable]
public class EntityDataContainer
{
    public EntityData[] EntityData;
}
