namespace Api
{
    public class RequestResponse
    {
        public static Response Success()
        {
            return new Response(true, "Opração realizada com sucesso!");
        }
        public static Response Success(string mensagem)
        {
            return new Response(true, mensagem);
        }

        public static Response Error()
        {
            return new Response(false, "Falha ao realizar operação!");
        }
        public static Response Error(string mensagem)
        {
            return new Response(false, mensagem);
        }
    }

    public partial class Response
    {
        public Response(bool success, string message)
        {
            this.Success = success;
            this.Message = message;
        }

        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
