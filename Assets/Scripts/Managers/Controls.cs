using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Controls 
{
    static ActionMap controls;

    public static void Init(Player player)
    {
        controls = new ActionMap();
        controls.UI.ToggleUI.performed += _ => DisableUI();

        controls.InGame.ToggleUI.performed += _ => EnableUI();
        controls.InGame.Fire.performed += ctx => player.SetFiringState(ctx.ReadValueAsButton());
        controls.InGame.Look.performed += ctx => player.LookingAround(ctx.ReadValue<Vector2>());

    }

    public static void EnableUI()
    {
        controls.UI.Enable();
        controls.InGame.Disable();
    }

    public static void DisableUI()
    {
        controls.UI.Disable();
        controls.InGame.Enable();
    }

}

