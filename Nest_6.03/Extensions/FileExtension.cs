﻿namespace Nest_6._03.Extensions;

public static class FileExtension
{

    public static async Task<string> SaveFileAsync(this IFormFile file, string root,  string assets, string imgs ,string folderName)
    {
        string uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
        string path = Path.Combine(root,  assets,imgs, folderName, uniqueFileName);


        using FileStream fs = new FileStream(path, FileMode.Create);

        await file.CopyToAsync(fs);

        return uniqueFileName;
    }


    public static bool CheckFileType(this IFormFile file, string fileType)
    {
        if (file.ContentType.Contains(fileType))
        {
            return true;
        }
        return false;
    }

    public static bool CheckFileSize(this IFormFile file, int fileSize)
    {
        if (file.Length > fileSize * 1024 * 1024)
        {
            return false;
        }
        return true;
    }

   


}

