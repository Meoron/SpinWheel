using System;
using UnityEngine;

namespace Sources.Example{
	[Serializable]
	public class Item{
		[SerializeField] private string _id;
		[SerializeField] private Sprite _icon;
		[SerializeField] private string _name;

		public string ID => _id;
		public Sprite Icon => _icon;
		public string Name => _name;
	}
}