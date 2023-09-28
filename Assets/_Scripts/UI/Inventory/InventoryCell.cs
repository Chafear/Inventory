using Chafear.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace Chafear.UI.Inventory
{
	public sealed class InventoryCell : MonoBehaviour
	{
		[SerializeField] private Image validateImage;
		[SerializeField] private Image rarityImage;

		public void SetRarity( ERarity rarity )
		{
			rarityImage.color = Constants.UI.RarityColors[rarity];
		}

		public void SetValidation( bool isValid )
		{
			validateImage.color = isValid ? Color.green : Color.red;
		}

		public void ClearValidation( )
		{
			validateImage.color = new Color(1,1,1,0);
		}
		
		public void ClearRarity( )
		{
			rarityImage.color = new Color(1,1,1,0);
		}
	}
}
