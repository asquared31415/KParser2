using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using KParser.Conversion;
using KParser.File;

namespace KParser
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();

            var animFiles = new List<AnimFiles>();
            try
            {
                animFiles.Add(new AnimFiles
                {
                    Atlas = new AtlasFile("../../../TestAnims/oilfloater_0.png"),
                    Build = new BuildFile("../../../TestAnims/oilfloater_build.bytes"),
                    Anim = new AnimFile("../../../TestAnims/oilfloater_anim.bytes"),
                    OutDir = "../../../TestAnims/out/oilfloater"
                });
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e.Message);
            }

            foreach (var anim in animFiles)
            {
                var converter = new KAnimToScmlConverter(anim.Atlas, anim.Build, anim.Anim, anim.OutDir, Path.GetFileNameWithoutExtension(anim.Atlas.FilePath) + ".scml");
                converter.GetTexturesFile().WriteFile();
                converter.GetScmlFile().WriteFile();
            }

            stopWatch.Stop();
            var ts = stopWatch.Elapsed;
            Console.WriteLine($"Runtime: {ts.Hours:00}:{ts.Minutes:00}:{ts.Seconds:00}.{ts.Milliseconds / 10:00}");
        }
    }

    internal struct AnimFiles
    {
        public AtlasFile Atlas;
        public BuildFile Build;
        public AnimFile Anim;
        public string OutDir;
    }
}