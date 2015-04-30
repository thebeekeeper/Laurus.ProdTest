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
using System.Xml;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;
using System.Security.Cryptography.Xml;
using System.IO;
using Newtonsoft.Json;

/*
 *  This was all taken from here: http://www.rahulchugh.com/2011/09/digitally-signing-appconfig.html
 *  To generate the certificate, do the following:
 *  http://blogs.msdn.com/b/maximelamure/archive/2007/01/24/create-your-own-pfx-file-for-clickonce.aspx
 *  makecert -sv mypvk.pvk -n "CN=company name" certificatefile.cer
 *  pvk2pfx -pvk mypvk.pvk -spc certificatefile.cer -pfx mypfx.pfx -po mypassword
 *  win-r + certmgr.msc
 *  right click personal | all tasks | import
 *  find the pfx file + use "mypassword" for password
 *  get thumbprint by double clicking imported certificate
 *  run signer app.config "bd 09 ... th um bp ri nt"
 */
namespace Laurus.ProdTest.Core
{
	public class ConfigSecurity
	{
		// i can't get this to work any longer, so i'm replacing it

		///// <summary>
		///// Checks if the digital signature on config are valid or not
		///// </summary>
		///// <returns>Returns true if signatures are valid else returns false</returns>
		//public static bool CheckDigitalSignature(string configFilePath)
		//{
		//	// Get the path of the configuration file
		//	//string configFilePath = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None).FilePath;

		//	// Loading configuration file as an Xml Document
		//	XmlDocument config = new XmlDocument();
		//	// Preserving whitespaces is important the signatures are computed considering each and every character in the file
		//	config.PreserveWhitespace = true;
		//	config.Load(configFilePath);

		//	// Creating XMLDSIG processor 
		//	SignedXml xmldsig = new SignedXml(config);

		//	// Getting the Signature element from the config
		//	XmlElement signature = (XmlElement)config.GetElementsByTagName("Signature")[0];

		//	// Loading XMLDSIG element into the XMLDSIG processor  
		//	xmldsig.LoadXml(signature);

		//	// Getting the public cerficate file
		//	X509Certificate2 certificate = new X509Certificate2(GetCertificateFile());

		//	// Public Key to verify the digital signatures
		//	RSACryptoServiceProvider rsa = (RSACryptoServiceProvider)certificate.PublicKey.Key;

		//	// Checking the signatures and returning the result
		//	return xmldsig.CheckSignature(rsa);
		//}

		///// <summary>
		///// Get the public certificate file
		///// </summary>
		///// <returns>Returns the path of the certificate file</returns>
		//private static string GetCertificateFile()
		//{
		//	// How to retrieve the embedded certificate can be seen in detail at :
		//	// http://www.rahulchugh.com/2011/09/ensuring-file-is-not-tampered-when-used.html
		//	return certificateFilePath;
		//}
		public static bool CheckDigitalSignature(TestConfig testConfig)
		{
			var publicXml = "<RSAKeyValue><Modulus>w2Jln9sryi/CkQT1xjyIDuLGpHjm6akKc2nt+bGuuayBIonWbVJmRS7NKHxrN5k3m91BBavq0sFjEvnXZPkygulmZJdDzJe2PAUGyUSPTsj+lR4F5Pxr1d9hGxzygMEghf9W4rMb1HL+26KA65P5mnc7tjlsASU64WFkR9xp98M=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";

			var p = new RSACryptoServiceProvider();
			p.FromXmlString(publicXml);

			var signedData = JsonConvert.SerializeObject(testConfig.Tests);

			return p.VerifyHash(
				 GetSha1Hash(signedData),
				 CryptoConfig.MapNameToOID("SHA1"),
				 Convert.FromBase64String(testConfig.Security.Signature));
		}
		private static byte[] GetFileSha1Hash(string file)
		{
			using (var fs = new FileStream(
				file, FileMode.Open))
			{
				return new SHA1CryptoServiceProvider().ComputeHash(fs);
			}
		}

		private static byte[] GetSha1Hash(string dataToHash)
		{
			using (var s = new MemoryStream(Encoding.UTF8.GetBytes(dataToHash ?? "")))
			{
				return new SHA1CryptoServiceProvider().ComputeHash(s);
			}
		}
	}
}
