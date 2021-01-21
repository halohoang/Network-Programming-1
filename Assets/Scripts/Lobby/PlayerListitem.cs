using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

namespace SAE
{

    public class PlayerListitem : MonoBehaviourPunCallbacks
    {
        //variables
        [SerializeField] TMP_Text text;
         Player player;

        //functions
        public void SetUp(Player _player)
        {
            player = _player;
            text.text = _player.NickName;

        }
        public override void OnPlayerLeftRoom(Player otherPlayer)
        {
            if (player == otherPlayer)
            {
                Destroy(gameObject);
            }
        }
        public override void OnLeftRoom()
        {
            Destroy(gameObject);
        }

    }
    

}