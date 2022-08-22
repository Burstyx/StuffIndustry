using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using System;
using System.Collections;
using Random = UnityEngine.Random;

public class MailManager : MonoBehaviour
{
    public GameObject mailInfoBtn;
    public GameObject mailBody;
    public GameObject mailInfoBtnList;
    public GameObject mailBodyList;

    public Dictionary<GameObject, GameObject> mailIdentifier = new Dictionary<GameObject, GameObject>();

    public Mail[] mails;
    int mailIndex = 0;

    bool hidden;

    public static MailManager instance;

    public AudioSource audioSource;
    public AudioClip clickClip;
    public AudioClip doorClip;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            return;
        }
        Debug.Log("MailManager instance already created!");
    }

    void Start()
    {
        StartCoroutine(DelayedStart());
    }

    int secondToWait = 30;

    IEnumerator DelayedStart()
    {
        yield return new WaitForSeconds(2f);
        NextMail();
        StartCoroutine(NextMission());
    }

    IEnumerator NextMission()
    {
        yield return new WaitForSeconds(secondToWait);
        if (!MissionManager.instance.inMission && mailInfoBtnList.transform.childCount <= 8)
        {
            NextMail();
        }
        StartCoroutine(NextMission());
        secondToWait = Random.Range(30, 35);
    }

    private void Update()
    {
        List<GameObject> bodyList = new(mailIdentifier.Values);

        if (hidden && mailBodyList.activeInHierarchy)
        {
            hidden = false;
            for (int i = 0; i < bodyList.Count; i++)
            {
                bodyList[i].SetActive(false);
            }
        }

        if (!mailBodyList.activeInHierarchy)
        {
            hidden = true;
        }
    }

    public void NextMail()
    {
        if (mailIndex > mails.Length)
            return;
        GameObject mailBodyInstance = Instantiate(mailBody, mailBodyList.transform);
        mailBodyInstance.transform.Find("Title").GetComponent<TMP_Text>().text = mails[mailIndex].title;
        mailBodyInstance.transform.Find("Author").GetComponent<TMP_Text>().text = mails[mailIndex].author;
        mailBodyInstance.transform.Find("Body").GetComponent<TMP_Text>().text = mails[mailIndex].body;
        mailBodyInstance.SetActive(false);

        GameObject mailInfoInstance = Instantiate(mailInfoBtn, mailInfoBtnList.transform);
        mailInfoInstance.transform.Find("Title").GetComponent<TMP_Text>().text = mails[mailIndex].title + " - " + mails[mailIndex].author;
        mailInfoInstance.GetComponent<Button>().onClick.AddListener(() => FindMail(mailInfoInstance));

        mailIdentifier.Add(mailInfoInstance, mailBodyInstance);

        NotificationsManager.instance.NewNotification("New mail!", "You just receive a new mail, go check it out!");

        if (mails[mailIndex].mission != "")
        {
            switch (mails[mailIndex].mission)
            {
                case "crypter":
                    MissionManager.instance.mailLinkToCryterMission = mailInfoInstance;
                    MissionManager.instance.crypterMission++;

                    break;
                case "antivirus":
                    MissionManager.instance.mailLinkToAntivirusMission = mailInfoInstance;
                    MissionManager.instance.antivirusMission++;
                    break;
                case "goodend":
                    Manager.instance.admin = true;
                    NotificationsManager.instance.NewNotification("Permissions updated", "You now have the admin permission!");
                    StartCoroutine(TriggerImmediateNextMail());
                    break;
                case "toctoc":
                    audioSource.PlayOneShot(doorClip);
                    break;
            }
        }

        mailIndex++;
    }

    IEnumerator TriggerImmediateNextMail()
    {
        yield return new WaitForSeconds(2f);
        MailManager.instance.NextMail();
    }

    public void FindMail(GameObject mailInfoBtn)
    {
        GameObject mailFound = null;
        mailIdentifier.TryGetValue(mailInfoBtn, out mailFound);

        if (mailFound)
        {
            List<GameObject> bodyList = new(mailIdentifier.Values);
            for (int i = 0; i < bodyList.Count; i++)
            {

                bodyList[i].SetActive(false);
            }
            mailFound.SetActive(true);
            audioSource.PlayOneShot(clickClip);
        }
    }
}
