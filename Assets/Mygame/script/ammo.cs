using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class ammo : MonoBehaviourPun
{
    public float dano = 10f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*[PunRPC]
    public void Destruir(){
        Destroy(this.gameObject);
    }*/

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Ground"))
        {
            // ootr
                //this.GetComponent<PhotonView>().RPC("Destruir",RpcTarget.All);
                Destroy(this.gameObject);
        }
         if(collision.CompareTag("Player") && collision.GetComponent<Move>() && collision.GetComponent<PhotonView>().IsMine)
        {
                collision.GetComponent<Move>().TakeDamage(-dano);
                //Debug.Log("player");
                //this.GetComponent<PhotonView>().RPC("Destruir",RpcTarget.All);
                Destroy(this.gameObject);
        }
    }
}