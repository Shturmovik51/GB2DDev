using UnityEngine;
using TMPro;

public class CarView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _fuelText;
    [SerializeField] private Transform LeftWhell;
    [SerializeField] private Transform RightWhell;

    public TextMeshProUGUI FuelText => _fuelText;

    public void Init(TextMeshProUGUI fuelText)
    {
        _fuelText = fuelText;
    }

    public void SetFuelText(int value)
    {
        _fuelText.text = value.ToString();
    }

    public void RotateWheels(float value)
    {
        LeftWhell.Rotate(new Vector3(LeftWhell.rotation.x, LeftWhell.rotation.y, value*20000));
        RightWhell.Rotate(new Vector3(RightWhell.rotation.x, RightWhell.rotation.y, value*20000));

        Debug.Log(value);
    }
} 

