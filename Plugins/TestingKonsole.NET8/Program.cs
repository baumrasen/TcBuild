
using System;
using System.Collections;
using System.IO;
using System.Diagnostics;
using System.Collections.Specialized;
using System.Threading;

using TcPluginBase;
using TcPluginBase.Content;
using TcPluginBase.FileSystem;

using InLoox.PM.Domain.Model.Aggregates.Api;
using Simple.OData.Client;
using System.Runtime;
using System.Reflection;
using System.Text;

namespace TestingKonsole.NET8
{
    class Program
    {
        //private static InlooxInfoList _iil = new InlooxInfoList();

        //// Define the regular expression for Inloox projekt number
        //// @"\\(\d\d\d\d_\d\d\d\d\d]*)"
        //// @"^\\(\d\d\d\d_\d\d\d\d\d].)"
        //private static Regex rx = new Regex(@"(\d\d\d\d_\d\d\d\d\d]*)", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        private static FindData findData;
        private static object ff;
        private static bool ff_move;

        private static object fff;
        private static bool fff_move;

        private static object ffff;
        private static bool ffff_move;


        private static string fffff;
        private static string fffff_move;

        private static DateTime starttime;
        private static DateTime endtime;

        //private static InlooxInfoList iil = new InlooxInfoList();

        private static bool debug = false;

        private static ODataClient client;

        static async Task Main(string[] args)
        {

            starttime = DateTime.Now;

            var EndPoint = new Uri("https://inloox-test.ibb-engineering.de");
            var EndPointOdata = new Uri(EndPoint, "/api/v1/odata/");

            // temp
            var token = "ilx_CYp2sCdyYCVzJS9OjBYJ2GR67Mu3UZRJ";

            Console.WriteLine("console is starting...");

            var settings = new ODataClientSettings(EndPointOdata);
            settings.BeforeRequest += delegate (HttpRequestMessage message)
            {
                message.Headers.Add("x-api-key", token);
            };
            client = new ODataClient(settings);

            // example 1: Show name of your account
            var accountInfo = await GetAccountInfo();

            Console.WriteLine(accountInfo.Name);

            // example 2: Get projects
            //var projects = await GetProjects();

            // example 3: Get time entries for one month
            // await GetAllTimeEntriesForMonth(DateTime.Now, a => Console.WriteLine(a));


            //iil = InlooxDefaults.IiL;

            //WriteLine(String.Format("INLOOX-Server: {0}", InlooxDefaults.IiL.GetInlooxInfo()));

            //for (int i = 0; i < 2; i++)
            //{
            //    doWorkWFX();
            //}

            // change AuthToken in Registry
            //AppSettingsManager.SetValueString("AuthToken", "12345678");

            //InlooxLogin pswDialog = new InlooxLogin(this, packedFile);
            //if (pswDialog.ShowDialog() == DialogResult.OK)
            //{
            //    password = pswDialog.Password;
            //}

            bool gotauthtoken = false;

            //string authtoken = "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyy dummy aus Program.cs";
            string errormsg = "";
            // Uri _uri_ = new Uri("http://v-smg-101:8989/odata/");

            // https://inloox-test.ibb-engineering.de/api/v1/swagger/index.html
            Uri _uri_ = EndPointOdata;

            //string tokenpath = "../oauth2/token";

            //string username = @"ibb\a.vogt";
            //string password = "test";

            // bool checkauthtoken = iil.CheckCurrentToken(_uri_, authtoken, out errormsg);
            bool checkauthtoken = true;
            // bool gotauthtoken = iil.SetLoginPreferences(new Uri("http://v-smg-101.ibb.time-partner.com:8989/odata/"), "ibb\a.vogt", "05Fossy05!", out authtoken, out errormsg);

            if (checkauthtoken)
                gotauthtoken = true;
            else
            {

                //InlooxLogin InlooxLoginDialog = new AV.TotalCommander.TcPlugins.InlooxContent.InlooxLogin("InlooxAccessToken");

                //if (InlooxLoginDialog.ShowDialog() == DialogResult.OK)
                //{
                //    authtoken = InlooxLoginDialog.AuthToken;
                //}
                //if (String.IsNullOrEmpty(authtoken))
                //{
                //    // return InlooxLoginResult.Cancel;
                //}

                //// gotauthtoken = iil.SetLoginPreferences(_uri_, tokenpath, username, password, out authtoken, out errormsg);
                //gotauthtoken = iil.CheckCurrentToken(_uri_, authtoken, out errormsg);
            }

            if (gotauthtoken)
            {
                Console.WriteLine(String.Format("authtoken is there"));
            }
            else
            {
                Console.WriteLine(String.Format("Error on getting authoken: {0}", errormsg));
                Console.ReadLine();
                return;
            }

            //await doContentAsync();

            //for (int i = 0; i < 2; i++)
            //{
            //    doWorkWFX();
            //}

            //for (int i = 0; i < 3; i++)
            //{
            //    GetValueFlags f = GetValueFlags.DelayIfSlow;

            //    if (i > 0)
            //        f = GetValueFlags.None;

            //    doWorkWDX(f);
            //    Thread.Sleep(2000);
            //}

            endtime = DateTime.Now;

            TimeSpan diff = endtime - starttime;

            Console.WriteLine(String.Format("ENTER to close the application ({0}.{1})", diff.Seconds, diff.Milliseconds));
            Console.ReadLine();

        }

