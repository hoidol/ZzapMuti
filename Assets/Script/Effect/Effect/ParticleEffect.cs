using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEffect : Effect
{
    ParticleSystem[] _particles;

    public override void InitEffect()
    {
        base.InitEffect();
        _particles = GetComponentsInChildren<ParticleSystem>();
    }


    public override void PlayEffect()
    {
        float _maxDuration = 0;
        for (int i = 0; i < _particles.Length; i++)
        {
            if(_maxDuration <= _particles[i].main.duration)
            {
                _maxDuration = _particles[i].main.duration;
            }
            _particles[i].Play();
        }
        StartCoroutine(ProcessEffect(_maxDuration));
    }

    IEnumerator ProcessEffect(float _sec)
    {
        yield return new WaitForSeconds(_sec);
        gameObject.SetActive(false);
    }
}
