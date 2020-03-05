﻿#region LICENSE

// The contents of this file are subject to the Common Public Attribution
// License Version 1.0. (the "License"); you may not use this file except in
// compliance with the License. You may obtain a copy of the License at
// https://github.com/NiclasOlofsson/MiNET/blob/master/LICENSE.
// The License is based on the Mozilla Public License Version 1.1, but Sections 14
// and 15 have been added to cover use of software over a computer network and
// provide for limited attribution for the Original Developer. In addition, Exhibit A has
// been modified to be consistent with Exhibit B.
// 
// Software distributed under the License is distributed on an "AS IS" basis,
// WITHOUT WARRANTY OF ANY KIND, either express or implied. See the License for
// the specific language governing rights and limitations under the License.
// 
// The Original Code is MiNET.
// 
// The Original Developer is the Initial Developer.  The Initial Developer of
// the Original Code is Niclas Olofsson.
// 
// All portions of the code written by Niclas Olofsson are Copyright (c) 2014-2019 Niclas Olofsson.
// All Rights Reserved.

#endregion

namespace MiNET
{
	public enum LevelEventType : short
	{
		SoundClick = 1000,
		SoundClickFail = 1001, // try also with data 1200 for Dispenser sound
		SoundShoot = 1002,
		SoundDoorClose = 1003, // spooky sound with data 20
		SoundFizz = 1004,
		SoundTNTFuse = 1005,

		SoundGhast = 1007,
		SoundBlazeFireball = 1008, // same as the one below
		SoundGhastFireball = 1009,
		SoundZombieDoorHit = 1010,
		SoundZombieBreakDoor = 1012,

		SoundBatFly = 1015,
		SoundZombieInfect = 1016,
		SoundZombieHeal = 1017,
		SoundEndermanTeleport = 1018,

		SoundAnvilBreak = 1020,
		SoundAnvilUse = 1021,
		SoundAnvilFall = 1022,

		SoundItemDrop = 1030,
		SoundItemThrown = 1031,

		SoundItemFrameItemAdded = 1040,
		SoundItemFramePlaced = 1041,
		SoundItemFrameRemoved = 1042,
		SoundItemFrameItemRemoved = 1043,
		SoundItemFrameItemRotated = 1044, // Not sure if the names are correct

		SoundCameraTakePicture = 1050,
		SoundExpOrb = 1051,

		SoundButtonClick = 3500,

		ParticleShoot = 2000,
		ParticleDestroy = 2001,
		ParticleSplash = 2002,
		ParticleEyeDespawn = 2003,
		ParticleSpawn = 2004,
		ParticleGreenThingy = 2005,
		GuardianCurse = 2006,
		ParticleBlockForceField = 2008,
		ParticleProjectileHit = 2009,
		ParticleEndermanTeleport = 2013,
		ParticlePunchBlock = 2014,

		StartRain = 3001,
		StartThunder = 3002,
		StopRain = 3003,
		StopThunder = 3004,

		DankMemes = 3500,
		CauldronExplode = 3501, // no idea what it is or why it is named like that
		CauldronDyeArmor = 3502,
		CauldronFillPotion = 3504,
		CauldronFillWater = 3506,

		SetData = 4000,

		PlayersSleeping = 9800,
	}

