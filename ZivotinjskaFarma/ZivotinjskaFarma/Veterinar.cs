using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZivotinjskaFarma
{
    public interface IVeterinar
    {
        double ocjenaZdravstvenogStanjaZivotinje(Zivotinja zivotinja);
    }
    
    public class Veterinar : IVeterinar
    {
        public double ocjenaZdravstvenogStanjaZivotinje(Zivotinja zivotinja)
        {
            throw new NotImplementedException();
        }
    }

    public class Spy: IVeterinar
    {
        public double ocjenaZdravstvenogStanjaZivotinje(Zivotinja zivotinja)
        {
            if (zivotinja.Vrsta == ZivotinjskaVrsta.Patka)
                return 5;
            return 4;
            //throw new NotImplementedException();
        }
    }
}
