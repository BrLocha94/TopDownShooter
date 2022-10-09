using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CouterText : MonoBehaviour, IReceiver<int>
{
    [SerializeField]
    private Text targetText;

    public void ReceiveUpdate(int updatedValue)
    {
        targetText.text = "= " + updatedValue.ToString();
    }
}
