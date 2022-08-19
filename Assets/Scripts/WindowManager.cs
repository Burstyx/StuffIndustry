using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowManager : MonoBehaviour
{
    public static WindowManager instance;

    public GameObject mailWindow;
    public GameObject crypterWindow;
    public GameObject gameCreatorWindow;
    public GameObject coffeeMakerWindow;
    public GameObject antivirusWindow;

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
        blockInput.SetActive(true);
        switch(windowName)
        {
            case "mail":
                mailWindow.SetActive(true);
                break;
            case "crypter":
                crypterWindow.SetActive(true);
                break;
            case "gameCreator":
                gameCreatorWindow.SetActive(true);
                break;
            case "coffeeMaker":
                coffeeMakerWindow.SetActive(true);
                break;
            case "antivirus":
                antivirusWindow.SetActive(true);
                break;
            default:
                Debug.LogError("Window name is not correct");
                break;
        }
    }

    public void CloseWindow()
    {
        blockInput.SetActive(false);

        mailWindow.SetActive(false);
        crypterWindow.SetActive(false);
        gameCreatorWindow.SetActive(false);
        coffeeMakerWindow.SetActive(false);
        antivirusWindow.SetActive(false);
    }
}
