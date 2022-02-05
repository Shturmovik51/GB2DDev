using UnityEngine;
using TMPro;

public class CarView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _fuelText;

    public TextMeshProUGUI FuelText => _fuelText;

    public void Init(TextMeshProUGUI fuelText)
    {
        _fuelText = fuelText;
    }

    public void SetFuelText(int value)
    {
        _fuelText.text = value.ToString();
    }
} 