        //private static void doWorkWFX()
        //{

        //    findData = null;

        //    Console.WriteLine("doWorkWFX START now try next step...");
        //    ff = FindFirst("\\", out findData);

        //    do
        //    {
        //        ff_move = FindNext(ref ff, out findData);
        //    }
        //    while (ff_move);

        //    Console.WriteLine("now try next step...");

        //    fff = FindFirst("\\2019_09624", out findData);

        //    do
        //    {
        //        fff_move = FindNext(ref fff, out findData);
        //    }
        //    while (fff_move);

        //    Console.WriteLine("now try next step...");

        //    ffff = FindFirst("\\2018_09067\\10_Datenaustausch", out findData);

        //    //do
        //    //{
        //    //    ffff_move = FindNext(ref ffff, out findData);
        //    //}
        //    //while (ffff_move);

        //    //fffff = "2018_09067\\10_Datenaustausch\\";

        //    //    fffff_move = PrepareInlooxPathSubstring(fffff);

        //    Console.WriteLine("doWorkWFX  END");
        //}

        private async Task doContentAsync()
        {

            Boolean gotinfo = true;
            String division = "Creo";
            String projectnumber = "2024_11294";

            //InlooxInfo iinfo = null;

            var projects = await GetProjectFromNumber(projectnumber);

            //if ((gotinfo) && (division != ""))
            //{
            //    iinfo = iil.GetInlooxInfoFromDivision(division, projectnumber);
            //}
            //else if ((gotinfo) && (projectnumber != ""))
            //{
            //    iinfo = iil.GetInlooxInfoFromProjectNumber(projectnumber);
            //}
            //Console.WriteLine(String.Format("Das Projekt {0} hat als Kunde {1} hinterlegt.", iinfo.Number, iinfo.ClientName));

            foreach (ApiDynamicProject p in projects)
            {
                Console.WriteLine(p.Project_Number + " - " + p.Project_Name);
            }
            

        }

        async Task<IEnumerable<ApiDynamicProject>> GetProjectFromNumber(String ProjectNumber)
        {
            // this will only return the first 100 projects
            // for paging and filtering see sample 3
            if (client == null)
                throw new InvalidOperationException("Initialize client first");

            var annotations = new ODataFeedAnnotations();
            var pl = (await client.For<ApiDynamicProject>("DynamicProject").
                Filter(p =>
                    p.Project_Number == ProjectNumber
                )
                .FindEntriesAsync()).ToList();

            while (annotations.NextPageLink != null)
            {
                pl.AddRange(await client
                    .For<ApiDynamicProject>("DynamicProject")
                    .FindEntriesAsync(annotations.NextPageLink, annotations));

                // loadedFunc($"Loaded {projects.Count()} entries");
            }

            return pl;
        }

        //private static void doWorkWDX(GetValueFlags flag)
        //{

        //    Console.WriteLine("doWorkWDX START now try next step...");

        //    StringDictionary set = new StringDictionary();

        //    InlooxContent ct = new InlooxContent(set);

