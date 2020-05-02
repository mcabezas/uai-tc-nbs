using System.Net;

namespace Http.Router
{
    public static class ResponseWriter
    {
        public static void Write(HttpListenerResponse response, HttpStatusCode status, string body)
        {
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(body);
            response.ContentLength64 = buffer.Length;
            System.IO.Stream output = response.OutputStream;
            response.StatusCode = (int)status;
            output.Write(buffer, 0, buffer.Length);
            output.Close();
        }

        public static void Write(HttpListenerResponse response, string body)
        {
            Write(response, HttpStatusCode.OK, body);
        }

    }
}