using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;

public class MailManager : MonoBehaviour
{
    public GameObject mailInfoBtn;
    public GameObject mailBody;
    public GameObject mailInfoBtnList;
    public GameObject mailBodyList;

    public Dictionary<GameObject, GameObject> mailIdentifier = new Dictionary<GameObject, GameObject>();

    public Mail[] mails;
    int mailIndex = 0;

    public static MailManager instance;

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
        NextMail();
    }

    private void Update()
    {
        List<GameObject> bodyList = new(mailIdentifier.Values);

        int openedMail = 0;

        Debug.Log(openedMail);

        for (int i = 0; i < bodyList.Count; i++)
        {
            if (bodyList[i].activeInHierarchy)
            {
                openedMail++;
                Debug.Log("is active");
            }
            if(openedMail > 2)
            {
                for (int j = 0; j < bodyList.Count; j++)
                {
                    bodyList[j].SetActive(false);
                }
            }
        }
    }

    public void NextMail()
    {
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
    }

    public void FindMail(GameObject mailInfoBtn)
    {
        GameObject mailFound = null;
        mailIdentifier.TryGetValue(mailInfoBtn, out mailFound);

        if (mailFound)
        {
            mailFound.SetActive(true);
        }
    }
}
