using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NickNameInput : MonoBehaviour
{
    [SerializeField] private InputField _nicknameInputField;

    public void Start()
    {
        Init();
    }

    public void Init()
    {
        Debug.Log(UserDataSave.Instance.Data.nickName);
        _nicknameInputField.text = UserDataSave.Instance.Data.nickName;
    }

    public void RestoreNickname()
    {
        UserDataSave.Instance.SetNickName(_nicknameInputField.text);
    }
}
