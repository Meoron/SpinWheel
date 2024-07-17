using System.Linq;
using UnityEngine;

namespace Sources.Example{
	public static class ItemDatabaseService{
		private static ItemDatabase _itemDatabase;
		
		static ItemDatabaseService(){
			if (_itemDatabase == null){
				_itemDatabase = Resources.Load<ItemDatabase>("WheelOfLuck/Configs/ItemDatabase");
			}
		}
		
		public static Sprite GetItemIcon(string itemId){
			return _itemDatabase.Items.FirstOrDefault(x => x.ID == itemId).Icon;
		}

		public static string GetItemName(string itemId){
			return _itemDatabase.Items.FirstOrDefault(x => x.ID == itemId).Name;
		}
	}
}