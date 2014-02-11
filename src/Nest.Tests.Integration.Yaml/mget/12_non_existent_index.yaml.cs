using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.Mget
{
	public partial class MgetTests
	{	


		public class NonExistentIndexTests : YamlTestsBase
		{
			[Test]
			public void NonExistentIndexTest()
			{	

				//do index 
				_body = new {
					foo= "bar"
				};
				_status = this._client.IndexPost("test_1", "test", "1", _body);
				_response = _status.Deserialize<dynamic>();

				//do cluster.health 
				_status = this._client.ClusterHealthGet(nv=>nv
					.Add("wait_for_status","yellow")
				);
				_response = _status.Deserialize<dynamic>();

				//do mget 
				_body = new {
					docs= new dynamic[] {
						new {
							_index= "test_2",
							_type= "test",
							_id= "1"
						}
					}
				};
				_status = this._client.MgetPost(_body);
				_response = _status.Deserialize<dynamic>();

				//is_false .docs[0].exists; 
				this.IsFalse(_response.docs[0].exists);

				//do mget 
				_body = new {
					index= "test_2",
					docs= new dynamic[] {
						new {
							_index= "test_1",
							_type= "test",
							_id= "1"
						}
					}
				};
				_status = this._client.MgetPost(_body);
				_response = _status.Deserialize<dynamic>();

				//is_true .docs[0].exists; 
				this.IsTrue(_response.docs[0].exists);

			}
		}
	}
}