        //    foreach (string s in ContentTestString())
        //    {
        //        string fieldValue5 = "";
        //        ContentFieldType fieldType5;
        //        string fieldValue6 = "";
        //        ContentFieldType fieldType6;

        //        GetValueResult g5 = ct.GetValue(s, 5, 0, 2047, flag, out fieldValue5, out fieldType5);
        //        GetValueResult g6 = ct.GetValue(s, 6, 0, 2047, flag, out fieldValue6, out fieldType6);

        //        Console.WriteLine(String.Format("doWorkWDX  info {0} {1}", fieldValue5, fieldValue6));
        //    }

        //    Console.WriteLine(String.Format("doWorkWDX  info InlooxInfoListInfo2 {0}", InlooxDefaults.IiL.GetInlooxInfo2()));
        //    //findData = null;

        //    //Console.WriteLine("doWork START now try next step...");
        //    //ff = FindFirst("\\", out findData);

        //    //do
        //    //{
        //    //    ff_move = FindNext(ref ff, out findData);
        //    //}
        //    //while (ff_move);

        //    //Console.WriteLine("now try next step...");

        //    //fff = FindFirst("\\2018_09067", out findData);

        //    //do
        //    //{
        //    //    fff_move = FindNext(ref fff, out findData);
        //    //}
        //    //while (fff_move);

        //    //Console.WriteLine("now try next step...");

        //    //ffff = FindFirst("\\2018_09067\\10_Datenaustausch", out findData);

        //    //do
        //    //{
        //    //    ffff_move = FindNext(ref ffff, out findData);
        //    //}
        //    //while (ffff_move);

        //    //fffff = "2018_09067\\10_Datenaustausch\\";

        //    //    fffff_move = PrepareInlooxPathSubstring(fffff);


        //    endtime = DateTime.Now;

        //    TimeSpan diff = endtime - starttime;

        //    Console.WriteLine(String.Format("doWorkWDX END ({0}.{1})", diff.Seconds, diff.Milliseconds));

        //    //Console.WriteLine("doWorkWDX  END");
        //}

        //public static object FindFirst(string path, out FindData findData)
        //{

        //    WriteLine(String.Format("FindFirst - START: {0}", path));

        //    findData = null;
        //    if (path == "\\")
        //    {

        //        //root, get drive names
        //        //IEnumerator inlooxEnum = InlooxDefaults.IiL.FullFavoritesInfoArray.GetEnumerator();
        //        IEnumerator inlooxEnum = InlooxDefaults.IiL.FullProjectsInfoArray.GetEnumerator();
        //        if (inlooxEnum.MoveNext())
        //        {
        //            InlooxInfo drive = (InlooxInfo)inlooxEnum.Current;
        //            if (drive != null)
        //            {
        //                WriteLine(String.Format(" FindFirst Inloox GetFindData: {0}", drive.DocumentFolder));
        //                GetInlooxProjectData(drive.DocumentFolder, out findData);
        //            }
        //            return inlooxEnum;
        //        }
        //        return null;

        //        ////root, get drive names
        //        //IEnumerator driveEnum = DriveInfo.GetDrives().GetEnumerator();
        //        //if (driveEnum.MoveNext())
        //        //{
        //        //    DriveInfo drive = (DriveInfo)driveEnum.Current;
        //        //    if (drive != null)
        //        //        GetFindData(drive.Name, out findData);
        //        //    return driveEnum;
        //        //}
        //        //return null;

        //    }

        //    //inloox project, get directory info
        //    if (path.Substring(0, 1) == "\\")
        //    {
        //        IEnumerator dirInlooxEnum = Directory.GetFileSystemEntries(InlooxDefaults.PrepareInlooxPath(path)).GetEnumerator();
        //        if (dirInlooxEnum.MoveNext())
        //        {
        //            string fsEntry = (string)dirInlooxEnum.Current;
        //            WriteLine(String.Format(" FindFirst dirInlooxEnum: {0}", fsEntry));

        //            GetFindData(fsEntry, out findData);
        //            return dirInlooxEnum;
        //        }
        //    }

        //    //directories
        //    IEnumerator dirFileEnum = Directory.GetFileSystemEntries(PreparePath(path)).GetEnumerator();
        //    if (dirFileEnum.MoveNext())
        //    {
        //        string fsEntry = (string)dirFileEnum.Current;
        //        WriteLine(String.Format(" FindFirst dirFileEnum: {0}", fsEntry));

