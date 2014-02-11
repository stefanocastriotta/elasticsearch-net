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


		public class FieldsTests : YamlTestsBase
		{
			[Test]
			public void FieldsTest()
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
							_id= "1"
						},
						new {
							_id= "1",
							fields= "foo"
						},
						new {
							_id= "1",
							fields= new [] {
								"foo"
							}
						},
						new {
							_id= "1",
							fields= new [] {
								"foo",
								"_source"
							}
						}
					}
				};
				_status = this._client.MgetPost("test_1", "test", _body);
				_response = _status.Deserialize<dynamic>();

				//is_false .docs[0].fields; 
				this.IsFalse(_response.docs[0].fields);

				//is_false .docs[1]._source; 
				this.IsFalse(_response.docs[1]._source);

				//is_false .docs[2]._source; 
				this.IsFalse(_response.docs[2]._source);

				//do mget 
				_body = new {
					docs= new dynamic[] {
						new {
							_id= "1"
						},
						new {
							_id= "1",
							fields= "foo"
						},
						new {
							_id= "1",
							fields= new [] {
								"foo"
							}
						},
						new {
							_id= "1",
							fields= new [] {
								"foo",
								"_source"
							}
						}
					}
				};
				_status = this._client.MgetPost("test_1", "test", _body, nv=>nv
					.Add("fields","foo")
				);
				_response = _status.Deserialize<dynamic>();

				//is_false .docs[0]._source; 
				this.IsFalse(_response.docs[0]._source);

				//is_false .docs[1]._source; 
				this.IsFalse(_response.docs[1]._source);

				//is_false .docs[2]._source; 
				this.IsFalse(_response.docs[2]._source);

				//do mget 
				_body = new {
					docs= new dynamic[] {
						new {
							_id= "1"
						},
						new {
							_id= "1",
							fields= "foo"
						},
						new {
							_id= "1",
							fields= new [] {
								"foo"
							}
						},
						new {
							_id= "1",
							fields= new [] {
								"foo",
								"_source"
							}
						}
					}
				};
				_status = this._client.MgetPost("test_1", "test", _body, nv=>nv
					.Add("fields","System.Collections.Generic.List`1[System.Object]")
				);
				_response = _status.Deserialize<dynamic>();

				//is_false .docs[0]._source; 
				this.IsFalse(_response.docs[0]._source);

				//is_false .docs[1]._source; 
				this.IsFalse(_response.docs[1]._source);

				//is_false .docs[2]._source; 
				this.IsFalse(_response.docs[2]._source);

				//do mget 
				_body = new {
					docs= new dynamic[] {
						new {
							_id= "1"
						},
						new {
							_id= "1",
							fields= "foo"
						},
						new {
							_id= "1",
							fields= new [] {
								"foo"
							}
						},
						new {
							_id= "1",
							fields= new [] {
								"foo",
								"_source"
							}
						}
					}
				};
				_status = this._client.MgetPost("test_1", "test", _body, nv=>nv
					.Add("fields","System.Collections.Generic.List`1[System.Object]")
				);
				_response = _status.Deserialize<dynamic>();

				//is_false .docs[1]._source; 
				this.IsFalse(_response.docs[1]._source);

				//is_false .docs[2]._source; 
				this.IsFalse(_response.docs[2]._source);

			}
		}
	}
}
