using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableItem : MonoBehaviour
{
	public Rigidbody m_RigidBody;

	private bool m_CurrentlyInteracting;

	private float m_VelocityFactor = 20000f;
	private Vector3 m_PosDelta;

	private float m_RotationFactor = 400f;
	private Quaternion m_RotationDelta;
	private float m_Angle;
	private Vector3	m_Axis;

	private HandController m_AttachedHand;

	private Transform m_InteractionPoint;

	private bool m_BeingThrown;
	private Vector3 m_ThrownPosition;
	public float m_ThrownDistance;
	private void Start()
	{
		m_RigidBody = GetComponent<Rigidbody>();
		m_InteractionPoint = new GameObject().transform;
		m_VelocityFactor /= m_RigidBody.mass;
		m_RotationFactor /= m_RigidBody.mass;
	}

	private void Update()
	{
		if(m_AttachedHand && m_CurrentlyInteracting)
		{
			m_PosDelta = m_AttachedHand.transform.position - m_InteractionPoint.position;
			m_RigidBody.velocity = m_PosDelta * m_VelocityFactor * Time.fixedDeltaTime;

			m_RotationDelta = m_AttachedHand.transform.rotation * Quaternion.Inverse(m_InteractionPoint.rotation);
			m_RotationDelta.ToAngleAxis(out m_Angle, out m_Axis);

			if(m_Angle > 180)
			{
				m_Angle -= 360;
			}

			m_RigidBody.angularVelocity = (Time.fixedDeltaTime * m_Angle * m_Axis) * m_RotationFactor;
		}
	}


	public void BeginInteraction(HandController hand)
	{
		m_AttachedHand = hand;
		m_InteractionPoint.position = hand.transform.position;
		m_InteractionPoint.rotation = hand.transform.rotation;
		m_InteractionPoint.SetParent(transform, true);
		Debug.Log("Interaction Started");
		m_CurrentlyInteracting = true;
	}

	public void EndInteraction(HandController hand)
	{
		Debug.Log("Interaction Ended");
		if ( hand == m_AttachedHand)
		{
			m_AttachedHand = null;
			m_CurrentlyInteracting = false;
			m_BeingThrown = true;
			m_ThrownPosition = transform.position;
		}
	}

	public bool IsInteracting()
	{
		return m_CurrentlyInteracting;
	}

	private void OnCollisionEnter(Collision collision)
	{
		if(m_BeingThrown == true)
		{
			m_ThrownDistance = Vector3.Distance(m_ThrownPosition, transform.position);
			m_BeingThrown = false;
		}
	}
}
