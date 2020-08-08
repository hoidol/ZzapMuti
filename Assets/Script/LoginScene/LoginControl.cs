using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoginControl : MonoBehaviour
{
    [SerializeField] private InputField _emailInputField;
    [SerializeField] private InputField _passwordInputField;

    public void Create()
    {
        if (GameAuthControl.Instance.CreateUser(_emailInputField.text, _passwordInputField.text))
        {
            SceneManager.LoadScene("Lobby");
        }
    }

    public void Login()
    {
        if(GameAuthControl.Instance.Login(_emailInputField.text, _passwordInputField.text))
        {
            SceneManager.LoadScene("Lobby");
        }
    }

    public void Logisn()
    {
        GameAuthControl.Instance.LoginAnonymousEvent += CallLoginsn;
        GameAuthControl.Instance.LoginAnonymous();
    }

    public void CallLoginsn(bool _isSussces)
    {
        if(_isSussces)
        {
            ToLobby();
            Debug.Log("ASDFASDFE");
        }
        GameAuthControl.Instance.LoginAnonymousEvent -= CallLoginsn;
    }

    public void ToLobby()
    {
        SceneManager.LoadScene("Lobby");
    }

    public void Save()
    {

        UserDataSave.Instance.SaveUserData("S", new string[8] { "4", "4", "4", "4", "4", "4", "4", "4" });
    }

}
