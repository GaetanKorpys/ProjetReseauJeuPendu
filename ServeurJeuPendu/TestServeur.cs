using ServerJeuPendu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServeurJeuPendu
{
    internal class TestServeur
    {
        static void Main(string[] args)
        {
            Serveur serveur = new Serveur();
            serveur.ExecuteServeur();
        }
    }
}
