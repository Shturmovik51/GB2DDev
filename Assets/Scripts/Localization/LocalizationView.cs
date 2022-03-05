using System.Collections;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

public class LocalizationView : MonoBehaviour, IView
{
    [SerializeField] private Transform _buttonsParent;
    [SerializeField] private Button _buttonPrefab;
    [SerializeField] private Button _outButton;

    private void Start()
    {
       // StartCoroutine(WaitLocale());
    }

    public void StartCor(UnityAction Out)
    {
        StartCoroutine(WaitLocale());
        _outButton.onClick.AddListener(Out);
    }

    private IEnumerator WaitLocale()
    {
        yield return LocalizationSettings.InitializationOperation;
        var locales = LocalizationSettings.AvailableLocales.Locales;
        foreach (var locale in locales)
        {
            CreateButtonForLocale(locale);
        }
    }

    private void CreateButtonForLocale(Locale locale)
    {
        var go = GameObject.Instantiate(_buttonPrefab, _buttonsParent);
        var text = go.GetComponentInChildren<TextMeshProUGUI>();
        if (text != null)
            text.text = locale.Identifier.Code;
        go.onClick.AddListener(() => LocalizationSettings.SelectedLocale = locale);
    }

    private void ChangeLocale(int index)
    {
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[index];
    }

    public void Show()
    {
        
    }

    public void Hide()
    {
       
    }
}
