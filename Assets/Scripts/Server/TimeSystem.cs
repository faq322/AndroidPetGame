using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Globalization;

public class TimeSystem : MonoBehaviour
{
    static TimeSpan playTime;
    static TimeSpan lastJoinTime;
    static TimeSpan offlineTime;

    private void Awake()
    {
       //StartCoroutine(CheckTimeOnline());
    }
    
    static public void CheckTime()
    {
       // playTime = Time.time;
    }

/*    //offline time
    public void CheckOffline()
    {
        TimeSpan ts;
        if (PlayerPrefs.HasKey("LastSession"))
        {
            ts = DateTime.Now - DateTime.Parse(PlayerPrefs.GetString("LastSession"));
            Debug.Log(string.Format("Vi otsutstvovali {0} dnej, {1} chasov, {2} minut, {3} sekund.", ts.Days, ts.Hours, ts.Minutes, ts.Seconds));
            Debug.Log(ts);
            lastJoinTime = ts;
        }
        PlayerPrefs.SetString("LastSession", DateTime.Now.ToString());
        Debug.Log("Application ending after " + Time.time + " seconds");
    }
*/


    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
           // CheckOffline();
        }
    }

    private void OnApplicationQuit()
    {
       // CheckOffline();
    }


    private IEnumerator CheckTimeOnline()
    {
       // WWWForm form = new WWWForm(); // переменная которую мы отошлем серверу
       // form.AddField("Name", );

        WWW www = new WWW("http://p64328.hostru08.fornex.host/cameout.com/test/timemanager.php");
        yield return www;
        if (www.error != null)
        {
            Debug.Log("Time error");
            yield break;
        }
        string time = www.text;
        string format = "yyyy.dd.HH.mm.ss";
        TimeSpan ts;
        if (PlayerPrefs.HasKey("LastSession"))
        {
            ts = DateTime.ParseExact(time, format, CultureInfo.InvariantCulture) - DateTime.ParseExact(PlayerPrefs.GetString("LastSession"), format, CultureInfo.InvariantCulture);
            Debug.Log(string.Format("Vi otsutstvovali {0} dnej, {1} chasov, {2} minut, {3} sekund.", ts.Days, ts.Hours, ts.Minutes, ts.Seconds));
            lastJoinTime = ts;
        }
        PlayerPrefs.SetString("LastSession", time);
    }
}
