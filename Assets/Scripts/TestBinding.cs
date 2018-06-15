using System;
using System.Collections;
using System.Collections.Generic;
using Torii.Binding;
using UnityEngine;
using UnityEngine.UI;

public class TestBinding : MonoBehaviour, IPropertyWatcher
{
    public BindBroker bindBroker;

    public Image HealthBar;
    public Text HealthBarText;

    public float HealthBarFillAmount
    {
        get { return _playerHealth / _playerMaxHealth; }
        set
        {
            HealthBar.fillAmount = value / _playerMaxHealth;
        }
    }

    public float HealthBarTextText
    {
        get { return _playerHealth; }
        set { HealthBarText.text = value.ToString() + "%"; }
    }

    private float _playerMaxHealth = 100;
    private float _playerHealth = 100;

    public float PlayerHealth
    {
        get { return _playerHealth; }
        set
        {
            _playerHealth = value;
            NotifyPropertyChange(nameof(PlayerHealth));
        }
    }
    

    public Guid GUID { get; private set; }

    public event Action<string, IPropertyWatcher> OnPropertyChange;

    public void NotifyPropertyChange(string propertyName)
    {
        OnPropertyChange?.Invoke(propertyName, this);
    }

    // Use this for initialization
    void Start ()
    {
        GUID = Guid.NewGuid();

        bindBroker = new BindBroker();
        bindBroker.RegisterData(this);

        bindBroker.Bind(() => PlayerHealth, () => HealthBarFillAmount, BindingType.OneWay);
        bindBroker.Bind(() => PlayerHealth, () => HealthBarTextText, BindingType.OneWay);
    }

    [ContextMenu("Damage")]
    public void TakeDamage()
    {
        PlayerHealth -= 10;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
