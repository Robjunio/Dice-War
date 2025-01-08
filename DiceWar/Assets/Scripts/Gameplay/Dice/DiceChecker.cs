using System.Collections.Generic;
using UnityEngine;

public class DiceChecker : MonoBehaviour
{
    [SerializeField] LayerMask hitLayers;
    private List<GameObject> diceFaces = new List<GameObject>();

    private void OnTriggerEnter(Collider other)
    {
        var diceFace = other.gameObject;

        diceFaces.Add(diceFace);
    }


    private void OnTriggerExit(Collider other)
    {
        var diceFace = other.gameObject;

        diceFaces.Remove(diceFace);
    }
    public List<int> GetDicesValues()
    {
        List<int> dicesValues = new List<int>();

        foreach (var diceFace in diceFaces)
        {
            switch(diceFace.name) {
                case "Side 1":
                    dicesValues.Add(1);
                    break;
                case "Side 2":
                    dicesValues.Add(2);
                    break;
                case "Side 3":
                    dicesValues.Add(3);
                    break;
                case "Side 4":
                    dicesValues.Add(4);
                    break;
                case "Side 5":
                    dicesValues.Add(5);
                    break;
                case "Side 6":
                    dicesValues.Add(6);
                    break;
            }
        }
        dicesValues.Sort();
        dicesValues.Reverse();

        diceFaces.Clear();

        return dicesValues;
    }
}
