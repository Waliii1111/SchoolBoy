using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickableManager : MonoBehaviour
{
    public Image[] pickableUIImages; // UI images for collected items
    public GameObject[] pickableItems; // Pickable items in the scene
    public Vector3[] hintPositions; // Manually set hint positions for each item
    public Image hintArrow; // UI arrow (on Canvas)
    public RectTransform arrowRect; // RectTransform of the arrow
    public float hintDuration = 5f; // How long the hint stays active
    public float closeDistance = 3f; // Distance at which the hint disappears

    private int currentHintIndex = 0;
    private bool isHintActive = false;
    private Camera mainCamera;
    private Transform player;

    private void Start()
    {
        foreach (Image img in pickableUIImages)
        {
            img.gameObject.SetActive(false);
        }

        mainCamera = Camera.main;
        player = GameObject.FindGameObjectWithTag("Player").transform;

        if (hintArrow != null)
        {
            hintArrow.gameObject.SetActive(false);
        }
    }

    public void PickUpItem(GameObject pickable)
    {
        for (int i = 0; i < pickableItems.Length; i++)
        {
            if (pickableItems[i] == pickable)
            {
                if (i < pickableUIImages.Length)
                {
                    pickableUIImages[i].gameObject.SetActive(true);
                }

                Destroy(pickable);
                return;
            }
        }
    }
    
    public bool AreAllItemsCollected()
    {
        foreach (Image img in pickableUIImages)
        {
            if (!img.gameObject.activeSelf)
            {
                return false;
            }
        }
        return true;
    }

    public void ShowHint()
    {
        if (currentHintIndex >= hintPositions.Length) return;

        hintArrow.gameObject.SetActive(true);
        isHintActive = true;
        StartCoroutine(UpdateArrow(hintPositions[currentHintIndex]));

        currentHintIndex++;
    }

    private IEnumerator UpdateArrow(Vector3 hintPosition)
    {
        float timer = hintDuration;

        while (timer > 0 && isHintActive)
        {
            if (Vector3.Distance(player.position, hintPosition) < closeDistance)
            {
                hintArrow.gameObject.SetActive(false);
                isHintActive = false;
                yield break;
            }

            // Convert world position to screen position
            Vector3 screenHintPos = mainCamera.WorldToScreenPoint(hintPosition);

            // Check if hint is behind the player (off-screen)
            if (screenHintPos.z < 0)
            {
                screenHintPos.x = Screen.width - screenHintPos.x;
                screenHintPos.y = Screen.height - screenHintPos.y;
            }

            // Arrow should always point from the screen center to the hint position
            Vector3 screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, 0);
            Vector3 direction = screenHintPos - screenCenter;

            // Convert direction to rotation angle
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
            arrowRect.rotation = Quaternion.Euler(0, 0, angle);

            timer -= Time.deltaTime;
            yield return null;
        }

        hintArrow.gameObject.SetActive(false);
        isHintActive = false;
    }
}
