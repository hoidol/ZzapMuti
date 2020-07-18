using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lobby
{
    public class LobbyUnitSlotRegistButton : MonoBehaviour
    {
        [SerializeField] private LobbyUnitInfoUI _lobbyUnitInfoUI;

        public event System.Action<UnitData> _RegistEvent;

        public void OnClick()
        {
            _RegistEvent?.Invoke(_lobbyUnitInfoUI._UnitData);
        }
    }
}