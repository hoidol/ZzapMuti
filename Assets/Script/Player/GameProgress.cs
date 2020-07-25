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

    private System.Action endDrawFunc;

    [Header("DrawManager")]
    [SerializeField] private PlayerDrawManager _playerDrawManager;

    public PlayerDrawManager playerDrawMgr
    {
        get { return _playerDrawManager; }
        set { _playerDrawManager = value; }
    }

    [Header("UI")]
    [SerializeField] private Button endPlayerDrawButton;

    [Header("Player UI")]
    [SerializeField] private PlayerInfoUI _redPlayerInfoUI;
    [SerializeField] private PlayerInfoUI _bluePlayerInfoUI;

    [Header("Timer")]
    [SerializeField] private Timer _timer;
    [SerializeField] private LeftTimeSlider _leftTimeSlider;
    [SerializeField] private TimeOverDamage _timeOverDamage;

    [Header("OtherInfo")]
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
        DrawPlayer();
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

    public void DrawPlayer()
    {
        endPlayerDrawButton.gameObject.SetActive(true);
        _drawTeam = EnumInfo.TeamType.Player;

        SetBlind(false, true);

        _playerDrawManager.SetPlayerDraw(_redPlayer._DeckManager._Deck,EnumInfo.TeamType.Player);

        endDrawFunc = DrawOpposite;
        DrawRoutine(30, EndDraw);
    }

    public void DrawRoutine(int _second, System.Action _endCallback)
    {
        _leftTimeSlider.ResetTimer();
        _leftTimeSlider.SetGoalTime(_second);
        _leftTimeSlider.Connect();

        _timeOverDamage.enabled = false;

        _timer.gameObject.SetActive(true);
        _timer.TimerReset();
        _timer.GoalSecond = _second;

        _timer.GoalSecondEvent += _endCallback;
        
        _timer.Play();

    }

    public void DrawOpposite()
    {
        endPlayerDrawButton.gameObject.SetActive(false);
        _drawTeam = EnumInfo.TeamType.Opposite;
        
        SetBlind(true, false);
        
        endDrawFunc = StartBattle;
        DrawRoutine(30, EndDraw);
    }

    /// <summary>
    /// 외부 버튼에서 호출하거나 시간이 지나면 호출
    /// </summary>
    public void EndDraw()
    {
        _playerDrawManager.gameObject.SetActive(false);
        _timer.gameObject.SetActive(false);
        endDrawFunc?.Invoke();
    }

    public void SetBattleTimer()
    {
        _leftTimeSlider.ResetTimer();
        _leftTimeSlider.SetGoalTime(40);
        _leftTimeSlider.Connect();

        _timer.gameObject.SetActive(true);
        _timer.TimerReset();

        _timeOverDamage.enabled = true;
        _timer.Play();
    }

    public void StartBattle()
    {
        SetBlind(false, false);
        
        SetPlayerInfoUI(false);

        TileManager._Instance.StartBattle();
        
        UnitManager.Instance.StartBattle();
        
        SetBattleTimer();
    }

    public void SetPlayerInfoUI(bool _active)
    {
        _redPlayerInfoUI.gameObject.SetActive(_active);
        _bluePlayerInfoUI.gameObject.SetActive(_active);
    }

    public void SetBlind(bool _red, bool _blue)
    {
        _redBlind.gameObject.SetActive(_red);
        _blueBlind.gameObject.SetActive(_blue);
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
            DrawPlayer();
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
