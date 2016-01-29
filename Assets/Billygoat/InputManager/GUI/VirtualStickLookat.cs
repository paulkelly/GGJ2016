using UnityEngine;
using System.Collections;

public class VirtualStickLookat : MonoBehaviour
{
	public void ResetPosition()
	{
		UpdatePosition (new Vector2 (0, 0));
	}
	
	public void UpdatePosition(Vector2 newPos)
	{
		Vector3 localPosition = new Vector3 (newPos.x, -newPos.y, transform.localPosition.z);

		transform.localPosition = localPosition;

	}
}
