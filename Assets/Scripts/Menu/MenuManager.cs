using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;
    
    [Header("Dependencies")]
    public Menu[] menus;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }

    public void OpenMenu(string menuName)
    {
        foreach (Menu menu in menus)
        {
            if (menu.menuName == menuName)
            {
                menu.OpenMenu();
            }
            else if(menu.isOpen)
            {
                menu.CloseMenu();
            }
        }
    }

    public void CloseMenu(string menuName)
    {
        foreach (Menu menu in menus)
        {
            if (menu.menuName == menuName)
            {
                menu.CloseMenu();
            }
        }
    }

    public void CloseAllOpenMenus()
    {
        foreach (Menu menu in menus)
        {
            if (menu.isOpen)
            {
                menu.CloseMenu();
            }
        }
    }
    
    public bool IsMenuOpen(string menuName)
    {
        return menus.Any(t => t.menuName == menuName && t.isOpen);
    }
    
}
