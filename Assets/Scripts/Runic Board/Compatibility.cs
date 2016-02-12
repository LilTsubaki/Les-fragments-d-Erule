using UnityEngine;
using System.Collections;

public class Compatibility {

    private string _id;
    private CompatibilityMalus _malus;

    public Compatibility() { }

    public Compatibility(string id, CompatibilityMalus malus)
    {
        _id = id;
        _malus = malus;
    }
}
