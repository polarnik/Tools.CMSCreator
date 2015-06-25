using System;
using System.Configuration;
using System.IO;
using System.Security.Cryptography.Pkcs;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;

namespace Tools.CMSCreator
{
	public partial class FormMain : Form
	{
		public FormMain()
		{
			InitializeComponent();
			string storeName = ConfigurationManager.AppSettings.Get("storeName");
			StoreLocation storeLocation = (StoreLocation)Enum.Parse(typeof(StoreLocation), ConfigurationManager.AppSettings.Get("storeLocation"));
			X509FindType x509FindType = (X509FindType)Enum.Parse(typeof(X509FindType), ConfigurationManager.AppSettings.Get("x509FindType"));
			string findValue = ConfigurationManager.AppSettings.Get("findValue");

			x509Certificate2 = LoadCertificate(storeName, storeLocation, x509FindType, findValue);
		}

		X509Certificate2 x509Certificate2 { get; set; }

		public X509Certificate2 LoadCertificate(string storeName, StoreLocation
		storeLocation, X509FindType findType, string value)
		{
			// The following code gets the cert from the keystore
			X509Store store = CreateStoreObject(storeName, storeLocation);
			store.Open(OpenFlags.ReadOnly);
			X509Certificate2Collection certCollection =
					 store.Certificates.Find(findType,
					 value, false);
			X509Certificate2Enumerator enumerator = certCollection.GetEnumerator();
			X509Certificate2 cert = null;
			while (enumerator.MoveNext())
			{
				cert = enumerator.Current;
			}
			return cert;
		}

		public X509Certificate2 LoadCertificate(StoreName storeName, StoreLocation
		storeLocation, X509FindType findType, string value)
		{
			return LoadCertificate(storeName.ToString(), storeLocation, findType, value);
		}

		/// <summary>
		/// Создать объект хранилища
		/// </summary>
		public X509Store CreateStoreObject(string storeName, StoreLocation storeLocation)
		{
			StoreName res;
			var isSystemStore = Enum.TryParse(storeName, out res);
			if (isSystemStore)
				return new X509Store(res, storeLocation);
			return new X509Store(storeName, storeLocation);
		}

		private void FormMain_DragDrop(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(DataFormats.FileDrop))
			{
				string[] filePaths = (string[])(e.Data.GetData(DataFormats.FileDrop));
				foreach (string fileLoc in filePaths)
				{
					// Code to read the contents of the text file
					if (File.Exists(fileLoc))
					{
						FileInfo info = new FileInfo(fileLoc);
						if (radioButtonCMS.Checked)
						{
							if (string.Equals(info.Extension, ".cms", StringComparison.InvariantCultureIgnoreCase))
							{
								MessageBox.Show(string.Format("File {0} alredy signed (cms).", fileLoc));
							}
							else
							{
								byte[] documentContent = ReadFile(fileLoc);
								byte[] signContent = Sign(x509Certificate2, documentContent, false);
								string cmsPath = fileLoc + ".cms";
								WriteFile(cmsPath, signContent);
							}
						}
						else
						{
							if (string.Equals(info.Extension, ".sign", StringComparison.InvariantCultureIgnoreCase))
							{
								MessageBox.Show(string.Format("File {0} alredy signed (sign).", fileLoc));
							}
							else
							{
								byte[] documentContent = ReadFile(fileLoc);
								byte[] signContent = Sign(x509Certificate2, documentContent, true);
								string signPath = fileLoc + ".sign";
								WriteFile(signPath, signContent);
							}
						}
					}
				}
			}
		}

		private byte[] ReadFile(string path)
		{
			byte[] content = null;
			try
			{
				content = File.ReadAllBytes(path);
			}
			catch (IOException e)
			{
				MessageBox.Show(
					string.Format("Не удалось прочитать файл {0}\n\n{1}", path, e.Message), 
					"Ошибка открытия файла");
			}
			return content;
		}

		private void WriteFile(string path, byte[] fileContent)
		{
			try
			{
				File.WriteAllBytes(path, fileContent);
			}
			catch (IOException e)
			{
				MessageBox.Show(
					string.Format("Не удалось записать данные в файл {0}\n\n{1}", path, e.Message),
					"Ошибка записи в файл");
			}
		}

		public byte[] Sign(X509Certificate2 certificate, byte[] data, bool detached)
		{
			// то что подписываем
			var contentInfo = new ContentInfo(data);
			var signedCms = new SignedCms(contentInfo, detached);
			// сертификато для подписания

			var cmsSigner = new CmsSigner(SubjectIdentifierType.IssuerAndSerialNumber, certificate);
			signedCms.ComputeSignature(cmsSigner, true);
			// подпись
			return signedCms.Encode();
		}

		private void FormMain_DragEnter(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(DataFormats.FileDrop))
			{
				e.Effect = DragDropEffects.Copy;
			}
			else
			{
				e.Effect = DragDropEffects.None;
			}
		}
	}
}
