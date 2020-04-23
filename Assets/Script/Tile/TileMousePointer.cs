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

                    if (_nowSelectTile._TileIndexType == TileIndexType.Unit)
                    {
                        _pointerSpriteRenderer.sprite = _nowSelectTile._UnitIndex.GetComponentInChildren<SpriteRenderer>().sprite;
                    }
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
                    //유닛 이동
                    if (_nowSelectTile._TileIndexType==TileIndexType.Unit&& _tileTemp._TileIndexType == TileIndexType.Nothing)
                    {
                        Debug.Log(_tileTemp._TilePosIndex);

                        if (_nowSelectTile._TileTeam != _tileTemp._TileTeam)
                            return;

                        _tileTemp.SetUnit(_nowSelectTile._UnitIndex);
                        _nowSelectTile.SetNothing();

                    }
                    else if(_nowSelectTile._TileIndexType == TileIndexType.Unit && _tileTemp._TileIndexType == TileIndexType.Unit)
                    {
                        if(_nowSelectTile._UnitIndex._unitIdx==_tileTemp._UnitIndex._unitIdx)
                        {
                            //강화
                        }
                    }
                }
            }
        }
    }

    public void FollowSpriteToMousePointer()
    {
        _pointerSpriteRenderer.transform.position =  Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _pointerSpriteRenderer.transform.position = new Vector3(_pointerSpriteRenderer.transform.position.x, _pointerSpriteRenderer.transform.position.y, 0);
    }

    public void SetUnitSprite()
    {

    }

    public void MoveUnit()
    {

    }

    public void ReinforceUnit()
    {

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