        //        GetFindData(fsEntry, out findData);
        //        return dirFileEnum;
        //    }
        //    return null;
        //}

        //public static bool FindNext(ref object o, out FindData findData)
        //{
        //    findData = null;
        //    if (!(o is IEnumerator))
        //        return false;
        //    IEnumerator fsEnum = (IEnumerator)o;
        //    if (fsEnum.MoveNext())
        //    {
        //        object current = fsEnum.Current;
        //        if (current != null)
        //        {
        //            if (current is DriveInfo)
        //            {
        //                WriteLine(String.Format("FindNext - DriveInfo: {0}", ((DriveInfo)current).Name));
        //                GetFindData(((DriveInfo)current).Name, out findData);
        //            }

        //            else if (current is InlooxInfo)
        //            {
        //                WriteLine(String.Format("FindNext - InlooxInfo: {0}", ((InlooxInfo)current).DocumentFolder));
        //                GetInlooxProjectData(((InlooxInfo)current).DocumentFolder, out findData);
        //            }

        //            else if (current is string)
        //            {
        //                WriteLine(String.Format("FindNext - current: {0}", (string)current));
        //                GetFindData((string)current, out findData);
        //            }

        //            else
        //                throw new InvalidOperationException("Unknown type in FindNext: " + current.GetType().FullName);
        //            return true;
        //        }
        //    }
        //    return false;
        //}

        //private static void GetFindData(string path, out FindData findData)
        //{
        //    WriteLine(String.Format(" GetFindData - START: {0}", (string)path));
        //    if (path.Length == 3 && path.EndsWith(":\\"))
        //    {
        //        WriteLine(String.Format("  GetFindData - path.Length==3: {0}", (string)path));
        //        DriveInfo dInfo = new DriveInfo(path.Substring(0, 1));
        //        if (Directory.Exists(path))
        //            findData = new FindData(path.Substring(0, 2), (ulong)dInfo.TotalSize, FileAttributes.Directory);
        //        else
        //            findData = new FindData(path.Substring(0, 2), FileAttributes.Directory);
        //    }
        //    else if (Directory.Exists(path))
        //    {
        //        WriteLine(String.Format("  GetFindData - DirExist: {0}", (string)path));
        //        DirectoryInfo info = new DirectoryInfo(path);
        //        findData = new FindData(Path.GetFileName(path), 0, info.Attributes,
        //            info.LastWriteTime, info.CreationTime, info.LastAccessTime);
        //    }
        //    else if (File.Exists(path))
        //    {
        //        WriteLine(String.Format("  GetFindData - FileExist: {0}", (string)path));
        //        FileInfo info = new FileInfo(path);
        //        findData = new FindData(Path.GetFileName(path), (ulong)info.Length, info.Attributes,
        //            info.LastWriteTime, info.CreationTime, info.LastAccessTime);
        //    }
        //    else if (path.StartsWith("\\\\") && path.IndexOf('\\', 2) > 2)
        //    {
        //        WriteLine(String.Format("  GetFindData - StartWith \\\\: {0}", (string)path));
        //        findData = new FindData(Path.GetFileName(path), FileAttributes.Directory);
        //    }
        //    else
        //        throw new FileNotFoundException("File not found", path);
        //}

        //private static void GetInlooxProjectData(string path, out FindData findData)
        //{
        //    WriteLine(String.Format(" GetInlooxProjectData - START: {0}", (string)path));

        //    //string drive;
        //    //string unc;
        //    //string division;
        //    //string projectnumber;

        //    //bool gotinfo = InlooxDefaults.GetDataFromPath(path, out drive, out unc, out division, out projectnumber);
        //    ////string pn = InlooxDefaults.GetDataFromPath(path);
        //    //WriteLine(String.Format(" GetInlooxProjectData -    PN: {0}", (string)projectnumber));

        //    string drive;
        //    string unc;
        //    string division;
        //    string projectnumber;

        //    bool gotinfo = InlooxDefaults.GetDataFromPath(path, out drive, out unc, out division, out projectnumber);
        //    if (gotinfo)
        //        Debug.WriteLine(String.Format("         ContentTest - GetValue  {0}{1} / {2} / {3}", drive, unc, division, projectnumber));
        //    else
        //        Debug.WriteLine(String.Format("         ContentTest - GetValue - NO MATCH for Projectnumber, etc"));

