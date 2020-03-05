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

using System;
using System.Collections.Generic;
using System.Linq;
using log4net;
using MiNET.Plugins;

namespace MiNET.Net
{
	public partial class McpeAvailableCommands
	{
		private static readonly ILog Log = LogManager.GetLogger(typeof(McpeAvailableCommands));

		public CommandSet CommandSet { get; set; }

		partial void AfterDecode()
		{
			var stringValues = new List<string>();
			{
				uint count = ReadUnsignedVarInt();
				Log.Debug($"String values {count}");
				for (int i = 0; i < count; i++)
				{
					string str = ReadString();
					Log.Debug($"{i} - {str}");
					stringValues.Add(str);
				}
			}
			int stringValuesCount = stringValues.Count();

			{
				uint count = ReadUnsignedVarInt();
				Log.Debug($"Postfix values {count}");
				for (int i = 0; i < count; i++)
				{
					string s = ReadString();
					Log.Debug(s);
				}
			}

			{
				uint count = ReadUnsignedVarInt();
				Log.Debug($"Enum indexes {count}");

				string last = null;

				string clazzType = null;
				for (int i = 0; i < count; i++)
				{
					string enumName = ReadString();
					uint enumValueCount = ReadUnsignedVarInt();
					Log.Debug($"{i} - {enumName}:{enumValueCount}");
					for (int j = 0; j < enumValueCount; j++)
					{
						int idx;
						if (stringValuesCount <= byte.MaxValue)
						{
							idx = ReadByte();
						}
						else if (stringValuesCount <= short.MaxValue)
						{
							idx = ReadShort();
						}
						else
						{
							idx = ReadInt();
						}

						Log.Debug($"{enumName}, {idx} - {stringValues[idx]}");
					}
				}
			}

			{
				uint count = ReadUnsignedVarInt();
				Log.Debug($"Commands definitions {count}");
				for (int i = 0; i < count; i++)
				{
					string commandName = ReadString();
					string description = ReadString();
					int flags = ReadByte();
					int permissions = ReadByte();

					int aliasEnumIndex = ReadInt();

					uint overloadCount = ReadUnsignedVarInt();
					for (int j = 0; j < overloadCount; j++)
					{
						uint parameterCount = ReadUnsignedVarInt();
						Log.Debug($"{commandName}, {description}, flags={flags}, {((CommandPermission) permissions)}, alias={aliasEnumIndex}, overloads={overloadCount}, params={parameterCount}");
						for (int k = 0; k < parameterCount; k++)
						{
							string commandParamName = ReadString();
							int tmp = ReadShort();
							int tmp1 = ReadShort();
							bool isEnum = (tmp1 & 0x30) == 0x30;
							bool isSoftEnum = (tmp1 & 0x0410) == 0x0410;
							int commandParamType = -1;
							int commandParamEnumIndex = -1;
							int commandParamSoftEnumIndex = -1;
							int commandParamPostfixIndex = -1;
							if ((tmp1 & 0x30) == 0x30)
							{
								commandParamEnumIndex = tmp & 0xffff;
							}
							else if ((tmp1 & 0x100) == 0x100)
							{
								commandParamPostfixIndex = tmp & 0xffff;
							}
							else if ((tmp1 & 0x10) == 0x10)
							{
								commandParamType = tmp & 0xffff;
							}
							else if ((tmp1 & 0x0410) == 0x0410)
							{
								commandParamType = tmp & 0xffff;
							}
							else
							{
								Log.Warn("No parameter style read (enum, valid, postfix)");
							}

							bool optional = ReadBool();
							byte unknown = ReadByte();
							Log.Debug($"\t{commandParamName}, 0x{tmp:X4}, 0x{tmp1:X4}, {isEnum}, {isSoftEnum}, {(GetParameterTypeName(commandParamType))}, {commandParamEnumIndex}, {commandParamSoftEnumIndex}, {commandParamPostfixIndex}, {optional}, {unknown}");
						}
					}
				}
			}
			{
				// Soft enums?

				uint count = ReadUnsignedVarInt();
				Log.Debug($"Soft enums {count}");
				for (int i = 0; i < count; i++)
				{
					Log.Debug($"Soft Enum: {ReadString()}={ReadBool()}");
				}
			}

			{
				// constraints
				uint count = ReadUnsignedVarInt();
				Log.Debug($"Constraints {count}");
				for (int i = 0; i < count; i++)
				{
					Log.Debug($"Constraint: {ReadInt()} _ {ReadInt()}");
					uint someCount = ReadUnsignedVarInt();
					for (int j = 0; j < someCount; j++)
					{
						Log.Debug($"\tUnknown byte: {ReadByte()}");
					}
				}
			}
		}

