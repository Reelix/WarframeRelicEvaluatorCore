using System.Text.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Tesseract; // OCR

// Install-Package MouseKeyHook
using Gma.System.MouseKeyHook; // Shortcut Keys
using System.Text.Json.Serialization;

namespace WarframeRelicEvaluatorCore
{
    public partial class Form1 : Form
    {
        // https://docs.google.com/spreadsheets/d/1AaTziJzt35bifF5kwrap003cuPqFwy0FnV6i2sBW7ho/edit
        static string warframeItems = "";
        static string username = Environment.UserName;
        const int textTop = 415;
        const int textHeight = 45;
        WebClient wc = new WebClient();
        TesseractEngine tessEngine;

        public Form1()
        {
            InitializeComponent();
        }

        private IKeyboardMouseEvents m_GlobalHook;

        public void Subscribe()
        {
            // Note: for the application hook, use the Hook.AppEvents() instead
            m_GlobalHook = Hook.GlobalEvents();

            m_GlobalHook.KeyDown += GlobalHookKey_Down;
            m_GlobalHook.KeyUp += GlobalHookKey_Up;
            // m_GlobalHook.MouseDownExt += GlobalHookMouseDownExt;
        }

        private void GlobalHookKey_Down(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.NumPad0)
            {
                if (button3.Text == "Shrink")
                {
                    Shrink();
                }
                else
                {
                    Grow();
                }
            }
        }

        private void GlobalHookKey_Up(object sender, KeyEventArgs e)
        {

        }

        public void Unsubscribe()
        {
            // m_GlobalHook.MouseDownExt -= GlobalHookMouseDownExt;
            m_GlobalHook.KeyDown -= GlobalHookKey_Down;
            m_GlobalHook.KeyUp -= GlobalHookKey_Up;

            //It is recommened to dispose it
            m_GlobalHook.Dispose();
        }


        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Unsubscribe();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // If this line throws an error, you're missing the tessdata folder in your debug / release folder
            tessEngine = new TesseractEngine(Application.StartupPath + @"\tessdata", "eng");

            // Load the items
            LoadItemDB();

            // Set the hotkey
            Subscribe();

