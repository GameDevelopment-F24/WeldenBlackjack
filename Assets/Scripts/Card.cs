using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public int cardValue = 0;

    private SpriteRenderer spriteRenderer;

    private Sprite cardBack;
    private Sprite cardFront;

    private Vector2 startPos = new Vector2(-4.6f, 3.5f);
    private bool isMoving;
    private Vector2 targetPos;

    public bool faceUp;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = cardBack;
        faceUp = false;
    }
    public void Update()
    {
        if (isMoving)
        {
            MoveToHand();
        }

        if(atTargetPos()){
            isMoving = false;
        }
    }

    public bool atTargetPos()
    {
        return (Vector2)transform.position == targetPos;
    }
    public void Deal(Vector2 location)
    {
        targetPos = location;
        isMoving = true;
    }

    public void FlipCard()
    {
        StartCoroutine(RotateCard());
    }

    public void MoveToHand()
    {
        Vector2 direction = (targetPos - (Vector2)transform.position);
        if (direction.magnitude < 0.1f)
        {
            transform.position = targetPos;
            return;
        }
        direction = direction.normalized;
        transform.Translate(direction * 20f * Time.deltaTime);
    }

    private IEnumerator RotateCard()
    {
        if (!faceUp){
            for (float i = 0f; i <= 180f; i += 10f)
            {
                transform.localRotation = Quaternion.Euler(0f, i, 0f);
                if (i == 90f)
                {
                    spriteRenderer.sprite = cardFront;
                    transform.localScale = new Vector3(-0.2f, 0.2f, 1);
                }
                yield return new WaitForSeconds(0.01f);
            }
        }else if (faceUp){
            for (float i = 180f; i >= 0f; i -= 10f)
            {
                transform.localRotation = Quaternion.Euler(0f, i, 0f);
                if (i == 90f)
                {
                    spriteRenderer.sprite = cardBack;
                    transform.localScale = new Vector3(0.2f, 0.2f, 1);
                }
                yield return new WaitForSeconds(0.01f);
            }
        }
        faceUp = !faceUp;
    }

    public int GetCardValue()
    {
        return cardValue;
    }
    public void SetCardValue(int value)
    {
        cardValue = value;
    }
    public string GetCardName()
    {
        return spriteRenderer.sprite.name;
    }
    public void ChangeBackSprite(Sprite newSprite)
    {
        cardBack = newSprite;
    }
    public void ChangeFrontSprite(Sprite newSprite)
    {
        cardFront = newSprite;
    }
    public void ReturnToDeck(){
        targetPos = startPos;
        isMoving = true;
    }
    public void ResetCard()
    {
        Sprite CardBack = GameObject.Find("Deck").GetComponent<Deck>().CardBack();
        gameObject.GetComponent<SpriteRenderer>().sprite = CardBack;
    }
}
