//------------------------------------------------------------------------------
//                         COPYRIGHT 2009 GUIDEBEE
//                           ALL RIGHTS RESERVED.
//                     GUIDEBEE CONFIDENTIAL PROPRIETARY 
///////////////////////////////////// REVISIONS ////////////////////////////////
// Date       Name                 Tracking #         Description
// ---------  -------------------  ----------         --------------------------
// 12JUN2009  James Shen                 	          Initial Creation
////////////////////////////////////////////////////////////////////////////////
//--------------------------------- IMPORTS ------------------------------------
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using MapDigit.AJAX.JSON;

//--------------------------------- PACKAGE ------------------------------------
namespace MapDigit.AJAX
{
    //[-------------------------- MAIN CLASS ----------------------------------]
    ////////////////////////////////////////////////////////////////////////////
    //----------------------------- REVISIONS ----------------------------------
    // Date       Name                 Tracking #         Description
    // --------   -------------------  -------------      ----------------------
    // 12JUN2009  James Shen                 	          Initial Creation
    ////////////////////////////////////////////////////////////////////////////
    /**
     * With Requst object, application can issue a asynchronous http requst to a 
     * server and Requst handles the message in a seperate thread.
     * <hr><b>&copy; Copyright 2009 Guidebee, Inc. All Rights Reserved.</b>
     * @version     1.00, 12/06/09
     * @author      Guidebee, Inc.
     */
    public sealed class Request
    {

        /**
         * UTF-8 encoding. (Charset value)
         */
        public const string UTF8_CHARSET = "utf-8";

        /**
         * ISO-8859-1 encoding.(Charset value)
         */
        public const string ISO8859_CHARSET = "iso-8859-1";

        /**
         * GB2312 encoding.(Charset value)
         */
        public const string GB2312_CHARSET = "gb2312";

        /**
         * total bytes downloaded
         */
        public static long TotaldownloadedBytes;


        private const string GET = "GET";

        private const string POST = "POST";


        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 12JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Issue a synchronous GET requst.
         * @param url Http request url.
         * @param inputArgs  argument of for the url.
         * @param httpArgs extra http header.
         * @param listener RequestLister used to handle the sync http response.
         * @return the http response object.
         * @ any IOException.
         */
        public static Response Get(string url,
                 Arg[] inputArgs,
                 Arg[] httpArgs,
                 IRequestListener listener)
        {
            return Sync(GET, url, inputArgs, httpArgs,
                    listener, null);
        }

        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 12JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Issue a asynchronous GET requst.
         * @param url Http request url.
         * @param inputArgs  argument of for the url.
         * @param httpArgs extra http header.
         * @param listener RequestLister used to handle the async http response.
         * @param context message context ,wiil pass as the same in done().
         */
        public static void Get(
                 string url,
                 Arg[] inputArgs,
                 Arg[] httpArgs,
                 IRequestListener listener,
                 object context)
        {

            Async(GET, url, inputArgs, httpArgs, listener, null,
                    context);
        }

        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 12JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Issue a synchronous POST requst.
         * @param url Http request url.
         * @param inputArgs  argument of for the url.
         * @param httpArgs extra http header.
         * @param listener RequestLister used to handle the sync http response.
         * @param multiPart message body.
         * @return the http response object.
         * @ any IOException
         */
        public static Response Post(string url,
                 Arg[] inputArgs,
                 Arg[] httpArgs,
                 IRequestListener listener,
                 PostData multiPart)
        {

            return Sync(POST, url, inputArgs, httpArgs, listener,
                    multiPart);
        }

        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 12JUN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Issue an asynchronous POST requst.
         * @param url Http request url.
         * @param inputArgs  argument of for the url.
         * @param httpArgs extra http header.
         * @param listener RequestLister used to handle the async http response.
         * @param multiPart message body.
         * @param context message context ,wiil pass as the same in done().
         */
        public static void Post(
                 string url,
                 Arg[] inputArgs,
                 Arg[] httpArgs,
                 IRequestListener listener,
                 PostData multiPart,
                 object context)
        {

