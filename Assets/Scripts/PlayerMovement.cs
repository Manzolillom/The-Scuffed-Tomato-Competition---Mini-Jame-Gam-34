using System.Collections.Generic;
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
    Transform item;

    [SerializeField] Animator inventoryAnimator;
    Vector3 lastPlayerPosition;
    [SerializeField] float timeBetweenBounceChecks;
    float timeTillNextBounceCheck;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        lastPlayerPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();

        //Checking if holding an item and if so move it with you
        if (holdingAnItem)
        {
            item.position = inventorySpace.position;
        }
        else if (item != null)
        {
            item.position = placingItemPosition.position;
            item = null;
        }

        //Checks for bounces
        timeTillNextBounceCheck -= Time.deltaTime;
        if (timeTillNextBounceCheck < 0)
        {
            if(Vector3.Distance(lastPlayerPosition, transform.position) > 3f)
            {
                inventoryAnimator.SetTrigger("bounce");
            }
            lastPlayerPosition = transform.position;
            timeTillNextBounceCheck = timeBetweenBounceChecks;
        }

        //Limit playerspeed
        if (rb.velocity.magnitude > maxVelocity)
        {
            rb.AddForce(-rb.velocity);
        }

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
            item = closestItem;
        }
    }

    void Movement() //Realy basic player movement
    {
        if(Input.GetKey(KeyCode.W)) 
        {
            rb.AddForce(Vector3.forward * walkSpeed * Time.deltaTime);
            placingItemPosition.localPosition = new Vector3(0, 0, placingDistance); //changes item placing zone to where you're facing
        }
        if (Input.GetKey(KeyCode.S))
        {
            rb.AddForce(-Vector3.forward * walkSpeed * Time.deltaTime);
            placingItemPosition.localPosition = new Vector3(0, 0, -placingDistance); //changes item placing zone to where you're facing
        }
        if (Input.GetKey(KeyCode.A))
        {
            rb.AddForce(Vector3.left * walkSpeed * Time.deltaTime);
            placingItemPosition.localPosition = new Vector3(-placingDistance, 0, 0); //changes item placing zone to where you're facing
        }
        if (Input.GetKey(KeyCode.D))
        {
            rb.AddForce(Vector3.right * walkSpeed * Time.deltaTime);
            placingItemPosition.localPosition = new Vector3(placingDistance, 0, 0); //changes item placing zone to where you're facing
        }
    }
}
