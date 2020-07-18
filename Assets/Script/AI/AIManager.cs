using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : MonoBehaviour
{
    public static AIManager Instance;

    UserData _opponentData;
    //List<UnitData> _opponentUnitDataList = new List<UnitData>();
    List<string> _opponentUnitDataDeckList = new List<string>();
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public void SetOpponentData(UserData _oData)
    {
       for(int i =0;i< _oData.UnitInven.Length; i++)
        {
            UnitData _uData = DataManager.Instance.GetUnitDataWithUnitIdx(_oData.UnitInven[i]);
            //_opponentUnitDataList.Add(_uData);
            for(int j =0;j< _uData.MaxReinforce; j++)
                _opponentUnitDataDeckList.Add(_uData.UnitIdx);
        }
    }


    public void StartTurn(EnumInfo.TeamType _tT)
    {
        // 3개를 랜덤으로 뽑고
        // 그 중 하나를 선택
        int random1;
        int random2;

        string tmp;
        for (int i = 0; i < _opponentUnitDataDeckList.Count; ++i)
        {
            random1 = Random.Range(0, _opponentUnitDataDeckList.Count);
            random2 = Random.Range(0, _opponentUnitDataDeckList.Count);

            tmp = _opponentUnitDataDeckList[random1];
            _opponentUnitDataDeckList[random1] = _opponentUnitDataDeckList[random2];
            _opponentUnitDataDeckList[random2] = tmp;
        }

        
    }

    //고려해야 할 부분 : 1.  어떤 기준 유닛을 뽑을 지 2. 유닛을 어떤 기준으로 어디에 배치할지   
    // 1. 어떤 기준 유닛을 뽑을지에 대해서
    // 1)


    public void FinishTurn()
    {

    }

}