            Async(POST, url, inputArgs, httpArgs, listener,
                    multiPart, context);
        }

        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 01JAN2009  James Shen                 	          Initial Creation
        ////////////////////////////////////////////////////////////////////////////
        /**
         * Cancel this http requst.
         */
        public void Cancel()
        {
            _interrupted = true;
            Thread.Sleep(3000);
            if (_thread != null)
            {
                _thread.Interrupt();
            }
        }
        //--------------------------------- REVISIONS ------------------------------
        // Date       Name                 Tracking #         Description
        // ---------  -------------------  -------------      ----------------------
        // 12JUN2009  James Shen                 	          Initial Creation 
        ////////////////////////////////////////////////////////////////////////////
        /**
         *  Start a thread to process this http request/response, application shall
         *  not call this function directly.
         */
        private void Run()
        {
            var response = new Response();
            try
            {
                DoHTTP(response);
            }
            catch (Exception ex)
            {
                response._ex = ex;
            }
            finally
            {
                if (_listener != null)
                {
                    try
                    {
                        _listener.Done(_context, response);
                    }
                    catch
                    {

                    }
                }
            }
        }

        private const int BUFFER_SIZE = 512;
        private object _context;
        private string _url;
        private string _method;
        private Arg[] _httpArgs;
        private Arg[] _inputArgs;
        private PostData _multiPart;
        private IRequestListener _listener;
        private Thread _thread;
        private volatile bool _interrupted;
        private int _totalToSend;
        private int _totalToReceive;
        private int _sent;
        private int _received;
        private readonly UTF8Encoding _utf8 = new UTF8Encoding();


        private static Response Sync(
                 string method,
                 string url,
                 Arg[] inputArgs,
                 Arg[] httpArgs,
                 IRequestListener listener,
                 PostData multiPart)
        {

            var request = new Request
                              {
                                  _method = method,
                                  _url = url,
                                  _httpArgs = httpArgs,
                                  _inputArgs = inputArgs,
                                  _multiPart = multiPart,
                                  _listener = listener
                              };

            var response = new Response();
            request.DoHTTP(response);
            return response;
        }

        private static void Async(
                 string method,
                 string url,
                 Arg[] inputArgs,
                 Arg[] httpArgs,
                 IRequestListener listener,
                 PostData multiPart,
                 object context)
        {

            var request = new Request
                              {
                                  _method = method,
                                  _context = context,
                                  _listener = listener,
                                  _url = url,
                                  _httpArgs = httpArgs,
                                  _inputArgs = inputArgs,
                                  _multiPart = multiPart
                              };

            //strategies
            request._thread = new Thread(request.Run);
            request._thread.Start();
        }

        private Request()
        {
        }

        // data may be large, send in chunks while reporting progress and checking for interruption
        private void Write(Stream os, byte[] data)
        {

            if (_interrupted)
            {
                return;
            }

            // optimization if a small amount of data is being sent
            if (data.Length <= BUFFER_SIZE)
            {
                os.Write(data, 0, data.Length);
                _sent += data.Length;
                if (_listener != null)
                {
                    try
                    {
                        _listener.WriteProgress(_context, _sent, _totalToSend);
                    }
                    catch
                    {

                    }
                }
            }
            else
            {
                var offset = 0;
                int length;
                do
                {
                    length = Math.Min(BUFFER_SIZE, data.Length - offset);
                    if (length > 0)
                    {
                        os.Write(data, offset, length);
                        offset += length;
                        _sent += length;
                        if (_listener != null)
                        {
                            try
                            {
                                _listener.WriteProgress(_context, _sent, _totalToSend);
                            }
                            catch
                            {

                            }
                        }
                    }
                } while (!_interrupted && length > 0);
            }
        }


