using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
	[SerializeField] private float m_OriginalCheckTimer;
	private bool m_CheckOnePassed;
	private bool m_CheckTwoPassed;
	private bool m_CheckThreePassed;
	public float m_CheckTimer;
	public float m_Score;

	// Start is called before the first frame update
	void Start()
    {
		m_CheckTimer = m_OriginalCheckTimer;
    }

    // Update is called once per frame
    void Update()
    {
        if(m_CheckThreePassed == true)
		{
			m_Score++;
			m_CheckOnePassed = false;
			m_CheckTwoPassed = false;
			m_CheckThreePassed = false;
			m_CheckTimer = m_OriginalCheckTimer;
		}
		if (m_CheckOnePassed)
		{
			m_CheckTimer -= Time.deltaTime;
		}
		if(m_CheckTimer <= 0)
		{
			m_CheckTimer = m_OriginalCheckTimer;
			m_CheckOnePassed = false;
			m_CheckTwoPassed = false;
			m_CheckThreePassed = false;
		}
    }

	private void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("CheckOne"))
		{
			m_CheckOnePassed = true;
		}
		if (other.CompareTag("CheckTwo") && m_CheckOnePassed)
		{
			m_CheckTwoPassed = true;
		}
		if (other.CompareTag("CheckThree") && m_CheckTwoPassed)
		{
			m_CheckThreePassed = true;
		}
	}
}
