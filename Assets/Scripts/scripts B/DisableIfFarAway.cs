using UnityEngine;
using System.Collections;

public class DisableIfFarAway : MonoBehaviour {

    // --------------------------------------------------
    // Variables:

    private GameObject itemActivatorObject;
    private RenderDistance activationScript;
    public string Name;

	// --------------------------------------------------

	void Start()
	{
        itemActivatorObject = GameObject.Find(Name);
        activationScript = itemActivatorObject.GetComponent<RenderDistance>();

        StartCoroutine("AddToList");
    }

    IEnumerator AddToList()
    {
        yield return new WaitForSeconds(0.1f);

        activationScript.addList.Add(new ActivatorItem { item = this.gameObject });
    }
}