using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ButtonBehaviour : MonoBehaviour
{
    public float timeBeforeLose = 2.5f;
    public UnityEvent lose;

    float timer = 0;

    public Color defaultColor;
    public Color endColor;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(behaviour());
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        GetComponent<Image>().color = Color.Lerp(defaultColor, endColor, timer/timeBeforeLose);
    }

    IEnumerator behaviour()
    {
        yield return new WaitForSeconds(timeBeforeLose);
        Debug.Log("Lose!");
        lose.Invoke();
    }
}
