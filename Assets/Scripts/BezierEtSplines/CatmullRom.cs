using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// https://www.habrador.com/tutorials/interpolation/1-catmull-rom-splines/

public class CatmullRom : MonoBehaviour
{
	public Transform[] PointsList;
	public bool isLooping = true;
	
	void OnDrawGizmos()
	{
		Gizmos.color = Color.white;
		
		for (int i = 0; i < PointsList.Length; i++)
		{
			if ((i == 0 || i == PointsList.Length - 2 || i == PointsList.Length - 1) && !isLooping)
			{
				continue;
			}
    
			DisplayCatmullRomSpline(i);
		}
	}
	
	void DisplayCatmullRomSpline(int position)
	{
		Vector3 p0 = PointsList[ClampListPos(position - 1)].position;
		Vector3 p1 = PointsList[position].position;
		Vector3 p2 = PointsList[ClampListPos(position + 1)].position;
		Vector3 p3 = PointsList[ClampListPos(position + 2)].position;
		
		Vector3 lastPosition = p1;
		
		float resolution = 0.2f;
		
		int loops = Mathf.FloorToInt(1f / resolution);
    
		for (int i = 1; i <= loops; i++)
		{
			float t = i * resolution;
			
			Vector3 newPosition = GetCatmullRomPosition(t, p0, p1, p2, p3);
			
			Gizmos.DrawLine(lastPosition, newPosition);
			
			lastPosition = newPosition;
		}
	}
	
	int ClampListPos(int position)
	{
		if (position < 0)
		{
			position = PointsList.Length - 1;
		}
    
		if (position > PointsList.Length)
		{
			position = 1;
		}
		else if (position > PointsList.Length - 1)
		{
			position = 0;
		}
    
		return position;
	}
	
	Vector3 GetCatmullRomPosition(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
	{
		Vector3 a = 2f * p1;
		Vector3 b = p2 - p0;
		Vector3 c = 2f * p0 - 5f * p1 + 4f * p2 - p3;
		Vector3 d = -p0 + 3f * p1 - 3f * p2 + p3;
		
		Vector3 pos = 0.5f * (a + (b * t) + (c * t * t) + (d * t * t * t));
    
		return pos;
	}
}
