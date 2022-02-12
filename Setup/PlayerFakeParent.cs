using UnityEngine;

namespace GbHapticsIntegration.Setup
{
	public class PlayerFakeParent : MonoBehaviour
	{
		private void Update()
		{
			if (fakeParentTo == null)
				return;
			transform.position = fakeParentTo.position;
			transform.eulerAngles = new Vector3(0, fakeParentTo.eulerAngles.y, 0);
		}

		public Transform fakeParentTo;
	}
}
