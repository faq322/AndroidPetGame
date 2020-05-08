using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;


public static class SaveSystem
{
    //Save on device
    public static void SavePlayer (PlayerStats player)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.fun";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(player);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerData LoadPlayer()
    {
        string path = Application.persistentDataPath + "/player.fun";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            return data;
        }else
        {
            Debug.Log("Faij dla sohranenija ne najden");
            return null;
        }
    }




    //Server
    public static IEnumerator SavePlayerOnServer(PlayerStats player)
    {
        PlayerData data = new PlayerData(player);
        

        Debug.Log(data.playerLvl);
        Debug.Log(data.playerExp);
        Debug.Log(data.playerExpToNextLvl);
        Debug.Log(data.gameLvl);
        Debug.Log(data.hp);
        Debug.Log(data.maxHP);



        WWWForm form = new WWWForm(); // переменная которую мы отошлем серверу
        form.AddField("id",data.playerid);
        form.AddField("playerLvl", data.playerLvl);
        form.AddField("playerExp", data.playerExp);
        form.AddField("playerExpToNextLvl", data.playerExpToNextLvl);
        form.AddField("gameLvl", data.gameLvl);
        form.AddField("hp", data.hp);
        form.AddField("maxHP", data.maxHP);
        form.AddField("money", data.money);
        form.AddField("diamonds", data.diamonds);


        WWW www = new WWW("http://p64328.hostru08.fornex.host/cameout.com/test/saveplayer.php", form);
        Debug.Log("Saving on the server...");
        yield return www; // ждем ответ от сервера

        if (www.error != null) // проверяем, есть ли ошибка
        {
            Debug.Log("Ошибка: " + www.error); // выводим ошибку
            yield break; // прерываем инумератор
        }
        Debug.Log("Server otvetil: " + www.text); // выводим текст от сервера
        if (www.text == "Player data saved")
        {
            Debug.Log("Data saved on server!!!!");

            //PEREHOD V IGRU!!!!!!
            //StartGame(userName);

        }
        else
        {
            Debug.Log("Ne udalosj sohranitj dannie na srver");
        }

        yield return null;
    }


/*    //Ne rabotayet do konca
    public static IEnumerator LoadPlayerFromServer(PlayerStats player)
    {
        PlayerData data = new PlayerData(player);


        WWWForm form = new WWWForm(); // переменная которую мы отошлем серверу
        form.AddField("id", data.playerid);


        WWW www = new WWW("http://p64328.hostru08.fornex.host/cameout.com/test/loaddata.php", form);
        Debug.Log("Loading players data from the server...");
        yield return www; // ждем ответ от сервера

        if (www.error != null) // проверяем, есть ли ошибка
        {
            Debug.Log("Ошибка: " + www.error); // выводим ошибку
            yield break; // прерываем инумератор
        }
        Debug.Log("Server otvetil: " + www.text); // выводим текст от сервера

        //PlayerData data = JsonParse(www.text);

        yield return data;
    }*/

    static PlayerData JsonParse(string jsonstring)
    {
        return JsonUtility.FromJson<PlayerData>(jsonstring);
    }
}
