using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuBuilder : MonoBehaviour
{
    public delegate void OnMenuBuilderAction(bool status);
    public static event OnMenuBuilderAction OnMenuBuilder;

    public GameObject gbPanel, optionsPanel, exitPanel;
       
    private bool status = true;

    public void ChangeStatus() {
        BNG.InputBridge.Instance.VibrateController(0.1f, 0.3f, 0.1f, BNG.ControllerHand.Left);
        status = !status;
        if (status) {
            EnableBuilderMode();
        }
        else
        {
            DisableBuilderMode();
            GM_JsonData.SaveToJSON(GM_GameDataManager.gymBuilderObjects);
        }
    }

    public void EnableBuilderMode() {
        gbPanel.SetActive(true);
        optionsPanel.SetActive(true);
        exitPanel.SetActive(false);

        OnMenuBuilder?.Invoke(true);

    } 
    public void DisableBuilderMode() {
        gbPanel.SetActive(false);
        optionsPanel.SetActive(false);
        exitPanel.SetActive(true);

        OnMenuBuilder?.Invoke(false);
    }
}
