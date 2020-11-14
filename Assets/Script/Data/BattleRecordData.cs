[System.Serializable]
public class BattleRecordData
{
    public string Nick;
    public string[] UnitInven;
    public UnitTrackingDataContainer UnitTrackingDataContainer;
}
[System.Serializable]
public class BattleRecordDataContainer
{
    public BattleRecordData[] BattleRecordDatas;
}

[System.Serializable]
public class UnitTrackingData
{
    public int Round;
    public UnitTrackContainer UnitTrackContainer;
}

[System.Serializable]
public class UnitTrackingDataContainer
{
    public UnitTrackingData[] UnitTrackingDatas;
}

[System.Serializable]
public class UnitTrack
{
    public float TileX;
    public float TileY;
    public string UnitName;
    public int ReinforceLv;
}

[System.Serializable]
public class UnitTrackContainer
{
    public UnitTrack[] UnitTracks;
}