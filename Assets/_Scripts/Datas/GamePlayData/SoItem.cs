using UnityEngine;

namespace Chafear.Data
{
	[CreateAssetMenu]
	public class SoItem : ScriptableObject
	{
		public string Id;
		public ERarity Rarity;
		public SoInventoryShape ShapeData;
		public int BasePrice = 0;

		public string IconId
		{
			get
			{
				return MetaConfig.Addressables.IconsPath +
					Id +
					MetaConfig.Addressables.IconsExt;
			}
		}
	}
}
