using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;

    public UserData playerData;
    public UserData opponentData;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }


    public void InitPlayerMgr(UserData _pData,UserData _oData)
    {
        //내 덱과 상대방 덱 가져오기
        playerData = _pData;
        opponentData = _oData;

        AIManager.Instance.SetOpponentData(_oData);        
    }



}
