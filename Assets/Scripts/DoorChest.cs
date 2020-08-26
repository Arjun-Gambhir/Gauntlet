using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Current script in Doors script
public class DoorChest : MonoBehaviour
{
    public enum type { chest, door};

    public void OnTriggerEnter2D(Collider2D other) //check for collision
    {
        //check to see how many keys the player has, if at least one then open the door

        if (other.gameObject.tag == "Player") 
        {
            Players checkForKey = other.gameObject.GetComponent<Players>();
            if (checkForKey != null & checkForKey.keyCount >= 1) 
            {
                checkForKey.keyCount -= 1; 
                Destroy(gameObject); 

               
            }
        }
    }
}
