using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleOpenURL : MonoBehaviour
{
    public string URLlink;
    public void OpenDiscordLink(){Application.OpenURL(URLlink);}
}
