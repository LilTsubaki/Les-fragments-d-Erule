using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIPoles : MonoBehaviour {

    public Material _valueSublimation;
    public Material _valuePerfection;
    public Material _valueStabilite;


	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {

        float baseChance = RunicBoardManager.GetInstance().GetBaseStabilityByRuneNumber();
        float coef = RunicBoardManager.GetInstance().GetReductionCoefficientByRuneNumber();
        float malus = RunicBoardManager.GetInstance().GetTotalCompatibilityMalus();
        float perf = 0, subli = 0, stab = 0;

        RunicBoardManager.GetInstance().GetPolesInfluence(out perf, out subli, out stab);

        float inv = (1f / 44f) * 100f;

        _valueSublimation.SetFloat("_Cutoff", 1 - subli*inv);
        _valuePerfection.SetFloat("_Cutoff",  1 - perf*inv);
        _valueStabilite.SetFloat("_Cutoff", 1 - stab*inv);

	}
}
