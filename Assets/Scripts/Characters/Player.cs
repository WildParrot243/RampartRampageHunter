using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float SensitivityX;
    [SerializeField] float SensitivityY;
    [SerializeField, Range(0, 180), Tooltip("Left Right Rotation")] float YawLimit;
    [SerializeField, Range(0, 90), Tooltip("Up Down Rotation")] float PitchLimit;
    [SerializeField] Transform Head;
    [SerializeField] Weapon weapon;

    // Start is called before the first frame update
    void Start()
    {
        Controls.Init(this);
        Controls.DisableUI();

    }

    public void LookingAround(Vector2 vector2)
    {
        float time = Time.deltaTime;
        
        float bodyrotation = transform.localEulerAngles.y + vector2.x * SensitivityX * time;
        float headroat = Head.localEulerAngles.x- vector2.y * SensitivityY * time;
        if (bodyrotation > YawLimit && bodyrotation < 180)
        {
            bodyrotation = YawLimit;
        }

        else if (bodyrotation < 360 -  YawLimit && bodyrotation > 180)
        {
            bodyrotation = 360 - YawLimit;

        }

       if (headroat > PitchLimit && headroat < 180)
        {
            headroat = PitchLimit;
        }

        else if (headroat < 360 - PitchLimit && headroat > 180)
        {
            headroat = 360 - PitchLimit;

        }

        transform.localEulerAngles = new Vector3(0,bodyrotation);
        Head.localEulerAngles = new Vector3(headroat, 0);

    }

    public void SetFiringState(bool v)
    {
        print("Firing" + v);
        if (v) weapon.TryActivate();
        else weapon.StopActivate();
    }
}
