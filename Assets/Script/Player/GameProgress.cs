using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameProgress : MonoBehaviour
{
    public static GameProgress Instance;

    [Header("Red Player")]
    [SerializeField] private Player _redPlayer;
    [SerializeField] private GameObject _redBlind;

    [Header("Blue Player")]
    [SerializeField] private Player _bluePlayer;
    [SerializeField] private GameObject _blueBlind;

    private EnumInfo.TeamType _drawTeam;

    [Header("DrawManager")]
    [SerializeField] private PlayerDrawManager _playerDrawManager;

    public PlayerDrawManager playerDrawMgr
    {
        get { return _playerDrawManager; }
        set { _playerDrawManager = value; }
    }

    [Header("UI")]
    [SerializeField] private Button startBattleButton;

    [Header("Player UI")]
    [SerializeField] private PlayerInfoUI _redPlayerInfoUI;
    [SerializeField] private PlayerInfoUI _bluePlayerInfoUI;

    [Header("Timer")]
    [SerializeField] private Timer _timer;

    [SerializeField] private Text _roundText;

    [SerializeField] private GameEndUI _gameEndUI;

    [SerializeField] private GameObject _addTurnButton;

    private int _round=0;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
    public void Start()
    {
        Init();
        DrawRedPlayer();
    }

    public void Init()
    {
        _redPlayer.Init(EnumInfo.TeamType.Player);
        _bluePlayer.Init(EnumInfo.TeamType.Opposite);

        _redPlayerInfoUI.SetPlayer(_redPlayer);
        _bluePlayerInfoUI.SetPlayer(_bluePlayer);

        _round = 1;

        SetRoundUI();
    }

    public void DrawRedPlayer()
    {
        _drawTeam = EnumInfo.TeamType.Player;

        SetBlind(false, true);

        _playerDrawManager.SetPlayerDraw(_redPlayer._DeckManager._Deck,EnumInfo.TeamType.Player, ActiveAddTurnButton);
    }

    public void DrawBluePlayer()
    {
        _drawTeam = EnumInfo.TeamType.Opposite;
        
        SetBlind(true, false);

        _playerDrawManager.SetPlayerDraw(_bluePlayer._DeckManager._Deck, EnumInfo.TeamType.Opposite, ActiveAddTurnButton);
    }

    public void SetPlayerInfoUI(bool _active)
    {
        _redPlayerInfoUI.gameObject.SetActive(_active);
        _bluePlayerInfoUI.gameObject.SetActive(_active);
    }

    public void SetBlind(bool _red,bool _blue)
    {
        _redBlind.gameObject.SetActive(_red);
        _blueBlind.gameObject.SetActive(_blue);
    }

    public void ActiveAddTurnButton()
    {
        _addTurnButton.gameObject.SetActive(true);
    }

    /// <summary>
    /// AddTurn은 외부 버튼에서 호출
    /// </summary>
    public void AddTurn()
    {
        _addTurnButton.gameObject.SetActive(false);
        switch (_drawTeam)
        {
            case EnumInfo.TeamType.Player:
                DrawBluePlayer();
                break;

            case EnumInfo.TeamType.Opposite:
                SetCanBattle();
                break;
        }
    }

    public void SetCanBattle()
    {
        startBattleButton.gameObject.SetActive(true);

        SetBlind(false, false);
        SetPlayerInfoUI(false);
    }

    public void StartBattle()
    {
        TileManager._Instance.StartBattle();

        startBattleButton.gameObject.SetActive(false);
        UnitManager.Instance.StartBattle();

        _timer.gameObject.SetActive(true);
        _timer.Play();

     //   StartCoroutine(TestRoutine());
    }

    public void EndBattle(EnumInfo.TeamType _winTeam,int _discountLife)
    {
        _round++;

        SetPlayerInfoUI(true);

        UnitManager.Instance.FinishBattle();
        TileManager._Instance.EndBattle();

        _timer.Stop();
        _timer.gameObject.SetActive(false);

        if (_winTeam==EnumInfo.TeamType.Player)
        {
            _bluePlayer._Hp -= _discountLife;
        }
        else
        {
            _redPlayer._Hp -= _discountLife;
        }
        SetRoundUI();

        if (_redPlayer._Hp<=0)
        {
            EndGame(EnumInfo.TeamType.Opposite);
        }
        else if (_bluePlayer._Hp <= 0)
        {
            EndGame(EnumInfo.TeamType.Player);
        }
        else
        {
            DrawRedPlayer();
        }
    }

    public void SetRoundUI ()
    {
        _roundText.text = string.Format("Round {0}", _round);
    }

    public void EndGame(EnumInfo.TeamType _winTeam)
    {
        _gameEndUI.gameObject.SetActive(true);
        _gameEndUI.Initialize(_winTeam);
    }

    public void ReGame()
    {
        SceneManager.LoadScene("MinokScene");
    }
}
