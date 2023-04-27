using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject _playerPrefab;

    private MoveToDirection _playerMoveToDirection;
    private Movement _playerMovement;

    private static Material _playerMaterial = null;
    private ObjectPooler _objectPooler;

    [Header("UI")]
    [SerializeField] private GameObject _menu;
    [SerializeField] private GameObject _winMenu;
    [SerializeField] private GameObject _loseMenu;
    [SerializeField] private Text _moneyCountText;

    private int _countCoins = 10;

    private void Awake()
    {
        UpdateCoinStatus();
    }

    private void Start()
    {
        _playerMoveToDirection = _playerPrefab.GetComponent<MoveToDirection>();

        _playerMovement = _playerPrefab.GetComponent<Movement>();

        _objectPooler = GetComponent<ObjectPooler>();
    }

    private void OnEnable()
    {
        BuyColor.OnColorBought += SetMaterial;
    }

    private void OnDisable()
    {
        BuyColor.OnColorBought -= SetMaterial;
    }

    public void StartGame()
    {
        _playerMoveToDirection.StartMoveToDirection();

        if (_playerMaterial != null)
        {
            for (int i = 0; i < _objectPooler.pooledObjects.Count; i++)
            {
                _objectPooler.pooledObjects[i].GetComponentInChildren<Renderer>().material = _playerMaterial;
            }
        }

        CloseAllMenus();
    }

    public void Replay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Win()
    {
        CloseAllMenus();

        _playerMoveToDirection.StopMoveToDirection();
        _playerMovement.StopMovement();

        Player.Instance.AddMoney(_countCoins);
        UpdateCoinStatus();

        _winMenu.SetActive(true);
    }

    public void Lose()
    {
        CloseAllMenus();

        _playerMoveToDirection.StopMoveToDirection();
        _playerMovement.StopMovement();

        _loseMenu.SetActive(true);
    }

    void CloseAllMenus()
    {
        foreach (Transform child in _menu.transform)
        {
            child.gameObject.SetActive(false);
        }
    }

    private void SetMaterial(Material material)
    {
        _playerMaterial = material;

        UpdateCoinStatus();
    }

    private void UpdateCoinStatus()
    {
        _moneyCountText.text = Player.Instance.GetMoney().ToString();
    }
}
