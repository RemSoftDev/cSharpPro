using System;
using System.IO;
using System.Web;

namespace CSharpPro.Extensions
{
	public static class PostedFileExtension
	{
		private static readonly string _uploadfolderName = "Uploaded";

		public static string SaveFile(this HttpServerUtilityBase Server, HttpPostedFileBase file)
		{
			string fileName = Path.GetFileName(file.FileName);

			var uploadFolderPath = string.Format("\\Content\\images\\{0}", _uploadfolderName);
			var uploadServerPath = Server.MapPath(uploadFolderPath);
			var uploadfolder = new DirectoryInfo(uploadServerPath);

			var folder = Guid.NewGuid().ToString();
			var fileFolderPath = Path.Combine(uploadFolderPath, folder);
			var fileServerFolderPath = Server.MapPath(fileFolderPath);
			var fileFolder = new DirectoryInfo(fileServerFolderPath);

			if (!uploadfolder.Exists)
			{
				uploadfolder.Create();
			}

			if (!fileFolder.Exists)
			{
				fileFolder.Create();
			}

			string filePath = Path.Combine(fileFolderPath, fileName); ;
			string fileServerPath = Server.MapPath(filePath);
			file.SaveAs(fileServerPath);

			return filePath;
		}

		public static void DeleteFile(this HttpServerUtilityBase Server, string path)
		{
			var serverPath = Server.MapPath(path);
			var fileInfo = new FileInfo(serverPath);
			if (fileInfo.Directory.Exists)
				fileInfo.Directory.Delete(true);
		}
	}
}