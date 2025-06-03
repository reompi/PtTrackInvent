using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackInvent
{
    public class LoginResult
    {
        public bool Success { get; set; }
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cargo { get; set; }
    }
    public static class SessaoAtual
    {
        public static int Id { get; set; }
        public static string Nome { get; set; }
        public static string Cargo { get; set; }

        public static void Limpar()
        {
            Id = 0;
            Nome = null;
            Cargo = null;
        }
    }
}
