using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrypterWindow : MonoBehaviour
{
    public GameObject buttonInstance;
    public GameObject buttonList;

    bool spawnButton;
    int numberOfButtonClicked;

    public int numberOfButtonToClick;
    public float maxTimeSpawn;
    public float spawnSpeed;
    public float timeBeforeLose;

    float timer;

    public AudioSource audioSource;
    public AudioClip clip;

    // Update is called once per frame
    void Update()
    {
        if (spawnButton)
        {
            timer += Time.deltaTime * (spawnSpeed + MissionManager.instance.crypterMissionDone);
            if(timer >= Random.Range(maxTimeSpawn/2, maxTimeSpawn))
            {
                Vector2 calcRandomPosition = new Vector2(Random.Range(buttonInstance.GetComponent<RectTransform>().rect.width, Screen.width - buttonInstance.GetComponent<RectTransform>().rect.width), Random.Range(-buttonInstance.GetComponent<RectTransform>().rect.height, -Screen.height + buttonInstance.GetComponent<RectTransform>().rect.height) + Screen.height);

                GameObject button = Instantiate(buttonInstance, calcRandomPosition, Quaternion.identity, buttonList.transform);
                button.GetComponent<Button>().onClick.AddListener(() => clicked(button));
                button.GetComponent<ButtonBehaviour>().lose.AddListener(() => lose());
                button.GetComponent<ButtonBehaviour>().timeBeforeLose = timeBeforeLose - 0.25f * MissionManager.instance.crypterMissionDone;

                timer = 0;
            }
            if(numberOfButtonClicked >= numberOfButtonToClick)
            {
                Destroy(MissionManager.instance.mailLinkToCryterMission);
                MissionManager.instance.crypterMissionDone++;
                MissionManager.instance.crypterMission--;
                spawnButton = false;
                Debug.Log("Good job!");
                MissionManager.instance.inMission = false;
                WindowManager.instance.CloseWindow();
            }
        }
        else
        {
            for (int i = 0; i < buttonList.transform.childCount; i++)
            {
                Destroy(buttonList.transform.GetChild(i).gameObject);
            }
            numberOfButtonClicked = 0;
        }
       
        if(gameObject.activeInHierarchy)
        {
            spawnButton = true;
            MissionManager.instance.inMission = true;
        }
        else
        {
            spawnButton = false;
        }
    }

    public void clicked(GameObject btn)
    {
        Destroy(btn.gameObject);
        numberOfButtonClicked++;
        audioSource.PlayOneShot(clip);
    }

    public void lose()
    {
        Manager.instance.LoseGame();
    }
}
