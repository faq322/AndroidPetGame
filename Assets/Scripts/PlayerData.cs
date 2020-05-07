using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int playerid;
    public string playerName;
    public string playerPassword;


    public int playerLvl;
    public int playerExp;
    public int playerExpToNextLvl;
    public int gameLvl;

    public int hp;
    public int maxHP;


    public PlayerData (PlayerStats player)
    {
        playerid = PlayerStats.userID;
        playerPassword = PlayerStats.playerPassword;
        playerName = PlayerStats.playerName;


        playerLvl = player.PlayerLvl;
        playerExp = player.PlayerExp;
        playerExpToNextLvl = player.PlayerExpToNextLvl;
        gameLvl = player.gameLvl;

        hp = player.HP();
        maxHP = player.MaxHP;
    }


}
