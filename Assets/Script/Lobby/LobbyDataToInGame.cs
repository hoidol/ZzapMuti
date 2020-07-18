using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyDataToInGame : MonoBehaviour
{
    public static UserData _playerData;
    public static UserData _opponentData;

    public void Start()
    {
        PlayerManager.Instance.InitPlayerMgr(_playerData, _opponentData);
    }
}
