using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.IndicesGetFieldMapping
{
	public partial class IndicesGetFieldMappingTests
	{	


		public class Raise404WhenFieldDoesntExistTests : YamlTestsBase
		{
			[Test]
			public void Raise404WhenFieldDoesntExistTest()
			{	

				//do indices.create 
				_body = new {
					mappings= new {
						test_type= new {
							properties= new {
								text= new {
									type= "string",
									analyzer= "whitespace"
								}
							}
						}
					}
				};
				_status = this._client.IndicesCreatePost("test_index", _body);
				_response = _status.Deserialize<dynamic>();

				//do indices.get_field_mapping 
				_status = this._client.IndicesGetFieldMapping("test_index", "test_type", "not_text");
				_response = _status.Deserialize<dynamic>();

			}
		}
	}
}
