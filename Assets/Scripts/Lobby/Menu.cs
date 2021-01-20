using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SAE
{

    public class Menu : MonoBehaviour
    {
        //variables
        public string menuName;
        public bool open;
        //functions
        public void Open()
        {
            open = true;
            gameObject.SetActive(true);
        }
        public void Close()
        {
            open = false;
            gameObject.SetActive(false);
        }

    }

}