using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
	public bool hasBeenPlayed;
	public int cardSlotIndex;

    private int selectSpace = 3;
	GameManager gameManager;



    private void Start()
    {
		gameManager = FindObjectOfType<GameManager>();
    }

    private void OnMouseDown()
    {
        if (hasBeenPlayed == false)
        {
			transform.position += Vector3.down * selectSpace;
            hasBeenPlayed = true;
            gameManager.availableCardSlots[cardSlotIndex] = true;
            Invoke("MoveToUsedDeck", 1f);
        }
    }

	void MoveToUsedDeck()
    {
		gameManager.usedDeck.Add(this);
		gameObject.SetActive(false);
    }

}
