using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Lobby
{
    public class LobbyUnitInfoUI : MonoBehaviour
    {
        [Header("Info")]
        [SerializeField] private Text _unitNameText;
        [SerializeField] private Image _unitClassImage;
        [SerializeField] private Text _maxReinforceText;

        [SerializeField] private string _unitName;
        private UnitData _unitData;

        public void Start()
        {
            _unitData = DataManager.Instance.GetUnitDataWithUnitName(_unitName);

            InitUI();
        }

        public void InitUI()
        {
            _unitNameText.text = _unitData.UnitName;
            _unitClassImage.sprite = ClassIconContainer.Instance.GetClassIcon((EnumInfo.ClassType)System.Enum.Parse(typeof(EnumInfo.ClassType),_unitData.Class));

            _maxReinforceText.text = _unitData.MaxReinforce.ToString();
        }
    }
}