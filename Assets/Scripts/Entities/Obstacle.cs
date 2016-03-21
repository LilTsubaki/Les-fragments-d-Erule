using UnityEngine;
using System.Collections;

public class Obstacle : Entity {

    public Obstacle(Hexagon position) : base(position) {
        position._entity = this;
    }

}
