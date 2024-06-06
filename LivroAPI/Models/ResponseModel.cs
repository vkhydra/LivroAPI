namespace LivroAPI.Models
{
    public class ResponseModel<T>
    {
        // O tipo de dado que será retornado.
        public T? Dados { get; set; }
        // Se não for passado nada, a mensagem será vazia.
        public string Mensagem { get; set; } = string.Empty;
        // Se não for passado nada, o status será true.
        public bool Status { get; set; } = true;

    }
}
