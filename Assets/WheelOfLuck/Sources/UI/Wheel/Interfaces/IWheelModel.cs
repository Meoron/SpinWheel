using System.Collections.Generic;

namespace Sources.UI.Wheel.Interfaces{
	public interface IWheelModel{
		public List<(string, int)> CurrentItemList{ get; }
		public bool CanSpin{ get; }
		public void Init();
		public int SpinToRandomCell();
		public (string, int) DropItem();
		public bool IsPlayerHaveItem(string id);
	}
}