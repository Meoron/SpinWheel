using System;
using System.Collections.Generic;
using System.Linq;
using Sources.Example;
using Sources.UI.Wheel.Interfaces;
using Random = UnityEngine.Random;

namespace Sources.UI.Wheel{
	public sealed class WheelModel : IWheelModel{
		private WheelConfig _config;
		private Inventory _inventory;
		private (string, int) _itemForDrop; //Item1: Id, Item2: count; 
		private (string, int) _defaultItem; //Item1: Id, Item2: count; 
		private List<(string, int)> _currentItems = new List<(string, int)>();
		private bool _canSpin;
		private int _freeSpin;

		public bool CanSpin => _canSpin;
		public List<(string, int)> CurrentItemList => _currentItems;

		public WheelModel(WheelConfig wheelConfig, Inventory inventory){
			_config = wheelConfig;
			_inventory = inventory;
		}

		public void Init(){
			_freeSpin = _config.FreeSpins;
			_canSpin = IsCanSpin();
			_defaultItem = (_config.DefaultItem.Id, _config.DefaultItem.Count);
			
			AddItemsOnCurrentList(_config.OnceItems, _config.OnceItemsOnWheel, true);
			AddItemsOnCurrentList(_config.BonusItems, _config.BonusItemsOnWheel, false);
			AddItemsOnCurrentList(_config.MustItems, _config.MustItems.Count, false);
		}

		public int SpinToRandomCell(){
			var itemId = GetItemForDrop();
			var count = GetItemCount(itemId);
			_itemForDrop = (itemId, count);
			_freeSpin = _freeSpin > 0 ? _freeSpin - 1 : 0;

			return _currentItems.IndexOf((itemId, count));
		}

		public (string, int) DropItem(){
			var itemForDrop = _itemForDrop;
			_itemForDrop = (null, 0);

			_inventory.AddItem(itemForDrop.Item1, itemForDrop.Item2);
			UpdateItem(itemForDrop);
			_canSpin = IsCanSpin();

			return itemForDrop;
		}

		public bool IsPlayerHaveItem(string id){
			return _inventory.HasItem(id);
		}


		private bool IsCanSpin(){
			if (_freeSpin > 0){
				return true;
			}

			return _inventory.IsEnoughMoney(_config.MoneyOnSpin);
		}

		private void AddItemsOnCurrentList(List<ItemForWheel> items, int countItems, bool isOnceItemsList){
			for (int i = 0; i < countItems; i++){
				_currentItems.Add(GetRandomItem(items, _defaultItem, isOnceItemsList));
			}
		}

		private void UpdateItem((string, int) addedItem){
			var isOnceItemsList = _config.OnceItems.FirstOrDefault(x => x.Id == addedItem.Item1) != null;
			var itemsList = isOnceItemsList ? _config.OnceItems : _config.BonusItems;
				itemsList = _config.ItemContains(_config.MustItems, addedItem) ? _config.MustItems : itemsList;
			var newItem = GetRandomItem(itemsList, _defaultItem, isOnceItemsList);
			var index = _currentItems.IndexOf(addedItem);
			
			_currentItems.Insert(index, newItem);
			_currentItems.Remove(addedItem);
		}

		private (string, int) GetRandomItem(List<ItemForWheel> items, (string, int) defaultItem, bool isOnceItemsList){
			var itemIndex = Random.Range(0, items.Count);

			if (isOnceItemsList){
				var index = 0;
				while (IsPlayerHaveItem(items[itemIndex].Id)){
					itemIndex = index;

					if (index < items.Count){
						index++;
					}
					else{
						itemIndex = -1;
						break;
					}
				}
			}

			return itemIndex != -1 ? (items[itemIndex].Id, items[itemIndex].Count) : defaultItem;
		}

		private string GetItemForDrop(){
			float cumulativeChance = 0f;
			string itemId = null;
			float maxChanceValue = 0;

			for (int i = 0; i < _currentItems.Count; i++){
				maxChanceValue += _config.GetChance(_currentItems[i].Item1);
			}
			
			do{
				float randomChance = Random.Range(0f, maxChanceValue);

				for (int i = 0; i < _currentItems.Count; i++){
					cumulativeChance += _config.GetChance(_currentItems[i].Item1);
					if (randomChance <= cumulativeChance){
						itemId = _currentItems[i].Item1;
						break;
					}
				}
				
			} while (itemId == null);

			return itemId;
		}

		private int GetItemCount(string id){
			return _config.GetCount(id);
		}
	}
}