using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetFileDownload
{
    public class JogoQuina
    {
        public string concurso { get; set;}
        public string data { get; set; }
		public string number1{ get; set;}
		public string number2{ get; set;}
		public string number3{ get; set;}
		public string number4{ get; set;}
		public string number5{ get; set;}
		public string arrecadacaoTotal { get; set;}
		public string ganhadoresQuina { get; set;}
		public string cidade { get; set;}
		public string uf { get; set;}
		public string rateioquina { get; set;}
		public string ganhadoresQuadra { get; set;}
		public string rateioQuadra { get; set;}
		public string ganhadoresTerno { get; set;}
		public string rateioTerno { get; set;}
		public string acumulado { get; set;}
		public string estimativaPremio { get; set;}
		public string valorAcumaldo { get; set;}
		public string sorteioEspecial { get; set;}


    }
	public class ListaJogoQuina :List<JogoQuina>
	{
		
	}
}
