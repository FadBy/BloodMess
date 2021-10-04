using UnityEngine;

public class Loader : Manager<Loader>
{
    [SerializeField]
    private GameObject playerPrefab;
    private GameObject _player;
    public GameObject Player
    {
        get
        {
            if (_player == null)
            {
                _player = Instantiate(playerPrefab);
            }
            return _player;
        }
    }

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
}
