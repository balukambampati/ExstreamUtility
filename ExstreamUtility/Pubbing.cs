using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ExstreamUtility
{
    class Pubbing
    {
        public string AppName { get; set; }
        public string PubPath { get; set; }
        public string CntrlPath { get; set; }
        public string ApprovalState { get; set; }
        public string ExstreamUser { get; set; }
        public string ExstreamVersion { get; set; }
        public string PackagerPath { get; set; }
        public string ExstreamDSN { get; set; }
        public string PubName { get; set; }
        public string PubbingState { get; set; }

        public int StartPubbing()
        {
            int result = 0;
            string outmsg = "";

            switch (ApprovalState)
            {
                case "PEER":
                    PubbingState = "-VERSIONCUSTOM=PENDING PEER APPROVAL";
                    break;
                case "INTG":
                    PubbingState = "-VERSIONCUSTOM=PENDING INTG APPROVAL";
                    break;
                case "FNST":
                    PubbingState = "-VERSIONCUSTOM=PENDING FNST APPROVAL";
                    break;
                case "USER":
                    PubbingState = "-VERSIONCUSTOM=PENDING USER APPROVAL";
                    break;
                case "URC":
                    PubbingState = "-VERSIONCUSTOM=PENDING URC/LEGAL APPROVAL";
                    break;
                case "APPROVED":
                    PubbingState = "-WIP=APPROVED";
                    break;
                case "WIP":
                    PubbingState = "-WIP=WIP";
                    break;
                default:
                    System.Console.WriteLine("*******************************************" + DateTime.Now.ToString() + "*******************************************");
                    System.Console.WriteLine("Pass valid 'Approval State' argument");
                    System.Console.WriteLine("Packaging aborted with return code 1");
                    return 1;
            }
            System.Console.WriteLine("*******************************************" + DateTime.Now.ToString() + "*******************************************");
            System.Console.WriteLine(DateTime.Now.ToString() + ": " + AppName + " packaging started with approval state: " + ApprovalState);
            try
            {
                string[] controlLines = { "-APPLICATION=" + AppName, "-DSN=" + ExstreamDSN, "-ExstreamUser=" + ExstreamUser, "-APPLICATION_MODE=SBCS", "-MESSAGEFILE=" + PubPath + AppName + "_msg.dat", "-PACKAGEFILE=" + PubPath + PubName };
                File.WriteAllLines(CntrlPath + AppName + ".dat", controlLines);
                ProcessStartInfo psi = new ProcessStartInfo();
                psi.WorkingDirectory = Path.GetDirectoryName(PackagerPath);
                psi.CreateNoWindow = true;
                psi.FileName = PackagerPath + "Packager.exe";
                psi.WindowStyle = ProcessWindowStyle.Normal; //Hidden
                psi.Arguments = "\"" + "-CONTROLFILE=" + CntrlPath + AppName + ".dat" + "\"" + " " + "\"" + PubbingState + "\"";
                psi.UseShellExecute = false;
                psi.RedirectStandardOutput = true;
                Process exeProcess = Process.Start(psi);
                exeProcess.WaitForExit();
                result = exeProcess.ExitCode;
                outmsg = exeProcess.StandardOutput.ReadToEnd();
                //exeProcess.
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(DateTime.Now.ToString() + ":Packaging execution failed");
                System.Console.WriteLine(ex.InnerException.ToString());
                return result;
            }
            finally
            {
                System.Console.Write(DateTime.Now.ToString() + " : " + Regex.Replace(outmsg, @"^\s+$[\r\n]*", string.Empty, RegexOptions.Multiline));
            }

            return result;
        }

        public void readMessagefile()
        {
            string line = "";
            int errCnt = 0;
            int warnCnt = 0;
            Boolean breakflag = false;
            string patE = @"EX\d{6}E";
            string patS = @"EX\d{6}S";
            string patW = @"EX\d{6}W";
            if (!File.Exists(PubPath + AppName + "_msg.dat"))
            {
                System.Console.WriteLine(DateTime.Now.ToString() + ":Packaging failure - No message file");
            }
            else
            {
                Regex rE = new Regex(patE, RegexOptions.IgnoreCase);
                Regex rS = new Regex(patS, RegexOptions.IgnoreCase);
                Regex rW = new Regex(patW, RegexOptions.IgnoreCase);
                using (StreamReader file = new StreamReader(PubPath + AppName + "_msg.dat"))
                {
                    while ((line = file.ReadLine()) != null)
                    {
                        if (line.Contains("Packaging complete"))
                        {
                            breakflag = true;
                        }
                        Match mE = rE.Match(line);
                        Match mS = rS.Match(line);
                        Match mW = rW.Match(line);

                        if (mS.Success)
                        {
                            System.Console.WriteLine(DateTime.Now.ToString() + ": Packaging failure with severe errors " + mS.ToString());

                        }
                        else if (mE.Success)
                        {
                            System.Console.WriteLine(DateTime.Now.ToString() + ": Packaging complete with errors " + mE.ToString());

                        }
                        else if (mW.Success)
                        {
                            warnCnt += 1;
                        }
                    }
                    if (!breakflag)
                    {
                        System.Console.WriteLine(DateTime.Now.ToString() + ": Packaging failure");
                    }
                    if (warnCnt > 0)
                    {
                        System.Console.WriteLine(DateTime.Now.ToString() + ": Packaging complete with warnings");
                    }
                }
            }
        }
    }
}
