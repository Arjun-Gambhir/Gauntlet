using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour
{
    //checks for collision with the player
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //if the player has a key, open the door
            if (collision.gameObject.GetComponent<Players>().keyCount > 0)
                                                                         
            {
                collision.gameObject.GetComponent<Players>().keyCount--;
                collision.gameObject.GetComponent<Players>().UpdateMyUI();
                Destroy(this.gameObject);
            }
        }
    }

}