        //    if (Directory.Exists(path))
        //    {

        //        //InlooxInfo iinfo = iil.GetInlooxInfoFromDocumentFolder(fileName);
        //        InlooxInfo iinfo = null;
        //        //if ((gotinfo) && (projectnumber != ""))
        //        //{
        //        //    iinfo = iil.GetInlooxInfoFromProjectNumber(projectnumber);
        //        //}
        //        if ((gotinfo) && (division != ""))
        //        {
        //            iinfo = iil.GetInlooxInfoFromDivision(division, projectnumber);
        //        }
        //        else if ((gotinfo) && (projectnumber != ""))
        //        {
        //            iinfo = iil.GetInlooxInfoFromProjectNumber(projectnumber);
        //        }

        //        WriteLine(String.Format("  GetInlooxProjectData - DirExist: {0}", (string)path));
        //        DirectoryInfo info = new DirectoryInfo(path);
        //        //InlooxInfo iinfo = iif.GetInlooxInfoFromDocumentFolder(path);
        //        //WriteLine(String.Format("  GetInlooxProjectData - InlooxInfo: {0}, {1}, {2}", iinfo.Number, iinfo.ClientName, iinfo.ProjectName));
        //        findData = new FindData(Path.GetFileName(path), 0, info.Attributes,
        //            info.LastWriteTime, info.CreationTime, info.LastAccessTime);
        //    }
        //    else if (File.Exists(path))
        //    {
        //        WriteLine(String.Format("  GetInlooxProjectData - FileExist: {0}", (string)path));
        //        FileInfo info = new FileInfo(path);
        //        findData = new FindData(Path.GetFileName(path), (ulong)info.Length, info.Attributes,
        //            info.LastWriteTime, info.CreationTime, info.LastAccessTime);
        //    }
        //    else if (path == null)
        //    {
        //        findData = null;
        //    }
        //    else if (path.StartsWith("\\\\") && path.IndexOf('\\', 2) > 2)
        //    {
        //        WriteLine(String.Format("  GetInlooxProjectData - StartWith \\\\: {0}", (string)path));
        //        findData = new FindData(Path.GetFileName(path), FileAttributes.Directory);
        //    }
        //    else
        //    {
        //        findData = null;
        //        //throw new FileNotFoundException("File not found", path);
        //    }

        //}

        //private static string PreparePath(string path)
        //{
        //    return path.Substring(1) + "\\";
        //}


        //private static void WriteLine(string text1)
        //{
        //    WriteLine(text1, debug);
        //}

        //private static void WriteLine(string text1, bool debug)
        //{
        //    if (debug)
        //        Console.WriteLine(text1);
        //}


