using UnityEngine;
using System.Collections;

public class UIInit : MonoBehaviour {

	private UIManager mUIManager;

	private void Start()
	{
		Object obj = FindObjectOfType (typeof(UIManager));
		if (obj)
			mUIManager = obj as UIManager;
		if (mUIManager == null) {
			GameObject uiManager = new GameObject ("UIManager");
			mUIManager = uiManager.AddComponent <UIManager>();
		}
		mUIManager.InitializeUIs ();

		SetTure();


    }

	public void SetTure()
	{
		mUIManager.SetVisible("Button_StartGame", true);
		mUIManager.SetVisible("Button_Continue", true);
	}

}
