﻿namespace ITI.PL.Helpers
{
	public static class DocumentSetting
	{
		public static string UploadFile(IFormFile file, string folderName)
		{
			// 1. Get Located Folder Path
			var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", folderName);

			if (!Directory.Exists(folderPath))
				Directory.CreateDirectory(folderPath);

			// 2. Get File Name and Make it UNIQUE

			var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";

			// 3. Get File Path

			var filePath = Path.Combine(folderPath, fileName);

			// 4. Save File as Stream

			using var fileStream = new FileStream(filePath,FileMode.Create);

			file.CopyTo(fileStream);	

			return fileName;
		}

		public static void DeleteFile(string fileName,string folderName)
		{

			var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", folderName, fileName);
			if(File.Exists(filePath))
				File.Delete(filePath);

		}
	}
}
