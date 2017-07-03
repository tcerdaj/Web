using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using JehovaJireh.Configuration;
using JehovaJireh.Core.IRepositories;
using JehovaJireh.Logging;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace JehovaJireh.Data.Repositories
{
	public class ImageService : IImageService
	{
		internal CloudBlobClient cloudBlobClient;
		internal CloudBlobContainer cloudBlobContainer;
		internal CloudStorageAccount cloudStorageAccount;
		internal ILogger log;

		public ImageService(ILogger log)
		{
			try
			{
				this.log = log;
				this.cloudStorageAccount = CloudConfiguration.GetStorageAccount("StorageConnectionString");
				this.cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();
				this.cloudBlobContainer = cloudBlobClient.GetContainerReference("images");
				this.cloudBlobContainer.SetPermissions(
					new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob }
				);
				this.cloudBlobContainer.CreateIfNotExistsAsync();
			}
			catch (System.Exception ex)
			{
				if (this.log != null)
					this.log.Error("An error occurred in  ImageService(ILogger log) constructor.", ex);
			}
		}

		public async Task CreateIfNotExist()
		{
			if (await cloudBlobContainer.CreateIfNotExistsAsync())
			{
				await cloudBlobContainer.SetPermissionsAsync(
					new BlobContainerPermissions
					{
						PublicAccess = BlobContainerPublicAccessType.Blob
					}
					);
			}
		}
		public async Task<string> CreateUploadedImageAsync(HttpPostedFileBase file, string fileName)
		{
			if (file == null)
				return null;

			string imageFullPath = null;
			try
			{
				var _fileName = file.FileName;
				var imageName = fileName + Path.GetExtension(_fileName);
				CloudBlockBlob cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(imageName);
				cloudBlockBlob.Properties.ContentType = file.ContentType;
				await cloudBlockBlob.UploadFromStreamAsync(file.InputStream);
				imageFullPath = cloudBlockBlob.Uri.ToString();
			}
			catch (System.Exception ex)
			{
					Console.WriteLine("An error ocurred in CreateUploadedImageAsync method.", ex);
			}
			finally
			{
				Console.WriteLine("CreateUploadedImageAsync Finish.");
			}

			return imageFullPath;
		}
	}
}
