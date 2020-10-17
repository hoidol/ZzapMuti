using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyDataToInGame : MonoBehaviour
{
    public static UserData _playerData;
    public static UserData _opponentData;

    public void Start()
    {
        UserData _oD = new UserData();
        _oD.nickName = "AITester";
        _oD.UnitInven = new string[8];
        _oD.UnitInven[0] = "1";
        _oD.UnitInven[1] = "2";
        _oD.UnitInven[2] = "3";
        _oD.UnitInven[3] = "4";
        _oD.UnitInven[4] = "5";
        _oD.UnitInven[5] = "6";
        _oD.UnitInven[6] = "7";
        _oD.UnitInven[7] = "8";
        _opponentData = _oD;
        UserData _uD = new UserData();
        _oD.nickName = "AITester";
        _oD.UnitInven = new string[8];
        _oD.UnitInven[0] = "1";
        _oD.UnitInven[1] = "2";
        _oD.UnitInven[2] = "3";
        _oD.UnitInven[3] = "4";
        _oD.UnitInven[4] = "5";
        _oD.UnitInven[5] = "6";
        _oD.UnitInven[6] = "7";
        _oD.UnitInven[7] = "8";
        _playerData = _uD;
        PlayerManager.Instance.InitPlayerMgr(_playerData, _opponentData);
    }
}
