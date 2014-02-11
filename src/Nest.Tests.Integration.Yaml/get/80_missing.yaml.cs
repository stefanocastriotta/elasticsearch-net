using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.Get
{
	public partial class GetTests
	{	


		public class MissingDocumentWithCatchTests : YamlTestsBase
		{
			[Test]
			public void MissingDocumentWithCatchTest()
			{	

				//do get 
				_status = this._client.Get("test_1", "test", "1");
				_response = _status.Deserialize<dynamic>();

			}
		}

		public class MissingDocumentWithIgnoreTests : YamlTestsBase
		{
			[Test]
			public void MissingDocumentWithIgnoreTest()
			{	

				//do get 
				_status = this._client.Get("test_1", "test", "1", nv=>nv
					.Add("ignore","404")
				);
				_response = _status.Deserialize<dynamic>();

			}
		}
	}
}
