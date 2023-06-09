using System;
using System.Collections.Generic;
using SMLHelper.V2.Assets;
using SMLHelper.V2.Utility;
using UnityEngine;

namespace AlienRifle2
{
	// Token: 0x02000004 RID: 4
	public class RiflePrefab : ModPrefab
	{
		// Token: 0x06000005 RID: 5 RVA: 0x0000227E File Offset: 0x0000047E
		public RiflePrefab(string classId, string prefabFileName, TechType techType = 0) : base(classId, prefabFileName, techType)
		{
		}

		// Token: 0x06000006 RID: 6 RVA: 0x0000228C File Offset: 0x0000048C
		public override GameObject GetGameObject()
		{
			GameObject gameObject = MainPatch.bundle.LoadAsset<GameObject>("Assets/AlienRifle/Alien Rifle.prefab");
			PrefabUtils.GetOrAddComponent<PrefabIdentifier>(gameObject).ClassId = base.ClassID;
			PrefabUtils.GetOrAddComponent<LargeWorldEntity>(gameObject).cellLevel = 0;
			PrefabUtils.GetOrAddComponent<TechTag>(gameObject).type = base.TechType;
			PrefabUtils.GetOrAddComponent<Pickupable>(gameObject).isPickupable = true;
			SkyApplier orAddComponent = PrefabUtils.GetOrAddComponent<SkyApplier>(gameObject);
			SkyApplier skyApplier = orAddComponent;
			Renderer[] componentsInChildren = gameObject.GetComponentsInChildren<MeshRenderer>();
			skyApplier.renderers = componentsInChildren;
			orAddComponent.anchorSky = 0;
			GameObject gameObject2 = Resources.Load<GameObject>("WorldEntities/Doodads/Precursor/Prison/Relics/Alien_relic_07");
			Material material = gameObject2.GetComponentInChildren<MeshRenderer>().material;
			orAddComponent.renderers[0].materials = new Material[]
			{
				material,
				material
			};
			orAddComponent.renderers[0].GetComponent<MeshFilter>().mesh = gameObject2.GetComponentInChildren<MeshFilter>().mesh;
			VFXFabricating orAddComponent2 = PrefabUtils.GetOrAddComponent<VFXFabricating>(gameObject.transform.Find("RifleMesh").gameObject);
			orAddComponent2.localMinY = -0.4f;
			orAddComponent2.localMaxY = 0.2f;
			orAddComponent2.posOffset = new Vector3(-0.054f, 0.1f, -0.06f);
			orAddComponent2.eulerOffset = new Vector3(0f, 0f, 90f);
			orAddComponent2.scaleFactor = 1f;
			WorldForces orAddComponent3 = PrefabUtils.GetOrAddComponent<WorldForces>(gameObject);
			orAddComponent3.underwaterGravity = 0f;
			orAddComponent3.useRigidbody = PrefabUtils.GetOrAddComponent<Rigidbody>(gameObject);
			orAddComponent3.useRigidbody.useGravity = false;
			RifleTool orAddComponent4 = PrefabUtils.GetOrAddComponent<RifleTool>(gameObject);
			orAddComponent4.mainCollider = gameObject.GetComponentInChildren<Collider>();
			orAddComponent4.ikAimRightArm = true;
			orAddComponent4.ikAimLeftArm = true;
			orAddComponent4.useLeftAimTargetOnPlayer = true;
			orAddComponent4.chargeEffect = orAddComponent4.transform.Find("chargeparticles").GetComponent<ParticleSystem>();
			orAddComponent4.shootEffect = orAddComponent4.transform.Find("shooteffect").GetComponent<ParticleSystem>();
			orAddComponent4.chargeMeter = gameObject.transform.Find("HUD/ChargeBar");
			orAddComponent4.bulletPrefab = gameObject.transform.Find("BulletPrefab").gameObject;
			orAddComponent4.energyMixin = PrefabUtils.GetOrAddComponent<EnergyMixin>(gameObject);
			orAddComponent4.energyMixin.allowBatteryReplacement = true;
			orAddComponent4.energyMixin.compatibleBatteries = new List<TechType>
			{
				4210
			};
			orAddComponent4.energyMixin.defaultBattery = 0;
			EnergyMixin energyMixin = orAddComponent4.energyMixin;
			EnergyMixin.BatteryModels[] array = new EnergyMixin.BatteryModels[1];
			int num = 0;
			EnergyMixin.BatteryModels batteryModels = default(EnergyMixin.BatteryModels);
			batteryModels.model = gameObject.transform.Find("Battery").gameObject;
			batteryModels.techType = 4210;
			array[num] = batteryModels;
			energyMixin.batteryModels = array;
			orAddComponent4.energyMixin.storageRoot = PrefabUtils.GetOrAddComponent<ChildObjectIdentifier>(gameObject.transform.Find("Battery").gameObject);
			GameObject prefabForTechType = CraftData.GetPrefabForTechType(2000, true);
			orAddComponent4.sonarSound = PrefabUtils.GetOrAddComponent<FMOD_CustomEmitter>(gameObject);
			orAddComponent4.sonarSound.asset = prefabForTechType.GetComponent<SeaMoth>().sonarSound.asset;
			orAddComponent4.sonarSound.playOnAwake = false;
			orAddComponent4.sonarSound.followParent = true;
			orAddComponent4.sonarSound.restartOnPlay = true;
			orAddComponent4.shootSound = CraftData.GetPrefabForTechType(807, true).GetComponent<RepulsionCannon>().shootSound;
			orAddComponent4.chargeSound = PrefabUtils.GetOrAddComponent<FMOD_CustomLoopingEmitter>(gameObject);
			orAddComponent4.chargeSound.asset = prefabForTechType.GetComponent<SeaMoth>().pulseChargeSound.asset;
			orAddComponent4.chargeSound.followParent = true;
			orAddComponent4.sonarSound.restartOnPlay = true;
			orAddComponent4.Awake();
			return gameObject;
		}
	}
}
