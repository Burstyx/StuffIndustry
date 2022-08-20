using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopupWindow : MonoBehaviour
{
    string[] importantFileName = new string[] { "Password.txt", "NuclearCode.txt", "EmployeesList.txt", "DONOTDELETE.txt", "nevergonnagiveyouup.exe" };
    string[] nonImportantFileName = new string[] { "trojan.exe", "destructor.exe" };

public bool waitToUser = false;

    bool isImportant;
    string currentFile;

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
            Manager.instance.LoseGame();
            waitToUser = false;
        }
    }
}
