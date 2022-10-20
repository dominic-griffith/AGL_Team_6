using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchTrigger : MonoBehaviour
{
    private Animator anim;
    [HideInInspector] public bool isOn = false;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Fire")
        {
            TurnOnTorch();
        }
    }

    private void TurnOnTorch()
    {
        anim.SetBool("Play", true);
        //play torch sound
        isOn = true;

    }
}
