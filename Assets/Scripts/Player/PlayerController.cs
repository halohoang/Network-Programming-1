using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerController : MonoBehaviourPun, IPunObservable
{
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] new Rigidbody2D rigidbody;
    [SerializeField] GameObject cameraObject;


    //[SerializeField] UnityEngine.UI.Text nicknameText;
    [SerializeField] float moveSpeed;
   
    
    private float horizontal;
    private float vertical;
    

    private void Start()
    {
       // nicknameText.text = photonView.Owner.NickName;
       // SetLocalColors();

        if (!photonView.IsMine)
        {
            cameraObject.SetActive(false);
        }
    }

    private void SetLocalColors()
    {
        Color color = GetColorForPlayerById(photonView.OwnerActorNr);
        spriteRenderer.color = color;
        //nicknameText.color = color;

    }

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
          

            rigidbody.velocity = new Vector2(horizontal * moveSpeed, rigidbody.velocity.y);

        }

      
    }

   

    /*private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position + (Vector3)groundedCheckOffset, groundedCheckSize);
    }*/

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
