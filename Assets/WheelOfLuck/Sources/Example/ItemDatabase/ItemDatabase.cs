using System.Collections.Generic;
using UnityEngine;

namespace Sources.Example{
	[CreateAssetMenu(fileName = "ItemDatabase", menuName = "Scriptable Objects/ItemDatabase")]
	public sealed class ItemDatabase : ScriptableObject{
		[SerializeField] private List<Item> _items;

		public List<Item> Items => _items;
	}
}