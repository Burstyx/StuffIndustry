using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    int hour = 6;
    int minute = 0;
    float second;

    string hourText;
    string minuteText;

    public TMP_Text horloge;

    public GameObject logos;
    public GameObject windows;
    public GameObject bg;

    public bool lose = false;
    public bool admin = false;

    public static Manager instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            return;
        }
        Debug.Log("Manager instance already created!");
    }

    private void Update()
    {
        Debug.Log(lose);

        second += Time.deltaTime;
        if(second >= 5)
        {
            minute++;
            second = 0;
        }
        if(minute >= 60)
        {
            hour++;
            minute = 0;
        }
        if(hour >= 24)
        {
            hour = 0;
        }

        if(hour.ToString().Length == 1)
        {
            hourText = "0" + hour;
        }
        else
        {
            hourText = hour.ToString();
        }

        if (minute.ToString().Length == 1)
        {
            minuteText = "0" + minute;
        }
        else
        {
            minuteText = minute.ToString();
        }

        horloge.text = hourText + ":" + minuteText;
    }

    public void LoseGame()
    {
        lose = true;
        NotificationsManager.instance.NewNotification("You failed", "You failed");
        StartCoroutine(ShutdownComputer());
        WindowManager.instance.CloseWindow();
    }

    IEnumerator ShutdownComputer()
    {
        yield return new WaitForSeconds(5f);
        windows.SetActive(false);
        logos.SetActive(false);
        bg.SetActive(false);
        horloge.gameObject.SetActive(false);
        yield return new WaitForSeconds(2f);
        NotificationsManager.instance.NewNotification("You are in a loop...", "...that need to be restarted");
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ShutdownComputerGoodEnd()
    {
        NotificationsManager.instance.NewNotification("Command executed", "Shutting down the computer...");
        StartCoroutine(GoodEndStory());
        WindowManager.instance.CloseWindow();
    }

    IEnumerator GoodEndStory()
    {
        yield return new WaitForSeconds(2f);
        windows.SetActive(false);
        logos.SetActive(false);
        bg.SetActive(false);
        horloge.gameObject.SetActive(false);
        yield return new WaitForSeconds(2f);
        NotificationsManager.instance.NewNotification("", "You won...");
        yield return new WaitForSeconds(2f);
        NotificationsManager.instance.NewNotification("", "The first guy to finish... Alive...");
        yield return new WaitForSeconds(2f);
        NotificationsManager.instance.NewNotification("", "You are a hero... You are special...");
        yield return new WaitForSeconds(2f);
        NotificationsManager.instance.NewNotification("", "But the loop, is unbeatable.");
        yield return new WaitForSeconds(2f);
        NotificationsManager.instance.NewNotification("", "You won...");
        yield return new WaitForSeconds(2f);
        NotificationsManager.instance.NewNotification("", "But it is not the end...");
        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
