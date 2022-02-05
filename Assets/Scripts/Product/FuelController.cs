using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FuelController : BaseController
{
    public int FuelCount { get; private set; }
    private CarView _carView;

    public FuelController(int fuelCount, CarView carView)
    {
        FuelCount = fuelCount;
        _carView = carView;
    }

    public void DecreaseFuel()
    {
        FuelCount--;
        UpdateView();
    }

    public void UpdateView()
    {
        _carView.SetFuelText(FuelCount);
    }
}
