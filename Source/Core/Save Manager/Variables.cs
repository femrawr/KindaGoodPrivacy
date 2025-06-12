namespace KindaGoodPrivacy.Source.Core.SaveManager
{
    internal class Variables
    {
        public static readonly TimeSpan FILE_EXPIRE_TIME = TimeSpan.FromDays(5);

        public static readonly string extension = ".kgp";

        public static readonly string textFolderName = "TextSaves";
        public static readonly string mediaFolderName = "MediaSaves";
        public static readonly string tempFolderName = "TempSaves";

        public static readonly string imgFolderName = "ImgSaves";
        public static readonly string vidFolderName = "VidSaves";

        public static readonly string tempMetadataName = "_meta" + extension + "METADATA";

        public static readonly string textFolderPath = Path.Combine(
            Utils.Saving.mainFolderPath, textFolderName
        );

        public static readonly string mediaFolderPath = Path.Combine(
            Utils.Saving.mainFolderPath, mediaFolderName
        );

        public static readonly string imgFolderPath = Path.Combine(
            mediaFolderPath, imgFolderName
        );

        public static readonly string vidFolderPath = Path.Combine(
            mediaFolderPath, vidFolderName
        );

        public static readonly string tempFolderPath = Path.Combine(
            Utils.Saving.mainFolderPath, tempFolderName
        );

        public static readonly string tempMetadataPath = Path.Combine(
             Utils.Saving.mainFolderPath, tempFolderName, tempMetadataName
        );

        public const int SAVETYPE_TEXT = 0;
        public const int SAVETYPE_IMG = 1;
        public const int SAVETYPE_VID = 2;
    }
}
