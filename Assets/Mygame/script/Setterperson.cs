using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class Setterperson : MonoBehaviourPunCallbacks
{
    public int characterID;



    private void Start(){
        SetCharecterID();
    }
   
   public void SetCharecterID(){
       int i = 1;

       foreach(Transform item in transform){
           item.GetComponent<Selectperson>().SetID(i);
           i++;

        }  
   }
                  
   
    


    public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps){
        
        object characterID_Obj;
        if(changedProps.TryGetValue("PERSONAGEM", out characterID_Obj)){
            characterID = (int) characterID_Obj;
             

            if(characterID > 0){

                foreach(Transform item in transform){
                    
                    
                    if( item.GetComponent<Selectperson>().id == characterID){
                       
                        item.GetComponent<Selectperson>().SelectCharacter(targetPlayer.NickName, targetPlayer.ActorNumber);
                        
                        
                    }
                }
            }

        }
    }
}
