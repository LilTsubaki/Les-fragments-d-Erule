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
    public RawImage rawImage;
    public GameObject board;

    private bool _alreadyCreated;
    private List<Hexagon> hexas;

    public void Awake()
    {
        _alreadyCreated = false;
        hexas = new List<Hexagon>();
    }

    public void Update()
    {
        UpdateRange();
        UpdateArea();
    }

    public void UpdateArea()
    {
        SendBoardResponse sbr = RunicBoardManager.GetInstance()._runicBoardBehaviour.LatestSendBoardResponse;
        if (sbr != null && sbr._area != null && sbr._updateArea)
        {            
            sbr._updateArea = false;
            List<Hexagon> _finalArea = new List<Hexagon>();
            Area area = sbr._area;
            PlayBoard playBoard = new PlayBoard(5, 5);

            GameObject glyph = Resources.Load<GameObject>("prefabs/SM_Glyphe_1");

            if(!_alreadyCreated)
            {               
                hexas.Add(new Hexagon(0, 2, playBoard));
                hexas.Add(new Hexagon(1, 2, playBoard));
                hexas.Add(new Hexagon(2, 2, playBoard)); //centre
                hexas.Add(new Hexagon(3, 2, playBoard));
                hexas.Add(new Hexagon(4, 2, playBoard));

                hexas.Add(new Hexagon(1, 3, playBoard));
                hexas.Add(new Hexagon(2, 3, playBoard));
                hexas.Add(new Hexagon(3, 3, playBoard));
                hexas.Add(new Hexagon(4, 3, playBoard));

                hexas.Add(new Hexagon(2, 4, playBoard));
                hexas.Add(new Hexagon(3, 4, playBoard));
                hexas.Add(new Hexagon(4, 4, playBoard));

                hexas.Add(new Hexagon(0, 1, playBoard));
                hexas.Add(new Hexagon(1, 1, playBoard));
                hexas.Add(new Hexagon(2, 1, playBoard));
                hexas.Add(new Hexagon(3, 1, playBoard));

                hexas.Add(new Hexagon(0, 0, playBoard));
                hexas.Add(new Hexagon(1, 0, playBoard));
                hexas.Add(new Hexagon(2, 0, playBoard));

                for (int i = 0; i < hexas.Count; i++)
                {
                    Hexagon hexagon = playBoard.CreateHexagone(hexas[i]._posX, hexas[i]._posY);
                    hexagon.GameObject = new GameObject("Hexagon");
                    hexagon.GameObject.transform.parent = board.transform;
                    hexagon.GameObject.transform.localPosition = new Vector3(0.866f * hexagon._posX - 0.433f * hexagon._posY, 0.2f, 0.75f * hexagon._posY);
                    hexagon.GameObject.transform.localScale = Vector3.one;

                    hexagon.Glyph = GameObject.Instantiate(glyph);
                    hexagon.Glyph.transform.parent = hexagon.GameObject.transform;
                    hexagon.Glyph.transform.localPosition = new Vector3(0,0,0);
                    hexagon.Glyph.transform.localScale = Vector3.one;

                    hexagon.CurrentState = Hexagon.State.Targetable;
                    hexagon.Orientation = hexas[i].Orientation;

                    hexas[i] = hexagon;
                }
                _alreadyCreated = true;
            }
            
            foreach(Hexagon hexa in hexas)
            {
                hexa.CurrentState = Hexagon.State.Targetable;
                hexa.GameObject.SetActive(true);
            }

            if (area.Orientation == Orientation.EnumOrientation.Line || area.Orientation == Orientation.EnumOrientation.Any)
                _finalArea = area.AreaToHexa(Direction.EnumDirection.East, hexas[1]);
            
            if (area.Orientation == Orientation.EnumOrientation.Diagonal)
                _finalArea = area.AreaToHexa(Direction.EnumDirection.DiagonalNorthEast, hexas[1]);

            for (int i = 0; i < _finalArea.Count; i++)
            {
                _finalArea[i].CurrentState = Hexagon.State.OverEnnemiTargetable;
                Logger.Debug(_finalArea[i]._posX + " " + _finalArea[i]._posY + " : " + _finalArea[i].CurrentState + ", " + _finalArea[i].StateChanged);
            }

            for (int i = 0; i < hexas.Count; i++)
            {
                //Logger.Debug(hexas[i].Orientation);
                if (!_finalArea.Contains(hexas[i]))
                {
                    hexas[i].GameObject.SetActive(false);
                }
            }
        }
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

    void SetLayerRecursively(GameObject obj, int newLayer)
    {
        if (null == obj)
        {
            return;
        }

        obj.layer = newLayer;

        foreach (Transform child in obj.transform)
        {
            if (null == child)
            {
                continue;
            }
            SetLayerRecursively(child.gameObject, newLayer);
        }
    }
}

