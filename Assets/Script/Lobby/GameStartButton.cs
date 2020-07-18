using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Lobby
{
    public class GameStartButton : MonoBehaviour
    {
        [SerializeField] private LobbyUnitSlot _lobbyUnitSlot;

        private UserData _userData;
        private UserData _opponentData;

        public void GameStart()
        {
            _userData = new UserData()
            {
                nickName = "II긔요믜II",
                UnitInven = _lobbyUnitSlot._SlotList
            };
            _opponentData = new UserData()
            {
                nickName = "Oㅕ기ZI존",
                UnitInven = _lobbyUnitSlot._SlotList
            };
            LobbyDataToInGame._playerData = _userData;
            LobbyDataToInGame._opponentData = _opponentData;

            SceneManager.LoadScene("MinokScene");
        }
    }
}