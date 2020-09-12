using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SynergyInfoPanel : MonoBehaviour
{
    public RectTransform _rectTr;
    public Image _synergyInfoImage;

    public Text _synergyCountText;
     public CharacterCount _charCount;
    public SynergyData _synergyData;
    public void InitSynergyInfo()
    {
        _rectTr = GetComponent<RectTransform>();
    }

    public void UpdateSynergyInfo(CharacterCount _cData)
    {
        _charCount = _cData;
        _synergyCountText.text = _cData.NumberOfUnit.ToString();
       // _synergyInfoImage.sprite = Resources.Load<Sprite>(_cData.CharacterInfoData.Character)
    }

    public void OnClickedSynergyInfoBtn()
    {

    }



}
