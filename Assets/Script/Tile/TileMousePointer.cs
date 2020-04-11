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

                    }
                }
            }
        }


        if (Input.GetMouseButtonUp(0))
        {
            if (GetCastedTile(out _nowClickObject))
            {
                Tile _tileTemp = _nowClickObject.collider.GetComponent<Tile>();

                if (_tileTemp != null)
                {
                    if (_nowSelectTile ._TileIndexType==TileIndexType.Unit&& _tileTemp._TileIndexType == TileIndexType.Nothing)
                    {
                        Debug.Log(_tileTemp._TilePosIndex);
                        _tileTemp.SetUnit(_nowSelectTile._UnitIndex);
                        _nowSelectTile.SetNothing();
                    }
                }
            }
        }
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
