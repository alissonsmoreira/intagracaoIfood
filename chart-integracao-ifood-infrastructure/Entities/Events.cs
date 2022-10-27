using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace chart_integracao_ifood_infrastructure.Entities
{
    [Table("events")]
    public class Events
    {
        [Column("id")]
        public string Id { get; set; }

        [Column("orderid")]
        public string OrderId { get; set; }
        
        [Column("code")]
        public string Code { get; set; }
        
        [Column("fullcode")]
        public string FullCode { get; set; }
        
        [Column("createdat")]
        public DateTime CreatedAt { get; set; }
        
        [Column("acknowledged")]
        public bool Acknowledged { get; set; }
        
        [Column("processed")]
        public bool Processed { get; set; }
        
        [Column("sended")]
        public bool Sended { get; set; }

        [Column("payload")]
        public string Payload { get; set; }
    }
}