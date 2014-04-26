using System.Collections.Generic;

namespace game.world
{
	public class DepthMeshCollection
	{
		private List<List<string>> depthToMeshes;
		
		public DepthMeshCollection(uint numDepths)
		{
			init(numDepths);
		}
		
		private void init(uint numDepths)
		{
			depthToMeshes = new List<List<string>>();
			for (uint i = 0; i < numDepths; i++)
			{
				depthToMeshes.Add(new List<string>());
			}
		}
		
		public void addMeshIDAtDepth(string meshID, uint depth)
		{
			if (depth >= depthToMeshes.Count)
			{
				throw new Err
			}
		}
	}
}

