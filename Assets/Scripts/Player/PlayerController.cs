using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviourPun, IPunObservable
{
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] new Rigidbody2D rigidbody;
    [SerializeField] GameObject cameraObject;
    [SerializeField] TMP_Text NicknameText;

    [SerializeField] float moveSpeed;
   
    
    private float horizontal;
    private float vertical;

    public Camera cam;
    Vector2 mousePos;

    private void Start()
    {
       NicknameText.text = photonView.Owner.NickName;
        SetLocalColors();

        if (!photonView.IsMine)
        {
            cameraObject.SetActive(false);
        }
    }

    private void SetLocalColors()
    {
        Color color = GetColorForPlayerById(photonView.OwnerActorNr);
        //spriteRenderer.color = color;
       NicknameText.color = color;

    }
    [PunRPC]
    void FlipXRPC(float axis) => spriteRenderer.flipX = axis == -1;

    private Color GetColorForPlayerById(int id)
        {
            return Color.HSVToRGB((float)id / 10f, 1, 1);
        }

    private void FixedUpdate()
    {
        if (photonView.IsMine)
        {
            var oldVertical = vertical;
            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical");
          

            rigidbody.velocity = new Vector2(horizontal * moveSpeed, vertical*moveSpeed);

            mousePos = cam.ScreenToWorldPoint(Input.mousePosition);


            Vector2 lookDir = mousePos - rigidbody.position;
            //float angle = Mathf.Atan2(lookDir.y. lookDir.x);
        }
       

    }

   

    

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(horizontal);
            stream.SendNext(vertical);
        }
        else
        {
            horizontal = (float)stream.ReceiveNext();
            vertical = (float)stream.ReceiveNext();
  
        }
    }
}
