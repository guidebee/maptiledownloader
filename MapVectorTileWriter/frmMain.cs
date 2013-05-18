using System;
using System.Collections;
using System.Drawing;
using System.IO;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using MapDigit.Drawing;
using MapDigit.GIS;
using MapDigit.GIS.Vector;
using MapDigit.MapTile;
using MapTileDownloader;

namespace MapVectorTileWriter
{
    public partial class frmMain : Form
    {

        private int mapIndexX = 54382/16;
        private int mapIndexY = 26608/16-2;
        private int mapZoomLevel=12;
        private int mapType = MapType.MICROSOFTMAP;
        private FileStream mapFileIndex;
        private FileStream mapFileData;
        private BinaryReader MapIndexReader;
        private BinaryReader MapDataReader;
        //private readonly WebClient webClient = new WebClient();
        private bool [,]SelectedMapIndex=new bool[3,3];
        private bool []MapZoomLevelSelected=new bool[18];

        private Hashtable imageCache=new Hashtable();
        private MapTileDownloadManager mapTileDownloadManager;
        private Thread managerThread;
        private Thread addDownloadTaskThread;
        private Thread writerThread;
        public static MapTileVectorDataSource localMapTileFileReader;
        public static int downloadIndex = 0;
        Bitmap mapImage = new Bitmap(768, 768);
        private Graphics graphics;
        private Font drawFont;
        private SolidBrush drawBrush;
        private Pen pen;
        private Color greenColor;
        private SolidBrush fillBrush;

