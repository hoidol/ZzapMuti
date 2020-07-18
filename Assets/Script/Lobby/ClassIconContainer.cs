using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassIconContainer : MonoBehaviour
{
    public static ClassIconContainer Instance;

    [SerializeField] private Sprite _warriorSprite;
    [SerializeField] private Sprite _archerSprite;
    [SerializeField] private Sprite _wizardSprite;
    [SerializeField] private Sprite _assassinSprite;
    [SerializeField] private Sprite _supporterSprite;

    public void Awake()
    {
        Instance = this;
    }

    public void OnDestroy()
    {
        Instance = null;
    }

    public Sprite GetClassIcon(EnumInfo.ClassType _type)
    {
        switch(_type)
        {
            case EnumInfo.ClassType.Warrior:
                return _warriorSprite;
            case EnumInfo.ClassType.Archer:
                return _archerSprite;
            case EnumInfo.ClassType.Wizard:
                return _wizardSprite;
            case EnumInfo.ClassType.Assassin:
                return _assassinSprite;
            case EnumInfo.ClassType.Supporter:
                return _supporterSprite;
        }

        return null;
    }
}
