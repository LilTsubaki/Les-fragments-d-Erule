using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


 public class HexagonColor
{
    public static readonly Color _default = Color.grey;
    public static readonly Color _targetable = new Color(0.5f,0.5f,1);
    public static readonly Color _overEnnemiTargetable = new Color(1, 0.5f, 0.5f);
    public static readonly Color _overSelfTargetable = new Color(1, 1, 0.5f);
    public static readonly Color _accessible = new Color(0.5f, 1, 0.5f);
}

