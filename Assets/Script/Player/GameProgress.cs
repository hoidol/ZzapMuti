using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameProgress : MonoBehaviour
{
    public static GameProgress Instance;

    [Header("Red Player")]
    [SerializeField] private Player _redPlayer;

    [Header("Blue Player")]
    [SerializeField] private Player _bluePlayer;

    [Header("DrawManager")]
    [SerializeField] private PlayerDrawManager _playerDrawManager;

    [Header("UI")]
    [SerializeField] private Button startBattleButton;

    [SerializeField] private PlayerInfoUI _redPlayerInfoUI;
    [SerializeField] private PlayerInfoUI _bluePlayerInfoUI;

    [SerializeField] private Text _roundText;

    [SerializeField] private GameEndUI _gameEndUI;

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
        _redPlayer.Init(EnumInfo.TeamType.Red);
        _bluePlayer.Init(EnumInfo.TeamType.Blue);

        _redPlayerInfoUI.SetPlayer(_redPlayer);
        _bluePlayerInfoUI.SetPlayer(_bluePlayer);

        _round = 1;

        SetRoundUI();
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
        TileManager._Instance.StartBattle();

        startBattleButton.gameObject.SetActive(false);
        UnitManager.Instance.StartBattle();

        StartCoroutine(TestRoutine());
    }

    public IEnumerator TestRoutine()
    {
        yield return new WaitForSeconds(5);
        EndBattle(EnumInfo.TeamType.Red, 1);
    }

    public void EndBattle(EnumInfo.TeamType _winTeam,int _discountLife)
    {
        _round++;

        UnitManager.Instance.FinishBattle();
        TileManager._Instance.EndBattle();

        if (_winTeam==EnumInfo.TeamType.Red)
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
