using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Pickups : MonoBehaviour
{
    public int value = 1;

    public enum PickupType { Food, Key, Treasure, Potion }; //enum of pickup types
    public PickupType myType = PickupType.Treasure;

    public void OnTriggerEnter2D(Collider2D other) //check for collision
    {
        //switch case of each type of pickup, adds values to the players stats in script and updates UI

        if (other.CompareTag("Player"))
        {
            switch (myType)
            {
                case PickupType.Food:
                    other.gameObject.GetComponent<Players>().health += value;
                    break;
                case PickupType.Key:
                    other.gameObject.GetComponent<Players>().keyCount += value;
                    break;
                case PickupType.Treasure:
                    other.gameObject.GetComponent<Players>().AddScore(value);
                    break;
                case PickupType.Potion:
                    other.gameObject.GetComponent<Players>().potionCount += value;
                    break;
                default:
                    Debug.Log("pickup type incorrect");
                    break;
            }
            other.gameObject.GetComponent<Players>().UpdateMyUI();
            Destroy(this.gameObject);
        }
    }
}
