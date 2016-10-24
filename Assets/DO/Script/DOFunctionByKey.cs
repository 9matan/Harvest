using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DOFunctionByKey : MonoBehaviour, IDOBuilder {

	[SerializeField] protected KeyCode 	_key;
	[SerializeField] protected Button	_button;

	protected bool _isPressed = false;

	public void CatchKey ()
	{
		_isPressed = Input.GetKey (_key) || _isPressed;
	}

	public void CheckKey()
	{
		if (_isPressed)
			_button.onClick.Invoke ();
	}

	protected virtual void FixedUpdate()
	{
		this.CatchKey ();
	}

	protected virtual void Update()
	{
		this.CatchKey ();
		this.CheckKey ();
		_isPressed = false;
	}

#if UNITY_EDITOR
	
	//
	// < Builder >
	//	
	
	[ContextMenu("Build")]
	public virtual void Build()
	{
		this._BuildButton ();
	}

	protected void _BuildButton()
	{
		var go = this.gameObject.CreateChild("Button");
		_button = go.GetAdd<Button> ();
	}

	//
	// < /Builder >
	//

#else
	
	virtual public void Build() {}
	
#endif

}
