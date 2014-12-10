using UnityEngine;
using System.Collections;

public enum GameItem{
	NONE, 
	Log, Branch, Twig, Flint, Rock, Mushroom, Onion, Meat, MeatCooked, Hide,
	Knife, Hatchet, Radio, Trap
}
public static class GameItemStrings{
	public static string Get(GameItem gi){
		string s = "";

		switch (gi) {
		case GameItem.Log:
			s = "Log";
			break;
		case GameItem.Branch:
			s = "Branch";
			break;
		case GameItem.Twig:
			s = "Twig";
			break;
		case GameItem.Flint:
			s = "Flint";
			break;
		case GameItem.Rock:
			s = "Rock";
			break;
		case GameItem.Mushroom:
			s = "Mushroom";
			break;
		case GameItem.Onion:
			s = "Onion";
			break;
		case GameItem.Meat:
			s = "Meat";
			break;
		case GameItem.MeatCooked:
			s = "Cooked Meat";
			break;
		case GameItem.Hide:
			s = "Hide";
			break;
		case GameItem.Knife:
			s = "Knife";
			break;
		case GameItem.Hatchet:
			s = "Hatchet";
			break;
		case GameItem.Radio:
			s = "Radio";
			break;
		case GameItem.Trap:
			s = "Trap";
			break;
		default:
			s = "<UNDEFINED>";
			break;
				}

		return s;
	}
}