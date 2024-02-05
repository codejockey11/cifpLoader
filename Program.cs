using System;
using System.IO;
using System.IO.Compression;
using System.Collections.Generic;

namespace cifpLoader
{
    class Program
    {
        static Char[] recordType_001_05 = new Char[05];
        static Char[] facilityId_007_04 = new Char[04];
        static Char[] subsectionCode_013_01 = new Char[01];
        static Char[] siapId_014_05 = new Char[05];
        static Char[] transition_021_05 = new Char[05];
        static Char[] sequenceNbr_027_03 = new Char[03];
        static Char[] fixId_030_05 = new Char[05];
        static Char[] region_035_02 = new Char[02];
        static Char[] turnDirection_044_01 = new Char[01];
        static Char[] legType_048_02 = new Char[02];
        static Char[] navaid_051_04 = new Char[04];
        static Char[] theta_063_04 = new Char[04];
        static Char[] rho_067_04 = new Char[04];
        static Char[] magCourse_071_04 = new Char[04];
        static Char[] holdDistTime_075_04 = new Char[04];
        static Char[] altitude_085_05 = new Char[05];

        static StreamWriter ofileCIFP = new StreamWriter("cifp.txt");

        static void Main(String[] args)
        {
            String userprofileFolder = Environment.GetEnvironmentVariable("USERPROFILE");
            
            String[] fileEntries = Directory.GetFiles(userprofileFolder + "\\Downloads\\", "CIFP_*.zip");

            ZipArchive archive = ZipFile.OpenRead(fileEntries[0]);

            foreach (ZipArchiveEntry entry in archive.Entries)
            {
                //if (entry.FullName.EndsWith(".ari", StringComparison.OrdinalIgnoreCase))
                if (entry.Name.Equals("FAACIFP18", StringComparison.OrdinalIgnoreCase))
                {
                    entry.ExtractToFile("FAACIFP18.txt", true);

                    break;
                }
            }
            
            StreamReader file = new StreamReader("FAACIFP18.txt");

            String rec = file.ReadLine();

            while (!file.EndOfStream)
            {
                ProcessRecord(rec);
                
                rec = file.ReadLine();
            }

            ProcessRecord(rec);

            file.Close();

            ofileCIFP.Close();
        }

