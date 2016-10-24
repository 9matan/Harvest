using UnityEngine;
using System.Collections;

public class DOEncryptRegistryFactory : MonoBehaviour {

#if UNITY_EDITOR
	
	public string 	regName = "Bonus_";
	public int 		eNumber = 2;
	public bool		saveKeys = true;
	public bool		validateAtLoad = true;
	public bool		loadAtInitialize = true;
	
	[ContextMenu("Add registry")]
	virtual public void AddRegistry()
	{
		var name = regName + this.transform.childCount.ToString ();
		var go = new GameObject (name);
		go.transform.parent = this.transform;
		
		var reg = go.AddComponent<DOEncryptRegistry> ();
		reg.regName = name;
		reg.validateAtLoad = validateAtLoad;
		reg.loadAtInitialize = loadAtInitialize;

		for (int i = 0; i < eNumber; ++i)
			reg.AddEncryptor ();

		if(saveKeys)
			reg.SaveEncryptors ();
	}

#else
	
	void Awake()
	{
		Destroy(this);
	}
	
#endif


}
