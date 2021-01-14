using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundCheck : MonoBehaviour
{
    PlayerController PlayerController;


    void Awake()
    {
        PlayerController = GetComponentInParent<PlayerController>();

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == PlayerController.gameObject)
            return;

        PlayerController.SetGroundedState(true);
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == PlayerController.gameObject)
            return;

        PlayerController.SetGroundedState(false);
    }

    private void OnTriggerStay(Collider other)
    {
        PlayerController.SetGroundedState(true);
    }
}