	public enum LevelSoundEventType
	{
		ItemUseOn = 0,
		Hit = 1,
		Step = 2,
		Fly = 3,
		Jump = 4,
		Break = 5,
		Place = 6,
		HeavyStep = 7,
		Gallop = 8,
		Fall = 9,
		Ambient = 10,
		AmbientBaby = 11,
		AmbientInWater = 12,
		Breathe = 13,
		Death = 14,
		DeathInWater = 15,
		DeathToZombie = 16,
		Hurt = 17,
		HurtInWater = 18,
		Mad = 19,
		Boost = 20,
		Bow = 21,
		SquishBig = 22,
		SquishSmall = 23,
		FallBig = 24,
		FallSmall = 25,
		Splash = 26,
		Fizz = 27,
		Flap = 28,
		Swim = 29,
		Drink = 30,
		Eat = 31,
		Takeoff = 32,
		Shake = 33,
		Plop = 34,
		Land = 35,
		Saddle = 36,
		Armor = 37,
		MobArmorStandPlace = 38,
		AddChest = 39,
		Throw = 40,
		Attack = 41,
		AttackNodamage = 42,
		AttackStrong = 43,
		Warn = 44,
		Shear = 45,
		Milk = 46,
		Thunder = 47,
		Explode = 48,
		Fire = 49,
		Ignite = 50,
		Fuse = 51,
		Stare = 52,
		Spawn = 53,
		Shoot = 54,
		BreakBlock = 55,
		Launch = 56,
		Blast = 57,
		LargeBlast = 58,
		Twinkle = 59,
		Remedy = 60,
		Unfect = 61,
		Levelup = 62,
		BowHit = 63,
		BulletHit = 64,
		ExtinguishFire = 65,
		ItemFizz = 66,
		ChestOpen = 67,
		ChestClosed = 68,
		ShulkerboxOpen = 69,
		ShulkerboxClosed = 70,
		EnderchestOpen = 71,
		EnderchestClosed = 72,
		PowerOn = 73,
		PowerOff = 74,
		Attach = 75,
		Detach = 76,
		Deny = 77,
		Tripod = 78,
		Pop = 79,
		DropSlot = 80,
		Note = 81,
		Thorns = 82,
		PistonIn = 83,
		PistonOut = 84,
		Portal = 85,
		Water = 86,
		LavaPop = 87,
		Lava = 88,
		Burp = 89,
		BucketFillWater = 90,
		BucketFillLava = 91,
		BucketEmptyWater = 92,
		BucketEmptyLava = 93,
		ArmorEquipChain = 94,
		ArmorEquipDiamond = 95,
		ArmorEquipGeneric = 96,
		ArmorEquipGold = 97,
		ArmorEquipIron = 98,
		ArmorEquipLeather = 99,
		ArmorEquipElytra = 100,
		Record13 = 101,
		RecordCat = 102,
		RecordBlocks = 103,
		RecordChirp = 104,
		RecordFar = 105,
		RecordMall = 106,
		RecordMellohi = 107,
		RecordStal = 108,
		RecordStrad = 109,
		RecordWard = 110,
		Record11 = 111,
		RecordWait = 112,

		Flop = 114,
		ElderguardianCurse = 115,
		MobWarning = 116,
		MobWarningBaby = 117,
		Teleport = 118,
		ShulkerOpen = 119,
		ShulkerClose = 120,
		Haggle = 121,
		HaggleYes = 122,
		HaggleNo = 123,
		HaggleIdle = 124,
		Chorusgrow = 125,
		Chorusdeath = 126,
		Glass = 127,
		PotionBrewed = 128,
		CastSpell = 129,
		PrepareAttack = 130,
		PrepareSummon = 131,
		PrepareWololo = 132,
		Fang = 133,
		Charge = 134,
		CameraTakePicture = 135,
		LeashknotPlace = 136,
		LeashknotBreak = 137,
		Growl = 138,
		Whine = 139,
		Pant = 140,
		Purr = 141,
		Purreow = 142,
		DeathMinVolume = 143,
		DeathMidVolume = 144,

		ImitateCaveSpider = 146,
		ImitateCreeper = 147,
		ImitateElderGuardian = 148,
		ImitateEnderDragon = 149,
		ImitateEnderman = 150,

