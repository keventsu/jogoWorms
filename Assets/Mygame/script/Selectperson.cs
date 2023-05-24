using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Photon.Pun;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class Selectperson : MonoBehaviour, IPointerClickHandler
{
    public int id = 0;
    public bool isChosen;
    public string playerOwner = "";
    public int playerOwnerID = 0;
    // Start is called before the first frame update
    void Start()
    {
        isChosen = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetID(int value){
        id = value;
    }
    public int GetID(){
        return id;
    }


    public void OnPointerClick(PointerEventData ped){

        if(!isChosen){
            return;
        }
        Hashtable props = new Hashtable {
            {"PERSONAGEM", id }
        };
        PhotonNetwork.LocalPlayer.SetCustomProperties(props);
        
        

    }

    public void SelectCharacter(string p_playerName, int p_playerId){
        Transform allButtons = this.transform.parent;
        foreach(Transform item in allButtons){
            if(item.GetComponent<Selectperson>().playerOwnerID == p_playerId){
                item.GetComponent<Selectperson>().ResetCharacter();
            }

        }
        isChosen = false;
        playerOwner = p_playerName;
        playerOwnerID = p_playerId;
        GetComponentInChildren<Text>().text = p_playerName;
        GetComponentInChildren<Text>().color = ColorPlayer(p_playerId);
        GetComponent<Button>().interactable = false;
    }
    public void ResetCharacter(){
        isChosen = true;
        playerOwner = "";
        playerOwnerID = 0;
        GetComponentInChildren<Text>().text = "";
        GetComponentInChildren<Text>().color = ColorPlayer(0);
        GetComponent<Button>().interactable = true;
    }

    Color ColorPlayer(int playerID){
        
        Color colorReturn;
        switch (playerID)
        {
            case 1:
                colorReturn = new Color(255, 0, 0);

                break;
            case 2:
                colorReturn = new Color(0, 255, 0);

                break;
            case 3:
                colorReturn = new Color(0, 0, 255);

                break;
            case 4:
                colorReturn = new Color(255, 255, 0);

                break;
            case 5:
                colorReturn = new Color(255, 0, 255);

                break;
            case 6:
                colorReturn = new Color(0, 255, 255);

                break;
            case 7:
                colorReturn = new Color(255, 0, 130);

                break;
            case 8:
                colorReturn = new Color(255, 130, 0);

                break;
            default:
                colorReturn = new Color(255, 255, 255);

                break;

        }

    return colorReturn;




    }
}
