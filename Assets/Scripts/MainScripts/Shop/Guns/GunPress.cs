using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunPress : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    public GameObject playerObject;
    public PlayerStats player;
    //public Guns gun;


    public Text gunname;
    public Text guninfo;
    public static int guni;

    public void pick(int i)
    {
        gunname.text = player.guns[i].name;
        guninfo.text = player.guns[i].price.ToString();
        guni = i;
    }

    public void Buy()
    {
        int i = guni;
        Debug.Log(i);
        //Check if purchaused
        if (!player.guns[i].purchised)
        {
            if (ConfirmBuy(player.guns[i].price))
            {
                PlayerStats.gun = i;
                player.guns[i].purchised = true;
            }
        } else
        {
            //PlayerStats.gun = i;
        }
        
    }

    public bool ConfirmBuy(int price)
    {
        int money = player.pocket.money;
        if (price>money) return false;
        player.pocket.AddMoney(-price);
        return true;

    }
}
