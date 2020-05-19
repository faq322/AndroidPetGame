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
    public Text button;

    public static int item_num;
    public static string item_type;

    public CurrentGun mainCave; // where it displays


   
    public void setInfo(string _name, string _info)
    {
        name.text = _name;
        info.text = _info;

        switch (item_type)
        {
            case "Gun":
                if (player.guns[item_num].purchised)
                {
                    button.text = "Use";
                    info.text = "Purchised";
                    button.color = Color.yellow;
                }
                else
                {
                    button.text = "Buy";
                    button.color = Color.white;
                }
                break;
            case "Cave":
                if (player.caves[item_num].purchised) button.text = "Use"; else button.text = "Buy";
                break;
        }
    }

    public void UseOrBuy()
    {
        switch (item_type)
        {
            case "Gun":
                if (player.guns[item_num].purchised) Use(); else Buy();
                break;
            case "Cave":
                if (player.caves[item_num].purchised) Use(); else Buy();
                break;
        }
        mainCave.showUsedGun();
    }

    public void Use()
    {
        Debug.Log("item type: " + item_type);
        switch (item_type)
        {
            case "Gun":
                Debug.Log("Using gun...");
                PlayerStats.gun = item_num;
                break;
            case "Cave":
                Debug.Log("Using cave...");

                break;
            default:
                Debug.Log("error");
                break;
        }
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
                setInfo(player.guns[item_num].name, "Purchised!");
            }        
            else
            {
                //PlayerStats.gun = i;
                Debug.Log("Not enough money");
            }
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
                player.caves[i].purchised = true;
                player.caves[i].ShowCave();
            }
        }
        else
        {
            //PlayerStats.gun = i;
        }
    }


    public bool ConfirmBuy(int price)
    {
        int money = player.pocket.money;
        if (price > money) return false;
        player.pocket.AddMoney(-price);
        SaveSystem.SavePlayer(player);
        return true;

    }

}
