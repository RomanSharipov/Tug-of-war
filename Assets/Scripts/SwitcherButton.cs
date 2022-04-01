using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitcherButton : MonoBehaviour
{
    [SerializeField] private Button _button;

    public Button Button => _button;

    public void SwitchOnButton()
    {
        _button.gameObject.SetActive(true);
    }

    public void SwitchOffButton()
    {
        _button.gameObject.SetActive(false);
    }
}
