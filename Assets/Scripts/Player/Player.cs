using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class Player : Singleton<Player>
{
    private Wallet _wallet = new Wallet();

    private string _walletPath;

    private void Awake()
    {
        _walletPath = $"{Application.persistentDataPath}/Wallet.json";

        if (File.Exists(_walletPath))
        {
            string json = File.ReadAllText(_walletPath);

            _wallet = JsonUtility.FromJson<Wallet>(json);
        }

        _wallet.AddMoney(10);
    }

    public int GetMoney()
    {
        if (_wallet != null)
            return _wallet.PlayerMoney;
        else
            throw new System.Exception("Wallet doesn't exist");
    }

    public void AddMoney(int money)
    {
        _wallet.AddMoney(money);
    }

    public bool BuyItem(int money)
    {
        if (_wallet.RemoveMoney(money))
        {
            SaveJson();

            return true;
        }
        else
        {
            return false;
        }
    }

    private void SaveJson()
    {
        string json = JsonUtility.ToJson(_wallet);

        File.WriteAllText(_walletPath, json);
    }
}
