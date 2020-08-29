using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using System.Threading.Tasks;

public class UserDataSave : MonoBehaviour
{
    private static UserDataSave instance;
    public static UserDataSave Instance
    {
        get { return instance; }
    }

    private DatabaseReference _databaseReference;

    private UserData data;
    public UserData Data
    {
        get { return data; }
    }

    private bool isInit = false;
    public bool IsInit
    {
        get { return isInit; }
    }

    public void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this.gameObject);

        Init();   
    }

    public void Init()
    {
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://zzapmuti.firebaseio.com/");

        _databaseReference = FirebaseDatabase.DefaultInstance.RootReference;

        //LoadUserData();

    }

    public void SetNickName(string _nickName)
    {
        data.nickName = _nickName;
        SaveUserData(data);
    }

    public void SetUnitSlot(string[] _unitInven)
    {
        data.UnitInven = _unitInven;
        SaveUserData(data);
    }
    public void SetUnitInven(string[] _unitInven)
    {
        data.UnitInven = _unitInven;
        SaveUserData(data);
    }

    public void SaveUserData(UserData _userData)
    {
        string json = JsonUtility.ToJson(_userData);

        Task saveTask = _databaseReference.Child("PlayerData").
            Child(GameAuthControl.Instance.User.UserId).SetRawJsonValueAsync(json);

        if (!saveTask.IsFaulted)
        {
            Debug.Log("세이브 성공");
        }
        else
        {
            Debug.Log("세이브 실패");
        }
    }

    public void LoadUserData()
    {
        _databaseReference
       .Child("PlayerData").Child(GameAuthControl.Instance.User.UserId)
       .GetValueAsync().ContinueWith(task =>
       {
           if (task.IsFaulted)
           {
               Debug.Log("로드 실패");
               Debug.Log(task.AsyncState);
               // Handle the error...
           }
           else if (task.IsCompleted)
           {
               Debug.Log("로드 성공");
               DataSnapshot snapshot = task.Result;

               Debug.Log(snapshot.GetRawJsonValue());

               if (snapshot == null || snapshot.Value == null)
               {
                   UserDataSave.Instance.data = new UserData();
                   Debug.Log("New Data");
                   Debug.Log(UserDataSave.Instance.data.nickName);
               }
               else
               {
                   UserDataSave.Instance.data = JsonToUserData(snapshot.GetRawJsonValue());

                   Debug.Log("Load Data");
               }
               isInit = true;
               // Do something with snapshot...
           }
       });
    }

    private UserData JsonToUserData(string _jsonData)
    {
        UserData data;

        data = JsonUtility.FromJson<UserData>(_jsonData);

        return data;
    }
}