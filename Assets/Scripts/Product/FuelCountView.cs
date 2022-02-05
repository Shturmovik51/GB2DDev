using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FuelCountView
{
    public TextMeshProUGUI FuelCountText { get; private set; }

    public FuelCountView(TextMeshProUGUI fuelCountText)
    {
        FuelCountText = fuelCountText;
    }

    public void SetFuelCountText(int count)
    {
        FuelCountText.text = count.ToString();
    }
}
