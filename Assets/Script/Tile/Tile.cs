using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileIndexType
{
    Nothing,
    Unit,
    Obstacle
}

public class Tile : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _tileSpriteRenderer;
    public SpriteRenderer _TileSpriteRenderer
    {
        get { return _tileSpriteRenderer; }
    }

    [SerializeField] private Vector2 _tilePosIndex=new Vector2();
    public Vector2 _TilePosIndex
    {
        get { return _tilePosIndex; }
    }


    private Unit _unitIndex;
    public Unit _UnitIndex
    {
        get { return _unitIndex; }
    }

    private Unit _beforeBattleUnitIndex;

    private Unit _reservationUnit;
    public Unit _ReservationUnit
    {
        get { return _reservationUnit; }
        set { _reservationUnit = value; }
    }

    [SerializeField] private TileIndexType _tileIndexType;
    public TileIndexType _TileIndexType
    {
        get { return _tileIndexType; }
    }

    [SerializeField] private EnumInfo.TeamType _tileTeam;
    public EnumInfo.TeamType _TileTeam
    {
        get { return _tileTeam; }
        set { _tileTeam = value;
            SetTileTeamColor();
        }
    }

    public bool hasUnit = false;

    public void Awake()
    {
        if (_tileIndexType == TileIndexType.Obstacle)
            _tileSpriteRenderer.color = Color.red;
    }
    
    public void Initialize(Vector2 _tilePosIdx, float _distance)
    {
        _tilePosIndex = _tilePosIdx;

        transform.localPosition = _tilePosIdx * _distance;
    }

    public void SetNothing()
    {
        _unitIndex = null;
        _tileSpriteRenderer.color = Color.white;
        _tileIndexType = TileIndexType.Nothing;
        SetTileTeamColor();

        hasUnit = false;
    }

    public void SetUnit(Unit _unit)
    {
        _unitIndex = _unit;    

        if (_unit == null)
            _tileSpriteRenderer.color = Color.white;
        else
            _tileSpriteRenderer.color = Color.black;

        _tileIndexType = TileIndexType.Unit;
        hasUnit = true;

        _unit.SetTile(this);
    }

    public void SetUnit(string _unitIdx, EnumInfo.TeamType _teamTy)
    {
        //UnitData _uniData= DataManager.Instance.GetUnitDataWithUnitIdx(_unitIdx);

        hasUnit = true;

        _unitIndex = UnitManager.Instance.CreateUnitWithUnitIdx(_unitIdx,this, _teamTy);
        
        _unitIndex.SetTile(this);

        _tileSpriteRenderer.color = Color.black;

        _tileIndexType = TileIndexType.Unit;
    }

    public void SetTileTeamColor()
    {
        if (_TileTeam == EnumInfo.TeamType.Red)
            _tileSpriteRenderer.color = new Color(.7f, .4f, .4f, 1);
        else
            _tileSpriteRenderer.color = new Color(.4f, .4f, .7f, 1);
    }

    public void SetDataBeforeBattle()
    {
        _beforeBattleUnitIndex = _unitIndex;
    }

    public void RestoreDataBeforeBattle()
    {
        _unitIndex = _beforeBattleUnitIndex;
        _beforeBattleUnitIndex = null;

        if (_unitIndex == null)
            return;
       
        SetUnit(_unitIndex);
    }
}
