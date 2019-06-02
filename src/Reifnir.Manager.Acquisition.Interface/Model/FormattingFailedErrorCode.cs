namespace Reifnir.Manager.Acquisition.Interface.Model
{
    public enum FormattingFailedErrorCode
    {
        AssetIdDoesNotExist = 1,
        UnknownAssetFormat = 2,
        /// <summary>
        /// Fortunately, Scott Brick typically doesn't read books available in the public domain. That leaves it up to
        /// the user to choose ANY other reader when buying audiobooks that will be consumed by the system.
        /// https://wordsnstuffs.wordpress.com/2016/05/13/help-my-audio-book-has-been-bricked/
        /// </summary>
        ScottBrickDetected = 3
        ///etc
    }
}