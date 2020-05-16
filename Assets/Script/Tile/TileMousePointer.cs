using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMousePointer : MonoBehaviour
{
    private RaycastHit2D _nowClickObject;
    [SerializeField] private SpriteRenderer _pointerSpriteRenderer;

    private Tile _nowSelectTile;

    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (GetCastedTile(out _nowClickObject))
            {
                Tile _tileTemp = _nowClickObject.collider.GetComponent<Tile>();

                if (_tileTemp != null)
                {
                    _nowSelectTile = _tileTemp;

                    SetUnitSprite(_tileTemp);
                }
            }
        }

        if (_pointerSpriteRenderer.sprite != null)
            FollowSpriteToMousePointer();


        if (Input.GetMouseButtonUp(0))
        {
            _pointerSpriteRenderer.sprite = null;

            if (GetCastedTile(out _nowClickObject))
            {
                Tile _tileTemp = _nowClickObject.collider.GetComponent<Tile>();

                if (_tileTemp != null)
                {
                    if (_tileTemp == _nowSelectTile)
                        return;

                    CheckMoveUnit(_nowSelectTile, _tileTemp);

                    CheckReinforceUnit(_nowSelectTile, _tileTemp);
                }
            }
        }
    }

    public void FollowSpriteToMousePointer()
    {
        _pointerSpriteRenderer.transform.position =  Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _pointerSpriteRenderer.transform.position = new Vector3(_pointerSpriteRenderer.transform.position.x, _pointerSpriteRenderer.transform.position.y, 0);
    }

    public void SetUnitSprite(Tile _downTile)
    {
        if (_downTile._TileIndexType == TileIndexType.Unit)
        {
            _pointerSpriteRenderer.sprite = _downTile._UnitIndex.GetComponentInChildren<SpriteRenderer>().sprite;
        }
    }

    public void CheckMoveUnit(Tile _downTile, Tile _upTile)
    {
        if (_downTile == null || _upTile == null)
            return;

        if (_downTile._TileTeam != _upTile._TileTeam)
            return;

        if (_downTile._TileIndexType != TileIndexType.Unit || _upTile._TileIndexType != TileIndexType.Nothing)
            return;

        MoveUnit(_downTile, _upTile);
    }

    public void MoveUnit(Tile _downTile, Tile _upTile)
    {
        UnitManager.Instance.UnitMoveToTile(_downTile._UnitIndex, _upTile);

        _upTile.SetUnit(_nowSelectTile._UnitIndex);
        _downTile.SetNothing();
    }

    public void CheckReinforceUnit(Tile _downTile, Tile _upTile)
    {
        if (_downTile == null || _upTile == null)
            return;

        if (_downTile._TileTeam != _upTile._TileTeam)
            return;

        if (_downTile._TileIndexType != TileIndexType.Unit || _upTile._TileIndexType != TileIndexType.Unit)
            return;

        if (_downTile._UnitIndex._unitIdx == _upTile._UnitIndex._unitIdx)
        {
            ReinforceUnit(_downTile, _upTile);
        }
    }

    public void ReinforceUnit(Tile _downTile, Tile _upTile)
    {
        //강화
        Debug.Log("강화 Call");

        Unit unitTemp = UnitManager.Instance.CombineUnit(_downTile._UnitIndex, _upTile._UnitIndex, _upTile);

        if (unitTemp != null)
        {
            UnitManager.Instance.RemoveUnit(_downTile._UnitIndex);
            UnitManager.Instance.RemoveUnit(_upTile._UnitIndex);

            _nowSelectTile.SetNothing();
            _upTile.SetUnit(unitTemp);
        }
    }

    public bool GetCastedTile(out RaycastHit2D _outHitObject)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D[] hit = Physics2D.GetRayIntersectionAll(ray, Mathf.Infinity);
        

        for (int i=0;i< hit.Length;i++)
        {
            if(hit[i].collider.CompareTag("Tile"))
            {
                _outHitObject = hit[i];
                return true;
            }
        }
        _outHitObject=Physics2D.GetRayIntersection(ray, Mathf.Infinity);

        return false;
    }
}
