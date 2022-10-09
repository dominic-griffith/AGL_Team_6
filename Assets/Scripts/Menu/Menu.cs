using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public string menuName;
    public bool isOpen;

    public void OpenMenu()
    {
        isOpen = true;
        gameObject.SetActive(isOpen);
    }
    
    public void CloseMenu()
    {
        isOpen = false;
        gameObject.SetActive(isOpen);
    }
}
