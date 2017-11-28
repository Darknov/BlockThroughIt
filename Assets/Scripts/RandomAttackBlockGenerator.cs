using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAttackBlockGenerator : MonoBehaviour {

    public List<GameObject> possibleBlocks = new List<GameObject>();
    public GameObject destroyBlock;
    private System.Random lotto = new System.Random();

    public AttackBlock createRandomBlock(Transform parent, float blockSpeed, float jumpLength, Vector3 moveDirection)
    {
        int randomIndex = lotto.Next(0, possibleBlocks.Count-1);
        AttackBlock atkBlock = Instantiate(possibleBlocks[randomIndex], parent).GetComponent<AttackBlock>();

        atkBlock.MoveSpeed = blockSpeed;
        atkBlock.JumpLength = jumpLength;
        atkBlock.MoveDirection = moveDirection;

        atkBlock.Initialize();

        return atkBlock;
    }

    public AttackBlock createDestroyBlock(Transform parent, float blockSpeed, float jumpLength, Vector3 moveDirection)
    {
        AttackBlock atkBlock = Instantiate(destroyBlock, parent).GetComponent<AttackBlock>();

        atkBlock.MoveSpeed = blockSpeed;
        atkBlock.JumpLength = jumpLength;
        atkBlock.MoveDirection = moveDirection;

        atkBlock.Initialize();

        return atkBlock;
    }
}
