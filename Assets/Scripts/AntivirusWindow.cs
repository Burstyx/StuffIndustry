using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AntivirusWindow : MonoBehaviour
{
    string status = "scan";
    float progress;
    bool canProcess = false;

    public float progressSpeed;
    public int chanceToGetAPopup;

    int popupChance = 0;

    public Slider progressBar;
    public Button btn;
    public GameObject popup;

    public AudioSource audioSource;
    public AudioClip clickClip;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (canProcess)
        {
            MissionManager.instance.inMission = true;
            if (status == "scan")
            {
                progress += Time.deltaTime * (progressSpeed - MissionManager.instance.antivirusMissionDone/50);
                Debug.Log("scanning: " + progress);
                progressBar.value = progress;
                if(progressBar.value >= progressBar.maxValue)
                {
                    canProcess = false;
                    status = "fix";
                    btn.interactable = true;
                    btn.GetComponentInChildren<TMP_Text>().text = "Fix";
                    progress = 0;
                    progressBar.value = 0;
                }
            }
            else if (status == "fix" && !popup.GetComponent<PopupWindow>().waitToUser)
            {
                if (popupChance == chanceToGetAPopup)
                {
                    popup.GetComponent<PopupWindow>().ShowPopup();
                    popup.SetActive(true);
                    popupChance = 0;
                }
                else
                {
                    popupChance = Random.Range(0, chanceToGetAPopup + 1);
                    Debug.Log("fixing: " + progress);
                    Debug.Log("chance: " + popupChance);
                    progress += Time.deltaTime * progressSpeed;
                    progressBar.value = progress;
                }

                
                if (progressBar.value >= progressBar.maxValue)
                {
                    canProcess = false;
                    status = "scan";
                    btn.interactable = true;
                    progress = 0;
                    Destroy(MissionManager.instance.mailLinkToAntivirusMission);
                    MissionManager.instance.antivirusMission--;
                    progressBar.value = 0;
                    btn.GetComponentInChildren<TMP_Text>().text = "Scan";
                    MissionManager.instance.inMission = false;
                    MissionManager.instance.antivirusMissionDone++;
                    chanceToGetAPopup -= 50;
                    WindowManager.instance.CloseWindow();
                }
            }
        }
    }

    public void start()
    {
        canProcess = true;
        btn.interactable = false;

        audioSource.PlayOneShot(clickClip);
    }
}
