using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;

    public UserData playerData;
    public UserData opponentData;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

   [SerializeField] BattleRecordData _battleRecorData = new BattleRecordData();
    [SerializeField] List<UnitTrackingData> _unitTrackingDataList = new List<UnitTrackingData>();
    [SerializeField] List<UnitTrack> _unitTrackList = new List<UnitTrack>();
    public void InitPlayerMgr(UserData _pData, UserData _oData)
    {
        //내 덱과 상대방 덱 가져오기
        playerData = _pData;
        opponentData = _oData;

        _battleRecorData.Nick = _pData.nickName;
        _battleRecorData.UnitInven = _pData.UnitInven;

        AIManager.Instance.SetOpponentData(_oData);
    }



    public void StartBattle()
    {
        UnitTrackingData _uTData = new UnitTrackingData();

        UnitTrackContainer _unitTrackContainer = new UnitTrackContainer();
        _unitTrackList.Clear();
        for (int i =0;i< UnitManager.Instance._curPlayerUnitsOnTile.Count; i++)
        {
            if (!UnitManager.Instance._curPlayerUnitsOnTile[i].gameObject.activeSelf)
                continue;

            UnitTrack _uT = new UnitTrack();
            _uT.TileX = UnitManager.Instance._curPlayerUnitsOnTile[i]._tile._TilePosIndex.x;
            _uT.TileY = UnitManager.Instance._curPlayerUnitsOnTile[i]._tile._TilePosIndex.y;
            _uT.UnitIdx = UnitManager.Instance._curPlayerUnitsOnTile[i]._unitIdx;
            //_uT.ReinforceLv = UnitManager.Instance._curPlayerUnitsOnTile[i]._unitData.ReinforceLv;
            _unitTrackList.Add(_uT);
        }

        _unitTrackContainer.UnitTracks = _unitTrackList.ToArray();

        _uTData.UnitTrackContainer = _unitTrackContainer;

        _unitTrackingDataList.Add(_uTData);
    }

    public void FinishBattle()
    {
        UnitTrackingDataContainer _uTDC = new UnitTrackingDataContainer();
        _uTDC.UnitTrackingDatas = _unitTrackingDataList.ToArray();
        _battleRecorData.UnitTrackingDataContainer = _uTDC;

       // Debug.Log( JsonUtility.ToJson(_battleRecorData));
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            Time.timeScale = 3;
        }
    }
}
