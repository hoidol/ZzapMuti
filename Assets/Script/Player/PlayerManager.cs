using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }


    public void InitPlayerMgr()
    {
        //내 덱과 상대방 덱 가져오기


    }

}
