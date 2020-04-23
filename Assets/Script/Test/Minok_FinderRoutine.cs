using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Minok_FinderRoutine : MonoBehaviour
{
    [SerializeField] private Minok_Finder[] finder;
    private bool isFinding = false;
    public bool IsFinding
    {
        get { return isFinding; }
    }

    public void Start()
    {
        //StartCoroutine(Routine());
    }

    public void ScanPath()
    {
        AstarPath.active.Scan();
    }

    public void SetPath()
    {
        StartCoroutine(Routine());
    }

    public IEnumerator Routine()
    {
        isFinding = true;
        for (int i = 0; i < finder.Length; i++)
        {
            AstarPath.active.Scan();

            yield return new WaitUntil(() => { return !AstarPath.active.isScanning; });
            
            finder[i].ToPath();
            yield return new WaitUntil(() => { return !finder[i].IsFinding; });
            //yield return new WaitForSeconds(.1f);
        }

        isFinding = false;
    }
}
