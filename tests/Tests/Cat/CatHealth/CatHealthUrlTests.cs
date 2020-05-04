// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.Cat.CatHealth
{
	public class CatHealthUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() => await GET("/_cat/health")
			.Fluent(c => c.Cat.Health())
			.Request(c => c.Cat.Health(new CatHealthRequest()))
			.FluentAsync(c => c.Cat.HealthAsync())
			.RequestAsync(c => c.Cat.HealthAsync(new CatHealthRequest()));
	}
}
