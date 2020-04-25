﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Player
{
    [SerializeField] EnumInfo.TeamType _teamType;
    public EnumInfo.TeamType _TeamType
    {
        get { return _teamType; }
    }

    [SerializeField] private float _maxHp;
    public float _MaxHp
    {
        get { return _maxHp; }
    }

    private float _hp;
    public float _Hp
    {
        get { return _hp; }
        set { _hp = value; }
    }

    [SerializeField] private PlayerDeckManager _deckManager=new PlayerDeckManager();
    public PlayerDeckManager _DeckManager
    {
        get { return _deckManager; }
    }

    public void Init(EnumInfo.TeamType _teamTy)
    {
        _teamType = _teamTy;
        _maxHp = 50;
        _hp = _maxHp;

        _deckManager.SetAllDeck();
    }
}