using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using MapDigit.AJAX;
using MapDigit.Drawing;
using MapDigit.GIS;
using MapDigit.GIS.Drawing;
using MapDigit.GIS.Geometry;
using MapDigit.GIS.Raster;
using MapDigit.GIS.Service;
using MapDigit.MapTile;
using MapDigit.MapTileWriter;
using MapDigit.GIS.Vector;
using MapTileIndex=MapDigit.MapTileWriter.MapTileIndex;

namespace MapDigit
{
    public partial class MainWindow : Form, IRequestListener, IMapDrawingListener, IReaderListener,
        IGeocodingListener, IWritingProgressListener,IRoutingListener
    {
        private MapTileDownloadManager _mapTileDownloadManager;
        private RasterMap _rasterMap;
        private readonly IImage _mapImage;
        private readonly IGraphics _mapGraphics;
        private readonly GeoPoint _topLeft = new GeoPoint(0, 0);
        private int _oldX;
        private int _oldY;
        private int _selectX;
        private int _selectY;
        private MapTileWriter.MapTileWriter _mapTileWrite;
        private Thread _downloadThread;
        private Thread _tileIndexThread;
        private readonly IList<MapTileIndex> _tileIndexList = new List<MapTileIndex>();
        private readonly bool[] _mapZoomLevelSelected = new bool[18];
        private long _totalCount;
        private delegate void DoneWithDownloading();
        private delegate void UpdateInfo(string message);

        private int _mapType = MapType.MICROSOFTMAP;

        private volatile bool stopThread = false;



        private void ProcessTileIndex()
        {
            while (!stopThread)
            {
                int newX = 0;
                int newY = 0;
                int newZ = 0;
                lock (_tileIndexList)
                {
                    if (_tileIndexList.Count > 0)
                    {
                        MapTileIndex mapTileIndex = _tileIndexList[_tileIndexList.Count - 1];
                        newX = mapTileIndex.XIndex;
                        newY = mapTileIndex.YIndex;
                        newZ = mapTileIndex.ZoomLevel;
                        _tileIndexList.Clear();

                    }else
                    {
                        Thread.Sleep(5000);
                    }
                }
                if (newZ != 0)
                {
                    int oldZoom = _rasterMap.GetZoom();
                    GeoLatLng latLng = MapLayer.FromPixelToLatLng(new GeoPoint(newX*256 + 128, newY*256 + 128), newZ);
                    if (newZ != oldZoom)
                    {

                        _rasterMap.SetCenter(latLng, newZ);
                    }
                    else
                    {
                        GeoLatLng center = _rasterMap.GetCenter();
                        GeoPoint pt1 = _rasterMap.FromLatLngToScreenPixel(center);

                        GeoPoint pt2 = _rasterMap.FromLatLngToScreenPixel(latLng);

                        _rasterMap.PanDirection((int) (pt1.X - pt2.X), (int) (pt1.Y - pt2.Y));


                    }
                }


            }
        
    }







        private void UpdateStatus(string messsage)
        {
            if(InvokeRequired)
            {
                BeginInvoke(new UpdateInfo(UpdateStatus), messsage);
            }else
            {
                lblStatus.Text = messsage;
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            MapLayer.SetAbstractGraphicsFactory(NETGraphicsFactory.getInstance());
            _mapImage = MapLayer.GetAbstractGraphicsFactory().CreateImage(768, 768);
            _mapGraphics = _mapImage.GetGraphics();
            //InitVectorMap();
            _mapTileDownloadManager = new MapTileDownloadManager(this);
            _mapTileDownloadManager.Start();
            _rasterMap = new RasterMap(768, 768, _mapType, _mapTileDownloadManager);
            _rasterMap.SetMapDrawingListener(this);
            _rasterMap.SetGeocodingListener(this);
            _rasterMap.SetRoutingListener(this);

            // Get the configuration file.
            System.Configuration.Configuration config =
              ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            // Get the AppSetins section.
            AppSettingsSection appSettingSection =
                (AppSettingsSection)config.GetSection("appSettings");
            MapType.MAP_TYPE_URLS.Clear();

            foreach (var obj in appSettingSection.Settings.AllKeys)
            {
                var value = appSettingSection.Settings[obj];

                object type=MapType.MAP_TYPE_NAMES[obj];
                if(type!=null)
                {
                    string url = value.Value.Replace('#', '&');
                    MapType.MAP_TYPE_URLS.Add(type, url);
                    cboMapType.Items.Add(obj);
                }
                
            }
            cboMapType.Text = "MICROSOFTMAP";
  
        }


        public void InitVectorMap()
        {
            MapTileVectorDataSource localMapTileFileReader = new MapTileVectorDataSource(@"c:\nanjing\Nanjing.pst");
            GeoSet getSet = localMapTileFileReader.GetGeoSet();
            //getSet.GetMapMapFeatureLayer("POI.lyr").FontColor = 0x0000FF;
            //getSet.GetMapMapFeatureLayer("POI.lyr").FontName = "楷体";
            //getSet.GetMapMapFeatureLayer("LandMark.lyr").FontColor = 0;
            //getSet.GetMapMapFeatureLayer("LandMark.lyr").FontName = "Arial";
            //getSet.GetMapMapFeatureLayer("Adm_LandMark.lyr").FontColor = 0;
            //getSet.GetMapMapFeatureLayer("Adm_LandMark.lyr").FontName = "Arial";
            //getSet.GetMapMapFeatureLayer("Road.lyr").FontColor = 16711680;
            //getSet.GetMapMapFeatureLayer("Road.lyr").FontName = "楷体";
            //getSet.GetMapMapFeatureLayer("RailWay.lyr").FontColor = 0;
            //getSet.GetMapMapFeatureLayer("RailWay.lyr").FontName = "Arial";
            //getSet.GetMapMapFeatureLayer("Landuse.lyr").FontColor = 0;
            //getSet.GetMapMapFeatureLayer("Landuse.lyr").FontName = "Arial";
            //getSet.GetMapMapFeatureLayer("Block.lyr").FontColor = 0;
            //getSet.GetMapMapFeatureLayer("Landuse.lyr").FontName = "Arial";
            //getSet.GetMapMapFeatureLayer("Adm_Area.lyr").FontColor = 16711680;
            //getSet.GetMapMapFeatureLayer("Adm_Area.lyr").FontName = "宋体";
            //getSet.GetMapMapFeatureLayer("1.lyr").FontColor = 0x0000FF;
            //getSet.GetMapMapFeatureLayer("1.lyr").FontName = "楷体";
            //getSet.GetMapMapFeatureLayer("2.lyr").FontColor = 0;
            //getSet.GetMapMapFeatureLayer("2.lyr").FontName = "Arial";
            //getSet.GetMapMapFeatureLayer("3.lyr").FontColor = 0;
            //getSet.GetMapMapFeatureLayer("3.lyr").FontName = "Arial";
            //getSet.GetMapMapFeatureLayer("4.lyr").FontColor = 16711680;
            //getSet.GetMapMapFeatureLayer("4.lyr").FontName = "楷体";
            //getSet.GetMapMapFeatureLayer("5.lyr").FontColor = 0;
            //getSet.GetMapMapFeatureLayer("5.lyr").FontName = "Arial";

           // VectorMapRenderer vectorMapRenderer = new VectorMapRenderer(getSet);
            _mapTileDownloadManager = new MapTileDownloadManager(this, localMapTileFileReader);
 
           // getSet.Open();


 
           
        }

        public void ReadProgress(object context, int bytes, int total)
        {
           if(total!=0)
           {
               UpdateStatus("Reading ..." + (int) (((double) bytes/(double) total)*100.0) + "%");
           }
        }

        public void WriteProgress(object context, int bytes, int total)
        {

        }

        public void Done(object context, Response result)
        {
            
        }

        public void Done(object context, string rawResult)
        {
            
        }


        public void done(string query, MapPoint[] result)
        {
            if (result != null)
            {
                _rasterMap.SetCenter(result[0].Point, 15, _rasterMap.GetMapType());
            }else
            {
                UpdateStatus("Address not found!");
            }
        }

        public void Done(string query, MapDirection result)
        {
            if(result!=null)
            {
                _rasterMap.Resize(result.GetBound());
            }
        }

        public void readProgress(int bytes, int total)
        {
            if(total!=0)
           {
               UpdateStatus("Reading ...");
           }
        }

   
        public void Done()
        {
            _downloadThread = null;
            _rasterMap.Paint(_mapGraphics);
            picMapCanvas.Image = (Image)_mapImage.GetNativeImage();
            UpdateStatus("");
 
    
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            var center = new GeoLatLng(32.0176410, 118.7273120);
            _rasterMap.SetCenter(center, 2, _rasterMap.GetMapType());
            btnReset_Click(sender,e);
        }

        private void MainWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            _mapTileDownloadManager.Stop();
           if(_downloadThread!=null)
            {
                _downloadThread.Abort();
            }
            if(_tileIndexThread!=null)
            {
                _tileIndexThread.Abort();
            }
           
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            _rasterMap.PanDirection(0,64);
        }

        private void btnDown_Click(object sender, EventArgs e)
        {

            _rasterMap.PanDirection(0, -64);
        }

        private void btnLeft_Click(object sender, EventArgs e)
        {
            _rasterMap.PanDirection(64, 0);
        }

        private void btnRight_Click(object sender, EventArgs e)
        {
            _rasterMap.PanDirection(-64, 0);
        }

       
        private void btnZoomIn_Click(object sender, EventArgs e)
        {
            _rasterMap.ZoomIn();
  
        }

        private void btnZoomOut_Click(object sender, EventArgs e)
        {
            _rasterMap.ZoomOut();
  
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtAddress.Text))
            {
                _rasterMap.GetLocations(txtAddress.Text);

            }
            
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            GeoLatLng latLng = _rasterMap.FromScreenPixelToLatLng(_topLeft);
            GeoPoint pt = MapLayer.FromLatLngToPixel(latLng, _rasterMap.GetZoom());
            _rasterMap.PanDirection((int)(pt.X % 256), (int)(pt.Y % 256));
        }

        private void txtX1_TextChanged(object sender, EventArgs e)
        {
            ResetSelectedArea();
        }

        private void ResetSelectedArea()
        {
            try
            {
                int x1 = int.Parse(txtX1.Text);
                int y1 = int.Parse(txtY1.Text);
                int x2 = int.Parse(txtX2.Text);
                int y2 = int.Parse(txtY2.Text);
                _rasterMap.SelectedMapTileArea = new GeoBounds(x1, y1, Math.Abs(x2 - x1)+1, Math.Abs(y2 - y1)+1);
                _rasterMap.SetCenter(_rasterMap.GetScreenCenter(),_rasterMap.GetZoom());
                
            }catch
            {
            }
        }

        private void txtX2_TextChanged(object sender, EventArgs e)
        {
            ResetSelectedArea();
        }

        private void txtY1_TextChanged(object sender, EventArgs e)
        {
            ResetSelectedArea();
        }

        private void txtY2_TextChanged(object sender, EventArgs e)
        {
            ResetSelectedArea();
        }

        private void picMapCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if(e.Button==MouseButtons.Left)
            {
                _rasterMap.PanDirection(e.X-_oldX,e.Y-_oldY);

            }
            else if (e.Button == MouseButtons.Right)
            {
                _rasterMap.SetSelectedArea(_selectX, _selectY, Math.Abs(e.X - _selectX), Math.Abs(e.Y - _selectY));
                txtX1.Text = ((int)_rasterMap.SelectedMapTileArea.X).ToString();
                txtY1.Text = ((int)_rasterMap.SelectedMapTileArea.Y).ToString();
                txtX2.Text = ((int)(_rasterMap.SelectedMapTileArea.X + _rasterMap.SelectedMapTileArea.Width-1)).ToString();
                txtY2.Text = ((int)(_rasterMap.SelectedMapTileArea.Y + _rasterMap.SelectedMapTileArea.Height-1)).ToString();
                lblMessage.Text = "Totally " + CalculateHowManyTiles() + " tiles will be downloaded!";
            }
            _oldX = e.X;
            _oldY = e.Y;

        }

        private void picMapCanvas_MouseDown(object sender, MouseEventArgs e)
        {
            
            _oldX = e.X;
            _oldY = e.Y;
            _selectX = e.X;
            _selectY = e.Y;
        }

        private delegate void UpdateMessageDelegate(string message,long index);
        private int _messageLine;


        private void UpdateMessage(string message,long index)
        {
            txtMessage.Text = txtMessage.Text + "\r\n" + message;
            _messageLine++;
            if (_messageLine > 8)
            {
                _messageLine = 0;
                txtMessage.Text = "";
            }
            if (_totalCount!=0)
            progressDownload.Value =(int) (index*100/_totalCount);
 
        }




        public void AddMessage(string message,long index)
        {
            if (!this.Disposing)
            {
                if (txtMessage.InvokeRequired)
                {
                    txtMessage.Invoke(new UpdateMessageDelegate(UpdateMessage), new object[] {message, index});
                }
                else
                {
                    UpdateMessage(message, index);
                }
            }
        }


        
       

        public void Progress(long index, int x, int y, int z, bool failed)
        {
            lock (_tileIndexList)
            {
                var mapTileIndex = new MapTileIndex { XIndex = x, YIndex = y, ZoomLevel = z };
                _tileIndexList.Add(mapTileIndex);

            }
            AddMessage("Downloaded " + (index + 1) + "," + "X=" + x + ",Y=" + y + ",Z=" + z + (!failed ? " OK" : " Fail"),index);
        }

        private void FinishDownloading()
        {
            if(InvokeRequired)
            {
                BeginInvoke(new DoneWithDownloading(FinishDownloading));
            }else
            {
                AddMessage("Finished writing!!", _totalCount);
                btnStart.Enabled = true;
                btnStop.Enabled = false;
                _downloadThread = null; 
            }
            stopThread = true;
            if (_tileIndexThread != null)
            {
                _tileIndexThread.Abort();
                _tileIndexThread = null;
                stopThread = false;
            }
        }

        public void FinishWriting()
        {
            FinishDownloading();
            
        }

        private void WriteToMapFile()
        {
            var startX = (int)_rasterMap.SelectedMapTileArea.X;
            var startY = (int)_rasterMap.SelectedMapTileArea.Y;
            var endX = (int)(_rasterMap.SelectedMapTileArea.X + _rasterMap.SelectedMapTileArea.Width-1);
            var endY = (int)(_rasterMap.SelectedMapTileArea.Y + _rasterMap.SelectedMapTileArea.Height-1);

            _mapTileWrite = new MapTileWriter.MapTileWriter(startX, startY, endX, endY, _rasterMap.GetZoom(), _rasterMap.GetMapType(),
                                                           _mapTileDownloadManager)
                                {ZoomLevelSelected = _mapZoomLevelSelected};

            _mapTileWrite.SetWritingProgressListener(this);
    
            _mapTileWrite.WriteMapTileFile();  
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            btnStart.Enabled = false;
            btnStop.Enabled = true;

            _downloadThread = new Thread(WriteToMapFile);
            _downloadThread.Start();
            _tileIndexThread = new Thread(ProcessTileIndex);
            _tileIndexThread.Start();
            
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            var openFileDialog1 = new OpenFileDialog
                                                 {
                                                     InitialDirectory = "c:\\",
                                                     Filter = "map files (*.map)|*.map|All files (*.*)|*.*",
                                                     FilterIndex = 2,
                                                     RestoreDirectory = true
                                                 };

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    _mapTileDownloadManager.Stop();
                    GC.Collect();
                    string fileName = openFileDialog1.FileName;
                    var localMapTileFileReader = new MapTileStoredDataSource(fileName);
                    
                    _mapTileDownloadManager = new MapTileDownloadManager(this, localMapTileFileReader);
                    _mapTileDownloadManager.Start();
                    GeoLatLng center = _rasterMap.GetScreenCenter();
                    int zoom = _rasterMap.GetZoom();

                    _rasterMap = new RasterMap(768, 768, _mapType, _mapTileDownloadManager);
                    _rasterMap.SetCenter(center, zoom);
                    _rasterMap.SetMapDrawingListener(this);
                    _rasterMap.SetGeocodingListener(this);
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }

        }

        private void btnServer_Click(object sender, EventArgs e)
        {
            _mapTileDownloadManager.Stop();
            _mapTileDownloadManager = new MapTileDownloadManager(this);
            _mapTileDownloadManager.Start();
            GeoLatLng center = _rasterMap.GetScreenCenter();
            int zoom = _rasterMap.GetZoom();

            _rasterMap = new RasterMap(768, 768, _mapType, _mapTileDownloadManager);
            _rasterMap.SetCenter(center, zoom);
            _rasterMap.SetMapDrawingListener(this);
            _rasterMap.SetGeocodingListener(this);
        }



        private void chk2_CheckedChanged(object sender, EventArgs e)
        {
            _mapZoomLevelSelected[2] = chk2.Checked;
            lblMessage.Text = "Totally " + CalculateHowManyTiles() + " tiles will be downloaded!";

        }

        private void chk3_CheckedChanged(object sender, EventArgs e)
        {
            _mapZoomLevelSelected[3] = chk3.Checked;
            lblMessage.Text = "Totally " + CalculateHowManyTiles() + " tiles will be downloaded!";
        }

        private void chk4_CheckedChanged(object sender, EventArgs e)
        {
            _mapZoomLevelSelected[4] = chk4.Checked;
            lblMessage.Text = "Totally " + CalculateHowManyTiles() + " tiles will be downloaded!";
        }

        private void chk5_CheckedChanged(object sender, EventArgs e)
        {
            _mapZoomLevelSelected[5] = chk5.Checked;
            lblMessage.Text = "Totally " + CalculateHowManyTiles() + " tiles will be downloaded!";
        }

        private void chk6_CheckedChanged(object sender, EventArgs e)
        {
            _mapZoomLevelSelected[6] = chk6.Checked;
            lblMessage.Text = "Totally " + CalculateHowManyTiles() + " tiles will be downloaded!";
        }

        private void chk7_CheckedChanged(object sender, EventArgs e)
        {
            _mapZoomLevelSelected[7] = chk7.Checked;
            lblMessage.Text = "Totally " + CalculateHowManyTiles() + " tiles will be downloaded!";
        }

        private void chk8_CheckedChanged(object sender, EventArgs e)
        {
            _mapZoomLevelSelected[8] = chk8.Checked;
            lblMessage.Text = "Totally " + CalculateHowManyTiles() + " tiles will be downloaded!";
        }

        private void chk9_CheckedChanged(object sender, EventArgs e)
        {
            _mapZoomLevelSelected[9] = chk9.Checked;
            lblMessage.Text = "Totally " + CalculateHowManyTiles() + " tiles will be downloaded!";
        }

        private void chk10_CheckedChanged(object sender, EventArgs e)
        {
            _mapZoomLevelSelected[10] = chk10.Checked;
            lblMessage.Text = "Totally " + CalculateHowManyTiles() + " tiles will be downloaded!";
        }

        private void chk11_CheckedChanged(object sender, EventArgs e)
        {
            _mapZoomLevelSelected[11] = chk11.Checked;
            lblMessage.Text = "Totally " + CalculateHowManyTiles() + " tiles will be downloaded!";
        }

        private void chk12_CheckedChanged(object sender, EventArgs e)
        {
            _mapZoomLevelSelected[12] = chk12.Checked;
            lblMessage.Text = "Totally " + CalculateHowManyTiles() + " tiles will be downloaded!";
        }

        private void chk13_CheckedChanged(object sender, EventArgs e)
        {
            _mapZoomLevelSelected[13] = chk13.Checked;
            lblMessage.Text = "Totally " + CalculateHowManyTiles() + " tiles will be downloaded!";
        }

        private void chk14_CheckedChanged(object sender, EventArgs e)
        {
            _mapZoomLevelSelected[14] = chk14.Checked;
            lblMessage.Text = "Totally " + CalculateHowManyTiles() + " tiles will be downloaded!";
        }

        private void chk15_CheckedChanged(object sender, EventArgs e)
        {
            _mapZoomLevelSelected[15] = chk15.Checked;
            lblMessage.Text = "Totally " + CalculateHowManyTiles() + " tiles will be downloaded!";
        }

        private void chk16_CheckedChanged(object sender, EventArgs e)
        {
            _mapZoomLevelSelected[16] = chk16.Checked;
            lblMessage.Text = "Totally " + CalculateHowManyTiles() + " tiles will be downloaded!";
        }

        private void chk17_CheckedChanged(object sender, EventArgs e)
        {
            _mapZoomLevelSelected[17] = chk17.Checked;
            lblMessage.Text = "Totally " + CalculateHowManyTiles() + " tiles will be downloaded!";
        }

        public long CalculateHowManyTiles()
        {
            
            try
            {
                int startIndexX = (int)_rasterMap.SelectedMapTileArea.X;
                int startIndexY = (int)_rasterMap.SelectedMapTileArea.Y;
                int endIndexX = (int)(_rasterMap.SelectedMapTileArea.X + _rasterMap.SelectedMapTileArea.Width);
                int endIndexY = (int)(_rasterMap.SelectedMapTileArea.Y + _rasterMap.SelectedMapTileArea.Height);
                int howManyLevel = 0;
                int selectedMapIndex = (endIndexX  - startIndexX)*(endIndexY - startIndexY);
                int zoomLevel = _rasterMap.GetZoom();
                for (int level = zoomLevel; level < 18; level++)
                {
                    if (_mapZoomLevelSelected[level])
                    {
                        howManyLevel += (int) Math.Pow(4, level - zoomLevel);
                    }
                }

                long howMayTiles = howManyLevel * selectedMapIndex;
                _totalCount = howMayTiles;
                return howMayTiles;
            }catch
            {
                
            }
               
            return 0;

        }

        private void cboMapType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strType = cboMapType.Text;
            object mtype = MapType.MAP_TYPE_NAMES[strType];
            if(mtype!=null)
            {
                _mapType = (int) mtype;
                _rasterMap.SetMapType(_mapType);
            }
        }



        private void btnStop_Click(object sender, EventArgs e)
        {
            if (_downloadThread != null)
            {
                _downloadThread.Abort();
                _downloadThread = null;
            }
            if (_tileIndexThread != null)
            {
                _tileIndexThread.Abort();
                _tileIndexThread = null;
            }
            btnStart.Enabled = true;
            btnStop.Enabled = false;
          
            
        }
    }
}
