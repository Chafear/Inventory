namespace Chafear.Data
{
	public interface IItemInfo
	{
		int CurrentWidth { get; }
		int CurrentHeight { get; }
		int CurrentRotation { get; }
		bool[,] CurrentShape { get; }
		int ShapeSize { get; }
	}
}
