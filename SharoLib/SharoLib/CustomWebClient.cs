using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;


public class CustomWebClient : WebClient
{
    private static CookieContainer cookieContainer = new CookieContainer();

    public CustomWebClient()
    {
        GUID = Guid.NewGuid().ToString();
    }

    public string Referer { get; set; }
    public string GUID { get; set; }
    public static event NewReqHandler NewReq;
    public static event WCProgressChengHandler ProgressChenge;

    // WebClientはWebRequestのラッパーにすぎないので、
    // GetWebRequestのところの動作をちょっと横取りして書き換える
    protected override WebRequest GetWebRequest(Uri address)
    {
        var request = base.GetWebRequest(address);
        if (request is HttpWebRequest)
        {
            (request as HttpWebRequest).CookieContainer = cookieContainer;
            (request as HttpWebRequest).Referer = this.Referer;
            (request as HttpWebRequest).UserAgent = "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.1; WOW64; Trident/6.0)";
        }
        return request;
    }
    protected override void OnDownloadProgressChanged(DownloadProgressChangedEventArgs e)
    {
        if (ProgressChenge != null)
            ProgressChenge(GUID, (int)e.TotalBytesToReceive, (int)e.BytesReceived);

        base.OnDownloadProgressChanged(e);
    }
    protected override WebResponse GetWebResponse(WebRequest request)
    {
        if (NewReq != null)
            NewReq(request.RequestUri.ToString(), GUID);

        while (true)
        {
            try
            {
                var res = base.GetWebResponse(request);
                return res;
            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ReceiveFailure ||
                    ex.Status == WebExceptionStatus.ConnectionClosed ||
                    ex.Status == WebExceptionStatus.Timeout ||
                    ex.Status == WebExceptionStatus.ConnectFailure)
                {
                    Console.WriteLine("{0}しました。5秒後に再試行します", ex.Status);
                    GetWebRequest(request.RequestUri);
                    System.Threading.Thread.Sleep(5000);
                    continue;
                }

                throw ex;
            }
        }
    }
}
public delegate void NewReqHandler(string url, string guid);
public delegate void WCProgressChengHandler(string guid, int max, int value);