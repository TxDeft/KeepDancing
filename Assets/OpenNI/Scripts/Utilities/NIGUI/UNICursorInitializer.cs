using UnityEngine;
using System.Collections;

[RequireComponent(typeof(UNIGUICursor))]
public class UNICursorInitializer : MonoBehaviour {

	/// Just an initialization to get the cursor from the prefab and set it to UNIGUI
	void Awake () 
    {
        UNIGUICursor cursor=gameObject.GetComponent(typeof(UNIGUICursor)) as UNIGUICursor;
        UNIGUI.SetCursor(cursor);
        UNIGUI.ResetGroups(); // to make sure we don't have any baggages...
	}
}

