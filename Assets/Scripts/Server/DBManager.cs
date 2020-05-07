using System.Collections;
using System.Collections.Generic;
    using System;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class DBManager : MonoBehaviour
{
    public InputField inputUserName;

    public GameObject inputPasswordObject;
    public InputField inputPassword;

    public GameObject inputPasswordConfirmObject;
    public InputField inputPasswordConfirm;

    public GameObject buttonSubmit;

    public string userName;
    public string password;
    public string passwordConfirm;

    public string serverCommand = "";
    private void Start()
    {
        inputPasswordObject.SetActive(false);
        inputPasswordConfirmObject.SetActive(false);
        PlayerData data = SaveSystem.LoadPlayer();
        if (data != null)
        {

            userName = data.playerName;
            password = data.playerPassword;
            inputUserName.text = userName;
            inputPassword.text = password;
            Welcome();
            Debug.Log(Application.persistentDataPath);

        }

    }

    public void Welcome()
    {
        if (CheckInputField()) {
            Debug.Log("Server command: " + serverCommand);
            switch (serverCommand)
            {
                case "Login":
                    Debug.Log("Server logging in");
                    StartCoroutine(ServerLogin());
                    break;
                case "Register":
                    Debug.Log("Server registration");
                    StartCoroutine(ServerRegister());
                    break;
                default:
                    Debug.Log("What to do with user?");
                    StartCoroutine(WhatToDoWithUser());
                    break;
            }
        }
    }

    private bool CheckInputField()
    {
        switch (serverCommand)
        {
            case "Login":
                if (CheckInputFieldName() /*&& CheckInputFieldPassword()*/) return true;
                break;
            case "Register":
                if (CheckInputFieldName() /*&& *//*CheckInputFieldPassword() && CheckInputFieldPasswordConfirm()*/) return true;
                break;
            default:
                if (CheckInputFieldName()) return true;
                break;
        }

        return false;
    }

    private bool CheckInputFieldName()
    {
        if (inputUserName.text != "")
        {
            return true;// userName = inputUserName.text;
        }
        else
        {
            Debug.Log("No login in inputfield");
            return false;
        }
    }

    private bool CheckInputFieldPassword()
    {
        if (inputPassword.text != "")
        {
            return true;// userName = inputUserName.text;
        }
        else
        {
            Debug.Log("No Password in inputfield");
            return false;
        }
    }

    private bool CheckInputFieldPasswordConfirm()
    {
        bool all = false;
        bool notEmpty = false;
        bool match = false;

        //Proverka, estj li parol
        if (inputPasswordConfirm.text != "")
        {
            notEmpty = true;
        }
        else
        {
            Debug.Log("No PasswordConfirm in inputfield");
        }
        //Proverka, sovpadaet li paroli
        if (inputPasswordConfirm.text == inputPassword.text)
        {
            match = true;
        }
        else
        {
            Debug.Log("Paroli ne sovpadajut");
        }
        //Summa
        if (notEmpty && match) all = true;
        return all;
    }

    private IEnumerator WhatToDoWithUser()
    {
        userName = inputUserName.text;

        WWWForm form = new WWWForm(); // переменная которую мы отошлем серверу
        form.AddField("Name", userName);

        WWW www = new WWW("http://p64328.hostru08.fornex.host/cameout.com/test/welcome.php", form);
        yield return www; // ждем ответ от сервера

        if (www.error != null) // проверяем, есть ли ошибка
        {
            Debug.Log("Ошибка: " + www.error); // выводим ошибку
            yield break; // прерываем инумератор
        }
        Debug.Log("Server otvetil: " + www.text); // выводим текст от сервера
        serverCommand = www.text;
        if (www.text == "Login")
        {
            loginf();
        } else if (www.text == "Register")
        {
            registerf();
        }
        Welcome();
    }
    private void loginf()
    {
        inputPasswordObject.SetActive(true);
        inputUserName.enabled = false;
        //buttonNext.text = "asas";
    }

    private void registerf()
    {
        inputUserName.enabled = false;
        inputPasswordObject.SetActive(true);
        inputPasswordConfirmObject.SetActive(true);

        const string glyphs = "abcdefghijklmnopqrstuvwxyz0123456789";
        int charAmount = Random.Range(45, 50); //set those to the minimum and maximum length of your string
        for (int i = 0; i < charAmount; i++)
        {
            password = "";
            password += glyphs[Random.Range(0, glyphs.Length)];
            PlayerStats.playerPassword += password;

        }
    }

    private IEnumerator ServerLogin()
    {
/*        PlayerData data = SaveSystem.LoadPlayer();
        if(data != null && data.playerName == inputUserName.text) {
            userName = data.playerName;
            password = data.playerPassword;
        }*/

        //userName = inputUserName.text;
        //password = inputPassword.text;
        Debug.Log("Login in system: "+userName+" password in system: "+ password);
        

        WWWForm form = new WWWForm(); // переменная которую мы отошлем серверу
        form.AddField("Name", userName);
        form.AddField("Pass", password);

        WWW www = new WWW("http://p64328.hostru08.fornex.host/cameout.com/test/login.php", form);
        yield return www; // ждем ответ от сервера

        if (www.error != null) // проверяем, есть ли ошибка
        {
            Debug.Log("Ошибка: " + www.error); // выводим ошибку
            yield break; // прерываем инумератор
        }
        Debug.Log("Server otvetil: " + www.text); // выводим текст от сервера
        serverCommand = www.text;
        if (www.text == "OK")
        {
            Debug.Log("SUPER!!!!");

            //PEREHOD V IGRU!!!!!!
            StartCoroutine(StartGame(userName));

        }
        else if (www.text == "WRONG")
        {
            Debug.Log("NE SOVPADAJET PAROL");
        }
    }

    private IEnumerator ServerRegister()
    {
        userName = inputUserName.text;
        password = PlayerStats.playerPassword;

        WWWForm form = new WWWForm(); // переменная которую мы отошлем серверу
        form.AddField("Name", userName);
        form.AddField("Pass", password);

        WWW www = new WWW("http://p64328.hostru08.fornex.host/cameout.com/test/register.php", form);
        yield return www; // ждем ответ от сервера

        if (www.error != null) // проверяем, есть ли ошибка
        {
            Debug.Log("Ошибка: " + www.error); // выводим ошибку
            yield break; // прерываем инумератор
        }
        Debug.Log("Server otvetil: " + www.text); // выводим текст от сервера
        serverCommand = www.text;
        
        if (www.text == "REGISTERED")
        {
            Debug.Log("SUPER!!!!");
            //PEREHOD V IGRU!
            StartCoroutine(StartGame(userName));
        }
    }


    private IEnumerator StartGame(string login)
    {

        PlayerStats.playerName = login;


        WWWForm form = new WWWForm(); // переменная которую мы отошлем серверу
        form.AddField("Name", login);

        Debug.Log("Getting user id in process..");
        WWW www = new WWW("http://p64328.hostru08.fornex.host/cameout.com/test/findid.php", form);
        yield return www; // ждем ответ от сервера

        if (www.error != null) // проверяем, есть ли ошибка
        {
            Debug.Log("Ошибка: " + www.error); // выводим ошибку
            yield break; // прерываем инумератор
        }
        Debug.Log("ID: " + www.text); // выводим текст от сервера




        PlayerStats.userID = Int32.Parse(www.text);



        Application.LoadLevel("main");
    }
}

