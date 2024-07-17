using System;
using System.Collections.Generic;

namespace Sources.UI.Wheel.Interfaces{
	public interface IWheelView{
		public event Action OnSpin;
		public event Action OnEndSpinAnimation;
		
		public void Init(float speedAnimation);
		public void UpdateItemList(List<(string, int)> currentItems);
		public void CanSpin(bool canSpin);
		public void Spin(int cellIndex);
		public void Open();
		public void Close();
	}
}