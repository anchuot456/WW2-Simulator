using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ArmyCollision : MonoBehaviour
{
    private List<GameObject> armies;
    private void Start()
    {
        armies= new List<GameObject>(GameObject.FindGameObjectsWithTag("Army"));
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        var index = ArmySelectionController.getPoint(transform.position);
        var battleSceneIndex = (int)(index.y * 4 + index.x);

        gameObject.GetComponent<ArmyDetail>().SetStatus(ArmyDetail.Status.InBattle);
        SceneManager.LoadScene(battleSceneIndex+2);
    }
}
