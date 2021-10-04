using UnityEngine;

public class LevelManager : Manager<LevelManager>
{
    [SerializeField]
    private Transform startPoint;
    private Player _player;

    public Player Player => _player;

    private void Awake()
    {
        _player = Loader.Instance.Player.GetComponent<Player>();
        _player.transform.position = startPoint.position;
    }
}
