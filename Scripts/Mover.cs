using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface Mover
{

	List<TileAttributes> GetPossibleMoves();

	void Move (TileAttributes t);

	void Reset();
}
