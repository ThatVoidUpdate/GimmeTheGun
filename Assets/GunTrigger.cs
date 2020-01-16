using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunTrigger : MonoBehaviour
{
    private List<Collider2D> closeGuns;
    // Start is called before the first frame update
    void Start()
    {
        closeGuns = GetComponentInParent<Player>().closeGuns;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Whenever an object enters the trigger around the player, and it has the tag Gun and isnt in the colliders list, add it to the colliders list
        if (!closeGuns.Contains(other) && other.gameObject.CompareTag("Gun"))
        {
            closeGuns.Add(other);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        //If a gun move out of the trigger, stop it from shooting and being held, and remove it from the colliders list
        if (other.gameObject.CompareTag("Gun"))
        {
            //closeGuns.Find(x => x == other).GetComponent<Gun>().held = false;
            closeGuns.Find(x => x == other).GetComponent<Gun>().Shooting = false;

            closeGuns.Remove(other);
        }
    }
}
