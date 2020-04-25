using UnityEngine;
using UnityEngine.UI;

public class GameProgress : MonoBehaviour
{
    [Header("Red Player")]
    [SerializeField] private Player _redPlayer;

    [Header("Blue Player")]
    [SerializeField] private Player _bluePlayer;

    [SerializeField] private PlayerDrawManager _playerDrawManager;

    [Header("UI (나중에 UI Class 생성예정)")]
    [SerializeField] private Button startBattleButton;

    [SerializeField] private Text _redPlayerLifeText;
    [SerializeField] private Text _bluePlayerLifeText;

    private int _round=0;

    public void Start()
    {
        Init();
        DrawRedPlayer();
    }

    public void Init()
    {
        _redPlayer.Init(EnumInfo.TeamType.Red);
        _bluePlayer.Init(EnumInfo.TeamType.Blue);

        SetPlayerLifeUI();
    }

    public void DrawRedPlayer()
    {
        _playerDrawManager.SetPlayerDraw(_redPlayer._DeckManager._Deck,EnumInfo.TeamType.Red,DrawBluePlayer);
    }

    public void DrawBluePlayer()
    {
        _playerDrawManager.SetPlayerDraw(_bluePlayer._DeckManager._Deck, EnumInfo.TeamType.Blue, SetCanBattle);
    }

    public void SetCanBattle()
    {
        startBattleButton.gameObject.SetActive(true);
    }

    public void StartBattle()
    {
        startBattleButton.gameObject.SetActive(false);
        UnitManager.Instance.StartBattle();
    }

    public void EndBattle(EnumInfo.TeamType _winTeam,int _discountLife)
    {
        if(_winTeam==EnumInfo.TeamType.Red)
        {
            _bluePlayer._Hp -= _discountLife;
        }
        else
        {
            _redPlayer._Hp -= _discountLife;
        }
        SetPlayerLifeUI();

        if (_redPlayer._Hp<=0)
        {
            EndGame(EnumInfo.TeamType.Blue);
        }
        else if (_bluePlayer._Hp <= 0)
        {
            EndGame(EnumInfo.TeamType.Red);
        }
        else
        {
            DrawRedPlayer();
        }
    }

    public void SetPlayerLifeUI()
    {
        _redPlayerLifeText.text = string.Format("Red Life [{0}]", _redPlayer._Hp.ToString());
        _bluePlayerLifeText.text = string.Format("Blue Life [{0}]", _bluePlayer._Hp.ToString());
    }

    public void EndGame(EnumInfo.TeamType _winTeam)
    {
        Debug.Log("|||||||||||||||||||End Game|||||||||||||||||||\nWin player is "
            + _winTeam.ToString()+"player!");
    }
}
