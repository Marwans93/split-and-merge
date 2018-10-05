using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using MPI;

namespace RedSplitBlackMerge
{
    class Program
    {
        static void Main(string[] args)
        {
            Bitmap originalImage, firstQuarter, secondQuarter, thirdQuarter, forthQuarter;
            using (new MPI.Environment(ref args))
            {
                Intracommunicator comm = Communicator.world;

                if(comm.Rank == 0){
                //SPLIT-RED
                    originalImage = new Bitmap(Image.FromFile(@"C:\Users\user pc\Desktop\UiTMKJM\PART 7 SEPT2017_JAN2018\CSC580\RedSplitBlackMerge\Spiderman2.jpg"));

                Rectangle rect = new Rectangle(0, 0, originalImage.Width / 2, originalImage.Height / 2);
                firstQuarter = originalImage.Clone(rect, originalImage.PixelFormat);
                firstQuarter.Save(@"C:\Users\user pc\Desktop\UiTMKJM\PART 7 SEPT2017_JAN2018\CSC580\RedSplitBlackMerge\Spiderman2_part1.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                rect = new Rectangle(originalImage.Width / 2, 0, originalImage.Width / 2, originalImage.Height / 2);
                secondQuarter = originalImage.Clone(rect, originalImage.PixelFormat);
                secondQuarter.Save(@"C:\Users\user pc\Desktop\UiTMKJM\PART 7 SEPT2017_JAN2018\CSC580\RedSplitBlackMerge\Spiderman2_part2.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                rect = new Rectangle(0, originalImage.Height / 2, originalImage.Width / 2, originalImage.Height / 2);
                thirdQuarter = originalImage.Clone(rect, originalImage.PixelFormat);
                thirdQuarter.Save(@"C:\Users\user pc\Desktop\UiTMKJM\PART 7 SEPT2017_JAN2018\CSC580\RedSplitBlackMerge\Spiderman2_part3.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                rect = new Rectangle(originalImage.Width / 2, originalImage.Height / 2, originalImage.Width / 2, originalImage.Height / 2);
                forthQuarter = originalImage.Clone(rect, originalImage.PixelFormat);
                forthQuarter.Save(@"C:\Users\user pc\Desktop\UiTMKJM\PART 7 SEPT2017_JAN2018\CSC580\RedSplitBlackMerge\Spiderman2_part4.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                }

                comm.Barrier();

                //4PARTSREDTOBLACK
                Bitmap spiderman1 = new Bitmap(Image.FromFile(@"C:\Users\user pc\Desktop\UiTMKJM\PART 7 SEPT2017_JAN2018\CSC580\RedSplitBlackMerge\Spiderman2_part1.jpg"));
                Bitmap spiderman2 = new Bitmap(Image.FromFile(@"C:\Users\user pc\Desktop\UiTMKJM\PART 7 SEPT2017_JAN2018\CSC580\RedSplitBlackMerge\Spiderman2_part2.jpg"));
                Bitmap spiderman3 = new Bitmap(Image.FromFile(@"C:\Users\user pc\Desktop\UiTMKJM\PART 7 SEPT2017_JAN2018\CSC580\RedSplitBlackMerge\Spiderman2_part3.jpg"));
                Bitmap spiderman4 = new Bitmap(Image.FromFile(@"C:\Users\user pc\Desktop\UiTMKJM\PART 7 SEPT2017_JAN2018\CSC580\RedSplitBlackMerge\Spiderman2_part4.jpg"));
                
                if(comm.Rank == 0){
                for (int i = 0; i < spiderman1.Width; i++)
                {
                    for (int j = 0; j < spiderman1.Height; j++)
                    {
                        if ((spiderman1.GetPixel(i, j).R - spiderman1.GetPixel(i, j).G) > 40)
                            spiderman1.SetPixel(i, j, Color.Black);
                    }
                }
                spiderman1.Save(@"C:\Users\user pc\Desktop\UiTMKJM\PART 7 SEPT2017_JAN2018\CSC580\RedSplitBlackMerge\Spiderman2_part1B.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                }

                if(comm.Rank == 1){
                for (int i = 0; i < spiderman2.Width; i++)
                {
                    for (int j = 0; j < spiderman2.Height; j++)
                    {
                        if ((spiderman2.GetPixel(i, j).R - spiderman2.GetPixel(i, j).G) > 40)
                            spiderman2.SetPixel(i, j, Color.Black);
                    }
                }
                spiderman2.Save(@"C:\Users\user pc\Desktop\UiTMKJM\PART 7 SEPT2017_JAN2018\CSC580\RedSplitBlackMerge\Spiderman2_part2B.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                }

                if(comm.Rank == 2){
                for (int i = 0; i < spiderman3.Width; i++)
                {
                    for (int j = 0; j < spiderman3.Height; j++)
                    {
                        if ((spiderman3.GetPixel(i, j).R - spiderman3.GetPixel(i, j).G) > 40)
                            spiderman3.SetPixel(i, j, Color.Black);
                    }
                }
                spiderman3.Save(@"C:\Users\user pc\Desktop\UiTMKJM\PART 7 SEPT2017_JAN2018\CSC580\RedSplitBlackMerge\Spiderman2_part3B.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                }

                if(comm.Rank == 3){
                for (int i = 0; i < spiderman4.Width; i++)
                {
                    for (int j = 0; j < spiderman4.Height; j++)
                    {
                        if ((spiderman4.GetPixel(i, j).R - spiderman4.GetPixel(i, j).G) > 40)
                            spiderman4.SetPixel(i, j, Color.Black);
                    }
                }
                spiderman4.Save(@"C:\Users\user pc\Desktop\UiTMKJM\PART 7 SEPT2017_JAN2018\CSC580\RedSplitBlackMerge\Spiderman2_part4B.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                }

                comm.Barrier();

                if(comm.Rank == 0){
                //4PARTSBLACK-MERGED
                    Image img1 = new Bitmap(Image.FromFile(@"C:\Users\user pc\Desktop\UiTMKJM\PART 7 SEPT2017_JAN2018\CSC580\RedSplitBlackMerge\Spiderman2_part1B.jpg"));
                    Image img2 = new Bitmap(Image.FromFile(@"C:\Users\user pc\Desktop\UiTMKJM\PART 7 SEPT2017_JAN2018\CSC580\RedSplitBlackMerge\Spiderman2_part2B.jpg"));
                    Image img3 = new Bitmap(Image.FromFile(@"C:\Users\user pc\Desktop\UiTMKJM\PART 7 SEPT2017_JAN2018\CSC580\RedSplitBlackMerge\Spiderman2_part3B.jpg"));
                    Image img4 = new Bitmap(Image.FromFile(@"C:\Users\user pc\Desktop\UiTMKJM\PART 7 SEPT2017_JAN2018\CSC580\RedSplitBlackMerge\Spiderman2_part4B.jpg"));

                int width = img3.Width + img4.Width;
                int height = img1.Height + img3.Height;

                Bitmap imgFinal = new Bitmap(width, height);
                Graphics g = Graphics.FromImage(imgFinal);

                g.Clear(Color.Black);
                g.DrawImage(img1, new Point(0, 0));
                g.DrawImage(img2, new Point(img1.Width, 0));
                g.DrawImage(img3, new Point(0, img1.Height));
                g.DrawImage(img4, new Point(img1.Width, img1.Height));

                g.Dispose();
                img1.Dispose();
                img2.Dispose();
                img3.Dispose();
                img4.Dispose();

                imgFinal.Save(@"C:\Users\user pc\Desktop\UiTMKJM\PART 7 SEPT2017_JAN2018\CSC580\RedSplitBlackMerge\SpidermanColorChangedMerged.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                imgFinal.Dispose();
                }
             }
          }
     }
}
