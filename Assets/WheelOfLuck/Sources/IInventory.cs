namespace Sources{
	public interface IInventory{
		public void AddItem(string id, int count);
		public bool HasItem(string id);
		public bool IsEnoughMoney(int spendCount);
	}
}