		partial void AfterEncode()
		{
			try
			{
				if (CommandSet == null || CommandSet.Count == 0)
				{
					Log.Warn("No commands to send");
					WriteUnsignedVarInt(0);
					WriteUnsignedVarInt(0);
					WriteUnsignedVarInt(0);
					WriteUnsignedVarInt(0);
					WriteUnsignedVarInt(0);
					WriteUnsignedVarInt(0);
					return;
				}

				var commands = CommandSet;

				List<string> stringList = new List<string>();
				{
					foreach (var command in commands.Values)
					{
						var aliases = command.Versions[0].Aliases.Concat(new string[] {command.Name}).ToArray();
						foreach (var alias in aliases)
						{
							if (!stringList.Contains(alias))
							{
								stringList.Add(alias);
							}
						}

						var overloads = command.Versions[0].Overloads;
						foreach (var overload in overloads.Values)
						{
							var parameters = overload.Input.Parameters;
							if (parameters == null) continue;
							foreach (var parameter in parameters)
							{
								if (parameter.Type == "stringenum")
								{
									if (parameter.EnumValues == null) continue;
									foreach (var enumValue in parameter.EnumValues)
									{
										if (!stringList.Contains(enumValue))
										{
											stringList.Add(enumValue);
										}
									}
								}
							}
						}
					}

					WriteUnsignedVarInt((uint) stringList.Count); // Enum values
					foreach (var s in stringList)
					{
						Write(s);
						//Log.Debug($"String: {s}, {(short) stringList.IndexOf(s)} ");
					}
				}

				WriteUnsignedVarInt(0); // Postfixes

				List<string> enumList = new List<string>();
				foreach (var command in commands.Values)
				{
					if (command.Versions[0].Aliases.Length > 0)
					{
						string aliasEnum = command.Name + "CommandAliases";
						if (!enumList.Contains(aliasEnum))
						{
							enumList.Add(aliasEnum);
						}
					}

					var overloads = command.Versions[0].Overloads;
					foreach (var overload in overloads.Values)
					{
						var parameters = overload.Input.Parameters;
						if (parameters == null) continue;
						foreach (var parameter in parameters)
						{
							if (parameter.Type == "stringenum")
							{
								if (parameter.EnumValues == null) continue;

								if (!enumList.Contains(parameter.EnumType))
								{
									enumList.Add(parameter.EnumType);
								}
							}
						}
					}
				}

				//WriteUnsignedVarInt(0); // Enum indexes
				WriteUnsignedVarInt((uint) enumList.Count); // Enum indexes
				List<string> writtenEnumList = new List<string>();
				foreach (var command in commands.Values)
				{
					if (command.Versions[0].Aliases.Length > 0)
					{
						var aliases = command.Versions[0].Aliases.Concat(new string[] {command.Name}).ToArray();
						string aliasEnum = command.Name + "CommandAliases";
						if (!enumList.Contains(aliasEnum)) continue;
						if (writtenEnumList.Contains(aliasEnum)) continue;

						Write(aliasEnum);
						WriteUnsignedVarInt((uint) aliases.Length);
						foreach (var enumValue in aliases)
						{
							if (!stringList.Contains(enumValue)) Log.Error($"Expected enum value: {enumValue} in string list, but didn't find it.");
							if (stringList.Count <= byte.MaxValue)
							{
								Write((byte) stringList.IndexOf(enumValue));
							}
							else if (stringList.Count <= short.MaxValue)
							{
								Write((short) stringList.IndexOf(enumValue));
							}
							else
							{
								Write((int) stringList.IndexOf(enumValue));
							}

							//Log.Debug($"EnumType: {aliasEnum}, {enumValue}, {stringList.IndexOf(enumValue)} ");
						}
					}

					var overloads = command.Versions[0].Overloads;
					foreach (var overload in overloads.Values)
					{
						var parameters = overload.Input.Parameters;
						if (parameters == null) continue;
						foreach (var parameter in parameters)
						{
							if (parameter.Type == "stringenum")
							{
								if (parameter.EnumValues == null) continue;

								if (!enumList.Contains(parameter.EnumType)) continue;
								if (writtenEnumList.Contains(parameter.EnumType)) continue;

								writtenEnumList.Add(parameter.EnumType);

								Write(parameter.EnumType);
								WriteUnsignedVarInt((uint) parameter.EnumValues.Length);
								foreach (var enumValue in parameter.EnumValues)
								{
									if (!stringList.Contains(enumValue)) Log.Error($"Expected enum value: {enumValue} in string list, but didn't find it.");
									if (stringList.Count <= byte.MaxValue)
									{
										Write((byte) stringList.IndexOf(enumValue));
									}
									else if (stringList.Count <= short.MaxValue)
									{
										Write((short) stringList.IndexOf(enumValue));
									}
									else
									{
										Write((int) stringList.IndexOf(enumValue));
									}

									//Log.Debug($"EnumType: {parameter.EnumType}, {enumValue}, {stringList.IndexOf(enumValue)} ");
								}
							}
						}
					}
				}

				WriteUnsignedVarInt((uint) commands.Count);
				foreach (var command in commands.Values)
				{
					Write(command.Name);
					Write(command.Versions[0].Description);
					Write((byte) 0); // flags
					Write((byte) command.Versions[0].CommandPermission); // permissions

					if (command.Versions[0].Aliases.Length > 0)
					{
						string aliasEnum = command.Name + "CommandAliases";
						Write((int) enumList.IndexOf(aliasEnum));
					}
					else
					{
						Write((int) -1); // Enum index
					}


					//Log.Warn($"Writing command {command.Name}");

					var overloads = command.Versions[0].Overloads;
					WriteUnsignedVarInt((uint) overloads.Count); // Overloads
					foreach (var overload in overloads.Values)
					{
						//Log.Warn($"Writing command: {command.Name}");

						var parameters = overload.Input.Parameters;
						if (parameters == null)
						{
							WriteUnsignedVarInt(0); // Parameter count
							continue;
						}

						WriteUnsignedVarInt((uint) parameters.Length); // Parameter count
						foreach (var parameter in parameters)
						{
							//Log.Debug($"Writing command overload parameter {command.Name}, {parameter.Name}, {parameter.Type}");

							Write(parameter.Name); // parameter name
							if (parameter.Type == "stringenum" && parameter.EnumValues != null)
							{
								Write((short) enumList.IndexOf(parameter.EnumType));
								Write((short) 0x30);
							}
							else
							{
								Write((short) GetParameterTypeId(parameter.Type)); // param type
								Write((short) 0x10);
							}

							Write(parameter.Optional); // optional
							Write((byte) 0); // unknown
						}
					}
				}

				WriteUnsignedVarInt(0); //TODO: soft enums
				WriteUnsignedVarInt(0); //TODO: constraints
			}
			catch (Exception e)
			{
				Log.Error("Sending commands", e);
				//throw;
			}
		}

		private int GetParameterTypeId(string type)
		{
			return type switch
			{
				"enum" => -1,
				"unknown" => 0,
				"int" => 1,
				"float" => 2,
				"value" => 3,
				"wildcardint" => 4,
				"operator" => 5,
				"target" => 6,
				"string" => 29,
				"blockpos" => 37,
				"entitypos" => 38,
				"message" => 41,
				"rawtext" => 43,
				"json" => 47,
				"command" => 54,
				_ => 0
			};
		}

		private string GetParameterTypeName(int type)
		{
			return type switch
			{
				-1 => "enum",
				0 => "unknown",
				1 => "int",
				2 => "float",
				3 => "value",
				4 => "wildcardint",
				5 => "operator",
				6 => "target",
				14 => "filename",
				29 => "string",
				37 => "blockpos",
				38 => "entitypos",
				41 => "message", // kick, me, etc
				43 => "rawtext", // kick, me, etc
				47 => "son", // give, replace
				54 => "command",
				_ => $"undefined({type})"
			};
		}
	}
}