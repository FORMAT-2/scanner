using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Accusoft.ImagXpressSdk;
using Accusoft.FormDirectorSdk;
using Accusoft.FormFixSdk;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private Button buttonSelectImage1;
        private Button buttonSelectImage2;
        private Button buttonCompare;
        private PictureBox pictureBoxImage1;
        private PictureBox pictureBoxImage2;
        private Label labelImage1Status;
        private Label labelImage2Status;
        private string imagePath1;
        private string imagePath2;
        public Form1()
        {
            InitializeComponent();
            CreateUI();
        }

        private void CreateUI()
        {
            // Create and configure Button for selecting Image 1
            buttonSelectImage1 = new Button
            {
                Text = "Select Image 1",
                Location = new Point(30, 30),
                Size = new Size(120, 30)
            };
            buttonSelectImage1.Click += ButtonSelectImage1_Click;

            // Create and configure Button for selecting Image 2
            buttonSelectImage2 = new Button
            {
                Text = "Select Image 2",
                Location = new Point(30, 80),
                Size = new Size(120, 30)
            };
            buttonSelectImage2.Click += ButtonSelectImage2_Click;

            // Create and configure Button for comparing images
            buttonCompare = new Button
            {
                Text = "Compare Images",
                Location = new Point(30, 130),
                Size = new Size(120, 30)
            };
            buttonCompare.Click += ButtonCompare_Click;

            // Create and configure PictureBox for Image 1
            pictureBoxImage1 = new PictureBox
            {
                Location = new Point(180, 30),
                Size = new Size(200, 200),
                SizeMode = PictureBoxSizeMode.StretchImage,
                BorderStyle = BorderStyle.FixedSingle
            };

            // Create and configure PictureBox for Image 2
            pictureBoxImage2 = new PictureBox
            {
                Location = new Point(180, 250),
                Size = new Size(200, 200),
                SizeMode = PictureBoxSizeMode.StretchImage,
                BorderStyle = BorderStyle.FixedSingle
            };

            // Create and configure Label for Image 1 status
            labelImage1Status = new Label
            {
                Text = "Image 1: Not selected",
                Location = new Point(30, 260),
                AutoSize = true
            };

            // Create and configure Label for Image 2 status
            labelImage2Status = new Label
            {
                Text = "Image 2: Not selected",
                Location = new Point(30, 310),
                AutoSize = true
            };

            // Add controls to the form
            this.Controls.Add(buttonSelectImage1);
            this.Controls.Add(buttonSelectImage2);
            this.Controls.Add(buttonCompare);
            this.Controls.Add(pictureBoxImage1);
            this.Controls.Add(pictureBoxImage2);
            this.Controls.Add(labelImage1Status);
            this.Controls.Add(labelImage2Status);

            // Configure the form
            this.ClientSize = new Size(400, 500);
            this.Text = "Image Fetcher";
        }

        private void ButtonSelectImage1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = "Select Image 1";
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.tif;*.frs|All files (*.*)|*.*"; // Image types filter

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    imagePath1 = openFileDialog.FileName;
                    //pictureBoxImage1.Image = Image.FromFile(imagePath1);
                    labelImage1Status.Text = $"Image 1: {System.IO.Path.GetFileName(imagePath1)}";
                }
            }
        }

        private void ButtonSelectImage2_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = "Select Image 2";
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.tif;*.frs|All files (*.*)|*.*"; // Image types filter

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    imagePath2 = openFileDialog.FileName;
                    //pictureBoxImage2.Image = Image.FromFile(imagePath2);
                    labelImage2Status.Text = $"Image 2: {System.IO.Path.GetFileName(imagePath2)}";
                }
            }
        }

        private void ButtonCompare_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(imagePath1) || string.IsNullOrEmpty(imagePath2))
            {
                MessageBox.Show("Please select both images before comparing.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Compare images
            bool areSameSize = CompareImageSizes(imagePath1, imagePath2);
            string message = areSameSize ? "The images are the same size." : "The images are different sizes.";
            MessageBox.Show(message, "Comparison Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private bool CompareImageSizes(string path1, string path2)
        {
            return FormsProcessing(path1, path2);
        }
        public bool FormsProcessing(string formSetFile, string inputFile)
        {
            int Result = 0;
            int page = 1;
            Accusoft.FormDirectorSdk.FormSetFile myFormSet;
        //Accusoft.ImagXpressSdk.ImageXData qryFile = null;
        //if (qryFile.Format != Accusoft.ImagXpressSdk.ImageXFormat.Bmp && qryFile.Format != Accusoft.ImagXpressSdk.ImageXFormat.Tiff
        //        && qryFile.Format != Accusoft.ImagXpressSdk.ImageXFormat.Jpeg && qryFile.Format != Accusoft.ImagXpressSdk.ImageXFormat.Pdf)
        //{

        //    return false;
        //}
        //try
        //{
        //    using (ImageDataIX imgData = new ImageDataIX(formName, 0))
        //    {
        //        templateImage = imgData.Load();
        //    }
        //}
        //catch (Exception ex)
        //{
        //    return false;
        //}
        myFormSet = new Accusoft.FormDirectorSdk.FormSetFile(Accusoft.FormDirectorSdk.FormDirector);
            myFormSet.Name = "NewFormSet";
            myFormSet.Filename = "NewFormSet.frs";
            Accusoft.Forms.LicenseKeychain licenseKeychain = new Accusoft.Forms.LicenseKeychain();
            using (Accusoft.Forms.Processor formsProcessor = new Accusoft.Forms.Processor(formSetFile, licenseKeychain))
            using (ImagXpress imagXpress = new ImagXpress())
            {
                if (imagXpress.Licensing.LicenseStatus == LicenseStatus.NoLicense)
                {
                    imagXpress.Licensing.SetSolutionName("Accusoft");
                    imagXpress.Licensing.SetSolutionKey(0x00000000, 0x00000000, 0x00000000, 0x00000000);
                    imagXpress.Licensing.SetOEMLicenseKey("2.0.R5liQmv6TbTj8mpaUDQj86TKEj8bq61X8DE5vJ1bED8Dl4ZI1gvgQDlDvb7bfgVKZbQCpaTCQIEaqRZ4WgU6qXMmp6TjMgvKEg76U5qJEavI8bfXZDljpKTJ7KMJVmZIVDM4VXTJTXVXWKURva8aq4EmMClbVaTJlKEXqXUm7iQDfCEI1j75VjlREC7JTRUjvaWmUgvaW6fCZgqjfDMi8mUmEg1DQ5MblCQJpI8D86ZJ75V6l61KEgEmWI7gfaQC8RqIWbEiU6pRVbqRVgWD1jWm86Vap5W5TaE6f68DTRVbUaMaUXM4pC151CvJvDvRZCQiWIpgUI7i1I85qgE5ZJQDfb8mZCW5VXlbMjWIUITi1RWjfmq5WifmZ57bqb1C7DqRQJQCVR1aUIvgQJqDZD7jVRWRqmqCp41DfX76v6ljMafDlXEXZJUjZaEJ1Ivb7RTXWXWmVIlIlaliZJUgWm7bqRQDWbUK1aZ6WRWmqKMDQg1KlbqgvCva16vIZjZJQIMDvDZgv58gfjVR75vgZgqaZiMbVgE5M6VaTIWCE5lQppK");
                }
                Accusoft.ImagXpressSdk.ImageXData qryFile = null;
                qryFile = ImageX.QueryFile(imagXpress, inputFile);
                //ImageXData numPages = ImageX.NumPages(imagXpress, inputFile);
                using (ImageX filledFormImageX = ImageX.FromFile(imagXpress, inputFile, page))
                using (Bitmap filledFormImage = filledFormImageX.ToBitmap(false))
                {
                    Accusoft.Forms.FormResult formResult = null;
                    formResult = formsProcessor.ProcessImage(filledFormImage);
                    if (formResult.IdentificationResult.State != Accusoft.Forms.IdentificationState.MatchFound)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }

           
        }

        private int giveResult(Accusoft.Forms.FormResult formResult, ImageX filledFormImage, string fileName, int page)
        {
            int Result = 0;
            if (formResult.IdentificationResult.State != Accusoft.Forms.IdentificationState.MatchFound)
            {
                Result = -1;
            }
            else
            {
                Result = 1;
            }
            return Result;
        }

        public Bitmap Load(string filename)
        {
            ImageX imgX = null;
            Accusoft.ImagXpressSdk.ImagXpress Imagxpress = null;
            //imgX = ImageX.FromFile(AccuComponent.ImagXpress, this.FileName, this.ImageNum + 1);
            //else
                imgX = ImageX.FromFile(Imagxpress, filename);

            Bitmap bmapImage = imgX.ToBitmap(true);
            imgX.Dispose();

            return bmapImage;
        }

    }
}
