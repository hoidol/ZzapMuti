using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class EntityManager : MonoBehaviour
{
    public static EntityManager Instance;

    public Entity[] _entities;
    [SerializeField] private List<Entity> _entityList = new List<Entity>();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public void CallEntity(string _idx, Unit _cUnit, Vector2 _sPos,Unit _tUnit)
    {
        Entity _e = TryToGetEntity(_idx);
        _e.tr.position = _sPos;
        _e.gameObject.SetActive(true);
        _e.CallEntity(_cUnit, _tUnit);
    }


    public void CallEntity(string _idx, Unit _cUnit, Vector2 _sPos, Vector2 _vec2)
    {
        Entity _e = TryToGetEntity(_idx);
        _e.tr.position = _sPos;
        _e.gameObject.SetActive(true);
        _e.CallEntity(_cUnit, _vec2);
    }


    Entity TryToGetEntity(string _idx)
    {
        for(int i =0;i< _entityList.Count; i++)
        {
            if (_entityList[i].gameObject.activeSelf)
                continue;

            if (_idx.Equals(_entityList[i].Idx))
            {
                return _entityList[i];
            }
        }

        return SpawnEntity(_idx);
    }

    Entity SpawnEntity(string _idx)
    {
        for(int i =0;i< _entities.Length; i++)
        {
            if (_entities[i].Idx.Equals(_idx))
            {
                Entity _e = Instantiate(_entities[i]);
                _e.InitEntity();
                return _e;
            }
        }
        return null;
    }
}

