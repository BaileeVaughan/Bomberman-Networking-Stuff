using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;

public class BombermanTest
{
    public GameObject game;
    public Player[] players;

    public Player GetPlayer(int index)
    {
        foreach (var player in players)
        {
            if (player.playerNumber == index)
            {
                return player;
            }
        }
        return null;
    }

    [SetUp]
    public void SetUp()
    {
        GameObject gamePrefab = Resources.Load<GameObject>("Prefabs/Game");
        game = Object.Instantiate(gamePrefab);
        players = Object.FindObjectsOfType<Player>();
    }

    [UnityTest]
    public IEnumerator PlayerDropsBomb()
    {
        Player player1 = GetPlayer(1);
        player1.DropBomb();
        yield return new WaitForFixedUpdate();
        Bomb bomb = Object.FindObjectOfType<Bomb>();
        Assert.IsTrue(bomb != null, "Where's the fuckin bomb??");
    }

    [UnityTest]
    public IEnumerator PlayerMovement()
    {
        Player player1 = GetPlayer(1);

        Vector3 oldPosition = player1.transform.position;

        player1.Move(false, false, true, false);

        yield return new WaitForFixedUpdate();

        Vector3 newPositon = player1.transform.position;

        Assert.IsTrue(!oldPosition.Equals(newPositon));
    }

    [TearDown]
    public void TearDown()
    {
        Object.Destroy(game);
    }
}