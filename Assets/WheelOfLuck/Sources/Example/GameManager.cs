using Sources.UI.Wheel;
using UnityEngine;

namespace Sources.Example{
	public class GameManager : MonoBehaviour{
		private WheelPresenter _wheelPresenter;
		
		public void Awake(){
			var inventory = new Inventory();
			var config = Resources.Load<WheelConfig>("WheelOfLuck/Configs/WheelConfig");
			var wheelView = Resources.Load<WheelView>("WheelOfLuck/Prefabs/UI/View/WheelView");
			var wheelModel = new WheelModel(config, inventory);

			wheelView = Instantiate(wheelView);
			_wheelPresenter = new WheelPresenter(wheelView, wheelModel);
			_wheelPresenter.Init(config.SpeedAnimation);
			
			_wheelPresenter.Open();
		}

		public void OnDestroy(){
			//_wheelPresenter.Close();
		}
	}
}