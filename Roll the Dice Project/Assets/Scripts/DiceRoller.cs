using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceRoller : MonoBehaviour
{
    [SerializeField] Image diceContainer;
    [SerializeField] Sprite[] diceSprites;

    public int RollDice()
    {
        int roll = Random.Range(1, 7);

        diceContainer.sprite = diceSprites[roll - 1];

        return roll;
    }
}
