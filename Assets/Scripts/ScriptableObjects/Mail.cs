using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Mail", menuName = "Windows/MailWindow")]
public class Mail : ScriptableObject
{
    public string title;
    public string author;
    [TextArea(15,20)] public string body;
    public string mission;
}
