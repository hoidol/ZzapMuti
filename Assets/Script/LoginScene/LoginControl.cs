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
        GameAuthControl.Instance.LoginAnonymousEvent += CallLoginsn;
        StartCoroutine(LoginRoutine());
    }

    private bool isLogined = false;

    public void CallLoginsn(bool _isSussces)
    {
        Debug.Log("CAll Login");
        isLogined = true;
    }


    public IEnumerator LoginRoutine()
    {
        GameAuthControl.Instance.LoginAnonymous();

        yield return new WaitWhile(() => { return !isLogined; });
        UserDataSave.Instance.LoadUserData();
        yield return new WaitWhile(() => { return !UserDataSave.Instance.IsInit; });

        GameAuthControl.Instance.LoginAnonymousEvent -= CallLoginsn;
        SceneManager.LoadScene("Lobby");

    }
    

}
