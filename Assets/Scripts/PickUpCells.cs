using UnityEngine;

public class PickUpCells : MonoBehaviour
{

    static public int maxPower = 10;
    static public int currentPower = 0;
    static public bool targeted = false;

    public string tagToPickup = "PickUp";
    public float range = 5f;

    public Camera cam;

    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range))
        {
            if (hit.transform.gameObject.tag == tagToPickup)
            {
                targeted = true;
                if (Input.GetButton("Fire1"))
                {
                    PickUp(hit.transform.gameObject);
                }
            }
            else
            {
                targeted = false;
            }
        }
        else
        {
            targeted = false;
        }
    }

    void PickUp(GameObject gameObject)
    {
        currentPower = currentPower + 1;
        Destroy(gameObject);
    }
}
