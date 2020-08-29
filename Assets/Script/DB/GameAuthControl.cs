using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using Firebase.Unity.Editor;

public class GameAuthControl : MonoBehaviour
{
    private static GameAuthControl instance;
    public static GameAuthControl Instance
    {
        get { return instance; }
    }

    private FirebaseAuth auth;
    public FirebaseAuth Auth
    {
        get { return auth; }
    }

    private FirebaseUser user;
    public FirebaseUser User
    {
        get { return user; }
    }

    public event System.Action<bool> LoginAnonymousEvent;

    public void Awake()
    {
        instance = this;

        DontDestroyOnLoad(gameObject);

        InitializeFirebase();
    }

    public void OnDestroy()
    {
        //auth.SignOut();
    }

    // Handle initialization of the necessary firebase modules:
    void InitializeFirebase()
    {
        Debug.Log("Setting up Firebase Auth");
        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
    }

    public bool CreateUser(string _email,string _password)
    {
        bool isSucces = false;
        auth.CreateUserWithEmailAndPasswordAsync(_email, _password).ContinueWith(task => {
            if (task.IsCanceled)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
                isSucces = false;
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                isSucces = false;
                return;
            }

            // Firebase user has been created.
            Firebase.Auth.FirebaseUser newUser = task.Result;
            Debug.LogFormat("Firebase user created successfully: {0} ({1})",
                newUser.DisplayName, newUser.UserId);

            isSucces = true;
        });

        return isSucces;
    }

    public void ChangeDisplayName(string _name)
    {
        UserProfile userProfile = new UserProfile();
        userProfile.DisplayName = _name;
        userProfile.PhotoUrl = user.PhotoUrl;

        user.UpdateUserProfileAsync(userProfile);
    }
    

    public bool Login(string _email, string _password)
    {
        bool isSucces=false;

        auth.SignInWithEmailAndPasswordAsync(_email, _password).ContinueWith(task => {
            if (task.IsCanceled)
            {
                Debug.LogError("SignInWithEmailAndPasswordAsync was canceled.");
                isSucces = false;
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("SignInWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                isSucces = false;
                return;
            }

            Firebase.Auth.FirebaseUser newUser = task.Result;
            Debug.LogFormat("User signed in successfully: {0} ({1})",
                newUser.DisplayName, newUser.UserId);
            isSucces = true;
        });
        return isSucces;
    }

    public bool LoginAnonymous()
    {
        bool isSucces = false;
        auth.SignInAnonymouslyAsync().ContinueWith(task => {
            if (task.IsCanceled)
            {
                Debug.LogError("SignInAnonymouslyAsync was canceled.");
                isSucces = false;
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("SignInAnonymouslyAsync encountered an error: " + task.Exception);
                isSucces = false;
                return;
            }

            isSucces = true;
            Firebase.Auth.FirebaseUser newUser = task.Result;
            user = task.Result;

            Debug.LogFormat("User signed in successfully: {0} ({1})",
                newUser.DisplayName, newUser.UserId);
            LoginAnonymousEvent?.Invoke(isSucces);
        });


        return isSucces;
    }
}
