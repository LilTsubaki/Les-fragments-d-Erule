using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class UISpellDetails : MonoBehaviour
{
    public Text _textRange;
    public Text _textPiercing;
    public Text _textEnemyTargetable;
    public Text _textOrientation;

    public void Update()
    {
        UpdateRange();
    }

    public void UpdateRange()
    {
        SendBoardResponse sbr = RunicBoardManager.GetInstance()._runicBoardBehaviour.LatestSendBoardResponse;
        if(sbr != null && sbr._range != null)
        {
            Range range = sbr._range;
            _textRange.text = "Portée actuelle : " + range.MinRange + " - " + range.MaxRange;

            if (range.Piercing)
                _textPiercing.text = "Ce sort traverse les obstacles";
            else
                _textPiercing.text = "Ce sort ne traverse pas les obstacles";

            if (range.EnemyTargetable)
                _textEnemyTargetable.text = "Ce sort peut être lancé sur l'adversaire";
            else
                _textEnemyTargetable.text = "Ce sort ne peut pas être lancé sur l'adversaire";

            switch(range.Orientation)
            {
                case Orientation.EnumOrientation.Any:
                    _textOrientation.text = "Ce sort peut être lancé dans n'importe quelle direction";
                    break;
                case Orientation.EnumOrientation.Diagonal:
                    _textOrientation.text = "Ce sort ne peut être lancé qu'en diagonale";
                    break;
                case Orientation.EnumOrientation.Line:
                    _textOrientation.text = "Ce sort ne peut être lancé qu'en ligne";
                    break;
            }
           
        }
    }
}

