using System;
using UnityEngine;

namespace Tools
{
    public class PrefsSubscriptionProperty<T> : SubscriptionProperty<T>
    {
        private readonly string _key;
        private readonly Func<string, T> _converter;

        public PrefsSubscriptionProperty(string key, Func<string, T> converter) : base()
        {
            _key = key;
            _converter = converter;
            LoadData(_key);
            SubscribeOnChange(SaveData);
        }

        private void LoadData(string key)
        {
            if (PlayerPrefs.HasKey(key))
            {
                var str = PlayerPrefs.GetString(key);
                Value = _converter(str);
            }
            else
            {
                Value = default;
            }
        }
        private void SaveData(T obj)
        {
            PlayerPrefs.SetString(_key, obj.ToString());
        }
    }
}
