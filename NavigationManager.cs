using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NavigationManager : MonoBehaviour {


	public struct Route
	{
		public string RouteDescription;
		public bool CanTravel;
	}

	public static Dictionary<string, Route> RouteInformation = new Dictionary<string, Route>() {
		{ "Town", new Route {CanTravel = true}},
		{ "Shop", new Route {CanTravel = true}},
		{ "Arena", new Route {CanTravel = true}},
	};

	private static string PreviousLocation;

	public static string GetRouteInfo(string destination)
	{
		return RouteInformation.ContainsKey(destination) ? RouteInformation[destination].RouteDescription : null;
	}

	public static bool CanNavigate(string destination)
	{
		return RouteInformation.ContainsKey(destination) ? RouteInformation[destination].CanTravel : false;
	}

	public static void NavigateTo(string destination)
	{
		PreviousLocation = SceneManager.GetActiveScene().name;
		//PreviousLocation = Application.loadedLevelName;
		//Debug.Log (destination);
		if (PreviousLocation == "Arena" && destination == "Town") {
			GameState.PlayerReturningHome = true;
		} else {
			GameState.PlayerReturningHome = false;
		}

		ChangeScene (destination);
	}

	public static void GoBack()
	{
		var backlocation = PreviousLocation;
		PreviousLocation = SceneManager.GetActiveScene().name;
		//PreviousLocation = Application.loadedLevelName;

		SceneManager.LoadScene (backlocation);
		//FadeInOutManager.FadeToLevel (backlocation);
	}

	public static void ChangeScene(string destination){
		SceneManager.LoadScene (destination);
	}
}
