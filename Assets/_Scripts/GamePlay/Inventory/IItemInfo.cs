namespace Chafear.Data
{
	public interface IItemInfo : IRefreshable
	{
		int CurrentWidth { get; }
		int CurrentHeight { get; }
		int CurrentRotation { get; }
		bool[,] CurrentShape { get; }
		SoItem Data { get; }
		int ShapeSize { get; }
		void Rotate( );
	}
}
