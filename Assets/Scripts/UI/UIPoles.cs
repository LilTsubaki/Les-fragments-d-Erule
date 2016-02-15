using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIPoles : MonoBehaviour {

    public Text _valueSublimation;
    public Text _valuePerfection;
    public Text _valueStabilite;
    public Text _valueReussite;


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

        float totalStability = (baseChance - (malus / coef)) * 0.01f + stab;

        _valueSublimation.text = Mathf.Ceil(subli*100f).ToString();
        _valuePerfection.text = Mathf.Ceil(perf*100f).ToString();
        _valueStabilite.text = Mathf.Ceil(stab*100f).ToString();
        _valueReussite.text = Mathf.Clamp(Mathf.Ceil(totalStability * 100), 0, 100).ToString();

	}
}
