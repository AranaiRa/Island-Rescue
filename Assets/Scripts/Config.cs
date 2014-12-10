using UnityEngine;
using System.Collections;

public static class Config {

	public static bool Paused = false;
	public static KeyCode 
		Craft = KeyCode.F,
		NextItem = KeyCode.X,
		PrevItem = KeyCode.Z,
		Drop = KeyCode.Q,
		Use = KeyCode.E,
		Deposit = KeyCode.Tab;
	public static InventoryManager inventory;
	public static NeedsManager needs;
	public static Collector collector;
	public static bool hasTent = false;
	public static int numDaysSurvived = -1;
	
	public static void ResetInstanceInformation(){
		hasTent = false;
		numDaysSurvived = -1;
	}
}