using System;
using UnityEngine;

[Serializable]
public sealed class ItemForWheel{
	[SerializeField] private string _id;
	[SerializeField] private int _count;
	[SerializeField] private float _chance;

	public string Id => _id;
	public int Count => _count;
	public float Chance => _chance;
}