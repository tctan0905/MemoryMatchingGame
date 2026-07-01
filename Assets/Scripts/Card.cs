using UnityEngine;
using UnityEngine.UI;
public class Card : MonoBehaviour
{
    public int cardId; // The value of the card
    public  Image imgCard; // Reference to the card's sprite
    public bool isFlipped = false; // Indicates whether the card is flipped or not
    public bool isMatched = false; // Indicates whether the card has been matched or not

    public void SetCard(int id, Sprite sprite)
    {
        cardId = id;
        imgCard.sprite = sprite;
    }

    public void FlipCard()
    {
        isFlipped = !isFlipped;
        imgCard.gameObject.SetActive(true);
    }

    public void HideCard()
    {
        isFlipped = false;
        imgCard.gameObject.SetActive(false); // Hide the card's image
        //gameObject.SetActive(false);
    }
        
}
