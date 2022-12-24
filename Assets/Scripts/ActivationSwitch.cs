using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivationSwitch : MonoBehaviour
{
    [SerializeField] private GameObject obj;
    [SerializeField] private Animator leverAnimator;
    [SerializeField] private bool isOn = false;

    private void Start()
    {
        obj.SetActive(isOn);
    }

    public void Toggle()
    {
        isOn = !isOn;
        obj.SetActive(isOn);
        leverAnimator.SetBool("Activated", isOn);
    }
}
