using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Sources.UI.Wheel{
	[CreateAssetMenu(fileName = "WheelConfig", menuName = "Scriptable Objects/WheelConfig")]
	[Serializable]
	public class WheelConfig : ScriptableObject{
		[SerializeField] public List<ItemForWheel> OnceItems = new List<ItemForWheel>();
		[SerializeField] public List<ItemForWheel> BonusItems = new List<ItemForWheel>();
		[SerializeField] public List<ItemForWheel> MustItems = new List<ItemForWheel>();
		[SerializeField] public ItemForWheel DefaultItem;
		[SerializeField] public int OnceItemsOnWheel;
		[SerializeField] public int BonusItemsOnWheel;
		[SerializeField] public int FreeSpins;
		[SerializeField] public int MoneyOnSpin;
		[SerializeField] public float SpeedAnimation;

		public float GetChance(string id){
			return GetItemsList().FirstOrDefault(x => x.Id == id).Chance;
		}

		public int GetCount(string id){
			return GetItemsList().FirstOrDefault(x => x.Id == id).Count;
		}

		public bool ItemContains(List<ItemForWheel> itemList, (string, int) itemAndCount){
			return itemList.FirstOrDefault(x => x.Id == itemAndCount.Item1 && x.Count == itemAndCount.Item2)!=null;
		}
		
		private List<ItemForWheel> GetItemsList(){
			List<ItemForWheel> allItems = new List<ItemForWheel>();
			allItems.AddRange(OnceItems);
			allItems.AddRange(BonusItems);

			return allItems;
		}
	}
}