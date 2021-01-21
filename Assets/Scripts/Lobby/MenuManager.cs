using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SAE
{

    public class MenuManager : MonoBehaviour
    {
        //variables
        public static MenuManager Instance;
        [SerializeField] Menu[] menus;

        //functions
        private void Awake()
        {
            Instance = this;
        }
        public void OpenMenu(string menuName)
        {
            for (int i = 0; i < menus.Length; i ++)
            {
                if (menus[i].menuName == menuName)
                {
                    menus[i].Open();
                }
                else if (menus[i].open)
                {
                    CloseMenu(menus[i]);
                }
            }
        }
        public void OpenMenu(Menu menu)
        {
            for (int i = 0; i < menus.Length; i++)
            {
                if (menus[i].open)
                {
                    CloseMenu(menus[i]);
                }
            }
            menu.Open();
        }
        public void CloseMenu(Menu menu)
        {
            menu.Close();
        }
    }

}