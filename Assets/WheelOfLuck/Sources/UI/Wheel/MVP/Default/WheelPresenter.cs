using Sources.Example;
using Sources.UI.Wheel.Interfaces;

namespace Sources.UI.Wheel{
	public class WheelPresenter{
		private IWheelModel _model;
		private IWheelView _view;
		private ItemDatabase _itemDatabase;
		
		private Inventory _inventory;

		public WheelPresenter(IWheelView view, IWheelModel model){
			_model = model;
			_view = view;
		}

		public void Init(float speedAnimation){
			_model.Init();
			_view.Init(speedAnimation);
			_view.UpdateItemList(_model.CurrentItemList);
			_view.CanSpin(_model.CanSpin);
		}

		public void Open(){
			_view.Open();
			_view.OnSpin += OnSpin;
			_view.OnEndSpinAnimation += AddedItemToInventory;
		}

		public void Close(){
			_view.OnSpin -= OnSpin;
			_view.OnEndSpinAnimation -= AddedItemToInventory;
			_view.Close();
		}

		public void OnSpin(){
			_view.Spin(_model.SpinToRandomCell());
		}
		
		private void AddedItemToInventory(){
			_model.DropItem();
			
			_view.UpdateItemList(_model.CurrentItemList);
			_view.CanSpin(_model.CanSpin);
		}
	}
}