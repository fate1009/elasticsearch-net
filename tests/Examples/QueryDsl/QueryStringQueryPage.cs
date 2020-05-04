// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Xunit.XunitPlumbing;
using Examples.Models;
using Nest;
using System.ComponentModel;

namespace Examples.QueryDsl
{
	public class QueryStringQueryPage : ExampleBase
	{
		[U]
		[Description("query-dsl/query-string-query.asciidoc:42")]
		public void Line42()
		{
			// tag::ad6ea0c1e46712aa1fd6d3bfa0ec979e[]
			var searchResponse = client.Search<Blog>(s => s
				.AllIndices()
				.Query(q => q
					.QueryString(qs => qs
						.Query("(new york city) OR (big apple)")
						.DefaultField(p => p.Content)
					)
				)
			);
			// end::ad6ea0c1e46712aa1fd6d3bfa0ec979e[]

			searchResponse.MatchesExample(@"GET /_search
			{
			    ""query"": {
			        ""query_string"" : {
			            ""query"" : ""(new york city) OR (big apple)"",
			            ""default_field"" : ""content""
			        }
			    }
			}");
		}

		[U]
		[Description("query-dsl/query-string-query.asciidoc:255")]
		public void Line255()
		{
			// tag::f2d68493abd3ca430bd03a7f7f8d18f9[]
			var searchResponse = client.Search<object>(s => s
				.AllIndices()
				.Query(q => q
					.QueryString(qs => qs
						.Fields(f => f
							.Field("content")
							.Field("name")
						)
						.Query("this AND that")
					)
				)
			);
			// end::f2d68493abd3ca430bd03a7f7f8d18f9[]

			searchResponse.MatchesExample(@"GET /_search
			{
			    ""query"": {
			        ""query_string"" : {
			            ""fields"" : [""content"", ""name""],
			            ""query"" : ""this AND that""
			        }
			    }
			}");
		}

		[U]
		[Description("query-dsl/query-string-query.asciidoc:271")]
		public void Line271()
		{
			// tag::e17e8852ec3f31781e1364f4dffeb6d0[]
			var searchResponse = client.Search<object>(s => s
				.AllIndices()
				.Query(q => q
					.QueryString(qs => qs
						.Query("(content:this OR name:this) AND (content:that OR name:that)")
					)
				)
			);
			// end::e17e8852ec3f31781e1364f4dffeb6d0[]

			searchResponse.MatchesExample(@"GET /_search
			{
			    ""query"": {
			        ""query_string"": {
			            ""query"": ""(content:this OR name:this) AND (content:that OR name:that)""
			        }
			    }
			}");
		}

		[U]
		[Description("query-dsl/query-string-query.asciidoc:287")]
		public void Line287()
		{
			// tag::a2a25aad1fea9a541b52ac613c78fb64[]
			var searchResponse = client.Search<object>(s => s
				.AllIndices()
				.Query(q => q
					.QueryString(qs => qs
						.Fields(f => f
							.Field("content")
							.Field("name^5")
						)
						.Query("this AND that OR thus")
						.TieBreaker(0)
					)
				)
			);
			// end::a2a25aad1fea9a541b52ac613c78fb64[]

			searchResponse.MatchesExample(@"GET /_search
			{
			    ""query"": {
			        ""query_string"" : {
			            ""fields"" : [""content"", ""name^5""],
			            ""query"" : ""this AND that OR thus"",
			            ""tie_breaker"" : 0
			        }
			    }
			}", e =>
			{
				e.ApplyBodyChanges(body =>
				{
					body["query"]["query_string"]["tie_breaker"] = 0d;
				});
			});
		}

		[U]
		[Description("query-dsl/query-string-query.asciidoc:306")]
		public void Line306()
		{
			// tag::28aad2c5942bfb221c2bf1bbdc01658e[]
			var searchResponse = client.Search<object>(s => s
				.AllIndices()
				.Query(q => q
					.QueryString(qs => qs
						.Fields(f => f
							.Field("city.*")
						)
						.Query("this AND that OR thus")
					)
				)
			);
			// end::28aad2c5942bfb221c2bf1bbdc01658e[]

			searchResponse.MatchesExample(@"GET /_search
			{
			    ""query"": {
			        ""query_string"" : {
			            ""fields"" : [""city.*""],
			            ""query"" : ""this AND that OR thus""
			        }
			    }
			}");
		}

		[U]
		[Description("query-dsl/query-string-query.asciidoc:323")]
		public void Line323()
		{
			// tag::db6cba451ba562abe953d09ad80cc15c[]
			var searchResponse = client.Search<object>(s => s
				.AllIndices()
				.Query(q => q
					.QueryString(qs => qs
						.Query("city.\\*:(this AND that OR thus)")
					)
				)
			);
			// end::db6cba451ba562abe953d09ad80cc15c[]

			searchResponse.MatchesExample(@"GET /_search
			{
			    ""query"": {
			        ""query_string"" : {
			            ""query"" : ""city.\\*:(this AND that OR thus)""
			        }
			    }
			}");
		}

		[U]
		[Description("query-dsl/query-string-query.asciidoc:342")]
		public void Line342()
		{
			// tag::58b5003c0a53a39bf509aa3797aad471[]
			var searchResponse = client.Search<object>(s => s
				.AllIndices()
				.Query(q => q
					.QueryString(qs => qs
						.Fields(f => f
							.Field("content")
							.Field("name.*^5")
						)
						.Query("this AND that OR thus")
					)
				)
			);
			// end::58b5003c0a53a39bf509aa3797aad471[]

			searchResponse.MatchesExample(@"GET /_search
			{
			    ""query"": {
			        ""query_string"" : {
			            ""fields"" : [""content"", ""name.*^5""],
			            ""query"" : ""this AND that OR thus""
			        }
			    }
			}");
		}

		[U]
		[Description("query-dsl/query-string-query.asciidoc:408")]
		public void Line408()
		{
			// tag::f32f0c19b42de3b87dd764fe4ca17e7c[]
			var searchResponse = client.Search<object>(s => s
				.AllIndices()
				.Query(q => q
					.QueryString(qs => qs
						.DefaultField("title")
						.Query("ny city")
						.AutoGenerateSynonymsPhraseQuery(false)
					)
				)
			);
			// end::f32f0c19b42de3b87dd764fe4ca17e7c[]

			searchResponse.MatchesExample(@"GET /_search
			{
			   ""query"": {
			       ""query_string"" : {
			           ""default_field"": ""title"",
			           ""query"" : ""ny city"",
			           ""auto_generate_synonyms_phrase_query"" : false
			       }
			   }
			}");
		}

		[U]
		[Description("query-dsl/query-string-query.asciidoc:436")]
		public void Line436()
		{
			// tag::60ee33f3acfdd0fe6f288ac77312c780[]
			var searchResponse = client.Search<object>(s => s
				.AllIndices()
				.Query(q => q
					.QueryString(qs => qs
						.Fields(f => f
							.Field("title")
						)
						.Query("this that thus")
						.MinimumShouldMatch(2)
					)
				)
			);
			// end::60ee33f3acfdd0fe6f288ac77312c780[]

			searchResponse.MatchesExample(@"GET /_search
			{
			    ""query"": {
			        ""query_string"": {
			            ""fields"": [
			                ""title""
			            ],
			            ""query"": ""this that thus"",
			            ""minimum_should_match"": 2
			        }
			    }
			}");
		}

		[U]
		[Description("query-dsl/query-string-query.asciidoc:462")]
		public void Line462()
		{
			// tag::be1bd47393646ac6bbee177d1cdb7738[]
			var searchResponse = client.Search<object>(s => s
				.AllIndices()
				.Query(q => q
					.QueryString(qs => qs
						.Fields(f => f
							.Field("title")
							.Field("content")
						)
						.Query("this that thus")
						.MinimumShouldMatch(2)
					)
				)
			);
			// end::be1bd47393646ac6bbee177d1cdb7738[]

			searchResponse.MatchesExample(@"GET /_search
			{
			    ""query"": {
			        ""query_string"": {
			            ""fields"": [
			                ""title"",
			                ""content""
			            ],
			            ""query"": ""this that thus"",
			            ""minimum_should_match"": 2
			        }
			    }
			}");
		}

		[U]
		[Description("query-dsl/query-string-query.asciidoc:486")]
		public void Line486()
		{
			// tag::fdd38f0d248385a444c777e7acd97846[]
			var searchResponse = client.Search<object>(s => s
				.AllIndices()
				.Query(q => q
					.QueryString(qs => qs
						.Fields(f => f
							.Field("title")
							.Field("content")
						)
						.Query("this OR that OR thus")
						.MinimumShouldMatch(2)
					)
				)
			);
			// end::fdd38f0d248385a444c777e7acd97846[]

			searchResponse.MatchesExample(@"GET /_search
			{
			    ""query"": {
			        ""query_string"": {
			            ""fields"": [
			                ""title"",
			                ""content""
			            ],
			            ""query"": ""this OR that OR thus"",
			            ""minimum_should_match"": 2
			        }
			    }
			}");
		}

		[U]
		[Description("query-dsl/query-string-query.asciidoc:518")]
		public void Line518()
		{
			// tag::6f21a878fee3b43c5332b81aaddbeac7[]
			var searchResponse = client.Search<object>(s => s
				.AllIndices()
				.Query(q => q
					.QueryString(qs => qs
						.Fields(f => f
							.Field("title")
							.Field("content")
						)
						.Query("this OR that OR thus")
						.Type(TextQueryType.CrossFields)
						.MinimumShouldMatch(2)
					)
				)
			);
			// end::6f21a878fee3b43c5332b81aaddbeac7[]

			searchResponse.MatchesExample(@"GET /_search
			{
			    ""query"": {
			        ""query_string"": {
			            ""fields"": [
			                ""title"",
			                ""content""
			            ],
			            ""query"": ""this OR that OR thus"",
			            ""type"": ""cross_fields"",
			            ""minimum_should_match"": 2
			        }
			    }
			}");
		}
	}
}