        //private static string[] ContentTestString()
        //{
        //    string[] r = new string[213] {
        //            "s:\\Konstruktion\\ProE\\Projekte\\2016_08173\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2016_08177\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2016_08181\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2016_08184\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2016_08190\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2016_08385\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2016_08430\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2016_08435\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2016_08441\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2016_08446\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2016_08447\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2016_08452\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2016_08453\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2016_08461\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2016_08463\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2016_08464\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2016_08467\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2016_08469\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2016_08481\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2016_08494\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2016_08495\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2016_08496\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2017_08540\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2017_08542\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2017_08554\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2017_08562\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2017_08569\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2017_08571\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2017_08578\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2017_08581\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2017_08582\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2017_08584\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2017_08590\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2017_08595\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2017_08597\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2017_08601\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2017_08615\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2017_08619\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2017_08620\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2017_08624\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2017_08629\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2017_08632\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2017_08636\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2017_08639\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2017_08641\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2017_08642\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2017_08643\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2017_08644\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2017_08647\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2017_08651\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2017_08656\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2017_08666\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2017_08676\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2017_08686\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2017_08700\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2017_08713\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2017_08715\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2017_08716\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2017_08720\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2017_08721\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2017_08744\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2017_08745\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2017_08746\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2017_08752\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2017_08753\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2017_08755\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2017_08759\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2017_08760\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2017_08770\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2017_08771\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2017_08798\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2017_08801\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2017_08806\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2017_08810\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2017_08818\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2017_08819\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2017_08820\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2017_08851\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2017_08853\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2017_08857\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2017_08864\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2017_08866\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2017_08868\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2017_08869\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2017_08870\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2017_08872\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2017_08876\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2017_08881\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2017_08884\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2017_08886\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2017_08893\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2017_08894\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2017_08897\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2017_08903\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2017_08907\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2017_08910\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2017_08911\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2017_08913\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2017_08919\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2017_08921\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2018_08930\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2018_08931\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2018_08932\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2018_08933\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2018_08942\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2018_08943\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2018_08945\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2018_08949\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2018_08954\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2018_08955\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2018_08957\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2018_08959\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2018_08962\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2018_08963\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2018_08971\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2018_08972\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2018_08973\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2018_08974\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2018_08976\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2018_08977\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2018_08979\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2018_08983\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2018_08988\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2018_08989\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2018_08992\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2018_09015\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2018_09016\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2018_09017\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2018_09021\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2018_09027\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2018_09031\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2018_09033\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2018_09034\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2018_09045\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2018_09048\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2018_09063\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2018_09067\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2018_09081\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2018_09085\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2018_09098\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2018_09103\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2018_09108\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2018_09111\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2018_09112\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2018_09113\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2018_09124\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2018_09163\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2018_09170\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2018_09194\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2018_09195\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2018_09199\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2018_09201\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2018_09208\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2018_09213\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2018_09218\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2018_09219\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2018_09221\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2018_09223\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2018_09233\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2018_09235\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2018_09238\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2018_09240\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2018_09250\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2018_09251\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2018_09255\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2018_09271\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2018_09277\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2018_09279\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2018_09283\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2018_09286\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2018_09287\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2018_09288\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2018_09293\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2018_09296\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2018_09297\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2018_09299\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2018_09309\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2018_09316\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2018_09324\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2018_09333\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2018_09334\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2018_09336\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2018_09366\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2018_09378\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2018_09384\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2018_09392\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2018_09412\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2018_09413\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2018_09415\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2018_09423\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2018_09424\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2018_09426\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2018_09433\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2018_09436\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2018_09544\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2019_09443\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2019_09444\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2019_09446\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2019_09450\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2019_09453\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2019_09468\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2019_09469\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2019_09472\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2019_09473\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2019_09496\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2019_09500\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2019_09517\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2019_09518\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2019_09535\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2019_09543\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2019_09547\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2019_09550\\",
        //            "s:\\Konstruktion\\ProE\\Projekte\\2018_09544\\"
        //        };

        //    return r;
        //}





        // example 1
        static async Task<ApiAccountInfo> GetAccountInfo()
        {
            if (client == null)
                throw new InvalidOperationException("Initialize client first");

            //return await client.For<ApiAccountInfo>("AccountInfo").FindEntryAsync();


            try
            {
                //The code that causes the error goes here.
                return await client.For<ApiAccountInfo>("AccountInfo").FindEntryAsync();
            }
            catch (ReflectionTypeLoadException ex)
            {
                StringBuilder sb = new StringBuilder();
                foreach (Exception exSub in ex.LoaderExceptions)
                {
                    sb.AppendLine(exSub.Message);
                    FileNotFoundException exFileNotFound = exSub as FileNotFoundException;
                    if (exFileNotFound != null)
                    {
                        if (!string.IsNullOrEmpty(exFileNotFound.FusionLog))
                        {
                            sb.AppendLine("Fusion Log:");
                            sb.AppendLine(exFileNotFound.FusionLog);
                        }
                    }
                    sb.AppendLine();
                }
                string errorMessage = sb.ToString();
                //Display or log the error based on your application.
            }

            return await client.For<ApiAccountInfo>("AccountInfo").FindEntryAsync();
        }


        // example 2
        static async Task<IEnumerable<ApiProject>> GetProjects()
        {
            // this will only return the first 100 projects
            // for paging and filtering see sample 3
            if (client == null)
                throw new InvalidOperationException("Initialize client first");

            return await client.For<ApiProject>("Project").FindEntriesAsync();
        }


