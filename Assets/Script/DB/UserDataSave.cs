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
    }

    public void SaveUserData(string _nickname,string[] _unitInven)
    {
        UserData user = new UserData() { nickName= _nickname, UnitInven=_unitInven};
        string json = JsonUtility.ToJson(user);

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

    public void GetUserData()
    {
        _databaseReference
       .Child("PlayerData").Child("dHOSUJyXDWnHVLCqCgBr")
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

               //Debug.Log(snapshot.GetRawJsonValue());
               JsonToUserData(snapshot.GetRawJsonValue());
               // Do something with snapshot...
           }
       });
    }

    private void JsonToUserData(string _jsonData)
    {
        UserData data;

        data = JsonUtility.FromJson<UserData>(_jsonData);

        Debug.Log(data.nickName);
        Debug.Log(data.UnitInven);
    }
}