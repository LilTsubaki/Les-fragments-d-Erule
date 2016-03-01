using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


 public class ColorErule
{
    public static readonly Color _default = Color.grey;
    public static readonly Color _targetable = new Color(0f,0f,1f);
    public static readonly Color _overEnnemiTargetable = new Color(1f, 0f, 0f);
    public static readonly Color _overSelfTargetable = new Color(1f, 1f, 0f);
    public static readonly Color _accessible = new Color(0f, 1.0f, 0f);
    public static readonly Color _overAccessible = new Color(0.4f, 1.0f, 0.4f);
    public static readonly Color _groundEffectColor = new Color(0.8f, 0.53f, 1);

    public static readonly Color _spawn = Color.green;

    public static readonly Color _air = new Color(0.7f, 0.7f, 1.0f);
    public static readonly Color _earth = new Color(47.0f / 255.0f, 105.0f / 255.0f, 27.0f / 255.0f);
    public static readonly Color _fire = new Color(0.5f, 0, 0);
    public static readonly Color _metal = new Color(0.2f, 0.2f, 0.2f);
    public static readonly Color _water = new Color(0, 0, 0.5f);
    public static readonly Color _wood = new Color(212.0f / 255.0f, 161.0f / 255.0f, 144.0f / 255.0f);

    // Text Effects
    public static readonly Color _damage = Color.red;
    public static readonly Color _heal = Color.green;
    public static readonly Color _gain = new Color(0.7f, 0.7f, 1.0f);
    public static readonly Color _loss = new Color(1.0f, 0.55f, 0.0f);

}

