using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NotificationsManager : MonoBehaviour
{
    public GameObject notificationPanel;
    public List<Notification> notificationQueue = new();

    public float notificationShowTime = 3f;
    float notificationTimer;

    public static NotificationsManager instance;

    public AudioClip notifClip;
    public AudioSource audioSource;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            return;
        }
        Debug.Log("NotificationsManager instance already created!");
    }

    public void NewNotification(string title, string body)
    {
        Notification notification = new Notification();
        notification.title = title;
        notification.body = body;
        notificationQueue.Add(notification);
    }

    private void Update()
    {
        if(notificationQueue.Count > 0 && notificationTimer == 0)
        {
            for (int i = 0; i < notificationQueue.Count; i++)
            {
                notificationTimer += Time.deltaTime;
                ShowNotification();
            }
        }

        if(notificationTimer > 0)
        {
            notificationTimer += Time.deltaTime;
            if(notificationTimer >= notificationShowTime)
            {
                notificationTimer = 0;
                notificationQueue.RemoveAt(0);
                HideNotification();
            }
        }
    }

    void ShowNotification()
    {
        notificationPanel.transform.Find("Title").GetComponent<TMP_Text>().text = notificationQueue[0].title;
        notificationPanel.transform.Find("Message").GetComponent<TMP_Text>().text = notificationQueue[0].body;
        notificationPanel.GetComponent<Animator>().SetBool("notif", true);

        audioSource.clip = notifClip;
        audioSource.PlayOneShot(notifClip);
    }

    void HideNotification()
    {
        notificationPanel.GetComponent<Animator>().SetBool("notif", false);
    }
}
