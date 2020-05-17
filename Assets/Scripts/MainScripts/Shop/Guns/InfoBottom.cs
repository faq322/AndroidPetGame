using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoBottom : MonoBehaviour
{
    [SerializeField]
    public GameObject playerObject;
    public PlayerStats player;

    public Text name;
    public Text info;

    public static int item_num;
    public static string item_type;

    public void setInfo(string _name, string _info)
    {
        name.text = _name;
        info.text = _info;
    }

    public void Buy()
    {
        Debug.Log("item type: " + item_type);
        switch (item_type)
        {
            case "Gun":
                Debug.Log("Buying gun...");
                BuyGun();
                break;
            case "Cave":
                Debug.Log("Buying cave...");
                BuyCave();
                break;
            default:
                Debug.Log("error");
                break;
        }
    }

    public void BuyGun()
    {
        int i = item_num;
        Debug.Log(i);
        //Check if purchaused
        if (!player.guns[i].purchised)
        {
            if (ConfirmBuy(player.guns[i].price))
            {
                PlayerStats.gun = i;
                player.guns[i].purchised = true;
            }
        }
        else
        {
            //PlayerStats.gun = i;
        }
    }

    public void BuyCave()
    {
        int i = item_num;
        Debug.Log(i);
        if (!player.caves[i].purchised)
        {
            if (ConfirmBuy(player.caves[i].price))
            {
                ShowCave(i);
                player.caves[i].purchised = true;
            }
        }
        else
        {
            //PlayerStats.gun = i;
        }
    }

    public void ShowCave(int i)
    {
        player.caves[i].caveObject.SetActive(true);
    }

    public bool ConfirmBuy(int price)
    {
        int money = player.pocket.money;
        if (price > money) return false;
        player.pocket.AddMoney(-price);
        return true;

    }

}
