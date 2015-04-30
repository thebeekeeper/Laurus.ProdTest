/*
The MIT License (MIT)

Copyright (c) 2013 Nick Gamroth

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.IO;
using Newtonsoft.Json;

namespace Laurus.ProdTest.Core
{
	public class ConfigHandler
	{
		public static TestConfig ReadTestConfig()
		{
			if (!File.Exists("test.config"))
			{
				throw new Exception("Test run config file not found");
			}
			return JsonConvert.DeserializeObject<TestConfig>(File.ReadAllText("test.config"));
		}

		//public static TestCollection ReadTestConfig()
		//{
		//	var testConfigSection = ConfigurationManager.GetSection("TestsSection") as TestRunConfigurationSection;
		//	return testConfigSection.Tests;
		//}

		//public static TestCollection ReadTestFromFile(string file)
		//{
		//	if (!System.IO.File.Exists(file))
		//		throw new System.IO.FileNotFoundException();
		//	var configMap = new ExeConfigurationFileMap();
		//	configMap.ExeConfigFilename = file;
		//	var config = ConfigurationManager.OpenMappedExeConfiguration(configMap, ConfigurationUserLevel.None);
		//	var section = config.GetSection("TestsSection") as TestRunConfigurationSection;
		//	return section.Tests;
		//}
	}

	//public class TestRunConfigurationSection : ConfigurationSection
	//{
	//	[ConfigurationProperty("Tests", IsDefaultCollection = false)]
	//	[ConfigurationCollection(typeof(TestCollection),
	//		AddItemName = "add",
	//		ClearItemsName = "clear",
	//		RemoveItemName = "remove")]
	//	public TestCollection Tests
	//	{
	//		get
	//		{
	//			return (TestCollection)base["Tests"];
	//		}
	//	}
	//}

	public class TestConfig //: ConfigurationElement
	{
		public IEnumerable<Test> Tests { get; set; }
		public IDictionary<string, string> ConfigParameters { get; set; }
		public Security Security { get; set; }
		//public int Order { get; set; }

		//public string Type { get; set; }
	}
	public class Test
	{
		public string Type { get; set; }
	}

    public class Security
	{
		public string PublicKey { get; set; }
		public string Signature { get; set; }
	}

	//public class TestCollection : ConfigurationElementCollection
	//{
	//	public TestCollection()
	//	{
	//	}

	//	public TestConfig this[int index]
	//	{
	//		get { return (TestConfig)BaseGet(index); }
	//		set
	//		{
	//			if (BaseGet(index) != null)
	//			{
	//				BaseRemoveAt(index);
	//			}
	//			BaseAdd(index, value);
	//		}
	//	}

	//	public void Add(TestConfig serviceConfig)
	//	{
	//		BaseAdd(serviceConfig);
	//	}

	//	public void Clear()
	//	{
	//		BaseClear();
	//	}

	//	protected override ConfigurationElement CreateNewElement()
	//	{
	//		return new TestConfig();
	//	}

	//	protected override object GetElementKey(ConfigurationElement element)
	//	{
	//		return ((TestConfig)element).Order;
	//	}

	//	public void Remove(TestConfig serviceConfig)
	//	{
	//		BaseRemove(serviceConfig.Order);
	//	}

	//	public void RemoveAt(int index)
	//	{
	//		BaseRemoveAt(index);
	//	}

	//	public void Remove(string name)
	//	{
	//		BaseRemove(name);
	//	}
	//}
}