        // example 3
        static async Task<List<ApiDynamicTimeEntry>> GetAllTimeEntriesForMonth(DateTime month, Action<string> loadedFunc)
        {
            if (client == null)
                throw new InvalidOperationException("Initialize client first");

            var filterStart = new DateTime(month.Year, month.Month, 1);
            var filterEnd = new DateTime(month.Year, month.Month, 1).AddMonths(1);

            var annotations = new ODataFeedAnnotations();
            var timeentries = (await client.For<ApiDynamicTimeEntry>("DynamicTimeEntry")
                .Filter(k =>
                    k.TimeEntry_StartDateTime > filterStart &&
                    k.TimeEntry_EndDateTime < filterEnd
                )
                .FindEntriesAsync(annotations)).ToList();

            while (annotations.NextPageLink != null)
            {
                timeentries.AddRange(await client
                    .For<ApiDynamicTimeEntry>("DynamicTimeEntry")
                    .FindEntriesAsync(annotations.NextPageLink, annotations));

                loadedFunc($"Loaded {timeentries.Count()} entries");
            }

            return timeentries;
        }


        // example 4
        static async Task CreateTimeEntry(Guid projectId, string name, DateTime start)
        {
            if (client == null)
                throw new InvalidOperationException("Initialize client first");

            var values = new Dictionary<string, object>
    {
        { "ProjectId", projectId },
        { "DisplayName", name },
        { "StartDateTime", start },
        { "EndDateTime", start.AddHours(2) }
    };

            var res = await client.InsertEntryAsync("TimeEntry", values);
        }


        // example 5
        static async Task UpdateProjectName(Guid projectId, string newName)
        {
            if (client == null)
                throw new InvalidOperationException("Initialize client first");

            var project = new ApiProject()
            {
                Name = newName
            };

            await client.For<ApiProject>().Key(projectId).Set(new { project.Name }).UpdateEntryAsync();
        }

    }
}


//NEW InlooxInfo from temp ProjectNumber: 2016_08365
//     InlooxInfo GetAttributes: DISABLED 00000000-0000-0000-0000-000000000000 2016_08365
//        ContentTest - GetValue fileName: S:\Konstruktion\ProE\Projekte\2016_08372 / 6 / 0 / 2047 / DelayIfSlow
//    GetDataFromPath  START: S:\Konstruktion\ProE\Projekte\2016_08372
//      GetDataFromPath(match found) return: S vs  / ProE / 2016_08372
//         ContentTest - GetValue S / ProE / 2016_08372
//             UseProjectCacheNow InlooxInfo: 04.04.2019 09:35:37 > 04.04.2019 09:35:12
//      NEW InlooxInfo from temp ProjectNumber: 2016_08372
//           InlooxInfo GetAttributes: DISABLED 00000000-0000-0000-0000-000000000000 2016_08372
//        ContentTest - GetValue fileName: S:\Konstruktion\ProE\Projekte\2016_08372 / 5 / 0 / 2047 / DelayIfSlow
//    GetDataFromPath  START: S:\Konstruktion\ProE\Projekte\2016_08372
//      GetDataFromPath(match found) return: S vs  / ProE / 2016_08372
//         ContentTest - GetValue S / ProE / 2016_08372
//             UseProjectCacheNow InlooxInfo: 04.04.2019 09:35:37 > 04.04.2019 09:35:12
//      NEW InlooxInfo from temp ProjectNumber: 2016_08372
//           InlooxInfo GetAttributes: DISABLED 00000000-0000-0000-0000-000000000000 2016_08372
//        ContentTest - GetValue fileName: S:\Konstruktion\ProE\Projekte\2016_08385 / 6 / 0 / 2047 / DelayIfSlow
//    GetDataFromPath  START: S:\Konstruktion\ProE\Projekte\2016_08385
//      GetDataFromPath(match found) return: S vs  / ProE / 2016_08385
//         ContentTest - GetValue S / ProE / 2016_08385
//             UseProjectCacheNow InlooxInfo: 04.04.2019 09:35:37 > 04.04.2019 09:35:12
//      NEW InlooxInfo from temp ProjectNumber: 2016_08385
//           InlooxInfo GetAttributes: DISABLED 00000000-0000-0000-0000-000000000000 2016_08385
//        ContentTest - GetValue fileName: S:\Konstruktion\ProE\Projekte\2016_08385 / 5 / 0 / 2047 / DelayIfSlow


