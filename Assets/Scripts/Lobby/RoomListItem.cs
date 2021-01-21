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
        public RoomInfo Info;

        //funcions
        public void Setup(RoomInfo _info)
        {
            Info = _info;
            text.text = Info.Name;
        }
        public void Onclick()
        {
            Launcher.Instance.JoinRoom(Info);
        }
    }
}
