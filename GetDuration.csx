using System.Net;

public static async Task<HttpResponseMessage> Run(HttpRequestMessage req, TraceWriter log)
{
    log.Info("Starting Function");

    string start = req.GetQueryNameValuePairs()
        .FirstOrDefault(q => string.Compare(q.Key, "start", true) == 0)
        .Value;

    if (start == null)
    {
        dynamic data = await req.Content.ReadAsAsync<object>();
        start = data?.start;
    }

    log.Info("Start: " + start);

    string end = req.GetQueryNameValuePairs()
        .FirstOrDefault(q => string.Compare(q.Key, "end", true) == 0)
        .Value;

    if (end == null)
    {
        dynamic data = await req.Content.ReadAsAsync<object>();
        end = data?.end;
    }

   log.Info("End: " + end);

    if (string.IsNullOrEmpty(start) || string.IsNullOrEmpty(end))
        return req.CreateResponse(HttpStatusCode.OK, "00:00:00.000");


    DateTime dStart = DateTime.Parse(start);
    DateTime dEnd = DateTime.Parse(end);

    TimeSpan duration = dEnd - dStart;

    log.Info("Duration: " + duration);

    return req.CreateResponse(HttpStatusCode.OK, duration);
}