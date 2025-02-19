﻿/*
 *   _____                                ______
 *  /_   /  ____  ____  ____  _________  / __/ /_
 *    / /  / __ \/ __ \/ __ \/ ___/ __ \/ /_/ __/
 *   / /__/ /_/ / / / / /_/ /\_ \/ /_/ / __/ /_
 *  /____/\____/_/ /_/\__  /____/\____/_/  \__/
 *                   /____/
 *
 * Authors:
 *   钟峰(Popeye Zhong) <zongsoft@gmail.com>
 *
 * Copyright (C) 2010-2020 Zongsoft Studio <http://www.zongsoft.com>
 *
 * This file is part of Zongsoft.Data library.
 *
 * The Zongsoft.Data is free software: you can redistribute it and/or modify
 * it under the terms of the GNU Lesser General Public License as published by
 * the Free Software Foundation, either version 3.0 of the License,
 * or (at your option) any later version.
 *
 * The Zongsoft.Data is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
 * GNU Lesser General Public License for more details.
 *
 * You should have received a copy of the GNU Lesser General Public License
 * along with the Zongsoft.Data library. If not, see <http://www.gnu.org/licenses/>.
 */

using System;
using System.IO;
using System.Xml;

using Zongsoft.Collections;

namespace Zongsoft.Data.Metadata.Profiles
{
	public class MetadataFile : IDataMetadataProvider
	{
		#region 成员字段
		private readonly string _name;
		private readonly string _filePath;
		private IDataMetadataManager _manager;
		private readonly INamedCollection<IDataEntity> _entities;
		private readonly INamedCollection<IDataCommand> _commands;
		#endregion

		#region 构造函数
		public MetadataFile(string filePath, string name)
		{
			if(name != null)
				_name = name.Trim();

			_filePath = filePath;
			_entities = new NamedCollection<IDataEntity>(p => p.Name);
			_commands = new NamedCollection<IDataCommand>(p => p.Name);
		}
		#endregion

		#region 公共属性
		/// <summary>获取映射文件所属的应用名。</summary>
		public string Name { get => _name; }

		/// <summary>获取映射文件的完整路径。</summary>
		public string FilePath { get => _filePath; }

		/// <summary>获取映射文件所属的元数据管理器。</summary>
		public IDataMetadataManager Manager { get => _manager; set => _manager = value; }

		/// <summary>获取映射文件中的实体元素集。</summary>
		public INamedCollection<IDataEntity> Entities { get => _entities; }

		/// <summary>获取映射文件中的命令元素集。</summary>
		public INamedCollection<IDataCommand> Commands { get => _commands; }
		#endregion

		#region 显式实现
		IReadOnlyNamedCollection<IDataEntity> IDataMetadataContainer.Entities => (IReadOnlyNamedCollection<IDataEntity>)_entities;
		IReadOnlyNamedCollection<IDataCommand> IDataMetadataContainer.Commands => (IReadOnlyNamedCollection<IDataCommand>)_commands;
		#endregion

		#region 加载方法
		public static MetadataFile Load(string filePath, string name = null) => MetadataFileResolver.Default.Resolve(filePath, name);
		public static MetadataFile Load(Stream stream, string name = null) => MetadataFileResolver.Default.Resolve(stream, name);
		public static MetadataFile Load(TextReader reader, string name = null) => MetadataFileResolver.Default.Resolve(reader, name);
		public static MetadataFile Load(XmlReader reader, string name = null) => MetadataFileResolver.Default.Resolve(reader, name);
		#endregion

		#region 重写方法
		public override string ToString() => string.IsNullOrEmpty(_name) ? _filePath : $"{_name}({_filePath})";
		#endregion
	}
}
