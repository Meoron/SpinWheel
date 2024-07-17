using System;
using System.Collections.Generic;
using DG.Tweening;
using Sources.UI.Wheel.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.UI.Wheel{
	public sealed class WheelView : MonoBehaviour, IWheelView{
		[SerializeField] private Transform _wheel;
		[SerializeField] private Transform _cellContainer;
		[SerializeField] private Button _button;

		private float _wheelOffset_euler = 90;
		private int _additionalTurns = 10;
		private float _speed_seconds = 3f;

		private List<Color> _colorScheme = new List<Color>(){
			Color.green,
			Color.magenta,
			Color.cyan,
		};

		private RectTransform _wheelRect;
		private Cell _cellPrefab;
		private List<Cell> _cells = new List<Cell>();

		public event Action OnSpin;
		public event Action OnEndSpinAnimation;

		private void Awake(){
			_cellPrefab = Resources.Load<Cell>("WheelOfLuck/Prefabs/UI/Cell");
			_button.onClick.AddListener(SpinButtonClicked);
			_wheel.localEulerAngles = Vector3.forward * _wheelOffset_euler;
			_wheelRect = _wheel.GetComponent<RectTransform>();
		}

		public void Init(float speedAnimation){
			_speed_seconds = speedAnimation;
		}

		public void UpdateItemList(List<(string, int)> currentItems){
			InitCells(currentItems);
		}

		public void CanSpin(bool canSpin){
			ActiveButton(canSpin);
		}

		public void Open(){
			//Open animation logic
		}

		public void Close(){
			Destroy(gameObject);
		}

		public void Spin(int cellIndex){
			RotateWheel(cellIndex);
		}

		private void SpinButtonClicked(){
			OnSpin?.Invoke();
		}


		private void InitCells(List<(string, int)> currentItems){
			Cell cell;
			for (int i = 0; i < currentItems.Count; i++){
				if (i >= _cells.Count){
					cell = Instantiate(_cellPrefab, _cellContainer);
					_cells.Add(cell);
				}
				else{
					cell = _cells[i];
				}

				if (cell?.CurrentItemId != currentItems[i].Item1){
					cell?.Init(i, currentItems.Count, currentItems[i], _colorScheme, _wheelRect.rect.width / 2f);
				}
			}
		}

		private void ActiveButton(bool enable){
			_button.interactable = enable;
		}

		private void RotateWheel(int cellIndex){
			_button.interactable = false;

			var segmentAngle = 360f / _cells.Count;
			var cellPosition = segmentAngle * cellIndex;
			var additionalTurnsAngle = _additionalTurns * 360;
			var angleToZeroPoint = _wheel.localEulerAngles.z - 360f;
			var endRotate = -Vector3.forward *
							(angleToZeroPoint + additionalTurnsAngle + cellPosition - _wheelOffset_euler);

			_wheel.DORotate(endRotate, _speed_seconds, RotateMode.WorldAxisAdd).OnComplete(EndSpinAnimation);
		}

		private void EndSpinAnimation(){
			_button.interactable = true;
			OnEndSpinAnimation?.Invoke();
		}
	}
}