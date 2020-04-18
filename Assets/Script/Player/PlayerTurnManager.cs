﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurnManager : MonoBehaviour
{
    [SerializeField] private Player redPlayer;
    [SerializeField] private Player bluePlayer;

    private static PlayerTurnManager _instance;
    public static PlayerTurnManager _Instance
    {
        get { return _instance; }
    }

    private TeamType _createUnitPlayer;
    public TeamType _CreateUnitPlayer
    {
        get { return _createUnitPlayer; }
    }

    public void Awake()
    {
        _instance = this;

        redPlayer.Init(TeamType.Red);
        bluePlayer.Init(TeamType.Blue);
    }

    public void ComplateCreateUnit()
    {
        if (_createUnitPlayer == TeamType.Red)
            _createUnitPlayer = TeamType.Blue;
        else if (_createUnitPlayer == TeamType.Blue)
            _createUnitPlayer = TeamType.Red;
    }
}