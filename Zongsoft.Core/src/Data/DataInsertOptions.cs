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
 * This file is part of Zongsoft.Core library.
 *
 * The Zongsoft.Core is free software: you can redistribute it and/or modify
 * it under the terms of the GNU Lesser General Public License as published by
 * the Free Software Foundation, either version 3.0 of the License,
 * or (at your option) any later version.
 *
 * The Zongsoft.Core is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
 * GNU Lesser General Public License for more details.
 *
 * You should have received a copy of the GNU Lesser General Public License
 * along with the Zongsoft.Core library. If not, see <http://www.gnu.org/licenses/>.
 */

using System;
using System.Collections.Generic;

namespace Zongsoft.Data
{
	/// <summary>
	/// 表示数据新增操作选项的接口。
	/// </summary>
	public interface IDataInsertOptions : IDataMutateOptions
	{
		/// <summary>获取或设置一个值，指示是否强制应用新增序号器来生成序号值，默认不强制。</summary>
		bool SequenceSuppressed { get; set; }
	}

	/// <summary>
	/// 表示数据新增操作选项的类。
	/// </summary>
	public class DataInsertOptions : DataMutateOptions, IDataInsertOptions
	{
		#region 构造函数
		public DataInsertOptions() { }
		public DataInsertOptions(in Collections.Parameters parameters) : base(parameters) { }
		public DataInsertOptions(IEnumerable<KeyValuePair<string, object>> parameters) : base(parameters) { }
		#endregion

		#region 公共属性
		/// <inheritdoc />
		public bool SequenceSuppressed { get; set; }
		#endregion

		#region 静态方法
		/// <summary>创建一个禁用序号器的新增选项构建器。</summary>
		/// <returns>返回创建的<see cref="Builder"/>构建器对象。</returns>
		public static Builder SuppressSequence() => new() { SequenceSuppressed = true };

		/// <summary>创建一个禁用数据验证器的新增选项构建器。</summary>
		/// <returns>返回创建的<see cref="Builder"/>构建器对象。</returns>
		public static Builder SuppressValidator() => new() { ValidatorSuppressed = true };
		#endregion

		#region 嵌套子类
		public class Builder : DataMutateOptionsBuilder<DataInsertOptions>
		{
			#region 构造函数
			public Builder() { }
			#endregion

			#region 公共属性
			/// <summary>获取或设置一个值，指示是否强制应用新增序号器来生成序号值，默认不强制。</summary>
			public bool SequenceSuppressed { get; set; }
			#endregion

			#region 设置方法
			public Builder Parameter(string name, object value = null) { this.Parameters.SetValue(name, value); return this; }
			public Builder SuppressSequence() { this.SequenceSuppressed = true; return this; }
			public Builder UnsuppressSequence() { this.SequenceSuppressed = false; return this; }
			public Builder SuppressValidator() { this.ValidatorSuppressed = true; return this; }
			public Builder UnsuppressValidator() { this.ValidatorSuppressed = false; return this; }
			#endregion

			#region 构建方法
			public override DataInsertOptions Build() => new DataInsertOptions(this.Parameters)
			{
				SequenceSuppressed = this.SequenceSuppressed,
				ValidatorSuppressed = this.ValidatorSuppressed,
			};
			#endregion

			#region 类型转换
			public static implicit operator DataInsertOptions(Builder builder) => builder.Build();
			#endregion
		}
		#endregion
	}
}
