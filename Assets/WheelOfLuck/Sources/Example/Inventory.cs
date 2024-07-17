using System.Collections.Generic;
using UnityEngine;

namespace Sources.Example{
	public sealed class Inventory : IInventory{
		private string _moneyId = "004";
		private Dictionary<string, int> _inventory = new Dictionary<string, int>();

		public void AddItem(string id, int count){
			if (_inventory.ContainsKey(id)){
				_inventory[id] += count;
			}
			else{
				_inventory.Add(id,count);
			}
			
			Debug.Log($"Added item: {ItemDatabaseService.GetItemName(id)} x{count}");
		}

		public bool HasItem(string id){
			return _inventory.ContainsKey(id);
		}

		public bool IsEnoughMoney(int spendCount){
			bool enoughMoney = false;
			
			if (_inventory.ContainsKey(_moneyId)){
				var endValue = _inventory[_moneyId] - spendCount;
				enoughMoney = endValue>=0;
				_inventory[_moneyId] = enoughMoney ? _inventory[_moneyId] - spendCount : _inventory[_moneyId];
			}

			return enoughMoney;
		}
	}
}