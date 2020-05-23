using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public static EffectManager Instance;
    Transform _tr;
    public Effect[] _effects;

    List<Effect> _effectList = new List<Effect>();
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        _tr = transform;
    }


    public void PlayEffect(string _eIdx,Vector2 _pos)
    {
        Effect _e = TryToGetEffect(_eIdx);
        _e._tr.position = _pos;
        _e.gameObject.SetActive(true);
        _e.PlayEffect();
    }


    Effect TryToGetEffect(string _eIdx)
    {
        for(int i=0;i< _effectList.Count; i++)
        {
            if (_effectList[i].gameObject.activeSelf)
                continue;

            if (_effectList[i]._effectIdx.Equals(_eIdx))
            {
                return _effectList[i];
            }
        }

        for(int i = 0; i < _effects.Length; i++)
        {
            if (_effects[i]._effectIdx.Equals(_eIdx))
            {
                Effect _e = Instantiate(_effects[i], _tr);
                _e.InitEffect();
                _effectList.Add(_e);
                return _e;
            }
        }
        return null;
    }
}