        public frmMain()
        {
            InitializeComponent();
            MapLayer.SetAbstractGraphicsFactory(NETGraphicsFactory.getInstance());
            localMapTileFileReader = new MapTileVectorDataSource(@"D:\nanjing\Nanjing.pst");
            GeoSet getSet = localMapTileFileReader.GetGeoSet();
            graphics = Graphics.FromImage(mapImage);
            drawFont = new Font("Arial", 16, FontStyle.Bold);
            drawBrush = new SolidBrush(Color.Red);
            pen = new Pen(Color.Red);
            greenColor = Color.FromArgb(120, Color.Blue);
            fillBrush = new SolidBrush(greenColor);
            getSet.GetMapMapFeatureLayer("POI.lyr").FontColor = 0x0000FF;
            getSet.GetMapMapFeatureLayer("POI.lyr").FontName = "楷体";
            getSet.GetMapMapFeatureLayer("LandMark.lyr").FontColor = 0;
            getSet.GetMapMapFeatureLayer("LandMark.lyr").FontName = "Arial";
            getSet.GetMapMapFeatureLayer("Adm_LandMark.lyr").FontColor = 0;
            getSet.GetMapMapFeatureLayer("Adm_LandMark.lyr").FontName = "Arial";
            getSet.GetMapMapFeatureLayer("Road.lyr").FontColor = 16711680;
            getSet.GetMapMapFeatureLayer("Road.lyr").FontName = "楷体";
            getSet.GetMapMapFeatureLayer("RailWay.lyr").FontColor = 0;
            getSet.GetMapMapFeatureLayer("RailWay.lyr").FontName = "Arial";
            getSet.GetMapMapFeatureLayer("Landuse.lyr").FontColor = 0;
            getSet.GetMapMapFeatureLayer("Landuse.lyr").FontName = "Arial";
            getSet.GetMapMapFeatureLayer("Block.lyr").FontColor = 0;
            getSet.GetMapMapFeatureLayer("Landuse.lyr").FontName = "Arial";
            getSet.GetMapMapFeatureLayer("Adm_Area.lyr").FontColor = 16711680;
            getSet.GetMapMapFeatureLayer("Adm_Area.lyr").FontName = "宋体";
            mapTileDownloadManager = new MapTileDownloadManager(this);
            mapFileIndex = new FileStream("world.ind", FileMode.Open);
            mapFileData = new FileStream("world.dat", FileMode.Open);
            MapIndexReader = new BinaryReader(mapFileIndex);
            MapDataReader = new BinaryReader(mapFileData);
            for(int i=0;i<3;i++)
            {
                for(int j=0;j<3;j++)
                {
                    SelectedMapIndex[i, j] = false;
                }
            }
            SelectedMapIndex[0, 0] = true;
            DrawMapImages();
            cboMapType.SelectedIndex = 12;
            managerThread = new Thread(new ThreadStart(mapTileDownloadManager.ProcessTaskList));
            managerThread.Start();



        }

       
        private void DrawMapImages()
        {

           
            int xIndex, yIndex;
            // Create font and brush.
            
            this.Cursor = Cursors.WaitCursor;
            txtMessage.Text = "";
            for (xIndex = mapIndexX; xIndex < mapIndexX+3; xIndex++)
            {
                for (yIndex = mapIndexY; yIndex < mapIndexY+3; yIndex++)
                {

                    Image image = null;
                    if (mapZoomLevel < 8)
                    {
                        image = GetImageFromFile(mapZoomLevel, xIndex, yIndex);
                    }
                    else
                    {
                        image = DownloadImage(mapZoomLevel, xIndex, yIndex);
                    }
                    //image = DownloadImage(mapZoomLevel, xIndex, yIndex);
                    if(image!=null)
                    {
                        if (image is Bitmap)
                        {
                            ((Bitmap)image).SetResolution(96,96);
                        }

                        graphics.DrawImage(image, (xIndex - mapIndexX) * 256, (yIndex - mapIndexY) * 256,new Rectangle(0,0,256,256), GraphicsUnit.Pixel);
                        RectangleF drawRect = new RectangleF((xIndex - mapIndexX) * 256, (yIndex - mapIndexY)*256, 256, 256);
                        graphics.DrawRectangle(pen, drawRect.X, drawRect.Y, drawRect.Width, drawRect.Height);
                        string strIndex = "(" + xIndex + "," + yIndex + ")";

                        if (SelectedMapIndex[xIndex - mapIndexX, yIndex - mapIndexY])
                        {
                            graphics.FillRectangle(fillBrush, drawRect);
                            SelectedMapIndex[xIndex - mapIndexX, yIndex - mapIndexY] = true;

                        }
                        graphics.DrawString(strIndex,drawFont,drawBrush,drawRect);

                    }

                    
                }
            }
            this.Cursor = Cursors.Default;
            graphics.DrawRectangle(pen, 0, 0, 767, 767);
            string strZoom = "Zoom:" + mapZoomLevel;

            graphics.DrawString(strZoom, drawFont, drawBrush, 0, 768 - 32);

            picStoredMap.Image = mapImage;
            
         
        }



        private Image DownloadImage(int Level,int x, int y)
        {
            Level = 17 - Level;
            try
            {
                downloadIndex++;
                string url = "X="+ x+",Y="+y+",Zoom="+(17-Level);
                byte[] buffer = null;

                buffer = (byte[]) imageCache[url];
                if (buffer == null)
                {

                    Console.WriteLine("Creating image "+url);
                    localMapTileFileReader.GetImage(mapType, x, y, 17 - Level);
                    buffer = localMapTileFileReader.ImageArray;
                    imageCache[url] = buffer;
                }

                if(imageCache.Count>256 )
                {
                    imageCache.Clear();
                }
                if (buffer != null)
                {
                    MemoryStream pngData = new MemoryStream(buffer);
                    Image image = Image.FromStream(pngData);
                    return image;
                }

                return null;
            }catch(Exception e)
            {
                
            }
            return null;
        }

