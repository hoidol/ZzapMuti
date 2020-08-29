using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lobby
{
    public class LobbyUnitSlot : MonoBehaviour
    {
        [Header("Slot")]
        [SerializeField] private LobbyUnitInfoUI[] _lobbySlotUI;
        private string[] _slotList = new string[8];
        public string[] _SlotList
        {
            get { return _slotList; }
        }
        private UnitData _selectingUnitData;

        [SerializeField] private GameObject _selectingObject;

        [Header("Regist Buttons")]
        [SerializeField] private LobbyUnitSlotRegistButton[] _lobbyUnitSlotRegistButtons;

        public void Awake()
        {
            //for (int i = 0; i < _lobbySlotUI.Length; i++)
            //    _lobbySlotUI[i].SetNull();

            for (int i = 0; i < _lobbyUnitSlotRegistButtons.Length; i++)
                _lobbyUnitSlotRegistButtons[i]._RegistEvent += ActiveSelect;
        }

        public void Start()
        {
            Load();
        }

        public void Load()
        {
            for (int i = 0; i < _lobbySlotUI.Length; i++)
                _lobbySlotUI[i].SetUI(DataManager.Instance.GetUnitDataWithIdx(int.Parse(UserDataSave.Instance.Data.unitSlot[i])));
        }

        public void OnDestroy()
        {
            for (int i = 0; i < _lobbyUnitSlotRegistButtons.Length; i++)
                _lobbyUnitSlotRegistButtons[i]._RegistEvent -= ActiveSelect;
        }

        public void ActiveSelect(UnitData _unitData)
        {
            _selectingUnitData = _unitData;
            _selectingObject.gameObject.SetActive(true);
        }

        public void SetUnitSlot(int _index)
        {
            _selectingObject.gameObject.SetActive(false);

            _slotList[_index] = _selectingUnitData.Idx.ToString();
            _lobbySlotUI[_index].SetUI(_selectingUnitData);
        }
    }
}