using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minok_FinderControl : MonoBehaviour
{
    [SerializeField] private Minok_FinderRoutine _red;
    [SerializeField] private Minok_FinderRoutine _blue;

    private void Start()
    {
        StartCoroutine(Routine());
    }

    public IEnumerator Routine()
    {
        yield return new WaitForSeconds(2);

        while (true)
        {
            Debug.Log("TTTTT");
            //AstarPath.active.Scan();
            _red.SetPath();
            yield return new WaitUntil(() => { return !_red.IsFinding; });
            yield return new WaitForSeconds(.4f);

            //AstarPath.active.Scan();
            _blue.SetPath();
            yield return new WaitUntil(() => { return !_blue.IsFinding; });
            yield return new WaitForSeconds(.4f);
        }
    }
}
