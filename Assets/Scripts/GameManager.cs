using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{

    //Manager to manage the Cards Drawing of cards and shuffeling of the cards

    [Tooltip("Number of cards in the deck")]
    [SerializeField] List<Card> deck = new List<Card>();


    public List<Card> usedDeck = new List<Card>();
	[SerializeField] Transform[] cardSlot;
	public bool[] availableCardSlots;

	[SerializeField] TextMeshProUGUI deckSizeText;
	[SerializeField] TextMeshProUGUI discardPileText;
	[SerializeField] TextMeshProUGUI displayMessage;

	readonly string shuffleErrorMessage = "Cannot Shuffel! Used Card deck is Empty";
	readonly string emptyDeckMessage = "The deck is Empty";

	private void Update()
    {
		deckSizeText.text = deck.Count.ToString();
		discardPileText.text = usedDeck.Count.ToString();
    }


	//Inorder to remove first Card this function is called
	public void DrawTopCard()
    {
		DrawCard(0);
    }

	//Inorder to remove Random Card this function is called
	public void DrawRandomCard()
    {
		DrawCard(Random.Range(0, deck.Count));

    }

	//Called on draw button click this is used to activate the available cards inside the deck
	private void DrawCard(int SlotNumber)
    {
		if (deck.Count >= 1)
		{
			Card randomCard = deck[SlotNumber];

			//check for the availabel cardslot to be placed in the deck
			for (int i = 0; i < availableCardSlots.Length; i++)
			{
				if (availableCardSlots[i] == true)
				{

					randomCard.gameObject.SetActive(true);

					randomCard.cardSlotIndex = i;

					randomCard.transform.position = cardSlot[i].position;

					randomCard.hasBeenPlayed = false;
					availableCardSlots[i] = false;
					deck.Remove(randomCard);
					return;
				}
               
			}
		}
        else
        {

			StartCoroutine(DisplayMessage(emptyDeckMessage));
		}
	}

	//Function Shuffles the cars and add them from discardpile to the card deck
	public void Shuffle()
    {
		if(usedDeck.Count >= 1)
        {
			foreach(Card card in usedDeck)
            {
				deck.Add(card);
            }
			usedDeck.Clear();
        }
        else
        {
			StartCoroutine(DisplayMessage(shuffleErrorMessage));
        }
    }

	IEnumerator DisplayMessage(string message)
    {
		displayMessage.text = message;
		displayMessage.enabled = true;

		yield return new WaitForSeconds(2f);

		displayMessage.enabled = false;
		StopAllCoroutines();
	}

}
