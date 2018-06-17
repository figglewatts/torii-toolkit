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
    public Slider Slider1;
    public Slider Slider2;
    public Text Slider2Text;

    public float Slider1Value
    {
        get { return Slider1.value; }
        set
        {
            Slider1.value = value;
            NotifyPropertyChange(nameof(Slider1Value));
        }
    }

    public float Slider2Value
    {
        get { return Slider2.value; }
        set
        {
            Slider2.value = value;
            NotifyPropertyChange(nameof(Slider2Value));
        }
    }

    public float Slider2TextText
    {
        get { return Slider2.value; }
        set { Slider2Text.text = $"{Slider2.value}"; }
    }

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
        set { HealthBarText.text = $"{value}%"; }
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

        Slider1.onValueChanged.AddListener((f) => {NotifyPropertyChange(nameof(Slider1Value));});
        Slider2.onValueChanged.AddListener((f) => {NotifyPropertyChange(nameof(Slider2Value));});

        bindBroker = new BindBroker();
        bindBroker.RegisterData(this);

        bindBroker.Bind(() => PlayerHealth, () => HealthBarFillAmount, BindingType.OneWay);
        bindBroker.Bind(() => PlayerHealth, () => HealthBarTextText, BindingType.OneWay);

        bindBroker.Bind(() => Slider1Value, () => Slider2Value, BindingType.TwoWay);
        bindBroker.Bind(() => Slider2Value, () => Slider2TextText, BindingType.OneWay);
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
