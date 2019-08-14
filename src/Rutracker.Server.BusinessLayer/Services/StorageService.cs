﻿using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Rutracker.Server.BusinessLayer.Exceptions;
using Rutracker.Server.BusinessLayer.Interfaces;
using Rutracker.Server.BusinessLayer.Options;

namespace Rutracker.Server.BusinessLayer.Services
{
    public class StorageService : IStorageService
    {
        private readonly StorageSettings _storageSettings;
        private readonly CloudBlobClient _client;

        public StorageService(IOptions<StorageSettings> storageOptions)
        {
            _storageSettings = storageOptions?.Value ?? throw new ArgumentNullException(nameof(storageOptions));

            if (string.IsNullOrWhiteSpace(_storageSettings.ConnectionString))
            {
                throw new ArgumentException("ConnectionString can't be null.", nameof(_storageSettings.ConnectionString));
            }

            _client = CloudStorageAccount.Parse(_storageSettings.ConnectionString).CreateCloudBlobClient();

            if (_client == null)
            {
                throw new ArgumentException("Not valid blob client.", nameof(_client));
            }
        }

        public async Task UploadFileAsync(string containerName, string fileName, Stream stream)
        {
            var blockBlob = await GetBlockBlobAsync(containerName, fileName, createIfNotExists: true);

            await blockBlob.UploadFromStreamAsync(stream);
        }

        public async Task<Stream> DownloadFileAsync(string containerName, string fileName)
        {
            var blockBlob = await GetBlockBlobAsync(containerName, fileName);

            using var stream = File.OpenWrite(fileName);

            await blockBlob.DownloadToStreamAsync(stream);

            return stream;
        }

        public async Task<bool> DeleteFileAsync(string containerName, string fileName)
        {
            var blockBlob = await GetBlockBlobAsync(containerName, fileName);

            return await blockBlob.DeleteIfExistsAsync();
        }

        public async Task<string> PathToFileAsync(string containerName, string fileName)
        {
            var blockBlob = await GetBlockBlobAsync(containerName, fileName);

            return blockBlob.Uri.AbsoluteUri;
        }

        private async Task<CloudBlockBlob> GetBlockBlobAsync(string containerName, string fileName, bool createIfNotExists = false)
        {
            if (string.IsNullOrWhiteSpace(containerName))
            {
                throw new TorrentException($"The {nameof(containerName)} not valid.", ExceptionEventType.NotValidParameters);
            }

            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new TorrentException($"The {nameof(fileName)} not valid.", ExceptionEventType.NotValidParameters);
            }

            var container = _client.GetContainerReference(containerName);

            if (createIfNotExists)
            {
                await container.CreateIfNotExistsAsync();
                await container.SetPermissionsAsync(new BlobContainerPermissions
                {
                    PublicAccess = BlobContainerPublicAccessType.Container
                });
            }

            var blockBlob = container.GetBlockBlobReference(fileName);

            if (blockBlob == null)
            {
                throw new TorrentException($"The {nameof(blockBlob)} not found.", ExceptionEventType.NotFound);
            }

            return blockBlob;
        }
    }
}