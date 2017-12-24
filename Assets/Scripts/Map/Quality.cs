using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Quality : MonoBehaviour {

	public Button Low;
	public Button Medium;
	public Button High;

	void Start () {
		Low = Low.GetComponent<Button>();
		Medium = Medium.GetComponent<Button>();
		High = High.GetComponent<Button>();
	
	}
	public void lowww()
	{
		QualitySettings.SetQualityLevel(0);
	}
	public void meddd()
	{
		QualitySettings.SetQualityLevel(1);
	}
	public void highhh()
	{
		QualitySettings.SetQualityLevel(2);
	}

}