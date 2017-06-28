using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using JehovaJireh.Configuration;
using JehovaJireh.Core.IRepositories;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace JehovaJireh.Data.Repositories
{
	public class ImageService : IImageService
	{
		internal CloudBlobClient cloudBlobClient;
		internal CloudBlobContainer cloudBlobContainer;
		internal CloudStorageAccount cloudStorageAccount;
		public ImageService()
		{
			this.cloudStorageAccount = CloudConfiguration.GetStorageAccount("StorageConnectionString");
			this.cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();
			this.cloudBlobContainer = cloudBlobClient.GetContainerReference("Images");
			CreateIfNotExist();
		}

		private async void CreateIfNotExist()
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
		public async Task<string> CreateUploadedImageAsync(HttpPostedFileBase file)
		{
			if (file == null)
				throw new ArgumentNullException("file");

			string imageFullPath = null;
			try
			{
				var fileName = file.FileName;
				var imageName = Guid.NewGuid().ToString() + "-" + Path.GetExtension(fileName);
				CloudBlockBlob cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(imageName);
				cloudBlockBlob.Properties.ContentType = file.ContentType;
				await cloudBlockBlob.UploadFromStreamAsync(file.InputStream);
				imageFullPath = cloudBlockBlob.Uri.ToString();
			}
			catch (System.Exception)
			{

			}

			return imageFullPath;
		}
	}
}
