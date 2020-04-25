using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameProgress : MonoBehaviour
{
    [Header("Red Player")]
    [SerializeField] private Player _redPlayer;

    [Header("Blue Player")]
    [SerializeField] private Player _bluePlayer;

    [SerializeField] private PlayerDrawManager _playerDrawManager;

    private int _round=0;

    public void Start()
    {
        Init();
    }

    public void Init()
    {
        _redPlayer.Init(EnumInfo.TeamType.Red);
        _bluePlayer.Init(EnumInfo.TeamType.Blue);
    }

    public void SetDrawDeck()
    {

    }

    public void StartBattle()
    {
        UnitManager.Instance.StartBattle();
    }

    public void EndBattle(EnumInfo.TeamType _winTeam,int _discountLife)
    {

    }

    public void EndGame()
    {

    }
}
