using UnityEngine;
using System.Collections;

public class Transport : GameplayObject
{
    public override Type typeEnemy
    {
        get { return Type.Transport; }
    }

    public override GameplayObject Create(Transform root, LevelController levelController)
    {
        var transport = base.Create(root, levelController);

        PositionCube position = new PositionCube(3,0);

        var cube = levelController.gameField.GetCube(position);

        Vector3 newPos = cube.upSide.position + 
            levelController.gameField.GetOffsetDirection(DirectionMove.Direction.UpLeft);

        transport.transform.position = newPos;
        transport.transform.rotation = cube.upSide.rotation * Quaternion.Euler(new Vector3(-90, 0, 0))  ;

        return transport;
    }
}
