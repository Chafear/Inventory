using Chafear.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace Chafear
{
	public static class Extensions
	{
		public static void SetTreshold(this Image image )
		{
			image.alphaHitTestMinimumThreshold = Constants.UI.TriggerTreshlohd;
		}

		public static void SetInteractible( this CanvasGroup canvasGroup )
		{
			canvasGroup.blocksRaycasts = true;
			canvasGroup.interactable = true;
		}

		public static void SetNotInteractible( this CanvasGroup canvasGroup )
		{
			canvasGroup.blocksRaycasts = false;
			canvasGroup.interactable = false;
		}
	}
}
