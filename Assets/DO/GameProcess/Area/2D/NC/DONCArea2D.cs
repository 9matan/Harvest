using UnityEngine;
using System.Collections;

//
// < DOBounds2D >
//

[System.Serializable]
public struct DOBounds2D
{
	public float left;
	public float top;
	public float right;
	public float bottom;
	
	public Vector3[] points {
		get { return this.GetPoints(); }
	}

	public DOBounds2D (float _left, float _top, float _right, float _bottom)
	{
		left = _left; top = _top; right = _right; bottom = _bottom;
	}

	public static DOBounds2D operator* (DOBounds2D b, float v)
	{
		b.left *= v; b.right *= v; 
		b.top *= v; b.bottom *= v;
		return b;
	}

	public Vector3[] GetPoints()
	{
		Vector3[] ps = new Vector3[4];
		this.GetPoints (ps);
		return ps;
	}

	public void GetPoints(Vector3[] ps)
	{
		ps [0].x = left; ps [0].y = top;
		ps [1].x = right; ps [1].y = top;
		ps [2].x = right; ps [2].y = bottom;
		ps [3].x = left; ps [3].y = bottom;
	}

	public override string ToString ()
	{
		return string.Format ("left: {0}, top: {1}, right: {2}, bottom: {3}", 
		                      left, top, right, bottom);
	}
}

//
// </ DOBounds2D >
//

//
// < DONCArea2D >
//

public class DONCArea2D : MonoBehaviour, IDOBuilder {

	public Vector3[] worldPoints {
		get { return this.GetWorldPoints(); }
	}

	public DOBounds2D worldArea {
		get { return this.GetWorldArea(); }
	}

	public DOBounds2D localArea {
		get { return this.GetLocalArea(); }
	}

	public DOBounds2D area { 
		get { return _area; }
		set { _area = value; }
	}

	[SerializeField] protected DOBounds2D _area = new DOBounds2D(1.0f, 1.0f, 1.0f, 1.0f);

	public DOBounds2D GetLocalArea()
	{
		DOBounds2D local = new DOBounds2D ();

		local.right = _area.right;
		local.left = -_area.left;
		local.bottom = -_area.bottom;
		local.top = _area.top;

		return local;

	}

	public Vector3 GetRandomLocalPosition()
	{
		var world = this.GetLocalArea ();
		Vector3 pos = Vector3.zero;
		
		pos.x = Random.Range (world.left, world.right);
		pos.y = Random.Range (world.bottom, world.top);
		
		return pos;
	}

	public DOBounds2D GetWorldArea()
	{
		DOBounds2D world = new DOBounds2D ();

		world.left = this.transform.position.x - _area.left;
		world.right = this.transform.position.x + _area.right;
		world.top = this.transform.position.y + _area.top;
		world.bottom = this.transform.position.y - _area.bottom;

		return world;
	}

	public Vector3 GetRandomWorldPosition()
	{
		var world = this.GetWorldArea ();
		Vector3 pos = Vector3.zero;

		pos.x = Random.Range (world.left, world.right);
		pos.y = Random.Range (world.bottom, world.top);

		return pos;
	}                                

	// < points >

	protected Vector3[] _bufPoints = new Vector3[4];

	public Vector3[] GetWorldPoints()
	{
		Vector3[] ps = new Vector3[4];
		this.GetWorldPoints (ps);
		return ps;
	}

	public void GetWorldPoints(Vector3[] ps)
	{
		localArea.GetPoints (_bufPoints);
		for (int i = 0; i < _bufPoints.Length; ++i)
		{
			ps [i] = this.transform.TransformPoint (_bufPoints [i]);
		}
	}

	public bool IsHere(Vector3 worldPos)
	{
		Vector3 localPos = this.transform.InverseTransformPoint (worldPos);

		if(localPos.x < -_area.left || localPos.x > _area.right) return false;
		if(localPos.y < -_area.bottom || localPos.y > _area.top) return false;

		return true;
	}

	// </ points >

#if UNITY_EDITOR

	[Header("Gizmos")]
	public bool 		useGizmos = false;
	public DOGizmosInfo	gizmos = new DOGizmosInfo();

	protected DORectangle _rect = new DORectangle();

	protected void OnDrawGizmos()
	{
		if(useGizmos)
			this.DrawGizmos ();
	}

	public void DrawGizmos()
	{
		gizmos.BeginDrowing ();

		this.GetWorldPoints (_rect.points);
		_rect.DrawGizmos ();

		gizmos.EndDrawing ();
	}

#endif

#if UNITY_EDITOR
	
	//
	// < Builder >
	//	
	
	[ContextMenu("Build")]
	public virtual void Build()
	{
		
	}
	
	
	//
	// < /Builder >
	//
	
#else
	
	public virtual void Build(){}
	
#endif

}

//
// </ DONCArea2D >
//