		ImitateEvocationIllager = 152,
		ImitateGhast = 153,
		ImitateHusk = 154,
		ImitateIllusionIllager = 155,
		ImitateMagmaCube = 156,
		ImitatePolarBear = 157,
		ImitateShulker = 158,
		ImitateSilverfish = 159,
		ImitateSkeleton = 160,
		ImitateSlime = 161,
		ImitateSpider = 162,
		ImitateStray = 163,
		ImitateVex = 164,
		ImitateVindicationIllager = 165,
		ImitateWitch = 166,
		ImitateWither = 167,
		ImitateWitherSkeleton = 168,
		ImitateWolf = 169,
		ImitateZombie = 170,
		ImitateZombiePigman = 171,
		ImitateZombieVillager = 172,
		BlockEndPortalFrameFill = 173,
		BlockEndPortalSpawn = 174,
		RandomAnvilUse = 175,
		BottleDragonbreath = 176,
		PortalTravel = 177,
		ItemTridentHit = 178,
		ItemTridentReturn = 179,
		ItemTridentRiptide1 = 180,
		ItemTridentRiptide2 = 181,
		ItemTridentRiptide3 = 182,
		ItemTridentThrow = 183,
		ItemTridentThunder = 184,
		ItemTridentHitGround = 185,
		Default = 186,
		BlockFletchingTableUse = 187,
		ElemconstructOpen = 188,
		IcebombHit = 189,
		Balloonpop = 190,
		LtReactionIcebomb = 191,
		LtReactionBleach = 192,
		LtReactionEpaste = 193,
		LtReactionEpaste2 = 194,


		LtReactionFertilizer = 199,
		LtReactionFireball = 200,
		LtReactionMgsalt = 201,
		LtReactionMiscfire = 202,
		LtReactionFire = 203,
		LtReactionMiscexplosion = 204,
		LtReactionMiscmystical = 205,
		LtReactionMiscmystical2 = 206,
		LtReactionProduct = 207,
		SparklerUse = 208,
		GlowstickUse = 209,
		SparklerActive = 210,
		ConvertToDrowned = 211,
		BucketFillFish = 212,
		BucketEmptyFish = 213,
		BubbleUp = 214,
		BubbleDown = 215,
		BubblePop = 216,
		BubbleUpinside = 217,
		BubbleDowninside = 218,
		HurtBaby = 219,
		DeathBaby = 220,
		StepBaby = 221,

		Born = 223,
		BlockTurtleEggBreak = 224,
		BlockTurtleEggCrack = 225,
		BlockTurtleEggHatch = 226,

		BlockTurtleEggAttack = 228,
		BeaconActivate = 229,
		BeaconAmbient = 230,
		BeaconDeactivate = 231,
		BeaconPower = 232,
		ConduitActivate = 233,
		ConduitAmbient = 234,
		ConduitAttack = 235,
		ConduitDeactivate = 236,
		ConduitShort = 237,
		Swoop = 238,
		BlockBambooSaplingPlace = 239,
		Presneeze = 240,
		Sneeze = 241,
		AmbientTame = 242,
		Scared = 243,
		BlockScaffoldingClimb = 244,
		CrossbowLoadingStart = 245,
		CrossbowLoadingMiddle = 246,
		CrossbowLoadingEnd = 247,
		CrossbowShoot = 248,
		CrossbowQuickChargeStart = 249,
		CrossbowQuickChargeMiddle = 250,
		CrossbowQuickChargeEnd = 251,
		AmbientAggressive = 252,
		AmbientWorried = 253,
		CantBreed = 254,
		ItemShieldBlock = 255,
		ItemBookPut = 256,
		BlockGrindstoneUse = 257,
		BlockBellHit = 258,
		BlockCampfireCrackle = 259,
		Roar = 260,
		Stun = 261,
		BlockSweetBerryBushHurt = 262,
		BlockSweetBerryBushPick = 263,
		UiCartographyTableTakeResult = 264,
		UiStonecutterTakeResult = 265,
		BlockComposterEmpty = 266,
		BlockComposterFill = 267,
		BlockComposterFillSuccess = 268,
		BlockComposterReady = 269,
		BlockBarrelOpen = 270,
		BlockBarrelClose = 271,
		RaidHorn = 272,
		BlockLoomUse = 273,
		Undefined = 274
	}
}