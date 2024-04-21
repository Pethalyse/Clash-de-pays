using UnityEngine;
public class Player : MonoBehaviour
{
    [SerializeField] private bool isHumanPlayer;
    [SerializeField] private Character character;

    private int _life = 100;
    private int Life
    {
        get => _life;
        set
        {
            _life = value;
            OnPlayerLifeChanged?.Invoke(_life);
        }
    }

    public delegate void OnPlayerLifeChangedDelegate(int life);
    public event OnPlayerLifeChangedDelegate OnPlayerLifeChanged;
    
    private void Update()
    {
        if (isHumanPlayer)
        {
            if (Input.GetKey(KeyCode.UpArrow))
                character.Move(Vector3.forward);
            if (Input.GetKey(KeyCode.DownArrow))
                character.Move(Vector3.back);
            if (Input.GetKey(KeyCode.LeftArrow))
                character.Move(Vector3.left);
            if (Input.GetKey(KeyCode.RightArrow))
                character.Move(Vector3.right);
        }
    }
}
