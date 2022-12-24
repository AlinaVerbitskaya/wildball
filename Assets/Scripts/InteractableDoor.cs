using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableDoor : MonoBehaviour
{
    [SerializeField] private Canvas hintCanvas;
    [SerializeField] private Animator anim;
    private bool opened = false;

    void Start()
    {
        hintCanvas.gameObject.SetActive(true);
        ShowHint(false);
    }

    public IEnumerator ActivateOnButtonPress()
    {
        if (!opened)
        {
            ShowHint(true);
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.E));
            anim.SetBool("Activated", true);
            ShowHint(false);
            opened = true;
        }
    }

    public void ShowHint(bool show)
    {
        hintCanvas.enabled = show;
    }
}
