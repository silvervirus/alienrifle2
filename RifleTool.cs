using System;
using UnityEngine;

namespace AlienRifle2
{
	// Token: 0x02000005 RID: 5
	public class RifleTool : PlayerTool
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000007 RID: 7 RVA: 0x00002615 File Offset: 0x00000815
		public override string animToolName
		{
			get
			{
				return "stasisrifle";
			}
		}

		// Token: 0x06000008 RID: 8 RVA: 0x0000261C File Offset: 0x0000081C
		public void Update()
		{
			bool flag = this.sonarTimer > 0f;
			if (flag)
			{
				this.sonarTimer -= Time.deltaTime;
			}
			this.chargeMeter.localScale = Vector3.Lerp(new Vector3(0f, 1f, 0.0002f), new Vector3(0.008f, 1f, 0.0002f), this.charge);
			bool flag2 = Player.main.IsInBase() || Player.main.IsInSubmarine();
			if (flag2)
			{
				this.ikAimLeftArm = false;
				this.ikAimRightArm = false;
				this.useLeftAimTargetOnPlayer = false;
			}
			else
			{
				this.ikAimLeftArm = true;
				this.ikAimRightArm = true;
				this.useLeftAimTargetOnPlayer = true;
			}
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000026DC File Offset: 0x000008DC
		public override bool OnRightHandDown()
		{
			return false;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000026F0 File Offset: 0x000008F0
		public override bool OnRightHandHeld()
		{
			bool flag = this.canShoot && this.energyMixin.charge > 0f && !Player.main.IsInBase() && !Player.main.IsInSubmarine();
			if (flag)
			{
				bool flag2 = this.charge < 1f;
				if (flag2)
				{
					this.charge += 0.02f;
					bool flag3 = !this.chargeEffect.isPlaying;
					if (flag3)
					{
						this.chargeEffect.Play();
					}
					MainCameraControl.main.ShakeCamera(0.2f, 0.5f, 2, 2f);
					this.chargeSound.Play();
				}
				else
				{
					this.charge = 0f;
					this.canShoot = false;
					Player.main.armsController.animator.SetBool("using_tool", false);
					this.chargeEffect.Stop(true, 1);
					this.shootEffect.Play();
					this.energyMixin.ConsumeEnergy(50f);
					this.chargeSound.Stop();
					Utils.PlayFMODAsset(this.shootSound, base.transform, 20f);
					GameObject gameObject = Object.Instantiate<GameObject>(this.bulletPrefab, this.bulletPrefab.transform.position, this.bulletPrefab.transform.rotation);
					gameObject.transform.parent = null;
					gameObject.SetActive(true);
					gameObject.GetComponent<Rigidbody>().velocity = SNCameraRoot.main.GetAimingTransform().forward.normalized;
					gameObject.AddComponent<BulletControl>().rb = gameObject.GetComponent<Rigidbody>();
					Object.Destroy(gameObject, 5f);
				}
			}
			return true;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000028B7 File Offset: 0x00000AB7
		public override void OnDraw(Player p)
		{
			base.OnDraw(p);
			this.charge = 0f;
			this.chargeEffect.Stop(true, 0);
			this.shootEffect.Stop(true, 0);
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000028E9 File Offset: 0x00000AE9
		public override void OnHolster()
		{
			base.OnHolster();
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000028F4 File Offset: 0x00000AF4
		public override bool OnRightHandUp()
		{
			this.chargeEffect.Stop(true, 1);
			this.charge = 0f;
			this.canShoot = true;
			this.chargeSound.Stop();
			return true;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002934 File Offset: 0x00000B34
		public override bool OnAltDown()
		{
			bool flag = this.energyMixin.charge > 0f && this.sonarTimer <= 0f;
			if (flag)
			{
				MainCamera.camera.GetComponent<SonarScreenFX>().Ping();
				this.energyMixin.ConsumeEnergy(5f);
				this.sonarTimer = 3f;
				this.sonarSound.Play();
			}
			return true;
		}

		// Token: 0x04000004 RID: 4
		public ParticleSystem chargeEffect;

		// Token: 0x04000005 RID: 5
		public ParticleSystem shootEffect;

		// Token: 0x04000006 RID: 6
		public Transform chargeMeter;

		// Token: 0x04000007 RID: 7
		public GameObject bulletPrefab;

		// Token: 0x04000008 RID: 8
		public FMOD_CustomEmitter sonarSound;

		// Token: 0x04000009 RID: 9
		public FMODAsset shootSound;

		// Token: 0x0400000A RID: 10
		public FMOD_CustomLoopingEmitter chargeSound;

		// Token: 0x0400000B RID: 11
		private float charge = 0f;

		// Token: 0x0400000C RID: 12
		private bool canShoot = true;

		// Token: 0x0400000D RID: 13
		private float sonarTimer = 0f;
	}
}
