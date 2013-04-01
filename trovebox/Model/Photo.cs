using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Media.Imaging;

namespace trovebox.Model
{
    /// <summary>
    /// Represents a single photo returned from the JSON OpenPhoto api. The properties of this class should match directly
    /// the JSON properties of the API response.
    /// </summary>
    /// <remarks>
    /// For more information about the schema used, please visit http://theopenphotoproject.org/documentation/schemas/Photo
    /// </remarks>
    public class Photo
    {
        /// <summary>
        /// Explicitly set all string properties to empty string to avoid problems with RestSharp Serialization.
        /// </summary>
        public Photo()
        {
            Id = string.Empty;
            AppId = string.Empty;
            Url = string.Empty;
            Host = string.Empty;
            Title = string.Empty;
            Description = string.Empty;
            Key = string.Empty;
            Hash = string.Empty;

            ExifCameraMake = string.Empty;
            ExifCameraModel = string.Empty;
            ExifExposureTime = string.Empty;
            ExifFNumber = string.Empty;
            ExifMaxApertureValue = string.Empty;
            ExifFlash = string.Empty;
            ExifFocalLength = string.Empty;

            License = string.Empty;
            PathOriginal = string.Empty;
            PathBase = string.Empty;

        }

        public string Id { get; set; }
        public string AppId { get; set; }
        public string Url { get; set; }
        public string Host { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Key { get; set; }
        public string Hash { get; set; }
        public List<string> Tags { get; set; }
        public int Size { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int Rotation { get; set; }

        public int ExifOrientation { get; set; }
        public string ExifCameraMake { get; set; }
        public string ExifCameraModel { get; set; }
        public string ExifExposureTime { get; set; }
        public string ExifFNumber { get; set; }
        public string ExifMaxApertureValue { get; set; }
        public string ExifMeteringMode { get; set; }
        public string ExifFlash { get; set; }
        public string ExifFocalLength { get; set; }

        public int Altitute { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int Views { get; set; }
        public int Status { get; set; }
        public int Permission { get; set; }
        public string License { get; set; }

        public int DateTaken { get; set; }
        public int DateTakenDay { get; set; }
        public int DateTakenMonth { get; set; }
        public int DateTakenYear { get; set; }
        public int DateUploaded { get; set; }
        public int DateUploadedDay { get; set; }
        public int DateUploadedMonth { get; set; }
        public int DateUploadedYear { get; set; }

        public string PathOriginal { get; set; }
        public string PathBase { get; set; }
        public string Path100x100xCR { get; set; }
        public Uri Path100x100xUri { get { return new Uri(Path100x100xCR); } private set { Path100x100xUri = null; } }
    }
}