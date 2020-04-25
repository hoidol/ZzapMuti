using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurnManager : MonoBehaviour
{
    private Player redPlayer;
    private Player bluePlayer;

    private static PlayerTurnManager _instance;
    public static PlayerTurnManager _Instance
    {
        get { return _instance; }
    }

    private EnumInfo.TeamType _createUnitPlayer;
    public EnumInfo.TeamType _CreateUnitPlayer
    {
        get { return _createUnitPlayer; }
    }
}
