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
// All portions of the code written by Niclas Olofsson are Copyright (c) 2014-2020 Niclas Olofsson.
// All Rights Reserved.

#endregion

using System.Collections.Generic;
using System.Numerics;
using log4net;
using MiNET.Blocks;
using MiNET.Entities;
using MiNET.Utils;
using MiNET.Worlds;

namespace MiNET.Items
{
	public class ItemHoe : Item
	{
		private static readonly ILog Log = LogManager.GetLogger(typeof(ItemHoe));

		internal ItemHoe(short id) : base(id)
		{
			MaxStackSize = 1;
			ItemType = ItemType.Hoe;
		}

		public override void PlaceBlock(Level world, Player player, BlockCoordinates blockCoordinates, BlockFace face, Vector3 faceCoords)
		{
			Block block = world.GetBlock(blockCoordinates);
			if (block is Grass || (block is Dirt normalDirt && normalDirt.DirtType == "normal") || block is GrassPath)
			{
				var farmland = new Farmland
				{
					Coordinates = blockCoordinates,
				};

				if (farmland.FindWater(world, blockCoordinates, new List<BlockCoordinates>(), 0))
				{
					Log.Warn("Found water source");
					farmland.MoisturizedAmount = 7;
				}

				world.SetBlock(farmland);
				player.Inventory.DamageItemInHand(ItemDamageReason.BlockInteract, null, block);
			}
			else if (block is Dirt coarseDirt && coarseDirt.DirtType == "coarse")
			{
				Dirt dirt = new Dirt {Coordinates = blockCoordinates};

				world.SetBlock(dirt);
				player.Inventory.DamageItemInHand(ItemDamageReason.BlockInteract, null, block);
			}
		}

		public override bool DamageItem(Player player, ItemDamageReason reason, Entity target, Block block)
		{
			switch (reason)
			{
				case ItemDamageReason.BlockInteract:
				{
					if (block is Grass || block is Dirt || block is GrassPath)
					{
						Metadata++;
						return Metadata >= GetMaxUses() - 1;
					}
					return false;
				}
				case ItemDamageReason.EntityAttack:
				{
					Metadata++;
					return Metadata >= GetMaxUses() - 1;
				}
				default:
					return false;
			}
		}
	}
}