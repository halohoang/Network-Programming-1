using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using TMPro;

namespace SAE
{

    public class RoomListItem : MonoBehaviour
    {
        //Variales
        [SerializeField] TMP_Text text;
        RoomInfo info;

        //funcions
        public void Setup(RoomInfo _info)
        {
            info = _info;
            text.text = info.Name;
        }
        public void Onclick()
        {
            Launcher.Instance.JoinRoom(info);
        }
    }
}
