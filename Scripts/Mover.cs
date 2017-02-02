using UnityEngine;
using System.Collections;

public interface Mover
{

	TileAttributes[] GetPossibleMoves();

	void Reset();
}
