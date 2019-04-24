using System;
using System.Threading;
using Basler.Pylon;

namespace TestBasler
{
    class Program
    {
        static void Main(string[] args)
        {
            Camera camera = new Camera();

            camera.Open(5000, TimeoutHandling.ThrowException);
            // Start grabbing.
            camera.StreamGrabber.Start();

            // Grab a number of images.
            for (int i = 0; i < 10; ++i)
            {
                // Wait for an image and then retrieve it. A timeout of 5000 ms is used.
                IGrabResult grabResult = camera.StreamGrabber.RetrieveResult(5000, TimeoutHandling.ThrowException);
                using (grabResult)
                {
                    // Image grabbed successfully?
                    if (grabResult.GrabSucceeded)
                    {
                        // Display the grabbed image.
                        ImageWindow.DisplayImage(0, grabResult);
                        ImagePersistence.Save(ImageFileFormat.Jpeg, "C:\\Users\\LocalAdmin\\Work\\Basler\\GrabImages" + "\\" + $"Image{i}" + ".jpeg", grabResult);
                    }
                    else
                    {
                        Console.WriteLine("Error: {0} {1}", grabResult.ErrorCode, grabResult.ErrorDescription);
                    }
                }

                Thread.Sleep(500);
            }
            // Stop grabbing.
            camera.StreamGrabber.Stop();

            camera.Close();
        }
    }
}
