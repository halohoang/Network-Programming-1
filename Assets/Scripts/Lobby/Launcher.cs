using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using System.Linq;
namespace SAE
{

    public class Launcher : MonoBehaviourPunCallbacks
    {

        //variables
        [SerializeField] private TMP_InputField _roomNameInputField;
        [SerializeField] private TMP_Text errorText;
        [SerializeField] private TMP_Text _roomNameText;
        [SerializeField] private Transform _roomListContent;
        [SerializeField] private GameObject _roomListItemPrefab;
        [SerializeField]  GameObject playerListItemPrefab;
        [SerializeField] Transform playerListContent;

        public static Launcher Instance;
        //functions
        // Start is called before the first frame update
        private void Awake()
        {
            Instance = this;
        }
        void Start()
        {
            PhotonNetwork.ConnectUsingSettings();
        }

        public override void OnConnectedToMaster()
        {
            PhotonNetwork.JoinLobby();
        }

        public override void OnJoinedLobby()
        {
            MenuManager.Instance.OpenMenu("Title");
            PhotonNetwork.NickName = "Operator Serial" + Random.Range(0, 1000).ToString("0000");
        }
        // Update is called once per frame
        public void CreateRoom()
        {
            if (string.IsNullOrEmpty(_roomNameInputField.text))
            {
                return;
            }
            PhotonNetwork.CreateRoom(_roomNameInputField.text);
            MenuManager.Instance.OpenMenu("Loading");
        }
        public override void OnJoinedRoom()
        {
            MenuManager.Instance.OpenMenu("Room");
            _roomNameText.text = PhotonNetwork.CurrentRoom.Name;

            Player[] players = PhotonNetwork.PlayerList;

            for (int i = 0; i < players.Count(); i++)
            {
                Instantiate(playerListItemPrefab, playerListContent).GetComponent<PlayerListitem>().SetUp(players[i]);

            }
        }

        public override void OnCreateRoomFailed(short returnCode, string message)
        {
            errorText.text = "Room Creation failed" + message;
            MenuManager.Instance.OpenMenu("Error");
        }
        public void LeaveRoom()
        {
            PhotonNetwork.LeaveRoom();
            MenuManager.Instance.OpenMenu("Loading");
        }
        public override void OnLeftRoom()
        {
            MenuManager.Instance.OpenMenu("Title");

        }
        public void JoinRoom(RoomInfo info)
        {
            PhotonNetwork.JoinRoom(info.Name);
            MenuManager.Instance.OpenMenu("Loading");

            
        }

        public override void OnRoomListUpdate(List<RoomInfo> roomList)
        {
            foreach(Transform trans in _roomListContent)
            {
                Destroy(trans.gameObject);
            }
            for (int i = 0; i < roomList.Count; i++)
            {
                Instantiate(_roomListItemPrefab, _roomListContent).GetComponent<RoomListItem>().Setup(roomList[i]);
            }
        }
        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            Instantiate(playerListItemPrefab, playerListContent).GetComponent<PlayerListitem>().SetUp(newPlayer);
        }
        
    }

}