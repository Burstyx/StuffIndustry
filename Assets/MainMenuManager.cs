using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenuManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip clickClip;
    public GameObject blockInput;
    public GameObject creditsScreen;

    private void Start()
    {
        GetComponent<Animator>().SetBool("opened", true);
    }

    public void StartGame()
    {
        audioSource.PlayOneShot(clickClip);
        StartCoroutine(StartGameAnimation());
    }
    public void OpenCredits()
    {
        audioSource.PlayOneShot(clickClip);
        creditsScreen.SetActive(true);
    }

    public void Back()
    {
        audioSource.PlayOneShot(clickClip);
        creditsScreen.SetActive(false);
    }

    public void LeaveGame()
    {
        audioSource.PlayOneShot(clickClip);
        Application.Quit();
    }

    IEnumerator StartGameAnimation()
    {
        GetComponent<Animator>().SetBool("opened", false);
        blockInput.SetActive(false);
        yield return new WaitForSeconds(2f);
        SceneManager.LoadSceneAsync("Game");
    }

}
