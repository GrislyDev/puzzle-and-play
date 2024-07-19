using CandyCoded.HapticFeedback;
using GrislyTools;


public static class Taptic
{
	public static void Light()
	{
		DataManager.Data.GetValue("Taptic", out bool taptic, false);

		if (taptic)
			HapticFeedback.LightFeedback();
	}
}