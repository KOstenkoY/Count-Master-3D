[System.Serializable]
public class Wallet
{
    private int _playerMoney;

    public int PlayerMoney => _playerMoney;

    public void AddMoney(int money)
    {
        _playerMoney += money;
    }

    public bool RemoveMoney(int money)
    {
        _playerMoney -= money;

        if (_playerMoney >= 0)
        {
            return true;
        }
        else
        {
            _playerMoney += money;

            return false;
        }
    }
}
