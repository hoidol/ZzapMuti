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
        _redPlayer.Init(TeamType.Red);
        _bluePlayer.Init(TeamType.Blue);
    }

    public void SetDrawDeck()
    {

    }

    public void StartBattle()
    {

    }

    public void EndBattle()
    {

    }

    public void EndGame()
    {

    }
}
