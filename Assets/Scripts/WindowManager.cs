using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowManager : MonoBehaviour
{
    public static WindowManager instance;

    public GameObject mailWindow;
    public GameObject crypterWindow;
    public GameObject antivirusWindow;
    public GameObject chestWindow;

    public GameObject blockInput;


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            return;
        }
        Debug.Log("WindowManager instance already created!");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OpenWindow(string windowName)
    {
        if (!Manager.instance.lose)
        {
            switch (windowName)
            {
                case "mail":
                    mailWindow.SetActive(true);
                    blockInput.SetActive(true);
                    break;
                case "crypter":
                    if (MissionManager.instance.crypterMission > 0)
                        crypterWindow.SetActive(true);
                    else
                        NotificationsManager.instance.NewNotification("Impossible", "You don't have any mission to do here. Check your email!");
                    break;
                case "antivirus":
                    if (MissionManager.instance.antivirusMission > 0)
                    {
                        antivirusWindow.SetActive(true);
                        blockInput.SetActive(true);
                    }
                    else
                        NotificationsManager.instance.NewNotification("Impossible", "You don't have any mission to do here. Check your email!");
                    break;
                case "explorer":
                    if (Manager.instance.admin)
                    {
                        chestWindow.SetActive(true);
                        blockInput.SetActive(true);
                    }
                    else
                    {
                        NotificationsManager.instance.NewNotification("Impossible", "You don't have the required permission!");
                    }
                    break;
                default:
                    Debug.LogError("Window name is not correct");
                    break;
            }
        }
    }

    public void CloseWindow()
    {
        blockInput.SetActive(false);

        mailWindow.SetActive(false);
        crypterWindow.SetActive(false);
        antivirusWindow.SetActive(false);
        chestWindow.SetActive(false);
    }
}
