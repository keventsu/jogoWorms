using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

[RequireComponent(typeof(LineRenderer))]
public class DragNShoot : MonoBehaviourPun
{
    public float power = 0.01f;
    //public Rigidbody2D rb ;

    PhotonView photonView;

    public TrajectoryLine tl;

    public Vector2 minPower;
    public Vector2 maxPower;

    Camera cam;
    Vector2 force;
    Vector3 startPoint;
    Vector3 endPoint;

    public GameObject spawnbullet;
    public GameObject bullet;

    public int bulletsPerSec = 10;
    public int bullets = 0;

    public float timeLeft = 2.0f;

    public bool pause = false;


    private void Start()
    {
        cam = Camera.main;
        tl = GetComponent<TrajectoryLine>();
        photonView = GetComponent<PhotonView>();
    }

    private void Update()
    {
            if (photonView.IsMine)
        {
            
             mira();
            /*if(bullets == 10){
                pause = true;
                timeLeft -= Time.deltaTime;
                if(timeLeft < 0)
                {
                bullets = 0;
                pause = false;
                timeLeft = 2.0f;
                }
            }*/
        }
            
            
            

            
         
    }
    //
    public void Shoot(Vector2 force)
    {
        photonView.RPC("RpcShoot", RpcTarget.All, force);
    }
    [PunRPC]
    public void RpcShoot(Vector2 force)
    {
        //if(bullets < bulletsPerSec)
            
        //bullets++;
            
          
        GameObject bl = Instantiate(bullet, spawnbullet.transform.position, spawnbullet.transform.rotation);
        bl.GetComponent<Rigidbody2D>().AddForce(force * power, ForceMode2D.Impulse);
            

    }
    //
    void mira(){
        if (Input.GetMouseButtonDown(0))
        {
            
            startPoint = cam.ScreenToWorldPoint(Input.mousePosition);
            startPoint.z = 15;
            
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 currentPoint = cam.ScreenToWorldPoint(Input.mousePosition);
            currentPoint.z = 15;
            tl.RenderLine(startPoint, currentPoint);
        }

        if (Input.GetMouseButtonUp(0))
        {
            
            endPoint = cam.ScreenToWorldPoint(Input.mousePosition);
            endPoint.z = 15;
            
            
            force = new Vector2(Mathf.Clamp(startPoint.x - endPoint.x, minPower.x, maxPower.x), Mathf.Clamp(startPoint.y - endPoint.y, minPower.y, maxPower.y));
            
            Shoot(force);
            
            
            
            
            /*
            GameObject balas = PhotonNetwork.Instantiate(bullet.name, spawnbullet.transform.position, spawnbullet.transform.rotation, 0);
            balas.GetComponent<Rigidbody2D>().AddForce(force * power, ForceMode2D.Impulse);
            }
            */

            tl.EndLine();
        }
    
        
            
        
            
 
    }

}
