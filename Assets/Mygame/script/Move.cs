using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class Move : MonoBehaviour
{
    
    private string AR = "ar";
    private string CHAO = "chao";
    public string estado;
    public string tempo;
    private string OK = "ok";
    private  float timeLeft = 1.5f;

    private float vel;
    private Vector2 direcao;
    private float jumpForce;
    PhotonView photonView;
    //public Camera camerasmain;
    //public Camera camerasplayer;
    public float vidamax = 100f;
    public float vidaatual;
    public Image barravida;



    
    // Start is called before the first frame update
    void Start()
    {
        photonView = GetComponent<PhotonView>();
        vel = 3;
        direcao = Vector2.zero;
        jumpForce = 320;
        vidaatual = vidamax;
       // camerasmain.gameObject.SetActive (false);
        //camerasplayer.gameObject.SetActive (true);
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
        {
           
            inputPersonagem();
        transform.Translate(direcao * vel * Time.deltaTime);
        timeLeft -= Time.deltaTime;
         if(timeLeft < 0)
         {
             tempo = OK;
            
         }
        }
    }

    public void TakeDamage(float value){
        photonView.RPC("TakeDamageNet", RpcTarget.All, value);

    }
     [PunRPC]
    public void TakeDamageNet(float value){
        vidaconta(value);
    }
   
    void vidaconta(float value){
        vidaatual += value;
        barravida.fillAmount = vidaatual/100f;
    }
 

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            estado = CHAO;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (estado == CHAO)
        {
            if (collision.gameObject.tag == "Ground")
               { estado = AR;}
        }
 
    }
    void inputPersonagem()
    {
        direcao = Vector2.zero;

        if(Input.GetKey(KeyCode.Space) && estado == CHAO && tempo == OK)
        {
            //direcao += Vector2.up;
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpForce));
            timeLeft = 1.5f;
            tempo = null;

        }
        if(Input.GetKey(KeyCode.A))
        {
            
             direcao += Vector2.left;
        }
        if(Input.GetKey(KeyCode.D))
        {
          direcao += Vector2.right;
            
        }


    }
}
