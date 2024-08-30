using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float walkSpeed = 4; //1000 default in editor
    [SerializeField] float maxVelocity;
    [Space]
    List<Transform> itemsInRange = new List<Transform>();
    [SerializeField] Transform inventorySpace;
    [SerializeField] Transform placingItemPosition;
    [SerializeField] float placingDistance;
    bool holdingAnItem = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!holdingAnItem)
            {
                TryToPickUpItem();
            }
            else
            {
                holdingAnItem = false;
                ///Drop Item function here
            }
        }
    }

    private void OnTriggerEnter(Collider other) //if an item is in range goes on the list
    {
        if (itemsInRange.Contains(other.transform)) return; //if it already has the item in the list then return
        if(other.CompareTag("PickUpItem")) itemsInRange.Add(other.gameObject.transform); //adds the item to the list
    }

    private void OnTriggerExit(Collider other) //if an item leaves pickup range it gets removed from the list
    {
        if (!itemsInRange.Contains(other.transform)) return; //if it already doesn't have the item in the list then return
        itemsInRange.Remove(other.gameObject.transform); //removes the item from the list
    }

    void TryToPickUpItem()
    {
        Transform closestItem = null;
        foreach (Transform item in itemsInRange)
        {
            if (closestItem == null || Vector3.Distance(item.position, transform.position) < Vector3.Distance(closestItem.position, transform.position)) closestItem = item;
        }
        if(closestItem != null)
        {
            holdingAnItem = true;
            StartCoroutine(HoldItem(closestItem));
        }
    }

    IEnumerator HoldItem(Transform item)
    {
        while (holdingAnItem)
        {
            item.position = inventorySpace.position;
            yield return new WaitForEndOfFrame();
        }
        item.position = placingItemPosition.position;
        StopCoroutine(HoldItem(item));
    }

    void Movement() //Realy basic player movement
    {
        if(Input.GetKey(KeyCode.W)) 
        {
            rb.AddForce(Vector3.forward * walkSpeed * Time.deltaTime);
            placingItemPosition.localPosition = new Vector3(0, 0, placingDistance);
        }
        if (Input.GetKey(KeyCode.S))
        {
            rb.AddForce(-Vector3.forward * walkSpeed * Time.deltaTime);
            placingItemPosition.localPosition = new Vector3(0, 0, -placingDistance);
        }
        if (Input.GetKey(KeyCode.A))
        {
            rb.AddForce(Vector3.left * walkSpeed * Time.deltaTime);
            placingItemPosition.localPosition = new Vector3(-placingDistance, 0, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            rb.AddForce(Vector3.right * walkSpeed * Time.deltaTime);
            placingItemPosition.localPosition = new Vector3(placingDistance, 0, 0);
        }
    }
}
