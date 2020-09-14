using System;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using JakeJones.Home.Blog.Managers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace JakeJones.Home.Blog.Implementation.Managers
{
	internal class ImageManager  : IImageManager
	{
		public const string ImageFolder = "blog-images";
		private readonly string _path;

		public ImageManager(IWebHostEnvironment env)
		{
			_path = Path.Combine(env.WebRootPath, ImageFolder);
		}

		public async Task<string> SaveAsync(IFormFile file)
		{
			if (file == null)
			{
				throw new ArgumentNullException(nameof(file));
			}

			const int megabyte = 1024 * 1024;

			if (!file.ContentType.StartsWith("image/"))
			{
				throw new InvalidOperationException("Invalid MIME content type.");
			}

			var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
			string[] extensions = { ".gif", ".jpg", ".png", ".svg" };

			if (!extensions.Contains(extension))
			{
				throw new InvalidOperationException("Invalid file extension.");
			}

			if (file.Length > 8 * megabyte)
			{
				throw new InvalidOperationException("File size limit exceeded.");
			}

			if (!Directory.Exists(_path))
			{
				Directory.CreateDirectory(_path);
			}

			var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');

			using (var fileStream = new FileStream(Path.Combine(_path, fileName), FileMode.Create))
			{
				await file.CopyToAsync(fileStream);
			}

			return Path.Combine("/", ImageFolder, fileName).Replace('\\', '/');
		}
	}
}