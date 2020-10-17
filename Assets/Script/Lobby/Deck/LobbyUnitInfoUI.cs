using UnityEngine;
using UnityEngine.UI;

namespace Lobby
{
    public class LobbyUnitInfoUI : MonoBehaviour
    {
        [Header("Info")]
        [SerializeField] private Text _unitNameText;
        [SerializeField] private Image _unitClassImage;
        [SerializeField] private Image _unitClassShadowImage;
        [SerializeField] private Text _maxReinforceText;

        [SerializeField] private Image _nullImage;

        [SerializeField] private string _unitName;
        private UnitData _unitData;
        public UnitData _UnitData
        {
            get { return _unitData; }
        }

        public void SetNull()
        {
            _nullImage.gameObject.SetActive(true);
        }


        public void InitUI()
        {
            _nullImage.gameObject.SetActive(false);

            _unitNameText.text = _unitData.UnitName;
            _unitClassImage.sprite = ClassIconContainer.Instance.GetClassIcon((EnumInfo.ClassType)System.Enum.Parse(typeof(EnumInfo.ClassType),_unitData.Class));
            _unitClassShadowImage.sprite = _unitClassImage.sprite;
            _maxReinforceText.text = _unitData.MaxReinforce.ToString();
        }

        public void SetUI(UnitData _data)
        {
            _unitData = _data;

            _nullImage.gameObject.SetActive(false);

            _unitNameText.text = _data.UnitName;
            _unitClassImage.sprite = ClassIconContainer.Instance.GetClassIcon((EnumInfo.ClassType)System.Enum.Parse(typeof(EnumInfo.ClassType), _data.Class));
            _unitClassShadowImage.sprite = _unitClassImage.sprite;
            _maxReinforceText.text = _data.MaxReinforce.ToString();
        }
    }
}