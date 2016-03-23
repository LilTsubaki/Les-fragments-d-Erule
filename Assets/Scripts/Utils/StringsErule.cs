using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public class StringsErule
{
    public static readonly string portal = "{0} a créé un <b>portail</b>.";
    public static readonly string obstacle = "{0} a créé un <b>obstacle</b>.";
    public static readonly string area = "{0} a créé une <b>zone</b>.";
    public static readonly string damage = "{0} a subit <b>{1} {2}</b>.";
    public static readonly string heal = "{0} s'est soigné de <b>{1}</b>.";
    public static readonly string dot = "{0} subit des dégats <b>{1}</b> pendant <b>{2}</b> tours."; // TO DO
    public static readonly string hot = "{0} se soigne pendant <b>{1}</b> tours."; // TO DO
    public static readonly string rangeBonus = "{0} gagne <b>{1}</b> de <b>Portée</b> pendant <b>{2}</b> tours."; // TOUT DOUX
    public static readonly string rangeMalus = "{0} perd <b>{1}</b> de <b>Portée</b> pendant <b>{2}</b> tours."; // TOUT DOUX
    public static readonly string protectionBonus = "{0} reçoit un bonus de {1}% de protection {2}."; // TOUT DOUX
    public static readonly string protectionMalus = "{0} reçoit un malus de {1}% de protection {2}."; // TOUT DOUX
    public static readonly string perfect = "Le sort de {0} est Parfait, il garde sa rune centrale.";
    public static readonly string crit = "Le sort de {0} est Sublimé, il frappe plus fort.";
    public static readonly string unstable = "Le sort de {0} est Instable, il subit des dégats.";
}