            // Go to minimized mode - Optional
            // Shrink();
        }


        [DllImport("user32.dll")]
        internal static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        // For Debugging (The giant "Debug" button - Testing code here - Safe to ignore)
        private void Button1_Click_1(object sender, EventArgs e)
        {
            //int someNum = LevenshteinDistance.Compute("A ,w  Braton Prime Stock", "Braton Prime Stock");
            //MessageBox.Show(someNum.ToString());
            Bitmap lastBitmap = new Bitmap(Image.FromFile($@"C:\Users\{username}\Pictures\Warframe\ReeAppLatest.bmp"));
            Process4Players(lastBitmap);

            // RunTests();
        }

        #region Reelix's Tests - You can ignore this
        void RunTests()
        {
            DateTime timeBefore = DateTime.Now;
            textBox1.Text = "Running Tests...";
            if (RunTest1() && RunTest2() && RunTest3() && RunTest4() && RunTest5() && RunTest6() && RunTest7() && RunTest8())
            {
                DateTime timeAfter = DateTime.Now;
                TimeSpan timeDiff = timeAfter - timeBefore;
                textBox1.Text = "All Tests Passed in: " + timeDiff.TotalMilliseconds;

            }
        }

        bool RunTest1()
        {
            Bitmap testBitmap = new Bitmap(Image.FromFile($@"C:\Users\{username}\Pictures\Warframe\Tests\Test 1.bmp"));
            Process4Players(testBitmap, true);
            if (textBox1.Text.Contains("Unknown"))
            {
                return false;
            }
            return true;
        }

        bool RunTest2()
        {
            Bitmap testBitmap = new Bitmap(Image.FromFile(@$"C:\Users\{username}\Pictures\Warframe\Tests\Test 2.bmp"));
            Process4Players(testBitmap, true);
            if (textBox1.Text.Contains("Unknown"))
            {
                return false;
            }
            return true;
        }

        bool RunTest3()
        {
            Bitmap testBitmap = new Bitmap(Image.FromFile(@"C:\Users\Reelix\Pictures\Warframe\Tests\Test 3.bmp"));
            Process4Players(testBitmap, true);
            if (textBox1.Text.Contains("Unknown"))
            {
                return false;
            }
            return true;
        }

        bool RunTest4()
        {
            Bitmap testBitmap = new Bitmap(Image.FromFile(@"C:\Users\Reelix\Pictures\Warframe\Tests\Test 4.bmp"));
            Process4Players(testBitmap, true);
            if (textBox1.Text.Contains("Unknown"))
            {
                return false;
            }
            return true;
        }

        bool RunTest5()
        {
            Bitmap testBitmap = new Bitmap(Image.FromFile(@"C:\Users\Reelix\Pictures\Warframe\Tests\Test 5.png"));
            Process3Players(testBitmap, true);
            if (textBox1.Text.Contains("Unknown"))
            {
                return false;
            }
            return true;
        }

        bool RunTest6()
        {
            Bitmap testBitmap = new Bitmap(Image.FromFile(@"C:\Users\Reelix\Pictures\Warframe\Tests\Test 6.bmp"));
            Process4Players(testBitmap, true);
            if (textBox1.Text.Contains("Unknown"))
            {
                return false;
            }
            return true;
        }

        bool RunTest7()
        {
            Bitmap testBitmap = new Bitmap(Image.FromFile(@"C:\Users\Reelix\Pictures\Warframe\Tests\Test 7.bmp"));
            Process4Players(testBitmap, true);
            if (textBox1.Text.Contains("Unknown"))
            {
                return false;
            }
            return true;
        }

        bool RunTest8()
        {
            Bitmap testBitmap = new Bitmap(Image.FromFile(@"C:\Users\Reelix\Pictures\Warframe\Tests\Test 8.bmp"));
            Process4Players(testBitmap, false);
            if (textBox1.Text.Contains("Unknown"))
            {
                return false;
            }
            return true;
        }
        #endregion

        private Bitmap DoSetup()
        {
            textBox1.Text = "Loading...";
            Process p = Process.GetProcessesByName("Warframe.x64").FirstOrDefault();
            if (p == null)
            {
                textBox1.Text = "Warframe needs to be running...";
                return null;
            }

            FocusWindow(p.Id);
            Thread.Sleep(50); // Give it time to focus
            Process thisApp = Process.GetCurrentProcess();
            Bitmap warframeScreenshot = CaptureWindow(p.MainWindowHandle);
            FocusWindow(thisApp.Id);
            warframeScreenshot.Save($@"C:\Users\{username}\Pictures\Warframe\ReeAppLatest.bmp");
            if (warframeScreenshot.Width != 1920 || warframeScreenshot.Height != 1080)
            {
                textBox1.Text = "Error: Cannot capture Warframe window" + Environment.NewLine + "Make sure Warframe is set to Display Mode: Borderless Fullscreen";
                return null;
            }
            return warframeScreenshot;
        }

        private void btn3Players_Click(object sender, EventArgs e)
        {
            // 3 Players
            Bitmap warframeScreenshot = DoSetup();
            if (warframeScreenshot != null)
            {
                Process3Players(warframeScreenshot);
            }
        }

        private void btn4Players_Click(object sender, EventArgs e)
        {
            // 3 Players
            Bitmap warframeScreenshot = DoSetup();
            if (warframeScreenshot != null)
            {
                Process4Players(warframeScreenshot);
            }
        }

        void Process3Players(Bitmap theBitmap, bool debug = false)
        {
            textBox1.Text = "Loading...";
            this.Refresh();
            if (!debug)
            {
                textBox1.Text = "";
            }
            // Split into the 3 items - Assuming 3 players

            // Item 1

            int item = 1;
            foreach (int xPos in new int[] { 595, 840, 1080 }) // 1920x1080
            {
                Rectangle srcRect = new Rectangle(xPos, textTop, 240, textHeight);
                Bitmap cropped = (Bitmap)theBitmap.Clone(srcRect, theBitmap.PixelFormat);

                cropped.Save($@"C:\Users\{username}\Pictures\Warframe\Woof\{item}.bmp");
                string ocrResult = DoOCR($@"C:\Users\{username}\Pictures\Warframe\Woof\{item}.bmp");
                ocrResult = FixCommonOCRErrors(ocrResult);
                string foundItem = FindItem(ocrResult);
                if (foundItem != "")
                {
                    if (debug)
                    {
                        textBox1.Text += foundItem + Environment.NewLine;
                    }
                    else
                    {
                        var itemCost = GetItemCost(foundItem);
                        textBox1.Text += $"Item {item}: {foundItem} - Value: {itemCost.lowValue} to {itemCost.highValue} {Environment.NewLine}";
                    }
                }
                else
                {
                    textBox1.Text += $"Item {item}: Unknown --> {ocrResult} {Environment.NewLine}";
                }
            }
        }

        void Process4Players(Bitmap theBitmap, bool debug = false)
        {
            textBox1.Text = "Loading...";
            textBox1.Refresh();
            if (!debug)
            {
                textBox1.Text = "";
            }
            // Split into the 4 items - Assuming 4 players

            int item = 1;
            foreach (int xPos in new int[] { 475, 725, 960, 1205 }) // 1920x1080
            {
                Rectangle srcRect = new Rectangle(xPos, textTop, 240, textHeight);
                Bitmap cropped = (Bitmap)theBitmap.Clone(srcRect, theBitmap.PixelFormat);

                // If Folder doesn't exist?
                cropped.Save($@"C:\Users\{username}\Pictures\Warframe\Woof\{item}.bmp");
                string ocrResult = DoOCR($@"C:\Users\{username}\Pictures\Warframe\Woof\{item}.bmp");
                ocrResult = FixCommonOCRErrors(ocrResult);
                string foundItem = FindItem(ocrResult);
                if (foundItem != "")
                {
                    if (debug)
                    {
                        textBox1.Text += foundItem + Environment.NewLine;
                    }
                    else
                    {
                        var itemCost = GetItemCost(foundItem);
                        textBox1.Text += $"Item {item}: {foundItem} - Value: {itemCost.lowValue} to {itemCost.highValue} {Environment.NewLine}";
                        textBox1.Refresh();
                    }
                }
                else
                {
                    textBox1.Text += $"Item {item}: Unknown --> {ocrResult} {Environment.NewLine}";
                }
                item++;
            }
        }

        private string DoOCR(string filePath)
        {
            string returnText = "";
            tessEngine.SetVariable("tessedit_char_whitelist", "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ &"); // Only regular letters

            // string theVersion = tessEngine.Version;
            using (var img = Pix.LoadFromFile(filePath))
            {
                using (var page = tessEngine.Process(img))
                {
                    returnText = page.GetText();
                }
            }
            return returnText;
        }

        private string FixCommonOCRErrors(string text)
        {
            text = text.Replace("Prine", "Prime");
            text = text.Replace("Primestock", "Prime Stock");
            text = text.Replace("\n", " ");
            text = text.Trim();
            return text;
        }

        private string FindItem(string itemName)
        {
            // Make it API Friendly
            if (itemName.Contains("Neuroptics") || itemName.Contains("Systems") || itemName.Contains("Chassis"))
            {
                itemName = itemName.Replace(" Blueprint", "");
            }

            // First, see if it exists directly
            foreach (string item in warframeItems.Split(','))
            {
                if (itemName.ToLower().Contains(item.ToLower()))
                {
                    return item;
                }
            }

            // Ok - It doesn't exist directly - Hard Mode!
            int lowestLevNum = 10;
            string levItem = "";
            foreach (string item in warframeItems.Split(','))
            {
                int levNum = LevenshteinDistance.Compute(itemName, item);
                if (levNum < lowestLevNum)
                {
                    lowestLevNum = levNum;
                    levItem = item;
                }
            }
            if (lowestLevNum <= 5) // Close enough
            {
                return levItem;
            }
            return "";
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct Rect
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }


        [DllImport("user32.dll")]
        private static extern IntPtr GetWindowRect(IntPtr hWnd, ref Rect rect);

        // Screenshot of the Warframe Window
        public static Bitmap CaptureWindow(IntPtr handle)
        {
            var rect = new Rect();
            GetWindowRect(handle, ref rect);
            var bounds = new Rectangle(rect.Left, rect.Top, rect.Right - rect.Left, rect.Bottom - rect.Top);
            var result = new Bitmap(bounds.Width, bounds.Height);

            using (var graphics = Graphics.FromImage(result))
            {
                graphics.CopyFromScreen(new Point(bounds.Left, bounds.Top), Point.Empty, bounds.Size);
            }

            return result;
        }

        [DllImport("user32.dll")]
        static extern bool SetForegroundWindow(IntPtr hWnd);

        private void FocusWindow(int processId)
        {
            Process p = Process.GetProcessById(processId);
            if (p != null)
            {
                SetForegroundWindow(p.MainWindowHandle);
            }
        }

        // https://api.warframe.market/v1/items/akbolto_prime_receiver/orders
        private (int lowValue, int highValue) GetItemCost(string itemName)
        {
            // Make it API Friendly
            if (itemName.Contains("Neuroptics") || itemName.Contains("Systems") || itemName.Contains("Chassis"))
            {
                itemName = itemName.Replace(" Blueprint", "");
            }
            // The real name is Kavasa Prime X, but warframe.market calls it Kavasa Prime Collar X
            itemName = itemName.Replace("Kavasa Prime Buckle", "Kavasa Prime Collar Buckle");
            itemName = itemName.Replace("Kavasa Prime Band", "Kavasa Prime Collar Band");

            // API deals in lower case
            // API uses _'s as spaces
            // API uses "and" instead of "&"
            itemName = itemName.ToLower().Replace(" ", "_").Replace("&", "and");

            // Forma are special - This can be sped up
            if (itemName == "forma_blueprint")
            {
                return (5, 5);
            }

            string urlString = "https://api.warframe.market/v1/items/" + itemName + "/orders";
            string rawItemData = wc.DownloadString(urlString);

            MarketRootObject marketData = new MarketRootObject();
            try
            {
                JsonSerializerOptions woof = new JsonSerializerOptions();
                marketData = JsonSerializer.Deserialize<MarketRootObject>(rawItemData);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"ReeError: {ex}");
                return (999, 999);
            }
            // Only users that are active (Last online in the past 7 days)
            List<Order> orders = marketData.payload.orders.Where(x => x.user.last_seen > (DateTime.Now.AddDays(-7))).ToList();
            var something = orders.Any();

            // Only en users on pc (ru / console have weird prices)
            orders = orders.Where(x => x.platform == "pc" && x.region == "en").ToList();

            // And only orders that were semi-recently placed
            orders = orders.Where(x => x.creation_date > (DateTime.Now.AddDays(-21))).ToList();

            // Get the Highest Buy Order
            List<Order> buyOrders = orders.Where(x => x.order_type == "buy").ToList();

            Order HighestBuyOrder = buyOrders.OrderByDescending(x => x.platinum).FirstOrDefault();

            if (HighestBuyOrder == null)
            {
                HighestBuyOrder = new Order();
            }

            // Get the Lowest Sell Order
            List<Order> sellOrders = orders.Where(x => x.order_type == "sell").ToList();
            Order LowestSellOrder = sellOrders.OrderBy(x => x.platinum).First();

            // Nice return values
            if (HighestBuyOrder.platinum > LowestSellOrder.platinum)
            {
                return ((int)LowestSellOrder.platinum, (int)HighestBuyOrder.platinum);
            }
            else
            {
                return ((int)HighestBuyOrder.platinum, (int)LowestSellOrder.platinum);
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            if (button3.Text == "Shrink")
            {
                Shrink();
            }
            else
            {
                Grow();
            }
        }

        private void Shrink()
        {
            Process p = Process.GetProcessesByName("Warframe.x64").FirstOrDefault();
            if (p == null)
            {
                textBox1.Text = "Warframe needs to be running...";
                Grow();
            }
            else
            {
                button3.Text = "Grow";
                this.Size = new Size(75, 26);
                this.Location = new Point(Screen.PrimaryScreen.Bounds.Width - 75, Screen.PrimaryScreen.Bounds.Height - 26);
                FocusWindow(p.Id);
            }
        }

        private void Grow()
        {
            button3.Text = "Shrink";
            this.Size = new Size(730, 420);
            this.Location = new Point(500, 500);
            FocusWindow(Process.GetCurrentProcess().Id);
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            string toCopy = textBox1.Text;
            List<string> items = toCopy.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).ToList();
            // Item 1: Lex Prime Barrel - Value: 1 to 2
            string finalText = "";
            foreach (string item in items)
            {
                string itemName = item.Remove(0, item.IndexOf(":") + 1);
                itemName = itemName.Substring(0, itemName.IndexOf("-") - 1);
                string itemFromPrice = item.Remove(0, item.IndexOf("Value: ") + 7);
                itemFromPrice = itemFromPrice.Substring(0, itemFromPrice.IndexOf(" "));
                string itemToPrice = item.Remove(0, item.IndexOf("Value: "));
                itemToPrice = itemToPrice.Remove(0, itemToPrice.IndexOf("to ") + 3);

                finalText += $"{itemName} ({itemFromPrice}:platinum:->{itemToPrice}:platinum:) ";
            }
            finalText += "- Auto-Generated By: Reelix";
            Clipboard.SetText(finalText);

            // And Shrink
            Shrink();
        }

        private void BtnUpdateDatabase_Click(object sender, EventArgs e)
        {
            lblDatabase.Text = "Database: ???";
            textBox1.Text = "Updating...";
            this.Refresh();
            string rawItemData = wc.DownloadString("https://api.warframe.market/v1/items");
            ItemDataRoot itemDataRoot = new ItemDataRoot();
            try
            {
                ItemDataRoot parsedItemData = JsonSerializer.Deserialize<ItemDataRoot>(rawItemData);
                List<string> items = parsedItemData.payload.items.Select(x => x.item_name).ToList();
                items = items.Where(x => x.Contains(" Prime")).ToList(); // Only prime items can drop from relics (And Forma, but market doesn't have Forma)
                items = items.Where(x => !x.Contains(" Set")).ToList(); // Prime Sets don't count
                string longItemList = string.Join(",", items);
                StreamWriter sw1 = new StreamWriter(Application.StartupPath + @"\Database.txt");
                sw1.WriteLine(longItemList);
                sw1.Flush();
                sw1.Close();
                LoadItemDB();
                this.TopMost = false;
                textBox1.Text = "";
                this.Refresh();
                MessageBox.Show("Database Updated!");
                this.TopMost = true;
            }
            catch
            {
                this.TopMost = false;
                MessageBox.Show("Unable to update the DB :(");
                this.TopMost = true;
            }
        }

        private void LoadItemDB()
        {
            if (!File.Exists(Application.StartupPath + @"\Database.txt"))
            {
                lblDatabase.Text = "Database: N/A";
            }
            else
            {
                StreamReader sr1 = new StreamReader(Application.StartupPath + @"\Database.txt");
                warframeItems = sr1.ReadLine();
                sr1.Close();
                StringBuilder sb1 = new StringBuilder(warframeItems);
                sb1.Append(",Forma Blueprint"); // A valid Relic item warframe.market doesn't have
                warframeItems = sb1.ToString();
                int itemCount = warframeItems.Split(',').Length;
                lblDatabase.Text = "Database: " + itemCount;
            }
        }
    }
}
