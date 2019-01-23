using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketMovement : MonoBehaviour
{
	[SerializeField] private float m_MovementSpeed;
	[SerializeField] private Transform m_BoardTransform;
	[SerializeField] private Vector3 MaxPositions;
	[SerializeField] private Vector3 MinPositions;

	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		if (Input.GetKeyDown(KeyCode.I) && m_BoardTransform.position.y <= MaxPositions.y)
		{
			m_BoardTransform.position = new Vector3(m_BoardTransform.position.x, m_BoardTransform.position.y + m_MovementSpeed, m_BoardTransform.position.z);
		}
		if (Input.GetKeyDown(KeyCode.K) && m_BoardTransform.position.y >= MinPositions.y)
		{
			m_BoardTransform.position = new Vector3(m_BoardTransform.position.x, m_BoardTransform.position.y - m_MovementSpeed, m_BoardTransform.position.z);
		}
		if (Input.GetKeyDown(KeyCode.L) && m_BoardTransform.position.x >= MinPositions.x)
		{
			m_BoardTransform.position = new Vector3(m_BoardTransform.position.x - m_MovementSpeed, m_BoardTransform.position.y , m_BoardTransform.position.z);
		}
		if (Input.GetKeyDown(KeyCode.J) && m_BoardTransform.position.x <= MaxPositions.x)
		{
			m_BoardTransform.position = new Vector3(m_BoardTransform.position.x + m_MovementSpeed, m_BoardTransform.position.y, m_BoardTransform.position.z);
		}
		if (Input.GetKeyDown(KeyCode.N) && m_BoardTransform.position.z >= MinPositions.z)
		{
			m_BoardTransform.position = new Vector3(m_BoardTransform.position.x, m_BoardTransform.position.y, m_BoardTransform.position.z - m_MovementSpeed);
		}
		if (Input.GetKeyDown(KeyCode.M) && m_BoardTransform.position.z <= MaxPositions.z)
		{
			m_BoardTransform.position = new Vector3(m_BoardTransform.position.x , m_BoardTransform.position.y, m_BoardTransform.position.z + m_MovementSpeed);
		}
	}
}
