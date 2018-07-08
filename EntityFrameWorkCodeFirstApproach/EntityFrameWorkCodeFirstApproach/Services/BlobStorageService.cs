using Microsoft.WindowsAzure.ServiceRuntime;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EntityFrameWorkCodeFirstApproach.Services
{
    public class BlobStorageServices
    {
        public CloudBlobContainer GetCloudBlobContainer()
        {
            // Retrieve storage account from connection string.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=vishalgit;AccountKey=S808gjNhyH2gNMmpiXZjdp8FBCS2X2hSV+VeEUFuO/m9TIgrrc/YtLQhSiDJ/9Yfat7HGgRvUopaCKoyMLhALQ==;EndpointSuffix=core.windows.net");

            // Create the blob client.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            // Retrieve reference to a previously created container.
            CloudBlobContainer blobContainer = blobClient.GetContainerReference("Documents");
            if (blobContainer.CreateIfNotExists())
            {
                blobContainer.SetPermissions(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });//Allowing public Access  

            }
            return blobContainer;

        }

    }
}