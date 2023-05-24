using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class Roomlistitem : MonoBehaviourPunCallbacks
{
    public Text roomName;
    public Text roomPlayers;
    public int pi_playermax = 8;
    public string roomInfo = "/";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Iniciar(string ps_roomName, int pi_roonPlayers, int pi_playermax){
        roomName.text = ps_roomName;
        roomPlayers.text = pi_roonPlayers + roomInfo + pi_playermax;
    }
    public void BotaoJoin(){
        if(PhotonNetwork.InLobby){
            PhotonNetwork.JoinRoom(roomName.text);
        }
    }

}
