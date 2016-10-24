using UnityEngine;
using System.Collections;

public class HarvestPlayerController : MonoBehaviour {

	[SerializeField]
	protected HarvestPlayerSkillManager _skillManager;

	[SerializeField]
	protected Camera _camera;

	protected Vector3 _GetGUIPosition()
	{
#if UNITY_ANDROID && !UNITY_EDITOR
		return Input.GetTouch(0).position;
#else
		return Input.mousePosition;
#endif
	}
	
	protected bool _IsPressed()
	{
		return Input.GetButtonDown("Fire1");
    }

	protected bool _isPressed;

	protected void FixedUpdate()
	{
		_isPressed = _isPressed || this._IsPressed();
    }

	protected void Update()
	{
		if (GameController.i.gameOver) return;

		_isPressed = _isPressed || this._IsPressed();

		if (_isPressed)
		{
			_skillManager.UseSkill(
				this.GetWorldPosition(
					this._GetGUIPosition()));
		}

		_isPressed = false;
    }

	public Vector3 GetWorldPosition(Vector3 guipos)
	{
		return _camera.ScreenToWorldPoint(guipos);
	}

}
