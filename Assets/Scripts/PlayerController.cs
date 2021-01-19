using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerController : MonoBehaviourPun, IPunObservable
{
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] new Rigidbody2D rigidbody;
    [SerializeField] TrailRenderer trailRenderer;
    

    [SerializeField] UnityEngine.UI.Text nicknameText;
    [SerializeField] float moveSpeed;
    [SerializeField] Vector2 groundedCheckSize;
    
    private float horizontal;
    private float vertical;
    

    private void Start()
    {
        nicknameText.text = photonView.Owner.NickName;
        SetLocalColors();
        
    }

    private void SetLocalColors()
    {
        Color color = GetColorForPlayerById(photonView.OwnerActorNr);
        nicknameText.color = color;

        trailRenderer.startColor = color;
        color.a = 0;
        trailRenderer.endColor = color;
    }

    /*private Color GetColorForPlayerById(int id)
    {
        return Color.HSVToRGB((float)id / 10f, 1, 1);
    }*/

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

   

    [PunRPC]
    private bool IsGrounded()
    {
        var hits = Physics2D.BoxCastAll(transform.position, groundedCheckSize, 0, Vector2.zero, 0);

        foreach (var hit in hits)
        {
            if (hit.collider.gameObject != gameObject)
            {
                return true;
            }
        }
        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, groundedCheckSize);
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
