using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PopupWindow : MonoBehaviour
{
    public GameObject mailCheckerLogo;
    public Image bg;

    public Sprite niancatBg;

    string[] importantFileName = new string[] { 
        "Password.txt",
        "NuclearCode.txt",
        "EmployeesList.json",
        "DONOTDELETE.txt",
        "BossSecretFile.txt",
        "MyWebsite.html",
        "HowToBecomeAGoodEmployee.mp4",
        "mailchecker.exe"
    };
    string[] nonImportantFileName = new string[] {
        "trojan.exe",
        "destructor.exe",
        "AbsolutelyNonMaliciousCode.virus",
        "NianCat.malware",
        "IseeYou.exe",
        "behindyou.behindyou",
        "youcantescape.exe",
        "stuffIndustryDestructor.exe"
    };

public bool waitToUser = false;

    bool isImportant;
    string currentFile;

    public AudioSource audioSource;
    public AudioClip popupClip;

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowPopup()
    {
        audioSource.PlayOneShot(popupClip);

        int whatKind = Random.Range(0, 2);
        waitToUser = true;
        if (whatKind == 0)
        {
            currentFile = importantFileName[Random.Range(0, importantFileName.Length)];
            isImportant = true;
        }
        else
        {
            currentFile = nonImportantFileName[Random.Range(0, nonImportantFileName.Length)];
            isImportant = false;
        }

        transform.Find("FileName").GetComponent<TMP_Text>().text = currentFile;
        gameObject.SetActive(true);
    }

    public void KeepFile()
    {
        if (isImportant)
        {
            Debug.Log("good");
            gameObject.SetActive(false);
            waitToUser = false;
        }
        else
        {
            Debug.Log("lose");
            if(currentFile == "NianCat.malware")
            {
                bg.sprite = niancatBg;
            }
            Manager.instance.LoseGame();
            waitToUser = false;
        }
        
    }

    public void DeleteFile()
    {
        if (!isImportant)
        {
            Debug.Log("good");
            gameObject.SetActive(false);
            waitToUser = false;
        }
        else
        {
            Debug.Log("lose");
            if (currentFile == "mailchecker.exe")
            {
                mailCheckerLogo.SetActive(false);
            }
            Manager.instance.LoseGame();
            waitToUser = false;
        }
    }
}
