using System;
using UnityEngine;
using Object = UnityEngine.Object;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private Player _mainPlayer;
    [SerializeField] private Player playerPrefab;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        //StartNewGame();
    }

    private void StartNewGame()
    {
        if (_mainPlayer != null)
        {
            Destroy(_mainPlayer);
        }
        _mainPlayer = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
    }
}