        static void ProcessRecord(String record)
        {
            recordType_001_05 = record.ToCharArray(0, 5);

            String rt = new String(recordType_001_05);

            if ((String.Compare(rt, "SUSAP") == 0) || (String.Compare(rt, "SCANP") == 0))
            {
                facilityId_007_04 = record.ToCharArray(6, 4);
                subsectionCode_013_01 = record.ToCharArray(12, 1);
                siapId_014_05 = record.ToCharArray(13, 5);
                transition_021_05 = record.ToCharArray(20, 5);
                sequenceNbr_027_03 = record.ToCharArray(26, 3);
                fixId_030_05 = record.ToCharArray(29, 5);
                region_035_02 = record.ToCharArray(34, 2);
                turnDirection_044_01 = record.ToCharArray(43, 1);
                legType_048_02 = record.ToCharArray(47, 2);
                navaid_051_04 = record.ToCharArray(50, 4);
                theta_063_04 = record.ToCharArray(62, 4);
                rho_067_04 = record.ToCharArray(66, 4);
                magCourse_071_04 = record.ToCharArray(70, 4);
                holdDistTime_075_04 = record.ToCharArray(74, 4);
                altitude_085_05 = record.ToCharArray(84, 5);

                if ( ((subsectionCode_013_01[0] == 'D') || (subsectionCode_013_01[0] == 'E') || (subsectionCode_013_01[0] == 'F')) && (legType_048_02[0] > ' '))
                {
                    String s = new String(facilityId_007_04).Trim();
                    
                    ofileCIFP.Write(s);
                    ofileCIFP.Write('~');

                    s = new String(subsectionCode_013_01).Trim();
                    
                    ofileCIFP.Write(s);
                    ofileCIFP.Write('~');

                    s = new String(siapId_014_05).Trim();
                    
                    ofileCIFP.Write(s);
                    ofileCIFP.Write('~');

                    s = new String(transition_021_05).Trim();
                    
                    ofileCIFP.Write(s);
                    ofileCIFP.Write('~');

                    s = new String(sequenceNbr_027_03).Trim();
                    
                    ofileCIFP.Write(s);
                    ofileCIFP.Write('~');

                    s = new String(fixId_030_05).Trim();
                    
                    ofileCIFP.Write(s);
                    ofileCIFP.Write('~');

                    s = new String(region_035_02).Trim();

                    ofileCIFP.Write(s);
                    ofileCIFP.Write('~');

                    s = new String(turnDirection_044_01).Trim();
                    
                    ofileCIFP.Write(s);
                    ofileCIFP.Write('~');

                    s = new String(legType_048_02).Trim();
                    
                    ofileCIFP.Write(s);
                    ofileCIFP.Write('~');

                    s = new String(navaid_051_04).Trim();
                    
                    ofileCIFP.Write(s);
                    ofileCIFP.Write('~');

                    s = new String(theta_063_04).Trim();
                    
                    if (theta_063_04[0] > ' ')
                    {
                        ofileCIFP.Write(theta_063_04[0]);
                        ofileCIFP.Write(theta_063_04[1]);
                        ofileCIFP.Write(theta_063_04[2]);
                        ofileCIFP.Write('.');
                        ofileCIFP.Write(theta_063_04[3]);
                    }
                    
                    ofileCIFP.Write('~');

                    s = new String(rho_067_04).Trim();
                    
                    if (rho_067_04[0] > ' ')
                    {
                        ofileCIFP.Write(rho_067_04[0]);
                        ofileCIFP.Write(rho_067_04[1]);
                        ofileCIFP.Write(rho_067_04[2]);
                        ofileCIFP.Write('.');
                        ofileCIFP.Write(rho_067_04[3]);
                    }
                    
                    ofileCIFP.Write('~');

                    s = new String(magCourse_071_04).Trim();
                    
                    if (magCourse_071_04[0] > ' ')
                    {
                        ofileCIFP.Write(magCourse_071_04[0]);
                        ofileCIFP.Write(magCourse_071_04[1]);
                        ofileCIFP.Write(magCourse_071_04[2]);
                        ofileCIFP.Write('.');
                        ofileCIFP.Write(magCourse_071_04[3]);
                    }
                    
                    ofileCIFP.Write('~');

                    s = new String(holdDistTime_075_04).Trim();
                    
                    if (holdDistTime_075_04[0] == 'T')
                    {
                        ofileCIFP.Write(holdDistTime_075_04[0]);
                        ofileCIFP.Write(holdDistTime_075_04[1]);
                        ofileCIFP.Write(holdDistTime_075_04[2]);
                        ofileCIFP.Write('.');
                        ofileCIFP.Write(holdDistTime_075_04[3]);
                    }
                    else if (holdDistTime_075_04[0] > ' ')
                    {
                        ofileCIFP.Write(holdDistTime_075_04[0]);
                        ofileCIFP.Write(holdDistTime_075_04[1]);
                        ofileCIFP.Write(holdDistTime_075_04[2]);
                        ofileCIFP.Write('.');
                        ofileCIFP.Write(holdDistTime_075_04[3]);
                    }
                    
                    ofileCIFP.Write('~');

                    s = new String(altitude_085_05).Trim();
                    
                    ofileCIFP.Write(s);

                    // point id and key
                    ofileCIFP.Write('~');
                    ofileCIFP.Write('~');

                    // navaid id and key
                    ofileCIFP.Write('~');
                    ofileCIFP.Write('~');

                    ofileCIFP.Write(ofileCIFP.NewLine);
                }
            }

        }

    }
}
