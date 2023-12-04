using Confluent.Kafka;
using System;

class Program
{
    static void Main()
    {
        var config = new ProducerConfig
        {
            BootstrapServers = "trabalho-receitas.servicebus.windows.net:9093",
            SaslMechanism = SaslMechanism.Plain, // Define o mecanismo de autenticação como Plain
            SecurityProtocol = SecurityProtocol.SaslPlaintext, // Define o protocolo de segurança como SaslPlaintext
            SaslUsername = "$ConnectionString", // Substitua pelo seu nome de usuário
            SaslPassword = "Endpoint=sb://trabalho-", // Substitua pela sua senha
        };

        using (var producer = new ProducerBuilder<Null, string>(config).Build())
        {
            string topic = "app-receitas";

            var message = new Message<Null, string>
            {
                Value = "[Daniel];[daniel@email];[Nova Receita];[Nova receita adicionada ao site]",
            };

            var deliveryReport = producer.ProduceAsync(topic, message).Result;

            Console.WriteLine($"Enviado para a partição: {deliveryReport.Partition}, Offset: {deliveryReport.Offset}");
        }
    }
}