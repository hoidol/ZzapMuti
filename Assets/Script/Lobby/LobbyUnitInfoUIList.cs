using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lobby
{
    public class LobbyUnitInfoUIList : MonoBehaviour
    {
        [SerializeField] private LobbyUnitInfoUI[] _lobbyUnitInfoUIs;

        [SerializeField] private string[] _unitNameList;

        public void Start()
        {
            Init();
        }

        public void Init()
        {
            for(int i=0;i<_lobbyUnitInfoUIs.Length;i++)
            {
                _lobbyUnitInfoUIs[i].SetUI(DataManager.Instance.GetUnitDataWithUnitName(_unitNameList[i]));
            }
        }
    }
}