using UnityEngine;
using System.Collections;

public enum EHarvestHumanMovementState
{
	STAY,
	LEFT,
	RIGHT,
	TOP,
	BOTTOM
}

public class HarvestHumanController : MonoBehaviour
{
	public HarvestHuman					human { get; protected set; }
	public EHarvestHumanMovementState	movementState { get; protected set; }

	[SerializeField]
	protected HarvestHumanRandomMovement _randomMovement;

	protected Vector3 _prevWorldPos = Vector3.zero;
	
	public void Initialize(HarvestHuman __human)
	{
		human = __human;

		_prevWorldPos = this.transform.position;
		_randomMovement.Initialize(this);
    }

	public void UpdateState()
	{
		this._UpdateMovementState();
    }

	public const float eps = 0.0001f;
	protected void _UpdateMovementState()
	{
		var dx = this.transform.position.x - _prevWorldPos.x;
		var dy = this.transform.position.y - _prevWorldPos.y;
		var dxm = Mathf.Abs(dx);
		var dym = Mathf.Abs(dy);

		if (dxm + dym <= eps)
		{
			movementState = EHarvestHumanMovementState.STAY;
		}
		else
		{
			if (dx > 0.0f && dy > 0.0f)
				movementState = EHarvestHumanMovementState.TOP;
			else if (dx > 0.0f && dy < 0.0f)
				movementState = EHarvestHumanMovementState.RIGHT;
			else if (dx < 0.0f && dy < 0.0f)
				movementState = EHarvestHumanMovementState.BOTTOM;
			else
				movementState = EHarvestHumanMovementState.LEFT;
        }

		_prevWorldPos = this.transform.position;
    }

	public void RandomMovement()
	{
		_randomMovement.gameObject.Show();
	}

	public void StopMovement()
	{
		human.rb2d.velocity = Vector2.zero;
		_randomMovement.Hide();
    }

	public void ContinueMovement()
	{
		_randomMovement.Show();
	}

	public void Clear()
	{

	}

}
