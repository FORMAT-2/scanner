///*************************************************************************
// * Copyright © 2011-2021 Accusoft Corporation.                           *
// * This sample code is provided to Accusoft licensees "as is"            *
// * with no restrictions on use or modification. No warranty for          *
// * use of this sample code is provided by Accusoft.                      *
// ************************************************************************/

//namespace WindowsFormsApp1
//{
//    using System;
//    using System.Drawing;
//    using System.IO;
//    using Accusoft.ImagXpressSdk;

//    public class ImageDataIX : IImageData, IDisposable
//    {
//        private bool _disposed = false;
//        private Bitmap _cacheBitmap = null;

//        /// <summary>
//        /// Constructor - filePath only (default/ignored image #)
//        /// </summary>
//        /// <param name="filePath"></param>
//        public ImageDataIX(string fileName) : this(fileName, -1) { }

//        /// <summary>
//        /// Constructor - filePath and image #
//        /// </summary>
//        /// <param name="filePath"></param>
//        /// <param name="imageNum"></param>
//        public ImageDataIX(string fileName, int imageNum)
//        {
//            _disposed = true;

//            this.FileName = fileName;
//            this.ImageNum = imageNum;

//            this.CanLoad = true;
//            this.CanSave = false;   // read-only
//        }

//        /// <summary>
//        /// IDisposable method
//        /// </summary>
//        public void Dispose()
//        {
//            Dispose(true);
//            GC.SuppressFinalize(this);
//        }

//        /// <summary>
//        /// Internal dispose method
//        /// </summary>
//        /// <param name="disposing"></param>
//        protected virtual void Dispose(bool disposing)
//        {
//            // If you need thread safety, use a lock around these 

//            if (!_disposed)
//            {
//                if (disposing)
//                {
//                    if (_cacheBitmap != null)
//                        _cacheBitmap.Dispose();
//                }

//                // Indicate that the instance has been disposed.
//                _cacheBitmap = null;
//                _disposed = true;
//            }
//        }

//        /// <summary>
//        /// Evaluate whether object can proces the provided image specification string
//        /// </summary>
//        /// <param name="fileName"></param>
//        /// <returns>true = can process</returns>
//        public static bool CanProcess(string imageSpec)
//        {
//            bool supported = Path.GetExtension(imageSpec).ToUpper().Equals(".TIF") ||
//                             Path.GetExtension(imageSpec).ToUpper().Equals(".TIFF") ||
//                             Path.GetExtension(imageSpec).ToUpper().Equals(".JPG") ||
//                             Path.GetExtension(imageSpec).ToUpper().Equals(".JPEG") ||
//                             Path.GetExtension(imageSpec).ToUpper().Equals(".BMP") ||
//                             Path.GetExtension(imageSpec).ToUpper().Equals(".PDF");

//            return supported;
//        }

//        /// <summary>
//        /// Gets flags indicating whether the object can load or save images
//        /// </summary>
//        public bool CanLoad { get; protected set; }
//        public bool CanSave { get; protected set; }

//        /// <summary>
//        /// Gets display name string
//        /// </summary>
//        public string DisplayName
//        {
//            get
//            {
//                string dispName = this.FileName;

//                // Combine filePath & image number
//                if (this.ImageNum >= 0)
//                    dispName = string.Format("{0}, image {1:D}", this.FileName, this.ImageNum + 1);

//                return dispName;
//            }
//        }

//        /// <summary>
//        /// Returns (theoretically) unique base name that is suitable as the base filePath for saving this image
//        /// </summary>
//        /// <returns></returns>
//        public string SaveNameBase()
//        {
//            if (string.IsNullOrEmpty(this.FileName))
//                throw new Exception("FileName property not set");

//            string saveName = Path.GetFileNameWithoutExtension(this.FileName);

//            if (this.ImageNum >= 0)
//                saveName += "_img" + this.ImageNum.ToString();

//            return saveName;
//        }

//        /// <summary>
//        /// Gets local filePath, if this exists - string.Empty otherwise
//        /// </summary>
//        public string FileName { get; protected set; }

//        /// <summary>
//        /// Gets 0-based image number in a multi-image file, -1 if image number is not relevant
//        /// </summary>
//        public int ImageNum { get; protected set; }

//        /// <summary>
//        /// Gets count of images in file
//        /// </summary>
//        /// <returns>image count, -1 if error</returns>
//        public static int GetImageCount(string imageSpec)
//        {
//            if (string.IsNullOrEmpty(imageSpec))
//                throw new ArgumentException("imageSpec not set", "imageSpec");

//            int imgCount = -1;
//            try
//            {
//                imgCount = ImageX.NumPages(AccuComponent.ImagXpress, imageSpec);
//            }
//            catch (Exception)
//            {
//            }

//            return imgCount;
//        }

//        /// <summary>
//        /// Load image and return .NET Bitmap object
//        /// </summary>
//        /// <returns>System.Drawing.Bitmap object with loaded image</returns>
//        public Bitmap Load()
//        {
//            if (this._cacheBitmap != null)
//                return this._cacheBitmap;   // quick return if already loaded

//            if (string.IsNullOrEmpty(this.FileName))
//                throw new Exception("FileName property not set");

//            ImageX imgX = null;
//            if (this.ImageNum >= 0)
//                imgX = ImageX.FromFile(AccuComponent.ImagXpress, this.FileName, this.ImageNum + 1);
//            else
//                imgX = ImageX.FromFile(AccuComponent.ImagXpress, this.FileName);

//            this._cacheBitmap = imgX.ToBitmap(true);
//            imgX.Dispose();

//            return this._cacheBitmap;
//        }
//    }
//}
