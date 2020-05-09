using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameEndUI : MonoBehaviour
{
    [SerializeField] private Text _winTeamText;

    public void Initialize(EnumInfo.TeamType _winTeam)
    {
        _winTeamText.text = string.Format("{0} Team\nw WIN", _winTeam);
    }
}
