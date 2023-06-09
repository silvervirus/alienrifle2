using System;
using UnityEngine;

namespace AlienRifle2
{
	// Token: 0x02000002 RID: 2
	public class BulletControl : MonoBehaviour
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public void Update()
		{
			this.rb.velocity = this.rb.velocity.normalized * 200f;
		}

		// Token: 0x06000002 RID: 2 RVA: 0x00002088 File Offset: 0x00000288
		public void OnCollisionEnter(Collision col)
		{
			LiveMixin component = col.collider.GetComponent<LiveMixin>();
			bool flag = component && component != Player.main.liveMixin;
			if (flag)
			{
				component.TakeDamage(500f, default(Vector3), 0, null);
				base.transform.GetChild(0).parent = null;
				Object.Destroy(base.gameObject);
			}
			else
			{
				BreakableResource component2 = col.collider.GetComponent<BreakableResource>();
				bool flag2 = component2;
				if (flag2)
				{
					component2.BreakIntoResources();
					base.transform.GetChild(0).parent = null;
					Object.Destroy(base.gameObject);
				}
			}
		}

		// Token: 0x04000001 RID: 1
		public Rigidbody rb;
	}
}
