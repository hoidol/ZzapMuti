using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lobby;
public class LobbyUnitSlotSave : MonoBehaviour
{
    [SerializeField] private LobbyUnitSlot lobbyUnitSlot;

    public void Save()
    {
        UserDataSave.Instance.SetUnitSlot(lobbyUnitSlot._SlotList);
    }
}