        private Image GetImageFromFile(int Level, int x, int y)
        {
            try
            {
                long index;
                long offset;
                long length;
                Level = 17 - Level;
                if (Level == 0)
                {
                    index = 0;
                }
                else
                {
                    int n = 17 - Level;
                    index = (long)((Math.Pow((double)4, (double)n) - 1) / 3);
                    int pw = 17 - Level;
                    int MaxTitle = 1;
                    for (int i = 0; i < pw; i++) MaxTitle *= 2;
                    index += x * MaxTitle + y;
                }

                mapFileIndex.Seek(index * 8, SeekOrigin.Begin);
                offset = MapIndexReader.ReadInt32();
                length = MapIndexReader.ReadInt32();
                mapFileData.Seek(offset, SeekOrigin.Begin);
                byte[] buffer = MapDataReader.ReadBytes((int)length);
                MemoryStream pngData = new MemoryStream(buffer);

                Image image = Image.FromStream(pngData);

                return image;
            }
            catch (Exception e)
            {

            }
            return null;

        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 03JAN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         *  cast double to integer
         * @param f the double value.
         * @return the closed interger for the double value.
         */
        protected static int cast2Integer(double f)
        {
            if (f < 0)
            {
                return (int) Math.Ceiling(f);
            }
            else
            {
                return (int) Math.Floor(f);
            }
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 03JAN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Get the index of map tiles based on given geographical coordinates
         * @param latitude  y coordinates in geographical space.
         * @param longitude x coordinates in geographical space.
         * @param zoomLevel   current zoom level
         * @return the the index of map tiles
         */
        protected static Point convertCoordindates2Tiles(double latitude,
                                                         double longitude, int zoomLevel)
        {

            Point pt = fromLatLngToPixel(new GeoLatLng(latitude, longitude), zoomLevel);
            double pixelx = pt.X;
            double longtiles = pixelx / MapType.MAP_TILE_WIDTH;
            int tilex = cast2Integer(longtiles);
            double pixely = pt.Y;
            int tiley = cast2Integer(pixely / MapType.MAP_TILE_WIDTH);
            return new Point(tilex, tiley);
        }



        public static Point fromLatLngToPixel(GeoLatLng latLng, int zoomLevel)
        {
            double latitude = latLng.latitude;
            double longitude = latLng.longitude;
            double power = 8 + zoomLevel;
            double mapsize = Math.Pow(2, power);
            double origin = mapsize / 2;
            double longdeg = Math.Abs(-180 - longitude);
            double longppd = mapsize / 360;
            double longppdrad = mapsize / (2 * Math.PI);
            double pixelx = longdeg * longppd;
            double e = Math.Sin(latitude * (1 / 180.0 * Math.PI));
            if (e > 0.9999)
            {
                e = 0.9999;
            }
            if (e < -0.9999)
            {
                e = -0.9999;
            }

            double pixely = origin + 0.5 * Math.Log((1 + e) / (1 - e)) * (-longppdrad);
            return new Point((int)pixelx, (int)pixely);
        }


       

        private void CalculateHowManyTiles()
        {
            int selectedMapIndex = 0;
            for(int i=0;i<3;i++)
            {
                for(int j=0;j<3;j++)
                {
                    if (SelectedMapIndex[i, j]) selectedMapIndex += 1;
                }
            }

            int howManyLevel = 0;
            for(int level=mapZoomLevel;level<18;level++)
            {
                if(MapZoomLevelSelected[level])
                {
                    howManyLevel += (int)Math.Pow(4, level - mapZoomLevel);
                }
            }

            int howMayTiles = howManyLevel*selectedMapIndex;
            progressBar1.Maximum = howMayTiles;
            progressBar1.Value = 0;
            lblTotal.Text = "Totally " + howMayTiles + " tiles will be downloaded!";
        }

        ////////////////////////////////////////////////////////////////////////////
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 03JAN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Computes the geographical coordinates from pixel coordinates.
         * @param pt  pixel coordinates.
         * @param zoomLevel   current zoom level
         * @return the geographical coordinates (latitude,longitude) pair
         */
        public static GeoLatLng fromPixelToLatLng(Point pt, int zoomLevel)
        {
            double maxLat = Math.PI;
            double zoom = zoomLevel;
            double maxTileY;
            double TileWidth = 256.0;
            double TileHeight = 256.0;
            double TileY = (pt.Y / TileHeight);
            double y = (pt.Y - TileY * TileHeight);
            double MercatorY, res, a;
            maxTileY = Math.Pow(2, zoom);
            MercatorY = TileY + y / TileHeight;
            res = maxLat * (1 - 2 * MercatorY / maxTileY);
            a = Math.Exp(2 * res);
            a = (a - 1) / (a + 1);
            a = a / Math.Sqrt(1 - a * a);
            double lat = Math.Atan(a) * 180 / Math.PI;

            double TileX = pt.X / TileHeight;
            double x = (pt.Y - TileX * TileHeight);
            double maxTileX;
            double MercatorX;
            maxTileX = Math.Pow(2, zoom);//2^zoom
            MercatorX = TileX + x / TileWidth;
            res = MercatorX / maxTileX;
            double lng = 360 * res - 180;
            return new GeoLatLng(lat, lng);
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            int maxTile = (int)Math.Pow(2, mapZoomLevel);
            mapIndexY -= 1;
            if (mapIndexY < 0) mapIndexY=0;
            DrawMapImages();

        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            int maxTile = (int)Math.Pow(2, mapZoomLevel);
            mapIndexY += 1;
            if (mapIndexY > maxTile - 3) mapIndexY = maxTile - 3;
            DrawMapImages();
        }

        private void btnLeft_Click(object sender, EventArgs e)
        {
            int maxTile = (int)Math.Pow(2, mapZoomLevel);
            mapIndexX -= 1;
            if (mapIndexX < 0) mapIndexX =0;
            DrawMapImages();
        }

        private void btnRight_Click(object sender, EventArgs e)
        {
            int maxTile = (int)Math.Pow(2, mapZoomLevel);
            mapIndexX += 1;
            if (mapIndexX > maxTile - 3) mapIndexX = maxTile - 3;
            DrawMapImages();
        }

        private void btnZoomIn_Click(object sender, EventArgs e)
        {
            mapZoomLevel += 1;
            int oldX = mapIndexX;
            int oldY = mapIndexY;
            mapIndexX = (oldX + 1)*2 - 1;
            mapIndexY = (oldY + 1) * 2 - 1;
            if (mapZoomLevel > 17)
            {
                mapZoomLevel = 17;
                mapIndexX = oldX;
                mapIndexY = oldY;
            }
            DrawMapImages();
            CalculateHowManyTiles();
        }

        private void btnZoomOut_Click(object sender, EventArgs e)
        {
            mapZoomLevel -= 1;
            int oldX = mapIndexX;
            int oldY = mapIndexY;
            mapIndexX = oldX/2;
            mapIndexY = oldY/2;
            if (mapZoomLevel<2)
            {
                mapZoomLevel = 2;
                mapIndexX = oldX;
                mapIndexY = oldY;
            }
            DrawMapImages();
            CalculateHowManyTiles();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            mapIndexX = 0;
            mapIndexY = 0;
            mapZoomLevel=2;
            DrawMapImages();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            mapTileDownloadManager.stopManager = true;
            if (addDownloadTaskThread!=null)
            {
                addDownloadTaskThread.Abort();
            }
            if (managerThread != null)
            {
                managerThread.Join();
            }
            Application.Exit();
        }

        

        private void chkLevel2_Click(object sender, EventArgs e)
        {
            MapZoomLevelSelected[2] = chkLevel2.Checked;
            CalculateHowManyTiles();
        }

        private void chkLevel3_Click(object sender, EventArgs e)
        {
            MapZoomLevelSelected[3] = chkLevel3.Checked;
            CalculateHowManyTiles();
        }

        private void chkLevel4_Click(object sender, EventArgs e)
        {
            MapZoomLevelSelected[4] = chkLevel4.Checked;
            CalculateHowManyTiles();
        }

        private void chkLevel5_Click(object sender, EventArgs e)
        {
            MapZoomLevelSelected[5] = chkLevel5.Checked;
            CalculateHowManyTiles();
        }

        private void chkLevel6_Click(object sender, EventArgs e)
        {
            MapZoomLevelSelected[6] = chkLevel6.Checked;
            CalculateHowManyTiles();
        }

        private void chkLevel7_Click(object sender, EventArgs e)
        {
            MapZoomLevelSelected[7] = chkLevel7.Checked;
            CalculateHowManyTiles();
        }

        private void chkLevel8_Click(object sender, EventArgs e)
        {
            MapZoomLevelSelected[8] = chkLevel8.Checked;
            CalculateHowManyTiles();
        }

        private void chkLevel9_Click(object sender, EventArgs e)
        {
            MapZoomLevelSelected[9] = chkLevel9.Checked;
            CalculateHowManyTiles();
        }

        private void chkLevel10_Click(object sender, EventArgs e)
        {
            MapZoomLevelSelected[10] = chkLevel10.Checked;
            CalculateHowManyTiles();
        }

        private void chkLevel11_Click(object sender, EventArgs e)
        {
            MapZoomLevelSelected[11] = chkLevel11.Checked;
            CalculateHowManyTiles();
        }

        private void chkLevel12_Click(object sender, EventArgs e)
        {
            MapZoomLevelSelected[12] = chkLevel12.Checked;
            CalculateHowManyTiles();
        }

        private void chkLevel13_Click(object sender, EventArgs e)
        {
            MapZoomLevelSelected[13] = chkLevel13.Checked;
            CalculateHowManyTiles();
        }

        private void chkLevel14_Click(object sender, EventArgs e)
        {
            MapZoomLevelSelected[14] = chkLevel14.Checked;
            CalculateHowManyTiles();
        }

        private void chkLevel15_Click(object sender, EventArgs e)
        {
            MapZoomLevelSelected[15] = chkLevel15.Checked;
            CalculateHowManyTiles();
        }

        private void chkLevel16_Click(object sender, EventArgs e)
        {
            MapZoomLevelSelected[16] = chkLevel16.Checked;
            CalculateHowManyTiles();
        }

        private void chkLevel17_Click(object sender, EventArgs e)
        {
            MapZoomLevelSelected[17] = chkLevel17.Checked;
            CalculateHowManyTiles();
        }

        private void cboMapType_SelectedIndexChanged(object sender, EventArgs e)
        {
            mapType = cboMapType.SelectedIndex;
        }


        private delegate void DoneWithDownloading();


        
        //addDownloadTaskThread
        private void AddDownloadTask()
        {
            try
            {
                int minX = int.Parse(txtStartX.Text) % 3;
                int minY = int.Parse(txtStartY.Text) % 3;
                int maxX = int.Parse(txtEndX.Text) % 3;
                int maxY = int.Parse(txtEndY.Text) % 3;

                int startX = Math.Min(minX, maxX);
                int startY = Math.Min(minY, maxY);
                int endX = Math.Max(minX, maxX);
                int endY = Math.Max(minY, maxY);

                for (int zoom = mapZoomLevel; zoom < 18; zoom++)
                {
                    if (MapZoomLevelSelected[zoom])
                    {

                        int zoomPower = (int)Math.Pow(2, zoom - mapZoomLevel);

                        for (int indexX = startX * zoomPower; indexX < (endX + 1) * zoomPower; indexX++)
                        {
                            for (int indexY = startY * zoomPower; indexY < (endY + 1) * zoomPower; indexY++)
                            {
                                MapTileIndex mapTileIndex = new MapTileIndex();
                                mapTileIndex.MapType = mapType;
                                mapTileIndex.ZoomLevel = 17 - zoom;
                                mapTileIndex.XIndex = indexX + mapIndexX * zoomPower;
                                mapTileIndex.YIndex = indexY + mapIndexY * zoomPower;
                                mapTileDownloadManager.AddToTaskList(mapTileIndex);
                                Thread.Sleep(1000);
                            }
                        }
                    }
                }
        
                


            }
            catch (Exception ex)
            {

            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            btnStart.Enabled = false;
            btnStop.Enabled = true;
            btnPause.Enabled = true;

            addDownloadTaskThread = new Thread(new ThreadStart(AddDownloadTask));
            addDownloadTaskThread.Start();
            mapTileDownloadManager.DownloadedCount = 0;

            try
            {
                int minX = int.Parse(txtStartX.Text)%3;
                int minY = int.Parse(txtStartY.Text)%3;
                int maxX = int.Parse(txtEndX.Text)%3;
                int maxY = int.Parse(txtEndY.Text)%3;

                int startX = Math.Min(minX, maxX);
                int startY = Math.Min(minY, maxY);
                int endX = Math.Max(minX, maxX);
                int endY = Math.Max(minY, maxY);

                MapTileWriter mapTileWriter = new MapTileWriter(startX + mapIndexX, startY + mapIndexY, endX + mapIndexX, endY + mapIndexY, mapZoomLevel, mapType, mapTileDownloadManager);
                mapTileWriter.zoomLevelSelected = MapZoomLevelSelected;
                writerThread = new Thread(new ThreadStart(mapTileWriter.WriteMapTileFile));
                writerThread.Start();

            }catch(Exception ex)
            {
                
            }




        }

       

        private void ResetMapTileRange()
        {
            try
            {
                int minX = int.Parse(txtStartX.Text) %3;
                int minY = int.Parse(txtStartY.Text) % 3;
                int maxX = int.Parse(txtEndX.Text) % 3;
                int maxY = int.Parse(txtEndY.Text) % 3;

                int startX = Math.Min(minX, maxX);
                int startY = Math.Min(minY, maxY);
                int endX = Math.Max(minX, maxX);
                int endY = Math.Max(minY, maxY);

                for(int i=0;i<3;i++)
                {
                    for(int j=0;j<3;j++)
                    {
                        SelectedMapIndex[i, j] = false;
                    }
                }

                for (int i = startX; i <= endX; i++)
                {
                    for (int j = startY; j <= endY; j++)
                    {
                        SelectedMapIndex[i, j] = true;
                    }
                }
            }
            catch(Exception e)
            {
                
            }
        }

        private void txtStartX_TextChanged(object sender, EventArgs e)
        {
            ResetMapTileRange();
            CalculateHowManyTiles();
            DrawMapImages();
        }


        private void txtStartY_TextChanged(object sender, EventArgs e)
        {
            ResetMapTileRange();
            CalculateHowManyTiles();
            DrawMapImages();
        }

        private void txtEndX_TextChanged(object sender, EventArgs e)
        {
            ResetMapTileRange();
            CalculateHowManyTiles();
            DrawMapImages();
        }

        private void txtEndY_TextChanged(object sender, EventArgs e)
        {
            ResetMapTileRange();
            CalculateHowManyTiles();
            DrawMapImages();
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            if(addDownloadTaskThread!=null)
            {
                if (btnPause.Text.EndsWith("Pause"))
                {
                    addDownloadTaskThread.Suspend();
                    btnPause.Text = "Resume";
                }else
                {
                    addDownloadTaskThread.Resume();
                    btnPause.Text = "Pause";
                }
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if(addDownloadTaskThread!=null)
            {
                addDownloadTaskThread.Abort();
            }
            btnStart.Enabled = true;
        } 

        delegate void UpdateMessage(string message);

        public void AddMessage(string message)
        {
            if(txtMessage.InvokeRequired)
            {
                txtMessage.Invoke(new UpdateMessage(updateMessage), message);
            }else
            {
                updateMessage(message);
            }
        }

        private int MessageLine = 0;
        private void updateMessage(string message)
        {
            txtMessage.Text += "\r\n"+message;
            MessageLine++;
            progressBar1.Value = mapTileDownloadManager.DownloadedCount;
            if(MessageLine>20)
            {
                MessageLine = 0;
                txtMessage.Text = "";
            }
        }
  
        private void done()
        {
            btnStart.Enabled = true;
            btnPause.Enabled = false;
            btnStop.Enabled = false;
            txtMessage.Text = "Done";
        }

        public void doneWithDownloading()
        {
            if(btnStart.InvokeRequired)
            {
                btnStart.Invoke(new DoneWithDownloading(done));
            }else
            {
                done();
            }
        }

    }

    public class GeoLatLng
    {
        public GeoLatLng(double lat,double lng)
        {
            latitude = lat;
            longitude = lng;
        }
        
        public double latitude;
        public double longitude;
    }
}