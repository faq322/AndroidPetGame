using System.Collections;
using System.Collections.Generic;
    using System;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class DBManager : MonoBehaviour
{
    public InputField inputUserName;

    public Text textError;
    public Text textProcess;

    public GameObject buttonSubmit;

    public string userName;
    public string password;

    public string serverCommand = "";
    private void Start()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        if (data != null )
        {
            userName = data.playerName;
            password = data.playerPassword;
            inputUserName.text = userName;
            if (CheckInputFieldName()) Welcome();
            Debug.Log(Application.persistentDataPath);
            Debug.Log(data.playerName);
        }

    }

    public void Welcome()
    {
        Error("");
        if (CheckInputFieldName()) {
            Debug.Log("Server command: " + serverCommand);
            switch (serverCommand)
            {
                case "Login":
                    Debug.Log("Server logging in");
                    Process("Logging in...");
                    StartCoroutine(ServerLogin());
                    break;
                case "Register":
                    Debug.Log("Server registration");
                    Process("Registering...");
                    StartCoroutine(ServerRegister());
                    break;
                default:
                    Debug.Log("What to do with user?");
                    Process("Searching for your login...");
                    StartCoroutine(WhatToDoWithUser());
                    break;
            }
        }
    }


    private bool CheckInputFieldName()
    {
        if (inputUserName.text != "")
        {
            return true;// userName = inputUserName.text;
        }
        else
        {
            Error("No Login in inputfield!");
            return false;
        }
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
        inputUserName.enabled = false;
        //buttonNext.text = "asas";
    }

    private void registerf()
    {
        inputUserName.enabled = false;
        
        //Создание случайного пароля
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
            Process("Logging in complete!");
            //PEREHOD V IGRU!!!!!!
            StartCoroutine(StartGame(userName));

        }
        else 
        {
            Error("USER ALREADY EXISTS");
            inputUserName.enabled = true;
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
            Process("Registration complete!");
            StartCoroutine(StartGame(userName));
        }
    }

    public void Process(string process)
    {
        textProcess.text = process;
    }

    public void Error(string error)
    {
        textError.text = error;
        Debug.Log(error);
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

