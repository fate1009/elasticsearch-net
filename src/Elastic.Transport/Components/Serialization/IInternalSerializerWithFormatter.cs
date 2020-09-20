// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Transport.Utf8Json;

namespace Elastic.Transport.Serialization
{
	public interface IInternalSerializer
	{
		bool TryGetJsonFormatter(out IJsonFormatterResolver formatterResolver);
	}

}