        private void DoHTTP(Response response)
        {

            var args = new StringBuilder();
            if (_inputArgs != null)
            {
                if (_inputArgs.Length > 0)
                {
                    for (var i = 0; i < _inputArgs.Length; i++)
                    {
                        if (_inputArgs[i] != null)
                        {
                            args.Append(Encode(_inputArgs[i].GetKey()));
                            args.Append("=");
                            args.Append(Encode(_inputArgs[i].GetValue()));
                            if (i + 1 < _inputArgs.Length &&
                                    _inputArgs[i + 1] != null)
                            {
                                args.Append("&");
                            }
                        }
                    }
                }
            }

            var location = new StringBuilder(_url);
            if (GET.Equals(_method) && args.Length > 0)
            {
                location.Append("?");
                location.Append(args.ToString());
            }

            HttpWebRequest conn = (HttpWebRequest)WebRequest.Create(location.ToString());
            conn.Proxy = WebRequest.DefaultWebProxy;
            conn.Proxy.Credentials = CredentialCache.DefaultCredentials;
            conn.Method = _method;
            if (_httpArgs != null)
            {
                for (int i = 0; i < _httpArgs.Length; i++)
                {
                    if (_httpArgs[i] != null)
                    {
                        var value = _httpArgs[i].GetValue();
                        if (value != null)
                        {
                            conn.Headers.Add(_httpArgs[i].GetKey(), value);
                        }
                    }
                }
            }

            if (_interrupted)
            {
                return;
            }

            if (POST.Equals(_method))
            {
                Stream os = null;
                try
                {
                    os = conn.GetRequestStream();
                    WritePostData(args, os);
                }
                finally
                {
                    if (os != null)
                    {
                        try
                        {
                            os.Close();
                        }
                        catch (IOException)
                        {
                        }
                    }
                }
            }

            if (_interrupted)
            {
                return;
            }

            var httpWResp = (HttpWebResponse)conn.GetResponse();
            response._responseCode = httpWResp.StatusCode;
            CopyResponseHeaders(httpWResp, response);

            if (response._responseCode != HttpStatusCode.OK)
            {
                return;
            }

            if (_interrupted)
            {
                return;
            }

            ProcessContentType(httpWResp, response);
            ReadResponse(httpWResp, response);
        }

        private void WritePostData(StringBuilder args, Stream os)
        {
            if (_multiPart != null)
            {
                var multipartBoundaryBits = _utf8.GetBytes(_multiPart.GetBoundary());
                var newline = _utf8.GetBytes("\r\n");
                var dashdash = _utf8.GetBytes("--");

                // estimate totalBytesToSend
                var parts = _multiPart.GetParts();
                for (var i = 0; i < parts.Length; i++)
                {
                    var headers = parts[i].GetHeaders();
                    for (var j = 0; j < headers.Length; j++)
                    {
                        _totalToSend += _utf8.GetBytes(headers[j].GetKey()).Length;
                        _totalToSend += _utf8.GetBytes(headers[j].GetValue()).Length;
                        _totalToSend += multipartBoundaryBits.Length + dashdash.Length + 3 * newline.Length;
                    }
                    _totalToSend += parts[i].GetData().Length;
                }
                // closing boundary marker
                _totalToSend += multipartBoundaryBits.Length + 2 * dashdash.Length + 2 * newline.Length;

                for (var i = 0; i < parts.Length && !_interrupted; i++)
                {
                    Write(os, newline);
                    Write(os, dashdash);
                    Write(os, multipartBoundaryBits);
                    Write(os, newline);

                    var wroteAtleastOneHeader = false;
                    var headers = parts[i].GetHeaders();
                    for (var j = 0; j < headers.Length; j++)
                    {
                        Write(os, _utf8.GetBytes(headers[j].GetKey() + ": " + headers[j].GetValue()));
                        Write(os, newline);
                        wroteAtleastOneHeader = true;
                    }
                    if (wroteAtleastOneHeader)
                    {
                        Write(os, newline);
                    }

                    Write(os, parts[i].GetData());
                }

                // closing boundary marker
                Write(os, newline);
                Write(os, dashdash);
                Write(os, multipartBoundaryBits);
                Write(os, dashdash);
                Write(os, newline);
            }
            else if (_inputArgs != null)
            {
                var argBytes = _utf8.GetBytes(args.ToString());
                _totalToSend = argBytes.Length;
                Write(os, argBytes);
            }
            else
            {
                throw new IOException("No data to POST -" +
                        " either input args or multipart must be non-null");
            }
        }





