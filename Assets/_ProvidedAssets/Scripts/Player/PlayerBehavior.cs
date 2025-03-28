using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBehavior : MonoBehaviour
{
    public Image aimDotUI;
    public Camera playerCamera;
    public float maxDistance = 10f;
    public GameObject doorButton;
    public GameObject pickupButton;

    private GameObject currentDoor;
    private GameObject currentPickable;

    void Update()
    {
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, maxDistance))
        {
            aimDotUI.transform.position = Camera.main.WorldToScreenPoint(hit.point);

            if (hit.collider.CompareTag("Door") || hit.collider.CompareTag("Locked"))
            {
                HandleDoorDetection(hit.collider.gameObject);
            }
            else if (hit.collider.CompareTag("Pickables"))
            {
                HandlePickableDetection(hit.collider.gameObject);
            }
            else
            {
                ResetDetection();
            }
        }
        else
        {
            ResetDetection();
        }
    }

    void HandleDoorDetection(GameObject door)
    {
        if (currentDoor != door)
        {
            currentDoor = door;
            Debug.Log("🎯 Looking at door: " + currentDoor.name);
        }
        doorButton.SetActive(true);
        pickupButton.SetActive(false);
    }

    void HandlePickableDetection(GameObject pickable)
    {
        if (currentPickable != pickable)
        {
            currentPickable = pickable;
            Debug.Log("🎯 Looking at pickable: " + currentPickable.name);
        }
        pickupButton.SetActive(true);
        doorButton.SetActive(false);
    }

    void ResetDetection()
    {
        aimDotUI.transform.position = new Vector2(Screen.width / 2, Screen.height / 2);
        currentDoor = null;
        currentPickable = null;
        doorButton.SetActive(false);
        pickupButton.SetActive(false);
    }

    public void OpenDoor()
    {
        if (currentDoor == null) return;

        if (currentDoor.CompareTag("Locked") && !ReferenceManager.Instance.pickableManager.AreAllItemsCollected())
        {
            
            Debug.Log("❌ The door is locked! Collect all items first.");
            return;
        }

        Doorhandler doorScript = currentDoor.GetComponent<Doorhandler>();
        if (doorScript != null)
        {
            doorScript.Open();
        }
    }

    public void PickUpObject()
    {
        if (currentPickable == null) return;

        ReferenceManager.Instance.pickableManager.PickUpItem(currentPickable);
        currentPickable = null;
        pickupButton.SetActive(false);
    }
}
