using Gameplay;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace Manager
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;
        [SerializeField] private Player mainPlayer;
        [SerializeField] private Player playerPrefab;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            Cursor.visible = false;
            //StartNewGame();
        }

        private void StartNewGame()
        {
            if (mainPlayer != null)
            {
                Destroy(mainPlayer);
            }
            mainPlayer = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
        }

        public Player GetMainPlayer()
        {
            return mainPlayer;
        }
    }
}