        private void ReadResponse(WebResponse httpWResp,
                 Response response)
        {


            _totalToReceive = (int)httpWResp.ContentLength;
            if (_totalToReceive > 0)
            {
                TotaldownloadedBytes += _totalToReceive;
            }
            _received = 0;
            var cbuf = new byte[BUFFER_SIZE];
            MemoryStream bos = null;
            Stream stream = null;
            try
            {
                stream = httpWResp.GetResponseStream();
                bos = new MemoryStream();
                int nread;
                while ((nread = stream.Read(cbuf, 0, BUFFER_SIZE)) > 0 && !_interrupted)
                {
                    bos.Write(cbuf, 0, nread);
                    _received += nread;
                    if (_listener != null)
                    {
                        try
                        {
                            _listener.ReadProgress(_context, _received, _totalToReceive);
                        }
                        catch (Exception)
                        {

                        }
                    }
                }
            }
            finally
            {
                if (stream != null)
                {
                    stream.Close();
                }
                if (bos != null)
                {
                    bos.Close();
                }
            }

            if (_interrupted)
            {
                return;
            }

            if (_totalToReceive < 0)
            {
                TotaldownloadedBytes += _received;
            }
            var content = _utf8.GetString(bos.GetBuffer());
            try
            {
                response._result = Result.FromContent(content, response._contentType);
                if (_listener != null)
                {
                    try
                    {
                        if (response.GetCode() == HttpStatusCode.OK)
                        {
                            _listener.Done(_context, content);
                        }
                        else
                        {
                            _listener.Done(_context, "");
                        }
                    }
                    catch (Exception)
                    {

                    }
                }
            }
            catch (JSONException e)
            {
                throw new IOException(e.Message);
            }
        }

        private static void CopyResponseHeaders(WebResponse conn,
                 Response response)
        {

            response._headers = new Arg[conn.Headers.Count];
            for (int i = 0; i < conn.Headers.Count; i++)
            {
                string[] values = conn.Headers.GetValues(i);
                if (values != null)
                {
                    response._headers[i] = new Arg(conn.Headers.GetKey(i), values[0]);
                }
            }


        }

        private static void ProcessContentType(WebResponse conn,
                 Response response)
        {

            response._contentType = conn.ContentType;
            if (response._contentType == null)
            {
                // assume UTF-8 and XML if not specified
                response._contentType = Result.APPLICATION_XML_CONTENT_TYPE;
                response._charset = UTF8_CHARSET;
                return;
            }
            var semi = response._contentType.IndexOf(';');
            if (semi >= 0)
            {
                response._charset = response._contentType.Substring(semi + 1).Trim();
                var eq = response._charset.IndexOf('=');
                if (eq < 0)
                {
                    throw new IOException("Missing charset value: " + response._charset);
                }
                response._charset = Unquote(response._charset.Substring(eq + 1).Trim());
                response._contentType = response._contentType.Substring(0, semi).Trim();
            }
            if (response._charset != null)
            {
                var charset = response._charset.ToLower();
                if (!(charset.StartsWith(UTF8_CHARSET) ||
                        charset.EndsWith(UTF8_CHARSET) ||
                        charset.StartsWith(ISO8859_CHARSET) ||
                        charset.EndsWith(ISO8859_CHARSET) ||
                        charset.StartsWith(GB2312_CHARSET) ||
                        charset.EndsWith(GB2312_CHARSET)))
                {
                    throw new IOException("Unsupported charset: " + response._charset);
                }
            }

        }

        private static string Unquote(string str)
        {
            if (str.StartsWith("\"") && str.StartsWith("\"") ||
                    str.StartsWith("'") && str.EndsWith("'"))
            {
                return str.Substring(1, str.Length - 1);
            }
            return str;
        }


        private static string Encode(string str)
        {
            if (str == null)
            {
                return null;
            }
            return str.Replace(' ', '+');
        }
    }
}
