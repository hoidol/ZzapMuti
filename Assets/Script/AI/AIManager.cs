using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : MonoBehaviour
{
    public static AIManager Instance;

    [SerializeField] UserData _opponentData;
    //List<UnitData> _opponentUnitDataList = new List<UnitData>();
    public List<UnitData> _opponentUnitDataDeckList = new List<UnitData>();

    [SerializeField] AIPlayType[] _aiPlayTypes;

    [SerializeField] AIPlayType _curAIPlayType;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public void InitAIMgr(UserData _oData)
    {
        _aiPlayTypes = GetComponentsInChildren<AIPlayType>();
        _opponentData = _oData;
      /*  for (int i =0;i< _aiPlayTypes.Length; i++)
            _aiPlayTypes[i].InitAIPlayType();*/

       for (int i =0;i< _oData.UnitInven.Length; i++)
        {
            Debug.Log("_oData.UnitInven[i] : " + _oData.UnitInven[i]);
            UnitData _uData = DataManager.Instance.GetUnitDataWithIdx(int.Parse(_oData.UnitInven[i]));
            //_opponentUnitDataList.Add(_uData);
            for(int j =0;j< _uData.MaxReinforce; j++)
                _opponentUnitDataDeckList.Add(_uData);
        }
        _curAIPlayType = _aiPlayTypes[Random.Range(0, _aiPlayTypes.Length)];
        _curAIPlayType.InitAIPlayType();
    }


    public void StartTurn(int _r)
    {
        // 3개를 랜덤으로 뽑고
        // 그 중 하나를 선택
       

        _curAIPlayType.StartTurn(_r);
        //UnitManager.Instance._curPlayerUnitsOnTile
    }



    public void FinishTurn()
    {

    }

    public void PickUnitCard(UnitData _uData)
    {
        _opponentUnitDataDeckList.Remove(_uData);
    }

}
