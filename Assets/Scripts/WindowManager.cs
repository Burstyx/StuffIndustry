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

    public AudioSource audioSource;
    public AudioClip clickClip;


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
                    mailWindow.GetComponent<Animator>().SetBool("opened", true);
                    blockInput.SetActive(true);
                    audioSource.PlayOneShot(clickClip);
                    break;
                case "crypter":
                    if (MissionManager.instance.crypterMission > 0)
                    {
                        crypterWindow.SetActive(true);
                        audioSource.PlayOneShot(clickClip);
                    }
                    else
                        NotificationsManager.instance.NewNotification("Impossible", "You don't have any mission to do here. Check your email!");
                    break;
                case "antivirus":
                    if (MissionManager.instance.antivirusMission > 0)
                    {
                        antivirusWindow.GetComponent<Animator>().SetBool("opened", true);
                        blockInput.SetActive(true);
                        audioSource.PlayOneShot(clickClip);
                    }
                    else
                        NotificationsManager.instance.NewNotification("Impossible", "You don't have any mission to do here. Check your email!");
                    break;
                case "explorer":
                    if (Manager.instance.admin)
                    {
                        chestWindow.GetComponent<Animator>().SetBool("opened", true);
                        blockInput.SetActive(true);
                        audioSource.PlayOneShot(clickClip);
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

        mailWindow.GetComponent<Animator>().SetBool("opened", false);
        crypterWindow.SetActive(false);
        antivirusWindow.GetComponent<Animator>().SetBool("opened", false);
        chestWindow.GetComponent<Animator>().SetBool("opened", false);
    }
}
