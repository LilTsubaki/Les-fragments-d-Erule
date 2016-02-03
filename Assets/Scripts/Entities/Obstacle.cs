using UnityEngine;
using System.Collections;

public class Obstacle : Entity {

	public GameObject _gameobject;
    public Obstacle(Hexagon position) : base(position) {
        position._entity = this;
    }

}
