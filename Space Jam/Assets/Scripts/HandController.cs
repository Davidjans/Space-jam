using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class HandController : MonoBehaviour {
    private Valve.VR.EVRButtonId m_GripButton = Valve.VR.EVRButtonId.k_EButton_Grip;

    private Valve.VR.EVRButtonId m_TriggerButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;

    private Valve.VR.EVRButtonId m_TouchpadButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad;

	HashSet<InteractableItem> m_ObjectsHoveringOver = new HashSet<InteractableItem>();

	private InteractableItem m_ClosestItem;
	private InteractableItem m_InteractingItem;

    private SteamVR_Controller.Device m_Controller {
        get{ return steam.Input((int)m_TrackedObject.index);}
    }
    private SteamVR_TrackedObject m_TrackedObject;

	void Start () {
        m_TrackedObject = GetComponent<SteamVR_TrackedObject>();	
	}
	
	// Update is called once per frame
	void Update () {
		if(m_Controller == null){
            Debug.Log("Wheres yo controller my dude");
            return;
        }

        if (m_Controller.GetPressDown(m_TriggerButton))
        {
			float minDistance = float.MaxValue;
			float distance;
			foreach (InteractableItem item in m_ObjectsHoveringOver)
			{
				distance = (item.transform.position - transform.position).sqrMagnitude;
				if (distance < minDistance)
				{
					minDistance = distance;
					m_ClosestItem = item;
				}
			}
			m_InteractingItem = m_ClosestItem;

			if (m_InteractingItem)
			{
				if (m_InteractingItem.IsInteracting())
				{
					m_InteractingItem.EndInteraction(this);
				}
				m_InteractingItem.BeginInteraction(this);
			}
		}
        if (m_Controller.GetPressUp(m_TriggerButton))
        {
			m_InteractingItem.EndInteraction(this);
		}

        if (m_Controller.GetPressDown(m_GripButton))
        {
			

		}
        if (m_Controller.GetPressUp(m_GripButton))
        {
			
		}


        if (m_Controller.GetPressDown(m_TouchpadButton))
        {
			FindObjectOfType<CrateBehaviour>().m_CheckWhatPassed();
        }
        if (m_Controller.GetPressUp(m_TouchpadButton))
        {
        }
    }

	private void OnTriggerEnter(Collider collider)
	{
		InteractableItem collidedItem = collider.GetComponent<InteractableItem>();
		if (collidedItem)
		{
			m_ObjectsHoveringOver.Add(collidedItem);
		}
	}
	private void OnTriggerExit(Collider collider)
	{
		InteractableItem collidedItem = collider.GetComponent<InteractableItem>();
		if (collidedItem)
		{
			m_ObjectsHoveringOver.Remove(collidedItem);
		}
	}
}
