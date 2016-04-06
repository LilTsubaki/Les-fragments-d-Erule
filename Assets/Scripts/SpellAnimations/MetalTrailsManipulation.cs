using UnityEngine;
using System.Collections;
using Xft;

public class MetalTrailsManipulation : MonoBehaviour {

    public XWeaponTrail _cut;
    public XWeaponTrail _distort;

	public void ActivateTrails()
    {
        _cut.Activate();
        _distort.Activate();
    }

    public void DeactivateTrails()
    {
        _cut.Deactivate();
        _distort.Deactivate();
    }
}
