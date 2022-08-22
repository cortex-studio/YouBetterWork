using System.Collections;
using System.Collections.Generic;
using MoreMountains.NiceVibrations;
using UnityEngine;

public class HitVibration : MonoBehaviour
{
    public void Hit()
    {
        MMVibrationManager.Haptic(HapticTypes.HeavyImpact);
    }
}
