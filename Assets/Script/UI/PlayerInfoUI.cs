using UnityEngine;
using UnityEngine.UI;

public class PlayerInfoUI : MonoBehaviour
{
    private Player _targetPlayer;

    [SerializeField] private Text _lifeText;
    [SerializeField] private Text _nameText;
    
    public void SetPlayer(Player _player)
    {
        _targetPlayer = _player;

        _nameText.text = _player._Name;
        _targetPlayer.ChangeHpEvent += SetLifeText;

        SetLifeText();
    }

    public void DisconnectPlayer()
    {
        _targetPlayer.ChangeHpEvent -= SetLifeText;
    }

    public void OnDestroy()
    {
        DisconnectPlayer();
    }

    public void SetLifeText()
    {
        _lifeText.text 
            = string.Format("{0} Life [{1}]",_targetPlayer._TeamType ,_targetPlayer._Hp);
    }
}
