using System.Collections.Generic;
using Sources.Example;
using UnityEngine;
using UnityEngine.UI;

public sealed class Cell : MonoBehaviour{
	[SerializeField] private Image _background;
	[SerializeField] private Text _text;
	[SerializeField] private Image _icon;

	private string _currentItemId;

	public string CurrentItemId => _currentItemId;

	public void Init(int cellIndex, int cellsCount, (string, int) item, List<Color> colorScheme, float radius){
		_currentItemId = item.Item1;
		_text.text = $"x{item.Item2}";
		_background.color = GetBackgroundColor(cellIndex, cellsCount, colorScheme);
		_icon.sprite = ItemDatabaseService.GetItemIcon(item.Item1);
		SetSizeAndPosition(cellIndex, cellsCount, radius);
	}

	private void SetSizeAndPosition(int cellIndex, int cellsCount, float radius){
		var angle = 180f / cellsCount;
		var segmentAngle = 360f / cellsCount;
		var segmentAnglePosition = Vector3.forward * (segmentAngle * cellIndex + angle);

		_background.fillAmount = 1f / cellsCount;
		transform.localEulerAngles = segmentAnglePosition;
		//_text.transform.localEulerAngles = -Vector3.forward * angle;


		float angleInRadians = -(segmentAngle / 2) * Mathf.Deg2Rad;
		// Вычисление координат x и y
		float xText = GetXPositionOnCircle(angleInRadians, radius * 0.55f);
		float xIcon = GetXPositionOnCircle(angleInRadians, radius * 0.85f);
		float yText = GetYPositionOnCircle(angleInRadians, radius * 0.55f);
		float yIcon = GetYPositionOnCircle(angleInRadians, radius * 0.85f);

		_text.transform.localPosition = Vector3.right * xText + Vector3.up * yText;
		_icon.transform.localPosition = Vector3.right * xIcon + Vector3.up * yIcon;
	}

	private float GetXPositionOnCircle(float angle, float radius){
		return 1 + radius * Mathf.Cos(angle);
	}

	private float GetYPositionOnCircle(float angle, float radius){
		return radius * Mathf.Sin(angle);
	}

	private Color GetBackgroundColor(int cellIndex, int cellsCount, List<Color> colorScheme){
		if (cellsCount % 2 == 0){
			return cellIndex % 2 == 0 ? colorScheme[0] : colorScheme[1];
		}

		cellIndex++;

		return colorScheme[cellIndex % 3];
	}
}