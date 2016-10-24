using UnityEngine;
using System.Collections;

public class HarvestHumanGenArea : DONCArea2D {

	[SerializeField]
	protected Vector3 _minSpeed;
	[SerializeField]
	protected Vector3 _maxSpeed; 

	public void Generate(HarvestHuman human)
	{
		human.rb2d.velocity = this._GetSpeed();
		human.rb2d.position = this.GetRandomWorldPosition();
    }

	protected Vector3 _GetSpeed()
	{
		return DORandom.GetRandomVector3(_minSpeed, _maxSpeed);
	}



}
